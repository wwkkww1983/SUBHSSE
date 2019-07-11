using System;
using System.Linq;
using BLL;
using Model;

namespace FineUIPro.Web.ManagementReport
{
    public partial class ServerSafeReportItemSave : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 安全文件上报主键
        /// </summary>
        public string SafeReportId
        {
            get
            {
                return (string)ViewState["SafeReportId"];
            }
            set
            {
                ViewState["SafeReportId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.SafeReportId = Request.QueryString["SafeReportId"];
                if (!String.IsNullOrEmpty(this.SafeReportId))
                {
                    var safeReport = BLL.SafeReportService.GetSafeReportBySafeReportId(this.SafeReportId);
                    if (safeReport != null)
                    {
                        this.txtSafeReportName.Text = safeReport.SafeReportName;
                        var safeProjectList = (from x in Funs.DB.Manager_SafeReportItem
                                               where x.SafeReportId == this.SafeReportId
                                               select x.ProjectId).ToList();

                        var projects =  (from x in Funs.DB.Base_Project
                                                   where x.ProjectState == null || x.ProjectState == BLL.Const.ProjectState_1
                                                   orderby x.ProjectCode descending
                                                   select x).ToList();
                        if (safeProjectList.Count() > 0)
                        {
                            projects = projects.Where(x => !safeProjectList.Contains(x.ProjectId)).ToList();
                        }

                        this.drpProjects.DataValueField = "ProjectId";
                        this.drpProjects.DataTextField = "ProjectName";
                        this.drpProjects.DataSource = projects;
                        this.drpProjects.DataBind();
                        Funs.FineUIPleaseSelect(this.drpProjects);


                        var safeUnitList = (from x in Funs.DB.Manager_SafeReportUnitItem
                                               where x.SafeReportId == this.SafeReportId
                                               select x.UnitId).ToList();

                        var units = BLL.UnitService.GetBranchUnitList();
                        if (safeUnitList.Count() > 0)
                        {
                            units = units.Where(x => !safeUnitList.Contains(x.UnitId)).ToList();
                        }

                        this.drpUnits.DataValueField = "UnitId";
                        this.drpUnits.DataTextField = "UnitName";
                        this.drpUnits.DataSource = units;
                        this.drpUnits.DataBind();
                        Funs.FineUIPleaseSelect(this.drpUnits);
                    }
                }
            }
        }
        #endregion

        #region 保存方法
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //if (this.drpProjects.SelectedValueArray.Count() > 0 && (this.drpProjects.SelectedItemArray.Count() != 1 || this.drpProjects.SelectedValue != BLL.Const._Null))
            //{
            //}
            //else
            //{
            //    Alert.ShowInTop("请选择要添加的项目！", MessageBoxIcon.Warning);
            //}
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="type"></param>
        private void SaveData()
        {
            foreach (var item in this.drpProjects.SelectedValueArray)
            {
                if(item != BLL.Const._Null)
                {
                    Model.Manager_SafeReportItem newSafeReportItem = new Model.Manager_SafeReportItem
                    {
                        SafeReportItemId = SQLHelper.GetNewID(typeof(Model.Manager_SafeReportItem)),
                        SafeReportId = this.SafeReportId,
                        ProjectId = item,
                        States = BLL.Const.State_0
                    };
                    BLL.SafeReportItemService.AddSafeReportItem(newSafeReportItem);
                }
            }

            foreach (var item in this.drpUnits.SelectedValueArray)
            {
                if (item != BLL.Const._Null)
                {
                    Model.Manager_SafeReportUnitItem newSafeReportItem = new Model.Manager_SafeReportUnitItem
                    {
                        SafeReportUnitItemId = SQLHelper.GetNewID(typeof(Model.Manager_SafeReportUnitItem)),
                        SafeReportId = this.SafeReportId,
                        UnitId = item,
                        States = BLL.Const.State_0
                    };
                    BLL.SafeReportUnitItemService.AddSafeReportUnitItem(newSafeReportItem);
                }
            }
        }
        #endregion
    }
}