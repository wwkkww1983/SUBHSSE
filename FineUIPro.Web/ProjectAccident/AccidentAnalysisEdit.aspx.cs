using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.ProjectAccident
{
    public partial class AccidentAnalysisEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string AccidentAnalysisId
        {
            get
            {
                return (string)ViewState["AccidentAnalysisId"];
            }
            set
            {
                ViewState["AccidentAnalysisId"] = value;
            }
        }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string FullAttachUrl
        {
            get
            {
                return (string)ViewState["FullAttachUrl"];
            }
            set
            {
                ViewState["FullAttachUrl"] = value;
            }
        }

        private static List<Model.ProjectAccident_AccidentAnalysisItem> items = new List<Model.ProjectAccident_AccidentAnalysisItem>();

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                ////权限按钮方法
                this.GetButtonPower();
                items.Clear();
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                BLL.ProjectService.InitProjectDropDownList(ddlProjectId, true);

                this.AccidentAnalysisId = Request.Params["AccidentAnalysisId"];
                var accidentAnalysis = BLL.AccidentAnalysisService.GetAccidentAnalysisById(this.AccidentAnalysisId);
                if (accidentAnalysis != null)
                {
                    this.txtCompileMan.Text = accidentAnalysis.CompileMan;
                    this.txtRemarks.Text = accidentAnalysis.Remarks;
                    if (accidentAnalysis.CompileDate != null)
                    {
                        this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", accidentAnalysis.CompileDate);
                    }
                
                    if (!string.IsNullOrEmpty(accidentAnalysis.ProjectId))
                    {
                        this.ddlProjectId.SelectedValue = accidentAnalysis.ProjectId;
                    }
                    items = BLL.AccidentAnalysisItemService.GetItemsNoSum(AccidentAnalysisId);
                    this.Grid1.DataSource = items;
                    this.Grid1.DataBind();
                }
                else
                {
                    var accidentTypes = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_0012);
                    foreach (var a in accidentTypes)
                    {
                        if (a.ConstText != "总计")
                        {
                            Model.ProjectAccident_AccidentAnalysisItem item = new Model.ProjectAccident_AccidentAnalysisItem
                            {
                                AccidentAnalysisItemId = SQLHelper.GetNewID(typeof(Model.ProjectAccident_AccidentAnalysisItem)),
                                AccidentType = a.ConstText
                            };
                            items.Add(item);
                        }
                    }
                    this.Grid1.DataSource = items;
                    this.Grid1.DataBind();
                    this.dpkCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
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
            Model.ProjectAccident_AccidentAnalysis accidentAnalysis = new Model.ProjectAccident_AccidentAnalysis();

            if (this.ddlProjectId.SelectedValue == BLL.Const._Null)
            {
                ShowNotify("请选择项目");
                return;
            }
            accidentAnalysis.Remarks = this.txtRemarks.Text.Trim();
            accidentAnalysis.CompileMan = this.txtCompileMan.Text.Trim();
            if (!string.IsNullOrEmpty(this.dpkCompileDate.Text.Trim()))
            {
                accidentAnalysis.CompileDate = Convert.ToDateTime(this.dpkCompileDate.Text.Trim());
            }
            if (this.ddlProjectId.SelectedValue != BLL.Const._Null)
            {
                accidentAnalysis.ProjectId = this.ddlProjectId.SelectedValue;
            }
            if (string.IsNullOrEmpty(this.AccidentAnalysisId))
            {
                accidentAnalysis.AccidentAnalysisId = SQLHelper.GetNewID(typeof(Model.ProjectAccident_AccidentAnalysis));
                BLL.AccidentAnalysisService.AddAccidentAnalysis(accidentAnalysis);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加事故处理");
            }
            else
            {
                
                accidentAnalysis.AccidentAnalysisId = this.AccidentAnalysisId;
                BLL.AccidentAnalysisService.UpdateAccidentAnalysis(accidentAnalysis);
                BLL.AccidentAnalysisItemService.DeleteAccidentAnalysisItemByAccidentAnalysisId(AccidentAnalysisId);
            }
            AddItems(accidentAnalysis.AccidentAnalysisId);
            // 2. 关闭本窗体，然后刷新父窗体
            // PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
            // 2. 关闭本窗体，然后回发父窗体
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(wedId) + ActiveWindow.GetHideReference());

        }

        private void AddItems(string accidentCauseReportId)
        {
            int Death = 0, Injuries = 0, MinorInjuries = 0;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                if (values["AccidentType"].ToString() != "")
                {
                    Model.ProjectAccident_AccidentAnalysisItem item = items.FirstOrDefault(x => x.AccidentType == values["AccidentType"].ToString());
                    if (values["Death"].ToString() != "")
                    {
                        item.Death = values.Value<int>("Death");
                      
                    }
                    if (values["Injuries"].ToString() != "")
                    {
                        item.Injuries = values.Value<int>("Injuries");
                      
                    }
                    if (values["MinorInjuries"].ToString() != "")
                    {
                        item.MinorInjuries = values.Value<int>("MinorInjuries");
                      
                    }
                 
                }
            }
            Model.ProjectAccident_AccidentAnalysisItem totalItem = new Model.ProjectAccident_AccidentAnalysisItem
            {
                AccidentAnalysisItemId = SQLHelper.GetNewID(typeof(Model.ProjectAccident_AccidentAnalysisItem)),


                Death = Death,
                Injuries = Injuries,
                MinorInjuries = MinorInjuries
            };

            items.Add(totalItem);
            foreach (var item in items)
            {
                item.AccidentAnalysisId = accidentCauseReportId;
                BLL.AccidentAnalysisItemService.AddAccidentAnalysisItem(item);
            }
        }

        #region 获取按钮权限
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ServerAccidentAnalysisMenuId);
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