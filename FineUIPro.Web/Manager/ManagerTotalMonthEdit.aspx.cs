using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Data;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerTotalMonthEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string ManagerTotalMonthId
        {
            get
            {
                return (string)ViewState["ManagerTotalMonthId"];
            }
            set
            {
                ViewState["ManagerTotalMonthId"] = value;
            }
        }
        /// <summary>
        /// 项目主键
        /// </summary>
        public string ProjectId
        {
            get
            {
                return (string)ViewState["ProjectId"];
            }
            set
            {
                ViewState["ProjectId"] = value;
            }
        }

        private bool AppendToEnd = false;
        #endregion

        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;                
                this.ManagerTotalMonthId = Request.Params["ManagerTotalMonthId"];
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                if (!string.IsNullOrEmpty(this.ManagerTotalMonthId))
                {
                    Model.Manager_ManagerTotalMonth ManagerTotalMonth = BLL.ManagerTotalMonthService.GetManagerTotalMonthById(this.ManagerTotalMonthId);
                    if (ManagerTotalMonth != null)
                    {
                        this.ProjectId = ManagerTotalMonth.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        ///读取编号
                        this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ManagerTotalMonthId);
                        this.txtTitle.Text = ManagerTotalMonth.Title;                       
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", ManagerTotalMonth.CompileDate);
                        if (!string.IsNullOrEmpty(ManagerTotalMonth.CompileMan))
                        {
                            this.drpCompileMan.SelectedValue = ManagerTotalMonth.CompileMan;
                        }

                        this.txtMonthContent.Text = HttpUtility.HtmlDecode(ManagerTotalMonth.MonthContent);
                        this.txtMonthContent2.Text = HttpUtility.HtmlDecode(ManagerTotalMonth.MonthContent2);
                        this.txtMonthContent3.Text = HttpUtility.HtmlDecode(ManagerTotalMonth.MonthContent3);
                       // this.txtMonthContent4.Text = HttpUtility.HtmlDecode(ManagerTotalMonth.MonthContent4);
                        this.txtMonthContent5.Text = HttpUtility.HtmlDecode(ManagerTotalMonth.MonthContent5);
                        this.txtMonthContent6.Text = HttpUtility.HtmlDecode(ManagerTotalMonth.MonthContent6);
                    }                  
                }
                else
                {                
                    ////自动生成编码
                    this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectManagerTotalMonthMenuId, this.ProjectId, this.CurrUser.UnitId);                  
                    this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtTitle.Text = DateTime.Now.GetDateTimeFormats('y')[0].ToString()+ this.SimpleForm1.Title;
                    this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                    var codeTemplateRule = BLL.ProjectData_CodeTemplateRuleService.GetProjectData_CodeTemplateRuleByMenuIdProjectId(BLL.Const.ProjectManagerTotalMonthMenuId, this.ProjectId);
                    if (codeTemplateRule != null)
                    {
                        this.txtMonthContent.Text = HttpUtility.HtmlDecode(codeTemplateRule.Template);
                    }
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectManagerTotalMonthMenuId;
                this.ctlAuditFlow.DataId = this.ManagerTotalMonthId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;

                #region 本月HSE工作存在问题与处理（或拟采取对策）
                // 删除选中单元格的客户端脚本
                //string deleteScript = GetDeleteScript();
                // 新增数据初始值
                JObject defaultObj = new JObject();
                defaultObj.Add("ExistenceHiddenDanger", "");
                defaultObj.Add("CorrectiveActions", "");
                defaultObj.Add("PlanCompletedDate", "");
                defaultObj.Add("ResponsiMan", "");
                defaultObj.Add("ActualCompledDate", "");
                defaultObj.Add("Remark", "");
               // defaultObj.Add("Delete", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(Icon.Delete)));
                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);
                
                // 绑定表格
                this.BindGrid();
                #endregion
            }
        }
        #endregion

        #region 本月HSE工作存在问题与处理（或拟采取对策）数据绑定
        /// <summary>
        /// 本月HSE工作存在问题与处理（或拟采取对策）数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @" SELECT ManagerTotalMonthItemId"
                          + @" ,ManagerTotalMonthId"
                          + @" ,ExistenceHiddenDanger"
                          + @" ,CorrectiveActions"
                          + @" ,PlanCompletedDate"
                          + @" ,ResponsiMan"
                          + @" ,ActualCompledDate"
                          + @" ,Remark"
                          + @" FROM Manager_ManagerTotalMonthItem"
                          + @" WHERE ManagerTotalMonthId = @ManagerTotalMonthId";
            SqlParameter[] parameter = new SqlParameter[]       
                    {                       
                        new SqlParameter("@ManagerTotalMonthId",this.ManagerTotalMonthId),
                    };
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.DataSource = tb;
            Grid1.DataBind();
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.UserService.InitUserDropDownList(this.drpCompileMan, this.ProjectId, true);
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                ShowNotify("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Manager_ManagerTotalMonth ManagerTotalMonth = new Model.Manager_ManagerTotalMonth
            {
                ProjectId = this.ProjectId,
                Title = this.txtTitle.Text.Trim(),
                MonthContent = HttpUtility.HtmlEncode(this.txtMonthContent.Text),
                MonthContent2 = HttpUtility.HtmlEncode(this.txtMonthContent2.Text),
                MonthContent3 = HttpUtility.HtmlEncode(this.txtMonthContent3.Text),
                //ManagerTotalMonth.MonthContent4 = HttpUtility.HtmlEncode(this.txtMonthContent4.Text);
                MonthContent5 = HttpUtility.HtmlEncode(this.txtMonthContent5.Text),
                MonthContent6 = HttpUtility.HtmlEncode(this.txtMonthContent6.Text)
            };
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null)
            {
                ManagerTotalMonth.CompileMan = this.drpCompileMan.SelectedValue;
            }
            ManagerTotalMonth.CompileDate = Funs.GetNewDateTime(this.txtCompileDate.Text.Trim());
            ManagerTotalMonth.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                ManagerTotalMonth.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.ManagerTotalMonthId))
            {
                ManagerTotalMonth.ManagerTotalMonthId = this.ManagerTotalMonthId;
                BLL.ManagerTotalMonthService.UpdateManagerTotalMonth(ManagerTotalMonth);
                BLL.LogService.AddSys_Log(this.CurrUser, null, ManagerTotalMonth.ManagerTotalMonthId, BLL.Const.ProjectManagerTotalMonthMenuId, BLL.Const.BtnModify);
            }
            else
            {
                this.ManagerTotalMonthId = SQLHelper.GetNewID(typeof(Model.Manager_ManagerTotalMonth));
                ManagerTotalMonth.ManagerTotalMonthId = this.ManagerTotalMonthId;
                BLL.ManagerTotalMonthService.AddManagerTotalMonth(ManagerTotalMonth);
                BLL.LogService.AddSys_Log(this.CurrUser, null, ManagerTotalMonth.ManagerTotalMonthId, BLL.Const.ProjectManagerTotalMonthMenuId, BLL.Const.BtnAdd);
            }
            
            //保存本月HSE工作存在问题与处理（或拟采取对策）
            if (Grid1.GetModifiedData().Count > 0)
            {
                JArray teamGroupData = Grid1.GetMergedData();
                foreach (JObject teamGroupRow in teamGroupData)
                {
                    JObject values = teamGroupRow.Value<JObject>("values");
                    Model.Manager_ManagerTotalMonthItem newManagerTotalMonthItem = new Model.Manager_ManagerTotalMonthItem
                    {
                        ManagerTotalMonthItemId = SQLHelper.GetNewID(typeof(Model.Manager_ManagerTotalMonthItem)),
                        ManagerTotalMonthId = this.ManagerTotalMonthId,
                        ExistenceHiddenDanger = values.Value<string>("ExistenceHiddenDanger"),
                        CorrectiveActions = values.Value<string>("CorrectiveActions"),
                        PlanCompletedDate = Funs.GetNewDateTime(values.Value<string>("PlanCompletedDate")),
                        ResponsiMan = values.Value<string>("ResponsiMan"),
                        ActualCompledDate = Funs.GetNewDateTime(values.Value<string>("ActualCompledDate")),
                        Remark = values.Value<string>("Remark")
                    };
                    string managerTotalMonthItemId = values.Value<string>("ManagerTotalMonthItemId");
                    if (string.IsNullOrEmpty(managerTotalMonthItemId))
                    {
                        BLL.ManagerTotalMonthItemService.AddManagerTotalMonthItem(newManagerTotalMonthItem);
                    }
                    else
                    {
                        newManagerTotalMonthItem.ManagerTotalMonthItemId = managerTotalMonthItemId;
                        BLL.ManagerTotalMonthItemService.UpdateManagerTotalMonthItem(newManagerTotalMonthItem);
                    }
                }
            }

            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectManagerTotalMonthMenuId, this.ManagerTotalMonthId, (type == BLL.Const.BtnSubmit ? true : false), ManagerTotalMonth.Title, "../Manager/ManagerTotalMonthView.aspx?ManagerTotalMonthId={0}");
        }
        #endregion        

             
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var getD =Funs.DB.Manager_ManagerTotalMonthItem.FirstOrDefault(x => x.ManagerTotalMonthItemId == rowID);
                    if (getD != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getD.CorrectiveActions, getD.ManagerTotalMonthItemId, BLL.Const.ProjectManagerTotalMonthMenuId, BLL.Const.BtnDelete);
                        BLL.ManagerTotalMonthItemService.DeleteManagerTotalMonthItemByManagerTotalMonthItemId(rowID);
                    }
                }
               
                BindGrid();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
            else
            {
                Alert.ShowInTop("请先至少选中一条记录！", MessageBoxIcon.Warning);
            }
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
    }
}