using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 物资管理
    /// </summary>
    public static class GoodsManageService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取物资管理
        /// </summary>
        /// <param name="goodsManageId"></param>
        /// <returns></returns>
        public static Model.CostGoods_GoodsManage GetGoodsManageById(string goodsManageId)
        {
            return Funs.DB.CostGoods_GoodsManage.FirstOrDefault(e => e.GoodsManageId == goodsManageId);
        }

        /// <summary>
        /// 添加物资管理
        /// </summary>
        /// <param name="goodsManage"></param>
        public static void AddGoodsManage(Model.CostGoods_GoodsManage goodsManage)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_GoodsManage newGoodsManage = new Model.CostGoods_GoodsManage
            {
                GoodsManageId = goodsManage.GoodsManageId,
                ProjectId = goodsManage.ProjectId,
                UnitId = goodsManage.UnitId,
                GoodsCategoryId = goodsManage.GoodsCategoryId,
                GoodsCode = goodsManage.GoodsCode,
                GoodsName = goodsManage.GoodsName,
                SizeModel = goodsManage.SizeModel,
                FactoryCode = goodsManage.FactoryCode,
                CheckDate = goodsManage.CheckDate,
                EnableYear = goodsManage.EnableYear,
                CheckPerson = goodsManage.CheckPerson,
                InTime = goodsManage.InTime,
                States = goodsManage.States,
                CompileMan = goodsManage.CompileMan,
                CompileDate = goodsManage.CompileDate,
                Remark = goodsManage.Remark
            };
            db.CostGoods_GoodsManage.InsertOnSubmit(newGoodsManage);
            db.SubmitChanges();

            CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.GoodsManageMenuId, goodsManage.ProjectId, goodsManage.UnitId, goodsManage.GoodsManageId, goodsManage.CompileDate);
        }

        /// <summary>
        /// 修改物资管理
        /// </summary>
        /// <param name="goodsManage"></param>
        public static void UpdateGoodsManage(Model.CostGoods_GoodsManage goodsManage)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_GoodsManage newGoodsManage = db.CostGoods_GoodsManage.FirstOrDefault(e => e.GoodsManageId == goodsManage.GoodsManageId);
            if (newGoodsManage != null)
            {
                //newGoodsManage.ProjectId = goodsManage.ProjectId;
                newGoodsManage.UnitId = goodsManage.UnitId;
                newGoodsManage.GoodsCategoryId = goodsManage.GoodsCategoryId;
                newGoodsManage.GoodsCode = goodsManage.GoodsCode;
                newGoodsManage.GoodsName = goodsManage.GoodsName;
                newGoodsManage.SizeModel = goodsManage.SizeModel;
                newGoodsManage.FactoryCode = goodsManage.FactoryCode;
                newGoodsManage.CheckDate = goodsManage.CheckDate;
                newGoodsManage.EnableYear = goodsManage.EnableYear;
                newGoodsManage.CheckPerson = goodsManage.CheckPerson;
                newGoodsManage.InTime = goodsManage.InTime;
                newGoodsManage.States = goodsManage.States;
                newGoodsManage.CompileMan = goodsManage.CompileMan;
                newGoodsManage.CompileDate = goodsManage.CompileDate;
                newGoodsManage.Remark = goodsManage.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除物资管理
        /// </summary>
        /// <param name="goodsManageId"></param>
        public static void DeleteGoodsManageById(string goodsManageId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.CostGoods_GoodsManage goodsManage = db.CostGoods_GoodsManage.FirstOrDefault(e => e.GoodsManageId == goodsManageId);
            if (goodsManage != null)
            {
                CodeRecordsService.DeleteCodeRecordsByDataId(goodsManageId);//删除编号
                ProjectDataFlowSetService.DeleteFlowSetByDataId(goodsManageId);//删除流程
                ////删除审核流程表
                BLL.CommonService.DeleteFlowOperateByID(goodsManageId);
                db.CostGoods_GoodsManage.DeleteOnSubmit(goodsManage);
                db.SubmitChanges();
            }
        }
    }
}