using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
    public class AccidentDesciptionCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取事故说明
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_AccidentDesciptionC> GetAccidentDesciptionByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_AccidentDesciptionC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加事故说明
        /// </summary>
        /// <param name="accidentDesciption"></param>
        public static void AddAccidentDesciption(Model.Manager_Month_AccidentDesciptionC accidentDesciption)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_AccidentDesciptionC newAccidentDesciption = new Model.Manager_Month_AccidentDesciptionC
            {
                AccidentDesId = SQLHelper.GetNewID(typeof(Model.Manager_Month_AccidentDesciptionC)),
                MonthReportId = accidentDesciption.MonthReportId,
                Matter = accidentDesciption.Matter,
                MonthDataNum = accidentDesciption.MonthDataNum,
                YearDataNum = accidentDesciption.YearDataNum,
                SortIndex = accidentDesciption.SortIndex
            };
            db.Manager_Month_AccidentDesciptionC.InsertOnSubmit(newAccidentDesciption);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除事故说明
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteAccidentDesciptionByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_AccidentDesciptionC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_AccidentDesciptionC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        public static ListItem[] GetAccidentDesciptionList()
        {
            ListItem[] lis = new ListItem[13];
            lis[0] = new ListItem("未遂事件");
            lis[1] = new ListItem("人身伤亡事故");
            lis[2] = new ListItem("轻伤事故");
            lis[3] = new ListItem("重伤事故");
            lis[4] = new ListItem("死亡事故");
            lis[5] = new ListItem("火灾、爆炸事故");
            lis[6] = new ListItem("设备事故");
            lis[7] = new ListItem("环境事故");
            lis[8] = new ListItem("车辆事故");
            lis[9] = new ListItem("群体中毒、传染病事件");
            lis[10] = new ListItem("百万工时伤害率");
            lis[11] = new ListItem("伤害严重率");
            lis[12] = new ListItem("经济损失事故");
            return lis;
        }
    }
}
