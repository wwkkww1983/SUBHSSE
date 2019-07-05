using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 物资出库管理
    /// </summary>
    public static class GoodsOut2Service
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取物资出库管理
        /// </summary>
        /// <param name="goodsOutId"></param>
        /// <returns></returns>
        public static Model.CostGoods_GoodsOut GetGoodsOutById(string goodsOutId)
        {
            return Funs.DB.CostGoods_GoodsOut.FirstOrDefault(e => e.GoodsOutId == goodsOutId);
        }

        /// <summary>
        /// 添加物资出库管理
        /// </summary>
        /// <param name="goodsOut"></param>
        public static void AddGoodsOut(Model.CostGoods_GoodsOut goodsOut)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_GoodsOut newGoodsOut = new Model.CostGoods_GoodsOut
            {
                GoodsOutId = goodsOut.GoodsOutId,
                ProjectId = goodsOut.ProjectId,
                GoodsOutCode = goodsOut.GoodsOutCode,
                GoodsDefId = goodsOut.GoodsDefId,
                GoodsNum = goodsOut.GoodsNum,
                OutPerson = goodsOut.OutPerson,
                OutDate = goodsOut.OutDate,
                States = goodsOut.States,
                CompileMan = goodsOut.CompileMan,
                CompileDate = goodsOut.CompileDate
            };
            db.CostGoods_GoodsOut.InsertOnSubmit(newGoodsOut);
            db.SubmitChanges();
            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.GoodsOut2MenuId, goodsOut.ProjectId, null, goodsOut.GoodsOutId, goodsOut.CompileDate);
        }

        /// <summary>
        /// 修改物资出库管理
        /// </summary>
        /// <param name="goodsOut"></param>
        public static void UpdateGoodsOut(Model.CostGoods_GoodsOut goodsOut)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_GoodsOut newGoodsOut = db.CostGoods_GoodsOut.FirstOrDefault(e => e.GoodsOutId == goodsOut.GoodsOutId);
            if (newGoodsOut != null)
            {
                //newGoodsOut.ProjectId = goodsOut.ProjectId;
                newGoodsOut.GoodsOutCode = goodsOut.GoodsOutCode;
                newGoodsOut.GoodsDefId = goodsOut.GoodsDefId;
                newGoodsOut.GoodsNum = goodsOut.GoodsNum;
                newGoodsOut.OutPerson = goodsOut.OutPerson;
                newGoodsOut.OutDate = goodsOut.OutDate;
                newGoodsOut.States = goodsOut.States;
                newGoodsOut.CompileMan = goodsOut.CompileMan;
                newGoodsOut.CompileDate = goodsOut.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除物资出库管理
        /// </summary>
        /// <param name="goodsOutId"></param>
        public static void DeleteGoodsOutById(string goodsOutId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_GoodsOut goodsOut = db.CostGoods_GoodsOut.FirstOrDefault(e => e.GoodsOutId == goodsOutId);
            if (goodsOut != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(goodsOutId);//删除编号
                ProjectDataFlowSetService.DeleteFlowSetByDataId(goodsOutId);//删除流程
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(goodsOutId);
                db.CostGoods_GoodsOut.DeleteOnSubmit(goodsOut);
                db.SubmitChanges();
            }
        }
    }
}
