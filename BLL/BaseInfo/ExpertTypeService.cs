using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
  public static  class ExpertTypeService
    {

        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_ExpertType GetExpertTypeById(string expertTypeId)
        {
            return Funs.DB.Base_ExpertType.FirstOrDefault(e => e.ExpertTypeId == expertTypeId);
        }

        /// <summary>
        /// 根据名称获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_ExpertType GetExpertTypeByName(string expertTypeName)
        {
            return Funs.DB.Base_ExpertType.FirstOrDefault(e => e.ExpertTypeName == expertTypeName);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddExpertType(Model.Base_ExpertType expertType)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_ExpertType newExpertType = new Model.Base_ExpertType
            {
                ExpertTypeId = expertType.ExpertTypeId,
                ExpertTypeCode = expertType.ExpertTypeCode,
                ExpertTypeName = expertType.ExpertTypeName,
                Remark = expertType.Remark
            };

            db.Base_ExpertType.InsertOnSubmit(newExpertType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdateExpertType(Model.Base_ExpertType expertType)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_ExpertType newExpertType = db.Base_ExpertType.FirstOrDefault(e => e.ExpertTypeId == expertType.ExpertTypeId);
            if (newExpertType != null)
            {
                newExpertType.ExpertTypeCode = expertType.ExpertTypeCode;
                newExpertType.ExpertTypeName = expertType.ExpertTypeName;
                newExpertType.Remark = expertType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="expertTypeId"></param>
        public static void DeleteExpertTypeById(string expertTypeId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_ExpertType expertType = db.Base_ExpertType.FirstOrDefault(e => e.ExpertTypeId == expertTypeId);
            {
                db.Base_ExpertType.DeleteOnSubmit(expertType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_ExpertType> GetExpertTypeList()
        {
            var list = (from x in Funs.DB.Base_ExpertType orderby x.ExpertTypeCode select x).ToList();
            return list;
        }

        /// <summary>
        /// 获取专家类别下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_ExpertType> GetExpertTypeDropDownList()
        {
            var list = (from x in Funs.DB.Base_ExpertType orderby x.ExpertTypeCode select x).ToList();           
            return list;
        }
    }
}