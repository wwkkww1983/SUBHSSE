using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// HSSE月总结
    /// </summary>
    public static class ManagerTotalMonthService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取HSSE月总结
        /// </summary>
        /// <param name="ManagerTotalMonthId"></param>
        /// <returns></returns>
        public static Model.Manager_ManagerTotalMonth GetManagerTotalMonthById(string ManagerTotalMonthId)
        {
            return Funs.DB.Manager_ManagerTotalMonth.FirstOrDefault(e => e.ManagerTotalMonthId == ManagerTotalMonthId);
        }

        /// <summary>
        /// 根据安全工作总结Id获取安全工作总结信息
        /// </summary>
        /// <param name="ManagerTotalId"></param>
        /// <returns></returns>
        public static List<Model.Manager_ManagerTotalMonth> GetManagerTotalByDate(DateTime date)
        {
            return (from x in Funs.DB.Manager_ManagerTotalMonth
                    where x.CompileDate.Value.Year == date.Year && x.CompileDate.Value.Month == date.Month
                    select x).ToList();
        }

        /// <summary>
        /// 增加图片信息
        /// </summary>
        /// <param name="personQuality">图片实体</param>
        public static void AddManagerTotalMonth(Model.Manager_ManagerTotalMonth ManagerTotalMonth)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerTotalMonth newManagerTotalMonth = new Model.Manager_ManagerTotalMonth
            {
                ManagerTotalMonthId = ManagerTotalMonth.ManagerTotalMonthId,
                ProjectId = ManagerTotalMonth.ProjectId,
                Title = ManagerTotalMonth.Title,
                MonthContent = ManagerTotalMonth.MonthContent,
                MonthContent2 = ManagerTotalMonth.MonthContent2,
                MonthContent3 = ManagerTotalMonth.MonthContent3,
                MonthContent4 = ManagerTotalMonth.MonthContent4,
                MonthContent5 = ManagerTotalMonth.MonthContent5,
                MonthContent6 = ManagerTotalMonth.MonthContent6,
                CompileDate = ManagerTotalMonth.CompileDate,
                CompileMan = ManagerTotalMonth.CompileMan,
                States = ManagerTotalMonth.States,
                AttachUrl = ManagerTotalMonth.AttachUrl
            };
            db.Manager_ManagerTotalMonth.InsertOnSubmit(newManagerTotalMonth);
            db.SubmitChanges();

            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectManagerTotalMonthMenuId, newManagerTotalMonth.ProjectId, null, newManagerTotalMonth.ManagerTotalMonthId, newManagerTotalMonth.CompileDate);
        }

        /// <summary>
        /// 修改HSSE月总结
        /// </summary>
        /// <param name="ManagerTotalMonth"></param>
        public static void UpdateManagerTotalMonth(Model.Manager_ManagerTotalMonth ManagerTotalMonth)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerTotalMonth newManagerTotalMonth = db.Manager_ManagerTotalMonth.FirstOrDefault(e => e.ManagerTotalMonthId == ManagerTotalMonth.ManagerTotalMonthId);
            if (newManagerTotalMonth != null)
            {
                //newManagerTotalMonth.ProjectId = ManagerTotalMonth.ProjectId;
                newManagerTotalMonth.Title = ManagerTotalMonth.Title;               
                newManagerTotalMonth.MonthContent = ManagerTotalMonth.MonthContent;
                newManagerTotalMonth.MonthContent2 = ManagerTotalMonth.MonthContent2;
                newManagerTotalMonth.MonthContent3 = ManagerTotalMonth.MonthContent3;
                newManagerTotalMonth.MonthContent4 = ManagerTotalMonth.MonthContent4;
                newManagerTotalMonth.MonthContent5 = ManagerTotalMonth.MonthContent5;
                newManagerTotalMonth.MonthContent6 = ManagerTotalMonth.MonthContent6;
                newManagerTotalMonth.CompileDate = ManagerTotalMonth.CompileDate;
                newManagerTotalMonth.CompileMan = ManagerTotalMonth.CompileMan;
                newManagerTotalMonth.States = ManagerTotalMonth.States;
                newManagerTotalMonth.AttachUrl = ManagerTotalMonth.AttachUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除HSSE月总结
        /// </summary>
        /// <param name="ManagerTotalMonthId"></param>
        public static void deleteManagerTotalMonthById(string ManagerTotalMonthId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_ManagerTotalMonth ManagerTotalMonth = db.Manager_ManagerTotalMonth.FirstOrDefault(e => e.ManagerTotalMonthId == ManagerTotalMonthId);
            if (ManagerTotalMonth != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(ManagerTotalMonth.ManagerTotalMonthId);
                BLL.CommonService.DeleteAttachFileById(ManagerTotalMonth.ManagerTotalMonthId);  ///删除附件
                BLL.UploadFileService.DeleteFile(Funs.RootPath, ManagerTotalMonth.AttachUrl);  ///删除附件
                //////删除审核流程
                BLL.CommonService.DeleteFlowOperateByID(ManagerTotalMonth.ManagerTotalMonthId);
                db.Manager_ManagerTotalMonth.DeleteOnSubmit(ManagerTotalMonth);
                db.SubmitChanges();
            }
        }
    }
}