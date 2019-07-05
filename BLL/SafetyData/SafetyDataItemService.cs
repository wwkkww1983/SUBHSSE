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
            AddSafetyDataCheckItemSubmit(newSafetyDataItem);
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
                AddSafetyDataCheckItemSubmit(newSafetyDataItem);
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
        ///  更新考核计划 单据提交时间
        /// </summary>
        /// <param name="safetyDataItem"></param>
        private static void AddSafetyDataCheckItemSubmit(Model.SafetyData_SafetyDataItem safetyDataItem)
        {
            var safetyDataCheckItem = from x in Funs.DB.SafetyData_SafetyDataCheckItem
                                      join y in Funs.DB.SafetyData_SafetyDataCheckProject on x.SafetyDataCheckProjectId equals y.SafetyDataCheckProjectId
                                      where y.ProjectId == safetyDataItem.ProjectId && x.SafetyDataId == safetyDataItem.SafetyDataId 
                                            && safetyDataItem.CompileDate >= x.StartDate && safetyDataItem.CompileDate <= x.EndDate
                                      select x;
            if (safetyDataCheckItem.Count() > 0)
            {
                foreach (var item in safetyDataCheckItem)
                {
                    item.SubmitDate = safetyDataItem.SubmitDate;
                    if (item.SubmitDate <= item.EndDate) ///准时提交
                    {
                        item.RealScore = item.ShouldScore;
                    }
                    else   ///超期提交
                    {
                        item.RealScore = 0;
                    }

                    BLL.SafetyDataCheckItemService.UpdateSafetyDataCheckItem(item);
                }
            }
        }
    }
}
