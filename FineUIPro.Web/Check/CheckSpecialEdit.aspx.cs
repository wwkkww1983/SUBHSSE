using BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

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
        private static List<Model.View_CheckSpecialDetail> checkSpecialDetails = new List<Model.View_CheckSpecialDetail>();
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ProjectId = this.CurrUser.LoginProjectId;
                this.InitDropDownList();
                this.CheckSpecialId = Request.Params["CheckSpecialId"];
                var checkSpecial = Check_CheckSpecialService.GetCheckSpecialByCheckSpecialId(this.CheckSpecialId);
                if (checkSpecial != null)
                {
                    this.ProjectId = checkSpecial.ProjectId;
                    if (this.ProjectId != this.CurrUser.LoginProjectId)
                    {
                        this.InitDropDownList();
                    }
                    this.txtCheckSpecialCode.Text = CodeRecordsService.ReturnCodeByDataId(this.CheckSpecialId);
                    if (checkSpecial.CheckTime != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkSpecial.CheckTime);
                    }                    
                    this.txtPartInPersonNames.Text = checkSpecial.PartInPersonNames;
                    if (!string.IsNullOrEmpty(checkSpecial.CheckItemSetId))
                    {
                        this.drpSupCheckItemSet.SelectedValue = checkSpecial.CheckItemSetId;
                        this.getCheckItemDrp();
                    }
                    if (!string.IsNullOrEmpty(checkSpecial.PartInPersonIds))
                    {
                        this.drpPartInPersons.SelectedValueArray = checkSpecial.PartInPersonIds.Split(',');
                    }
                    checkSpecialDetails = (from x in Funs.DB.View_CheckSpecialDetail
                                           where x.CheckSpecialId == this.CheckSpecialId
                                           orderby x.CheckItem
                                           select x).ToList();
                    if (checkSpecialDetails.Count() > 0)
                    {
                        this.drpSupCheckItemSet.Readonly = true;
                    }
                }
                else
                {
                    ////自动生成编码
                    this.txtCheckSpecialCode.Text = CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectCheckSpecialMenuId, this.ProjectId, this.CurrUser.UnitId);
                    this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);              
                    this.drpSupCheckItemSet.SelectedIndex = 0;
                    this.drpSupCheckItemSet.SelectedIndex = 0;
                }

                // 删除选中单元格的客户端脚本
                string deleteScript = GetDeleteScript();
                // 新增数据初始值
                JObject defaultObj = new JObject
                {
                    { "WorkArea", "" },
                    { "UnitName", "" },
                     { "Unqualified", "" },
                    { "CheckContent", "" },
                     { "CompleteStatusName", "" },
                    { "HandleStepStr", "" },
                     { "HandleStep", "" },
                    { "UnitId", "" },
                    { "Delete", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(Icon.Delete)) }
                };
                // 在第一行新增一条数据
                btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, false);
                // 删除选中行按钮
                //btnDelete.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！") + deleteScript;

                Grid1.DataSource = checkSpecialDetails;
                Grid1.DataBind();
            }
        }
        #endregion

        /// <summary>
        ///  初始化下拉框
        /// </summary>
        private void InitDropDownList()
        {
            //检查组成员
            UserService.InitUserDropDownList(this.drpPartInPersons, this.ProjectId, true);

            checkSpecialDetails.Clear();
            ConstValue.InitConstNameDropDownList(this.drpHandleStep, ConstValue.Group_HandleStep, true);

            Technique_CheckItemSetService.InitCheckItemSetDropDownList(this.drpSupCheckItemSet, "2", "0", false);
            this.getCheckItemDrp();
            ///责任单位
            UnitService.InitUnitNameByProjectIdUnitTypeDropDownList(this.drpWorkUnit, this.ProjectId, Const.ProjectUnitType_2, false);
        }

        /// <summary>
        /// 
        /// </summary>
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
                checkSpecial.PartInPersonNames = this.txtPartInPersonNames.Text.Trim();
                checkSpecial.CheckTime = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());

                ////单据状态
                checkSpecial.States = Const.State_0;
                this.CheckSpecialId = checkSpecial.CheckSpecialId;
                checkSpecial.CompileMan = this.CurrUser.UserId;
                BLL.Check_CheckSpecialService.AddCheckSpecial(checkSpecial);
                BLL.LogService.AddSys_Log(this.CurrUser, checkSpecial.CheckSpecialCode, checkSpecial.CheckSpecialId, BLL.Const.ProjectCheckSpecialMenuId, BLL.Const.BtnAdd);
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
            if (string.IsNullOrEmpty(this.drpSupCheckItemSet.SelectedValue) || this.drpSupCheckItemSet.SelectedValue == Const._Null)
            {
                ShowNotify("请选择检查类别！", MessageBoxIcon.Warning);
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
            if(string.IsNullOrEmpty(this.drpSupCheckItemSet.SelectedValue) || this.drpSupCheckItemSet.SelectedValue == Const._Null)
            {
                ShowNotify("请选择检查类别！", MessageBoxIcon.Warning);
                return;
            }
            this.SaveData(Const.BtnSave);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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

            checkSpecial.PartInPersonNames= this.txtPartInPersonNames.Text.Trim();
            checkSpecial.CheckTime = Funs.GetNewDateTime(this.txtCheckDate.Text.Trim());
            ////单据状态
            checkSpecial.States = Const.State_0;
            if (type == Const.BtnSubmit)
            {
                checkSpecial.States = Const.State_1;
            }
            if (!string.IsNullOrEmpty(this.drpSupCheckItemSet.SelectedValue) && this.drpSupCheckItemSet.SelectedValue != Const._Null)
            {
                checkSpecial.CheckItemSetId = this.drpSupCheckItemSet.SelectedValue;
            }
            if (!string.IsNullOrEmpty(this.CheckSpecialId))
            {
                checkSpecial.CheckSpecialId = this.CheckSpecialId;
                Check_CheckSpecialService.UpdateCheckSpecial(checkSpecial);
                LogService.AddSys_Log(this.CurrUser, checkSpecial.CheckSpecialCode, checkSpecial.CheckSpecialId,BLL.Const.ProjectCheckSpecialMenuId,BLL.Const.BtnModify);
                Check_CheckSpecialDetailService.DeleteCheckSpecialDetails(this.CheckSpecialId);
            }
            else
            {
                checkSpecial.CheckSpecialId = SQLHelper.GetNewID(typeof(Model.Check_CheckSpecial));
                this.CheckSpecialId = checkSpecial.CheckSpecialId;
                checkSpecial.CompileMan = this.CurrUser.UserId;
                Check_CheckSpecialService.AddCheckSpecial(checkSpecial);
                LogService.AddSys_Log(this.CurrUser, checkSpecial.CheckSpecialCode, checkSpecial.CheckSpecialId, BLL.Const.ProjectCheckSpecialMenuId, BLL.Const.BtnAdd);
            }
            this.SaveDetail(type, checkSpecial);
        }

        /// <summary>
        ///  保存明细项
        /// </summary>
        private void SaveDetail(string type, Model.Check_CheckSpecial checkSpecial)
        {
            List<Model.Check_CheckSpecialDetail> detailLists = new List<Model.Check_CheckSpecialDetail>();
            JArray teamGroupData = Grid1.GetMergedData();
            foreach (JObject teamGroupRow in teamGroupData)
            {
                JObject values = teamGroupRow.Value<JObject>("values");
                Model.Check_CheckSpecialDetail newDetail = new Model.Check_CheckSpecialDetail
                {
                    CheckSpecialDetailId = SQLHelper.GetNewID(),
                    CheckSpecialId = this.CheckSpecialId,
                    CheckContent = values.Value<string>("CheckItemName"),
                    Unqualified = values.Value<string>("Unqualified"),
                    WorkArea = values.Value<string>("WorkArea"),
                };
                var getUnit = Funs.DB.Base_Unit.FirstOrDefault(x => x.UnitName == values.Value<string>("UnitName"));
                if (getUnit != null)
                {
                    newDetail.UnitId = getUnit.UnitId;
                }
                var getHandleStep = Funs.DB.Sys_Const.FirstOrDefault(x => x.GroupId == ConstValue.Group_HandleStep && x.ConstText == values.Value<string>("HandleStepStr"));
                if (getHandleStep != null)
                {
                    newDetail.HandleStep = getHandleStep.ConstValue;
                }
                if (values.Value<string>("CompleteStatusName") == "已整改")
                {
                    newDetail.CompleteStatus = true;
                    newDetail.CompletedDate = DateTime.Now;
                }
                else
                {
                    newDetail.CompleteStatus = false;
                }
                var getCheckItem = Funs.DB.Technique_CheckItemSet.FirstOrDefault(x => x.SupCheckItem == this.drpSupCheckItemSet.SelectedValue && x.CheckItemName == newDetail.CheckContent);
                if (getCheckItem != null)
                {
                    newDetail.CheckItem = getCheckItem.CheckItemSetId;
                }
                Check_CheckSpecialDetailService.AddCheckSpecialDetail(newDetail);
                if (type == Const.BtnSubmit)
                {
                    if (newDetail.CompleteStatus == false)
                    {
                        detailLists.Add(newDetail);
                    }
                }
            }

            if (detailLists.Count() > 0)
            {
                ////隐患整改单
                var getDetail1 = detailLists.Where(x => x.HandleStep == "1");
                if (getDetail1.Count() > 0)
                {
                    var getUnitList = getDetail1.Select(x => x.UnitId).Distinct();
                    foreach (var unitItem in getUnitList)
                    {
                        Model.RectifyNoticesItem rectifyNotices = new Model.RectifyNoticesItem
                        {
                            ProjectId = checkSpecial.ProjectId,
                            UnitId = unitItem,
                            CompleteManId = this.CurrUser.UserId,
                            CheckManNames = checkSpecial.PartInPersons,
                            CheckManIds = checkSpecial.PartInPersonIds,
                            CheckedDate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", checkSpecial.CheckTime),
                            States = Const.State_0,
                        };
                        rectifyNotices.RectifyNoticesItemItem = new List<Model.RectifyNoticesItemItem>();
                        var getUnitDItem = getDetail1.Where(x => x.UnitId == unitItem);
                        foreach (var item in getUnitDItem)
                        {
                            Model.RectifyNoticesItemItem newRItem = new Model.RectifyNoticesItemItem();
                            if (!string.IsNullOrEmpty(item.WorkArea))
                            {
                                newRItem.WrongContent = item.WorkArea + item.Unqualified;
                            }else
                            {
                                newRItem.WrongContent = item.Unqualified;
                            }
                            if (string.IsNullOrEmpty(rectifyNotices.CheckSpecialDetailId))
                            {
                                rectifyNotices.CheckSpecialDetailId = item.CheckSpecialDetailId;
                            }
                            else
                            {
                                rectifyNotices.CheckSpecialDetailId += "," + item.CheckSpecialDetailId;
                            }
                            var getAtt = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == item.CheckSpecialDetailId);
                            if (getAtt != null && !string.IsNullOrEmpty(getAtt.AttachUrl))
                            {
                                newRItem.PhotoBeforeUrl = getAtt.AttachUrl;
                            }

                            rectifyNotices.RectifyNoticesItemItem.Add(newRItem);
                        }

                        APIRectifyNoticesService.SaveRectifyNotices(rectifyNotices);
                    }
                }
                ///处罚单
                var getDetail2 = detailLists.Where(x => x.HandleStep == "2");
                if (getDetail2.Count() > 0)
                {
                    var getUnitList = getDetail2.Select(x => x.UnitId).Distinct();
                    foreach (var unitItem in getUnitList)
                    {
                        Model.PunishNoticeItem punishNotice = new Model.PunishNoticeItem
                        {
                            ProjectId = checkSpecial.ProjectId,
                            PunishNoticeDate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", checkSpecial.CheckTime),
                            UnitId = unitItem,
                            CompileManId = this.CurrUser.UserId,
                            PunishStates = Const.State_0,
                        };
                                           
                        var getUnitDItem = getDetail2.Where(x => x.UnitId == unitItem);
                        foreach (var item in getUnitDItem)
                        {
                            Model.RectifyNoticesItemItem newRItem = new Model.RectifyNoticesItemItem();                        
                            punishNotice.IncentiveReason += item.Unqualified;
                            if (string.IsNullOrEmpty(punishNotice.CheckSpecialDetailId))
                            {
                                punishNotice.CheckSpecialDetailId = item.CheckSpecialDetailId;
                            }
                            else
                            {
                                punishNotice.CheckSpecialDetailId += "," + item.CheckSpecialDetailId;
                            }
                            var getAtt = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == item.CheckSpecialDetailId);
                            if (getAtt != null && !string.IsNullOrEmpty(getAtt.AttachUrl))
                            {
                                punishNotice.PunishUrl = getAtt.AttachUrl;
                            }
                        }

                        APIPunishNoticeService.SavePunishNotice(punishNotice);
                    }
                }
                ///暂停令
                var getDetail3 = detailLists.Where(x => x.HandleStep == "3");
                if (getDetail3.Count() > 0)
                {
                    var getUnitList = getDetail3.Select(x => x.UnitId).Distinct();
                    foreach (var unitItem in getUnitList)
                    {
                        Model.PauseNoticeItem pauseNotice = new Model.PauseNoticeItem
                        {
                            ProjectId = checkSpecial.ProjectId,
                            UnitId = unitItem,                          
                            PauseTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", checkSpecial.CheckTime),                           
                            PauseStates = Const.State_0,
                        };

                        var getUnitDItem = getDetail3.Where(x => x.UnitId == unitItem);
                        foreach (var item in getUnitDItem)
                        {
                            Model.RectifyNoticesItemItem newRItem = new Model.RectifyNoticesItemItem();
                            pauseNotice.ThirdContent += item.Unqualified;
                            if (string.IsNullOrEmpty(pauseNotice.ProjectPlace))
                            {
                                pauseNotice.ProjectPlace = item.WorkArea;
                            }
                            else
                            {
                                if (!pauseNotice.ProjectPlace.Contains(item.WorkArea))
                                {
                                    pauseNotice.ProjectPlace += "," + item.WorkArea;
                                }
                            }
                            if (string.IsNullOrEmpty(pauseNotice.CheckSpecialDetailId))
                            {
                                pauseNotice.CheckSpecialDetailId = item.CheckSpecialDetailId;
                            }
                            else
                            {
                                pauseNotice.CheckSpecialDetailId += "," + item.CheckSpecialDetailId;
                            }
                            var getAtt = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == item.CheckSpecialDetailId);
                            if (getAtt != null && !string.IsNullOrEmpty(getAtt.AttachUrl))
                            {
                                pauseNotice.PauseNoticeAttachUrl = getAtt.AttachUrl;
                            }
                        }

                        APIPauseNoticeService.SavePauseNotice(pauseNotice);
                    }
                }
            }
        }

        #region 获取检查类型
        /// <summary>
        /// 获取检查类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertCheckItemType(object CheckItem)
        {
            return Check_ProjectCheckItemSetService.ConvertCheckItemType(CheckItem);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 设置LinkButtonField的点击客户端事件
            LinkButtonField deleteField = Grid1.FindColumn("Delete") as LinkButtonField;
            deleteField.OnClientClick = GetDeleteScript();
        }
        /// <summary>
        /// 删除提示
        /// </summary>
        /// <returns></returns>
        private string GetDeleteScript()
        {
            return Confirm.GetShowReference("删除选中行？", String.Empty, MessageBoxIcon.Question, Grid1.GetDeleteSelectedRowsReference(), String.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpSupCheckItemSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getCheckItemDrp();
        }

        private void getCheckItemDrp()
        {
            this.drpCheckItem.Items.Clear();
            Technique_CheckItemSetService.InitCheckItemSetNameDropDownList(this.drpCheckItem, "2", this.drpSupCheckItemSet.SelectedValue, false);
            checkSpecialDetails.Clear();
            Grid1.DataSource = checkSpecialDetails;
            Grid1.DataBind();
        }
    }
}