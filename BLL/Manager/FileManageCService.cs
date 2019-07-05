using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class FileManageCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取文件管理信息
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_FileManageC> GetFileManageByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_FileManageC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }

        /// <summary>
        /// 添加文件管理
        /// </summary>
        /// <param name="fileManage"></param>
        public static void AddFileManage(Model.Manager_Month_FileManageC fileManage)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_FileManageC newFileManage = new Model.Manager_Month_FileManageC
            {
                FileManageId = SQLHelper.GetNewID(typeof(Model.Manager_Month_FileManageC)),
                MonthReportId = fileManage.MonthReportId,
                FileName = fileManage.FileName,
                Disposal = fileManage.Disposal,
                FileArchiveLocation = fileManage.FileArchiveLocation,
                SortIndex = fileManage.SortIndex
            };
            db.Manager_Month_FileManageC.InsertOnSubmit(newFileManage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除所有相关文件管理信息
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteFileManageByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_FileManageC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_FileManageC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
