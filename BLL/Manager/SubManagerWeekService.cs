using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 分包商HSSE周报
    /// </summary>
    public static class SubManagerWeekService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取分包商HSSE周报
        /// </summary>
        /// <param name="subManagerWeekId"></param>
        /// <returns></returns>
        public static Model.Manager_SubManagerWeek GetSubManagerWeekById(string subManagerWeekId)
        {
            return Funs.DB.Manager_SubManagerWeek.FirstOrDefault(e => e.SubManagerWeekId == subManagerWeekId);
        }

        /// <summary>
        /// 添加分包商HSSE周报
        /// </summary>
        /// <param name="subManagerWeek"></param>
        public static void AddSubManagerWeek(Model.Manager_SubManagerWeek subManagerWeek)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SubManagerWeek newSubManagerWeek = new Model.Manager_SubManagerWeek
            {
                SubManagerWeekId = subManagerWeek.SubManagerWeekId,
                ProjectId = subManagerWeek.ProjectId,
                SubManagerWeekCode = subManagerWeek.SubManagerWeekCode,
                SubManagerWeekName = subManagerWeek.SubManagerWeekName,
                FileContent = subManagerWeek.FileContent,
                CompileMan = subManagerWeek.CompileMan,
                CompileDate = subManagerWeek.CompileDate,
                States = subManagerWeek.States
            };
            db.Manager_SubManagerWeek.InsertOnSubmit(newSubManagerWeek);
            db.SubmitChanges();
            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.SubManagerWeekMenuId, subManagerWeek.ProjectId, null, subManagerWeek.SubManagerWeekId, subManagerWeek.CompileDate);
        }

        /// <summary>
        /// 修改分包商HSSE周报
        /// </summary>
        /// <param name="subManagerWeek"></param>
        public static void UpdateSubManagerWeek(Model.Manager_SubManagerWeek subManagerWeek)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SubManagerWeek newSubManagerWeek = db.Manager_SubManagerWeek.FirstOrDefault(e => e.SubManagerWeekId == subManagerWeek.SubManagerWeekId);
            if (newSubManagerWeek != null)
            {
                //newSubManagerWeek.ProjectId = subManagerWeek.ProjectId;
                newSubManagerWeek.SubManagerWeekCode = subManagerWeek.SubManagerWeekCode;
                newSubManagerWeek.SubManagerWeekName = subManagerWeek.SubManagerWeekName;
                newSubManagerWeek.FileContent = subManagerWeek.FileContent;
                newSubManagerWeek.CompileMan = subManagerWeek.CompileMan;
                newSubManagerWeek.CompileDate = subManagerWeek.CompileDate;
                newSubManagerWeek.States = subManagerWeek.States;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除分包商HSSE周报
        /// </summary>
        /// <param name="subManagerWeekId"></param>
        public static void DeleteSubManagerWeekById(string subManagerWeekId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_SubManagerWeek subManagerWeek = db.Manager_SubManagerWeek.FirstOrDefault(e => e.SubManagerWeekId == subManagerWeekId);
            if (subManagerWeek != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(subManagerWeekId);
                BLL.CommonService.DeleteAttachFileById(subManagerWeekId);//删除附件
                BLL.CommonService.DeleteFlowOperateByID(subManagerWeekId);//删除审核流程
                db.Manager_SubManagerWeek.DeleteOnSubmit(subManagerWeek);
                db.SubmitChanges();
            }
        }
    }
}
