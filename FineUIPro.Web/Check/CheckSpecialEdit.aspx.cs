using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckSpecialEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckSpecialId
        {
            get
            {
                return (string)ViewState["CheckSpecialId"];
            }
            set
            {
                ViewState["CheckSpecialId"] = value;
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
        private static List<Model.View_Check_CheckSpecialDetail> checkSpecialDetails = new List<Model.View_Check_CheckSpecialDetail>();
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
                hdAttachUrl.Text = string.Empty;
                hdId.Text = string.Empty;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();

                this.ProjectId = this.CurrUser.LoginProjectId;
                this.InitDropDownList();
                this.CheckSpecialId = Request.Params["CheckSpecialId"];
                var checkSpecial = BLL.Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(this.CheckSpecialId);
                if (checkSpecial != null)
                {
                    this.ProjectId = checkSpecial.ProjectId;
                    if (this.ProjectId != this.CurrUser.LoginProjectId)
                    {
                        this.InitDropDownList();
                    }
                    this.txtCheckSpecialCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CheckSpecialId);
                    if (checkSpecial.CheckTime != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkSpecial.CheckTime);
                    }
                    if (!string.IsNullOrEmpty(checkSpecial.CheckType))
                    {
                        this.drpCheckType.SelectedValue = checkSpecial.CheckType;
                    }
                    if (!string.IsNullOrEmpty(checkSpecial.PartInUnits))
                    {
                        this.drpUnit.SelectedValueArray = checkSpecial.PartInUnits.Split(',');
                    }
                    if (!string.IsNullOrEmpty(checkSpecial.CheckPerson))
                    {
                        this.drpCheckPerson.SelectedValue = checkSpecial.CheckPerson;
                    }
                    //if (!string.IsNullOrEmpty(checkSpecial.CheckAreas))
                    //{
                    //    this.drpCheckAreas.SelectedValueArray = checkSpecial.CheckAreas.Split(',');
                    //}
                    if (!string.IsNullOrEmpty(checkSpecial.PartInPersonIds))
                    {
                        this.drpPartInPersons.SelectedValueArray = checkSpecial.PartInPersonIds.Split(',');
                    } 
                    this.txtDaySummary.Text = HttpUtility.HtmlDecode(checkSpecial.DaySummary);
                    checkSpecialDetails = (from x in Funs.DB.View_Check_CheckSpecialDetail where x.CheckSpecialId == this.CheckSpecialId orderby x.CheckItem select x).ToList();
                }
                else
                {
                    ////自动生成编码
                    this.txtCheckSpecialCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectCheckSpecialMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtDaySummary.Text = HttpUtility.HtmlDecode("其他情况日小结");
                }
                Grid1.DataSource = checkSpecialDetails;
                Grid1.DataBind();
                SetColor();

                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCheckSpecialMenuId;
                this.ctlAuditFlow.DataId = this.CheckSpecialId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
            }
        }
        #endregion

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
            //检查区域
            //BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpCheckAreas, this.ProjectId, false);
            checkSpecialDetails.Clear();
        }

        #region 生成隐患整改通知单按钮
        /// <summary>
        /// 生成隐患整改通知单按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateRectifyNotice_Click(object sender, EventArgs e)
        {
            if (this.Grid1.Rows.Count == 0)
            {
                Alert.ShowInTop("请先增加检查记录！", MessageBoxIcon.Warning);
                return;
            }
            int n = 0;
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                if (this.Grid1.Rows[i].Values[7].ToString() == "隐患整改通知单")
                {
                    n++;
                    break;
                }
            }
            if (n == 0)
            {
                Alert.ShowInTop("没有需要生成隐患整改通知单的检查项！", MessageBoxIcon.Warning);
                return;
            }
            string unitIds = string.Empty;
            string rectifyNoticeAndUnitIds = string.Empty;
            string rectifyNoticeCode = string.Empty;
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                if (this.Grid1.Rows[i].Values[7].ToString() == "隐患整改通知单")
                {
                    Model.Check_CheckSpecialDetail detail = BLL.Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialDetailId(this.Grid1.Rows[i].DataKeys[0].ToString());
                    if (string.IsNullOrEmpty(detail.RectifyNoticeId))
                    {
                        string unitId = this.Grid1.Rows[i].Values[10].ToString();
                        if (!unitIds.Contains(unitId))
                        {
                            Model.Check_RectifyNotice rectifyNotice = new Model.Check_RectifyNotice
                            {
                                RectifyNoticeId = SQLHelper.GetNewID(typeof(Model.Check_RectifyNotice)),
                                ProjectId = this.ProjectId,
                                UnitId = unitId,
                                RectifyNoticeCode = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectRectifyNoticeMenuId, this.ProjectId, unitId)
                            };
                            rectifyNotice.CheckArea += this.Grid1.Rows[i].Values[11].ToString();
                            BLL.Check_RectifyNoticeService.AddRectifyNotice(rectifyNotice);

                            Model.Check_RectifyNoticeDetail d = new Model.Check_RectifyNoticeDetail
                            {
                                RectifyNoticeDetailId = detail.CheckSpecialDetailId,
                                RectifyNoticeId = rectifyNotice.RectifyNoticeId,
                                CheckItem = detail.CheckItem,
                                CheckItemType = detail.CheckItemType,
                                Unqualified = detail.Unqualified,
                                CheckArea = detail.CheckArea,
                                UnitId = detail.UnitId,
                                Suggestions = detail.Suggestions,
                                CheckContent = detail.CheckContent
                            };
                            BLL.Check_RectifyNoticeDetailService.AddRectifyNoticeDetail(d);
                            unitIds += unitId + ",";
                            rectifyNoticeAndUnitIds += rectifyNotice.RectifyNoticeId + "," + unitId + "|";
                            detail.RectifyNoticeId = rectifyNotice.RectifyNoticeId;
                            BLL.Check_CheckSpecialDetailService.UpdateCheckSpecialDetail(detail);
                            if (string.IsNullOrEmpty(rectifyNoticeCode))
                            {
                                rectifyNoticeCode += rectifyNotice.RectifyNoticeCode;
                            }
                            else
                            {
                                rectifyNoticeCode += "," + rectifyNotice.RectifyNoticeCode;
                            }
                        }
                        else
                        {
                            string[] list = rectifyNoticeAndUnitIds.Split('|');
                            foreach (var item in list)
                            {
                                if (item.Contains(unitId))
                                {
                                    string rectifyNoticeId = item.Split(',')[0];
                                    Model.Check_RectifyNotice rectifyNotice = BLL.Check_RectifyNoticeService.GetRectifyNoticeByRectifyNoticeId(rectifyNoticeId);
                                    if (!rectifyNotice.CheckArea.Contains(this.Grid1.Rows[i].Values[11].ToString()))
                                    {
                                        rectifyNotice.CheckArea += "," + this.Grid1.Rows[i].Values[11].ToString();
                                    }
                                    BLL.Check_RectifyNoticeService.UpdateRectifyNotice(rectifyNotice);
                                    Model.Check_RectifyNoticeDetail d = new Model.Check_RectifyNoticeDetail
                                    {
                                        RectifyNoticeDetailId = detail.CheckSpecialDetailId,
                                        RectifyNoticeId = rectifyNoticeId,
                                        CheckItem = detail.CheckItem,
                                        CheckItemType = detail.CheckItemType,
                                        Unqualified = detail.Unqualified,
                                        CheckArea = detail.CheckArea,
                                        UnitId = detail.UnitId,
                                        Suggestions = detail.Suggestions,
                                        CheckContent = detail.CheckContent
                                    };
                                    BLL.Check_RectifyNoticeDetailService.AddRectifyNoticeDetail(d);
                                    detail.RectifyNoticeId = rectifyNoticeId;
                                    BLL.Check_CheckSpecialDetailService.UpdateCheckSpecialDetail(detail);
                                }
                            }
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(rectifyNoticeCode))
            {
                Alert.ShowInTop("已生成隐患整改通知单：" + rectifyNoticeCode + "！", MessageBoxIcon.Success);
            }
            else
            {
                Alert.ShowInTop("隐患整改通知单已存在，请到对应模块进行处理！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 选择按钮
        /// <summary>
        /// 选择按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CheckSpecialId))
            {
                SaveNew();
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShowCheckItem.aspx?CheckSpecialId={0}&checkType=2", this.CheckSpecialId, "编辑 - ")));
        }
        #endregion

        private void SaveNew()
        {
            if (string.IsNullOrEmpty(this.CheckSpecialId))
            {
                Model.Check_CheckSpecial checkSpecial = new Model.Check_CheckSpecial
                {
                    CheckSpecialId = SQLHelper.GetNewID(typeof(Model.Check_CheckSpecial)),
                    CheckSpecialCode = this.txtCheckSpecialCode.Text.Trim(),
                    ProjectId = this.ProjectId
                };
                if (this.drpCheckType.SelectedValue != BLL.Const._Null)
                {
                    checkSpecial.CheckType = this.drpCheckType.SelectedValue;
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
                checkSpecial.PartInUnits = unitIds;
                if (this.drpCheckPerson.SelectedValue != BLL.Const._Null)
                {
                    checkSpecial.CheckPerson = this.drpCheckPerson.SelectedValue;
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
                    checkSpecial.PartInPersonIds = partInPersonIds.Substring(0, partInPersonIds.LastIndexOf(","));
                    checkSpecial.PartInPersons = partInPersons.Substring(0, partInPersons.LastIndexOf(","));
                }
                
                checkSpecial.CheckTime = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
                checkSpecial.DaySummary = HttpUtility.HtmlEncode(this.txtDaySummary.Text.Trim());
                ////单据状态
                checkSpecial.States = BLL.Const.State_0;
                this.CheckSpecialId = checkSpecial.CheckSpecialId;
                checkSpecial.CompileMan = this.CurrUser.UserId;
                BLL.Check_CheckSpecialService.AddCheckSpecial(checkSpecial);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加专项检查", checkSpecial.CheckSpecialId);
            }
        }

        #region 提交按钮
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!IsAllFix())
            {
                Alert.ShowInTop("请将检查项的不合格项描述补充完整！", MessageBoxIcon.Warning);
                return;
            }
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
            if (!IsAllFix())
            {
                Alert.ShowInTop("请将检查项的不合格项描述补充完整！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(BLL.Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 明细项是否全部填写内容
        /// <summary>
        /// 明细项是否全部填写内容
        /// </summary>
        /// <returns></returns>
        private bool IsAllFix()
        {
            bool isAllFix = true;
            if (!string.IsNullOrEmpty(this.CheckSpecialId))
            {
                var details = BLL.Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialId(this.CheckSpecialId);
                //if (details.Count() > 0)
                //{
                    foreach (var item in details)
                    {
                        if (string.IsNullOrEmpty(item.Unqualified))
                        {
                            isAllFix = false;
                            break;
                        }
                    }
                //}
                //else
                //{
                //    isAllFix = false;
                //}
            }
            return isAllFix;
        }
        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="type"></param>
        private void SaveData(string type)
        {
            Model.Check_CheckSpecial checkSpecial = new Model.Check_CheckSpecial
            {
                CheckSpecialCode = this.txtCheckSpecialCode.Text.Trim(),
                ProjectId = this.ProjectId
            };
            if (this.drpCheckType.SelectedValue != BLL.Const._Null)
            {
                checkSpecial.CheckType = this.drpCheckType.SelectedValue;
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
            checkSpecial.PartInUnits = unitIds;
            if (this.drpCheckPerson.SelectedValue != BLL.Const._Null)
            {
                checkSpecial.CheckPerson = this.drpCheckPerson.SelectedValue;
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
                checkSpecial.PartInPersonIds = partInPersonIds.Substring(0, partInPersonIds.LastIndexOf(","));
                checkSpecial.PartInPersons = partInPersons.Substring(0, partInPersons.LastIndexOf(","));
            }
            //检查区域
            //string workAreaIds = string.Empty;
            //foreach (var item in this.drpCheckAreas.SelectedValueArray)
            //{
            //    workAreaIds += item + ",";
            //}
            //if (!string.IsNullOrEmpty(workAreaIds))
            //{
            //    workAreaIds = workAreaIds.Substring(0, workAreaIds.LastIndexOf(","));
            //}
            //checkSpecial.CheckAreas = workAreaIds;
            checkSpecial.CheckTime = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
            checkSpecial.DaySummary = HttpUtility.HtmlEncode(this.txtDaySummary.Text.Trim());
            ////单据状态
            checkSpecial.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                checkSpecial.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.CheckSpecialId))
            {
                checkSpecial.CheckSpecialId = this.CheckSpecialId;
                BLL.Check_CheckSpecialService.UpdateCheckSpecial(checkSpecial);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改专项检查", checkSpecial.CheckSpecialId);
            }
            else
            {
                checkSpecial.CheckSpecialId = SQLHelper.GetNewID(typeof(Model.Check_CheckSpecial));
                this.CheckSpecialId = checkSpecial.CheckSpecialId;
                checkSpecial.CompileMan = this.CurrUser.UserId;
                BLL.Check_CheckSpecialService.AddCheckSpecial(checkSpecial);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加专项检查", checkSpecial.CheckSpecialId);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectCheckSpecialMenuId, this.CheckSpecialId, (type == BLL.Const.BtnSubmit ? true : false), this.txtCheckDate.Text.Trim(), "../Check/CheckSpecialView.aspx?CheckSpecialId={0}");
        }

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            checkSpecialDetails = (from x in Funs.DB.View_Check_CheckSpecialDetail where x.CheckSpecialId == this.CheckSpecialId orderby x.CheckItem select x).ToList();
            Grid1.DataSource = checkSpecialDetails;
            Grid1.DataBind();
            SetColor();
        }
        #endregion

        private void SetColor()
        {
            for (int i = 0; i < this.Grid1.Rows.Count; i++)
            {
                if (this.Grid1.Rows[i].Values[5].ToString() == "")
                {
                    Grid1.Rows[i].CellCssClasses[5] = "red";
                }
                if (this.Grid1.Rows[i].Values[6].ToString() == "")
                {
                    Grid1.Rows[i].CellCssClasses[6] = "red";
                }
                if (this.Grid1.Rows[i].Values[7].ToString() == "")
                {
                    Grid1.Rows[i].CellCssClasses[7] = "red";
                }
            }
        }

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
            string checkSpecialDetailId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckSpecialDetailEdit.aspx?CheckSpecialDetailId={0}", checkSpecialDetailId, "编辑 - ")));

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
                    BLL.Check_CheckSpecialDetailService.DeleteCheckSpecialDetailById(rowID);
                }
                checkSpecialDetails = (from x in Funs.DB.View_Check_CheckSpecialDetail where x.CheckSpecialId == this.CheckSpecialId orderby x.CheckItem select x).ToList();
                Grid1.DataSource = checkSpecialDetails;
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
            if (string.IsNullOrEmpty(this.CheckSpecialId))
            {
                SaveNew();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckSpecial&menuId={1}", this.CheckSpecialId, BLL.Const.ProjectCheckSpecialMenuId)));
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
            if (CompleteStatus != null)
            {
                if (!string.IsNullOrEmpty(CompleteStatus.ToString()))
                {
                    bool completeStatus = Convert.ToBoolean(CompleteStatus.ToString());
                    if (completeStatus)
                    {
                        return "是";
                    }
                    else
                    {
                        return "否";
                    }
                }
            }
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
            string checkSpecialDetailId = Grid1.DataKeys[e.RowIndex][0].ToString();
            Model.Check_CheckSpecialDetail detail = BLL.Check_CheckSpecialDetailService.GetCheckSpecialDetailByCheckSpecialDetailId(checkSpecialDetailId);
            if (e.CommandName == "click")
            {
                Model.Check_CheckSpecialDetail newDetail = new Model.Check_CheckSpecialDetail
                {
                    CheckSpecialDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckSpecialDetail)),
                    CheckSpecialId = detail.CheckSpecialId,
                    CheckItem = detail.CheckItem,
                    CheckContent = detail.CheckContent,
                    Unqualified = "隐患",
                    Suggestions = "整改",
                    CompleteStatus = true
                };
                BLL.Check_CheckSpecialDetailService.AddCheckSpecialDetail(newDetail);
                checkSpecialDetails = (from x in Funs.DB.View_Check_CheckSpecialDetail where x.CheckSpecialId == this.CheckSpecialId orderby x.CheckItem select x).ToList();
                Grid1.DataSource = checkSpecialDetails;
                Grid1.DataBind();
                SetColor();
            }
        }
        #endregion
    }
}