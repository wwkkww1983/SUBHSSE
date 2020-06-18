using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace FineUIPro.Web.Check
{
    public partial class RectifyNoticesEdit : PageBase
    {
        /// <summary>
        /// 项目主键
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
        public string RectifyNoticesId
        {
            get
            {
                return (string)ViewState["RectifyNoticesId"];
            }
            set
            {
                ViewState["RectifyNoticesId"] = value;
            }
        }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string State
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
            }
        }
        public List<Model.Check_RectifyNoticesItem> viewTestPlanTrainingList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                ////自动生成编码
                this.txtRectifyNoticesCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectRectifyNoticesMenuId, this.ProjectId, this.CurrUser.UnitId);
                //受检单位            
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId,Const.ProjectUnitType_2, false);
                //区域
                BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkAreaId, this.CurrUser.LoginProjectId, true);
                ///安全经理
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpSignPerson, this.CurrUser.LoginProjectId, BLL.CommonService.GetIsThisUnitId(), true);

                ///检察人员
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpCheckPerson, this.CurrUser.LoginProjectId, BLL.CommonService.GetIsThisUnitId(), true);
                RectifyNoticesId = Request.Params["RectifyNoticesId"];
                if (!string.IsNullOrEmpty(RectifyNoticesId))
                {                    
                    this.hdRectifyNoticesId.Text = RectifyNoticesId;
                    Model.Check_RectifyNotices RectifyNotices = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);
                    if (!string.IsNullOrEmpty(RectifyNotices.UnitId))
                    {
                        this.drpUnitId.SelectedValue = RectifyNotices.UnitId;
                    }
                    if (!string.IsNullOrEmpty(RectifyNotices.WorkAreaId))
                    {
                        this.drpWorkAreaId.SelectedValue = RectifyNotices.WorkAreaId;
                    }
                    if (!string.IsNullOrEmpty(RectifyNotices.CheckManIds))
                    {
                        this.drpCheckPerson.SelectedValueArray = RectifyNotices.CheckManIds.Split(',');
                    }
                    this.txtRectifyNoticesCode.Text = RectifyNotices.RectifyNoticesCode;
                    this.txtCompleteDate.Text = RectifyNotices.CompleteDate.ToString();
                    if (!string.IsNullOrEmpty(RectifyNotices.HiddenHazardType))
                    {
                        this.drpHiddenHazardType.SelectedValue = RectifyNotices.HiddenHazardType;
                    }
                    if (!string.IsNullOrEmpty(RectifyNotices.States))
                    {
                        State = RectifyNotices.States;
                    }
                    else
                    {
                        State = "0";
                        this.next1.Hidden = true;
                        this.next2.Hidden = true;
                        this.next3.Hidden = true;
                        this.next4.Hidden = true;
                    }
                    if (State == "0") {
                        this.next1.Hidden = true;
                        this.next2.Hidden = true;
                        this.next3.Hidden = true;
                        this.next4.Hidden = true;
                        
                    }
                    if (State == "1")
                    {
                        this.next.Hidden = true;
                        this.next2.Hidden = true;
                        this.next3.Hidden = true;
                        this.next4.Hidden = true;
                        this.Itemcontent.Hidden = true;
                        this.btnSure.Hidden = true;
                        BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpDutyPerson, this.CurrUser.LoginProjectId, this.drpUnitId.SelectedValue, true);//接收人
                        BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpProfessionalEngineer, this.CurrUser.LoginProjectId, BLL.CommonService.GetIsThisUnitId(), true);//专业工程师
                        BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpConstructionManager, this.CurrUser.LoginProjectId, BLL.CommonService.GetIsThisUnitId(), true);//施工经理
                        BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpProjectManager, this.CurrUser.LoginProjectId, BLL.CommonService.GetIsThisUnitId(), true);//项目经理

                    }
                    else if (State == "2")
                    {
                        this.next.Hidden = true;
                        this.next1.Hidden = true;
                        this.next3.Hidden = true;
                        this.next4.Hidden = true;
                        this.after.Hidden = false;
                        this.txtWrongContent.Readonly = true;
                        this.txtRequirement.Readonly = true;
                        this.txtLimitTime.Readonly = true;
                        BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpUnitHeadManId, this.CurrUser.LoginProjectId, this.drpUnitId.SelectedValue, true);//施工单位负责人
                    }
                    else if (State == "3")
                    {
                        this.next.Hidden = true;
                        this.next1.Hidden = true;
                        this.next2.Hidden = true;
                        this.next4.Hidden = true;
                        this.Itemcontent.Hidden = true;
                        this.btnSure.Hidden = true;
                        BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpSignPerson1, this.CurrUser.LoginProjectId, this.drpUnitId.SelectedValue, true);//复查人
                    }
                    else if (State == "4")
                    {
                        this.next.Hidden = true;
                        this.next1.Hidden = true;
                        this.next2.Hidden = true;
                        this.next3.Hidden = true;
                        this.end.Hidden = false;
                        this.after.Hidden = false;
                        this.txtWrongContent.Readonly = true;
                        this.txtRequirement.Readonly = true;
                        this.txtLimitTime.Readonly = true;
                        this.txtRectifyResults.Readonly = true;
                        Funs.FineUIPleaseSelect(drpIsRectify); 
                    }
                    else if(State=="5")
                    {
                        this.next.Hidden = true;
                        this.next1.Hidden = true;
                        this.next2.Hidden = true;
                        this.next3.Hidden = true;
                        this.Itemcontent.Hidden = true;
                    }
                }
                else
                {
                    this.txtCompleteDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    State = "0";
                    this.next1.Hidden = true;
                    this.next2.Hidden = true;
                    this.next3.Hidden = true;
                    this.next3.Hidden = true;
                    this.next4.Hidden = true;
                }

                BindGrid();
            }
        }

        public void BindGrid()
        {
            if (!string.IsNullOrEmpty(RectifyNoticesId))
            {
                string strSql = @"select RectifyNoticesItemId, RectifyNoticesId, WrongContent, Requirement, LimitTime, RectifyResults, IsRectify  from [dbo].[Check_RectifyNoticesItem] ";
                List<SqlParameter> listStr = new List<SqlParameter>();
                strSql += "where RectifyNoticesId = @RectifyNoticesId";
                listStr.Add(new SqlParameter("@RectifyNoticesId", RectifyNoticesId));
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                var table = this.GetPagedDataTable(Grid1, tb);
                Grid1.DataSource = table;
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
            }
        }

        protected void btnSure_Click(object sender, EventArgs e)
        {
            var getViewList = this.CollectGridInfo();
            getViewList = getViewList.Where(x => x.RectifyNoticesItemId != this.hdTestPlanTrainingId.Text).ToList();
            bool IsRectify = true;
            if (this.drpIsRectify.SelectedValue != BLL.Const._Null) {
                if (this.drpIsRectify.SelectedValue == "true") {
                    IsRectify = true;
                }
                else {
                    IsRectify = false;
                }
            }
            Model.Check_RectifyNoticesItem newView = new Model.Check_RectifyNoticesItem
            {
                RectifyNoticesItemId = this.hdTestPlanTrainingId.Text.ToString() == "" ? SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesItem)) : this.hdTestPlanTrainingId.Text,
                RectifyNoticesId = this.hdRectifyNoticesId.Text.ToString() == "" ? SQLHelper.GetNewID(typeof(Model.Check_RectifyNotices)) : this.hdRectifyNoticesId.Text.ToString(),
                WrongContent = txtWrongContent.Text,
                Requirement = txtRequirement.Text,
                LimitTime = Convert.ToDateTime(txtLimitTime.Text.Trim()),
                RectifyResults = txtRectifyResults.Text,
                IsRectify=IsRectify
            };
            getViewList.Add(newView);
            this.Grid1.DataSource = getViewList;
            this.Grid1.DataBind();
            this.InitText();
            this.hdRectifyNoticesId.Text = newView.RectifyNoticesId;
        }

        #region 收集页面信息
        /// <summary>
        ///  收集页面信息
        /// </summary>
        /// <returns></returns>
        private List<Model.Check_RectifyNoticesItem> CollectGridInfo()
        {
            List<Model.Check_RectifyNoticesItem> getViewList = new List<Model.Check_RectifyNoticesItem>();
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                var str = Grid1.Rows[i].DataKeys[1] + "";
                if (str.IndexOf("True") > -1)
                {
                    Model.Check_RectifyNoticesItem newView = new Model.Check_RectifyNoticesItem
                    {
                        RectifyNoticesItemId = Grid1.Rows[i].DataKeys[0].ToString(),
                        RectifyNoticesId = this.RectifyNoticesId,
                        WrongContent = Grid1.Rows[i].Values[0].ToString(),
                        Requirement = Grid1.Rows[i].Values[1].ToString(),
                        LimitTime = Convert.ToDateTime(Grid1.Rows[i].Values[2].ToString()),
                        RectifyResults = Grid1.Rows[i].Values[3].ToString(),
                        IsRectify = true
                    };

                    getViewList.Add(newView);
                }
                else if (str.IndexOf("False") > -1)
                {
                    Model.Check_RectifyNoticesItem newView = new Model.Check_RectifyNoticesItem
                    {
                        RectifyNoticesItemId = Grid1.Rows[i].DataKeys[0].ToString(),
                        RectifyNoticesId = this.RectifyNoticesId,
                        WrongContent = Grid1.Rows[i].Values[0].ToString(),
                        Requirement = Grid1.Rows[i].Values[1].ToString(),
                        LimitTime = Convert.ToDateTime(Grid1.Rows[i].Values[2].ToString()),
                        RectifyResults = Grid1.Rows[i].Values[3].ToString(),
                        IsRectify = false
                    };

                    getViewList.Add(newView);
                }
                else
                {
                    Model.Check_RectifyNoticesItem newView = new Model.Check_RectifyNoticesItem
                    {
                        RectifyNoticesItemId = Grid1.Rows[i].DataKeys[0].ToString(),
                        RectifyNoticesId = this.RectifyNoticesId,
                        WrongContent = Grid1.Rows[i].Values[0].ToString(),
                        Requirement = Grid1.Rows[i].Values[1].ToString(),
                        LimitTime = Funs.GetNewDateTime(Grid1.Rows[i].Values[2].ToString()),
                        RectifyResults = Grid1.Rows[i].Values[3].ToString()
                    };

                    getViewList.Add(newView);
                }
            }

            return getViewList;
        }
        #endregion

        #region 页面清空
        /// <summary>
        /// 页面清空
        /// </summary>
        private void InitText()
        {
            this.hdTestPlanTrainingId.Text = string.Empty;
            this.txtWrongContent.Text = string.Empty;
            this.txtRequirement.Text = string.Empty;
            this.txtLimitTime.Text = string.Empty;
            txtRectifyResults.Text = string.Empty;
            this.drpIsRectify.SelectedIndex = 0;
        }
        #endregion

        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.hdTestPlanTrainingId.Text))   //新增记录
            {
                this.hdTestPlanTrainingId.Text = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesItem));
            }
            if (State == "2" || State == "4")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotices&menuId={1}&type=-1", this.hdTestPlanTrainingId.Text + "#2", BLL.Const.ProjectRectifyNoticesMenuId)));
            }
            else
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotices&menuId={1}&type=0", this.hdTestPlanTrainingId.Text + "#2", BLL.Const.ProjectRectifyNoticesMenuId)));
            }            
        }
        protected void btnAttachUrlafter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.hdTestPlanTrainingId.Text))   //新增记录
            {
                this.hdTestPlanTrainingId.Text = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesItem));
            }
            if (State == "4")
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotices&menuId={1}&type=-1", this.hdTestPlanTrainingId.Text +"#1", BLL.Const.ProjectRectifyNoticesMenuId)));
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/RectifyNotices&menuId={1}&type=0", this.hdTestPlanTrainingId.Text + "#1", BLL.Const.ProjectRectifyNoticesMenuId)));
        }

        #region 修改
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            EditData();
        }
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            EditData();
        }
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInParent("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            var getViewList = this.CollectGridInfo();
            var item = getViewList.FirstOrDefault(x => x.RectifyNoticesItemId == Grid1.SelectedRowID);
            if (item != null)
            {
                this.hdTestPlanTrainingId.Text = item.RectifyNoticesItemId;
                this.txtRequirement.Text = item.Requirement.ToString();
                this.txtWrongContent.Text = item.WrongContent.ToString();
                this.txtLimitTime.Text = item.LimitTime.ToString();
                this.txtRectifyResults.Text = item.RectifyResults.ToString();
                if (item.IsRectify != null) {
                    if(item.IsRectify==true)
                    this.drpIsRectify.SelectedValue ="true";
                    else
                        this.drpIsRectify.SelectedValue = "false";
                }
            }
        }
        #endregion

        #region 删除
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                var getViewList = this.CollectGridInfo();
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var item = getViewList.FirstOrDefault(x => x.RectifyNoticesItemId == rowID);
                    if (item != null)
                    {
                        getViewList.Remove(item);
                    }
                }

                this.Grid1.DataSource = getViewList;
                this.Grid1.DataBind();
            }
        }




        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
                SavePauseNotice("save");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
                SavePauseNotice("submit");
        }
        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="saveType"></param>
        private void SavePauseNotice(string saveType)
        {
            Model.Check_RectifyNotices Notices = new Model.Check_RectifyNotices();
            Notices.RectifyNoticesCode = this.txtRectifyNoticesCode.Text.Trim();
            Notices.ProjectId = this.CurrUser.LoginProjectId;
            if (this.drpUnitId.SelectedValue != BLL.Const._Null)
            {
                Notices.UnitId = this.drpUnitId.SelectedValue;
            }
            if (this.drpWorkAreaId.SelectedValue != BLL.Const._Null)
            {
                Notices.WorkAreaId = this.drpWorkAreaId.SelectedValue;
            }
            if (this.drpCheckPerson.SelectedValue != BLL.Const._Null)
            {
               string str= GetStringByArray(this.drpCheckPerson.SelectedValueArray);
                string UserName = string.Empty;
                if (!string.IsNullOrEmpty(str))
                {
                    string[] seeUsers = str.Split(',');
                    foreach (var seeUser in seeUsers)
                    {
                        if (!string.IsNullOrEmpty(seeUser))
                        {
                            UserName+= BLL.UserService.getUserNamesUserIds(seeUser)+",";
                        }
                    }
                    if (!string.IsNullOrEmpty(UserName))
                    {
                        UserName = UserName.Substring(0, UserName.LastIndexOf(","));
                    }
                }
                Notices.CheckManNames =UserName;
                Notices.CheckManIds = str;
            }
            if (this.drpSignPerson.SelectedValue != BLL.Const._Null)
            {
                Notices.SignPerson = this.drpSignPerson.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.txtCompleteDate.Text.Trim()))
            {
                Notices.CheckedDate = Convert.ToDateTime(this.txtCompleteDate.Text.Trim());
            }
            if (this.drpHiddenHazardType.SelectedValue != BLL.Const._Null)
            {
                Notices.HiddenHazardType = this.drpHiddenHazardType.SelectedValue;
            }
            if (saveType == "submit")
            {
                Notices.States = Convert.ToInt32(Convert.ToInt32(State) + 1).ToString();
            }
            else
            {
                var isUpdate = Funs.DB.Check_RectifyNotices.FirstOrDefault(x => x.RectifyNoticesId == RectifyNoticesId);
                if (isUpdate != null)
                {
                    if (string.IsNullOrEmpty(Notices.States))
                    {
                        Notices.States = State;
                    }
                    else
                    {
                        Notices.States = isUpdate.States;
                    }
                }
                else
                {
                    Notices.States = State;
                }
            }
            if (!string.IsNullOrEmpty(RectifyNoticesId))
            {
                Model.Check_RectifyNotices isUpdate = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);
                if (Notices.States == "0" || Notices.States == "1")  ////编制人 修改或提交
                {
                    isUpdate.UnitId = this.drpUnitId.SelectedValue;
                    isUpdate.WorkAreaId = this.drpWorkAreaId.SelectedValue;
                    isUpdate.CheckManNames = BLL.UserService.GetUserNameByUserId(this.CurrUser.UserId);
                    isUpdate.CheckManIds = this.CurrUser.UserId;
                    isUpdate.CheckedDate = Funs.GetNewDateTime(this.txtCompleteDate.Text.Trim());
                    isUpdate.HiddenHazardType = this.drpHiddenHazardType.SelectedValue;
                    if (Notices.States == "1" && !string.IsNullOrEmpty(Notices.SignPerson))
                    {
                        isUpdate.SignPerson = Notices.SignPerson;
                        isUpdate.States = "1";
                        
                    }
                    BLL.Funs.DB.SubmitChanges();
                    Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesFlowOperate)),
                        RectifyNoticesId = isUpdate.RectifyNoticesId,
                        OperateName = "检查人员下发整改单",
                        OperateManId =CurrUser.UserId,
                        OperateTime = DateTime.Now,

                    };
                    BLL.Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                    BLL.Funs.DB.SubmitChanges();
                }
                else if (Notices.States == "2") ////总包单位项目安全经理 审核
                {
                    /// 不同意 打回 同意抄送专业工程师、施工经理、项目经理 并下发分包接收人（也就是施工单位项目安全经理）
                    if (this.rdbIsAgree.SelectedValue.Equals("false"))
                    {
                        Notices.States = isUpdate.States = "0";

                    }
                    else
                    {

                        if (this.drpProfessionalEngineer.SelectedValue != BLL.Const._Null)
                        {
                            isUpdate.ProfessionalEngineerId = this.drpProfessionalEngineer.SelectedValue;
                        }
                        if (this.drpConstructionManager.SelectedValue != BLL.Const._Null)
                        {
                            isUpdate.ConstructionManagerId = this.drpConstructionManager.SelectedValue;
                        }
                        if (this.drpProjectManager.SelectedValue != BLL.Const._Null)
                        {
                            isUpdate.ProjectManagerId = this.drpProjectManager.SelectedValue;
                        }
                        if (this.drpDutyPerson.SelectedValue != BLL.Const._Null)
                        {
                            isUpdate.DutyPersonId = this.drpDutyPerson.SelectedValue;
                            isUpdate.DutyPerson = this.drpDutyPerson.SelectedValue;
                            isUpdate.SignDate = DateTime.Now;
                            isUpdate.DutyPersonTime = DateTime.Now;
                        }
                        Notices.States = isUpdate.States = "2";
                        BLL.Funs.DB.SubmitChanges();
                        
                    }
                    Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesFlowOperate)),
                        RectifyNoticesId = isUpdate.RectifyNoticesId,
                        OperateName = "总包单位项目安全经理签发",
                        OperateManId = CurrUser.UserId,
                        Opinion=reason.Text,
                        IsAgree=Convert.ToBoolean(this.rdbIsAgree.SelectedValue),
                        OperateTime = DateTime.Now,

                    };
                    BLL.Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                    BLL.Funs.DB.SubmitChanges();

                }
                else if (Notices.States == "3") /// 施工单位项目安全经理 整改 提交施工单位项目负责人
                {
                    //// 整改明细反馈
                    var getViewList = this.CollectGridInfo();
                    if (getViewList != null && getViewList.Count() > 0)
                    {
                        foreach (var rItem in getViewList)
                        {
                            var getUpdateItem = Funs.DB.Check_RectifyNoticesItem.FirstOrDefault(x => x.RectifyNoticesItemId == rItem.RectifyNoticesItemId);
                            if (getUpdateItem != null)
                            {
                                getUpdateItem.RectifyResults = rItem.RectifyResults;
                                //if (getUpdateItem.IsRectify != true)
                                //{
                                //    getUpdateItem.IsRectify = null;
                                //}
                                Funs.DB.SubmitChanges();
                            }
                        }
                    }

                    if (this.drpUnitHeadManId.SelectedValue != BLL.Const._Null)
                    {
                        isUpdate.UnitHeadManId = this.drpUnitHeadManId.SelectedValue;
                        isUpdate.CompleteDate = DateTime.Now;
                        Notices.States = isUpdate.States = "3";
                    }
                    Funs.DB.SubmitChanges();
                    Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesFlowOperate)),
                        RectifyNoticesId = isUpdate.RectifyNoticesId,
                        OperateName = "责任人整改",
                        OperateManId = CurrUser.UserId,
                        OperateTime = DateTime.Now,

                    };
                    BLL.Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                    BLL.Funs.DB.SubmitChanges();

                }
                else if (Notices.States == "4")
                { /// 施工单位项目负责人不同意 打回施工单位项目安全经理,同意提交安全经理/安全工程师复查
                    if (this.rdbUnitHeadManAgree.SelectedValue.Equals("false"))
                    {
                        Notices.States = isUpdate.States = "2";
                        isUpdate.CompleteDate = null;
                    }
                    else
                    {
                        if (drpSignPerson1.SelectedValue != BLL.Const._Null)
                        {
                            isUpdate.UnitHeadManDate = DateTime.Now;
                            isUpdate.CheckPerson = drpSignPerson1.SelectedValue;
                            Notices.States = isUpdate.States = "4";
                        }
                        Funs.DB.SubmitChanges();
                       
                    }
                    Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                    {
                        FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesFlowOperate)),
                        RectifyNoticesId = isUpdate.RectifyNoticesId,
                        OperateName = "施工单位项目负责人审核",
                        OperateManId = CurrUser.UserId,
                        Opinion=this.reason1.Text,
                        IsAgree = Convert.ToBoolean(this.rdbUnitHeadManAgree.SelectedValue),
                        OperateTime = DateTime.Now,

                    };
                    BLL.Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                    BLL.Funs.DB.SubmitChanges();
                }
                else if (Notices.States == "5")
                {  ////安全经理/安全工程师 同意关闭，不同意打回施工单位项目安全经理
                    isUpdate.ReCheckOpinion = drpReCheckOpinion.SelectedValue;
                    if (this.drpReCheckOpinion.SelectedValue.Equals("false"))
                    {
                        Notices.States = isUpdate.States = "2";
                        isUpdate.UnitHeadManDate = null;
                        isUpdate.CompleteDate = null;
                        isUpdate.ProfessionalEngineerTime2 = null;
                        isUpdate.ConstructionManagerTime2 = null;
                        isUpdate.ProjectManagerTime2 = null;
                    }
                    else
                    {
                        isUpdate.ReCheckDate = DateTime.Now;
                        Notices.States = isUpdate.States = "5";
                    }
                    Funs.DB.SubmitChanges();
                    //// 整改明细反馈
                    var getViewList = this.CollectGridInfo();
                    if (getViewList != null && getViewList.Count() > 0)
                    {
                        foreach (var rItem in getViewList)
                        {
                            var getUpdateItem = Funs.DB.Check_RectifyNoticesItem.FirstOrDefault(x => x.RectifyNoticesItemId == rItem.RectifyNoticesItemId);
                            if (getUpdateItem != null)
                            {
                                if (this.drpReCheckOpinion.SelectedValue.Equals("false"))
                                {
                                    getUpdateItem.IsRectify = false;
                                }
                                else {
                                    getUpdateItem.IsRectify = true;
                                }
                                    
                                Funs.DB.SubmitChanges();
                            }
                        }
                    }
                    bool flag = false;
                    if (this.drpReCheckOpinion.SelectedValue == "整改通过") {
                        flag = true;
                    }
                    Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                        {
                            FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesFlowOperate)),
                            RectifyNoticesId = isUpdate.RectifyNoticesId,
                            OperateName = "总包单位安全经理/安全工程师复查",
                            OperateManId = CurrUser.UserId,
                            Opinion=this.drpReCheckOpinion.SelectedValue,
                            IsAgree=flag,
                            OperateTime = DateTime.Now,

                        };
                        BLL.Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                        BLL.Funs.DB.SubmitChanges();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.hdRectifyNoticesId.Text))
                {
                    Notices.RectifyNoticesId = this.hdRectifyNoticesId.Text;
                }
                else
                {
                    Notices.RectifyNoticesId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNotices));
                }
                Notices.CompleteManId = this.CurrUser.UserId;
                Notices.CompleteDate = DateTime.Now;
                Notices.Isprint = "0";
                Notices.Isprintf = "0";
                Notices.SignPerson = this.drpSignPerson.SelectedValue;
               RectifyNoticesService.AddRectifyNotices(Notices);
                RectifyNoticesId = Notices.RectifyNoticesId;
                Model.Check_RectifyNotices Notices1 = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);

                Model.Check_RectifyNoticesFlowOperate newOItem = new Model.Check_RectifyNoticesFlowOperate
                {
                    FlowOperateId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNoticesFlowOperate)),
                    RectifyNoticesId = Notices1.RectifyNoticesId,
                    OperateName = "检查人员下发整改单",
                    OperateManId =this.CurrUser.UserId,
                    OperateTime = DateTime.Now,

                };
                BLL.Funs.DB.Check_RectifyNoticesFlowOperate.InsertOnSubmit(newOItem);
                BLL.Funs.DB.SubmitChanges();

                var getViewList = this.CollectGridInfo();
                var getRectifyNoticesItem = from x in getViewList
                                            select new Model.Check_RectifyNoticesItem
                                            {
                                                RectifyNoticesItemId = x.RectifyNoticesItemId,
                                                RectifyNoticesId = Notices1.RectifyNoticesId,
                                                WrongContent = x.WrongContent,
                                                Requirement = x.Requirement,
                                                LimitTime = x.LimitTime,
                                            };
                if (getRectifyNoticesItem.Count() > 0)
                {
                    Funs.DB.Check_RectifyNoticesItem.InsertAllOnSubmit(getRectifyNoticesItem);
                    Funs.DB.SubmitChanges();
                }
            }

            ShowNotify("提交成功！", MessageBoxIcon.Success);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void rdbIsAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdbIsAgree.SelectedValue.Contains("false")) {
                this.reason.Hidden = false;
                this.step1_person.Hidden = true;
                this.step1_person2.Hidden = true;
            }
        }

        protected void rdbUnitHeadManAgree_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdbUnitHeadManAgree.SelectedValue.Contains("false"))
            {
                this.reason1.Hidden = false;
                this.step3_CheckPerson.Hidden = true;
            }
            else {
                this.reason1.Hidden = true;
                this.step3_CheckPerson.Hidden = false;
            }
        }

        protected void drpIsRectify_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpIsRectify.SelectedValue != BLL.Const._Null) {
                if (drpIsRectify.SelectedValue == "true") {
                    this.drpReCheckOpinion.SelectedValue = "整改通过";
                }
                else {
                    this.drpReCheckOpinion.SelectedValue = "整改不通过";
                }
            }
        }

        protected void drpCheckPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] array = this.drpCheckPerson.SelectedValueArray;
            List<string> str = new List<string>();
            foreach (var item in array)
            {
                if (item != BLL.Const._Null)
                {
                    str.Add(item);
                }

            }
            this.drpCheckPerson.SelectedValueArray = str.ToArray();
        }
        private string GetStringByArray(string[] array)
        {
            string str = string.Empty;
            foreach (var item in array)
            {
                if (item != BLL.Const._Null)
                {
                    str += item + ",";
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }
        protected string ConvertIsRectify(object flag)
        {
            
            if (flag != null)
            {
                if (flag.ToString() == "True") {
                    return "合格";
                }
                else if (flag.ToString() == "False")
                {
                    
                    return "不合格";
                }
            }
                return "";
        }
    }
}