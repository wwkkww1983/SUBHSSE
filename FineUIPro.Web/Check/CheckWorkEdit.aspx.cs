using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class CheckWorkEdit : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckWorkId
        {
            get
            {
                return (string)ViewState["CheckWorkId"];
            }
            set
            {
                ViewState["CheckWorkId"] = value;
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
        private static List<Model.View_Check_CheckWorkDetail> checkWorkDetails = new List<Model.View_Check_CheckWorkDetail>();
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

                this.txtMainUnitDeputy.Label = BLL.UnitService.GetUnitNameByUnitId(this.drpThisUnit.SelectedValue);
                this.InitDropDownList();
                if (this.CurrUser.UserId != BLL.Const.sysglyId)
                {
                    this.drpThisUnit.SelectedValue = this.CurrUser.UnitId;
                    this.drpMainUnitPerson.SelectedValue = this.CurrUser.UserId;
                }
                this.InitUsers();
                checkWorkDetails.Clear();

                this.CheckWorkId = Request.Params["CheckWorkId"];
                var checkWork = BLL.Check_CheckWorkService.GetCheckWorkByCheckWorkId(this.CheckWorkId);
                if (checkWork != null)
                {
                    this.ProjectId = checkWork.ProjectId;
                    if (this.ProjectId != this.CurrUser.LoginProjectId)
                    {
                        this.InitDropDownList();
                    }
                    this.txtCheckWorkCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CheckWorkId);
                    if (checkWork.CheckTime != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkWork.CheckTime);
                    }
                    this.txtArea.Text = checkWork.Area;
                    if (!string.IsNullOrEmpty(checkWork.ThisUnitId))
                    {
                        this.drpThisUnit.SelectedValue = checkWork.ThisUnitId;
                        this.txtMainUnitDeputy.Text = BLL.UnitService.GetUnitNameByUnitId(checkWork.ThisUnitId);
                    }
                    if (!string.IsNullOrEmpty(checkWork.MainUnitPerson))
                    {
                        this.drpMainUnitPerson.SelectedValueArray = checkWork.MainUnitPerson.Split(',');
                    }
                    this.txtPartInPersonNames.Text = checkWork.PartInPersonNames;
                    if (!string.IsNullOrEmpty(checkWork.SubUnits))
                    {
                        this.drpSubUnits.SelectedValueArray = checkWork.SubUnits.Split(',');
                        this.InitUsers();
                        if (!string.IsNullOrEmpty(checkWork.SubUnitPerson))
                        {
                            this.drpSubUnitPerson.SelectedValueArray = checkWork.SubUnitPerson.Split(',');
                        }
                    }
                    this.txtPartInPersonNames.Text = checkWork.PartInPersonNames;
                    if (checkWork.IsAgree == true)
                    {
                        this.ckIsAgree.Checked = true;
                    }
                    else
                    {
                        this.ckIsAgree.Checked = false;
                    }
                    this.txtMainUnitDeputy.Text = checkWork.MainUnitDeputy;
                    this.txtMainUnitDeputyDate.Text = string.Format("{0:yyyy-MM-dd}", checkWork.MainUnitDeputyDate);
                    this.txtSubUnitDeputy.Text = checkWork.SubUnitDeputy;
                    this.txtSubUnitDeputyDate.Text = string.Format("{0:yyyy-MM-dd}", checkWork.SubUnitDeputyDate);                    
                }
                else
                {
                    ////自动生成编码
                    this.txtCheckWorkCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectCheckWorkMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    var thisUnit = BLL.CommonService.GetIsThisUnit();
                    if (thisUnit != null && thisUnit.UnitId == Const.UnitId_CWCEC)
                    {
                        SaveNew(true);                       
                    }
                }

                Grid1.DataSource = (from x in Funs.DB.View_Check_CheckWorkDetail
                                    where x.CheckWorkId == this.CheckWorkId
                                    orderby x.SortIndex
                                    select x).ToList(); 
                Grid1.DataBind();
                //ChangeGridColor();

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCheckWorkMenuId;
                this.ctlAuditFlow.DataId = this.CheckWorkId; 
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
            BLL.UserService.InitUserProjectIdUnitIdDropDownList(this.drpMainUnitPerson, this.ProjectId, this.drpThisUnit.SelectedValue, false);
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
            if (string.IsNullOrEmpty(this.CheckWorkId))
            {
                SaveNew(false);
            }
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ShowCheckItem.aspx?CheckWorkId={0}&checkType=4", this.CheckWorkId, "编辑 - ")));
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void SaveNew(bool isWuHuan )
        {
            if (string.IsNullOrEmpty(this.CheckWorkId))
            {
                Model.Check_CheckWork checkWork = new Model.Check_CheckWork
                {
                    CheckWorkId = SQLHelper.GetNewID(typeof(Model.Check_CheckWork)),
                    CheckWorkCode = this.txtCheckWorkCode.Text.Trim(),
                    CheckTime = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim()),
                    ProjectId = this.ProjectId,
                    Area = this.txtArea.Text.Trim()
                };
                if (!string.IsNullOrEmpty(this.drpThisUnit.SelectedValue))
                {
                    checkWork.ThisUnitId = this.drpThisUnit.SelectedValue;
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
                checkWork.MainUnitPerson = mainUnitPerson;
                //参与单位
                string subUnits = string.Empty;
                foreach (var item in this.drpSubUnits.SelectedValueArray)
                {
                    subUnits += item + ",";
                }
                if (!string.IsNullOrEmpty(subUnits))
                {
                    checkWork.SubUnits = subUnits.Substring(0, subUnits.LastIndexOf(","));
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
                    checkWork.SubUnitPerson = subUnitPerson.Substring(0, subUnitPerson.LastIndexOf(","));
                }

                //if (this.ckbIsCompleted.Checked)
                //{
                //    checkWork.IsCompleted = true;
                //}

                checkWork.PartInPersonNames = this.txtPartInPersonNames.Text.Trim();
                checkWork.MainUnitDeputy = this.txtMainUnitDeputy.Text.Trim();
                checkWork.MainUnitDeputyDate = Funs.GetNewDateTime(this.txtMainUnitDeputyDate.Text.Trim());
                checkWork.SubUnitDeputy = this.txtSubUnitDeputy.Text.Trim();
                checkWork.SubUnitDeputyDate = Funs.GetNewDateTime(this.txtSubUnitDeputyDate.Text.Trim());
                checkWork.CompileMan = this.CurrUser.UserId;
                ////单据状态
                checkWork.States = BLL.Const.State_0;
                this.CheckWorkId = checkWork.CheckWorkId;
                if (this.ckIsAgree.Checked)
                {
                    checkWork.IsAgree = true;
                }
                BLL.Check_CheckWorkService.AddCheckWork(checkWork);
                BLL.LogService.AddSys_Log(this.CurrUser, checkWork.CheckWorkCode, checkWork.CheckWorkId, BLL.Const.ProjectCheckWorkMenuId, BLL.Const.BtnAdd);
                if (isWuHuan)
                {
                    checkWorkDetails = (from x in Funs.DB.Check_ProjectCheckItemDetail
                                        join y in Funs.DB.Check_ProjectCheckItemSet on x.CheckItemSetId equals y.CheckItemSetId
                                        where y.ProjectId == this.ProjectId && y.CheckType== "4"
                                        orderby y.SortIndex
                                        select new Model.View_Check_CheckWorkDetail
                                        {
                                            CheckWorkId = this.CheckWorkId,
                                            CheckItem = y.CheckItemSetId,
                                            CheckContent = x.CheckContent,
                                            CheckItemStr = x.CheckContent,
                                            SortIndex =x.SortIndex,
                                            CheckResult="合格",
                                        }).ToList();
                    foreach (var item in checkWorkDetails)
                    {
                        Model.Check_CheckWorkDetail detail = new Model.Check_CheckWorkDetail
                        {
                            CheckWorkDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckWorkDetail)),
                            CheckWorkId = item.CheckWorkId,
                            CheckItem = item.CheckItem,
                            CheckContent = item.CheckContent,
                            SortIndex = item.SortIndex,
                            CheckResult = item.CheckResult,
                        };
                        
                        BLL.Check_CheckWorkDetailService.AddCheckWorkDetail(detail);
                    }
                }             
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
            //if (!IsAllFix())
            //{
            //    Alert.ShowInTop("请将检查项的检查结果补充完整！", MessageBoxIcon.Warning);
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
        private bool IsAllFix()
        {
            bool isAllFix = true;
            if (!string.IsNullOrEmpty(this.CheckWorkId))
            {
                var details = BLL.Check_CheckWorkDetailService.GetCheckWorkDetailByCheckWorkId(this.CheckWorkId);
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
            Model.Check_CheckWork checkWork = new Model.Check_CheckWork
            {
                CheckWorkCode = this.txtCheckWorkCode.Text.Trim(),
                CheckTime = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim()),
                ProjectId = this.ProjectId,
                Area = this.txtArea.Text.Trim()
            };
            if (!string.IsNullOrEmpty(this.drpThisUnit.SelectedValue))
            {
                checkWork.ThisUnitId = this.drpThisUnit.SelectedValue;
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
            checkWork.MainUnitPerson = mainUnitPerson;
            //参与单位
            string subUnits = string.Empty;
            foreach (var item in this.drpSubUnits.SelectedValueArray)
            {
                subUnits += item + ",";
            }
            if (!string.IsNullOrEmpty(subUnits))
            {
                checkWork.SubUnits = subUnits.Substring(0, subUnits.LastIndexOf(","));
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
                checkWork.SubUnitPerson = subUnitPerson.Substring(0, subUnitPerson.LastIndexOf(","));
            }
            checkWork.PartInPersonNames = this.txtPartInPersonNames.Text.Trim();
            if (this.ckIsAgree.Checked)
            {
                checkWork.IsAgree = true;
            }
            checkWork.MainUnitDeputy = this.txtMainUnitDeputy.Text.Trim();
            checkWork.MainUnitDeputyDate = Funs.GetNewDateTime(this.txtMainUnitDeputyDate.Text.Trim());
            checkWork.SubUnitDeputy = this.txtSubUnitDeputy.Text.Trim();
            checkWork.SubUnitDeputyDate = Funs.GetNewDateTime(this.txtSubUnitDeputyDate.Text.Trim());
            ////单据状态
            checkWork.States = BLL.Const.State_0;
            if (type == BLL.Const.BtnSubmit)
            {
                checkWork.States = this.ctlAuditFlow.NextStep;
            }
            if (!string.IsNullOrEmpty(this.CheckWorkId))
            {
                checkWork.CheckWorkId = this.CheckWorkId;
                BLL.Check_CheckWorkService.UpdateCheckWork(checkWork);
                BLL.LogService.AddSys_Log(this.CurrUser, checkWork.CheckWorkCode, checkWork.CheckWorkId, BLL.Const.ProjectCheckWorkMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                checkWork.CheckWorkId = SQLHelper.GetNewID(typeof(Model.Check_CheckWork));
                checkWork.CompileMan = this.CurrUser.UserId; 
                this.CheckWorkId = checkWork.CheckWorkId;
                BLL.Check_CheckWorkService.AddCheckWork(checkWork);
                BLL.LogService.AddSys_Log(this.CurrUser, checkWork.CheckWorkCode, checkWork.CheckWorkId, BLL.Const.ProjectCheckWorkMenuId, BLL.Const.BtnModify);
            }
            ////保存流程审核数据         
            this.ctlAuditFlow.btnSaveData(this.ProjectId, BLL.Const.ProjectCheckWorkMenuId, this.CheckWorkId, (type == BLL.Const.BtnSubmit ? true : false), checkWork.Area, "../Check/CheckWorkView.aspx?CheckWorkId={0}");
        }

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, EventArgs e)
        {
            checkWorkDetails = (from x in Funs.DB.View_Check_CheckWorkDetail
                                where x.CheckWorkId == this.CheckWorkId
                                orderby x.SortIndex
                                select x).ToList();
            Grid1.DataSource = checkWorkDetails;
            Grid1.DataBind();
            //ChangeGridColor();
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
            string checkWorkDetailId = Grid1.SelectedRowID;
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckWorkDetailEdit.aspx?CheckWorkDetailId={0}", checkWorkDetailId, "编辑 - ")));

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
                                        
                    BLL.Check_CheckWorkDetailService.DeleteCheckWorkDetailById(rowID);
                }
                checkWorkDetails = (from x in Funs.DB.View_Check_CheckWorkDetail
                                    where x.CheckWorkId == this.CheckWorkId
                                    orderby x.SortIndex select x).ToList();
                Grid1.DataSource = checkWorkDetails;
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
            if (string.IsNullOrEmpty(this.CheckWorkId))
            {
                SaveNew(false);
            }
            PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckWork&menuId={1}", this.CheckWorkId, BLL.Const.ProjectCheckWorkMenuId)));
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
            string checkWorkDetailId = Grid1.DataKeys[e.RowIndex][0].ToString();
            Model.Check_CheckWorkDetail detail = BLL.Check_CheckWorkDetailService.GetCheckWorkDetailByCheckWorkDetailId(checkWorkDetailId);
            if (e.CommandName == "click")
            {
                Model.Check_CheckWorkDetail newDetail = new Model.Check_CheckWorkDetail
                {
                    CheckWorkDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckWorkDetail)),
                    CheckWorkId = detail.CheckWorkId,
                    CheckItem = detail.CheckItem,
                    SortIndex = detail.SortIndex,
                    CheckContent = detail.CheckContent,
                };

                BLL.Check_CheckWorkDetailService.AddCheckWorkDetail(newDetail);
                checkWorkDetails = (from x in Funs.DB.View_Check_CheckWorkDetail
                                    where x.CheckWorkId == this.CheckWorkId
                                    orderby x.SortIndex select x).ToList();
                Grid1.DataSource = checkWorkDetails;
                Grid1.DataBind();
                //ChangeGridColor();
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

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CheckWorkId))
            {
                this.SaveData(BLL.Const.BtnSave);
            }

            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("CheckWorkDetailIn.aspx?CheckWorkId={0}", this.CheckWorkId, "导入 - "), "导入", 1024, 560));
        }
    }
}