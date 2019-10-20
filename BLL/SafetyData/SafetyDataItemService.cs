using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class SafetyDataItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;
       
        /// <summary>
        /// 根据主键id获取项目明细
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.SafetyData_SafetyDataItem GetSafetyDataItemByID(string SafetyDataItemId)
        {
            return Funs.DB.SafetyData_SafetyDataItem.FirstOrDefault(x => x.SafetyDataItemId == SafetyDataItemId);
        }

        /// <summary>
        /// 添加项目文件
        /// </summary>
        /// <param name="SafetyDataItem"></param>
        public static void AddSafetyDataItem(Model.SafetyData_SafetyDataItem SafetyDataItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyDataItem newSafetyDataItem = new Model.SafetyData_SafetyDataItem
            {
                SafetyDataItemId = SafetyDataItem.SafetyDataItemId,
                SafetyDataId = SafetyDataItem.SafetyDataId,
                ProjectId = SafetyDataItem.ProjectId,
                Code = SafetyDataItem.Code,
                SortIndex = SafetyDataItem.SortIndex,
                Title = SafetyDataItem.Title,
                FileContent = SafetyDataItem.FileContent,
                CompileMan = SafetyDataItem.CompileMan,
                CompileDate = SafetyDataItem.CompileDate,
                SubmitDate = SafetyDataItem.SubmitDate,
                Remark = SafetyDataItem.Remark,
                AttachUrl = SafetyDataItem.AttachUrl,
                IsMenu = SafetyDataItem.IsMenu,
                Url = SafetyDataItem.Url
            };
            db.SafetyData_SafetyDataItem.InsertOnSubmit(newSafetyDataItem);
            db.SubmitChanges();
            ///  更新考核计划 单据提交时间
            AddSafetyDataItemSubmit(newSafetyDataItem);
        }

        /// <summary>
        /// 修改项目文件
        /// </summary>
        /// <param name="SafetyDataItem"></param>
        public static void UpdateSafetyDataItem(Model.SafetyData_SafetyDataItem SafetyDataItem)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyDataItem newSafetyDataItem = db.SafetyData_SafetyDataItem.FirstOrDefault(e => e.SafetyDataItemId == SafetyDataItem.SafetyDataItemId);
            if (newSafetyDataItem != null)
            {
                newSafetyDataItem.Code = SafetyDataItem.Code;
                newSafetyDataItem.SortIndex = SafetyDataItem.SortIndex;
                newSafetyDataItem.Title = SafetyDataItem.Title;
                newSafetyDataItem.FileContent = SafetyDataItem.FileContent;
                newSafetyDataItem.CompileMan = SafetyDataItem.CompileMan;
                newSafetyDataItem.CompileDate = SafetyDataItem.CompileDate;
                //newSafetyDataItem.SubmitDate = SafetyDataItem.SubmitDate;
                newSafetyDataItem.Remark = SafetyDataItem.Remark;
                newSafetyDataItem.AttachUrl = SafetyDataItem.AttachUrl;
                db.SubmitChanges();

                ///  更新考核计划 单据提交时间
                AddSafetyDataItemSubmit(newSafetyDataItem);
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="safetyDataItemId"></param>
        public static void DeleteSafetyDataItemByID(string safetyDataItemId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyDataItem SafetyDataItem = db.SafetyData_SafetyDataItem.FirstOrDefault(e => e.SafetyDataItemId == safetyDataItemId);
            if (SafetyDataItem != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(SafetyDataItem.SafetyDataItemId);
                db.SafetyData_SafetyDataItem.DeleteOnSubmit(SafetyDataItem);
                db.SubmitChanges();
            }
        }

        #region 获取企业管理资料最大编号
        /// <summary>
        ///  获取企业管理资料最大编号
        /// </summary>
        /// <returns></returns>
        public static string GetNewSafetyDataItemCode(string projectId, string safetyDataId)
        {
            string code = string.Empty;
            ////获取编码记录表最大排列序号              
            int maxNewSortIndex = 0;
            var maxSortIndex = Funs.DB.SafetyData_SafetyDataItem.Where(x => x.ProjectId == projectId && x.SafetyDataId == safetyDataId).Select(x => x.SortIndex).Max();
            if (maxSortIndex.HasValue)
            {
                maxNewSortIndex = maxSortIndex.Value;
            }
            maxNewSortIndex = maxNewSortIndex + 1;

            int digit = 3;
            var safeData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(safetyDataId);
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
        /// <param name="safetyDataItem"></param>
        public static void AddSafetyDataItemSubmit(Model.SafetyData_SafetyDataItem safetyDataItem)
        {
            var safetyDataPlan = from x in Funs.DB.SafetyData_SafetyDataPlan
                                 where x.ProjectId == safetyDataItem.ProjectId && x.SafetyDataId == safetyDataItem.SafetyDataId
                                       && safetyDataItem.CompileDate >= x.RealStartDate && safetyDataItem.CompileDate <= x.RealEndDate
                                       && !x.SubmitDate.HasValue
                                 select x;
            if (safetyDataPlan.Count() > 0)
            {
                foreach (var item in safetyDataPlan)
                {
                    item.SubmitDate = safetyDataItem.SubmitDate;
                    if (item.SubmitDate <= item.CheckDate) ///准时提交
                    {
                        item.RealScore = item.ShouldScore;
                    }
                    else   ///超期提交
                    {
                        item.RealScore = 0;
                    }

                    BLL.SafetyDataPlanService.UpdateSafetyDataPlan(item);
                }
            }
        }

        public static void GollSafetyData(string projectId)
        {
            var thisUnit = BLL.CommonService.GetIsThisUnit();
            if (CommonService.GetIsThisUnit(Const.UnitId_ECEC))
            {
                ////判断单据是否 加入到企业管理资料
                string menuId = BLL.Const.ProjectCheckDayMenuId;
                var safeData = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.MenuId == menuId);
                if (safeData != null)
                {
                    ///收集手机端考核项资料
                    var registrations = from x in Funs.DB.Inspection_Registration
                                        where x.ProjectId == projectId
                                        select x;
                    foreach (var item in registrations)
                    {
                        var safetyDataItem = BLL.SafetyDataItemService.GetSafetyDataItemByID(item.RegistrationId); ///明细是否存在
                        if (safetyDataItem == null)
                        {
                            BLL.SafetyDataService.AddSafetyData(menuId, item.RegistrationId, item.ProblemDescription, "../Check/RegistrationView.aspx?RegistrationId={0}", projectId);
                        }
                    }
                }
            }
        }
    }
}
