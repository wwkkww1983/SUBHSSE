using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BLL;

namespace FineUIPro.Web.Law
{
    public partial class HSSEStandardListUpload : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string StandardId
        {
            get
            {
                return (string)ViewState["StandardId"];
            }
            set
            {
                ViewState["StandardId"] = value;
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
                this.GetButtonPower();
                this.InitTreeMenu();

                this.drpType.DataTextField = "TypeName";
                drpType.DataValueField = "TypeId";
                drpType.DataSource = BLL.HSSEStandardListTypeService.GetHSSEStandardListTypeList();
                drpType.DataBind();
                Funs.FineUIPleaseSelect(this.drpType);

                this.txtCompileMan.Text = this.CurrUser.UserName;
                this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
            }
        }
        #endregion

        #region 初始化树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            tvUploadResources.Nodes.Clear();

            ///加载当前人
            TreeNode rootNode = new TreeNode
            {
                Text = this.CurrUser.UserName,
                NodeID = this.CurrUser.UserId,
                Expanded = true
            };
            this.tvUploadResources.Nodes.Add(rootNode);
            ////加载上传资源的状态
            var uploadType = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_UploadResources);
            if (uploadType.Count() > 0)
            {
                var newHSSEStandardsList = BLL.HSSEStandardsListService.GetHSSEStandardsListByCompileMan(this.CurrUser.UserName);
                foreach (var item in uploadType)
                {
                    TreeNode chidNode = new TreeNode
                    {
                        Text = item.ConstText,
                        NodeID = item.ConstValue
                    };
                    rootNode.Nodes.Add(chidNode);
                    this.BoundTree(chidNode.Nodes, item.ConstValue, newHSSEStandardsList);
                }
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string typeId, List<Model.Law_HSSEStandardsList> laws)
        {
            List<Model.Law_HSSEStandardsList> chidLaw = new List<Model.Law_HSSEStandardsList>();
            if (typeId == BLL.Const.UploadResources_1) ///未审核
            {
                chidLaw = laws.Where(x => x.IsPass == null).ToList();
            }
            if (typeId == BLL.Const.UploadResources_2) ///未采用
            {
                chidLaw = laws.Where(x => x.AuditDate.HasValue && x.IsPass == false).ToList();
            }
            if (typeId == BLL.Const.UploadResources_3) ///已采用
            {
                chidLaw = laws.Where(x => x.AuditDate.HasValue && x.IsPass == true).ToList();
            }
            if (chidLaw.Count() > 0)
            {
                foreach (var item in chidLaw)
                {
                    TreeNode gChidNode = new TreeNode
                    {
                        Text = item.StandardName,
                        NodeID = item.StandardId,
                        EnableClickEvent = true
                    };
                    nodes.Add(gChidNode);
                }
            }
        }
        #endregion

        #region 树节点选择
        /// <summary>
        ///  树节点选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvUploadResources_NodeCommand(object sender, FineUIPro.TreeCommandEventArgs e)
        {
            this.SetTemp();
            this.StandardId = this.tvUploadResources.SelectedNode.NodeID;
            var standardsList = BLL.HSSEStandardsListService.GetHSSEStandardsListByHSSEStandardsListId(this.StandardId);
            if (standardsList != null)
            {
                if (standardsList.AuditDate.HasValue)
                {
                    this.btnDelete.Hidden = true;
                    this.btnSave.Hidden = true;
                }
                else
                {
                    this.btnDelete.Hidden = false;
                    this.btnSave.Hidden = false;
                }

                if (standardsList != null)
                {
                    txtStandardNo.Text = standardsList.StandardNo;
                    txtStandardName.Text = standardsList.StandardName;
                    this.drpType.SelectedValue = standardsList.TypeId;
                    txtStandardGrade.Text = standardsList.StandardGrade;
                    txtCompileMan.Text = standardsList.CompileMan;
                    if (standardsList.CompileDate != null)
                    {
                        txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", standardsList.CompileDate);
                    }
                    if (!string.IsNullOrEmpty(standardsList.AttachUrl))
                    {
                        this.FullAttachUrl = standardsList.AttachUrl;
                        this.lbAttachUrl.Text = standardsList.AttachUrl.Substring(standardsList.AttachUrl.IndexOf("~") + 1);
                    }

                    standardsList.AttachUrl = this.FullAttachUrl;
                    this.ckb01.Checked = standardsList.IsSelected1.HasValue ? standardsList.IsSelected1.Value : false;
                    this.ckb02.Checked = standardsList.IsSelected2.HasValue ? standardsList.IsSelected2.Value : false;
                    this.ckb03.Checked = standardsList.IsSelected3.HasValue ? standardsList.IsSelected3.Value : false;
                    this.ckb04.Checked = standardsList.IsSelected4.HasValue ? standardsList.IsSelected4.Value : false;
                    this.ckb05.Checked = standardsList.IsSelected5.HasValue ? standardsList.IsSelected5.Value : false;
                    this.ckb06.Checked = standardsList.IsSelected6.HasValue ? standardsList.IsSelected6.Value : false;
                    this.ckb07.Checked = standardsList.IsSelected7.HasValue ? standardsList.IsSelected7.Value : false;
                    this.ckb08.Checked = standardsList.IsSelected8.HasValue ? standardsList.IsSelected8.Value : false;
                    this.ckb09.Checked = standardsList.IsSelected9.HasValue ? standardsList.IsSelected9.Value : false;
                    this.ckb10.Checked = standardsList.IsSelected10.HasValue ? standardsList.IsSelected10.Value : false;
                    this.ckb11.Checked = standardsList.IsSelected11.HasValue ? standardsList.IsSelected11.Value : false;
                    this.ckb12.Checked = standardsList.IsSelected12.HasValue ? standardsList.IsSelected12.Value : false;
                    this.ckb13.Checked = standardsList.IsSelected13.HasValue ? standardsList.IsSelected13.Value : false;
                    this.ckb14.Checked = standardsList.IsSelected14.HasValue ? standardsList.IsSelected14.Value : false;
                    this.ckb15.Checked = standardsList.IsSelected15.HasValue ? standardsList.IsSelected15.Value : false;
                    this.ckb16.Checked = standardsList.IsSelected16.HasValue ? standardsList.IsSelected16.Value : false;
                    this.ckb17.Checked = standardsList.IsSelected17.HasValue ? standardsList.IsSelected17.Value : false;
                    this.ckb18.Checked = standardsList.IsSelected18.HasValue ? standardsList.IsSelected18.Value : false;
                    this.ckb19.Checked = standardsList.IsSelected19.HasValue ? standardsList.IsSelected19.Value : false;
                    this.ckb20.Checked = standardsList.IsSelected20.HasValue ? standardsList.IsSelected20.Value : false;
                    this.ckb21.Checked = standardsList.IsSelected21.HasValue ? standardsList.IsSelected21.Value : false;
                    this.ckb22.Checked = standardsList.IsSelected22.HasValue ? standardsList.IsSelected22.Value : false;
                    this.ckb23.Checked = standardsList.IsSelected23.HasValue ? standardsList.IsSelected23.Value : false;
                    this.ckb24.Checked = standardsList.IsSelected24.HasValue ? standardsList.IsSelected24.Value : false;
                    this.ckb25.Checked = standardsList.IsSelected25.HasValue ? standardsList.IsSelected25.Value : false;
                    this.ckb90.Checked = standardsList.IsSelected90.HasValue ? standardsList.IsSelected90.Value : false;
                }
            }
        }
        #endregion

        #region 增加按钮
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.SetTemp();
        }
        #endregion

        #region 删除按钮
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var standardsList = BLL.HSSEStandardsListService.GetHSSEStandardsListByHSSEStandardsListId(this.StandardId);
            if (standardsList != null && !standardsList.AuditDate.HasValue)
            {
                BLL.HSSEStandardsListService.DeleteHSSEStandardsList(this.StandardId);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "删除上报的安全标准规范");
                this.SetTemp();
                this.InitTreeMenu();
                ShowNotify("删除成功！");
            }
        }
        #endregion

        #region 界面清空
        /// <summary>
        /// 清空
        /// </summary>
        private void SetTemp()
        {
            this.txtStandardNo.Focus();
            this.StandardId = string.Empty;

            this.txtCompileMan.Text = this.CurrUser.UserName;
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);

            this.txtStandardNo.Text = string.Empty;
            this.txtStandardName.Text = string.Empty;
            this.txtStandardGrade.Text = string.Empty;
            this.drpType.SelectedValue = null;
            this.ckb01.Checked = false;
            this.ckb02.Checked = false;
            this.ckb03.Checked = false;
            this.ckb04.Checked = false;
            this.ckb05.Checked = false;
            this.ckb06.Checked = false;
            this.ckb07.Checked = false;
            this.ckb08.Checked = false;
            this.ckb09.Checked = false;
            this.ckb10.Checked = false;
            this.ckb11.Checked = false;
            this.ckb12.Checked = false;
            this.ckb13.Checked = false;
            this.ckb14.Checked = false;
            this.ckb15.Checked = false;
            this.ckb16.Checked = false;
            this.ckb17.Checked = false;
            this.ckb18.Checked = false;
            this.ckb19.Checked = false;
            this.ckb20.Checked = false;
            this.ckb21.Checked = false;
            this.ckb22.Checked = false;
            this.ckb23.Checked = false;
            this.ckb90.Checked = false;

            this.lbAttachUrl.Text = string.Empty;
            this.FullAttachUrl = string.Empty;
            this.lbAttachUrl.Text = string.Empty;
            this.FullAttachUrl = string.Empty;

            this.btnDelete.Hidden = false;
            this.btnSave.Hidden = false;
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
            SaveDate();
            this.InitTreeMenu();
            ShowNotify("保存成功！");
        }

        private void SaveDate()
        {
            Model.Law_HSSEStandardsList hSSEStandardsList = new Model.Law_HSSEStandardsList
            {
                StandardNo = txtStandardNo.Text.Trim(),
                StandardName = txtStandardName.Text.Trim(),
                StandardGrade = txtStandardGrade.Text.Trim()
            };
            if (drpType.SelectedValue != BLL.Const._Null)
            {
                hSSEStandardsList.TypeId = drpType.SelectedValue;
            }
            hSSEStandardsList.IsPass = null;
            hSSEStandardsList.CompileMan = this.CurrUser.UserName;
            hSSEStandardsList.UnitId = CommonService.GetUnitId(this.CurrUser.UnitId);
            if (!string.IsNullOrEmpty(txtCompileDate.Text.Trim()))
            {
                hSSEStandardsList.CompileDate = Convert.ToDateTime(txtCompileDate.Text.Trim());
            }
            hSSEStandardsList.AttachUrl = this.FullAttachUrl;
            hSSEStandardsList.IsSelected1 = this.ckb01.Checked;
            hSSEStandardsList.IsSelected2 = this.ckb02.Checked;
            hSSEStandardsList.IsSelected3 = this.ckb03.Checked;
            hSSEStandardsList.IsSelected4 = this.ckb04.Checked;
            hSSEStandardsList.IsSelected5 = this.ckb05.Checked;
            hSSEStandardsList.IsSelected6 = this.ckb06.Checked;
            hSSEStandardsList.IsSelected7 = this.ckb07.Checked;
            hSSEStandardsList.IsSelected8 = this.ckb08.Checked;
            hSSEStandardsList.IsSelected9 = this.ckb09.Checked;
            hSSEStandardsList.IsSelected10 = this.ckb10.Checked;
            hSSEStandardsList.IsSelected11 = this.ckb11.Checked;
            hSSEStandardsList.IsSelected12 = this.ckb12.Checked;
            hSSEStandardsList.IsSelected13 = this.ckb13.Checked;
            hSSEStandardsList.IsSelected14 = this.ckb14.Checked;
            hSSEStandardsList.IsSelected15 = this.ckb15.Checked;
            hSSEStandardsList.IsSelected16 = this.ckb16.Checked;
            hSSEStandardsList.IsSelected17 = this.ckb17.Checked;
            hSSEStandardsList.IsSelected18 = this.ckb18.Checked;
            hSSEStandardsList.IsSelected19 = this.ckb19.Checked;
            hSSEStandardsList.IsSelected20 = this.ckb20.Checked;
            hSSEStandardsList.IsSelected21 = this.ckb21.Checked;
            hSSEStandardsList.IsSelected22 = this.ckb22.Checked;
            hSSEStandardsList.IsSelected23 = this.ckb23.Checked;
            hSSEStandardsList.IsSelected24 = this.ckb24.Checked;
            hSSEStandardsList.IsSelected25 = this.ckb25.Checked;
            hSSEStandardsList.IsSelected90 = this.ckb90.Checked;
            if (string.IsNullOrEmpty(this.StandardId))
            {
                this.StandardId = hSSEStandardsList.StandardId = SQLHelper.GetNewID(typeof(Model.Law_HSSEStandardsList));
                BLL.HSSEStandardsListService.AddHSSEStandardsList(hSSEStandardsList);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "增加安全标准规范");
            }
            else
            {
                hSSEStandardsList.StandardId = StandardId;
                BLL.HSSEStandardsListService.UpdateHSSEStandardsList(hSSEStandardsList);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "修改安全标准规范");
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpFile_Click(object sender, EventArgs e)
        {
            //if (fuAttachUrl.HasFile)
            //{
            //    this.lbAttachUrl.Text = fuAttachUrl.ShortFileName;
            //    if (ValidateFileTypes(this.lbAttachUrl.Text))
            //    {
            //        ShowNotify("无效的文件类型！",MessageBoxIcon.Warning);
            //        return;
            //    }
            //    this.FullAttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.fuAttachUrl, this.FullAttachUrl, UploadFileService.HSSEStandardsListFilePath);
            //    if (string.IsNullOrEmpty(this.FullAttachUrl))
            //    {
            //        ShowNotify("文件名已经存在！", MessageBoxIcon.Warning);
            //        return;
            //    }
            //    else
            //    {
            //        ShowNotify("文件上传成功！",MessageBoxIcon.Success);
            //    }
            //}
            //else
            //{
            //    ShowNotify("上传文件不存在！", MessageBoxIcon.Warning);
            //}
        }

        /// <summary>
        /// 查看附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSee_Click(object sender, EventArgs e)
        {
            //string filePath = BLL.Funs.RootPath + this.FullAttachUrl;
            //string fileName = Path.GetFileName(filePath);
            //FileInfo info = new FileInfo(filePath);
            //if (info.Exists)
            //{
            //    long fileSize = info.Length;
            //    Response.Clear();
            //    Response.ContentType = "application/x-zip-compressed";
            //    Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            //    Response.AddHeader("Content-Length", fileSize.ToString());
            //    Response.TransmitFile(filePath, 0, fileSize);
            //    Response.Flush();
            //    Response.Close();
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('模板不存在，请联系管理员！')", true);
            //}
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteFile_Click(object sender, EventArgs e)
        {
            //this.fuAttachUrl.Reset();
            //this.lbAttachUrl.Text = string.Empty;
            //this.FullAttachUrl = string.Empty;
        }
        #endregion

        #region 权限设置
        /// <summary>
        /// 权限设置
        /// </summary>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSEStandardListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnUploadResources))
                {
                    this.btnNew.Hidden = false;
                    this.btnDelete.Hidden = false;
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion

        #region 验证标准规范名称是否存在
        /// <summary>
        /// 验证标准规范名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var standard = Funs.DB.Law_HSSEStandardsList.FirstOrDefault(x => x.StandardName == this.txtStandardName.Text.Trim() && (x.StandardId != this.StandardId || (this.StandardId == null && x.StandardId != null)));
            if (standard != null)
            {
                ShowNotify("输入的标准名称已存在！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadResources_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.StandardId))
            {
                SaveDate();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HSSEStandardsList&menuId=EFDSFVDE-RTHN-7UMG-4THA-5TGED48F8IOL&type=0", StandardId)));
        }
        #endregion
    }
}