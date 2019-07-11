using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.ServerCheck
{
    public partial class CheckRectifyEdit : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckRectifyId
        {
            get
            {
                return (string)ViewState["CheckRectifyId"];
            }
            set
            {
                ViewState["CheckRectifyId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.View_CheckRectifyListFromSUB> CheckRectifyItems = new List<Model.View_CheckRectifyListFromSUB>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                this.GetButtonPower();
                this.CheckRectifyId = Request.Params["CheckRectifyId"];
                if (!string.IsNullOrEmpty(this.CheckRectifyId))
                {
                    var rectify = Funs.DB.Check_CheckRectify.FirstOrDefault(x => x.CheckRectifyId == this.CheckRectifyId);
                    if (rectify != null)
                    {
                        var unit = BLL.UnitService.GetUnitByUnitId(rectify.UnitId);
                        if (unit != null)
                        {
                            this.lbUnitName.Text = unit.UnitName;
                        }

                        this.lbProjectName.Text = rectify.ProjectId;
                        this.lbCheckRectifyCode.Text = rectify.CheckRectifyCode;
                        if (!string.IsNullOrEmpty(rectify.IssueMan))
                        {
                            this.txtIssueMan.Text = rectify.IssueMan;
                        }                      
                        if (rectify.IssueDate.HasValue)
                        {
                            this.txtIssueDate.Text = string.Format("{0:yyyy-MM-dd}", rectify.IssueDate);                            
                        }
                        if (rectify.HandleState == BLL.Const.State_3)
                        {
                            this.btnSave.Hidden = true;
                            this.btnSaveUp.Hidden = true;
                        }

                        CheckRectifyItems = (from x in Funs.DB.View_CheckRectifyListFromSUB where x.CheckRectifyId == this.CheckRectifyId orderby x.SortIndex select x).ToList();
                        Grid1.DataSource = CheckRectifyItems;
                        Grid1.DataBind();
                    }
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
            this.SavaData(BLL.Const.BtnSave);
        }

        /// <summary>
        /// 保存并提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveUp_Click(object sender, EventArgs e)
        {
            this.SavaData(BLL.Const.BtnSaveUp);
        }

        /// <summary>
        /// 保存数据方法
        /// </summary>
        /// <param name="type"></param>
        private void SavaData(string type)
        {
            jerqueSaveList();
            foreach (var item in CheckRectifyItems)
            {
                var newCheckRectifyItem = BLL.CheckRectifyItemService.GetCheckRectifyItemByCheckRectifyItemId(item.CheckRectifyItemId);
                if (newCheckRectifyItem != null)
                {
                    newCheckRectifyItem.RealEndDate = item.RealEndDate;
                    newCheckRectifyItem.OrderEndPerson = item.OrderEndPerson;
                    newCheckRectifyItem.Verification = item.Verification;
                    BLL.CheckRectifyItemService.UpdateCheckRectifyItem(newCheckRectifyItem);
                }
            }

            var newCheckRectify = BLL.CheckRectifyService.GetCheckRectifyByCheckRectifyId(this.CheckRectifyId);
            if (newCheckRectify != null && newCheckRectify.HandleState != BLL.Const.State_3)
            {
                newCheckRectify.HandleState = BLL.Const.State_2;    //待上报              
                BLL.CheckRectifyService.UpdateCheckRectify(newCheckRectify);
            }

            if (type == BLL.Const.BtnSaveUp)
            {
                this.SynchData();
            }

            BLL.LogService.AddSys_Log(this.CurrUser, string.Empty, string.Empty, BLL.Const.CheckRectifyMenuId, BLL.Const.BtnModify);            
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

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
                string checkRectifyItemId = values.Value<string>("CheckRectifyItemId").ToString();
                var item = CheckRectifyItems.FirstOrDefault(e => e.CheckRectifyItemId == checkRectifyItemId);
                if (item != null)
                {
                    item.OrderEndPerson = values.Value<string>("OrderEndPerson").ToString();
                    item.RealEndDate = Funs.GetNewDateTime(values.Value<string>("RealEndDate").ToString());
                    item.Verification = values.Value<string>("Verification").ToString();
                }
            }
        }
        
        #region 权限设置
        /// <summary>
        /// 权限按钮设置
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.CheckRectifyMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.btnSaveUp.Hidden = false;
                }
            }
        }
        #endregion

        #region 关闭按钮事件
        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClose_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 安全监督检查整改上报到集团公司 
        /// <summary>
        /// 同步方法
        /// </summary>
        private void SynchData()
        {
            string unitId = string.Empty;
            var unit = BLL.CommonService.GetIsThisUnit();
            if (unit != null)
            {
                unitId = unit.UnitId;
            }

            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertCheck_CheckRectifyTableCompleted += new EventHandler<HSSEService.DataInsertCheck_CheckRectifyTableCompletedEventArgs>(poxy_DataInsertCheck_CheckRectifyTableCompleted);
            var rectify = from x in Funs.DB.View_CheckRectifyListFromSUB
                          where x.RealEndDate.HasValue && x.CheckRectifyId == this.CheckRectifyId
                          select new HSSEService.Check_CheckRectify
                          {
                              CheckRectifyId = x.CheckRectifyId,
                              CheckRectifyCode = x.CheckRectifyCode,
                              ProjectId = x.ProjectId,
                              UnitId = x.UnitId,
                              CheckDate = x.CheckDate,
                              IssueMan = x.IssueMan,
                              IssueDate = x.IssueDate,
                              HandleState = x.HandleState,
                              CheckRectifyItemId = x.CheckRectifyItemId,
                              ConfirmMan = x.ConfirmMan,
                              ConfirmDate = x.ConfirmDate,
                              OrderEndDate = x.OrderEndDate,
                              OrderEndPerson = x.OrderEndPerson,
                              RealEndDate = x.RealEndDate,
                              Verification = x.Verification,
                              AttachFileId = x.AttachFileId2,
                              ToKeyId = x.ToKeyId2,
                              AttachSource = x.AttachSource2,
                              AttachUrl = x.AttachUrl2,

                              ////附件转为字节传送
                              FileContext = FileStructService.GetMoreFileStructByAttachUrl(x.AttachUrl2),
                          };

            poxy.DataInsertCheck_CheckRectifyTableAsync(rectify.ToList());
        }

        /// <summary>
        /// 安全监督检查整改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertCheck_CheckRectifyTableCompleted(object sender, HSSEService.DataInsertCheck_CheckRectifyTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var newCheckRectify = BLL.CheckRectifyService.GetCheckRectifyByCheckRectifyId(item);
                    if (newCheckRectify != null)
                    {
                        var itme = Funs.DB.Check_CheckRectifyItem.FirstOrDefault(x => x.CheckRectifyId == item && !x.RealEndDate.HasValue);
                        if (itme == null)
                        {
                            newCheckRectify.HandleState = BLL.Const.State_3;    //已完成              
                            BLL.CheckRectifyService.UpdateCheckRectify(newCheckRectify);
                        }
                    }
                }

                BLL.LogService.AddSys_Log(this.CurrUser, "【集团检查整改】上传到服务器" + idList.Count.ToString() + "条数据；", string.Empty, BLL.Const.CheckRectifyMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【集团检查整改】上传到服务器失败；", string.Empty, BLL.Const.CheckRectifyMenuId, BLL.Const.BtnUploadResources);
            }
        }
        #endregion
    }
}