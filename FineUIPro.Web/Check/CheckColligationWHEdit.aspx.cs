using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckColligationWHEdit :PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckColligationId
        {
            get
            {
                return (string)ViewState["CheckColligationId"];
            }
            set
            {
                ViewState["CheckColligationId"] = value;
            }
        }
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
        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.View_Check_CheckColligationDetail> checkColligationDetails = new List<Model.View_Check_CheckColligationDetail>();
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.InitDropDownList();
                checkColligationDetails.Clear();

                this.CheckColligationId = Request.Params["CheckColligationId"];
                var checkColligation = BLL.Check_CheckColligationService.GetCheckColligationByCheckColligationId(this.CheckColligationId);
                if (checkColligation != null)
                {
                    this.ProjectId = checkColligation.ProjectId;
                    if (this.ProjectId != this.CurrUser.LoginProjectId)
                    {
                        this.InitDropDownList();
                    }
                    this.txtCheckColligationCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CheckColligationId);
                    if (checkColligation.CheckTime != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkColligation.CheckTime);
                    }
                    if (!string.IsNullOrEmpty(checkColligation.CheckType))
                    {
                        this.drpCheckType.SelectedValue = checkColligation.CheckType;
                    }
                    if (!string.IsNullOrEmpty(checkColligation.PartInUnits))
                    {
                        this.drpUnit.SelectedValueArray = checkColligation.PartInUnits.Split(',');
                    }
                    if (!string.IsNullOrEmpty(checkColligation.CheckPerson))
                    {
                        this.drpCheckPerson.SelectedValue = checkColligation.CheckPerson;
                    }
                    if (!string.IsNullOrEmpty(checkColligation.PartInPersonIds))
                    {
                        this.drpPartInPersons.SelectedValueArray = checkColligation.PartInPersonIds.Split(',');
                    }
                    this.txtPartInPersonNames.Text = checkColligation.PartInPersonNames;
                    //this.txtDaySummary.Text = HttpUtility.HtmlDecode(checkColligation.DaySummary);
                    checkColligationDetails = (from x in Funs.DB.View_Check_CheckColligationDetail where x.CheckColligationId == this.CheckColligationId orderby x.CheckItem select x).ToList();
                }
                else
                {
                    ////自动生成编码
                    this.txtCheckColligationCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectCheckColligationWHMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    //this.txtDaySummary.Text = HttpUtility.HtmlDecode("其他情况日小结");
                }
                Grid1.DataSource = checkColligationDetails;
                Grid1.DataBind();
                //SetColor();

                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCheckColligationWHMenuId;
                this.ctlAuditFlow.DataId = this.CheckColligationId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            //参与单位           
            BLL.UnitService.InitUnitDropDownList(this.drpUnit, this.ProjectId, false);
            this.drpUnit.SelectedValue = this.CurrUser.UnitId;
            //检查组长
            BLL.UserService.InitUserDropDownList(this.drpCheckPerson, this.ProjectId, true);
            //检查组成员
            BLL.UserService.InitUserDropDownList(this.drpPartInPersons, this.ProjectId, true);
        }
        #endregion

        #region 保存数据
        private void SaveNew()
        {
            if (string.IsNullOrEmpty(this.CheckColligationId))
            {
                Model.Check_CheckColligation checkColligation = new Model.Check_CheckColligation
                {
                    CheckColligationId = SQLHelper.GetNewID(typeof(Model.Check_CheckColligation)),
                    CheckColligationCode = this.txtCheckColligationCode.Text.Trim(),
                    ProjectId = this.ProjectId
                };
                if (this.drpCheckType.SelectedValue != BLL.Const._Null)
                {
                    checkColligation.CheckType = this.drpCheckType.SelectedValue;
                }
                //参与单位
                string unitIds = string.Empty;
                foreach (var item in this.drpUnit.SelectedValueArray)
                {
                    unitIds += item + ",";
                }
                if (!string.IsNullOrEmpty(unitIds))
                {
                    unitIds = unitIds.Substring(0, unitIds.LastIndexOf(","));
                }
                checkColligation.PartInUnits = unitIds;
                if (this.drpCheckPerson.SelectedValue != BLL.Const._Null)
                {
                    checkColligation.CheckPerson = this.drpCheckPerson.SelectedValue;
                }

                ///组成员
                string partInPersonIds = string.Empty;
                string partInPersons = string.Empty;
                foreach (var item in this.drpPartInPersons.SelectedValueArray)
                {
                    var user = BLL.UserService.GetUserByUserId(item);
                    if (user != null)
                    {
                        partInPersonIds += user.UserId + ",";
                        partInPersons += user.UserName + ",";
                    }
                }
                if (!string.IsNullOrEmpty(partInPersonIds))
                {
                    checkColligation.PartInPersonIds = partInPersonIds.Substring(0, partInPersonIds.LastIndexOf(","));
                    checkColligation.PartInPersons = partInPersons.Substring(0, partInPersons.LastIndexOf(","));
                }
                checkColligation.PartInPersonNames = this.txtPartInPersonNames.Text.Trim();

                checkColligation.CheckTime = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
                //checkColligation.DaySummary = HttpUtility.HtmlEncode(this.txtDaySummary.Text.Trim());
                ////单据状态
                checkColligation.States = BLL.Const.State_0;
                this.CheckColligationId = checkColligation.CheckColligationId;
                checkColligation.CompileMan = this.CurrUser.UserId;
                BLL.Check_CheckColligationService.AddCheckColligation(checkColligation);
                BLL.LogService.AddSys_Log(this.CurrUser, checkColligation.CheckColligationCode, checkColligation.CheckColligationId, BLL.Const.ProjectCheckColligationWHMenuId, BLL.Const.BtnAdd);
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Check_CheckColligation checkColligation = new Model.Check_CheckColligation
            {
                CheckColligationCode = this.txtCheckColligationCode.Text.Trim(),
                ProjectId = this.ProjectId
            };
            if (this.drpCheckType.SelectedValue != BLL.Const._Null)
            {
                checkColligation.CheckType = this.drpCheckType.SelectedValue;
            }
            //参与单位
            string unitIds = string.Empty;
            foreach (var item in this.drpUnit.SelectedValueArray)
            {
                unitIds += item + ",";
            }
            if (!string.IsNullOrEmpty(unitIds))
            {
                unitIds = unitIds.Substring(0, unitIds.LastIndexOf(","));
            }
            checkColligation.PartInUnits = unitIds;
            if (this.drpCheckPerson.SelectedValue != BLL.Const._Null)
            {
                checkColligation.CheckPerson = this.drpCheckPerson.SelectedValue;
            }
            ///组成员
            string partInPersonIds = string.Empty;
            string partInPersons = string.Empty;
            foreach (var item in this.drpPartInPersons.SelectedValueArray)
            {
                var user = BLL.UserService.GetUserByUserId(item);
                if (user != null)
                {
                    partInPersonIds += user.UserId + ",";
                    partInPersons += user.UserName + ",";
                }
            }
            if (!string.IsNullOrEmpty(partInPersonIds))
            {
                checkColligation.PartInPersonIds = partInPersonIds.Substring(0, partInPersonIds.LastIndexOf(","));
                checkColligation.PartInPersons = partInPersons.Substring(0, partInPersons.LastIndexOf(","));
            }
            checkColligation.PartInPersonNames = this.txtPartInPersonNames.Text.Trim();
            checkColligation.CheckTime = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
            //checkColligation.DaySummary = HttpUtility.HtmlEncode(this.txtDaySummary.Text.Trim());
            ////单据状态
            checkColligation.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                checkColligation.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.CheckColligationId))
            {
                checkColligation.CheckColligationId = this.CheckColligationId;
                BLL.Check_CheckColligationService.UpdateCheckColligation(checkColligation);
                BLL.LogService.AddSys_Log(this.CurrUser, checkColligation.CheckColligationCode, checkColligation.CheckColligationId, BLL.Const.ProjectCheckColligationWHMenuId, BLL.Const.BtnModify);
            }
            else
            {
                checkColligation.CheckColligationId = SQLHelper.GetNewID(typeof(Model.Check_CheckColligation));
                this.CheckColligationId = checkColligation.CheckColligationId;
                checkColligation.CompileMan = this.CurrUser.UserId;
                BLL.Check_CheckColligationService.AddCheckColligation(checkColligation);
                BLL.LogService.AddSys_Log(this.CurrUser, checkColligation.CheckColligationCode, checkColligation.CheckColligationId, BLL.Const.ProjectCheckColligationWHMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectCheckColligationWHMenuId, this.CheckColligationId, (type == BLL.Const.BtnSubmit ? true : false), this.txtCheckDate.Text.Trim(), "../Check/CheckColligationView.aspx?CheckColligationId={0}");
        }
        #endregion

        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //if (!IsAllFix())
            //{
            //    Alert.ShowInTop("请将检查项的不合格项描述补充完整！", MessageBoxIcon.Warning);
            //    return;
            //}
            if (this.ctlAuditFlow.NextStep == BLL.Const.State_1 && this.ctlAuditFlow.NextPerson == BLL.Const._Null)
            {
                Alert.ShowInTop("请选择下一步办理人！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSubmit);
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());
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
            //if (!IsAllFix())
            //{
            //    Alert.ShowInTop("请将检查项的不合格项描述补充完整！", MessageBoxIcon.Warning);
            //    return;
            //}
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 明细项是否全部填写内容
        /// <summary>
        /// 明细项是否全部填写内容
        /// </summary>
        /// <returns></returns>
        //private bool IsAllFix()
        //{
        //    bool isAllFix = true;
        //    if (!string.IsNullOrEmpty(this.CheckColligationId))
        //    {
        //        var details = BLL.Check_CheckColligationDetailService.GetCheckColligationDetailByCheckColligationId(this.CheckColligationId);
                
        //        foreach (var item in details)
        //        {
        //            if (string.IsNullOrEmpty(item.Unqualified))
        //            {
        //                isAllFix = false;
        //                break;
        //            }
        //        }
        //    }
        //    return isAllFix;
        //}
        #endregion
      
        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            checkColligationDetails = (from x in Funs.DB.View_Check_CheckColligationDetail where x.CheckColligationId == this.CheckColligationId orderby x.CheckItem select x).ToList();
            Grid1.DataSource = checkColligationDetails;
            Grid1.DataBind();
        }
        #endregion       

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuEdit_Click(null, null);
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string checkColligationDetailId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckColligationDetailWHEdit.aspx?CheckColligationDetailId={0}", checkColligationDetailId, "编辑 - ")));
        }
        #endregion

        #region 删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    var getV = BLL.Check_CheckColligationDetailService.GetCheckColligationDetailByCheckColligationDetailId(rowID);
                    if (getV != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, getV.CheckItem, getV.CheckColligationDetailId, BLL.Const.ProjectCheckColligationWHMenuId, BLL.Const.BtnDelete);
                        BLL.Check_CheckColligationDetailService.DeleteCheckColligationDetailById(rowID);
                    }
                }
                checkColligationDetails = (from x in Funs.DB.View_Check_CheckColligationDetail where x.CheckColligationId == this.CheckColligationId orderby x.CheckItem select x).ToList();
                Grid1.DataSource = checkColligationDetails;
                Grid1.DataBind();
                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
            }
        }
        #endregion

        #region 获取检查类型
        /// <summary>
        /// 获取检查类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertCheckItemType(object CheckItem)
        {
            return BLL.Check_ProjectCheckItemSetService.ConvertCheckItemType(CheckItem);
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CheckColligationId))
            {
                SaveNew();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckColligation&menuId={1}", this.CheckColligationId, BLL.Const.ProjectCheckColligationWHMenuId)));
        }
        #endregion

        #region 转换字符串
        /// <summary>
        /// 转换整改完成情况
        /// </summary>
        /// <param name="workStage"></param>
        /// <returns></returns>
        protected string ConvertCompleteStatus(object CompleteStatus)
        {
            //if (CompleteStatus != null)
            //{
            //    if (!string.IsNullOrEmpty(CompleteStatus.ToString()))
            //    {
            //        bool completeStatus = Convert.ToBoolean(CompleteStatus.ToString());
            //        if (completeStatus)
            //        {
            //            return "是";
            //        }
            //        else
            //        {
            //            return "否";
            //        }
            //    }
            //}
            return "";
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
            //string checkColligationDetailId = Grid1.DataKeys[e.RowIndex][0].ToString();
            //Model.Check_CheckColligationDetail detail = BLL.Check_CheckColligationDetailService.GetCheckColligationDetailByCheckColligationDetailId(checkColligationDetailId);
            //if (e.CommandName == "click")
            //{
            //    Model.Check_CheckColligationDetail newDetail = new Model.Check_CheckColligationDetail
            //    {
            //        CheckColligationDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckColligationDetail)),
            //        CheckColligationId = detail.CheckColligationId,
            //        CheckItem = detail.CheckItem,
            //        CheckContent = detail.CheckContent,
            //        Unqualified = "隐患",
            //        Suggestions = "整改",
            //        CompleteStatus = true
            //    };
            //    BLL.Check_CheckColligationDetailService.AddCheckColligationDetail(newDetail);
            //    checkColligationDetails = (from x in Funs.DB.View_Check_CheckColligationDetail where x.CheckColligationId == this.CheckColligationId orderby x.CheckItem select x).ToList();
            //    Grid1.DataSource = checkColligationDetails;
            //    Grid1.DataBind();
            //    SetColor();
            //}
        }
        #endregion   

        #region 增加明细
        /// <summary>
        /// 增加明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddDetail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CheckColligationId))
            {
                SaveNew();
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckColligationDetailWHEdit.aspx?CheckColligationId={0}", this.CheckColligationId, "编辑 - ")));
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 处理措施
        /// </summary>
        /// <param name="handleStep"></param>
        /// <returns></returns>
        protected string HandleStepStr(object handleStep)
        {
            if (handleStep != null)
            {
                string name = string.Empty;
                List<string> lists = handleStep.ToString().Split('|').ToList();
                foreach (var item in lists)
                {
                    Model.Sys_Const con = BLL.ConstValue.GetConstByConstValueAndGroupId(item, BLL.ConstValue.Group_HandleStep);
                    if (con != null)
                    {
                        name += con.ConstText + "|";
                    }
                }
                if (!string.IsNullOrEmpty(name))
                {
                    name = name.Substring(0, name.LastIndexOf('|'));
                }
                return name;
            }
            return null;
        }
        #endregion
    }
}