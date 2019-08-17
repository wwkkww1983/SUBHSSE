using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Hazard
{
    public partial class ProjectSuperviseCheckRectifyEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string SuperviseCheckRectifyId
        {
            get
            {
                return (string)ViewState["SuperviseCheckRectifyId"];
            }
            set
            {
                ViewState["SuperviseCheckRectifyId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.View_Supervise_SuperviseCheckRectifyItem> superviseCheckRectifyItems = new List<Model.View_Supervise_SuperviseCheckRectifyItem>();
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ////权限按钮方法
                this.GetButtonPower();
                this.SuperviseCheckRectifyId = Request.Params["SuperviseCheckRectifyId"];
                if (!string.IsNullOrEmpty(this.SuperviseCheckRectifyId))
                {
                    var rectify = Funs.DB.View_Supervise_SuperviseCheckRectify.FirstOrDefault(x => x.SuperviseCheckRectifyId == this.SuperviseCheckRectifyId);
                    if (rectify != null)
                    {
                        this.lbUnitName.Text = rectify.UnitName;
                        this.lbProjectName.Text = rectify.ProjectName;
                        this.lbSuperviseCheckRectifyCode.Text = rectify.SuperviseCheckRectifyCode;
                        if (!string.IsNullOrEmpty(rectify.IssueMan))
                        {
                            this.txtIssueMan.Text = rectify.IssueMan;
                        }
                        else
                        {
                            this.txtIssueMan.Text = this.CurrUser.UserName;
                        }
                        if (rectify.IssueDate != null)
                        {
                            this.txtIssueDate.Text = string.Format("{0:yyyy-MM-dd}", rectify.IssueDate);
                        }
                        else
                        {
                            this.txtIssueDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                        }

                        superviseCheckRectifyItems = (from x in Funs.DB.View_Supervise_SuperviseCheckRectifyItem where x.SuperviseCheckRectifyId == this.SuperviseCheckRectifyId orderby x.RectifyCode select x).ToList();
                        Grid1.DataSource = superviseCheckRectifyItems;
                        Grid1.DataBind();
                    }
                }
            }
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
            Save("add");
        }

        //protected void btnUpdata_Click(object sender, EventArgs e)
        //{
        //    Save("updata");
        //}

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="type"></param>
        private void Save(string type)
        {
            Model.Supervise_SuperviseCheckRectify superviseCheckRectify = BLL.SuperviseCheckRectifyService.GetSuperviseCheckRectifyById(Request.Params["SuperviseCheckRectifyId"]);
            superviseCheckRectify.IssueMan = this.txtIssueMan.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtIssueDate.Text.Trim()))
            {
                superviseCheckRectify.IssueDate = Convert.ToDateTime(this.txtIssueDate.Text.Trim());
            }
            BLL.SuperviseCheckRectifyItemService.DeleteSuperviseCheckRectifyItemBySuperviseCheckRectifyId(Request.Params["SuperviseCheckRectifyId"]);
            jerqueSaveList();
            bool result = true;
            foreach (var item in superviseCheckRectifyItems)
            {
                Model.Supervise_SuperviseCheckRectifyItem newSuperviseCheckRectifyItem = new Model.Supervise_SuperviseCheckRectifyItem
                {
                    SuperviseCheckRectifyItemId = item.SuperviseCheckRectifyItemId,
                    SuperviseCheckRectifyId = item.SuperviseCheckRectifyId,
                    RectifyItemId = item.RectifyItemId,
                    ConfirmMan = item.ConfirmMan,
                    ConfirmDate = item.ConfirmDate,
                    OrderEndDate = item.OrderEndDate,
                    OrderEndPerson = item.OrderEndPerson,
                    RealEndDate = item.RealEndDate,
                    AttachUrl = item.AttachUrl
                };
                if (item.RealEndDate == null)
                {
                    result = false;
                }

                BLL.SuperviseCheckRectifyItemService.AddSuperviseCheckRectifyItem(newSuperviseCheckRectifyItem);
            }
            if (result)    //已全部确认完成
            {
                superviseCheckRectify.HandleState = "3";    //已完成
            }
            else
            {
                superviseCheckRectify.HandleState = "2";    //已签发但未完成
            }
            BLL.SuperviseCheckRectifyService.UpdateSuperviseCheckRectify(superviseCheckRectify);
            BLL.LogService.AddSys_Log(this.CurrUser, superviseCheckRectify.SuperviseCheckRectifyCode, superviseCheckRectify.SuperviseCheckRectifyId, BLL.Const.SuperviseCheckRectifyMenuId, BLL.Const.BtnModify);
            if (type == "updata" && superviseCheckRectify.IsFromMainUnit == true)     //保存并上报
            {
                Update(this.SuperviseCheckRectifyId);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 上传到集团公司
        /// <summary>
        /// 上传到集团公司
        /// </summary>
        /// <param name="superviseCheckRectifyId"></param>
        private void Update(string superviseCheckRectifyId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertSupervise_SuperviseCheckRectifyTableCompleted += new EventHandler<BLL.HSSEService.DataInsertSupervise_SuperviseCheckRectifyTableCompletedEventArgs>(poxy_DataInsertSupervise_SuperviseCheckRectifyTableCompleted);
            var rectify = from x in Funs.DB.View_SuperviseCheckRectify
                          where x.RealEndDate != null && x.SuperviseCheckRectifyId == superviseCheckRectifyId && x.IsFromMainUnit == true
                          select new BLL.HSSEService.Supervise_SuperviseCheckRectify
                          {
                              SuperviseCheckRectifyId = x.SuperviseCheckRectifyId,
                              SuperviseCheckRectifyCode = x.SuperviseCheckRectifyCode,
                              ProjectId = x.ProjectId,
                              UnitId = x.UnitId,
                              CheckDate = x.CheckDate,
                              IssueMan = x.IssueMan,
                              IssueDate = x.IssueDate,
                              SuperviseCheckReportId = x.SuperviseCheckReportId,
                              HandleState = x.HandleState,
                              SuperviseCheckRectifyItemId = x.SuperviseCheckRectifyItemId,
                              RectifyItemId = x.RectifyItemId,
                              ConfirmMan = x.ConfirmMan,
                              ConfirmDate = x.ConfirmDate,
                              OrderEndDate = x.OrderEndDate,
                              OrderEndPerson = x.OrderEndPerson,
                              RealEndDate = x.RealEndDate,
                          };
            poxy.DataInsertSupervise_SuperviseCheckRectifyTableAsync(rectify.ToList());
        }
        #endregion

        #region 企业监督检查整改
        /// <summary>
        /// 企业监督检查整改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertSupervise_SuperviseCheckRectifyTableCompleted(object sender, BLL.HSSEService.DataInsertSupervise_SuperviseCheckRectifyTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                BLL.LogService.AddSys_Log(this.CurrUser, "【企业监督检查整改】上传到服务器" + idList.Count.ToString() + "条数据；",null, BLL.Const.SuperviseCheckRectifyMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【企业监督检查整改】上传到服务器失败；", null, BLL.Const.SuperviseCheckRectifyMenuId, BLL.Const.BtnUploadResources);
            }
        }
        #endregion

        #region 保存集合
        /// <summary>
        /// 保存集合
        /// </summary>
        private void jerqueSaveList()
        {
            JArray mergedData = Grid1.GetMergedData();
            foreach (JObject mergedRow in mergedData)
            {
                string status = mergedRow.Value<string>("status");
                JObject values = mergedRow.Value<JObject>("values");
                var item = superviseCheckRectifyItems.FirstOrDefault(e => e.SuperviseCheckRectifyItemId == values.Value<string>("SuperviseCheckRectifyItemId"));
                item.ConfirmMan = values.Value<string>("ConfirmMan");
                item.ConfirmDate = Funs.GetNewDateTime(values.Value<string>("ConfirmDate"));
                item.OrderEndDate = Funs.GetNewDateTime(values.Value<string>("OrderEndDate"));
                item.OrderEndPerson = values.Value<string>("OrderEndPerson");
                item.RealEndDate = Funs.GetNewDateTime(values.Value<string>("RealEndDate"));
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
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SuperviseCheckRectifyMenuId);
            if (buttonList.Count() > 0)
            {

                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
                //if (buttonList.Contains(BLL.Const.BtnSaveUp))
                //{
                //    this.btnUpdata.Hidden = false;
                //}
            }
        }
        #endregion
    }
}