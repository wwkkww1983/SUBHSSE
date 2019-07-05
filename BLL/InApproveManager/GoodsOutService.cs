using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 普通货物出场报批
    /// </summary>
    public static class GoodsOutService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取普通货物出场报批
        /// </summary>
        /// <param name="goodsOutId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_GoodsOut GetGoodsOutById(string goodsOutId)
        {
            return Funs.DB.InApproveManager_GoodsOut.FirstOrDefault(e => e.GoodsOutId == goodsOutId);
        }

        /// <summary>
        /// 添加普通货物出场报批
        /// </summary>
        /// <param name="goodsOut"></param>
        public static void AddGoodsOut(Model.InApproveManager_GoodsOut goodsOut)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GoodsOut newGoodsOut = new Model.InApproveManager_GoodsOut
            {
                GoodsOutId = goodsOut.GoodsOutId,
                ProjectId = goodsOut.ProjectId,
                GoodsOutCode = goodsOut.GoodsOutCode,
                UnitId = goodsOut.UnitId,
                OutDate = goodsOut.OutDate,
                OutTime = goodsOut.OutTime,
                CarNum = goodsOut.CarNum,
                CarModel = goodsOut.CarModel,
                StartPlace = goodsOut.StartPlace,
                EndPlace = goodsOut.EndPlace,
                States = goodsOut.States,
                CompileMan = goodsOut.CompileMan,
                CompileDate = goodsOut.CompileDate
            };
            db.InApproveManager_GoodsOut.InsertOnSubmit(newGoodsOut);
            db.SubmitChanges();

            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.GoodsOutMenuId, goodsOut.ProjectId, null, goodsOut.GoodsOutId, goodsOut.CompileDate);
        }

        /// <summary>
        /// 修改普通货物出场报批
        /// </summary>
        /// <param name="goodsOut"></param>
        public static void UpdateGoodsOut(Model.InApproveManager_GoodsOut goodsOut)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GoodsOut newGoodsOut = db.InApproveManager_GoodsOut.FirstOrDefault(e => e.GoodsOutId == goodsOut.GoodsOutId);
            if (newGoodsOut != null)
            {
               // newGoodsOut.ProjectId = goodsOut.ProjectId;
                newGoodsOut.GoodsOutCode = goodsOut.GoodsOutCode;
                newGoodsOut.UnitId = goodsOut.UnitId;
                newGoodsOut.OutDate = goodsOut.OutDate;
                newGoodsOut.OutTime = goodsOut.OutTime;
                newGoodsOut.CarNum = goodsOut.CarNum;
                newGoodsOut.CarModel = goodsOut.CarModel;
                newGoodsOut.StartPlace = goodsOut.StartPlace;
                newGoodsOut.EndPlace = goodsOut.EndPlace;
                newGoodsOut.States = goodsOut.States;
                newGoodsOut.CompileMan = goodsOut.CompileMan;
                newGoodsOut.CompileDate = goodsOut.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除普通货物出场报批
        /// </summary>
        /// <param name="goodsOutId"></param>
        public static void DeleteGoodsOutById(string goodsOutId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GoodsOut goodsOut = BLL.GoodsOutService.GetGoodsOutById(goodsOutId);
            if (goodsOut != null)
            {
                CommonService.DeleteAttachFileById(goodsOutId);
                CodeRecordsService.DeleteCodeRecordsByDataId(goodsOutId);
                BLL.CommonService.DeleteFlowOperateByID(goodsOutId);  ////删除审核流程表
                db.InApproveManager_GoodsOut.DeleteOnSubmit(goodsOut);
                db.SubmitChanges();
            }
        }
    }
}
