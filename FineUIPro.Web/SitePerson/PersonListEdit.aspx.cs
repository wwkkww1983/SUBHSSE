﻿using BLL;
using System;
using System.Linq;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Text;
using System.IO;

namespace FineUIPro.Web.SitePerson
{
    public partial class PersonListEdit : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string PersonId
        {
            get
            {
                return (string)ViewState["PersonId"];
            }
            set
            {
                ViewState["PersonId"] = value;
            }
        }
        /// <summary>
        /// 二维码路径id
        /// </summary>
        public string QRCodeAttachUrl
        {
            get
            {
                return (string)ViewState["QRCodeAttachUrl"];
            }
            set
            {
                ViewState["QRCodeAttachUrl"] = value;
            }
        }
        /// <summary>
        /// 项目ID
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

        /// <summary>
        /// 单位ID
        /// </summary>
        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                ////权限按钮方法
                this.GetButtonPower();
                this.ProjectId = Request.Params["ProjectId"];
                this.UnitId = Request.Params["UnitId"];
                this.PersonId = Request.Params["PersonId"];
                this.InitDropDownList();

                if (!string.IsNullOrEmpty(this.PersonId))
                {
                    var person = BLL.PersonService.GetPersonById(this.PersonId);
                    if (person != null)
                    {
                        this.ProjectId = person.ProjectId;
                        if (this.ProjectId != this.CurrUser.LoginProjectId)
                        {
                            this.InitDropDownList();
                        }
                        if (!string.IsNullOrEmpty(person.Sex))
                        {
                            this.rblSex.SelectedValue = person.Sex;
                        }
                        if (!string.IsNullOrEmpty(person.UnitId))
                        {
                            var unit = BLL.UnitService.GetUnitByUnitId(person.UnitId);
                            if (unit != null)
                            {
                                this.txtUnitName.Text = unit.UnitName;
                                this.UnitId = person.UnitId;
                            }
                        }
                        if (!string.IsNullOrEmpty(person.WorkAreaId))
                        {
                            this.drpWorkArea.SelectedValue = person.WorkAreaId;
                        }
                        if (!string.IsNullOrEmpty(person.WorkPostId))
                        {
                            this.drpPost.SelectedValue = person.WorkPostId;
                        }
                        if (!string.IsNullOrEmpty(person.PositionId))
                        {
                            this.drpPosition.SelectedValue = person.PositionId;
                        }
                        if (!string.IsNullOrEmpty(person.PostTitleId))
                        {
                            this.drpTitle.SelectedValue = person.PostTitleId;
                        }
                        if (!string.IsNullOrEmpty(person.TeamGroupId))
                        {
                            this.drpTeamGroup.SelectedValue = person.TeamGroupId;
                        }
                        this.rblIsUsed.SelectedValue = person.IsUsed ? "True" : "False";
                        this.rblIsCardUsed.SelectedValue = person.IsCardUsed ? "True" : "False";
                        this.txtCardNo.Text = person.CardNo;
                        this.txtPersonName.Text = person.PersonName;
                        this.txtIdentityCard.Text = person.IdentityCard;
                        this.txtAddress.Text = person.Address;
                        this.txtTelephone.Text = person.Telephone;
                        this.txtOutResult.Text = person.OutResult;
                        if (person.InTime != null)
                        {
                            this.txtInTime.Text = string.Format("{0:yyyy-MM-dd}", person.InTime);
                        }
                        if (person.OutTime != null)
                        {
                            this.txtOutTime.Text = string.Format("{0:yyyy-MM-dd}", person.OutTime);
                        }
                        imgPhoto.ImageUrl = person.PhotoUrl;
                    }

                    var personQuality = BLL.PersonQualityService.GetPersonQualityByPersonId(this.PersonId);
                    if (personQuality != null)
                    {
                        this.drpCertificate.SelectedValue = personQuality.CertificateId;
                        this.txtCertificateCode.Text = personQuality.CertificateNo;
                        this.txtCertificateLimitTime.Text = string.Format("{0:yyyy-MM-dd}", personQuality.LimitDate);
                    }
                }
                else
                {
                    var unit = BLL.UnitService.GetUnitByUnitId(this.UnitId);
                    if (unit != null)
                    {
                        this.txtUnitName.Text = unit.UnitName;
                        this.txtCardNo.Text = string.Empty;
                    }
                    this.txtInTime.Text = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                    this.rblIsUsed.SelectedValue = "True";
                    this.rblIsCardUsed.SelectedValue = "True";
                }
            }
        }
        #endregion

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkArea, this.ProjectId, true);
            BLL.WorkPostService.InitWorkPostDropDownList(this.drpPost, true);
            BLL.PositionService.InitPositionDropDownList(this.drpPosition, true);
            BLL.PostTitleService.InitPostTitleDropDownList(this.drpTitle, true);
            BLL.TeamGroupService.InitTeamGroupProjectUnitDropDownList(this.drpTeamGroup, this.ProjectId, this.UnitId, true);
            BLL.CertificateService.InitCertificateDropDownList(this.drpCertificate, true);
        }

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPersonName.Text))
            {
                ShowNotify("人员姓名不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.txtInTime.Text))
            {
                ShowNotify("入场时间不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.txtCardNo.Text))
            {
                ShowNotify("人员卡号不能为空！", MessageBoxIcon.Warning);
                return;
            }
            SaveData();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void SaveData()
        {
            string pefx = string.Empty;
            Model.Base_Project project = BLL.ProjectService.GetProjectByProjectId(this.ProjectId);
            string projectCode = string.Empty;
            if (project != null)
            {
                projectCode = project.ProjectCode;
            }
            pefx = projectCode + "-";

            var unit = BLL.UnitService.GetUnitByUnitId(this.UnitId);
            if (unit != null && !string.IsNullOrEmpty(unit.UnitCode))
            {
                pefx = pefx + unit.UnitCode + "";
            }
            else
            {
                pefx = pefx + "000";
            }

            Model.SitePerson_Person person = new Model.SitePerson_Person
            {
                Sex = this.rblSex.SelectedValue,
                ProjectId = this.ProjectId
            };
            //if (this.drpUnit.SelectedValue != BLL.Const._Null)
            //{
            //    person.UnitId = this.drpUnit.SelectedValue;
            //}
            if (!string.IsNullOrEmpty(this.UnitId))
            {
                person.UnitId = this.UnitId;
            }
            if (this.drpTeamGroup.SelectedValue != BLL.Const._Null)
            {
                person.TeamGroupId = this.drpTeamGroup.SelectedValue;
            }
            if (this.drpWorkArea.SelectedValue != BLL.Const._Null)
            {
                person.WorkAreaId = this.drpWorkArea.SelectedValue;
            }
            if (this.drpPost.SelectedValue != BLL.Const._Null)
            {
                person.WorkPostId = this.drpPost.SelectedValue;
            }
            if (this.drpPosition.SelectedValue != BLL.Const._Null)
            {
                person.PositionId = this.drpPosition.SelectedValue;
            }
            if (this.drpTitle.SelectedValue != BLL.Const._Null)
            {
                person.PostTitleId = this.drpTitle.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.rblIsUsed.SelectedValue))
            {
                person.IsUsed = Convert.ToBoolean(this.rblIsUsed.SelectedValue);
            }
            if (!string.IsNullOrEmpty(this.rblIsCardUsed.SelectedValue))
            {
                person.IsCardUsed = Convert.ToBoolean(this.rblIsCardUsed.SelectedValue);
            }
            if (!string.IsNullOrEmpty(this.txtCardNo.Text.Trim()))
            {
                person.CardNo = this.txtCardNo.Text.Trim();
            }
            person.PersonName = this.txtPersonName.Text.Trim();

            if (!string.IsNullOrEmpty(this.txtIdentityCard.Text))
            {
                person.IdentityCard = this.txtIdentityCard.Text.Trim();
            }
            person.Address = this.txtAddress.Text.Trim();
            person.Telephone = this.txtTelephone.Text.Trim();
            person.OutResult = this.txtOutResult.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtInTime.Text.Trim()))
            {
                person.InTime = Convert.ToDateTime(this.txtInTime.Text.Trim());
            }
            if (!string.IsNullOrEmpty(this.txtOutTime.Text.Trim()))
            {
                person.OutTime = Convert.ToDateTime(this.txtOutTime.Text.Trim());
            }
            person.PhotoUrl = imgPhoto.ImageUrl;
            if (string.IsNullOrEmpty(PersonId))
            {
                if (!string.IsNullOrEmpty(this.txtCardNo.Text.Trim()))
                {
                    int cardNoCount = BLL.PersonService.GetPersonCountByCardNo(this.ProjectId, this.txtCardNo.Text.Trim());

                    if (cardNoCount > 0)
                    {
                        ShowNotify("此卡号已存在，不能重复！", MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(this.txtIdentityCard.Text))
                {
                    person.IdentityCard = this.txtIdentityCard.Text.Trim();
                    var identityCardCount = PersonService.GetPersonCountByIdentityCard(this.txtIdentityCard.Text.Trim(), this.CurrUser.LoginProjectId);
                    if (identityCardCount != null)
                    {
                        ShowNotify("此身份证号已存在，不能重复！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                
                if (!BLL.PersonService.IsExistPersonByUnit(this.UnitId, this.txtPersonName.Text.Trim(), this.ProjectId))
                {
                    this.PersonId = SQLHelper.GetNewID(typeof(Model.SitePerson_Person));
                    person.PersonId = this.PersonId;
                    BLL.PersonService.AddPerson(person);
                }

                BLL.LogService.AddSys_Log(this.CurrUser, person.PersonName, person.PersonId, BLL.Const.PersonListMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                var getPerson = BLL.PersonService.GetPersonById(PersonId);
                if (getPerson != null)
                { person.FromPersonId = getPerson.FromPersonId; }

                person.PersonId = PersonId;
                BLL.PersonService.UpdatePerson(person);
                BLL.LogService.AddSys_Log(this.CurrUser, person.PersonName, person.PersonId, BLL.Const.PersonListMenuId, BLL.Const.BtnModify);
            }

            if (!string.IsNullOrEmpty(person.PersonId))
            {
                ////更新特岗人员资质
                var personQuality = BLL.PersonQualityService.GetPersonQualityByPersonId(person.PersonId);
                if (personQuality != null)
                {
                    if (this.drpCertificate.SelectedValue != BLL.Const._Null)
                    {
                        personQuality.CertificateId = this.drpCertificate.SelectedValue;
                        personQuality.CertificateName = this.drpCertificate.SelectedItem.Text;
                    }
                    personQuality.CertificateNo = this.txtCertificateCode.Text.Trim();
                    personQuality.LimitDate = Funs.GetNewDateTime(this.txtCertificateLimitTime.Text);
                    personQuality.CompileMan = this.CurrUser.UserId;
                    personQuality.CompileDate = DateTime.Now;
                    BLL.PersonQualityService.UpdatePersonQuality(personQuality);
                }
                else
                {
                    Model.QualityAudit_PersonQuality newPersonQuality = new Model.QualityAudit_PersonQuality
                    {
                        PersonQualityId = SQLHelper.GetNewID(typeof(Model.QualityAudit_PersonQuality)),
                        PersonId = person.PersonId,
                        CompileMan = this.CurrUser.UserId,
                        CompileDate = DateTime.Now
                    };
                    if (this.drpCertificate.SelectedValue != BLL.Const._Null)
                    {
                        newPersonQuality.CertificateId = this.drpCertificate.SelectedValue;
                        newPersonQuality.CertificateName = this.drpCertificate.SelectedItem.Text;
                    }
                    newPersonQuality.CertificateNo = this.txtCertificateCode.Text.Trim();
                    newPersonQuality.LimitDate = Funs.GetNewDateTime(this.txtCertificateLimitTime.Text);
                    BLL.PersonQualityService.AddPersonQuality(newPersonQuality);
                }
            }
        }
        #endregion

        #region  上传照片
        /// <summary>
        /// 上传照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void filePhoto_FileSelected(object sender, EventArgs e)
        {
            if (filePhoto.HasFile)
            {
                string fileName = filePhoto.ShortFileName;
                if (!ValidateFileType(fileName))
                {
                    ShowNotify("无效的文件类型！");
                    return;
                }
                fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;
                filePhoto.SaveAs(Server.MapPath("~/upload/" + fileName));
                imgPhoto.ImageUrl = "~/upload/" + fileName;
                // 清空文件上传组件
                filePhoto.Reset();
            }
        }
        #endregion

        #region 验证身份证 卡号是否存在
        /// <summary>
        /// 验证身份证 卡号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCardNo.Text))
            {
                var q = Funs.DB.SitePerson_Person.FirstOrDefault(x => x.ProjectId == this.CurrUser.LoginProjectId && x.CardNo == this.txtCardNo.Text.Trim() && (x.PersonId != this.PersonId || (this.PersonId == null && x.PersonId != null)));
                if (q != null)
                {
                    ShowNotify("输入的卡号已存在！", MessageBoxIcon.Warning);
                }
            }

            if (!string.IsNullOrEmpty(this.txtIdentityCard.Text))
            {
                var q2 = Funs.DB.SitePerson_Person.FirstOrDefault(x => x.ProjectId == this.CurrUser.LoginProjectId && x.IdentityCard == this.txtIdentityCard.Text.Trim() && (x.PersonId != this.PersonId || (this.PersonId == null && x.PersonId != null)));
                if (q2 != null)
                {
                    ShowNotify("输入的身份证号码已存在！", MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #region 判断按钮权限
        /// <summary>
        /// 判断按钮权限
        /// </summary>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.PersonListMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                    this.filePhoto.Hidden = false;
                    this.btnReadIdentityCard.Hidden = false;
                }                
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtIdentityCard.Text.Trim()))
            {
                Alert.ShowInTop("身份证号码不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.txtPersonName.Text))
            {
                ShowNotify("人员姓名不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.txtCardNo.Text))
            {
                ShowNotify("人员卡号不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.txtInTime.Text))
            {
                ShowNotify("入场时间不能为空！", MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(this.PersonId))
            {
                this.SaveData();
            }
            this.CreateCode_Simple(this.txtCardNo.Text.Trim());
        }

        //生成二维码方法一
        private void CreateCode_Simple(string nr)
        {
            var person = BLL.PersonService.GetPersonById(this.PersonId);
            if (person != null)
            {
                BLL.UploadFileService.DeleteFile(Funs.RootPath, person.QRCodeAttachUrl);//删除二维码
                string imageUrl = string.Empty;
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = nr.Length;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                System.Drawing.Image image = qrCodeEncoder.Encode(nr, Encoding.UTF8);

                string filepath = Server.MapPath("~/") + BLL.UploadFileService.QRCodeImageFilePath;

                //如果文件夹不存在，则创建
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                string filename = DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".jpg";
                imageUrl = filepath + filename;

                System.IO.FileStream fs = new System.IO.FileStream(imageUrl, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

                fs.Close();
                image.Dispose();
                this.QRCodeAttachUrl = BLL.UploadFileService.QRCodeImageFilePath + filename;
                person.QRCodeAttachUrl = this.QRCodeAttachUrl;
                BLL.PersonService.UpdatePerson(person);
                this.btnQR.Hidden = false;
                this.btnQR.Text = "二维码重新生成";
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("../Controls/SeeQRImage.aspx?PersonId={0}", this.PersonId), "二维码查看", 400, 400));
            }
            else
            {
                Alert.ShowInTop("操作有误，重新生成！", MessageBoxIcon.Warning);
            }

            //二维码解码
            //var codeDecoder = CodeDecoder(filepath);

            //this.Image1.ImageUrl = "~/image/" + filename + ".jpg";
        }
    }
}