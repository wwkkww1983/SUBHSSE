using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Controls
{
    public partial class FlowOperateControl : System.Web.UI.UserControl
    {
        #region 定义项
        /// <summary>
        /// 单据ID
        /// </summary>
        public string DataId
        {
            get;
            set;
        }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuId
        {
            get;
            set;
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public string ProjectId
        {
            get;
            set;
        }

        /// <summary>
        /// 单位iID
        /// </summary>
        public string UnitId
        {
            get;
            set;
        }
        /// <summary>
        /// 下一步骤
        /// </summary>
        public string NextStep
        {
            get
            {
                if (!string.IsNullOrEmpty(this.rblFlowOperate.SelectedValue))
                {
                    return this.rblFlowOperate.SelectedValue;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 下一步办理人
        /// </summary>
        public string NextPerson
        {
            get
            {
                return this.drpPerson.SelectedValue;
            }
        }      
        #endregion

        #region 定义页面项
        /// <summary>
        /// 单据ID
        /// </summary>
        public string getDataId
        {
            get
            {
                return (string)ViewState["getDataId"];
            }
            set
            {
                ViewState["getDataId"] = value;
            }
        }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public string getMenuId
        {
            get
            {
                return (string)ViewState["getMenuId"];
            }
            set
            {
                ViewState["getMenuId"] = value;
            }
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {       
                this.getDataId = this.DataId;
                this.getMenuId = this.MenuId;
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpPerson, this.ProjectId,this.UnitId, true);
                this.GroupPanel1.TitleToolTip += BLL.MenuFlowOperateService.GetFlowOperateName(this.getMenuId);
                this.txtAuditFlowName.Text = "审核/审批";
                var flowOperate = Funs.DB.Sys_FlowOperate.FirstOrDefault(x => x.DataId == this.getDataId && x.State == BLL.Const.State_2 && x.IsClosed == true);
                if (flowOperate != null)
                {
                    this.GroupPanel1.Hidden = true;
                    this.GroupPanel2.Collapsed = false;
                }
                else
                {
                    var sysSet4 = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_MenuFlowOperate).FirstOrDefault();
                    if (sysSet4 != null && sysSet4.ConstValue == "1")
                    {
                        ///取当前单据审核步骤
                        int nextSortIndex = 1;
                        var maxFlowOperate = Funs.DB.Sys_FlowOperate.Where(x => x.DataId == this.getDataId && x.IsClosed == false).Max(x => x.SortIndex);
                        if (maxFlowOperate != null)
                        {
                            nextSortIndex = maxFlowOperate.Value;
                        }
                        else
                        {
                            this.txtAuditFlowName.Text = "编制单据";
                        }

                        var nextMenuFlowOperate = Funs.DB.Sys_MenuFlowOperate.FirstOrDefault(x => x.MenuId == this.MenuId && x.FlowStep == nextSortIndex);
                        if (nextMenuFlowOperate != null && nextMenuFlowOperate.IsFlowEnd != true)
                        {
                            this.rblFlowOperate.Enabled = false;
                            this.txtAuditFlowName.Text = nextMenuFlowOperate.AuditFlowName;
                            this.drpPerson.Items.Clear();
                            BLL.UserService.InitUserProjectIdRoleIdDropDownList(this.drpPerson, this.ProjectId, nextMenuFlowOperate.RoleId, true);
                        }
                        else
                        {
                            this.rblFlowOperate.Enabled = false;
                            this.rblFlowOperate.SelectedValue = BLL.Const.State_2;
                            this.SetFlowOperateEnd();
                        }
                    }
                }
                
                if (string.IsNullOrEmpty(this.ProjectId))
                {
                    this.IsFileCabinetA.Hidden = true;
                }

                this.BindGrid();
            }
        }
        #endregion

        #region 流程列表绑定数据
        /// <summary>
        /// 流程列表绑定数据
        /// </summary>
        private void BindGrid()
        {
            DataTable getDataTable = new DataTable();
            if (!string.IsNullOrEmpty(this.getDataId))
            {
                string strSql = @"SELECT FlowOperateId,MenuId,DataId,SortIndex,AuditFlowName,OperaterId,Users.UserName AS OperaterName,OperaterTime,Opinion,IsClosed "
                    + @" FROM dbo.Sys_FlowOperate LEFT JOIN Sys_User AS Users ON OperaterId = Users.UserId"
                    + @" WHERE IsClosed = 1 AND DataId=@DataId ORDER BY SortIndex DESC";
                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@DataId", this.getDataId));
                SqlParameter[] parameter = listStr.ToArray();
                getDataTable = SQLHelper.GetDataTableRunText(strSql, parameter);
            }

            Grid1.DataSource = getDataTable;
            Grid1.DataBind();
        }
        #endregion

        #region 下拉联动事件
        /// <summary>
        ///  处理方式下拉事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblFlowOperate_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetFlowOperateEnd();
        }

        /// <summary>
        /// 设置审核完成状态事件
        /// </summary>
        private void SetFlowOperateEnd()
        {
            if (this.rblFlowOperate.SelectedValue == BLL.Const.State_2)
            {
                this.txtAuditFlowName.Text = "审核完成";
                this.drpPerson.SelectedValue = BLL.Const._Null;
                this.drpPerson.Hidden = true;
                this.IsFileCabinetA.Hidden = false;
                var codeTemplateRule = Funs.DB.Sys_CodeTemplateRule.FirstOrDefault(x => x.MenuId == this.getMenuId && x.IsFileCabinetA == true);
                if (codeTemplateRule != null)
                {
                    this.IsFileCabinetA.Checked = true;
                }
            }
            else
            {
                ///取当前单据审核步骤
                int nextSortIndex = 1;
                var maxFlowOperate = Funs.DB.Sys_FlowOperate.Where(x => x.DataId == this.getDataId).Max(x => x.SortIndex);
                if (maxFlowOperate != null)
                {
                    nextSortIndex = maxFlowOperate.Value + 1;
                }
                else
                {
                    this.txtAuditFlowName.Text = "编制单据";
                }

                var nextMenuFlowOperate = Funs.DB.Sys_MenuFlowOperate.FirstOrDefault(x => x.MenuId == this.MenuId && x.FlowStep == nextSortIndex);
                if (nextMenuFlowOperate != null && nextMenuFlowOperate.IsFlowEnd != true)
                {
                    this.txtAuditFlowName.Text = nextMenuFlowOperate.AuditFlowName;
                    this.drpPerson.Items.Clear();
                    BLL.UserService.InitUserProjectIdRoleIdDropDownList(this.drpPerson, this.ProjectId, nextMenuFlowOperate.RoleId, true);
                }

                this.drpPerson.Hidden = false;
                this.IsFileCabinetA.Hidden = true;
                this.IsFileCabinetA.Checked = false;
            }
        }

        /// <summary>
        /// 得到角色名称字符串
        /// </summary>
        /// <param name="bigType"></param>
        /// <returns></returns>
        protected string ConvertRole(string roleIds)
        {
            return BLL.RoleService.getRoleNamesRoleIds(roleIds);
        }
        #endregion

        #region 组面板 折叠展开事件
        /// <summary>
        /// 组面板 折叠展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GroupPanel_Collapse(object sender, EventArgs e)
        {
            if (this.GroupPanel1.Collapsed)
            {
                this.GroupPanel2.Collapsed = false;
            }
        }

        /// <summary>
        /// 组面板 折叠展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GroupPanel2_Collapse(object sender, EventArgs e)
        {
            if (this.GroupPanel2.Collapsed)
            {
                this.GroupPanel1.Collapsed = false;
            }
        }

        /// <summary>
        /// 组面板 折叠展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GroupPanel_Expand(object sender, EventArgs e)
        {
            if (this.GroupPanel1.Expanded)
            {
                this.GroupPanel2.Expanded = false;
            }
        }

        /// <summary>
        /// 组面板 折叠展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GroupPanel2_Expand(object sender, EventArgs e)
        {
            if (this.GroupPanel2.Expanded)
            {
                this.GroupPanel1.Expanded = false;
            }
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">主键id</param>
        /// <param name="isClosed">是否关闭这步流程</param>
        /// <param name="content">单据内容</param>
        /// <param name="url">路径</param>
        public void btnSaveData(string projectId, string menuId, string dataId, bool isClosed, string content, string url)
        {
            Model.Sys_FlowOperate newFlowOperate = new Model.Sys_FlowOperate
            {
                MenuId = this.getMenuId,
                DataId = dataId,
                OperaterId = ((Model.Sys_User)Session["CurrUser"]).UserId,
                State = this.rblFlowOperate.SelectedValue,
                IsClosed = isClosed,
                Opinion = this.txtOpinions.Text.Trim(),
                ProjectId = projectId,
                Url = url
            };
            var user = BLL.UserService.GetUserByUserId(newFlowOperate.OperaterId);
            if (user != null)
            {
                var roles = BLL.RoleService.GetRoleByRoleId(user.RoleId);
                if (roles != null && !string.IsNullOrEmpty(roles.RoleName))
                {
                    newFlowOperate.AuditFlowName = "[" + roles.RoleName + "]";
                }
                else
                {
                    newFlowOperate.AuditFlowName = "[" + user.UserName + "]" ;
                }

                newFlowOperate.AuditFlowName += this.txtAuditFlowName.Text;
            }

            var updateFlowOperate = from x in Funs.DB.Sys_FlowOperate
                                    where x.DataId == newFlowOperate.DataId && (x.IsClosed == false || !x.IsClosed.HasValue)
                                    select x;
            if (updateFlowOperate.Count() > 0)
            {
                foreach (var item in updateFlowOperate)
                {
                    item.OperaterId = newFlowOperate.OperaterId;
                    item.OperaterTime = System.DateTime.Now;
                    item.State = newFlowOperate.State;
                    item.Opinion = newFlowOperate.Opinion;
                    item.AuditFlowName = this.txtAuditFlowName.Text;
                    item.IsClosed = newFlowOperate.IsClosed;
                    Funs.DB.SubmitChanges();
                }
            }
            else
            {
                int maxSortIndex = 1;
                var flowSet = Funs.DB.Sys_FlowOperate.Where(x => x.DataId == newFlowOperate.DataId);
                var sortIndex = flowSet.Select(x => x.SortIndex).Max();
                if (sortIndex.HasValue)
                {
                    maxSortIndex = sortIndex.Value + 1;
                }
                newFlowOperate.FlowOperateId = SQLHelper.GetNewID(typeof(Model.Sys_FlowOperate));
                newFlowOperate.SortIndex = maxSortIndex;
                newFlowOperate.OperaterTime = System.DateTime.Now;
                newFlowOperate.AuditFlowName = this.txtAuditFlowName.Text;                
                Funs.DB.Sys_FlowOperate.InsertOnSubmit(newFlowOperate);
                Funs.DB.SubmitChanges();
            }

            if (newFlowOperate.IsClosed == true)
            {
                var updateNoClosedFlowOperate = from x in Funs.DB.Sys_FlowOperate
                                        where x.DataId == newFlowOperate.DataId && (x.IsClosed == false || !x.IsClosed.HasValue)
                                        select x;
                if (updateNoClosedFlowOperate.Count() > 0)
                {
                    foreach (var itemClosed in updateNoClosedFlowOperate)
                    {
                        itemClosed.IsClosed = true;
                        Funs.DB.SubmitChanges();
                    }
                }

                if (newFlowOperate.State != BLL.Const.State_2) ///未审核完成的时 增加下一步办理
                {
                    int maxSortIndex = 1;
                    var flowSet = Funs.DB.Sys_FlowOperate.Where(x => x.DataId == newFlowOperate.DataId);
                    var sortIndex = flowSet.Select(x => x.SortIndex).Max();
                    if (sortIndex.HasValue)
                    {
                        maxSortIndex = sortIndex.Value + 1;
                    }

                    Model.Sys_FlowOperate newNextFlowOperate = new Model.Sys_FlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(typeof(Model.Sys_FlowOperate)),
                        MenuId = newFlowOperate.MenuId,
                        DataId = newFlowOperate.DataId,
                        ProjectId = newFlowOperate.ProjectId,
                        Url = newFlowOperate.Url,
                        SortIndex = maxSortIndex,
                        OperaterTime = System.DateTime.Now
                    };
                    if (this.drpPerson.SelectedValue != BLL.Const._Null)
                    {
                        newNextFlowOperate.OperaterId = this.drpPerson.SelectedValue;
                    }
                    else
                    {
                        newNextFlowOperate.OperaterId = newFlowOperate.OperaterId;
                    }
                    var operaterUsers = BLL.UserService.GetUserByUserId(newNextFlowOperate.OperaterId);
                    if (operaterUsers != null)
                    {
                        var operaterRoles = BLL.RoleService.GetRoleByRoleId(operaterUsers.RoleId);
                        if (operaterRoles != null && !string.IsNullOrEmpty(operaterRoles.RoleName))
                        {
                            newNextFlowOperate.AuditFlowName = "[" + operaterRoles.RoleName + "]" ;
                        }
                        else
                        {
                            newNextFlowOperate.AuditFlowName = "[" + operaterUsers.UserName + "]" ;
                        }

                        newNextFlowOperate.AuditFlowName += this.txtAuditFlowName.Text;
                    }
                    newNextFlowOperate.IsClosed = false;
                    newNextFlowOperate.State = BLL.Const.State_1;
                    Funs.DB.Sys_FlowOperate.InsertOnSubmit(newNextFlowOperate);
                    Funs.DB.SubmitChanges();
                }
                else  ///单据审核完成
                {
                    if (this.IsFileCabinetA.Checked) /// 选择归档【文件柜A(检查类)】
                    {
                       BLL.FileCabinetAService.AddFileCabinetA(menuId, dataId, content, url, projectId);
                    }

                    ////判断单据是否 加入到企业管理资料
                    var safeData = Funs.DB.SafetyData_SafetyData.FirstOrDefault(x => x.MenuId == menuId);
                    if (safeData != null)
                    {
                        BLL.SafetyDataService.AddSafetyData(menuId, dataId, content, url, projectId);
                    }

                    /// 在单据审核完成后 收集工程师日志 在单据中取值
                    this.CollectHSSELogByData(newFlowOperate.ProjectId, newFlowOperate.MenuId, newFlowOperate.DataId);
                }
                ///单据审核时 收集工程师日志 审核流程中取值
                this.CollectHSSELog(newFlowOperate.ProjectId, newFlowOperate.MenuId, newFlowOperate.DataId, newFlowOperate.OperaterId, newFlowOperate.OperaterTime);                
            }
        }
        #endregion

        #region 收集工程师日志 审核流程中取值
        /// <summary>
        /// 收集工程师日志记录
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">单据id</param>
        /// <param name="operaterId">操作人id</param>
        /// <param name="date">日期</param>
        private void CollectHSSELog(string projectId, string menuId, string dataId, string operaterId, DateTime? date)
        {
            switch (menuId)
            {                          
                case BLL.Const.ProjectRectifyNoticeMenuId:                    
                    var rectifyNotice = BLL.RectifyNoticesService.GetRectifyNoticesById(dataId);
                    if (rectifyNotice != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "22", rectifyNotice.WrongContent, Const.BtnAdd, 1);
                    }                     
                    break;
                case BLL.Const.ProjectLicenseManagerMenuId:
                    var license = BLL.LicenseManagerService.GetLicenseManagerById(dataId);
                    if (license != null)
                    {                        
                        var licenseType = BLL.LicenseTypeService.GetLicenseTypeById(license.LicenseTypeId);
                        if (licenseType != null)
                        {
                            BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "23", licenseType.LicenseTypeName, Const.BtnAdd, 1);
                        }
                    }
                    break;
                case BLL.Const.ProjectEquipmentSafetyListMenuId:
                    var equipment = BLL.EquipmentSafetyListService.GetEquipmentSafetyListById(dataId);
                    if (equipment != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "24", equipment.EquipmentSafetyListName, Const.BtnAdd, 1);
                    }
                    break;
                case BLL.Const.ProjectHazardListMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "25", "职业健康安全危险源辨识与评价", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectEnvironmentalRiskListMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "25", "环境危险源辨识与评价", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectDrillRecordListMenuId:                     
                    var drillRecord = BLL.DrillRecordListService.GetDrillRecordListById(dataId);
                    if (drillRecord != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "26", "应急演练:" + drillRecord.DrillRecordName, Const.BtnAdd, 1);
                    }
                    break;
                case BLL.Const.ProjectTrainRecordMenuId:
                    var trainRecord = BLL.EduTrain_TrainRecordService.GetTrainingByTrainingId(dataId);
                    if (trainRecord != null)
                    {
                        var details = BLL.EduTrain_TrainRecordDetailService.GetTrainRecordDetailByTrainingId(dataId);
                        BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "27", trainRecord.TrainContent, Const.BtnAdd, details.Count());
                    }
                    break;
                case BLL.Const.ProjectWeekMeetingMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "28", "周例会", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectMonthMeetingMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "28", "月例会", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectSpecialMeetingMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "28", "专题会议", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectAttendMeetingMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "28", "其他会议", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectPromotionalActivitiesMenuId:
                    var promotionalActivities = BLL.PromotionalActivitiesService.GetPromotionalActivitiesById(dataId);
                    if (promotionalActivities != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "29", promotionalActivities.Title, Const.BtnAdd, 1);
                    }
                    break;
                case BLL.Const.ProjectIncentiveNoticeMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "210", "奖励通知单", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectPunishNoticeMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "211", "处罚通知单", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectActionPlanListMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "31", "施工HSE实施计划", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ActionPlan_ManagerRuleMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "31", "项目现场HSE教育管理规定", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectConstructSolutionMenuId:                    
                    var solution = BLL.ConstructSolutionService.GetConstructSolutionById(dataId);
                    if (solution != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "32", solution.ConstructSolutionName, Const.BtnAdd, 1);
                    }                    
                    break;
                case BLL.Const.ProjectLargerHazardListMenuId:
                    BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "32", "危险性较大的工程清单", Const.BtnAdd, 1);
                    break;
                case BLL.Const.ProjectCostSmallDetailMenuId:
                    var costSmallDetail = BLL.CostSmallDetailService.GetCostSmallDetailById(dataId);
                    if (costSmallDetail != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, operaterId, date, "33", "审核" + BLL.UnitService.GetUnitNameByUnitId(costSmallDetail.UnitId) + "的费用申请", Const.BtnAdd, 1);
                    }
                    break;
            }
        }

        #endregion

        #region 收集工程师日志 在单据中取值
        /// <summary>
        /// 收集工程师日志记录
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="menuId">菜单id</param>
        /// <param name="dataId">单据id</param>
        /// <param name="operaterId">操作人id</param>
        /// <param name="date">日期</param>
        private void CollectHSSELogByData(string projectId, string menuId, string dataId)
        {
            switch (menuId)
            {
                case BLL.Const.ProjectCheckDayMenuId:
                    var checkDay = BLL.Check_CheckDayService.GetCheckDayByCheckDayId(dataId);
                    if (checkDay != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, checkDay.CheckPerson, checkDay.CheckTime, "21", "日常巡检", Const.BtnAdd, 1);
                    }
                    break;
                case BLL.Const.ProjectCheckSpecialMenuId:
                    var checkSpecial = BLL.Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(dataId);
                    if (checkSpecial != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, checkSpecial.CheckPerson, checkSpecial.CheckTime, "21", "专项检查", Const.BtnAdd, 1);
                        if (!string.IsNullOrEmpty(checkSpecial.PartInPersonIds))
                        {
                            List<string> partInPersonIds = Funs.GetStrListByStr(checkSpecial.PartInPersonIds, ',');
                            foreach (var item in partInPersonIds)
                            {
                                BLL.HSSELogService.CollectHSSELog(projectId, item, checkSpecial.CheckTime, "21", "专项检查", Const.BtnAdd, 1);
                            }
                        }
                    }
                    break;
                case BLL.Const.ProjectCheckColligationMenuId:
                    var checkColligation = BLL.Check_CheckColligationService.GetCheckColligationByCheckColligationId(dataId);
                    if (checkColligation != null)
                    {
                        BLL.HSSELogService.CollectHSSELog(projectId, checkColligation.CheckPerson, checkColligation.CheckTime, "21", "综合检查", Const.BtnAdd, 1);
                        if (!string.IsNullOrEmpty(checkColligation.PartInPersonIds))
                        {
                            List<string> partInPersonIds = Funs.GetStrListByStr(checkColligation.PartInPersonIds, ',');
                            foreach (var item in partInPersonIds)
                            {
                                BLL.HSSELogService.CollectHSSELog(projectId, item, checkColligation.CheckTime, "21", "综合检查", Const.BtnAdd, 1);
                            }
                        }
                    }
                    break;
                case BLL.Const.ProjectCheckWorkMenuId:
                    var checkWork = BLL.Check_CheckWorkService.GetCheckWorkByCheckWorkId(dataId);
                    if (checkWork != null)
                    {
                        if (!string.IsNullOrEmpty(checkWork.MainUnitPerson))
                        {
                            List<string> mainUnitPersonIds = Funs.GetStrListByStr(checkWork.MainUnitPerson, ',');
                            foreach (var item in mainUnitPersonIds)
                            {
                                BLL.HSSELogService.CollectHSSELog(projectId, item, checkWork.CheckTime, "21", "开工前HSE检查", Const.BtnAdd, 1);
                            }
                        }
                        if (!string.IsNullOrEmpty(checkWork.SubUnitPerson))
                        {
                            List<string> subUnitPersonIds = Funs.GetStrListByStr(checkWork.SubUnitPerson, ',');
                            foreach (var item in subUnitPersonIds)
                            {
                                BLL.HSSELogService.CollectHSSELog(projectId, item, checkWork.CheckTime, "21", "开工前HSE检查", Const.BtnAdd, 1);
                            }
                        }
                    }
                    break;
                case BLL.Const.ProjectCheckHolidayMenuId:
                    var checkHoliday = BLL.Check_CheckHolidayService.GetCheckHolidayByCheckHolidayId(dataId);
                    if (checkHoliday != null)
                    {
                        if (!string.IsNullOrEmpty(checkHoliday.MainUnitPerson))
                        {
                            List<string> mainUnitPersonIds = Funs.GetStrListByStr(checkHoliday.MainUnitPerson, ',');
                            foreach (var item in mainUnitPersonIds)
                            {
                                BLL.HSSELogService.CollectHSSELog(projectId, item, checkHoliday.CheckTime, "21", "季节性和节假日前HSE检查", Const.BtnAdd, 1);
                            }
                        }
                        if (!string.IsNullOrEmpty(checkHoliday.SubUnitPerson))
                        {
                            List<string> subUnitPersonIds = Funs.GetStrListByStr(checkHoliday.SubUnitPerson, ',');
                            foreach (var item in subUnitPersonIds)
                            {
                                BLL.HSSELogService.CollectHSSELog(projectId, item, checkHoliday.CheckTime, "21", "季节性和节假日前HSE检查", Const.BtnAdd, 1);
                            }
                        }
                    }
                    break;
            }
        }

        #endregion
    }
}