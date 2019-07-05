using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 物资类别
    /// </summary>
    public static class GoodsCategoryService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取物资类别
        /// </summary>
        /// <param name="GoodsCategoryId"></param>
        /// <returns></returns>
        public static Model.Base_GoodsCategory GetGoodsCategoryById(string GoodsCategoryId)
        {
            return Funs.DB.Base_GoodsCategory.FirstOrDefault(e => e.GoodsCategoryId == GoodsCategoryId);
        }

        /// <summary>
        /// 添加物资类别
        /// </summary>
        /// <param name="GoodsCategory"></param>
        public static void AddGoodsCategory(Model.Base_GoodsCategory GoodsCategory)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_GoodsCategory newGoodsCategory = new Model.Base_GoodsCategory
            {
                GoodsCategoryId = GoodsCategory.GoodsCategoryId,
                GoodsCategoryCode = GoodsCategory.GoodsCategoryCode,
                GoodsCategoryName = GoodsCategory.GoodsCategoryName,
                Remark = GoodsCategory.Remark
            };
            db.Base_GoodsCategory.InsertOnSubmit(newGoodsCategory);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改物资类别
        /// </summary>
        /// <param name="GoodsCategory"></param>
        public static void UpdateGoodsCategory(Model.Base_GoodsCategory GoodsCategory)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_GoodsCategory newGoodsCategory = db.Base_GoodsCategory.FirstOrDefault(e => e.GoodsCategoryId == GoodsCategory.GoodsCategoryId);
            if (newGoodsCategory != null)
            {
                newGoodsCategory.GoodsCategoryCode = GoodsCategory.GoodsCategoryCode;
                newGoodsCategory.GoodsCategoryName = GoodsCategory.GoodsCategoryName;
                newGoodsCategory.Remark = GoodsCategory.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除物资类别
        /// </summary>
        /// <param name="GoodsCategoryId"></param>
        public static void DeleteGoodsCategoryById(string GoodsCategoryId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_GoodsCategory GoodsCategory = db.Base_GoodsCategory.FirstOrDefault(e => e.GoodsCategoryId == GoodsCategoryId);
            if (GoodsCategory != null)
            {
                db.Base_GoodsCategory.DeleteOnSubmit(GoodsCategory);
                db.SubmitChanges();
            }
        }

        /// <summary>
        ///获取物资类别下拉选择项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_GoodsCategory> GetGoodsCategoryList()
        {
            return (from x in Funs.DB.Base_GoodsCategory orderby x.GoodsCategoryCode select x).ToList();
        }

        #region 物质类别表下拉框
        /// <summary>
        ///  物质类别表下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitUnitDropDownList(FineUIPro.DropDownList dropName, bool isShowPlease)
        {
            dropName.DataValueField = "GoodsCategoryId";
            dropName.DataTextField = "GoodsCategoryName";
            dropName.DataSource = GetGoodsCategoryList();
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
        #endregion
    }
}