using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 应知应会库明细
    /// </summary>
    public static class KnowledgeItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取应知应会库明细
        /// </summary>
        /// <param name="knowledgeIdItemId"></param>
        /// <returns></returns>
        public static Model.Training_KnowledgeItem GetKnowledgeItemById(string knowledgeIdItemId)
        {
            return Funs.DB.Training_KnowledgeItem.FirstOrDefault(e => e.KnowledgeItemId == knowledgeIdItemId);
        }

        /// <summary>
        /// 根据整理人获取所有相关明细信息
        /// </summary>
        /// <param name="knowledgeId"></param>
        /// <returns></returns>
        public static List<Model.Training_KnowledgeItem> GetKnowledgeItemByCompileMan(string cmpileMan)
        {
            return (from x in Funs.DB.Training_KnowledgeItem where x.CompileMan == cmpileMan select x).ToList();
        }

        /// <summary>
        /// 添加应知应会库
        /// </summary>
        /// <param name="knowledgeItem"></param>
        public static void AddKnowledgeItem(Model.Training_KnowledgeItem knowledgeItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Training_KnowledgeItem newKnowledgeItem = new Model.Training_KnowledgeItem
            {
                KnowledgeItemId = knowledgeItem.KnowledgeItemId,
                KnowledgeId = knowledgeItem.KnowledgeId,
                KnowledgeItemCode = knowledgeItem.KnowledgeItemCode,
                KnowledgeItemName = knowledgeItem.KnowledgeItemName,
                Remark = knowledgeItem.Remark,
                CompileMan = knowledgeItem.CompileMan,
                CompileDate = knowledgeItem.CompileDate,
                IsPass = knowledgeItem.IsPass,
                UnitId = knowledgeItem.UnitId,
                UpState = knowledgeItem.UpState
            };
            db.Training_KnowledgeItem.InsertOnSubmit(knowledgeItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改应知应会库
        /// </summary>
        /// <param name="knowledgeItem"></param>
        public static void UpdateKnowledgeItem(Model.Training_KnowledgeItem knowledgeItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Training_KnowledgeItem newKnowledgeItem = db.Training_KnowledgeItem.FirstOrDefault(e => e.KnowledgeItemId == knowledgeItem.KnowledgeItemId);
            if (newKnowledgeItem != null)
            {
                newKnowledgeItem.KnowledgeItemCode = knowledgeItem.KnowledgeItemCode;
                newKnowledgeItem.KnowledgeItemName = knowledgeItem.KnowledgeItemName;
                newKnowledgeItem.Remark = knowledgeItem.Remark;
                newKnowledgeItem.UpState = knowledgeItem.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 修改应知应会  是否采用
        /// </summary>
        /// <param name="knowledgeItem"></param>
        public static void UpdateKnowledgeItemIsPass(Model.Training_KnowledgeItem knowledgeItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Training_KnowledgeItem newKnowledgeItem = db.Training_KnowledgeItem.FirstOrDefault(e => e.KnowledgeItemId == knowledgeItem.KnowledgeItemId);
            if (newKnowledgeItem != null)
            {
                newKnowledgeItem.AuditMan = knowledgeItem.AuditMan;
                newKnowledgeItem.AuditDate = knowledgeItem.AuditDate;
                newKnowledgeItem.IsPass = knowledgeItem.IsPass;
                newKnowledgeItem.UpState = knowledgeItem.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除应知应会库明细
        /// </summary>
        /// <param name="knowledgeItemId"></param>
        public static void DeleteKnowledgeItem(string knowledgeItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Training_KnowledgeItem knowledgeItem = db.Training_KnowledgeItem.FirstOrDefault(e => e.KnowledgeItemId == knowledgeItemId);
            if (knowledgeItem != null)
            {
                db.Training_KnowledgeItem.DeleteOnSubmit(knowledgeItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据应知应会库主键删除所有相关明细信息
        /// </summary>
        /// <param name="knowledgeId"></param>
        public static void DeleteKnowledgeItemList(string knowledgeId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Training_KnowledgeItem where x.KnowledgeId == knowledgeId select x).ToList();
            if (q != null)
            {
                db.Training_KnowledgeItem.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }
    }
}
