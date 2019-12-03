using BLL;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Technique
{
    public partial class HAZOPEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string HAZOPId
        {
            get
            {
                return (string)ViewState["HAZOPId"];
            }
            set
            {
                ViewState["HAZOPId"] = value;
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
                var unit = BLL.CommonService.GetIsThisUnit();
                if (unit != null)
                {
                    this.hdUnitId.Text = unit.UnitId;
                    this.txtUnitName.Text = unit.UnitName;
                }

                this.HAZOPId = Request.Params["HAZOPId"];
                if (!string.IsNullOrEmpty(this.HAZOPId))
                {
                    var hazop = BLL.HAZOPService.GetHAZOPById(this.HAZOPId);
                    if (hazop != null)
                    {
                        if (!string.IsNullOrEmpty(hazop.UnitId))
                        {                           
                            var u = BLL.UnitService.GetUnitByUnitId(hazop.UnitId);
                            if (u != null)
                            {
                                this.hdUnitId.Text = u.UnitId;
                                this.txtUnitName.Text = u.UnitName;
                            }
                        }
                        this.txtTitle.Text = hazop.HAZOPTitle;
                        this.txtAbstract.Text = hazop.Abstract;
                        if (hazop.HAZOPDate != null)
                        {
                            this.dpkHAZOPDate.Text = string.Format("{0:yyyy-MM-dd}", hazop.HAZOPDate);
                        }
                        if (!string.IsNullOrEmpty(hazop.AttachUrl))
                        {
                            //this.FullAttachUrl = hazop.AttachUrl;
                            //this.lbAttachUrl.Text = hazop.AttachUrl.Substring(hazop.AttachUrl.IndexOf("~") + 1);
                        }
                    }
                }
                else
                {
                    this.dpkHAZOPDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
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
            SaveData(BLL.Const.UpState_1, true);
        }

        /// <summary>
        /// 保存并上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveUp_Click(object sender, EventArgs e)
        {
            SaveData(BLL.Const.UpState_2, true);
            var unit = BLL.CommonService.GetIsThisUnit();
            if (unit != null && !string.IsNullOrEmpty(unit.UnitId))
            {
                UpHAZOP(this.HAZOPId, unit.UnitId);//上报
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData(string upState, bool isClose)
        {
            Model.Technique_HAZOP hazop = new Model.Technique_HAZOP
            {
                UnitId = this.hdUnitId.Text,
                HAZOPTitle = this.txtTitle.Text.Trim(),
                Abstract = this.txtAbstract.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.dpkHAZOPDate.Text.Trim()))
            {
                hazop.HAZOPDate = Convert.ToDateTime(this.dpkHAZOPDate.Text.Trim());
            }
            //hazop.AttachUrl = this.FullAttachUrl;
            hazop.UpState = upState;
            if (string.IsNullOrEmpty(HAZOPId))
            {
                hazop.CompileMan = this.CurrUser.UserName;
                hazop.CompileDate = DateTime.Now;
                hazop.IsPass = true;
                this.HAZOPId = hazop.HAZOPId = SQLHelper.GetNewID(typeof(Model.Technique_HAZOP));
                BLL.HAZOPService.AddHAZOP(hazop);
                BLL.LogService.AddSys_Log(this.CurrUser, hazop.HAZOPTitle, hazop.HAZOPId, BLL.Const.HAZOPMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                hazop.HAZOPId = this.HAZOPId;
                BLL.HAZOPService.UpdateHAZOP(hazop);
                BLL.LogService.AddSys_Log(this.CurrUser, hazop.HAZOPTitle, hazop.HAZOPId, BLL.Const.HAZOPMenuId, BLL.Const.BtnModify);
            }
            if (isClose)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        #endregion

        #region 上报
        /// <summary>
        /// 上报方法
        /// </summary>
        /// <param name="hazopId"></param>
        /// <param name="unitId"></param>
        public void UpHAZOP(string hazopId, string unitId)
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            poxy.DataInsertTechnique_HAZOPTableCompleted += new EventHandler<HSSEService.DataInsertTechnique_HAZOPTableCompletedEventArgs>(poxy_DataInsertTechnique_HAZOPTableCompleted);
            var hazop = from x in Funs.DB.View_Technique_HAZOP
                        join y in Funs.DB.AttachFile on x.HAZOPId equals y.ToKeyId
                        where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                        select new HSSEService.Technique_HAZOP
                        {
                            HAZOPId = x.HAZOPId,
                            UnitId = unitId,
                            Abstract = x.Abstract,
                            HAZOPDate = x.HAZOPDate,
                            HAZOPTitle = x.HAZOPTitle,
                            CompileMan = x.CompileMan,
                            CompileDate = x.CompileDate,
                            IsPass = null,
                            AttachFileId = y.AttachFileId,
                            ToKeyId = y.ToKeyId,
                            AttachSource = y.AttachSource,
                            AttachUrl = y.AttachUrl,
                            ////附件转为字节传送
                            FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),
                        };
            poxy.DataInsertTechnique_HAZOPTableAsync(hazop.ToList());
        }

        /// <summary>
        /// HAZOP管理上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_HAZOPTableCompleted(object sender, HSSEService.DataInsertTechnique_HAZOPTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var hazop = BLL.HAZOPService.GetHAZOPById(item);
                    if (hazop != null)
                    {
                        hazop.UpState = BLL.Const.UpState_3;
                        BLL.HAZOPService.UpdateHAZOP(hazop);
                    }
                }

                BLL.LogService.AddSys_Log(this.CurrUser, "【HAZOP管理】上报到集团公司" + idList.Count.ToString() + "条数据；", string.Empty, BLL.Const.HAZOPMenuId, BLL.Const.BtnUploadResources);
            }
            else
            {
                BLL.LogService.AddSys_Log(this.CurrUser, "【HAZOP管理】上报到集团公司失败；", string.Empty, BLL.Const.HAZOPMenuId, BLL.Const.BtnUploadResources);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HAZOPMenuId);
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

        #region 验证标题是否存在
        /// <summary>
        /// 验证标题是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Technique_HAZOP.FirstOrDefault(x => x.IsPass == true && x.HAZOPTitle == this.txtTitle.Text.Trim() && (x.HAZOPId != this.HAZOPId || (this.HAZOPId == null && x.HAZOPId != null)));
            if (q != null)
            {
                ShowNotify("输入的标题已存在！", MessageBoxIcon.Warning);
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
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HAZOP&type=-1", HAZOPId)));

            }
            else
            {
                if (string.IsNullOrEmpty(this.HAZOPId))
                {
                    SaveData(BLL.Const.UpState_1, false);
                }
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HAZOP&menuId={1}", HAZOPId, BLL.Const.HAZOPMenuId)));
            }
        }
        #endregion
    }
}