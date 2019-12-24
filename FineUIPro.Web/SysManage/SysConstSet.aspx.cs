using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class SysConstSet : PageBase
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var thisUnit=BLL.CommonService.GetIsThisUnit();
                if (thisUnit != null)
                {
                    if (thisUnit.UnitId == BLL.Const.UnitId_14)
                    {
                        this.frFlowOperate.Hidden = false;
                    }
                    if (thisUnit.UnitId == BLL.Const.UnitId_CWCEC)
                    {
                        this.frChangeData.Hidden = false;
                    }
                    if (thisUnit.UnitId == BLL.Const.UnitId_SEDIN)
                    {
                        this.Tab3.Hidden = false;
                    }
                }

                /// TAB1加载页面方法
                this.LoadTab1Data();
                /// TAB2加载页面方法
                this.LoadTab2Data();
                /// TAB2加载页面方法
                this.LoadTab3Data();
            }
        }

        #region TAB1加载页面方法
        /// <summary>
        /// 加载页面方法
        /// </summary>
        private void LoadTab1Data()
        {
            var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_Synchronization).FirstOrDefault();
            if (sysSet != null)
            {
                if (sysSet.ConstValue == "1")
                {
                    this.ckSynchronization.Checked = true;
                }
                else
                {
                    this.ckSynchronization.Checked = false;
                }
            }
            var sysSet2 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_IsMonthReportGetAVG).FirstOrDefault();
            if (sysSet2 != null)
            {
                if (sysSet2.ConstValue == "1")
                {
                    this.ckIsMonthReportGetAVG.Checked = true;
                }
                else
                {
                    this.ckIsMonthReportGetAVG.Checked = false;
                }
            }
            var sysSet3 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MonthReportFreezeDay).FirstOrDefault();
            if (sysSet3 != null)
            {
                this.txtMonthReportFreezeDay.Text = sysSet3.ConstValue;
            }

            var sysSet4 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MenuFlowOperate).FirstOrDefault();
            if (sysSet4 != null)
            {
                if (sysSet4.ConstValue == "1")
                {
                    this.ckMenuFlowOperate.Checked = true;
                }
                else
                {
                    this.ckMenuFlowOperate.Checked = false;
                }
            }

            var sysSet5 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_ChangeData).FirstOrDefault();
            if (sysSet5 != null)
            {
                if (sysSet5.ConstValue == "1")
                {
                    this.ckChangeData.Checked = true;
                }
                else
                {
                    this.ckChangeData.Checked = false;
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var sysSet = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_Synchronization).FirstOrDefault();
            if (sysSet != null)
            {
                if (this.ckSynchronization.Checked == true)
                {
                    sysSet.ConstValue = "1";
                }
                else
                {
                    sysSet.ConstValue = "0";
                }
                Funs.DB.SubmitChanges();
            }
            var sysSet2 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_IsMonthReportGetAVG).FirstOrDefault();
            if (sysSet2 != null)
            {
                if (this.ckIsMonthReportGetAVG.Checked == true)
                {
                    sysSet2.ConstValue = "1";
                }
                else
                {
                    sysSet2.ConstValue = "0";
                }
                Funs.DB.SubmitChanges();
            }
            var sysSet3 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MonthReportFreezeDay).FirstOrDefault();
            if (sysSet3 != null)
            {
                sysSet3.ConstValue = this.txtMonthReportFreezeDay.Text.Trim();
                Funs.DB.SubmitChanges();
            }

            var sysSet4 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MenuFlowOperate).FirstOrDefault();
            if (sysSet4 != null)
            {
                if (this.ckMenuFlowOperate.Checked == true)
                {
                    sysSet4.ConstValue = "1";
                }
                else
                {
                    sysSet4.ConstValue = "0";
                }
                Funs.DB.SubmitChanges();
            }

            var sysSet5 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_ChangeData).FirstOrDefault();
            if (sysSet5 != null)
            {
                if (this.ckChangeData.Checked == true)
                {
                    sysSet5.ConstValue = "1";
                }
                else
                {
                    sysSet5.ConstValue = "0";
                }
                Funs.DB.SubmitChanges();
            }

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            BLL.LogService.AddSys_Log(this.CurrUser, "修改系统环境设置！", string.Empty, BLL.Const.SysConstSetMenuId, BLL.Const.BtnModify);
        }


        #region 多附件转换
        #region 附件路径多附件转化
        /// <summary>
        /// 附件路径多附件转化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnArrowRefresh_Click(object sender, EventArgs e)
        {
            ///法律法规
            var LawRegulationList = from x in Funs.DB.Law_LawRegulationList select x;
            if (LawRegulationList.Count() > 0)
            {
                foreach (var item in LawRegulationList)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.LawRegulationId, item.AttachUrl,BLL.Const.LawRegulationListMenuId);
                    }
                }
            }
            ////标准规范
            var HSSEStandardsList = from x in Funs.DB.Law_HSSEStandardsList select x;
            if (HSSEStandardsList.Count() > 0)
            {
                foreach (var item in HSSEStandardsList)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.StandardId, item.AttachUrl,BLL.Const.HSSEStandardListMenuId);
                    }
                }
            }
            ////规章制度
            var RulesRegulations = from x in Funs.DB.Law_RulesRegulations select x;
            if (RulesRegulations.Count() > 0)
            {
                foreach (var item in RulesRegulations)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.RulesRegulationsId, item.AttachUrl,BLL.Const.RulesRegulationsMenuId);
                    }
                }
            }
            ///管理规定
            var ManageRule = from x in Funs.DB.Law_ManageRule select x;
            if (ManageRule.Count() > 0)
            {
                foreach (var item in ManageRule)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.ManageRuleId, item.AttachUrl,BLL.Const.ManageRuleMenuId);
                    }
                }
            }
            ///安全组织体系
            var HSSEOrganize = from x in Funs.DB.HSSESystem_HSSEOrganize select x;
            if (HSSEOrganize.Count() > 0)
            {
                foreach (var item in HSSEOrganize)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.HSSEOrganizeId, item.AttachUrl,BLL.Const.HSSEOrganizeMenuId);
                    }
                }
            }

            ///培训教材库
            var TrainingItem = from x in Funs.DB.Training_TrainingItem select x;
            if (TrainingItem.Count() > 0)
            {
                foreach (var item in TrainingItem)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.TrainingItemId, item.AttachUrl,BLL.Const.TrainDBMenuId);
                    }
                }
            }
            ///安全试题库
            var TrainTestDBItem = from x in Funs.DB.Training_TrainTestDBItem select x;
            if (TrainTestDBItem.Count() > 0)
            {
                foreach (var item in TrainTestDBItem)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.TrainTestItemId, item.AttachUrl,BLL.Const.TestDBMenuId);
                    }
                }
            }

            ///HAZOP管理
            var HAZOP = from x in Funs.DB.Technique_HAZOP select x;
            if (HAZOP.Count() > 0)
            {
                foreach (var item in HAZOP)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.HAZOPId, item.AttachUrl,BLL.Const.HAZOPMenuId);
                    }
                }
            }
            ///安全评价
            var Appraise = from x in Funs.DB.Technique_Appraise select x;
            if (Appraise.Count() > 0)
            {
                foreach (var item in Appraise)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.AppraiseId, item.AttachUrl,BLL.Const.AppraiseMenuId);
                    }
                }
            }

            ///安全专家资质
            var Expert = from x in Funs.DB.Technique_Expert select x;
            if (Expert.Count() > 0)
            {
                foreach (var item in Expert)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.ExpertId, item.AttachUrl,BLL.Const.ExpertMenuId);
                    }
                }
            } 
            ///应急预案
            var Emergency = from x in Funs.DB.Technique_Emergency select x;
            if (Emergency.Count() > 0)
            {
                foreach (var item in Emergency)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.EmergencyId, item.AttachUrl,BLL.Const.EmergencyMenuId);
                    }
                }
            }
            ///专项方案
            var SpecialScheme = from x in Funs.DB.Technique_SpecialScheme select x;
            if (SpecialScheme.Count() > 0)
            {
                foreach (var item in SpecialScheme)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.SpecialSchemeId, item.AttachUrl,BLL.Const.SpecialSchemeMenuId);
                    }
                }
            }
            ///企业安全文件上报
            var SubUnitReportItem = from x in Funs.DB.Supervise_SubUnitReportItem select x;
            if (SubUnitReportItem.Count() > 0)
            {
                foreach (var item in SubUnitReportItem)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.SubUnitReportItemId, item.AttachUrl,BLL.Const.SubUnitReportMenuId);
                    }
                }
            }           
            ///绩效评价
            //var ProjectEvaluation = from x in Funs.DB.ProjectSupervision_ProjectEvaluation select x;
            //if (ProjectEvaluation.Count() > 0)
            //{
            //    foreach (var item in ProjectEvaluation)
            //    {
            //        if (!string.IsNullOrEmpty(item.AttachUrl))
            //        {
            //            this.InsertAttachFile(item.ProjectEvaluationId, item.AttachUrl,BLL.Const.ProjectEvaluationMenuId);
            //        }
            //    }
            //}
            ///事故快报
            var AccidentReport = from x in Funs.DB.ProjectAccident_AccidentReport select x;
            if (AccidentReport.Count() > 0)
            {
                foreach (var item in AccidentReport)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.AccidentReportId, item.AttachUrl,BLL.Const.ServerAccidentReportMenuId);
                    }
                }
            }
            ///事故处理
            var AccidentStatistics = from x in Funs.DB.ProjectAccident_AccidentStatistics select x;
            if (AccidentStatistics.Count() > 0)
            {
                foreach (var item in AccidentStatistics)
                {
                    if (!string.IsNullOrEmpty(item.AttachUrl))
                    {
                        this.InsertAttachFile(item.AccidentStatisticsId, item.AttachUrl,BLL.Const.ServerAccidentStatisticsMenuId);
                    }
                }
            }
            ShowNotify("多附件转化完成！", MessageBoxIcon.Success);
        }
         #endregion

        #region 多附件转化方法
        /// <summary>
        /// 多附件转化方法
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="attachUrl"></param>
        private void InsertAttachFile(string ID, string attachUrl, string menuId)
        {
            var att = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == ID);
            if (att == null)
            {
                Model.AttachFile newAttachFile = new Model.AttachFile
                {
                    AttachFileId = SQLHelper.GetNewID(typeof(Model.AttachFile)),
                    ToKeyId = ID,
                    AttachSource = BLL.UploadFileService.GetSourceByAttachUrl(attachUrl, 100, string.Empty),
                    AttachUrl = attachUrl,
                    MenuId = menuId
                };
                Funs.DB.AttachFile.InsertOnSubmit(newAttachFile);
                Funs.DB.SubmitChanges();
            }
            else
            {
                if (string.IsNullOrEmpty(att.MenuId))
                {
                    att.MenuId = menuId;
                    Funs.DB.SubmitChanges();
                }
            }
        }
        #endregion
        #endregion
        #endregion

        #region TAB2加载页面方法
        /// <summary>
        /// TAB2加载页面方法
        /// </summary>
        private void LoadTab2Data()
        {
            this.treeMenu.Nodes.Clear();
            var sysMenu = BLL.SysMenuService.GetIsUsedMenuListByMenuType(this.rblMenuType.SelectedValue);
            if (sysMenu.Count() > 0)
            {
                this.InitTreeMenu(sysMenu, null);
            }            
        }

        #region 加载菜单下拉框树
        /// <summary>
        /// 加载菜单下拉框树
        /// </summary>
        private void InitTreeMenu(List<Model.Sys_Menu> menusList, TreeNode node)
        {
            string supMenu = "0";
            if (node != null)
            { 
                supMenu = node.NodeID;
            }
            var menuItemList = menusList.Where(x => x.SuperMenu == supMenu).OrderBy(x => x.SortIndex);    //获取菜单列表
            if (menuItemList.Count() > 0)
            {
                foreach (var item in menuItemList)
                {
                    var noMenu = Const.noSysSetMenusList.FirstOrDefault(x => x == item.MenuId);
                    if (noMenu == null)
                    {
                        TreeNode newNode = new TreeNode
                        {
                            Text = item.MenuName,
                            NodeID = item.MenuId,                          
                        };
                        
                        if (node == null)
                        {                            
                            this.treeMenu.Nodes.Add(newNode);
                        }
                        else
                        {
                            node.Nodes.Add(newNode);
                        }
                        if (!item.IsEnd.HasValue || item.IsEnd == false)
                        {
                            InitTreeMenu(menusList, newNode);
                        }
                    }
                }
            }
        }
        #endregion

        #region 下拉框回发事件
        /// <summary>
        /// 下拉框回发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpMenu_TextChanged(object sender, EventArgs e)
        {
            string menuId = this.drpMenu.Value;
            ///加载流程列表
            this.BindGrid();
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(menuId);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
                var codeTemplateRule = BLL.SysConstSetService.GetCodeTemplateRuleByMenuId(sysMenu.MenuId);
                if (codeTemplateRule != null)
                {
                    if (codeTemplateRule.IsProjectCode == true)
                    {
                        this.ckProjectCode.Checked = true;
                    }
                    else
                    {
                        this.ckProjectCode.Checked = false;
                    }
                    this.txtPrefix.Text = codeTemplateRule.Prefix;
                    if (codeTemplateRule.IsUnitCode == true)
                    {
                        this.ckUnitCode.Checked = true;
                    }
                    else
                    {
                        this.ckUnitCode.Checked = false;
                    }
                    this.txtDigit.Text = codeTemplateRule.Digit.ToString();
                    this.txtTemplate.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    this.txtSymbol.Text = codeTemplateRule.Symbol;
                    if (codeTemplateRule.IsFileCabinetA == true)
                    {
                        this.ckIsFileCabinetA.Checked = true;
                    }
                    else
                    {
                        this.ckIsFileCabinetA.Checked = false;
                    }
                    if (codeTemplateRule.IsFileCabinetB == true)
                    {
                        this.ckIsFileCabinetB.Checked = true;
                    }
                    else
                    {
                        this.ckIsFileCabinetB.Checked = false;
                    }
                }
                else
                {
                    this.ckProjectCode.Checked = true;
                    this.txtDigit.Text = "4";
                    this.txtSymbol.Text = "-";
                    this.txtPrefix.Text = string.Empty;
                    this.ckUnitCode.Checked = false;
                    this.txtTemplate.Text = HttpUtility.HtmlDecode(string.Empty);

                    this.ckIsFileCabinetA.Checked = false;
                    this.ckIsFileCabinetB.Checked = true;
                }
            }
            else
            {
                this.drpMenu.Text = string.Empty;
                this.drpMenu.Value = string.Empty;
                if (sysMenu != null)
                { 
                    ShowNotify("请选择末级菜单操作！", MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #region 流程列表绑定数据
        /// <summary>
        /// 流程列表绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT flow.FlowOperateId,flow.MenuId,flow.FlowStep,flow.GroupNum,flow.OrderNum,flow.AuditFlowName,flow.RoleId,flow.IsFlowEnd"                
                + @" FROM dbo.Sys_MenuFlowOperate AS flow "
                + @" WHERE flow.MenuId=@MenuId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            string menuId = string.Empty;
            if (!string.IsNullOrEmpty(this.drpMenu.Value))
            {
                menuId = this.drpMenu.Value;
            }
            listStr.Add(new SqlParameter("@MenuId", menuId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            // 2.获取当前分页数据
            //var table = this.GetPagedDataTable(Grid1, tb1);
            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();

            if (CommonService.GetIsThisUnit(Const.UnitId_SEDIN) && LicensePublicService.lisenWorkList.Contains(this.drpMenu.Value))
            {
                this.Grid1.Columns[1].Hidden = false;
                this.Grid1.Columns[2].Hidden = false;
            }
            else
            {
                this.Grid1.Columns[1].Hidden = true;
                this.Grid1.Columns[2].Hidden = true;               
            }
        }
        
        /// <summary>
        /// 得到角色名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertRole(object roleIds)
        {
            return BLL.RoleService.getRoleNamesRoleIds(roleIds);
        }

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 增加编辑事件
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFlowOperateNew_Click(object sender, EventArgs e)
        {
            var sysMenu = SysMenuService.GetSysMenuByMenuId(this.drpMenu.Value);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
                var getMenuFlowOperate = Funs.DB.Sys_MenuFlowOperate.FirstOrDefault(x => x.MenuId == sysMenu.MenuId && x.IsFlowEnd == true);
                if (getMenuFlowOperate == null)
                {
                    PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MenuFlowOperateEdit.aspx?MenuId={0}&FlowOperateId={1}", sysMenu.MenuId, string.Empty, "增加 - ")));
                }
                else
                {
                    Alert.ShowInParent("流程已存在结束步骤！", MessageBoxIcon.Warning);
                }
                
            }           
        }

        /// <summary>
        /// Grid双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {          
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(this.drpMenu.Value);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("MenuFlowOperateEdit.aspx?MenuId={0}&FlowOperateId={1}", sysMenu.MenuId, Grid1.SelectedRowID, "编辑 - ")));
            }
        }
        #endregion

        #region  删除数据
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFlowOperateDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                if (CommonService.GetIsThisUnit(Const.UnitId_SEDIN) && LicensePublicService.lisenWorkList.Contains(this.drpMenu.Value))
                {
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        BLL.SysConstSetService.DeleteMenuFlowOperateLicense(rowID);
                    }
                }
                else
                {
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        BLL.SysConstSetService.DeleteMenuFlowOperateByFlowOperateId(rowID);
                    }
                }

                BLL.MenuFlowOperateService.SetSortIndex(this.drpMenu.Value);
                BindGrid();
                BLL.LogService.AddSys_Log(this.CurrUser, "删除审批流程信息！", null, BLL.Const.SysConstSetMenuId, BLL.Const.BtnDelete);
                ShowNotify("删除数据成功!");
            }
        }
        #endregion

        #region 关闭弹出窗口
        /// <summary>
        /// 关闭弹出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion
        #endregion

        #region TAB2保存按钮
        /// <summary>
        /// TAB2保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTab2Save_Click(object sender, EventArgs e)
        {
            var sysMenu = BLL.SysMenuService.GetSysMenuByMenuId(this.drpMenu.Value);
            if (sysMenu != null && sysMenu.IsEnd == true)
            {
                this.SaveData2(sysMenu.MenuId);
                BLL.LogService.AddSys_Log(this.CurrUser, "修改菜单编码模板设置！", null, BLL.Const.SysConstSetMenuId, BLL.Const.BtnModify);
                ShowNotify("保存成功！", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("请选择菜单！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// TAB2保存方法
        /// </summary>
        private void SaveData2(string menuId)
        {
            Model.Sys_CodeTemplateRule newCodeTemplateRule = new Model.Sys_CodeTemplateRule
            {
                MenuId = menuId,
                Template = HttpUtility.HtmlEncode(this.txtTemplate.Text),
                Symbol = this.txtSymbol.Text.Trim(),
                IsProjectCode = this.ckProjectCode.Checked,
                Prefix = this.txtPrefix.Text.Trim(),
                IsUnitCode = this.ckUnitCode.Checked,
                Digit = Funs.GetNewInt(this.txtDigit.Text),
                IsFileCabinetA = this.ckIsFileCabinetA.Checked,
                IsFileCabinetB = this.ckIsFileCabinetB.Checked
            };
            var getCodeTemplateRule = BLL.SysConstSetService.GetCodeTemplateRuleByMenuId(menuId);
            if (getCodeTemplateRule != null)
            {
                newCodeTemplateRule.CodeTemplateRuleId = getCodeTemplateRule.CodeTemplateRuleId;
                BLL.SysConstSetService.UpdateCodeTemplateRule(newCodeTemplateRule);
            }
            else
            {
                BLL.SysConstSetService.AddCodeTemplateRule(newCodeTemplateRule);
            }
        }
        #endregion

        #endregion

        #region TAB3加载页面方法
        /// <summary>
        /// 加载页面方法
        /// </summary>
        private void LoadTab3Data()
        {
            var sysTestRule = Funs.DB.Sys_TestRule.FirstOrDefault();
            if (sysTestRule != null)
            {
                this.txtDuration.Text = sysTestRule.Duration.ToString();
                this.txtSValue.Text = sysTestRule.SValue.ToString();
                this.txtMValue.Text = sysTestRule.MValue.ToString();
                this.txtJValue.Text = sysTestRule.JValue.ToString();
                this.txtSCount.Text = sysTestRule.SCount.ToString();
                this.txtMCount.Text = sysTestRule.MCount.ToString();
                this.txtJCount.Text = sysTestRule.JCount.ToString();                
                txtTab3_TextChanged(null, null);
                this.txtPassingScore.Text = sysTestRule.PassingScore.ToString();
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTab3Save_Click(object sender, EventArgs e)
        {
            var getTestRule = from x in Funs.DB.Sys_TestRule select x;
            if (getTestRule.Count() > 0)
            {
                Funs.DB.Sys_TestRule.DeleteAllOnSubmit(getTestRule);
            }

            Model.Sys_TestRule newTestRule = new Model.Sys_TestRule
            {
                TestRuleId = SQLHelper.GetNewID(),
                Duration = Funs.GetNewIntOrZero(this.txtDuration.Text),
                SValue = Funs.GetNewIntOrZero(this.txtSValue.Text),
                MValue = Funs.GetNewIntOrZero(this.txtMValue.Text),
                JValue = Funs.GetNewIntOrZero(this.txtJValue.Text),
                SCount = Funs.GetNewIntOrZero(this.txtSCount.Text),
                MCount = Funs.GetNewIntOrZero(this.txtMCount.Text),
                JCount = Funs.GetNewIntOrZero(this.txtJCount.Text),
                PassingScore = Funs.GetNewIntOrZero(this.txtPassingScore.Text),
            };

            Funs.DB.Sys_TestRule.InsertOnSubmit(newTestRule);
            Funs.DB.SubmitChanges();

            ShowNotify("保存成功！", MessageBoxIcon.Success);
            LogService.AddSys_Log(this.CurrUser, "修改考试规则设置！", string.Empty, Const.SysConstSetMenuId, Const.BtnModify);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtTab3_TextChanged(object sender, EventArgs e)
        {
            int SValue = Funs.GetNewIntOrZero(this.txtSValue.Text);
            int MValue = Funs.GetNewIntOrZero(this.txtMValue.Text);
            int JValue = Funs.GetNewIntOrZero(this.txtJValue.Text);
            int SCount = Funs.GetNewIntOrZero(this.txtSCount.Text);
            int MCount = Funs.GetNewIntOrZero(this.txtMCount.Text);
            int JCount = Funs.GetNewIntOrZero(this.txtJCount.Text);
            this.lbTotalScore.Text = (SCount * SValue + MCount * MValue + JCount * JValue).ToString();
            this.lbTotalCount.Text = (SCount + MCount + JCount).ToString();
        }
        #endregion

        /// <summary>
        ///  选择菜单类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblMenuType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadTab2Data();
            this.drpMenu.Text = string.Empty;
            this.drpMenu.Value = string.Empty;
        }
    }
}