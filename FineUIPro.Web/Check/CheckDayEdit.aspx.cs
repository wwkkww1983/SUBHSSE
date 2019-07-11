using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckDayEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckDayId
        {
            get
            {
                return (string)ViewState["CheckDayId"];
            }
            set
            {
                ViewState["CheckDayId"] = value;
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
        private static List<Model.View_Check_CheckDayDetail> checkDayDetails = new List<Model.View_Check_CheckDayDetail>();
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
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                BLL.UserService.InitUserDropDownList(this.drpCheckPerson, this.CurrUser.LoginProjectId, true);
                ///天气  
                BLL.ConstValue.InitConstValueDropDownList(this.drpWeather, ConstValue.Group_Weather, true);
                checkDayDetails.Clear();

                this.CheckDayId = Request.Params["CheckDayId"];
                var checkDay = BLL.Check_CheckDayService.GetCheckDayByCheckDayId(this.CheckDayId);
                if (checkDay != null)
                {
                    this.ProjectId = checkDay.ProjectId;
                    this.txtCheckDayCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CheckDayId);
                    if (!string.IsNullOrEmpty(checkDay.WeatherId))
                    {
                        this.drpWeather.SelectedValue = checkDay.WeatherId;
                    }
                    if (!string.IsNullOrEmpty(checkDay.CheckPerson))
                    {
                        this.drpCheckPerson.SelectedValue = checkDay.CheckPerson;
                    }

                    if (checkDay.CheckTime != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkDay.CheckTime);
                    }
                    this.txtDaySummary.Text = HttpUtility.HtmlDecode(checkDay.DaySummary);
                    checkDayDetails = (from x in Funs.DB.View_Check_CheckDayDetail where x.CheckDayId == this.CheckDayId orderby x.CheckItem select x).ToList();
                }
                else
                {
                    ////自动生成编码
                    this.txtCheckDayCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectCheckDayMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.drpCheckPerson.SelectedValue = this.CurrUser.UserId;
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtDaySummary.Text = HttpUtility.HtmlDecode("其他情况日小结");
                }
                Grid1.DataSource = checkDayDetails;
                Grid1.DataBind();
                SetColor();
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCheckDayMenuId;
                this.ctlAuditFlow.DataId = this.CheckDayId;
                this.ctlAuditFlow.ProjectId = this.ProjectId;
                this.ctlAuditFlow.UnitId = this.CurrUser.UnitId;
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
            if (string.IsNullOrEmpty(this.CheckDayId))
            {
                SaveNew();
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShowCheckItem.aspx?CheckDayId={0}&checkType=1", this.CheckDayId, "编辑 - ")));
        }
        #endregion

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
                    Model.Check_CheckDayDetail detail = BLL.Check_CheckDayDetailService.GetCheckDayDetailByCheckDayDetailId(this.Grid1.Rows[i].DataKeys[0].ToString());
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
                                RectifyNoticeDetailId = detail.CheckDayDetailId,
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
                            BLL.Check_CheckDayDetailService.UpdateCheckDayDetail(detail);
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
                                        RectifyNoticeDetailId = detail.CheckDayDetailId,
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
                                    BLL.Check_CheckDayDetailService.UpdateCheckDayDetail(detail);
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

        private void SaveNew()
        {
            if (string.IsNullOrEmpty(this.CheckDayId))
            {
                Model.Check_CheckDay checkDay = new Model.Check_CheckDay
                {
                    CheckDayId = SQLHelper.GetNewID(typeof(Model.Check_CheckDay)),
                    CheckDayCode = this.txtCheckDayCode.Text.Trim(),
                    ProjectId = this.ProjectId
                };
                if (this.drpWeather.SelectedValue != BLL.Const._Null)
                {
                    checkDay.WeatherId = this.drpWeather.SelectedValue;
                }
                if (this.drpCheckPerson.SelectedValue != BLL.Const._Null)
                {
                    checkDay.CheckPerson = this.drpCheckPerson.SelectedValue;
                }
                checkDay.CheckTime = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
                checkDay.DaySummary = HttpUtility.HtmlEncode(this.txtDaySummary.Text.Trim());
                ////单据状态
                checkDay.States = BLL.Const.State_0;
                this.CheckDayId = checkDay.CheckDayId;
                checkDay.CompileMan = this.CurrUser.UserId;
                BLL.Check_CheckDayService.AddCheckDay(checkDay);
                BLL.LogService.AddSys_Log(this.CurrUser, checkDay.CheckDayCode, checkDay.CheckDayId, BLL.Const.ProjectCheckDayMenuId, BLL.Const.BtnAdd);
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
            if (!string.IsNullOrEmpty(this.CheckDayId))
            {
                var details = BLL.Check_CheckDayDetailService.GetCheckDayDetailByCheckDayId(this.CheckDayId);
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
            Model.Check_CheckDay checkDay = new Model.Check_CheckDay
            {
                CheckDayCode = this.txtCheckDayCode.Text.Trim(),
                ProjectId = this.ProjectId
            };
            if (this.drpWeather.SelectedValue != BLL.Const._Null)
            {
                checkDay.WeatherId = this.drpWeather.SelectedValue;
            }
            if (this.drpCheckPerson.SelectedValue != BLL.Const._Null)
            {
                checkDay.CheckPerson = this.drpCheckPerson.SelectedValue;
            }
            checkDay.CheckTime = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
            checkDay.DaySummary = HttpUtility.HtmlEncode(this.txtDaySummary.Text.Trim());
            ////单据状态
            checkDay.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                checkDay.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.CheckDayId))
            {
                checkDay.CheckDayId = this.CheckDayId;
                BLL.Check_CheckDayService.UpdateCheckDay(checkDay);
                BLL.LogService.AddSys_Log(this.CurrUser, checkDay.CheckDayCode, checkDay.CheckDayId, BLL.Const.ProjectCheckDayMenuId, BLL.Const.BtnModify);
            }
            else
            {
                checkDay.CheckDayId = SQLHelper.GetNewID(typeof(Model.Check_CheckDay));
                this.CheckDayId = checkDay.CheckDayId;
                checkDay.CompileMan = this.CurrUser.UserId;
                BLL.Check_CheckDayService.AddCheckDay(checkDay);
                BLL.LogService.AddSys_Log(this.CurrUser, checkDay.CheckDayCode, checkDay.CheckDayId, BLL.Const.ProjectCheckDayMenuId, BLL.Const.BtnAdd);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectCheckDayMenuId, this.CheckDayId, (type == BLL.Const.BtnSubmit ? true : false), this.txtCheckDate.Text.Trim(), "../Check/CheckDayView.aspx?CheckDayId={0}");
        }

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            checkDayDetails = (from x in Funs.DB.View_Check_CheckDayDetail where x.CheckDayId == this.CheckDayId orderby x.CheckItem select x).ToList();
            Grid1.DataSource = checkDayDetails;
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
            string checkDayDetailId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckDayDetailEdit.aspx?CheckDayDetailId={0}", checkDayDetailId, "编辑 - ")));

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
                    BLL.Check_CheckDayDetailService.DeleteCheckDayDetailById(rowID);
                }
                checkDayDetails = (from x in Funs.DB.View_Check_CheckDayDetail where x.CheckDayId == this.CheckDayId orderby x.CheckItem select x).ToList();
                Grid1.DataSource = checkDayDetails;
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
            if (string.IsNullOrEmpty(this.CheckDayId))
            {
                SaveNew();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckDay&menuId={1}", this.CheckDayId, BLL.Const.ProjectCheckDayMenuId)));
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
            string checkDayDetailId = Grid1.DataKeys[e.RowIndex][0].ToString();
            Model.Check_CheckDayDetail detail = BLL.Check_CheckDayDetailService.GetCheckDayDetailByCheckDayDetailId(checkDayDetailId);
            if (e.CommandName == "click")
            {
                Model.Check_CheckDayDetail newDetail = new Model.Check_CheckDayDetail
                {
                    CheckDayDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckDayDetail)),
                    CheckDayId = detail.CheckDayId,
                    CheckItem = detail.CheckItem,
                    CheckContent = detail.CheckContent,
                    Unqualified = "隐患",
                    Suggestions = "整改",
                    CompleteStatus = true
                };
                BLL.Check_CheckDayDetailService.AddCheckDayDetail(newDetail);
                checkDayDetails = (from x in Funs.DB.View_Check_CheckDayDetail where x.CheckDayId == this.CheckDayId orderby x.CheckItem select x).ToList();
                Grid1.DataSource = checkDayDetails;
                Grid1.DataBind();
                SetColor();
            }
        }
        #endregion
        
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CheckDayId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckDayDetailIn.aspx?CheckDayId={0}", this.CheckDayId, "导入 - "), "导入", 1024, 560));
        }
    }
}