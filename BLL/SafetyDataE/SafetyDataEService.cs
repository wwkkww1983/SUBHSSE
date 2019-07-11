using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class SafetyDataEService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据用户id获取企业安全管理资料主表列表
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static List<Model.SafetyDataE_SafetyDataE> GetSafetyDataEList()
        {
            var SafetyDataEList = from x in Funs.DB.SafetyDataE_SafetyDataE
                                   orderby x.Code
                                   select x;
            return SafetyDataEList.ToList();
        }

        /// <summary>
        /// 根据主键id获取企业安全管理资料
        /// </summary>
        /// <param name="appraise"></param>
        /// <returns></returns>
        public static Model.SafetyDataE_SafetyDataE GetSafetyDataEBySafetyDataEId(string SafetyDataEId)
        {
            return Funs.DB.SafetyDataE_SafetyDataE.FirstOrDefault(x => x.SafetyDataEId == SafetyDataEId);
        }

        ///// <summary>
        ///// 根据菜单id获取企业安全管理资料
        ///// </summary>
        ///// <param name="appraise"></param>
        ///// <returns></returns>
        //public static Model.SafetyDataE_SafetyDataE GetSafetyDataEByMenuId(string menuid)
        //{
        //    return Funs.DB.SafetyDataE_SafetyDataE.FirstOrDefault(x => x.MenuId == menuid && x.IsCheck == true);
        //}
      
        /// <summary>
        /// 添加企业安全管理资料
        /// </summary>
        /// <param name="SafetyDataE"></param>
        public static void AddSafetyDataE(Model.SafetyDataE_SafetyDataE SafetyDataE)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyDataE_SafetyDataE newSafetyDataE = new Model.SafetyDataE_SafetyDataE
            {
                SafetyDataEId = SafetyDataE.SafetyDataEId,
                Code = SafetyDataE.Code,
                Title = SafetyDataE.Title,
                Score = SafetyDataE.Score,
                Digit = SafetyDataE.Digit,
                SupSafetyDataEId = SafetyDataE.SupSafetyDataEId,
                IsEndLever = SafetyDataE.IsEndLever,
                Remark = SafetyDataE.Remark,
                IsCheck = SafetyDataE.IsCheck
            };
            db.SafetyDataE_SafetyDataE.InsertOnSubmit(newSafetyDataE);
            db.SubmitChanges();

            ///更新考核项
            if (newSafetyDataE.IsCheck == true)
            {
                UpdateSafetyDataEIsCheck(newSafetyDataE.SupSafetyDataEId);
            }
        }

        /// <summary>
        /// 修改企业安全管理资料
        /// </summary>
        /// <param name="SafetyDataE"></param>
        public static void UpdateSafetyDataE(Model.SafetyDataE_SafetyDataE SafetyDataE)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyDataE_SafetyDataE newSafetyDataE = db.SafetyDataE_SafetyDataE.FirstOrDefault(e => e.SafetyDataEId == SafetyDataE.SafetyDataEId);
            if (newSafetyDataE != null)
            {
                newSafetyDataE.Code = SafetyDataE.Code;
                newSafetyDataE.Title = SafetyDataE.Title;
                newSafetyDataE.Score = SafetyDataE.Score;
                newSafetyDataE.Digit = SafetyDataE.Digit;
                newSafetyDataE.SupSafetyDataEId = SafetyDataE.SupSafetyDataEId;
                newSafetyDataE.IsEndLever = SafetyDataE.IsEndLever;
                newSafetyDataE.Remark = SafetyDataE.Remark;
                newSafetyDataE.IsCheck = SafetyDataE.IsCheck;
                db.SubmitChanges();
                  
                ///更新考核项
                if (newSafetyDataE.IsCheck == true)
                {
                    UpdateSafetyDataEIsCheck(newSafetyDataE.SupSafetyDataEId);
                }
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="SafetyDataEId"></param>
        public static void DeleteSafetyDataEByID(string SafetyDataEId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.SafetyDataE_SafetyDataE SafetyDataE = db.SafetyDataE_SafetyDataE.FirstOrDefault(e => e.SafetyDataEId == SafetyDataEId);
            {
                ///删除相应的计划总表
                BLL.SafetyDataEPlanService.DeleteSafetyDataEPlanBySafetyDataEId(SafetyDataEId);
                db.SafetyDataE_SafetyDataE.DeleteOnSubmit(SafetyDataE);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 是否存在文件夹名称
        /// </summary>
        /// <param name="postName"></param>
        /// <returns>true-存在，false-不存在</returns>
        public static bool IsExistTitle(string SafetyDataEId, string supSafetyDataEId, string title)
        {
            var q = Funs.DB.SafetyDataE_SafetyDataE.FirstOrDefault(x => x.SupSafetyDataEId == supSafetyDataEId && x.Title == title
                    && x.SafetyDataEId != SafetyDataEId);
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
        /// <param name="SafetyDataEId"></param>
        /// <returns>true-可以，false-不可以</returns>
        public static bool IsDeleteSafetyDataE(string SafetyDataEId)
        {
            bool isDelete = true;
            var SafetyDataE = GetSafetyDataEBySafetyDataEId(SafetyDataEId);
            if (SafetyDataE != null)
            {
                if (SafetyDataE.IsEndLever == true)
                {
                    var detailCout = Funs.DB.SafetyDataE_SafetyDataEItem.FirstOrDefault(x => x.SafetyDataEId == SafetyDataEId);
                    if (detailCout != null)
                    {
                        isDelete = false;
                    }
                }
                else
                {
                    var supItemSetCount = Funs.DB.SafetyDataE_SafetyDataE.FirstOrDefault(x => x.SupSafetyDataEId == SafetyDataEId);
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
        /// <param name="SafetyDataEId">节点主键</param>
        /// <param name="isMenu">true-来自系统；false-来自定稿</param>
        /// <returns>true-显示，false-不显示</returns>
        public static bool IsShowSafetyDataETreeNode(string SafetyDataEId,bool isMenu)
        {
            bool isShow = false;
            var SafetyDataE = GetSafetyDataEBySafetyDataEId(SafetyDataEId);
            if (SafetyDataE != null)
            {
                if (isMenu) /// 系统文件
                {
                    if (SafetyDataE.IsEndLever == true)
                    {
                        isShow = false;
                    }
                    else
                    {
                        var supSafe = Funs.DB.SafetyDataE_SafetyDataE.FirstOrDefault(x => x.SupSafetyDataEId == SafetyDataEId );
                        if (supSafe != null)
                        {
                            isShow = true;
                        }
                        else
                        {
                            var supSafeEnd =from x in Funs.DB.SafetyDataE_SafetyDataE where x.SupSafetyDataEId == SafetyDataEId && (x.IsEndLever == false || x.IsEndLever == null) select x;
                            if (supSafeEnd.Count() == 0)
                            {
                                isShow = false;
                            }
                            else
                            {
                                foreach (var item in supSafeEnd)
                                {
                                    isShow = IsShowSafetyDataETreeNode(item.SafetyDataEId, isMenu);
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
                    if (SafetyDataE.IsEndLever == true)
                    {
                        isShow = false;
                    }
                    else
                    {
                        var supSafe = Funs.DB.SafetyDataE_SafetyDataE.FirstOrDefault(x => x.SupSafetyDataEId == SafetyDataEId);
                        if (supSafe != null)
                        {
                            isShow = true;
                        }
                        else
                        {
                            var supSafeEnd = from x in Funs.DB.SafetyDataE_SafetyDataE where x.SupSafetyDataEId == SafetyDataEId && (x.IsEndLever == false || x.IsEndLever == null) select x;
                            if (supSafeEnd.Count() == 0)
                            {
                                isShow = false;
                            }
                            else
                            {
                                foreach (var item in supSafeEnd)
                                {
                                    isShow = IsShowSafetyDataETreeNode(item.SafetyDataEId, isMenu);
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
        public static void AddSafetyDataE(string menuId, string dataId, string content, string url, string projectId)
        {
            //var SafetyDataE = Funs.DB.SafetyDataE_SafetyDataE.FirstOrDefault(x=> x.MenuId == menuId);
            //if (SafetyDataE != null)   ////判断这个菜单id 是否在企业管理资料 在则插入明细
            //{
            //    AddDataToSafetyDataEItem(dataId, content, SafetyDataE.SafetyDataEId, url, projectId);                
            //}            
        }
        #endregion

        #region 增加企业管理资料明细
        /// <summary>
        ///  增加企业管理资料明细
        /// </summary>
        public static void AddDataToSafetyDataEItem(string dataId, string content, string SafetyDataEId, string url, string projectId)
        {
            var SafetyDataEItem = BLL.SafetyDataEItemService.GetSafetyDataEItemByID(dataId); ///明细是否存在
            if (SafetyDataEItem != null)
            {
                SafetyDataEItem.Title = content;
                BLL.SafetyDataEItemService.UpdateSafetyDataEItem(SafetyDataEItem);
            }
            else
            {
                Model.SafetyDataE_SafetyDataEItem newSafetyDataEItem = new Model.SafetyDataE_SafetyDataEItem
                {
                    SafetyDataEItemId = dataId,
                    SafetyDataEId = SafetyDataEId,
                    ProjectId = projectId
                };

                string newCode = BLL.SafetyDataEItemService.GetNewSafetyDataEItemCode(projectId, SafetyDataEId);
                newSafetyDataEItem.SortIndex = Funs.GetNewInt(newCode);                
                var safeData = BLL.SafetyDataEService.GetSafetyDataEBySafetyDataEId(SafetyDataEId);
                if (safeData != null && !string.IsNullOrEmpty(safeData.Code))
                {
                    newCode = safeData.Code + "-" + newCode;
                }
                var project = BLL.ProjectService.GetProjectByProjectId(projectId);
                if (project != null)
                {
                    newCode = project.ProjectCode + "-" + newCode;
                }

                newSafetyDataEItem.Code = newCode;                
                newSafetyDataEItem.Title = content;
                newSafetyDataEItem.CompileDate = System.DateTime.Now;  ////单据时间 【todo：要从页面处理】
                newSafetyDataEItem.SubmitDate = System.DateTime.Now;   ////单据提交时间     
                newSafetyDataEItem.IsMenu = true;
                newSafetyDataEItem.Url = url;
                BLL.SafetyDataEItemService.AddSafetyDataEItem(newSafetyDataEItem);
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// 更新是否考核项
        /// </summary>
        /// <param name="SafetyDataE"></param>
        public static void UpdateSafetyDataEIsCheck(string supSafetyDataEId)
        {
            if (!string.IsNullOrEmpty(supSafetyDataEId) && supSafetyDataEId != "0")
            {
                var supSafetyDataE = Funs.DB.SafetyDataE_SafetyDataE.FirstOrDefault(e => e.SafetyDataEId == supSafetyDataEId);
                if (supSafetyDataE != null && supSafetyDataE.IsCheck != true)
                {
                    supSafetyDataE.IsCheck = true;
                    Funs.DB.SubmitChanges();
                    if (!string.IsNullOrEmpty(supSafetyDataE.SupSafetyDataEId) && supSafetyDataE.SupSafetyDataEId != "0")
                    {
                        UpdateSafetyDataEIsCheck(supSafetyDataE.SupSafetyDataEId);
                    }
                }
            }
        }
    }
}
