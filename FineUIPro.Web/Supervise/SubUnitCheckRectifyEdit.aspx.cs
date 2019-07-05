using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Supervise
{
    public partial class SubUnitCheckRectifyEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 监督检查报告主键
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
        /// 监督评价报告主键
        /// </summary>
        public string SubUnitCheckRectifyId
        {
            get
            {
                return (string)ViewState["SubUnitCheckRectifyId"];
            }
            set
            {
                ViewState["SubUnitCheckRectifyId"] = value;
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

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.Supervise_SubUnitCheckRectifyItem> subUnitCheckRectifyItems = new List<Model.Supervise_SubUnitCheckRectifyItem>();
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
                GetButtonPower();
                subUnitCheckRectifyItems.Clear();
                this.drpUnitId.SelectedValue = this.CurrUser.UnitId;
                this.dpkUpDateTime.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                this.drpUnitId.DataTextField = "UnitName";
                this.drpUnitId.DataValueField = "UnitId";
                this.drpUnitId.DataSource = BLL.UnitService.GetThisUnitDropDownList();
                this.drpUnitId.DataBind();

                this.drpCheckRectType.DataTextField = "ConstText";
                this.drpCheckRectType.DataValueField = "ConstValue";
                this.drpCheckRectType.DataSource = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_CheckRectType);
                this.drpCheckRectType.DataBind();

                this.SuperviseCheckReportId = Request.Params["SuperviseCheckReportId"];
                if (!string.IsNullOrEmpty(this.SuperviseCheckReportId))
                {
                    Model.Supervise_SubUnitCheckRectify subUnitCheckRectify = BLL.SubUnitCheckRectifyService.GetSubUnitCheckRectifyBySuperviseCheckReportId(this.SuperviseCheckReportId);
                    if (subUnitCheckRectify!=null)
                    {
                        this.SubUnitCheckRectifyId = subUnitCheckRectify.SubUnitCheckRectifyId;
                        if (!string.IsNullOrEmpty(subUnitCheckRectify.UnitId))
                        {
                            this.drpUnitId.SelectedValue = subUnitCheckRectify.UnitId;
                        }
                        this.drpCheckRectType.SelectedValue = subUnitCheckRectify.CheckRectType;
                        if (subUnitCheckRectify.UpDateTime.HasValue)
                        {
                            this.dpkUpDateTime.Text = string.Format("{0:yyyy-MM-dd}", subUnitCheckRectify.UpDateTime);
                        }
                        if (subUnitCheckRectify.CheckEndDate.HasValue)
                        {
                            this.dpkCheckEndDate.Text = string.Format("{0:yyyy-MM-dd}", subUnitCheckRectify.CheckEndDate);
                        }
                        this.txtValues1.Text = subUnitCheckRectify.Values1.Trim();
                        this.txtValues2.Text = subUnitCheckRectify.Values2.Trim();
                        this.txtValues3.Text = subUnitCheckRectify.Values3.Trim();
                        this.txtValues4.Text = subUnitCheckRectify.Values4.Trim();
                        this.txtValues5.Text = subUnitCheckRectify.Values5.Trim();
                        this.txtValues6.Text = subUnitCheckRectify.Values6.Trim();
                        this.txtValues7.Text = subUnitCheckRectify.Values7.Trim();
                        this.txtValues8.Text = subUnitCheckRectify.Values8.Trim();
                        if (!string.IsNullOrEmpty(subUnitCheckRectify.AttachUrl))
                        {
                            this.FullAttachUrl = subUnitCheckRectify.AttachUrl;
                            this.lbAttachUrl.Text = subUnitCheckRectify.AttachUrl.Substring(subUnitCheckRectify.AttachUrl.IndexOf("~") + 1);
                        }
                        subUnitCheckRectifyItems = BLL.SubUnitCheckRectifyItemService.GetSubUnitCheckRectifyItemList(this.SubUnitCheckRectifyId);

                        int i = subUnitCheckRectifyItems.Count * 10;
                        int count = subUnitCheckRectifyItems.Count;
                        if (count < 10)
                        {
                            for (int j = 0; j < (10 - count); j++)
                            {
                                Model.Supervise_SubUnitCheckRectifyItem newItem = new Model.Supervise_SubUnitCheckRectifyItem
                                {
                                    SubUnitCheckRectifyItemId = SQLHelper.GetNewID(typeof(Model.Supervise_SubUnitCheckRectifyItem))
                                };
                                subUnitCheckRectifyItems.Add(newItem);
                            }
                        }
                        this.Grid1.DataSource = subUnitCheckRectifyItems;
                        this.Grid1.DataBind();
                    }
                    else
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Model.Supervise_SubUnitCheckRectifyItem newItem = new Model.Supervise_SubUnitCheckRectifyItem
                            {
                                SubUnitCheckRectifyItemId = SQLHelper.GetNewID(typeof(Model.Supervise_SubUnitCheckRectifyItem))
                            };
                            subUnitCheckRectifyItems.Add(newItem);
                        }
                        Grid1.DataSource = subUnitCheckRectifyItems;
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
            SaveData(BLL.Const.UpState_1);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(string upState)
        {
            Model.Supervise_SubUnitCheckRectify subUnitCheckRectify = new Model.Supervise_SubUnitCheckRectify
            {
                SuperviseCheckReportId = this.SuperviseCheckReportId
            };
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                subUnitCheckRectify.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpCheckRectType.SelectedValue != BLL.Const._Null)
            {
                subUnitCheckRectify.CheckRectType = this.drpCheckRectType.SelectedValue;
            }
            subUnitCheckRectify.Values1 = this.txtValues1.Text.Trim();
            subUnitCheckRectify.Values2 = this.txtValues2.Text.Trim();
            subUnitCheckRectify.Values3 = this.txtValues3.Text.Trim();
            subUnitCheckRectify.Values4 = this.txtValues4.Text.Trim();
            subUnitCheckRectify.Values5 = this.txtValues5.Text.Trim();
            subUnitCheckRectify.Values6 = this.txtValues6.Text.Trim();
            subUnitCheckRectify.Values7 = this.txtValues7.Text.Trim();
            subUnitCheckRectify.Values8 = this.txtValues8.Text.Trim();
            subUnitCheckRectify.AttachUrl = this.FullAttachUrl;
            subUnitCheckRectify.UpState = upState;
            subUnitCheckRectify.UpDateTime = Funs.GetNewDateTime(this.dpkUpDateTime.Text.Trim());
            subUnitCheckRectify.CheckEndDate = Funs.GetNewDateTime(this.dpkCheckEndDate.Text.Trim());
            if (!string.IsNullOrEmpty(this.SubUnitCheckRectifyId))
            {
                subUnitCheckRectify.SubUnitCheckRectifyId = this.SubUnitCheckRectifyId;
                BLL.SubUnitCheckRectifyService.UpdateSubUnitCheckRectify(subUnitCheckRectify);
                BLL.SubUnitCheckRectifyItemService.DeleteSubUnitCheckRectifyItemsList(this.SubUnitCheckRectifyId);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "修改安全监督评价报告");
            }
            else
            {
                subUnitCheckRectify.SubUnitCheckRectifyId = SQLHelper.GetNewID(typeof(Model.Supervise_SubUnitCheckRectify));
                BLL.SubUnitCheckRectifyService.AddSubUnitCheckRectify(subUnitCheckRectify);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加安全监督评价报告");
            }
            this.GetItems(subUnitCheckRectify.SubUnitCheckRectifyId);
            foreach (var item in subUnitCheckRectifyItems)
            {
                if (!string.IsNullOrEmpty(item.Name))
                {
                    BLL.SubUnitCheckRectifyItemService.AddSubUnitCheckRectifyItem(item);
                }
            }
        }

        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="subUnitCheckRectifyId"></param>
        private void GetItems(string subUnitCheckRectifyId)
        {
            subUnitCheckRectifyItems.Clear();
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");
                Model.Supervise_SubUnitCheckRectifyItem item = new Model.Supervise_SubUnitCheckRectifyItem();
                if (values["SubUnitCheckRectifyItemId"].ToString() != "")
                {
                    item.SubUnitCheckRectifyItemId = values.Value<string>("SubUnitCheckRectifyItemId");
                }
                item.SubUnitCheckRectifyId = subUnitCheckRectifyId;
                if (values["Name"].ToString() != "")
                {
                    item.Name = values.Value<string>("Name");
                }
                if (values["Sex"].ToString() != "")
                {
                    item.Sex = values.Value<string>("Sex");
                }
                if (values["UnitName"].ToString() != "")
                {
                    item.UnitName = values.Value<string>("UnitName");
                }
                if (values["PostName"].ToString() != "")
                {
                    item.PostName = values.Value<string>("PostName");
                }
                if (values["WorkTitle"].ToString() != "")
                {
                    item.WorkTitle = values.Value<string>("WorkTitle");
                }
                if (values["CheckPostName"].ToString() != "")
                {
                    item.CheckPostName = values.Value<string>("CheckPostName");
                }
                if (values["CheckDate"].ToString() != "")
                {
                    item.CheckDate = Convert.ToDateTime(values.Value<string>("CheckDate"));
                }
                subUnitCheckRectifyItems.Add(item);
            }
        }

        #endregion

        #region Grid点击事件
        /// <summary>
        /// Grid1行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            GetItems(string.Empty);
            if (e.CommandName == "Add")
            {
                Model.Supervise_SubUnitCheckRectifyItem newItem = new Model.Supervise_SubUnitCheckRectifyItem
                {
                    SubUnitCheckRectifyItemId = SQLHelper.GetNewID(typeof(Model.Supervise_SubUnitCheckRectifyItem))
                };
                subUnitCheckRectifyItems.Add(newItem);
                Grid1.DataSource = subUnitCheckRectifyItems;
                Grid1.DataBind();
            }
            if (e.CommandName == "Delete")
            {
                foreach (var item in subUnitCheckRectifyItems)
                {
                    if (item.SubUnitCheckRectifyItemId == rowID)
                    {
                        subUnitCheckRectifyItems.Remove(item);
                        break;
                    }
                }
                Grid1.DataSource = subUnitCheckRectifyItems;
                Grid1.DataBind();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpAttachUrl_Click(object sender, EventArgs e)
        {
            if (fuAttachUrl.HasFile)
            {
                this.lbAttachUrl.Text = fuAttachUrl.ShortFileName;
                if (ValidateFileTypes(this.lbAttachUrl.Text))
                {
                    ShowNotify("无效的文件类型！", MessageBoxIcon.Warning);
                    return;
                }
                this.FullAttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.fuAttachUrl, this.FullAttachUrl, UploadFileService.SubUnitCheckRectifyFilePath);
                if (string.IsNullOrEmpty(this.FullAttachUrl))
                {
                    ShowNotify("文件名已经存在！", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    ShowNotify("文件上传成功！", MessageBoxIcon.Success);
                }
            }
            else
            {
                ShowNotify("上传文件不存在！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 查看附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSeeAttachUrl_Click(object sender, EventArgs e)
        {
            string filePath = BLL.Funs.RootPath + this.FullAttachUrl;
            string fileName = Path.GetFileName(filePath);
            FileInfo info = new FileInfo(filePath);
            if (info.Exists)
            {
                long fileSize = info.Length;
                Response.Clear();
                Response.ContentType = "application/x-zip-compressed";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.AddHeader("Content-Length", fileSize.ToString());
                Response.TransmitFile(filePath, 0, fileSize);
                Response.Flush();
                Response.Close();
                this.SimpleForm1.Reset();
            }
            else
            {
                ShowNotify("文件不存在！",MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteAttachUrl_Click(object sender, EventArgs e)
        {
            this.fuAttachUrl.Reset();
            this.lbAttachUrl.Text = string.Empty;
            this.FullAttachUrl = string.Empty;
        }
        #endregion

        #region 权限设置
        /// <summary>
        /// 权限设置
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.SuperviseCheckReportMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;                   
                    this.btnUpAttachUrl.Hidden = false;
                    this.btnDeleteAttachUrl.Hidden = false;
                }
            }
        }
        #endregion
    }
}