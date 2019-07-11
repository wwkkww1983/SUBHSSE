using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Model;

namespace FineUIPro.Web.Technique
{
    public partial class ExpertUpload : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string ExpertId
        {
            get
            {
                return (string)ViewState["ExpertId"];
            }
            set
            {
                ViewState["ExpertId"] = value;
            }
        }

        /// <summary>
        /// 照片附件路径
        /// </summary>
        public string PhotoAttachUrl
        {
            get
            {
                return (string)ViewState["PhotoAttachUrl"];
            }
            set
            {
                ViewState["PhotoAttachUrl"] = value;
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
                this.InitTreeMenu();
                ////权限按钮方法
                this.GetButtonPower();
                this.ddlExpertType.DataTextField = "ExpertTypeName";
                ddlExpertType.DataValueField = "ExpertTypeId";
                ddlExpertType.DataSource = BLL.ExpertTypeService.GetExpertTypeDropDownList();
                ddlExpertType.DataBind();
                Funs.FineUIPleaseSelect(this.ddlExpertType);

                this.ddlPersonSpecialty.DataTextField = "PersonSpecialtyName";
                ddlPersonSpecialty.DataValueField = "PersonSpecialtyId";
                ddlPersonSpecialty.DataSource = BLL.PersonSpecialtyService.GetPersonSpecialtyDropDownList();
                ddlPersonSpecialty.DataBind();
                Funs.FineUIPleaseSelect(this.ddlPersonSpecialty);
                
                BLL.PostTitleService.InitPostTitleDropDownList(this.ddlPostTitle, true);
                //性别       
                BLL.ConstValue.InitConstValueDropDownList(this.ddlSex, ConstValue.Group_0002, true);
                //婚姻状况       
                BLL.ConstValue.InitConstValueDropDownList(this.ddlMarriage, ConstValue.Group_0003, true);
                //文化程度
                BLL.ConstValue.InitConstValueDropDownList(this.ddlEducation, ConstValue.Group_0004, true);
                //民族
                BLL.ConstValue.InitConstValueDropDownList(this.ddlNation, ConstValue.Group_0005, true);                
                this.txtCompileMan.Text = this.CurrUser.UserName;
                this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
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
                var expert = BLL.ExpertService.GetExpertByCompileMan(this.CurrUser.UserName);
                foreach (var item in uploadType)
                {
                    TreeNode chidNode = new TreeNode
                    {
                        Text = item.ConstText,
                        NodeID = item.ConstValue
                    };
                    rootNode.Nodes.Add(chidNode);
                    this.BoundTree(chidNode.Nodes, item.ConstValue, expert);
                }
            }
        }

        /// <summary>
        /// 遍历增加子节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string typeId, List<Model.Technique_Expert> expert)
        {
            List<Model.Technique_Expert> chidLaw = new List<Model.Technique_Expert>();
            if (typeId == BLL.Const.UploadResources_1) ///未审核
            {
                chidLaw = expert.Where(x => x.IsPass == null).ToList();
            }
            if (typeId == BLL.Const.UploadResources_2) ///未采用
            {
                chidLaw = expert.Where(x => x.AuditDate.HasValue && x.IsPass == false).ToList();
            }
            if (typeId == BLL.Const.UploadResources_3) ///已采用
            {
                chidLaw = expert.Where(x => x.AuditDate.HasValue && x.IsPass == true).ToList();
            }
            if (chidLaw.Count() > 0)
            {
                foreach (var item in chidLaw)
                {
                    TreeNode gChidNode = new TreeNode
                    {
                        Text = item.ExpertName,
                        NodeID = item.ExpertId,
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
            this.ExpertId = this.tvUploadResources.SelectedNode.NodeID;
            var q = BLL.ExpertService.GetExpertById(this.ExpertId);
            if (q != null)
            {
                if (q.AuditDate.HasValue)
                {
                    this.btnDelete.Hidden = true;
                    this.btnSave.Hidden = true;
                }
                else
                {
                    this.btnDelete.Hidden = false;
                    this.btnSave.Hidden = false;
                }
                if (q != null)
                {
                    tbxExpertName.Text = q.ExpertName;
                    ddlSex.SelectedValue = q.Sex;
                    if (q.Birthday != null)
                    {
                        dpBirthDay.Text = string.Format("{0:yyyy-MM-dd}", q.Birthday);
                    }
                    if (q.Age != null)
                    {
                        tbxAge.Text = q.Age.ToString();
                    }
                    ddlMarriage.SelectedValue = q.Marriage;
                    ddlNation.SelectedValue = q.Nation;
                    tbxExpertCode.Text = q.ExpertCode;
                    tbxIdentityCard.Text = q.IdentityCard;
                    tbxEmail.Text = q.Email;
                    tbxTelephone.Text = q.Telephone;
                    ddlEducation.SelectedValue = q.Education;
                    tbxHometown.Text = q.Hometown;
                    txtUnit.Text = q.UnitName;
                    ddlExpertType.SelectedValue = q.ExpertTypeId;
                    ddlPersonSpecialty.SelectedValue = q.PersonSpecialtyId;
                    ddlPostTitle.SelectedValue = q.PostTitleId;
                    txtPerformance.Text = q.Performance;
                    if (q.EffectiveDate != null)
                    {
                        txtEffectiveDate.Text = string.Format("{0:yyyy-MM-dd}", q.EffectiveDate);
                    }
                    if (!string.IsNullOrEmpty(q.PhotoUrl))
                    {
                        this.PhotoAttachUrl = q.PhotoUrl;
                        this.Image1.ImageUrl = "~/" + this.PhotoAttachUrl;
                    }

                    if (!string.IsNullOrEmpty(q.CompileMan))
                    {
                        this.txtCompileMan.Text = q.CompileMan;
                    }
                    if (q.CompileDate != null)
                    {
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", q.CompileDate);
                    }
                }
            }
        }
        #endregion

        #region 增加
        /// <summary>
        /// 增加上传资源按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            this.SetTemp();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除上传资源按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var expert = BLL.ExpertService.GetExpertById(this.ExpertId);
            if (expert != null && !expert.AuditDate.HasValue)
            {
                BLL.LogService.AddSys_Log(this.CurrUser, expert.ExpertCode, expert.ExpertId, BLL.Const.ExpertMenuId, Const.BtnDelete);
                BLL.ExpertService.DeleteExpertId(this.ExpertId);
                this.SetTemp();
                this.InitTreeMenu();
                ShowNotify("删除成功！", MessageBoxIcon.Success);
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
            InitTreeMenu();
            ShowNotify("保存成功!", MessageBoxIcon.Success);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            Model.Technique_Expert expert = new Technique_Expert
            {
                ExpertCode = tbxExpertCode.Text.Trim(),
                ExpertName = tbxExpertName.Text.Trim(),
                Sex = ddlSex.SelectedValue
            };
            if (!string.IsNullOrEmpty(dpBirthDay.Text.Trim()))
            {
                expert.Birthday = Convert.ToDateTime(dpBirthDay.Text.Trim());
            }
            if (!string.IsNullOrEmpty(tbxAge.Text.Trim()))
            {
                expert.Age = Convert.ToInt32(tbxAge.Text.Trim());
            }
            expert.Marriage = ddlMarriage.SelectedValue;
            expert.Nation = ddlNation.SelectedValue;
            expert.IdentityCard = tbxIdentityCard.Text.Trim();
            expert.Email = tbxEmail.Text.Trim();
            expert.Telephone = tbxTelephone.Text.Trim();
            expert.Education = ddlEducation.SelectedValue;
            expert.Hometown = tbxHometown.Text.Trim();
            expert.UnitName = txtUnit.Text.Trim();
            ////专家类型下拉框
            if (this.ddlExpertType.SelectedValue != BLL.Const._Null)
            {
                if (!string.IsNullOrEmpty(this.ddlExpertType.Text))
                {
                    var expertType = BLL.ExpertTypeService.GetExpertTypeByName(this.ddlExpertType.SelectedText);
                    if (expertType != null)
                    {
                        expert.ExpertTypeId = expertType.ExpertTypeId;
                    }
                    else
                    {
                        Model.Base_ExpertType newExpertType = new Model.Base_ExpertType
                        {
                            ExpertTypeId = SQLHelper.GetNewID(typeof(Model.Base_ExpertType)),
                            ExpertTypeName = this.ddlExpertType.Text
                        };
                        BLL.ExpertTypeService.AddExpertType(newExpertType);
                        expert.ExpertTypeId = newExpertType.ExpertTypeId;
                    }
                }
                else
                {
                    expert.ExpertTypeId = ddlExpertType.SelectedValue;
                }
            }
            ////专业下拉框
            if (this.ddlPersonSpecialty.SelectedValue != BLL.Const._Null)
            {
                if (!string.IsNullOrEmpty(this.ddlPersonSpecialty.Text))
                {
                    var personSpecialty = BLL.PersonSpecialtyService.GetPersonSpecialtyByName(this.ddlPersonSpecialty.SelectedText);
                    if (personSpecialty != null)
                    {
                        expert.PersonSpecialtyId = personSpecialty.PersonSpecialtyId;
                    }
                    else
                    {
                        Model.Base_PersonSpecialty newPersonSpecialty = new Model.Base_PersonSpecialty
                        {
                            PersonSpecialtyId = SQLHelper.GetNewID(typeof(Model.Base_PersonSpecialty)),
                            PersonSpecialtyName = this.ddlPersonSpecialty.Text
                        };
                        BLL.PersonSpecialtyService.AddPersonSpecialty(newPersonSpecialty);
                        expert.PersonSpecialtyId = newPersonSpecialty.PersonSpecialtyId;
                    }
                }
                else
                {
                    expert.PersonSpecialtyId = ddlPersonSpecialty.SelectedValue;
                }
            }
            ////职称下拉框
            if (this.ddlPostTitle.SelectedValue != BLL.Const._Null)
            {
                if (!string.IsNullOrEmpty(this.ddlPostTitle.Text))
                {
                    var postTitle = BLL.PostTitleService.GetPostTitleByName(this.ddlPostTitle.SelectedText);
                    if (postTitle != null)
                    {
                        expert.PostTitleId = postTitle.PostTitleId;
                    }
                    else
                    {
                        Model.Base_PostTitle newPostTitle = new Model.Base_PostTitle
                        {
                            PostTitleId = SQLHelper.GetNewID(typeof(Model.Base_PostTitle)),
                            PostTitleName = this.ddlPostTitle.Text
                        };
                        BLL.PostTitleService.AddPostTitle(newPostTitle);
                        expert.PostTitleId = newPostTitle.PostTitleId;
                    }
                }
                else
                {
                    expert.PostTitleId = ddlPostTitle.SelectedValue;
                }
            }
            expert.Performance = txtPerformance.Text.Trim();
            if (!string.IsNullOrEmpty(txtEffectiveDate.Text.Trim()))
            {
                expert.EffectiveDate = Convert.ToDateTime(txtEffectiveDate.Text.Trim());
            }
            expert.PhotoUrl = this.PhotoAttachUrl;
            expert.UnitId = this.CurrUser.UnitId;
            if (string.IsNullOrEmpty(this.ExpertId))
            {
                expert.CompileMan = this.CurrUser.UserName;
                expert.CompileDate = Convert.ToDateTime(this.txtCompileDate.Text.Trim());
                expert.IsPass = null;
                this.ExpertId = expert.ExpertId = SQLHelper.GetNewID(typeof(Model.Technique_Expert));
                BLL.ExpertService.AddExpert(expert);
                BLL.LogService.AddSys_Log(this.CurrUser, expert.ExpertCode, expert.ExpertId, BLL.Const.ExpertMenuId, Const.BtnAdd);
            }
            else
            {
                expert.ExpertId = this.ExpertId;
                BLL.ExpertService.UpdateExpert(expert);
                BLL.LogService.AddSys_Log(this.CurrUser, expert.ExpertCode, expert.ExpertId, BLL.Const.ExpertMenuId, Const.BtnModify);
            }
        }
        #endregion

        #region 上传照片
        /// <summary>
        /// 上传照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPhoto_Click(object sender, EventArgs e)
        {
            if (filePhoto.HasFile)
            {
                string fileName = filePhoto.ShortFileName;

                if (!ValidateFileType(fileName))
                {
                    ShowNotify("无效的文件类型！", MessageBoxIcon.Warning);
                    return;
                }
                this.PhotoAttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.filePhoto, this.PhotoAttachUrl, UploadFileService.ExpertFilePath);
                this.Image1.ImageUrl = "~/" + this.PhotoAttachUrl;
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
            if (string.IsNullOrEmpty(this.ExpertId))
            {
                SaveData();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Expert&menuId=05495F29-B583-43D9-89D3-3384D6783A3F&type=0", this.ExpertId)));
        }
        #endregion

        #region 清空文本框
        /// <summary>
        /// 清空文本框
        /// </summary>
        private void SetTemp()
        {
            this.tbxExpertCode.Focus();
            this.ExpertId = string.Empty;
            this.txtCompileMan.Text = this.CurrUser.UserName;
            this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
            tbxExpertName.Text = string.Empty;
            ddlSex.SelectedValue = string.Empty;
            dpBirthDay.Text = string.Empty;
            tbxAge.Text = string.Empty;
            ddlMarriage.SelectedValue = "null";
            ddlNation.SelectedValue = "null";
            tbxExpertCode.Text = string.Empty;
            tbxIdentityCard.Text = string.Empty;
            tbxEmail.Text = string.Empty;
            tbxTelephone.Text = string.Empty;
            ddlEducation.SelectedValue = "null";
            tbxHometown.Text = string.Empty;
            txtUnit.Text = string.Empty;
            ddlExpertType.SelectedValue = "null";
            ddlPersonSpecialty.SelectedValue = "null";
            ddlPostTitle.SelectedValue = "null";
            txtPerformance.Text = string.Empty;
            txtEffectiveDate.Text = string.Empty;
            this.filePhoto.Reset();
            this.lbAttachUrl.Text = string.Empty;
            this.btnDelete.Hidden = false;
            this.btnSave.Hidden = false;
        }
        #endregion

        #region 验证编号是否存在
        /// <summary>
        /// 验证编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Technique_Expert.FirstOrDefault(x => x.ExpertCode == this.tbxExpertCode.Text.Trim() && (x.ExpertId != this.ExpertId || (this.ExpertId == null && x.ExpertId != null)));
            if (q != null)
            {
                ShowNotify("输入的编号已存在！", MessageBoxIcon.Warning);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ExpertMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }

                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.btnPhoto.Hidden = false;
                }
            }
        }
        #endregion
    }
}