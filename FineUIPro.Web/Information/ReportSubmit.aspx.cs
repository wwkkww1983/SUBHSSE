using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FineUIPro.Web.Information
{
    public partial class ReportSubmit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 报表类别
        /// </summary>
        public string Type
        {
            get
            {
                return (string)ViewState["Type"];
            }
            set
            {
                ViewState["Type"] = value;
            }
        }

        /// <summary>
        /// 报表主键Id
        /// </summary>
        public string Id
        {
            get
            {
                return (string)ViewState["Id"];
            }
            set
            {
                ViewState["Id"] = value;
            }
        }
        #endregion

        #region 加载页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                this.BindGrid();
                this.BindGrid1();
                //BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpHandleMan, this.CurrUser.LoginProjectId,this.CurrUser.UnitId, true);                 
                //BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpHandleMan2, this.CurrUser.LoginProjectId, this.CurrUser.UnitId, true); 
                this.drpHandleMan2.Enabled = false; 
                this.Type = Request.Params["Type"];
                this.Id = Request.Params["Id"];                
            }
        }
        #endregion

        #region 人员下拉框绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string unitId = this.CurrUser.UnitId;
            var thisUnit = BLL.CommonService.GetIsThisUnit();
            if (thisUnit != null)
            {
                unitId = thisUnit.UnitId;
            }

            string strSql = @"SELECT UserId,UserName,IdentityCard,UserCode,role.RoleName"
                    + @" FROM Sys_User AS users"
                    + @" LEFT JOIN Sys_Role AS role ON users.RoleId= role.RoleId"
                    + @" WHERE users.IsPost=1 AND role.IsAuditFlow=1 AND UnitId ='" + unitId + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
            {
                strSql += " AND (UserName LIKE @Name OR IdentityCard LIKE @Name OR UserCode LIKE @Name OR role.RoleName LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtUserName.Text.Trim() + "%"));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #region 查询
        /// <summary>
        /// 下拉框查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.drpHandleMan.Values = null;
            this.BindGrid();
        }
        #endregion
        #endregion

        #region 人员下拉框绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid1()
        {
            string unitId = this.CurrUser.UnitId;
            var thisUnit = BLL.CommonService.GetIsThisUnit();
            if (thisUnit != null && string.IsNullOrEmpty(unitId))
            {
                unitId = thisUnit.UnitId;
            }

            string strSql = @"SELECT UserId,UserName,IdentityCard,UserCode,role.RoleName"
                    + @" FROM Sys_User AS users"
                    + @" LEFT JOIN Sys_Role AS role ON users.RoleId= role.RoleId"
                    + @" WHERE users.IsPost=1 AND role.IsAuditFlow=1 AND UnitId ='" + unitId + "'";
            List<SqlParameter> listStr = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
            {
                strSql += " AND (UserName LIKE @Name OR IdentityCard LIKE @Name OR UserCode LIKE @Name OR role.RoleName LIKE @Name)";
                listStr.Add(new SqlParameter("@Name", "%" + this.txtUserName.Text.Trim() + "%"));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid2.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid2.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid2, tb);
            Grid2.DataSource = table;
            Grid2.DataBind();
        }

        #region 查询
        /// <summary>
        /// 下拉框查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            this.drpHandleMan2.Values = null;
            this.BindGrid1();
        }
        #endregion
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.cbNext.Checked && !this.cbEnd.Checked)
            {
                ShowNotify("请选择办理步骤！", MessageBoxIcon.Warning);
                return;
            }
            string handleMan = this.drpHandleMan.Value;
            string handleMan2 = this.drpHandleMan2.Value;
            #region 百万工时安全统计月报
            if (Type == "MillionsMonthlyReport")//百万工时安全统计月报
            {
                Model.Information_MillionsMonthlyReport report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByMillionsMonthlyReportId(Id);
                if (report != null)
                {
                    if (this.cbNext.Checked)   //提交下一步办理人
                    {
                        if (!BLL.ProjectDataFlowSetService.IsExitOperate(BLL.Const.MillionsMonthlyReportMenuId, this.Id))  //首次生成审批记录
                        {
                            ////编制人添加记录信息
                            Model.ProjectData_FlowOperate newFlow = new Model.ProjectData_FlowOperate
                            {
                                MenuId = BLL.Const.MillionsMonthlyReportMenuId,
                                DataId = this.Id,
                                OperaterId = this.CurrUser.UserId,
                                OperaterTime = DateTime.Now,
                                IsClosed = true,
                                State = BLL.Const.State_1,
                                Opinion = this.txtOpinion.Text
                            };
                            BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newFlow);
                        }
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.MillionsMonthlyReportMenuId, this.Id);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.Opinion = this.txtOpinion.Text;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                       
                        ////增加 下一步办理信息
                        Model.ProjectData_FlowOperate newdateUnFlowOperate = new Model.ProjectData_FlowOperate
                        {
                            MenuId = BLL.Const.MillionsMonthlyReportMenuId,
                            DataId = this.Id,
                            OperaterId = handleMan,
                            IsClosed = false,
                            State = BLL.Const.State_1,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_3;
                        report.HandleMan = handleMan;
                        BLL.MillionsMonthlyReportService.UpdateMillionsMonthlyReport(report);
                    }
                    else    //完成返回上报人
                    {
                        if (!BLL.ProjectDataFlowSetService.IsExitOperate(BLL.Const.MillionsMonthlyReportMenuId, this.Id))  //首次生成审批记录
                        {
                            ////编制人添加记录信息
                            Model.ProjectData_FlowOperate newFlow = new Model.ProjectData_FlowOperate
                            {
                                MenuId = BLL.Const.MillionsMonthlyReportMenuId,
                                DataId = this.Id,
                                OperaterId = this.CurrUser.UserId,
                                OperaterTime = DateTime.Now,
                                IsClosed = true,
                                State = BLL.Const.State_2,
                                Opinion = ""
                            };
                            BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newFlow);
                        }
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.MillionsMonthlyReportMenuId, this.Id);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.Opinion = this.txtOpinion.Text;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                        ////增加完成返回上报人信息
                        Model.ProjectData_FlowOperate newdateUnFlowOperate = new Model.ProjectData_FlowOperate
                        {
                            MenuId = BLL.Const.MillionsMonthlyReportMenuId,
                            DataId = this.Id,
                            OperaterId = handleMan2,
                            IsClosed = false,
                            State = BLL.Const.State_2,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_4;
                        report.HandleMan = handleMan2;
                        BLL.MillionsMonthlyReportService.UpdateMillionsMonthlyReport(report);
                    }
                }
            }
            #endregion

            #region 职工伤亡事故原因分析
            if (Type == "AccidentCauseReport")//职工伤亡事故原因分析
            {
                Model.Information_AccidentCauseReport report = BLL.AccidentCauseReportService.GetAccidentCauseReportByAccidentCauseReportId(Id);
                if (report != null)
                {
                    if (this.cbNext.Checked)   //提交下一步办理人
                    {
                        if (!BLL.ProjectDataFlowSetService.IsExitOperate(BLL.Const.AccidentCauseReportMenuId, this.Id))  //首次生成审批记录
                        {
                            ////编制人添加记录信息
                            Model.ProjectData_FlowOperate newFlow = new Model.ProjectData_FlowOperate
                            {
                                MenuId = BLL.Const.AccidentCauseReportMenuId,
                                DataId = this.Id,
                                OperaterId = this.CurrUser.UserId,
                                OperaterTime = DateTime.Now,
                                IsClosed = true,
                                State = BLL.Const.State_1,
                                Opinion = this.txtOpinion.Text
                            };
                            BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newFlow);
                        }
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.AccidentCauseReportMenuId, this.Id);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.Opinion = this.txtOpinion.Text;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                        ////增加 下一步办理信息
                        Model.ProjectData_FlowOperate newdateUnFlowOperate = new Model.ProjectData_FlowOperate
                        {
                            MenuId = BLL.Const.AccidentCauseReportMenuId,
                            DataId = this.Id,
                            OperaterId = handleMan,
                            IsClosed = false,
                            State = BLL.Const.State_1,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_3;
                        report.HandleMan = handleMan;
                        BLL.AccidentCauseReportService.UpdateAccidentCauseReport(report);
                    }
                    else    //完成返回上报人
                    {
                        if (!BLL.ProjectDataFlowSetService.IsExitOperate(BLL.Const.AccidentCauseReportMenuId, this.Id))  //首次生成审批记录
                        {
                            ////编制人添加记录信息
                            Model.ProjectData_FlowOperate newFlow = new Model.ProjectData_FlowOperate
                            {
                                MenuId = BLL.Const.AccidentCauseReportMenuId,
                                DataId = this.Id,
                                OperaterId = this.CurrUser.UserId,
                                OperaterTime = DateTime.Now,
                                IsClosed = true,
                                State = BLL.Const.State_2,
                                Opinion = ""
                            };
                            BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newFlow);
                        }
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.AccidentCauseReportMenuId, this.Id);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.Opinion = this.txtOpinion.Text;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                        ////增加完成返回上报人信息
                        Model.ProjectData_FlowOperate newdateUnFlowOperate = new Model.ProjectData_FlowOperate
                        {
                            MenuId = BLL.Const.AccidentCauseReportMenuId,
                            DataId = this.Id,
                            OperaterId = handleMan2,
                            IsClosed = false,
                            State = BLL.Const.State_2,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_4;
                        report.HandleMan = handleMan2;
                        BLL.AccidentCauseReportService.UpdateAccidentCauseReport(report);
                    }
                }
            }
            #endregion

            #region 安全生产数据季报
            if (Type == "SafetyQuarterlyReport")//安全生产数据季报
            {
                Model.Information_SafetyQuarterlyReport report = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportById(Id);
                if (report != null)
                {
                    if (this.cbNext.Checked)   //提交下一步办理人
                    {
                        if (!BLL.ProjectDataFlowSetService.IsExitOperate(BLL.Const.SafetyQuarterlyReportMenuId, this.Id))  //首次生成审批记录
                        {
                            ////编制人添加记录信息
                            Model.ProjectData_FlowOperate newFlow = new Model.ProjectData_FlowOperate
                            {
                                MenuId = BLL.Const.SafetyQuarterlyReportMenuId,
                                DataId = this.Id,
                                OperaterId = this.CurrUser.UserId,
                                OperaterTime = DateTime.Now,
                                IsClosed = true,
                                State = BLL.Const.State_1,
                                Opinion = this.txtOpinion.Text
                            };
                            BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newFlow);
                        }
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.SafetyQuarterlyReportMenuId, this.Id);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.Opinion = this.txtOpinion.Text;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                        ////增加 下一步办理信息
                        Model.ProjectData_FlowOperate newdateUnFlowOperate = new Model.ProjectData_FlowOperate
                        {
                            MenuId = BLL.Const.SafetyQuarterlyReportMenuId,
                            DataId = this.Id,
                            OperaterId = handleMan,
                            IsClosed = false,
                            State = BLL.Const.State_1,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_3;
                        report.HandleMan = handleMan;
                        BLL.SafetyQuarterlyReportService.UpdateSafetyQuarterlyReport(report);
                    }
                    else    //完成返回上报人
                    {
                        if (!BLL.ProjectDataFlowSetService.IsExitOperate(BLL.Const.SafetyQuarterlyReportMenuId, this.Id))  //首次生成审批记录
                        {
                            ////编制人添加记录信息
                            Model.ProjectData_FlowOperate newFlow = new Model.ProjectData_FlowOperate
                            {
                                MenuId = BLL.Const.SafetyQuarterlyReportMenuId,
                                DataId = this.Id,
                                OperaterId = this.CurrUser.UserId,
                                OperaterTime = DateTime.Now,
                                IsClosed = true,
                                State = BLL.Const.State_2,
                                Opinion = ""
                            };
                            BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newFlow);
                        }
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.SafetyQuarterlyReportMenuId, this.Id);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.Opinion = this.txtOpinion.Text;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                        ////增加完成返回上报人信息
                        Model.ProjectData_FlowOperate newdateUnFlowOperate = new Model.ProjectData_FlowOperate
                        {
                            MenuId = BLL.Const.SafetyQuarterlyReportMenuId,
                            DataId = this.Id,
                            OperaterId = handleMan2,
                            IsClosed = false,
                            State = BLL.Const.State_2,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_4;
                        report.HandleMan = handleMan2;
                        BLL.SafetyQuarterlyReportService.UpdateSafetyQuarterlyReport(report);
                    }
                }
            }
            #endregion

            #region 应急演练开展情况季报表
            if (Type == "DrillConductedQuarterlyReport")//应急演练开展情况季报表
            {
                Model.Information_DrillConductedQuarterlyReport report = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportById(Id);
                if (report != null)
                {
                    if (this.cbNext.Checked)   //提交下一步办理人
                    {
                        if (!BLL.ProjectDataFlowSetService.IsExitOperate(BLL.Const.DrillConductedQuarterlyReportMenuId, this.Id))  //首次生成审批记录
                        {
                            ////编制人添加记录信息
                            Model.ProjectData_FlowOperate newFlow = new Model.ProjectData_FlowOperate
                            {
                                MenuId = BLL.Const.DrillConductedQuarterlyReportMenuId,
                                DataId = this.Id,
                                OperaterId = this.CurrUser.UserId,
                                OperaterTime = DateTime.Now,
                                IsClosed = true,
                                State = BLL.Const.State_1,
                                Opinion = this.txtOpinion.Text
                            };
                            BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newFlow);
                        }
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.DrillConductedQuarterlyReportMenuId, this.Id);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.Opinion = this.txtOpinion.Text;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                        ////增加 下一步办理信息
                        Model.ProjectData_FlowOperate newdateUnFlowOperate = new Model.ProjectData_FlowOperate
                        {
                            MenuId = BLL.Const.DrillConductedQuarterlyReportMenuId,
                            DataId = this.Id,
                            OperaterId = handleMan,
                            IsClosed = false,
                            State = BLL.Const.State_1,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_3;
                        report.HandleMan = handleMan;
                        BLL.DrillConductedQuarterlyReportService.UpdateDrillConductedQuarterlyReport(report);
                    }
                    else    //完成返回上报人
                    {
                        if (!BLL.ProjectDataFlowSetService.IsExitOperate(BLL.Const.DrillConductedQuarterlyReportMenuId, this.Id))  //首次生成审批记录
                        {
                            ////编制人添加记录信息
                            Model.ProjectData_FlowOperate newFlow = new Model.ProjectData_FlowOperate
                            {
                                MenuId = BLL.Const.DrillConductedQuarterlyReportMenuId,
                                DataId = this.Id,
                                OperaterId = this.CurrUser.UserId,
                                OperaterTime = DateTime.Now,
                                IsClosed = true,
                                State = BLL.Const.State_2,
                                Opinion = ""
                            };
                            BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newFlow);
                        }
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.DrillConductedQuarterlyReportMenuId, this.Id);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.Opinion = this.txtOpinion.Text;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                        ////增加完成返回上报人信息
                        Model.ProjectData_FlowOperate newdateUnFlowOperate = new Model.ProjectData_FlowOperate
                        {
                            MenuId = BLL.Const.DrillConductedQuarterlyReportMenuId,
                            DataId = this.Id,
                            OperaterId = handleMan2,
                            IsClosed = false,
                            State = BLL.Const.State_2,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_4;
                        report.HandleMan = handleMan2;
                        BLL.DrillConductedQuarterlyReportService.UpdateDrillConductedQuarterlyReport(report);
                    }
                }
            }
            #endregion

            #region 应急演练工作计划半年报
            if (Type == "DrillPlanHalfYearReport")//应急演练工作计划半年报
            {
                Model.Information_DrillPlanHalfYearReport report = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportById(this.Id);
                if (report != null)
                {
                    if (this.cbNext.Checked)   //提交下一步办理人
                    {
                        if (!BLL.ProjectDataFlowSetService.IsExitOperate(BLL.Const.DrillPlanHalfYearReportMenuId, this.Id))  //首次生成审批记录
                        {
                            ////编制人添加记录信息
                            Model.ProjectData_FlowOperate newFlow = new Model.ProjectData_FlowOperate
                            {
                                MenuId = BLL.Const.DrillPlanHalfYearReportMenuId,
                                DataId = this.Id,
                                OperaterId = this.CurrUser.UserId,
                                OperaterTime = DateTime.Now,
                                IsClosed = true,
                                State = BLL.Const.State_1,
                                Opinion = this.txtOpinion.Text
                            };
                            BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newFlow);
                        }
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.DrillPlanHalfYearReportMenuId, this.Id);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.Opinion = this.txtOpinion.Text;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                        ////增加 下一步办理信息
                        Model.ProjectData_FlowOperate newdateUnFlowOperate = new Model.ProjectData_FlowOperate
                        {
                            MenuId = BLL.Const.DrillPlanHalfYearReportMenuId,
                            DataId = this.Id,
                            OperaterId = handleMan,
                            IsClosed = false,
                            State = BLL.Const.State_1,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_3;
                        report.HandleMan = handleMan;
                        BLL.DrillPlanHalfYearReportService.UpdateDrillPlanHalfYearReport(report);
                    }
                    else    //完成返回上报人
                    {
                        if (!BLL.ProjectDataFlowSetService.IsExitOperate(BLL.Const.DrillPlanHalfYearReportMenuId, this.Id))  //首次生成审批记录
                        {
                            ////编制人添加记录信息
                            Model.ProjectData_FlowOperate newFlow = new Model.ProjectData_FlowOperate
                            {
                                MenuId = BLL.Const.DrillPlanHalfYearReportMenuId,
                                DataId = this.Id,
                                OperaterId = this.CurrUser.UserId,
                                OperaterTime = DateTime.Now,
                                IsClosed = true,
                                State = BLL.Const.State_2,
                                Opinion = ""
                            };
                            BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newFlow);
                        }
                        ////更新 当前人要处理的意见
                        Model.ProjectData_FlowOperate updateUnFlowOperate = BLL.ProjectDataFlowSetService.GetFlowOperateOpinion(BLL.Const.DrillPlanHalfYearReportMenuId, this.Id);
                        if (updateUnFlowOperate != null)
                        {
                            updateUnFlowOperate.OperaterTime = System.DateTime.Now;
                            updateUnFlowOperate.Opinion = this.txtOpinion.Text;
                            updateUnFlowOperate.IsClosed = true;
                            BLL.ProjectDataFlowSetService.UpdateFlowOperateOpinion(updateUnFlowOperate);
                        }
                        ////增加完成返回上报人信息
                        Model.ProjectData_FlowOperate newdateUnFlowOperate = new Model.ProjectData_FlowOperate
                        {
                            MenuId = BLL.Const.DrillPlanHalfYearReportMenuId,
                            DataId = this.Id,
                            OperaterId = handleMan2,
                            IsClosed = false,
                            State = BLL.Const.State_2,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_4;
                        report.HandleMan = handleMan2;
                        BLL.DrillPlanHalfYearReportService.UpdateDrillPlanHalfYearReport(report);
                    }
                }
            }
            #endregion

            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 勾选下一步
        protected void cbNext_CheckedChanged(object sender, CheckedEventArgs e)
        {
            if (this.cbNext.Checked)
            {
                this.drpHandleMan.Enabled = true;
                this.cbEnd.Checked = false;
                this.drpHandleMan2.Enabled = false;
            }
            else
            {
                this.drpHandleMan.Enabled = false;
            }
        }
        #endregion

        #region 勾选返回上报人
        protected void cbEnd_CheckedChanged(object sender, CheckedEventArgs e)
        {
            if (this.cbEnd.Checked)
            {
                this.drpHandleMan2.Enabled = true;
                this.cbNext.Checked = false;
                this.drpHandleMan.Enabled = false;

                Model.ProjectData_FlowOperate flowOperate = BLL.ProjectDataFlowSetService.getCompileFlowOperate(this.Id);
                if (flowOperate != null)
                {
                    this.drpHandleMan2.Value = flowOperate.OperaterId;
                }
            }
            else
            {
                this.drpHandleMan2.Enabled = false;
            }
        }
        #endregion
    }
}