using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{

    public static class CheckRectifyService
    {       
        /// <summary>
        /// 根据整改ID获取监督检查信息
        /// </summary>
        /// <param name="CheckRectifyName"></param>
        /// <returns></returns>
        public static Model.Check_CheckRectify GetCheckRectifyByCheckRectifyId(string checkRectifyId)
        {
            return Funs.DB.Check_CheckRectify.FirstOrDefault(e => e.CheckRectifyId == checkRectifyId);
        }
        
        /// <summary>
        /// 添加安全监督检查整改
        /// </summary>
        /// <param name="checkRectify"></param>
        public static void AddCheckRectify(Model.Check_CheckRectify checkRectify)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckRectify newCheckRectify = new Model.Check_CheckRectify
            {
                CheckRectifyId = checkRectify.CheckRectifyId,
                CheckRectifyCode = checkRectify.CheckRectifyCode,
                ProjectId = checkRectify.ProjectId,
                UnitId = checkRectify.UnitId,
                CheckDate = checkRectify.CheckDate,
                IssueMan = checkRectify.IssueMan,
                IssueDate = checkRectify.IssueDate,
                HandleState = checkRectify.HandleState
            };
            db.Check_CheckRectify.InsertOnSubmit(newCheckRectify);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全监督检查整改
        /// </summary>
        /// <param name="checkRectify"></param>
        public static void UpdateCheckRectify(Model.Check_CheckRectify checkRectify)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Check_CheckRectify newCheckRectify = db.Check_CheckRectify.FirstOrDefault(e => e.CheckRectifyId == checkRectify.CheckRectifyId);
            if (newCheckRectify != null)
            {                
                newCheckRectify.CheckRectifyCode = checkRectify.CheckRectifyCode;
                newCheckRectify.ProjectId = checkRectify.ProjectId;
                newCheckRectify.UnitId = checkRectify.UnitId;
                newCheckRectify.CheckDate = checkRectify.CheckDate;
                newCheckRectify.IssueMan = checkRectify.IssueMan;
                newCheckRectify.IssueDate = checkRectify.IssueDate;
                
                newCheckRectify.HandleState = checkRectify.HandleState;
                db.SubmitChanges();
            }
        }
    }
}