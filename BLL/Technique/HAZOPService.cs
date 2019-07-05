using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// HAZOP管理
    /// </summary>
    public static class HAZOPService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取HAZOP
        /// </summary>
        /// <param name="hazopId"></param>
        /// <returns></returns>
        public static Model.Technique_HAZOP GetHAZOPById(string hazopId)
        {
            return Funs.DB.Technique_HAZOP.FirstOrDefault(e => e.HAZOPId == hazopId);
        }

        /// <summary>
        /// 根据整理人获取HAZOP管理
        /// </summary>
        /// <param name="compileMan"></param>
        /// <returns></returns>
        public static List<Model.Technique_HAZOP> GetHAZOPByCompileMan(string compileMan)
        {
            return (from x in Funs.DB.Technique_HAZOP where x.CompileMan == compileMan select x).ToList();
        }

        /// <summary>
        /// 添加HAZOP
        /// </summary>
        /// <param name="hazop"></param>
        public static void AddHAZOP(Model.Technique_HAZOP hazop)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Technique_HAZOP newHAZOP = new Model.Technique_HAZOP
            {
                HAZOPId = hazop.HAZOPId,
                UnitId = hazop.UnitId,
                HAZOPTitle = hazop.HAZOPTitle,
                Abstract = hazop.Abstract,
                HAZOPDate = hazop.HAZOPDate,
                AttachUrl = hazop.AttachUrl,
                CompileMan = hazop.CompileMan,
                CompileDate = hazop.CompileDate,
                IsPass = hazop.IsPass,
                UpState = hazop.UpState
            };
            db.Technique_HAZOP.InsertOnSubmit(newHAZOP);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改HAZOP管理
        /// </summary>
        /// <param name="hazop"></param>
        public static void UpdateHAZOP(Model.Technique_HAZOP hazop)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Technique_HAZOP newHAZOP = db.Technique_HAZOP.FirstOrDefault(e => e.HAZOPId == hazop.HAZOPId);
            if (newHAZOP != null)
            {
                newHAZOP.UnitId = hazop.UnitId;
                newHAZOP.HAZOPTitle = hazop.HAZOPTitle;
                newHAZOP.Abstract = hazop.Abstract;
                newHAZOP.HAZOPDate = hazop.HAZOPDate;
                newHAZOP.AttachUrl = hazop.AttachUrl;
                newHAZOP.UpState = hazop.UpState;
                db.SubmitChanges();
            }
        }

         /// <summary>
        /// 修改HAZOP管理 是否采用
        /// </summary>
        /// <param name="hazop"></param>
        public static void UpdateHAZOPIsPass(Model.Technique_HAZOP hazop)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Technique_HAZOP newHAZOP = db.Technique_HAZOP.FirstOrDefault(e => e.HAZOPId == hazop.HAZOPId);
            if (newHAZOP != null)
            {
                newHAZOP.AuditMan = hazop.AuditMan;
                newHAZOP.AuditDate = hazop.AuditDate;
                newHAZOP.IsPass = hazop.IsPass;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除HAZOP管理
        /// </summary>
        /// <param name="hazaopId"></param>
        public static void DeleteHAZOPById(string hazopId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Technique_HAZOP hazop = db.Technique_HAZOP.FirstOrDefault(e => e.HAZOPId == hazopId);
            if (hazop != null)
            {
                if (!string.IsNullOrEmpty(hazop.AttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, hazop.AttachUrl);//删除附件
                }
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(hazop.HAZOPId);
                db.Technique_HAZOP.DeleteOnSubmit(hazop);
                db.SubmitChanges();
            }
        }
    }
}
