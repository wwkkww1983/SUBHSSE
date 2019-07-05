using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

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
                LoadData();               
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpHandleMan, this.CurrUser.LoginProjectId,this.CurrUser.UnitId, true);                 
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpHandleMan2, this.CurrUser.LoginProjectId, this.CurrUser.UnitId, true); 
                this.drpHandleMan2.Enabled = false; 
                this.Type = Request.Params["Type"];
                this.Id = Request.Params["Id"];
                Model.ProjectData_FlowOperate flowOperate = BLL.ProjectDataFlowSetService.getCompileFlowOperate(this.Id);
                if (flowOperate != null)
                {
                    this.drpHandleMan2.SelectedValue = flowOperate.OperaterId;
                }
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }
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
                            OperaterId = this.drpHandleMan.SelectedValue,
                            IsClosed = false,
                            State = BLL.Const.State_1,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_3;
                        report.HandleMan = drpHandleMan.SelectedValue;
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
                            OperaterId = this.drpHandleMan2.SelectedValue,
                            IsClosed = false,
                            State = BLL.Const.State_2,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_4;
                        report.HandleMan = drpHandleMan2.SelectedValue;
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
                            OperaterId = this.drpHandleMan.SelectedValue,
                            IsClosed = false,
                            State = BLL.Const.State_1,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_3;
                        report.HandleMan = drpHandleMan.SelectedValue;
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
                            OperaterId = this.drpHandleMan2.SelectedValue,
                            IsClosed = false,
                            State = BLL.Const.State_2,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_4;
                        report.HandleMan = drpHandleMan2.SelectedValue;
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
                            OperaterId = this.drpHandleMan.SelectedValue,
                            IsClosed = false,
                            State = BLL.Const.State_1,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_3;
                        report.HandleMan = drpHandleMan.SelectedValue;
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
                            OperaterId = this.drpHandleMan2.SelectedValue,
                            IsClosed = false,
                            State = BLL.Const.State_2,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_4;
                        report.HandleMan = drpHandleMan2.SelectedValue;
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
                            OperaterId = this.drpHandleMan.SelectedValue,
                            IsClosed = false,
                            State = BLL.Const.State_1,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_3;
                        report.HandleMan = drpHandleMan.SelectedValue;
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
                            OperaterId = this.drpHandleMan2.SelectedValue,
                            IsClosed = false,
                            State = BLL.Const.State_2,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_4;
                        report.HandleMan = drpHandleMan2.SelectedValue;
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
                            OperaterId = this.drpHandleMan.SelectedValue,
                            IsClosed = false,
                            State = BLL.Const.State_1,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_3;
                        report.HandleMan = drpHandleMan.SelectedValue;
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
                            OperaterId = this.drpHandleMan2.SelectedValue,
                            IsClosed = false,
                            State = BLL.Const.State_2,
                            Opinion = ""
                        };
                        BLL.ProjectDataFlowSetService.AddProjectData_FlowOperate(newdateUnFlowOperate);
                        report.HandleState = BLL.Const.HandleState_4;
                        report.HandleMan = drpHandleMan2.SelectedValue;
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
            }
            else
            {
                this.drpHandleMan2.Enabled = false;
            }
        }
        #endregion
    }
}