using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class SafetyDataService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据用户id获取企业安全管理资料主表列表
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static List<Model.SafetyData_SafetyData> GetSafetyDataList()
        {
            var SafetyDataList = from x in Funs.DB.SafetyData_SafetyData
                                   orderby x.Code
                                   select x;
            return SafetyDataList.ToList();
        }

        /// <summary>
        /// 根据主键id获取企业安全管理资料
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.SafetyData_SafetyData GetSafetyDataBySafetyDataId(string safetyDataId)
        {
            return Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.SafetyDataId == safetyDataId);
        }

        /// <summary>
        /// 根据菜单id获取企业安全管理资料
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.SafetyData_SafetyData GetSafetyDataByMenuId(string menuid)
        {
            return Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.MenuId == menuid && x.IsCheck == true);
        }
      
        /// <summary>
        /// 添加企业安全管理资料
        /// </summary>
        /// <param name="safetyData"></param>
        public static void AddSafetyData(Model.SafetyData_SafetyData safetyData)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyData newSafetyData = new Model.SafetyData_SafetyData
            {
                SafetyDataId = safetyData.SafetyDataId,
                MenuId = safetyData.MenuId,
                Code = safetyData.Code,
                Title = safetyData.Title,
                Score = safetyData.Score,
                Digit = safetyData.Digit,
                SupSafetyDataId = safetyData.SupSafetyDataId,
                IsEndLever = safetyData.IsEndLever,
                Remark = safetyData.Remark,
                CheckType = safetyData.CheckType,
                CheckTypeValue1 = safetyData.CheckTypeValue1,
                CheckTypeValue2 = safetyData.CheckTypeValue2,
                IsCheck = safetyData.IsCheck
            };
            db.SafetyData_SafetyData.InsertOnSubmit(newSafetyData);
            db.SubmitChanges();

            ///更新考核项
            if (newSafetyData.IsCheck == true)
            {
                UpdateSafetyDataIsCheck(newSafetyData.SupSafetyDataId);
            }
        }

        /// <summary>
        /// 修改企业安全管理资料
        /// </summary>
        /// <param name="safetyData"></param>
        public static void UpdateSafetyData(Model.SafetyData_SafetyData safetyData)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyData newSafetyData = db.SafetyData_SafetyData.FirstOrDefault(e => e.SafetyDataId == safetyData.SafetyDataId);
            if (newSafetyData != null)
            {
                newSafetyData.Code = safetyData.Code;
                newSafetyData.Title = safetyData.Title;
                newSafetyData.MenuId = safetyData.MenuId;
                newSafetyData.Score = safetyData.Score;
                newSafetyData.Digit = safetyData.Digit;
                newSafetyData.SupSafetyDataId = safetyData.SupSafetyDataId;
                newSafetyData.IsEndLever = safetyData.IsEndLever;
                newSafetyData.Remark = safetyData.Remark;
                newSafetyData.CheckType = safetyData.CheckType;
                newSafetyData.CheckTypeValue1 = safetyData.CheckTypeValue1;
                newSafetyData.CheckTypeValue2 = safetyData.CheckTypeValue2;
                newSafetyData.IsCheck = safetyData.IsCheck;
                db.SubmitChanges();
                  
                ///更新考核项
                if (newSafetyData.IsCheck == true)
                {
                    UpdateSafetyDataIsCheck(newSafetyData.SupSafetyDataId);
                }
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="safetyDataId"></param>
        public static void DeleteSafetyDataByID(string safetyDataId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyData_SafetyData SafetyData = db.SafetyData_SafetyData.FirstOrDefault(e => e.SafetyDataId == safetyDataId);
            {
                ///删除相应的计划总表
                BLL.SafetyDataPlanService.DeleteSafetyDataPlanBySafetyDataId(safetyDataId);
                db.SafetyData_SafetyData.DeleteOnSubmit(SafetyData);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否存在文件夹名称
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistTitle(string SafetyDataId, string supSafetyDataId, string title)
        {
            var q = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.SupSafetyDataId == supSafetyDataId && x.Title == title
                    && x.SafetyDataId != SafetyDataId);
            if (q != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 是否可删除节点
        /// <summary>
        /// 是否可删除节点
        /// </summary>
        /// <param name="SafetyDataId"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeleteSafetyData(string SafetyDataId)
        {
            bool isDelete = true;
            var SafetyData = GetSafetyDataBySafetyDataId(SafetyDataId);
            if (SafetyData != null)
            {
                if (SafetyData.IsEndLever == true)
                {
                    var detailCout = Funs.DB.SafetyData_SafetyDataItem.FirstOrDefault(x => x.SafetyDataId == SafetyDataId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.SupSafetyDataId == SafetyDataId);
                    if (supItemSetCount != null)
                    {
                        isDelete = false;
                    }
                }
            }
            return isDelete;
        }
        #endregion

        #region 树节点是否显示
        /// <summary>
        /// 树节点是否显示
        /// </summary>
        /// <param name="safetyDataId">节点主键</param>
        /// <param name="isMenu">true-来自系统；false-来自定稿</param>
        /// <returns>true-显示，false-不显示</returns>
        public static bool IsShowSafetyDataTreeNode(string safetyDataId,bool isMenu)
        {
            bool isShow = false;
            var safetyData = GetSafetyDataBySafetyDataId(safetyDataId);
            if (safetyData != null)
            {
                if (isMenu) /// 系统文件
                {
                    if (safetyData.IsEndLever == true)
                    {
                        if (!string.IsNullOrEmpty(safetyData.MenuId))
                        {
                            isShow= true;
                        }
                        else
                        {
                            isShow= false;
                        }
                    }
                    else
                    {
                        var supSafe = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.SupSafetyDataId == safetyDataId && x.MenuId != null);
                        if (supSafe != null)
                        {
                            isShow = true;
                        }
                        else
                        {
                            var supSafeEnd =from x in Funs.DB.SafetyData_SafetyData where x.SupSafetyDataId == safetyDataId && (x.IsEndLever == false || x.IsEndLever == null) select x;
                            if (supSafeEnd.Count() == 0)
                            {
                                isShow = false;
                            }
                            else
                            {
                                foreach (var item in supSafeEnd)
                                {
                                    isShow = IsShowSafetyDataTreeNode(item.SafetyDataId, isMenu);
                                    if (isShow)
                                    {
                                        break;
                                    }
                                } 
                            }
                        }
                    }
                }
                else  ///定稿文件
                {
                    if (safetyData.IsEndLever == true)
                    {
                        if (string.IsNullOrEmpty(safetyData.MenuId))
                        {
                            isShow = true;
                        }
                        else
                        {
                            isShow = false;
                        }
                    }
                    else
                    {
                        var supSafe = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.SupSafetyDataId == safetyDataId && x.MenuId == null);
                        if (supSafe != null)
                        {
                            isShow = true;
                        }
                        else
                        {
                            var supSafeEnd = from x in Funs.DB.SafetyData_SafetyData where x.SupSafetyDataId == safetyDataId && (x.IsEndLever == false || x.IsEndLever == null) select x;
                            if (supSafeEnd.Count() == 0)
                            {
                                isShow = false;
                            }
                            else
                            {
                                foreach (var item in supSafeEnd)
                                {
                                    isShow = IsShowSafetyDataTreeNode(item.SafetyDataId, isMenu);
                                    if (isShow)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }               
            }

            return isShow;
        }
        #endregion

        #region 页面信息添加到企业管理资料
        #region 添加到企业管理资料中方法
        /// <summary>
        /// 添加到企业管理资料中方法
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="dataId"></param>
        /// <param name="content"></param>
        public static void AddSafetyData(string menuId, string dataId, string content, string url, string projectId)
        {
            var safetyData = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x=> x.MenuId == menuId);
            if (safetyData != null)   ////判断这个菜单id 是否在企业管理资料 在则插入明细
            {
                AddDataToSafetyDataItem(dataId, content, safetyData.SafetyDataId, url, projectId);                
            }            
        }
        #endregion

        #region 增加企业管理资料明细
        /// <summary>
        ///  增加企业管理资料明细
        /// </summary>
        public static void AddDataToSafetyDataItem(string dataId, string content, string safetyDataId, string url, string projectId)
        {
            var safetyDataItem = BLL.SafetyDataItemService.GetSafetyDataItemByID(dataId); ///明细是否存在
            if (safetyDataItem != null)
            {
                safetyDataItem.Title = content;
                BLL.SafetyDataItemService.UpdateSafetyDataItem(safetyDataItem);
            }
            else
            {
                Model.SafetyData_SafetyDataItem newSafetyDataItem = new Model.SafetyData_SafetyDataItem
                {
                    SafetyDataItemId = dataId,
                    SafetyDataId = safetyDataId,
                    ProjectId = projectId
                };

                string newCode = BLL.SafetyDataItemService.GetNewSafetyDataItemCode(projectId, safetyDataId);
                newSafetyDataItem.SortIndex = Funs.GetNewInt(newCode);                
                var safeData = BLL.SafetyDataService.GetSafetyDataBySafetyDataId(safetyDataId);
                if (safeData != null && !string.IsNullOrEmpty(safeData.Code))
                {
                    newCode = safeData.Code + "-" + newCode;
                }
                var project = BLL.ProjectService.GetProjectByProjectId(projectId);
                if (project != null)
                {
                    newCode = project.ProjectCode + "-" + newCode;
                }

                newSafetyDataItem.Code = newCode;                
                newSafetyDataItem.Title = content;
                newSafetyDataItem.CompileDate = System.DateTime.Now;  ////单据时间 【todo：要从页面处理】
                newSafetyDataItem.SubmitDate = System.DateTime.Now;   ////单据提交时间     
                newSafetyDataItem.IsMenu = true;
                newSafetyDataItem.Url = url;
                BLL.SafetyDataItemService.AddSafetyDataItem(newSafetyDataItem);
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// 更新是否考核项
        /// </summary>
        /// <param name="safetyData"></param>
        public static void UpdateSafetyDataIsCheck(string supSafetyDataId)
        {
            if (!string.IsNullOrEmpty(supSafetyDataId) && supSafetyDataId != "0")
            {
                var supSafetyData = Funs.DB.SafetyData_SafetyData.FirstOrDefault(e => e.SafetyDataId == supSafetyDataId);
                if (supSafetyData != null && supSafetyData.IsCheck != true)
                {
                    supSafetyData.IsCheck = true;
                    Funs.DB.SubmitChanges();
                    if (!string.IsNullOrEmpty(supSafetyData.SupSafetyDataId) && supSafetyData.SupSafetyDataId != "0")
                    {
                        UpdateSafetyDataIsCheck(supSafetyData.SupSafetyDataId);
                    }
                }
            }
        }
    }
}
