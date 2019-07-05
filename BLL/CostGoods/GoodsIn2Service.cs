using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 物资入库管理
    /// </summary>
    public static class GoodsIn2Service
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取物资入库管理
        /// </summary>
        /// <param name="goodsInId"></param>
        /// <returns></returns>
        public static Model.CostGoods_GoodsIn GetGoodsInById(string goodsInId)
        {
            return Funs.DB.CostGoods_GoodsIn.FirstOrDefault(e => e.GoodsInId == goodsInId);
        }

        /// <summary>
        /// 添加物资入库管理
        /// </summary>
        /// <param name="goodsIn"></param>
        public static void AddGoodsIn(Model.CostGoods_GoodsIn goodsIn)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_GoodsIn newGoodsIn = new Model.CostGoods_GoodsIn
            {
                GoodsInId = goodsIn.GoodsInId,
                ProjectId = goodsIn.ProjectId,
                GoodsInCode = goodsIn.GoodsInCode,
                GoodsDefId = goodsIn.GoodsDefId,
                GoodsNum = goodsIn.GoodsNum,
                InPerson = goodsIn.InPerson,
                InDate = goodsIn.InDate,
                States = goodsIn.States,
                CompileMan = goodsIn.CompileMan,
                CompileDate = goodsIn.CompileDate
            };
            db.CostGoods_GoodsIn.InsertOnSubmit(newGoodsIn);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.GoodsIn2MenuId, goodsIn.ProjectId, null, goodsIn.GoodsInId, goodsIn.CompileDate);
        }

        /// <summary>
        /// 修改物资入库管理
        /// </summary>
        /// <param name="goodsIn"></param>
        public static void UpdateGoodsIn(Model.CostGoods_GoodsIn goodsIn)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_GoodsIn newGoodsIn = db.CostGoods_GoodsIn.FirstOrDefault(e => e.GoodsInId == goodsIn.GoodsInId);
            if (newGoodsIn != null)
            {
                //newGoodsIn.ProjectId = goodsIn.ProjectId;
                newGoodsIn.GoodsInCode = goodsIn.GoodsInCode;
                newGoodsIn.GoodsDefId = goodsIn.GoodsDefId;
                newGoodsIn.GoodsNum = goodsIn.GoodsNum;
                newGoodsIn.InPerson = goodsIn.InPerson;
                newGoodsIn.InDate = goodsIn.InDate;
                newGoodsIn.States = goodsIn.States;
                newGoodsIn.CompileMan = goodsIn.CompileMan;
                newGoodsIn.CompileDate = goodsIn.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除物资入库管理
        /// </summary>
        /// <param name="goodsInId"></param>
        public static void DeleteGoodsInById(string goodsInId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_GoodsIn goodsIn = db.CostGoods_GoodsIn.FirstOrDefault(e => e.GoodsInId == goodsInId);
            if (goodsIn != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(goodsInId);//删除编号
                ProjectDataFlowSetService.DeleteFlowSetByDataId(goodsInId);//删除流程
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(goodsInId);
                db.CostGoods_GoodsIn.DeleteOnSubmit(goodsIn);
                db.SubmitChanges();
            }
        }        
    }
}
