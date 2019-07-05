using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckHolidayEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckHolidayId
        {
            get
            {
                return (string)ViewState["CheckHolidayId"];
            }
            set
            {
                ViewState["CheckHolidayId"] = value;
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
        private static List<Model.View_Check_CheckHolidayDetail> checkHolidayDetails = new List<Model.View_Check_CheckHolidayDetail>();
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
              
                this.InitDropDownList();
                if (this.CurrUser.UserId != BLL.Const.sysglyId)
                {
                    this.drpThisUnit.SelectedValue = this.CurrUser.UnitId;
                    this.drpMainUnitPerson.SelectedValue = this.CurrUser.UserId;
                }

                this.txtMainUnitDeputy.Label = BLL.UnitService.GetUnitNameByUnitId(this.drpThisUnit.SelectedValue);
                this.InitUsers();
                checkHolidayDetails.Clear();
                this.CheckHolidayId = Request.Params["CheckHolidayId"];
                var checkHoliday = BLL.Check_CheckHolidayService.GetCheckHolidayByCheckHolidayId(this.CheckHolidayId);
                if (checkHoliday != null)
                {
                    this.ProjectId = checkHoliday.ProjectId;
                    if (this.ProjectId != this.CurrUser.LoginProjectId)
                    {
                        this.InitDropDownList();
                    }
                    this.txtCheckHolidayCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CheckHolidayId);
                    if (checkHoliday.CheckTime != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkHoliday.CheckTime);
                    }
                    this.txtArea.Text = checkHoliday.Area;
                    if (!string.IsNullOrEmpty(checkHoliday.ThisUnitId))
                    {
                        this.drpThisUnit.SelectedValue = checkHoliday.ThisUnitId;
                        this.txtMainUnitDeputy.Text = BLL.UnitService.GetUnitNameByUnitId(checkHoliday.ThisUnitId);
                    }
                    if (!string.IsNullOrEmpty(checkHoliday.MainUnitPerson))
                    {
                        this.drpMainUnitPerson.SelectedValueArray = checkHoliday.MainUnitPerson.Split(',');
                    }
                    if (!string.IsNullOrEmpty(checkHoliday.SubUnits))
                    {
                        this.drpSubUnits.SelectedValueArray = checkHoliday.SubUnits.Split(',');
                        this.InitUsers();
                        if (!string.IsNullOrEmpty(checkHoliday.SubUnitPerson))
                        {
                            this.drpSubUnitPerson.SelectedValueArray = checkHoliday.SubUnitPerson.Split(',');
                        }
                    }                             
                    if (!String.IsNullOrEmpty(checkHoliday.Evaluate))
                    {
                        this.drpEvaluate.SelectedValue = checkHoliday.Evaluate;
                    }
                    this.txtMainUnitDeputy.Text = checkHoliday.MainUnitDeputy;
                    this.txtMainUnitDeputyDate.Text = string.Format("{0:yyyy-MM-dd}", checkHoliday.MainUnitDeputyDate);
                    this.txtSubUnitDeputy.Text = checkHoliday.SubUnitDeputy;
                    this.txtSubUnitDeputyDate.Text = string.Format("{0:yyyy-MM-dd}", checkHoliday.SubUnitDeputyDate);
                    checkHolidayDetails = (from x in Funs.DB.View_Check_CheckHolidayDetail where x.CheckHolidayId == this.CheckHolidayId orderby x.CheckItem select x).ToList();
                }
                else
                {
                    ////自动生成编码
                    this.txtCheckHolidayCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectCheckHolidayMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
                Grid1.DataSource = checkHolidayDetails;
                Grid1.DataBind();
                ChangeGridColor();
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCheckHolidayMenuId;
                this.ctlAuditFlow.DataId = this.CheckHolidayId;
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
            BLL.UnitService.InitUnitDropDownList(this.drpThisUnit, this.ProjectId, false);          
            //本单位人员            
            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpMainUnitPerson, this.ProjectId,this.drpThisUnit.SelectedValue, false);
           // this.drpMainUnitPerson.SelectedValue = this.CurrUser.UserId;
            //参与单位
            BLL.UnitService.InitUnitDropDownList(this.drpSubUnits, this.ProjectId, true);
        }

        #region 改变Grid颜色
        private void ChangeGridColor()
        {
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(Grid1.Rows[i].Values[5].ToString()))
                {
                    Grid1.Rows[i].RowCssClass = "red";
                }
                else if (string.IsNullOrEmpty(Grid1.Rows[i].Values[6].ToString()))
                {
                    Grid1.Rows[i].RowCssClass = "yellow";
                }
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
            if (string.IsNullOrEmpty(this.CheckHolidayId))
            {
                SaveNew();
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShowCheckItem.aspx?CheckHolidayId={0}&checkType=5", this.CheckHolidayId, "编辑 - ")));
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void SaveNew()
        {
            if (string.IsNullOrEmpty(this.CheckHolidayId))
            {
                Model.Check_CheckHoliday checkHoliday = new Model.Check_CheckHoliday
                {
                    CheckHolidayId = SQLHelper.GetNewID(typeof(Model.Check_CheckHoliday)),
                    CheckHolidayCode = this.txtCheckHolidayCode.Text.Trim(),
                    CheckTime = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim()),
                    ProjectId = this.ProjectId,
                    Area = this.txtArea.Text.Trim(),
                    Evaluate = this.drpEvaluate.SelectedValue
                };
                if (!string.IsNullOrEmpty(this.drpThisUnit.SelectedValue))
                {
                    checkHoliday.ThisUnitId = this.drpThisUnit.SelectedValue;
                }

                //本单位人员
                string mainUnitPerson = string.Empty;
                foreach (var item in this.drpMainUnitPerson.SelectedValueArray)
                {
                    mainUnitPerson += item + ",";
                }
                if (!string.IsNullOrEmpty(mainUnitPerson))
                {
                    mainUnitPerson = mainUnitPerson.Substring(0, mainUnitPerson.LastIndexOf(","));
                }
                checkHoliday.MainUnitPerson = mainUnitPerson;
                //参与单位
                string subUnits = string.Empty;
                foreach (var item in this.drpSubUnits.SelectedValueArray)
                {
                    subUnits += item + ",";
                }
                if (!string.IsNullOrEmpty(subUnits))
                {
                    checkHoliday.SubUnits = subUnits.Substring(0, subUnits.LastIndexOf(","));
                }

                //参与用户
                string subUnitPerson = string.Empty;
                foreach (var item in this.drpSubUnitPerson.SelectedValueArray)
                {
                    if (item != BLL.Const._Null)
                    {
                        subUnitPerson += item + ",";
                    }
                }
                if (!string.IsNullOrEmpty(subUnitPerson))
                {
                    checkHoliday.SubUnitPerson = subUnitPerson.Substring(0, subUnitPerson.LastIndexOf(","));
                }
                //if (this.ckbIsCompleted.Checked)
                //{
                //    checkHoliday.IsCompleted = true;
                //}
                checkHoliday.MainUnitDeputy = this.txtMainUnitDeputy.Text.Trim();
                checkHoliday.MainUnitDeputyDate = Funs.GetNewDateTime(this.txtMainUnitDeputyDate.Text.Trim());
                checkHoliday.SubUnitDeputy = this.txtSubUnitDeputy.Text.Trim();
                checkHoliday.SubUnitDeputyDate = Funs.GetNewDateTime(this.txtSubUnitDeputyDate.Text.Trim());
                checkHoliday.CompileMan = this.CurrUser.UserId;
                ////单据状态
                checkHoliday.States = BLL.Const.State_0;
                this.CheckHolidayId = checkHoliday.CheckHolidayId;
                BLL.Check_CheckHolidayService.AddCheckHoliday(checkHoliday);
                BLL.LogService.AddLog(this.ProjectId, this.CurrUser.UserId, "增加季节性/节假日检查信息");
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
                Alert.ShowInTop("请将检查项的检查结果补充完整！", MessageBoxIcon.Warning);
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
                Alert.ShowInTop("请将检查项的检查结果补充完整！", MessageBoxIcon.Warning);
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
            if (!string.IsNullOrEmpty(this.CheckHolidayId))
            {
                var details = BLL.Check_CheckHolidayDetailService.GetCheckHolidayDetailByCheckHolidayId(this.CheckHolidayId);
                if (details.Count() > 0)
                {
                    foreach (var item in details)
                    {
                        if (string.IsNullOrEmpty(item.CheckResult))
                        {
                            isAllFix = false;
                            break;
                        }
                    }
                }
                else
                {
                    isAllFix = false;
                }
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
            Model.Check_CheckHoliday checkHoliday = new Model.Check_CheckHoliday
            {
                CheckHolidayCode = this.txtCheckHolidayCode.Text.Trim(),
                CheckTime = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim()),
                ProjectId = this.ProjectId,
                Area = this.txtArea.Text.Trim(),
                Evaluate = this.drpEvaluate.SelectedValue
            };
            if (!string.IsNullOrEmpty(this.drpThisUnit.SelectedValue))
            {
                checkHoliday.ThisUnitId = this.drpThisUnit.SelectedValue;
            }
            //本单位人员
            string mainUnitPerson = string.Empty;
            foreach (var item in this.drpMainUnitPerson.SelectedValueArray)
            {
                mainUnitPerson += item + ",";
            }
            if (!string.IsNullOrEmpty(mainUnitPerson))
            {
                mainUnitPerson = mainUnitPerson.Substring(0, mainUnitPerson.LastIndexOf(","));
            }
            checkHoliday.MainUnitPerson = mainUnitPerson;
            //参与单位
            string subUnits = string.Empty;
            foreach (var item in this.drpSubUnits.SelectedValueArray)
            {
                subUnits += item + ",";
            }
            if (!string.IsNullOrEmpty(subUnits))
            {
                 checkHoliday.SubUnits = subUnits.Substring(0, subUnits.LastIndexOf(","));
            }
            //参与用户
            string subUnitPerson = string.Empty;
            foreach (var item in this.drpSubUnitPerson.SelectedValueArray)
            {
                if (item != BLL.Const._Null)
                {
                    subUnitPerson += item + ",";
                }
            }
            if (!string.IsNullOrEmpty(subUnitPerson))
            {
                checkHoliday.SubUnitPerson = subUnitPerson.Substring(0, subUnitPerson.LastIndexOf(","));
            }
            //if (this.ckbIsCompleted.Checked)
            //{
            //    checkHoliday.IsCompleted = true;
            //}
            checkHoliday.MainUnitDeputy = this.txtMainUnitDeputy.Text.Trim();
            checkHoliday.MainUnitDeputyDate = Funs.GetNewDateTime(this.txtMainUnitDeputyDate.Text.Trim());
            checkHoliday.SubUnitDeputy = this.txtSubUnitDeputy.Text.Trim();
            checkHoliday.SubUnitDeputyDate = Funs.GetNewDateTime(this.txtSubUnitDeputyDate.Text.Trim());
            ////单据状态
            checkHoliday.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                checkHoliday.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.CheckHolidayId))
            {
                checkHoliday.CheckHolidayId = this.CheckHolidayId;
                BLL.Check_CheckHolidayService.UpdateCheckHoliday(checkHoliday);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "修改季节性/节假日检查", checkHoliday.CheckHolidayId);
            }
            else
            {
                checkHoliday.CheckHolidayId = SQLHelper.GetNewID(typeof(Model.Check_CheckHoliday));
                this.CheckHolidayId = checkHoliday.CheckHolidayId;
                checkHoliday.CompileMan = this.CurrUser.UserId;
                BLL.Check_CheckHolidayService.AddCheckHoliday(checkHoliday);
                BLL.LogService.AddLogDataId(this.ProjectId, this.CurrUser.UserId, "添加季节性/节假日检查", checkHoliday.CheckHolidayId);
            }

            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectCheckHolidayMenuId, this.CheckHolidayId, (type == BLL.Const.BtnSubmit ? true : false), checkHoliday.Area, "../Check/CheckHolidayView.aspx?CheckHolidayId={0}");
        }

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            checkHolidayDetails = (from x in Funs.DB.View_Check_CheckHolidayDetail where x.CheckHolidayId == this.CheckHolidayId orderby x.CheckItem select x).ToList();
            Grid1.DataSource = checkHolidayDetails;
            Grid1.DataBind();
            ChangeGridColor();
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
            string checkHolidayDetailId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckHolidayDetailEdit.aspx?CheckHolidayDetailId={0}", checkHolidayDetailId, "编辑 - ")));

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
                    BLL.Check_CheckHolidayDetailService.DeleteCheckHolidayDetailById(rowID);
                }
                checkHolidayDetails = (from x in Funs.DB.View_Check_CheckHolidayDetail where x.CheckHolidayId == this.CheckHolidayId orderby x.CheckItem select x).ToList();
                Grid1.DataSource = checkHolidayDetails;
                Grid1.DataBind();
                ChangeGridColor();
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
            if (string.IsNullOrEmpty(this.CheckHolidayId))
            {
                SaveNew();
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckHoliday&menuId={1}", this.CheckHolidayId, BLL.Const.ProjectCheckHolidayMenuId)));
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
            string checkHolidayDetailId = Grid1.DataKeys[e.RowIndex][0].ToString();
            Model.Check_CheckHolidayDetail detail = BLL.Check_CheckHolidayDetailService.GetCheckHolidayDetailByCheckHolidayDetailId(checkHolidayDetailId);
            if (e.CommandName == "click")
            {
                Model.Check_CheckHolidayDetail newDetail = new Model.Check_CheckHolidayDetail
                {
                    CheckHolidayDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckHolidayDetail)),
                    CheckHolidayId = detail.CheckHolidayId,
                    CheckItem = detail.CheckItem,
                    CheckContent = detail.CheckContent,
                    CheckResult = "隐患",
                    CheckOpinion = "整改"
                };
                BLL.Check_CheckHolidayDetailService.AddCheckHolidayDetail(newDetail);
                checkHolidayDetails = (from x in Funs.DB.View_Check_CheckHolidayDetail where x.CheckHolidayId == this.CheckHolidayId orderby x.CheckItem select x).ToList();
                Grid1.DataSource = checkHolidayDetails;
                Grid1.DataBind();
                ChangeGridColor();
            }
        }
        #endregion

        #region DropDownList下拉选择事件
        /// <summary>
        /// 分包单位下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpSubUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            //stthis.drpSubUnits.SelectedValueArray
            this.InitUsers();
        }

        private void InitUsers()
        {
            var users = from x in Funs.DB.Sys_User
                        join y in Funs.DB.Project_ProjectUser on x.UserId equals y.UserId
                        where y.ProjectId == this.ProjectId && this.drpSubUnits.SelectedValueArray.Contains(x.UnitId)
                        select x;

            this.drpSubUnitPerson.DataValueField = "UserId";
            this.drpSubUnitPerson.DataTextField = "UserName";
            this.drpSubUnitPerson.DataSource = users;
            this.drpSubUnitPerson.DataBind();
            Funs.FineUIPleaseSelect(this.drpSubUnitPerson);
            this.drpSubUnitPerson.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpThisUnit_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            InitDropDownList();
            this.drpMainUnitPerson.SelectedIndex = 0;
            this.txtMainUnitDeputy.Text = BLL.UnitService.GetUnitNameByUnitId(this.drpThisUnit.SelectedValue);
        }
    }
}