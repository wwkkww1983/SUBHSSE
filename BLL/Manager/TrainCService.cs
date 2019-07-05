using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class TrainCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取HSSE培训
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_TrainC> GetTrainByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_TrainC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加HSSE培训
        /// </summary>
        /// <param name="train"></param>
        public static void AddTrain(Model.Manager_Month_TrainC train)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_TrainC newTrain = new Model.Manager_Month_TrainC
            {
                TrainId = SQLHelper.GetNewID(typeof(Model.Manager_Month_TrainC)),
                MonthReportId = train.MonthReportId,
                TrainName = train.TrainName,
                TrainMan = train.TrainMan,
                TrainDate = train.TrainDate,
                TrainObject = train.TrainObject,
                Remark = train.Remark,
                SortIndex = train.SortIndex
            };
            db.Manager_Month_TrainC.InsertOnSubmit(newTrain);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除HSSE培训
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteTrainByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_TrainC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_TrainC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
