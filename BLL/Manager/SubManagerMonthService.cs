using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 分包商月报
    /// </summary>
    public static class SubManagerMonthService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取分包商月报
        /// </summary>
        /// <param name="subManagerMonthId"></param>
        /// <returns></returns>
        public static Model.Manager_SubManagerMonth GetSubManagerMonthById(string subManagerMonthId)
        {
            return Funs.DB.Manager_SubManagerMonth.FirstOrDefault(e => e.SubManagerMonthId == subManagerMonthId);
        }

        /// <summary>
        /// 添加分包商月报
        /// </summary>
        /// <param name="subManagerMonth"></param>
        public static void AddSubManagerMonth(Model.Manager_SubManagerMonth subManagerMonth)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SubManagerMonth newSubManagerMonth = new Model.Manager_SubManagerMonth
            {
                SubManagerMonthId = subManagerMonth.SubManagerMonthId,
                ProjectId = subManagerMonth.ProjectId,
                SubManagerMonthCode = subManagerMonth.SubManagerMonthCode,
                SubManagerMonthName = subManagerMonth.SubManagerMonthName,
                FileContent = subManagerMonth.FileContent,
                CompileMan = subManagerMonth.CompileMan,
                CompileDate = subManagerMonth.CompileDate,
                States = subManagerMonth.States
            };
            db.Manager_SubManagerMonth.InsertOnSubmit(newSubManagerMonth);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.SubManagerMonthMenuId, subManagerMonth.ProjectId, null, subManagerMonth.SubManagerMonthId, subManagerMonth.CompileDate);
        }

        /// <summary>
        /// 修改分包商月报
        /// </summary>
        /// <param name="subManagerMonth"></param>
        public static void UpdateSubManagerMonth(Model.Manager_SubManagerMonth subManagerMonth)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SubManagerMonth newSubManagerMonth = db.Manager_SubManagerMonth.FirstOrDefault(e => e.SubManagerMonthId == subManagerMonth.SubManagerMonthId);
            if (newSubManagerMonth != null)
            {
                //newSubManagerMonth.ProjectId = subManagerMonth.ProjectId;
                newSubManagerMonth.SubManagerMonthCode = subManagerMonth.SubManagerMonthCode;
                newSubManagerMonth.SubManagerMonthName = subManagerMonth.SubManagerMonthName;
                newSubManagerMonth.FileContent = subManagerMonth.FileContent;
                newSubManagerMonth.CompileMan = subManagerMonth.CompileMan;
                newSubManagerMonth.CompileDate = subManagerMonth.CompileDate;
                newSubManagerMonth.States = subManagerMonth.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除分包商月报
        /// </summary>
        /// <param name="subManagerMonthId"></param>
        public static void DeleteSubManagerMonthById(string subManagerMonthId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SubManagerMonth subManagerMonth = db.Manager_SubManagerMonth.FirstOrDefault(e => e.SubManagerMonthId == subManagerMonthId);
            if (subManagerMonth != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(subManagerMonthId);
                BLL.CommonService.DeleteAttachFileById(subManagerMonthId);//删除附件
                BLL.CommonService.DeleteFlowOperateByID(subManagerMonthId);//删除审核流程
                db.Manager_SubManagerMonth.DeleteOnSubmit(subManagerMonth);
                db.SubmitChanges();
            }
        }
    }
}
