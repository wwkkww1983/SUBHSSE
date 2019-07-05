using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 应知应会库主表
    /// </summary>
    public static class KnowledgeService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取应知应会库信息
        /// </summary>
        /// <param name="knowledgeId"></param>
        /// <returns></returns>
        public static Model.Training_Knowledge GetKnowLedgeById(string knowledgeId)
        {
            return Funs.DB.Training_Knowledge.FirstOrDefault(e => e.KnowledgeId == knowledgeId);
        }

        /// <summary>
        /// 根据上一节点菜单id获取应知应会库
        /// </summary>
        /// <param name="supKnowledgeId"></param>
        /// <returns></returns>
        public static List<Model.Training_Knowledge> GetKnowLedgeBySupKnowledgeId(string supKnowledgeId)
        {
            return (from x in Funs.DB.Training_Knowledge where x.SupKnowledgeId == supKnowledgeId select x).ToList();
        }

        /// <summary>
        /// 添加应知应会库
        /// </summary>
        /// <param name="knowledge"></param>
        public static void AddKnowledge(Model.Training_Knowledge knowledge)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Training_Knowledge newKnowledge = new Model.Training_Knowledge
            {
                KnowledgeId = knowledge.KnowledgeId,
                KnowledgeCode = knowledge.KnowledgeCode,
                KnowledgeName = knowledge.KnowledgeName,
                SupKnowledgeId = knowledge.SupKnowledgeId,
                IsEndLever = knowledge.IsEndLever
            };
            db.Training_Knowledge.InsertOnSubmit(newKnowledge);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改应知应会库
        /// </summary>
        /// <param name="knowledge"></param>
        public static void UpdateKnowledge(Model.Training_Knowledge knowledge)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Training_Knowledge newKnowledge = db.Training_Knowledge.FirstOrDefault(e => e.KnowledgeId == knowledge.KnowledgeId);
            if (newKnowledge != null)
            {
                newKnowledge.KnowledgeCode = knowledge.KnowledgeCode;
                newKnowledge.KnowledgeName = knowledge.KnowledgeName;
                newKnowledge.SupKnowledgeId = knowledge.SupKnowledgeId;
                newKnowledge.IsEndLever = knowledge.IsEndLever;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除应知应会库
        /// </summary>
        /// <param name="knowledgeId"></param>
        public static void DeleteKnowledge(string knowledgeId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Training_Knowledge knowledge = db.Training_Knowledge.FirstOrDefault(e => e.KnowledgeId == knowledgeId);
            if (knowledge != null)
            {
                db.Training_Knowledge.DeleteOnSubmit(knowledge);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否可删除资源节点
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeleteKnowledge(string knowledgeId)
        {
            bool isDelete = true;
            var knowledge = BLL.KnowledgeService.GetKnowLedgeById(knowledgeId);
            if (knowledge != null)
            {
                if (knowledge.IsBuild == true)
                {
                    isDelete = false;
                }
                if (knowledge.IsEndLever == true)
                {
                    var detailCout = Funs.DB.Training_KnowledgeItem.FirstOrDefault(x => x.KnowledgeId == knowledgeId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = BLL.KnowledgeService.GetKnowLedgeBySupKnowledgeId(knowledgeId);
                    if (supItemSetCount.Count() > 0)
                    {
                        isDelete = false;
                    }
                }
            }
            return isDelete;
        }
    }
}