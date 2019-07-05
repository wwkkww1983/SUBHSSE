using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 物资类别
    /// </summary>
    public static class GoodsDefService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取物资类别
        /// </summary>
        /// <param name="goodsDefId"></param>
        /// <returns></returns>
        public static Model.Base_GoodsDef GetGoodsDefById(string goodsDefId)
        {
            return Funs.DB.Base_GoodsDef.FirstOrDefault(e => e.GoodsDefId == goodsDefId);
        }

        /// <summary>
        /// 添加物资类别
        /// </summary>
        /// <param name="goodsDef"></param>
        public static void AddGoodsDef(Model.Base_GoodsDef goodsDef)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_GoodsDef newGoodsDef = new Model.Base_GoodsDef
            {
                GoodsDefId = goodsDef.GoodsDefId,
                GoodsDefCode = goodsDef.GoodsDefCode,
                GoodsDefName = goodsDef.GoodsDefName,
                Remark = goodsDef.Remark
            };
            db.Base_GoodsDef.InsertOnSubmit(newGoodsDef);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改物资类别
        /// </summary>
        /// <param name="goodsDef"></param>
        public static void UpdateGoodsDef(Model.Base_GoodsDef goodsDef)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_GoodsDef newGoodsDef = db.Base_GoodsDef.FirstOrDefault(e => e.GoodsDefId == goodsDef.GoodsDefId);
            if (newGoodsDef != null)
            {
                newGoodsDef.GoodsDefCode = goodsDef.GoodsDefCode;
                newGoodsDef.GoodsDefName = goodsDef.GoodsDefName;
                newGoodsDef.Remark = goodsDef.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除物资类别
        /// </summary>
        /// <param name="goodsDefId"></param>
        public static void DeleteGoodsDefById(string goodsDefId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_GoodsDef goodsDef = db.Base_GoodsDef.FirstOrDefault(e => e.GoodsDefId == goodsDefId);
            if (goodsDef != null)
            {
                db.Base_GoodsDef.DeleteOnSubmit(goodsDef);
                db.SubmitChanges();
            }
        }

       /// <summary>
       /// 根据物资类别获取库存数量
       /// </summary>
        /// <param name="goodsDefId"></param>
       /// <returns></returns>
        public static int GetGoodsNumByGoodsDefId(string goodsDefId, string projectId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            int? a = (from y in db.CostGoods_GoodsIn
                      where y.GoodsDefId == goodsDefId
                      && y.ProjectId == projectId
                      select y.GoodsNum).Sum();
            int? b = (from z in db.CostGoods_GoodsOut
                      where z.GoodsDefId == goodsDefId
                      && z.ProjectId == projectId
                      select z.GoodsNum).Sum();
            return (a ?? 0) - (b ?? 0);
        }

        /// <summary>
        /// 根据物资类别获取物资信息
        /// </summary>
        /// <param name="goodsType"></param>
        /// <returns></returns>
        public static List<Model.Base_GoodsDef> GetGoodsDefListByGoodsType(string goodsType)
        {
            return (from x in Funs.DB.Base_GoodsDef where x.GoodsDefCode == goodsType select x).ToList();
        }
    }
}