using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class SafetyDataEItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键id获取项目明细
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.SafetyDataE_SafetyDataEItem GetSafetyDataEItemByID(string SafetyDataEItemId)
        {
            return Funs.DB.SafetyDataE_SafetyDataEItem.FirstOrDefault(x => x.SafetyDataEItemId == SafetyDataEItemId);
        }

        /// <summary>
        /// 添加项目文件
        /// </summary>
        /// <param name="SafetyDataEItem"></param>
        public static void AddSafetyDataEItem(Model.SafetyDataE_SafetyDataEItem SafetyDataEItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyDataE_SafetyDataEItem newSafetyDataEItem = new Model.SafetyDataE_SafetyDataEItem
            {
                SafetyDataEItemId = SafetyDataEItem.SafetyDataEItemId,
                SafetyDataEId = SafetyDataEItem.SafetyDataEId,
                ProjectId = SafetyDataEItem.ProjectId,
                Code = SafetyDataEItem.Code,
                SortIndex = SafetyDataEItem.SortIndex,
                Title = SafetyDataEItem.Title,
                FileContent = SafetyDataEItem.FileContent,
                CompileMan = SafetyDataEItem.CompileMan,
                CompileDate = SafetyDataEItem.CompileDate,
                SubmitDate = SafetyDataEItem.SubmitDate,
                Remark = SafetyDataEItem.Remark,
                AttachUrl = SafetyDataEItem.AttachUrl,
                IsMenu = SafetyDataEItem.IsMenu,
                Url = SafetyDataEItem.Url
            };
            db.SafetyDataE_SafetyDataEItem.InsertOnSubmit(newSafetyDataEItem);
            db.SubmitChanges();
            ///  更新考核计划 单据提交时间
            AddSafetyDataEItemSubmit(newSafetyDataEItem);
        }

        /// <summary>
        /// 修改项目文件
        /// </summary>
        /// <param name="SafetyDataEItem"></param>
        public static void UpdateSafetyDataEItem(Model.SafetyDataE_SafetyDataEItem SafetyDataEItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyDataE_SafetyDataEItem newSafetyDataEItem = db.SafetyDataE_SafetyDataEItem.FirstOrDefault(e => e.SafetyDataEItemId == SafetyDataEItem.SafetyDataEItemId);
            if (newSafetyDataEItem != null)
            {
                newSafetyDataEItem.Code = SafetyDataEItem.Code;
                newSafetyDataEItem.SortIndex = SafetyDataEItem.SortIndex;
                newSafetyDataEItem.Title = SafetyDataEItem.Title;
                newSafetyDataEItem.FileContent = SafetyDataEItem.FileContent;
                newSafetyDataEItem.CompileMan = SafetyDataEItem.CompileMan;
                newSafetyDataEItem.CompileDate = SafetyDataEItem.CompileDate;
                //newSafetyDataEItem.SubmitDate = SafetyDataEItem.SubmitDate;
                newSafetyDataEItem.Remark = SafetyDataEItem.Remark;
                newSafetyDataEItem.AttachUrl = SafetyDataEItem.AttachUrl;
                db.SubmitChanges();

                ///  更新考核计划 单据提交时间
                AddSafetyDataEItemSubmit(newSafetyDataEItem);
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="SafetyDataEItemId"></param>
        public static void DeleteSafetyDataEItemByID(string SafetyDataEItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyDataE_SafetyDataEItem SafetyDataEItem = db.SafetyDataE_SafetyDataEItem.FirstOrDefault(e => e.SafetyDataEItemId == SafetyDataEItemId);
            if (SafetyDataEItem != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(SafetyDataEItem.SafetyDataEItemId);
                db.SafetyDataE_SafetyDataEItem.DeleteOnSubmit(SafetyDataEItem);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="SafetyDataEItemId"></param>
        public static void DeleteSafetyDataEItemByProjectId(string projectId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var safetyDataEItems = from x in db.SafetyDataE_SafetyDataEItem where x.ProjectId == projectId select x;
            foreach (var safetyDataEItem in safetyDataEItems)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(safetyDataEItem.SafetyDataEItemId);
                db.SafetyDataE_SafetyDataEItem.DeleteOnSubmit(safetyDataEItem);
                db.SubmitChanges();
            }
        }

        #region 获取企业管理资料最大编号
        /// <summary>
        ///  获取企业管理资料最大编号
        /// </summary>
        /// <returns></returns>
        public static string GetNewSafetyDataEItemCode(string projectId, string SafetyDataEId)
        {
            string code = string.Empty;
            ////获取编码记录表最大排列序号              
            int maxNewSortIndex = 0;
            var maxSortIndex = Funs.DB.SafetyDataE_SafetyDataEItem.Where(x => x.ProjectId == projectId && x.SafetyDataEId == SafetyDataEId).Select(x => x.SortIndex).Max();
            if (maxSortIndex.HasValue)
            {
                maxNewSortIndex = maxSortIndex.Value;
            }
            maxNewSortIndex = maxNewSortIndex + 1;

            int digit = 3;
            var safeData = BLL.SafetyDataEService.GetSafetyDataEBySafetyDataEId(SafetyDataEId);
            if (safeData != null && safeData.Digit.HasValue)
            {
                digit = safeData.Digit.Value;
            }
            code = (maxNewSortIndex.ToString().PadLeft(digit, '0'));   ///字符自动补零

            return code;
        }
        #endregion

        /// <summary>
        ///  单据 更新考核计划 单据提交时间
        /// </summary>
        /// <param name="SafetyDataEItem"></param>
        public static void AddSafetyDataEItemSubmit(Model.SafetyDataE_SafetyDataEItem SafetyDataEItem)
        {
            var SafetyDataEPlan = from x in Funs.DB.SafetyDataE_SafetyDataEPlan
                                  where x.ProjectId == SafetyDataEItem.ProjectId && x.SafetyDataEId == SafetyDataEItem.SafetyDataEId                                       
                                        && !x.SubmitDate.HasValue
                                  select x;
            if (SafetyDataEPlan.Count() > 0)
            {
                foreach (var item in SafetyDataEPlan)
                {
                    item.SubmitDate = SafetyDataEItem.SubmitDate;
                    if (item.SubmitDate <= item.CheckDate) ///准时提交
                    {
                        item.RealScore = item.ShouldScore;
                    }
                    else   ///超期提交
                    {
                        item.RealScore = 0;
                    }

                    BLL.SafetyDataEPlanService.UpdateSafetyDataEPlan(item);
                }
            }
        }
    }
}
