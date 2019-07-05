using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using System.IO;

namespace FineUIPro.Web.Technique
{
    public partial class ExpertSave : PageBase
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
                this.tbxExpertName.Focus();
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
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
                this.ExpertId = Request.QueryString["ExpertId"];
                if (!String.IsNullOrEmpty(ExpertId))
                {
                    var q = BLL.ExpertService.GetExpertById(ExpertId);
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
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(string upState)
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
            expert.UpState = upState;
            if (String.IsNullOrEmpty(ExpertId))
            {
                expert.CompileMan = this.CurrUser.UserName;
                expert.CompileDate = DateTime.Now;
                expert.IsPass = true;
                expert.ExpertId = SQLHelper.GetNewID(typeof(Model.Technique_Expert));
                ExpertId = expert.ExpertId;
                BLL.ExpertService.AddExpert(expert);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "添加安全专家");
            }
            else
            {
                expert.ExpertId = ExpertId;
                BLL.ExpertService.UpdateExpert(expert);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "修改安全专家");
            }
        }

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
        /// 保存并上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveUp_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.UpState_2);
            var unit = BLL.CommonService.GetIsThisUnit();
            if (unit != null && !string.IsNullOrEmpty(unit.UnitId))
            {
                UpExpert(ExpertId, unit.UnitId);//上报
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 上报到集团公司
        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="lawRegulation"></param>
        public void UpExpert(string expertId, string unitId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertTechnique_ExpertTableCompleted += new EventHandler<HSSEService.DataInsertTechnique_ExpertTableCompletedEventArgs>(poxy_DataInsertTechnique_ExpertTableCompleted);
            var expertList = from x in Funs.DB.View_Expert
                             join y in Funs.DB.AttachFile on x.ExpertId equals y.ToKeyId
                                 where x.ExpertId == expertId && x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                             select new HSSEService.Technique_Expert
                                 {
                                     ExpertId = x.ExpertId,
                                     ExpertCode = x.ExpertCode,
                                     ExpertName = x.ExpertName,
                                     Sex = x.SexStr,
                                     Birthday = x.Birthday,
                                     Age = x.Age,
                                     UnitName = x.UnitName,
                                     Marriage = x.Marriage,
                                     Nation = x.Nation,
                                     IdentityCard = x.IdentityCard,
                                     Email = x.Email,
                                     Telephone = x.Telephone,
                                     Education = x.Education,
                                     Hometown = x.Hometown,
                                     UnitId = unitId,
                                     ExpertTypeId = x.ExpertTypeId,
                                     PersonSpecialtyId = x.PersonSpecialtyId,
                                     PostTitleId = x.PostTitleId,
                                     Performance = x.Performance,
                                     EffectiveDate = x.EffectiveDate,
                                     CompileMan = x.CompileMan,
                                     CompileDate = x.CompileDate,
                                     PhotoUrl = x.PhotoUrl,
                                     ExpertTypeName = x.ExpertTypeName,
                                     ExpertTypeCode = x.ExpertTypeCode,
                                     PersonSpecialtyName = x.PersonSpecialtyName,
                                     PersonSpecialtyCode = x.PersonSpecialtyCode,
                                     PostTitleName = x.PostTitleName,
                                     PostTitleCode = x.PostTitleCode,
                                     AttachFileId = y.AttachFileId,
                                     ToKeyId = y.ToKeyId,
                                     AttachSource = y.AttachSource,
                                     AttachUrl = y.AttachUrl,
                                     ////附件转为字节传送
                                     AttachUrlFileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),
                                     PhotoUrlFileContext = BLL.FileStructService.GetFileStructByAttachUrl(x.PhotoUrl),
                                     IsPass = null,
                                 };
            poxy.DataInsertTechnique_ExpertTableAsync(expertList.ToList());
        }

        /// <summary>
        /// 安全专家从子单位上报到集团单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_ExpertTableCompleted(object sender, HSSEService.DataInsertTechnique_ExpertTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var expert = BLL.ExpertService.GetExpertById(item);
                    if (expert != null)
                    {
                        expert.UpState = BLL.Const.UpState_3;
                        BLL.ExpertService.UpdateExpertIsPass(expert);
                    }
                }
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "【安全专家】上报到集团公司" + idList.Count.ToString() + "条数据；");
            }
            else
            {
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "【安全专家】上报到集团公司失败；");
            }
        }
        #endregion        

        #region 照片上传
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
                SaveData(BLL.Const.UpState_1);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Expert&menuId=05495F29-B583-43D9-89D3-3384D6783A3F", this.ExpertId)));
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
            var q = Funs.DB.Technique_Expert.FirstOrDefault(x => x.IsPass == true && x.ExpertCode == this.tbxExpertCode.Text.Trim() && (x.ExpertId != this.ExpertId || (this.ExpertId == null && x.ExpertId != null)));
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
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSaveUp))
                {
                    this.btnSaveUp.Hidden = false;
                }
            }
        }
        #endregion
    }
}