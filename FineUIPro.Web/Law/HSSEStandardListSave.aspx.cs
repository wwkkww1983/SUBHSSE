using BLL;
using Model;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Law
{
    public partial class HSSEStandardListSave : PageBase
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
                this.GetButtonPower();//权限设置
                LoadData();
                this.drpType.DataTextField = "TypeName";
                drpType.DataValueField = "TypeId";
                drpType.DataSource = BLL.HSSEStandardListTypeService.GetHSSEStandardListTypeList();
                drpType.DataBind();
                Funs.FineUIPleaseSelect(this.drpType);

                StandardId = Request.QueryString["StandardId"];
                if (!String.IsNullOrEmpty(StandardId))
                {
                    var q = BLL.HSSEStandardsListService.GetHSSEStandardsListByHSSEStandardsListId(StandardId);
                    if (q != null)
                    {
                        txtStandardNo.Text = q.StandardNo;
                        txtStandardName.Text = q.StandardName;
                        this.drpType.SelectedValue = q.TypeId;
                        txtStandardGrade.Text = q.StandardGrade;
                        txtCompileMan.Text = q.CompileMan;
                        hdCompileMan.Text = q.CompileMan;
                        if (q.CompileDate != null)
                        {
                            txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", q.CompileDate);
                        }
                        //if (!string.IsNullOrEmpty(q.AttachUrl))
                        //{
                        //    this.FullAttachUrl = q.AttachUrl;
                        //    this.lbAttachUrl.Text = q.AttachUrl.Substring(q.AttachUrl.IndexOf("~") + 1);
                        //}

                        //q.AttachUrl = this.FullAttachUrl;
                        this.ckb01.Checked = q.IsSelected1.HasValue ? q.IsSelected1.Value : false;
                        this.ckb02.Checked = q.IsSelected2.HasValue ? q.IsSelected2.Value : false;
                        this.ckb03.Checked = q.IsSelected3.HasValue ? q.IsSelected3.Value : false;
                        this.ckb04.Checked = q.IsSelected4.HasValue ? q.IsSelected4.Value : false;
                        this.ckb05.Checked = q.IsSelected5.HasValue ? q.IsSelected5.Value : false;
                        this.ckb06.Checked = q.IsSelected6.HasValue ? q.IsSelected6.Value : false;
                        this.ckb07.Checked = q.IsSelected7.HasValue ? q.IsSelected7.Value : false;
                        this.ckb08.Checked = q.IsSelected8.HasValue ? q.IsSelected8.Value : false;
                        this.ckb09.Checked = q.IsSelected9.HasValue ? q.IsSelected9.Value : false;
                        this.ckb10.Checked = q.IsSelected10.HasValue ? q.IsSelected10.Value : false;
                        this.ckb11.Checked = q.IsSelected11.HasValue ? q.IsSelected11.Value : false;
                        this.ckb12.Checked = q.IsSelected12.HasValue ? q.IsSelected12.Value : false;
                        this.ckb13.Checked = q.IsSelected13.HasValue ? q.IsSelected13.Value : false;
                        this.ckb14.Checked = q.IsSelected14.HasValue ? q.IsSelected14.Value : false;
                        this.ckb15.Checked = q.IsSelected15.HasValue ? q.IsSelected15.Value : false;
                        this.ckb16.Checked = q.IsSelected16.HasValue ? q.IsSelected16.Value : false;
                        this.ckb17.Checked = q.IsSelected17.HasValue ? q.IsSelected17.Value : false;
                        this.ckb18.Checked = q.IsSelected18.HasValue ? q.IsSelected18.Value : false;
                        this.ckb19.Checked = q.IsSelected19.HasValue ? q.IsSelected19.Value : false;
                        this.ckb20.Checked = q.IsSelected20.HasValue ? q.IsSelected20.Value : false;
                        this.ckb21.Checked = q.IsSelected21.HasValue ? q.IsSelected21.Value : false;
                        this.ckb22.Checked = q.IsSelected22.HasValue ? q.IsSelected22.Value : false;
                        this.ckb23.Checked = q.IsSelected23.HasValue ? q.IsSelected23.Value : false;
                        this.ckb24.Checked = q.IsSelected24.HasValue ? q.IsSelected24.Value : false;
                        this.ckb25.Checked = q.IsSelected25.HasValue ? q.IsSelected25.Value : false;
                        this.ckb90.Checked = q.IsSelected90.HasValue ? q.IsSelected90.Value : false;
                    }
                }
                else
                {
                    txtCompileMan.Text = this.CurrUser.UserName;
                    hdCompileMan.Text = this.CurrUser.UserId;
                    txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
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
            SaveData(BLL.Const.UpState_1,true);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存并上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveUp_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.UpState_2,true);
            var unit = BLL.CommonService.GetIsThisUnit();
            if (unit != null && !string.IsNullOrEmpty(unit.UnitId))
            {
                UpHSSEStandardsList(this.StandardId, unit.UnitId);//上报
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="upState"></param>
        private void SaveData(string upState, bool isClose)
        {
            Model.Law_HSSEStandardsList hSSEStandardsList = new Law_HSSEStandardsList
            {
                StandardNo = txtStandardNo.Text.Trim(),
                StandardName = txtStandardName.Text.Trim(),
                StandardGrade = txtStandardGrade.Text.Trim()
            };
            if (drpType.SelectedValue != BLL.Const._Null)
            {
                hSSEStandardsList.TypeId = drpType.SelectedValue;
            }
            //hSSEStandardsList.AttachUrl = this.FullAttachUrl;
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
            hSSEStandardsList.UpState = upState;
            if (string.IsNullOrEmpty(this.StandardId))
            {
                hSSEStandardsList.IsPass = true;
                hSSEStandardsList.CompileMan = this.CurrUser.UserName;
                hSSEStandardsList.UnitId = CommonService.GetUnitId(this.CurrUser.UnitId);
                hSSEStandardsList.CompileDate = System.DateTime.Now;
                this.StandardId = SQLHelper.GetNewID(typeof(Model.Law_HSSEStandardsList));
                hSSEStandardsList.StandardId = this.StandardId;
                BLL.HSSEStandardsListService.AddHSSEStandardsList(hSSEStandardsList);
                BLL.LogService.AddSys_Log(this.CurrUser, hSSEStandardsList.StandardNo, hSSEStandardsList.StandardId, BLL.Const.HSSEStandardListMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                hSSEStandardsList.StandardId = this.StandardId;
                BLL.HSSEStandardsListService.UpdateHSSEStandardsList(hSSEStandardsList);
                BLL.LogService.AddSys_Log(this.CurrUser, hSSEStandardsList.StandardNo, hSSEStandardsList.StandardId, BLL.Const.HSSEStandardListMenuId, BLL.Const.BtnModify);
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        #endregion

        #region 标准规范上报
        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        private void UpHSSEStandardsList(string standardId, string unitId)
        {
            ////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertLaw_HSSEStandardsListTableCompleted += new EventHandler<HSSEService.DataInsertLaw_HSSEStandardsListTableCompletedEventArgs>(poxy_DataInsertLaw_HSSEStandardsListTableCompleted);
            var HSSEStandardsList = from x in Funs.DB.View_HSSEStandardsList
                                    join y in Funs.DB.AttachFile on x.StandardId equals y.ToKeyId
                                    where x.IsPass == true && x.StandardId == standardId
                                    && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                                    select new BLL.HSSEService.Law_HSSEStandardsList
                                    {
                                        StandardId = x.StandardId,
                                        StandardGrade = x.StandardGrade,
                                        StandardNo = x.StandardNo,
                                        StandardName = x.StandardName,
                                        TypeId = x.TypeId,
                                        TypeCode = x.TypeCode,
                                        TypeName = x.TypeName,
                                        IsSelected1 = x.IsSelected1,
                                        IsSelected2 = x.IsSelected2,
                                        IsSelected3 = x.IsSelected3,
                                        IsSelected4 = x.IsSelected4,
                                        IsSelected5 = x.IsSelected5,
                                        IsSelected6 = x.IsSelected6,
                                        IsSelected7 = x.IsSelected7,
                                        IsSelected8 = x.IsSelected8,
                                        IsSelected9 = x.IsSelected9,
                                        IsSelected10 = x.IsSelected10,
                                        IsSelected11 = x.IsSelected11,
                                        IsSelected12 = x.IsSelected12,
                                        IsSelected13 = x.IsSelected13,
                                        IsSelected14 = x.IsSelected14,
                                        IsSelected15 = x.IsSelected15,
                                        IsSelected16 = x.IsSelected16,
                                        IsSelected17 = x.IsSelected17,
                                        IsSelected18 = x.IsSelected18,
                                        IsSelected19 = x.IsSelected19,
                                        IsSelected20 = x.IsSelected20,
                                        IsSelected21 = x.IsSelected21,
                                        IsSelected22 = x.IsSelected22,
                                        IsSelected23 = x.IsSelected23,
                                        IsSelected24 = x.IsSelected24,
                                        IsSelected25 = x.IsSelected25,
                                        IsSelected90 = x.IsSelected90,
                                        CompileMan = x.CompileMan,
                                        CompileDate = x.CompileDate,
                                        IsPass = null,
                                        UnitId = unitId,
                                        AttachFileId = y.AttachFileId,
                                        ToKeyId = y.ToKeyId,
                                        AttachSource = y.AttachSource,
                                        AttachUrl = y.AttachUrl,
                                        ////附件转为字节传送
                                        FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),
                                    };
            poxy.DataInsertLaw_HSSEStandardsListTableAsync(HSSEStandardsList.ToList());
        }

        /// <summary>
        /// 标准规范上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertLaw_HSSEStandardsListTableCompleted(object sender, HSSEService.DataInsertLaw_HSSEStandardsListTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var standardsList = BLL.HSSEStandardsListService.GetHSSEStandardsListByHSSEStandardsListId(item);
                    if (standardsList != null)
                    {
                        standardsList.UpState = BLL.Const.UpState_3;
                        BLL.HSSEStandardsListService.UpdateHSSEStandardsList(standardsList);
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【标准规范】上传到服务器" + idList.Count.ToString() + "条数据；", null, BLL.Const.HSSEStandardListMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【标准规范】上传到服务器失败；", null, BLL.Const.HSSEStandardListMenuId, BLL.Const.BtnUploadResources);
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
            //    this.SimpleForm1.Reset();
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
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //this.fuAttachUrl.Reset();
            //this.lbAttachUrl.Text = string.Empty;
            //this.FullAttachUrl = string.Empty;
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
            var standard = Funs.DB.Law_HSSEStandardsList.FirstOrDefault(x => x.IsPass == true && x.StandardName == this.txtStandardName.Text.Trim() && (x.StandardId != this.StandardId || (this.StandardId == null && x.StandardId != null)));
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
                SaveData(BLL.Const.UpState_1, false);
            }
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HSSEStandardsList&type=-1", StandardId)));
            }
            else
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HSSEStandardsList&menuId=EFDSFVDE-RTHN-7UMG-4THA-5TGED48F8IOL", StandardId)));
            }            
        }
        #endregion

        #region 权限设置
        /// <summary>
        /// 权限设置
        /// </summary>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSEStandardListMenuId);
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