using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 普通货物入场报批
    /// </summary>
    public static class GoodsInService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取普通货物入场报批
        /// </summary>
        /// <param name="goodsInId"></param>
        /// <returns></returns>
        public static Model.InApproveManager_GoodsIn GetGoodsInById(string goodsInId)
        {
            return Funs.DB.InApproveManager_GoodsIn.FirstOrDefault(e => e.GoodsInId == goodsInId);
        }

        /// <summary>
        /// 添加普通货物入场报批
        /// </summary>
        /// <param name="goodsIn"></param>
        public static void AddGoodsIn(Model.InApproveManager_GoodsIn goodsIn)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GoodsIn newGoodsIn = new Model.InApproveManager_GoodsIn
            {
                GoodsInId = goodsIn.GoodsInId,
                ProjectId = goodsIn.ProjectId,
                GoodsInCode = goodsIn.GoodsInCode,
                UnitId = goodsIn.UnitId,
                InDate = goodsIn.InDate,
                InTime = goodsIn.InTime,
                CarNum = goodsIn.CarNum,
                DriverNameAndNum = goodsIn.DriverNameAndNum,
                GoodsInResult = goodsIn.GoodsInResult,
                GoodsInNote = goodsIn.GoodsInNote,
                States = goodsIn.States,
                CompileMan = goodsIn.CompileMan,
                CompileDate = goodsIn.CompileDate
            };
            db.InApproveManager_GoodsIn.InsertOnSubmit(newGoodsIn);
            db.SubmitChanges();

            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.GoodsInMenuId, goodsIn.ProjectId, null, goodsIn.GoodsInId, goodsIn.CompileDate);
        }

        /// <summary>
        /// 修改普通货物入场报批
        /// </summary>
        /// <param name="goodsIn"></param>
        public static void UpdateGoodsIn(Model.InApproveManager_GoodsIn goodsIn)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GoodsIn newGoodsIn = db.InApproveManager_GoodsIn.FirstOrDefault(e => e.GoodsInId == goodsIn.GoodsInId);
            if (newGoodsIn != null)
            {
                //newGoodsIn.ProjectId = goodsIn.ProjectId;
                newGoodsIn.GoodsInCode = goodsIn.GoodsInCode;
                newGoodsIn.UnitId = goodsIn.UnitId;
                newGoodsIn.InDate = goodsIn.InDate;
                newGoodsIn.InTime = goodsIn.InTime;
                newGoodsIn.CarNum = goodsIn.CarNum;
                newGoodsIn.DriverNameAndNum = goodsIn.DriverNameAndNum;
                newGoodsIn.GoodsInResult = goodsIn.GoodsInResult;
                newGoodsIn.GoodsInNote = goodsIn.GoodsInNote;
                newGoodsIn.States = goodsIn.States;
                newGoodsIn.CompileMan = goodsIn.CompileMan;
                newGoodsIn.CompileDate = goodsIn.CompileDate;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除普通货物入场报批
        /// </summary>
        /// <param name="goodsInId"></param>
        public static void DeleteGoodsInById(string goodsInId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.InApproveManager_GoodsIn goodsIn = db.InApproveManager_GoodsIn.FirstOrDefault(e => e.GoodsInId == goodsInId);
            if (goodsIn != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(goodsInId);
                CommonService.DeleteAttachFileById(goodsInId);
                BLL.CommonService.DeleteFlowOperateByID(goodsInId);  ////删除审核流程表
                db.InApproveManager_GoodsIn.DeleteOnSubmit(goodsIn);
                db.SubmitChanges();
            }
        }
    }
}