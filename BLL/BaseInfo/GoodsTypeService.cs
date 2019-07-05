using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 货物类型
    /// </summary>
    public static class GoodsTypeService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取货物类型
        /// </summary>
        /// <param name="goodsTypeId"></param>
        /// <returns></returns>
        public static Model.Base_GoodsType GetGoodsTypeById(string goodsTypeId)
        {
            return Funs.DB.Base_GoodsType.FirstOrDefault(e => e.GoodsTypeId == goodsTypeId);
        }

        /// <summary>
        /// 添加货物类型
        /// </summary>
        /// <param name="goodsType"></param>
        public static void AddGoodsType(Model.Base_GoodsType goodsType)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_GoodsType newGoodsType = new Model.Base_GoodsType
            {
                GoodsTypeId = goodsType.GoodsTypeId,
                GoodsTypeCode = goodsType.GoodsTypeCode,
                GoodsTypeName = goodsType.GoodsTypeName,
                Remark = goodsType.Remark
            };
            db.Base_GoodsType.InsertOnSubmit(newGoodsType);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改货物类型
        /// </summary>
        /// <param name="goodsType"></param>
        public static void UpdateGoodsType(Model.Base_GoodsType goodsType)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_GoodsType newGoodsType = db.Base_GoodsType.FirstOrDefault(e => e.GoodsTypeId == goodsType.GoodsTypeId);
            if (newGoodsType != null)
            {
                newGoodsType.GoodsTypeCode = goodsType.GoodsTypeCode;
                newGoodsType.GoodsTypeName = goodsType.GoodsTypeName;
                newGoodsType.Remark = goodsType.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除货物类型
        /// </summary>
        /// <param name="goodsTypeId"></param>
        public static void DeleteGoodsTypeById(string goodsTypeId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_GoodsType goodsType = db.Base_GoodsType.FirstOrDefault(e => e.GoodsTypeId == goodsTypeId);
            if (goodsType != null)
            {
                db.Base_GoodsType.DeleteOnSubmit(goodsType);
                db.SubmitChanges();
            }
        }

        /// <summary>
        ///获取货物类型下拉选择项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_GoodsType> GetGoodsTypeList()
        {
            return (from x in Funs.DB.Base_GoodsType orderby x.GoodsTypeCode select x).ToList();
        }
    }
}