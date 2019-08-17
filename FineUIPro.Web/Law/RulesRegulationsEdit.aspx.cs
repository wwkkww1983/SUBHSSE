using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;

namespace FineUIPro.Web.Law
{
    public partial class RulesRegulationsEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string RulesRegulationsId
        {
            get
            {
                return (string)ViewState["RulesRegulationsId"];
            }
            set
            {
                ViewState["RulesRegulationsId"] = value;
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
                LoadData();

                //加载规章制度类别下拉选项
                this.ddlRulesRegulationsTypeId.DataTextField = "RulesRegulationsTypeName";
                this.ddlRulesRegulationsTypeId.DataValueField = "RulesRegulationsTypeId";
                this.ddlRulesRegulationsTypeId.DataSource = BLL.RulesRegulationsTypeService.GetRulesRegulationsTypeList();
                ddlRulesRegulationsTypeId.DataBind();
                Funs.FineUIPleaseSelect(this.ddlRulesRegulationsTypeId);

                this.RulesRegulationsId = Request.Params["RulesRegulationsId"];
                if (!string.IsNullOrEmpty(this.RulesRegulationsId))
                {
                    var rulesRegulation = BLL.RulesRegulationsService.GetRulesRegulationsById(this.RulesRegulationsId);
                    if (rulesRegulation != null)
                    {
                        this.txtRulesRegulationsCode.Text = rulesRegulation.RulesRegulationsCode;
                        this.txtRulesRegulationsName.Text = rulesRegulation.RulesRegulationsName;
                        if (!string.IsNullOrEmpty(rulesRegulation.RulesRegulationsTypeId))
                        {
                            this.ddlRulesRegulationsTypeId.SelectedValue = rulesRegulation.RulesRegulationsTypeId;
                        }
                        if (rulesRegulation.CustomDate != null)
                        {
                            this.dpkCustomDate.Text = string.Format("{0:yyyy-MM-dd}", rulesRegulation.CustomDate);
                        }
                        this.txtApplicableScope.Text = rulesRegulation.ApplicableScope;
                        //if (!string.IsNullOrEmpty(rulesRegulation.AttachUrl))
                        //{
                        //    this.FullAttachUrl = rulesRegulation.AttachUrl;
                        //    this.lbAttachUrl.Text = rulesRegulation.AttachUrl.Substring(rulesRegulation.AttachUrl.IndexOf("~") + 1);
                        //}
                        this.txtRemark.Text = rulesRegulation.Remark;
                    }
                }
               
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
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
                UpRulesRegulations(this.RulesRegulationsId, unit.UnitId);//上报
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(string upState, bool isClose)
        {
            Model.Law_RulesRegulations rulesRegulations = new Model.Law_RulesRegulations
            {
                RulesRegulationsCode = this.txtRulesRegulationsCode.Text.Trim(),
                RulesRegulationsName = this.txtRulesRegulationsName.Text.Trim()
            };
            if (this.ddlRulesRegulationsTypeId.SelectedValue != BLL.Const._Null)
            {
                rulesRegulations.RulesRegulationsTypeId = this.ddlRulesRegulationsTypeId.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.dpkCustomDate.Text.Trim()))
            {
                rulesRegulations.CustomDate = Convert.ToDateTime(this.dpkCustomDate.Text.Trim());
            }
            rulesRegulations.ApplicableScope = this.txtApplicableScope.Text.Trim();
            rulesRegulations.Remark = this.txtRemark.Text.Trim();
            //rulesRegulations.AttachUrl = this.FullAttachUrl;
            rulesRegulations.UpState = upState;
            if (string.IsNullOrEmpty(this.RulesRegulationsId))
            {
                rulesRegulations.IsPass = true;
                rulesRegulations.CompileMan = this.CurrUser.UserName;
                rulesRegulations.CompileDate = System.DateTime.Now;
                rulesRegulations.UnitId = this.CurrUser.UnitId;
                this.RulesRegulationsId = SQLHelper.GetNewID(typeof(Model.Law_RulesRegulations));
                rulesRegulations.RulesRegulationsId = this.RulesRegulationsId;
                BLL.RulesRegulationsService.AddRulesRegulations(rulesRegulations);
                BLL.LogService.AddSys_Log(this.CurrUser, rulesRegulations.RulesRegulationsCode, rulesRegulations.RulesRegulationsId, BLL.Const.RulesRegulationsMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                rulesRegulations.RulesRegulationsId = this.RulesRegulationsId;
                BLL.RulesRegulationsService.UpdateRulesRegulations(rulesRegulations);
                BLL.LogService.AddSys_Log(this.CurrUser, rulesRegulations.RulesRegulationsCode, rulesRegulations.RulesRegulationsId, BLL.Const.RulesRegulationsMenuId, BLL.Const.BtnModify);
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        #endregion

        #region 生产制度上报到集团公司
        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="rulesRegulationsId"></param>
        /// <param name="unitId"></param>
        public void UpRulesRegulations(string rulesRegulationsId, string unitId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertLaw_RulesRegulationsTableCompleted += new EventHandler<BLL.HSSEService.DataInsertLaw_RulesRegulationsTableCompletedEventArgs>(poxy_DataInsertLaw_RulesRegulationsTableCompleted);
            var RulesRegulations = from x in Funs.DB.View_Law_RulesRegulations
                                   join y in Funs.DB.AttachFile on x.RulesRegulationsId equals y.ToKeyId
                                   where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                                   && x.RulesRegulationsId == rulesRegulationsId
                                   select new BLL.HSSEService.Law_RulesRegulations
                                   {
                                       RulesRegulationsId = x.RulesRegulationsId,
                                       RulesRegulationsCode = x.RulesRegulationsCode,
                                       RulesRegulationsName = x.RulesRegulationsName,
                                       RulesRegulationsTypeId = x.RulesRegulationsTypeId,
                                       RulesRegulationsTypeCode = x.RulesRegulationsTypeCode,
                                       RulesRegulationsTypeName = x.RulesRegulationsTypeName,
                                       CustomDate = x.CustomDate,
                                       ApplicableScope = x.ApplicableScope,
                                       Remark = x.Remark,
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
            poxy.DataInsertLaw_RulesRegulationsTableAsync(RulesRegulations.ToList());
        }

        /// <summary>
        /// 生产制度上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertLaw_RulesRegulationsTableCompleted(object sender, BLL.HSSEService.DataInsertLaw_RulesRegulationsTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var rulesRegulations = BLL.RulesRegulationsService.GetRulesRegulationsById(item);
                    if (rulesRegulations != null)
                    {
                        rulesRegulations.UpState = BLL.Const.UpState_3;
                        BLL.RulesRegulationsService.UpdateRulesRegulations(rulesRegulations);
                    }
                }
                BLL.LogService.AddSys_Log(this.CurrUser, "【政府部门安全规章】上报到集团公司" + idList.Count.ToString() + "条数据；", null, BLL.Const.RulesRegulationsMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【政府部门安全规章】上报到集团公司失败；", null, BLL.Const.RulesRegulationsMenuId, BLL.Const.BtnUploadResources);
            }
        }
        #endregion

        #region 附件方法
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
            //        ShowNotify("无效的文件类型！");
            //        return;
            //    }
            //    this.FullAttachUrl = BLL.UploadFileService.UploadAttachment(BLL.Funs.RootPath, this.fuAttachUrl, this.FullAttachUrl, UploadFileService.RulesRegulationsFilePath);
            //    if (string.IsNullOrEmpty(this.FullAttachUrl))
            //    {
            //        ShowNotify("文件名已经存在！");
            //        return;
            //    }
            //    else
            //    {
            //        ShowNotify("文件上传成功！");
            //    }
            //}
            //else
            //{
            //    ShowNotify("上传文件不存在！");
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

        #region 按钮权限
        /// <summary>
        /// 获取权限
        /// </summary>
        private void GetButtonPower()
        {
            if (Request.Params["value"] == "0")
            {
                return;
            }
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RulesRegulationsMenuId);
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

        #region 验证规章制度名称是否存在
        /// <summary>
        /// 验证规章制度名称是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var standard = Funs.DB.Law_RulesRegulations.FirstOrDefault(x => x.IsPass == true && x.RulesRegulationsName == this.txtRulesRegulationsName.Text.Trim() && (x.RulesRegulationsId != this.RulesRegulationsId || (this.RulesRegulationsId == null && x.RulesRegulationsId != null)));
            if (standard != null)
            {
                ShowNotify("输入的规章名称已存在！", MessageBoxIcon.Warning);
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
            if (this.btnSave.Hidden)
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RulesRegulations&type=-1", RulesRegulationsId)));
            }
            else
            {
                if (string.IsNullOrEmpty(this.RulesRegulationsId))
                {
                    SaveData(BLL.Const.UpState_1, false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RulesRegulations&menuId={1}", RulesRegulationsId, BLL.Const.RulesRegulationsMenuId)));
            }
        }
        #endregion
    }
}