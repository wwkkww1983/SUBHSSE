using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Supervise
{
    public partial class SuperviseCheckReportEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string SuperviseCheckReportId
        {
            get
            {
                return (string)ViewState["SuperviseCheckReportId"];
            }
            set
            {
                ViewState["SuperviseCheckReportId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.View_Supervise_SuperviseCheckReportItem> superviseCheckReportItems = new List<Model.View_Supervise_SuperviseCheckReportItem>();
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
                hdItemId.Text = string.Empty;
                hdAttachUrl.Text = string.Empty;
                hdId.Text = string.Empty;
                btnClose.OnClientClick = ActiveWindow.GetHideReference();

                superviseCheckReportItems.Clear();
                ////权限按钮方法
                this.GetButtonPower();
                Funs.FineUIPleaseSelect(this.ddlProjectId, "-请选择项目-");

                //加载单位下拉选项
                this.ddlUnitId.DataTextField = "UnitName";
                this.ddlUnitId.DataValueField = "UnitId";
                this.ddlUnitId.DataSource = BLL.UnitService.GetThisUnitDropDownList();
                this.ddlUnitId.DataBind();
               // Funs.FineUIPleaseSelect(this.ddlUnitId, "-请选择单位-");

                this.ddlProjectId.DataTextField = "ProjectName";
                this.ddlProjectId.DataValueField = "ProjectId";
                this.ddlProjectId.DataSource = BLL.ProjectService.GetProjectWorkList();
                ddlProjectId.DataBind();
                Funs.FineUIPleaseSelect(this.ddlProjectId, "-请选择项目-");

                this.SuperviseCheckReportId = Request.Params["SuperviseCheckReportId"];
                var superviseCheckReport = BLL.SuperviseCheckReportService.GetSuperviseCheckReportById(this.SuperviseCheckReportId);
                if (superviseCheckReport != null)
                {
                    this.txtSuperviseCheckReportCode.Text = superviseCheckReport.SuperviseCheckReportCode;
                    if (superviseCheckReport.CheckDate != null)
                    {
                        this.dpkCheckDate.Text = string.Format("{0:yyyy-MM-dd}", superviseCheckReport.CheckDate);
                    }
                    if (!string.IsNullOrEmpty(superviseCheckReport.UnitId))
                    {
                        this.ddlUnitId.SelectedValue = superviseCheckReport.UnitId;
                        this.ddlProjectId.Items.Clear();
                        this.ddlProjectId.DataTextField = "ProjectName";
                        this.ddlProjectId.DataValueField = "ProjectId";
                        this.ddlProjectId.DataSource = BLL.ProjectService.GetProjectWorkList();
                        ddlProjectId.DataBind();
                        Funs.FineUIPleaseSelect(this.ddlProjectId);
                        if (!string.IsNullOrEmpty(superviseCheckReport.ProjectId))
                        {
                            this.ddlProjectId.SelectedValue = superviseCheckReport.ProjectId;
                        }
                    }
                    this.txtCheckTeam.Text = superviseCheckReport.CheckTeam;
                    superviseCheckReportItems = (from x in Funs.DB.View_Supervise_SuperviseCheckReportItem where x.SuperviseCheckReportId == this.SuperviseCheckReportId orderby x.RectifyCode select x).ToList();


                    var report = BLL.SuperviseCheckReportService.GetSuperviseCheckReportById(this.SuperviseCheckReportId);
                    if (report != null && report.IsIssued == "1")    //已下发
                    {
                        this.btnSave.Hidden = true;
                        this.btnSelect.Hidden = true;
                        Grid1.Columns[10].Hidden = true;
                    }
                }
                else
                {
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(this.CurrUser.UnitId);
                    string unitName = unit != null ? (unit.UnitName + ":") : "";
                    this.txtCheckTeam.Text = unitName + this.CurrUser.UserName;
                    this.dpkCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);                   
                }
                Grid1.DataSource = superviseCheckReportItems;
                Grid1.DataBind();
                GetCheckItem();
            }
        }

        private void GetCheckItem()
        {
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                var item = superviseCheckReportItems.FirstOrDefault(x => x.SuperviseCheckReportItemId == Grid1.Rows[i].Values[7].ToString());
                if (item != null)
                {
                    if (item.IsSelected == true)
                    {
                        Grid1.Rows[i].Values[6] = "True";
                    }
                }
            }
        }
        #endregion

        #region Grid行点击事件
        /// <summary>
        /// Grid行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "IsSelected")
            {
                CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                if (checkField.GetCheckedState(e.RowIndex))
                {
                    hdId.Text = rowID;
                    PageContext.RegisterStartupScript(Window1.GetSaveStateReference(hdAttachUrl.ClientID)
              + Window1.GetShowReference("ShowFileUpload.aspx"));
                }
            }
            if (e.CommandName == "Attach")
            {
                var item = superviseCheckReportItems.FirstOrDefault(x => x.SuperviseCheckReportItemId == Grid1.Rows[e.RowIndex].Values[7].ToString());
                if (item != null)
                {
                    PageBase.ShowFileEvent(item.AttachUrl);
                }
            }

            if (e.CommandName == "Delete")
            {
                jerqueSaveList();
                superviseCheckReportItems = (from x in superviseCheckReportItems where x.SuperviseCheckReportItemId != rowID select x).ToList();
                Grid1.DataSource = superviseCheckReportItems;
                Grid1.DataBind();
                GetCheckItem();

                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 选择按钮
        /// <summary>
        /// 选择按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
             jerqueSaveList();
             string lists = string.Empty;
             foreach (var item in superviseCheckReportItems)
             {
                 lists += item.RectifyItemId + ",";
             }
             if (!string.IsNullOrEmpty(lists))
             {
                 lists = lists.Substring(0, lists.LastIndexOf(","));
             }

            string window = String.Format("ShowRectifyItem.aspx?lists={0}", lists, "选择 - ");             
            PageContext.RegisterStartupScript(Window2.GetSaveStateReference(hdItemId.ClientID)
              + Window2.GetShowReference(window));
        }
        #endregion

        #region 关闭窗口
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdItemId.Text))
            {
                if (superviseCheckReportItems.Count == 0)
                {
                    List<string> itemIds = hdItemId.Text.Split(',').ToList();
                    var rectifyItem = from x in Funs.DB.View_Technique_RectifyItem where itemIds.Contains(x.RectifyItemId) orderby x.RectifyCode select x;
                    foreach (var item in rectifyItem)
                    {
                        Model.View_Supervise_SuperviseCheckReportItem superviseCheckReportItem = new Model.View_Supervise_SuperviseCheckReportItem
                        {
                            SuperviseCheckReportItemId = SQLHelper.GetNewID(typeof(Model.Supervise_SuperviseCheckReportItem)),
                            SuperviseCheckReportId = this.SuperviseCheckReportId,
                            RectifyItemId = item.RectifyItemId,
                            HazardSourcePoint = item.HazardSourcePoint,
                            RiskAnalysis = item.RiskAnalysis,
                            RiskPrevention = item.RiskPrevention,
                            SimilarRisk = item.SimilarRisk,
                            RectifyName = item.RectifyName,
                            RectifyCode = item.RectifyCode,
                            IsSelected = false
                        };
                        superviseCheckReportItems.Add(superviseCheckReportItem);
                    }
                }
                else
                {
                    List<string> itemIds = hdItemId.Text.Split(',').ToList();
                    List<string> superviseCheckReportItemsRectifyItemIds = superviseCheckReportItems.Select(x => x.RectifyItemId).ToList();
                    foreach (var item in itemIds)
                    {
                        if (!superviseCheckReportItemsRectifyItemIds.Contains(item))
                        {
                            var rectifyItem = (from x in Funs.DB.View_Technique_RectifyItem where x.RectifyItemId == item orderby x.RectifyCode select x).FirstOrDefault();
                            if (rectifyItem != null)
                            {
                                Model.View_Supervise_SuperviseCheckReportItem superviseCheckReportItem = new Model.View_Supervise_SuperviseCheckReportItem
                                {
                                    SuperviseCheckReportItemId = SQLHelper.GetNewID(typeof(Model.Supervise_SuperviseCheckReportItem)),
                                    SuperviseCheckReportId = this.SuperviseCheckReportId,
                                    RectifyItemId = rectifyItem.RectifyItemId,
                                    HazardSourcePoint = rectifyItem.HazardSourcePoint,
                                    RiskAnalysis = rectifyItem.RiskAnalysis,
                                    RiskPrevention = rectifyItem.RiskPrevention,
                                    SimilarRisk = rectifyItem.SimilarRisk,
                                    RectifyName = rectifyItem.RectifyName,
                                    RectifyCode = rectifyItem.RectifyCode,
                                    IsSelected = false
                                };
                                superviseCheckReportItems.Add(superviseCheckReportItem);
                            }
                        }
                    }
                    superviseCheckReportItems = (from x in superviseCheckReportItems
                                                join y in Funs.DB.View_Technique_RectifyItem
                                                on x.RectifyItemId equals y.RectifyItemId
                                                orderby y.RectifyCode
                                                select x).ToList();
                }
                Grid1.DataSource = superviseCheckReportItems;
                Grid1.DataBind();
                hdItemId.Text = string.Empty;
            }
        }
        #endregion

        #region 保存按钮
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Model.Supervise_SuperviseCheckReport superviseCheckReport = new Model.Supervise_SuperviseCheckReport
            {
                SuperviseCheckReportCode = this.txtSuperviseCheckReportCode.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.dpkCheckDate.Text.Trim()))
            {
                superviseCheckReport.CheckDate = Convert.ToDateTime(this.dpkCheckDate.Text.Trim());
            }
            if (this.ddlProjectId.SelectedValue != BLL.Const._Null)
            {
                superviseCheckReport.ProjectId = this.ddlProjectId.SelectedValue;
            }
            if (this.ddlUnitId.SelectedValue != BLL.Const._Null)
            {
                superviseCheckReport.UnitId = this.ddlUnitId.SelectedValue;
            }
            superviseCheckReport.CheckTeam = this.txtCheckTeam.Text.Trim();
            if (string.IsNullOrEmpty(this.SuperviseCheckReportId))
            {
                superviseCheckReport.SuperviseCheckReportId = SQLHelper.GetNewID(typeof(Model.Supervise_SuperviseCheckReport));
                BLL.SuperviseCheckReportService.AddSuperviseCheckReport(superviseCheckReport);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加安全监督检查报告");
            }
            else
            {
                var report = BLL.SuperviseCheckReportService.GetSuperviseCheckReportById(this.SuperviseCheckReportId);
                if (report.IsIssued == "1")    //已下发
                {
                    ShowNotify("已下发检查整改，无法修改！", MessageBoxIcon.Warning);
                    return;
                }
                superviseCheckReport.SuperviseCheckReportId = this.SuperviseCheckReportId;
                BLL.SuperviseCheckReportService.UpdateSuperviseCheckReport(superviseCheckReport);
                BLL.SuperviseCheckReportItemService.DeleteSuperviseCheckReportItemBySuperviseCheckReportId(this.SuperviseCheckReportId);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加安全监督检查报告");
            }
            jerqueSaveList();
            foreach (var item in superviseCheckReportItems)
            {
                Model.Supervise_SuperviseCheckReportItem superviseCheckReportItem = new Model.Supervise_SuperviseCheckReportItem
                {
                    SuperviseCheckReportItemId = item.SuperviseCheckReportItemId,
                    SuperviseCheckReportId = superviseCheckReport.SuperviseCheckReportId,
                    RectifyItemId = item.RectifyItemId,
                    IsSelected = item.IsSelected,
                    AttachUrl = item.AttachUrl
                };
                BLL.SuperviseCheckReportItemService.AddSuperviseCheckReportItem(superviseCheckReportItem);
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            jerqueSaveList();
            if (!string.IsNullOrEmpty(hdAttachUrl.Text))
            {
                var item = superviseCheckReportItems.FirstOrDefault(x => x.SuperviseCheckReportItemId == hdId.Text);
                item.AttachUrl = hdAttachUrl.Text;
                item.AttachUrlName = hdAttachUrl.Text.Substring((hdAttachUrl.Text.LastIndexOf("~") + 1), (hdAttachUrl.Text.Length - hdAttachUrl.Text.LastIndexOf("~") - 1));
            }
            Grid1.DataSource = superviseCheckReportItems;
            Grid1.DataBind();
            GetCheckItem();
            hdId.Text = string.Empty;
            hdAttachUrl.Text = string.Empty;
        }
        #endregion

        #region 保存集合
        /// <summary>
        /// 保存集合
        /// </summary>
        private void jerqueSaveList()
        {
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                if (Grid1.Rows[i].Values[7] != null && Grid1.Rows[i].Values[7].ToString() != "")
                {
                    var item = superviseCheckReportItems.FirstOrDefault(e => e.SuperviseCheckReportItemId == Grid1.Rows[i].Values[7].ToString());
                    //item.IsSelected = ((System.Web.UI.WebControls.CheckBox)(Grid1.Rows[i].FindControl("ckbIsSelected"))).Checked;
                    CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                    item.IsSelected = checkField.GetCheckedState(i);
                    item.AttachUrl = Grid1.Rows[i].Values[8].ToString();
                }
            }
        }
        #endregion

        #region 验证检查编号是否存在
        /// <summary>
        /// 验证检查编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var standard = Funs.DB.Supervise_SuperviseCheckReport.FirstOrDefault(x => x.SuperviseCheckReportCode == this.txtSuperviseCheckReportCode.Text.Trim() && (x.SuperviseCheckReportId != this.SuperviseCheckReportId || (this.SuperviseCheckReportId == null && x.SuperviseCheckReportId != null)));
            if (standard != null)
            {
                ShowNotify("输入的检查编号已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SuperviseCheckReportMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion
    }
}