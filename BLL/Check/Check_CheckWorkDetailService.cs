using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 开工前检查明细表
    /// </summary>
    public class Check_CheckWorkDetailService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据开工前检查id获取所有相关明细信息
        /// </summary>
        /// <param name="CheckRectifyId"></param>
        /// <returns></returns>
        public static List<Model.Check_CheckWorkDetail> GetCheckWorkDetailByCheckWorkId(string checkWorkId)
        {
            return (from x in Funs.DB.Check_CheckWorkDetail where x.CheckWorkId == checkWorkId select x).ToList();
        }

        /// <summary>
        /// 根据主键获取开工前检查明细信息
        /// </summary>
        /// <param name="checkWorkDetailId"></param>
        /// <returns></returns>
        public static Model.Check_CheckWorkDetail GetCheckWorkDetailByCheckWorkDetailId(string checkWorkDetailId)
        {
            return Funs.DB.Check_CheckWorkDetail.FirstOrDefault(e => e.CheckWorkDetailId == checkWorkDetailId);
        }

        /// <summary>
        /// 增加开工前检查明细信息
        /// </summary>
        /// <param name="checkWorkDetail"></param>
        public static void AddCheckWorkDetail(Model.Check_CheckWorkDetail checkWorkDetail)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckWorkDetail newCheckWorkDetail = new Model.Check_CheckWorkDetail
            {
                CheckWorkDetailId = checkWorkDetail.CheckWorkDetailId,
                CheckWorkId = checkWorkDetail.CheckWorkId,
                CheckItem = checkWorkDetail.CheckItem,
                CheckResult = checkWorkDetail.CheckResult,
                CheckOpinion = checkWorkDetail.CheckOpinion,
                CheckStation = checkWorkDetail.CheckStation,
                HandleResult = checkWorkDetail.HandleResult,
                CheckContent = checkWorkDetail.CheckContent,
                WorkArea = checkWorkDetail.WorkArea,
                SortIndex = checkWorkDetail.SortIndex,
            };
            db.Check_CheckWorkDetail.InsertOnSubmit(newCheckWorkDetail);
            db.SubmitChanges();

        }

        /// <summary>
        /// 修改开工前检查明细信息
        /// </summary>
        /// <param name="CheckWorkDetail"></param>
        public static void UpdateCheckWorkDetail(Model.Check_CheckWorkDetail CheckWorkDetail)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var newCheckWorkDetail = db.Check_CheckWorkDetail.FirstOrDefault(x => x.CheckWorkDetailId == CheckWorkDetail.CheckWorkDetailId);
            if (newCheckWorkDetail != null)
            {
                newCheckWorkDetail.CheckResult = CheckWorkDetail.CheckResult;
                newCheckWorkDetail.CheckOpinion = CheckWorkDetail.CheckOpinion;
                newCheckWorkDetail.CheckStation = CheckWorkDetail.CheckStation;
                newCheckWorkDetail.HandleResult = CheckWorkDetail.HandleResult;
                newCheckWorkDetail.CheckContent = CheckWorkDetail.CheckContent;
                newCheckWorkDetail.WorkArea = CheckWorkDetail.WorkArea;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据开工前检查ID删除所有开工前检查明细信息
        /// </summary>
        /// <param name="checkWorkId"></param>
        public static void DeleteCheckWorkDetails(string checkWorkId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_CheckWorkDetail where x.CheckWorkId == checkWorkId select x).ToList();
            if (q != null)
            {
                foreach (var item in q)
                {
                    ////删除附件表
                    BLL.CommonService.DeleteAttachFileById(item.CheckWorkDetailId);
                }
                db.Check_CheckWorkDetail.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据开工前检查ID删除一条开工前检查明细信息
        /// </summary>
        /// <param name="checkWorkDetailId"></param>
        public static void DeleteCheckWorkDetailById(string checkWorkDetailId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Check_CheckWorkDetail where x.CheckWorkDetailId == checkWorkDetailId select x).FirstOrDefault();
            if (q != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(q.CheckWorkDetailId);
                db.Check_CheckWorkDetail.DeleteOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
