using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.HiddenInspection
{
    public partial class HiddenRectificationRecord : PageBase
    {
        #region 定义项
        /// <summary>
        /// 隐患巡检记录主键
        /// </summary>
        public string HazardRegisterRecordId
        {
            get
            {
                return (string)ViewState["HazardRegisterRecordId"];
            }
            set
            {
                ViewState["HazardRegisterRecordId"] = value;
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
                this.btnMenuDelete.ConfirmText = String.Format("你确定要删除选中的&nbsp;<b><script>{0}</script></b>&nbsp;行数据吗？", Grid1.GetSelectedCountReference());
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                this.drpCheckMan.DataTextField = "UserName";
                this.drpCheckMan.DataValueField = "UserId";
                this.drpCheckMan.DataSource = (from x in Funs.DB.View_Hazard_HazardRegister
                                               join y in Funs.DB.Sys_User
                                               on x.CheckManId equals y.UserId
                                               where x.ProblemTypes == "1" && x.ProjectId == this.CurrUser.LoginProjectId
                                               select y).Distinct().ToList();
                this.drpCheckMan.DataBind();
                Funs.FineUIPleaseSelect(this.drpCheckMan);
                GetRecords();
                // 绑定表格
                BindGrid();
            }
        }

        private void GetRecords()
        {
            Model.SUBHSSEDB db = Funs.DB;
            var hazardRegisterList = (from x in db.View_Hazard_HazardRegister
                                      where x.ProblemTypes == "1" && x.ProjectId == this.CurrUser.LoginProjectId
                                      select x).ToList();
            var checkManList = (from x in hazardRegisterList
                                select x.CheckManId).Distinct().ToList();   //获取巡检人集合
            var checkDateList = (from x in hazardRegisterList
                                 select x.CheckTime.Value.Date).Distinct().ToList();    //获取巡检日期集合
            foreach (var checkMan in checkManList)
            {
                foreach (var checkDate in checkDateList)
                {
                    Model.HSSE_Hazard_HazardRegisterRecord oldRecord = BLL.HSSE_Hazard_HazardRegisterRecordService.GetHazardRegisterRecordByCheckManAndDate(checkMan, checkDate, "1");
                    if (oldRecord == null)
                    {
                        var list = from x in hazardRegisterList
                                   where x.CheckManId == checkMan && x.CheckTime.Value.Date == checkDate
                                   orderby x.CheckTime
                                   select x;
                        if (list.Count() > 0)
                        {
                            Model.HSSE_Hazard_HazardRegisterRecord record = new Model.HSSE_Hazard_HazardRegisterRecord();
                            record.HazardRegisterRecordId = SQLHelper.GetNewID(typeof(Model.HSSE_Hazard_HazardRegisterRecord));
                            record.ProjectId = this.CurrUser.LoginProjectId;
                            string hazardRegisterIds = string.Empty;
                            foreach (var item in list)
                            {
                                hazardRegisterIds += item.HazardRegisterId + ",";
                            }
                            if (!string.IsNullOrEmpty(hazardRegisterIds))
                            {
                                hazardRegisterIds = hazardRegisterIds.Substring(0, hazardRegisterIds.LastIndexOf(","));
                            }
                            record.HazardRegisterIds = hazardRegisterIds;
                            record.CheckDate = checkDate;
                            record.CheckPerson = checkMan;
                            record.CheckType = "1";   //安全
                            record.CompileMan = this.CurrUser.UserId;
                            record.CompileDate = DateTime.Now;
                            db.HSSE_Hazard_HazardRegisterRecord.InsertOnSubmit(record);
                            db.SubmitChanges();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = "select * from HSSE_View_HazardRegisterRecord  Where ProjectId=@ProjectId and CheckType='1' ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            if (this.drpCheckMan.SelectedValue != BLL.Const._Null)
            {
                strSql += " And CheckPerson=@CheckPerson ";
                listStr.Add(new SqlParameter("@CheckPerson", this.drpCheckMan.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtDate.Text.Trim()))
            {
                strSql += " And CheckDate>= @Date ";
                listStr.Add(new SqlParameter("@Date", Funs.GetNewDateTime(this.txtDate.Text.Trim())));
            }

            if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
            {
                strSql += " And CheckDate <= @EndDate ";
                listStr.Add(new SqlParameter("@EndDate", Funs.GetNewDateTime(this.txtEndDate.Text.Trim())));
            }

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 表头过滤
        /// <summary>
        /// 表头过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 页索引改变事件
        /// <summary>
        /// 页索引改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }
        #endregion

        #region 分页选择下拉改变事件
        /// <summary>
        /// 分页选择下拉改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion

        #region 关闭弹出窗
        /// <summary>
        /// 关闭弹出窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 编制按钮
        /// <summary>
        /// 编制按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_HiddenRectificationRecordMenuId, BLL.Const.BtnSave))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenRectificationRecordEdit.aspx", "编辑 - ")));
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            this.DeleteData();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        private void DeleteData()
        {
            if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_HiddenRectificationRecordMenuId, BLL.Const.BtnDelete))
            {
                if (Grid1.SelectedRowIndexArray.Length > 0)
                {
                    bool isShow = false;
                    if (Grid1.SelectedRowIndexArray.Length == 1)
                    {
                        isShow = true;
                    }
                    foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                    {
                        string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                        if (this.judgementDelete(rowID, isShow))
                        {
                            var getD = BLL.HSSE_Hazard_HazardRegisterRecordService.GetHazardRegisterRecordByHazardRegisterRecordId(HazardRegisterRecordId);
                            if (getD != null)
                            {
                                BLL.LogService.AddSys_Log(this.CurrUser, null, getD.HazardRegisterRecordId, BLL.Const.HSSE_HiddenRectificationRecordMenuId, BLL.Const.BtnDelete);
                                BLL.HSSE_Hazard_HazardRegisterRecordService.DeleteHazardRegisterRecordByHazardRegisterRecordId(rowID);                                
                                BindGrid();
                                ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
                            }
                        }
                    }
                }
            }
            else
            {
                Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <param name="rowID"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        private bool judgementDelete(string rowID, bool isShow)
        {
            string content = string.Empty;
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            else
            {
                if (isShow)
                {
                    Alert.ShowInTop(content);
                }
                return false;
            }
        }
        #endregion

        #region 格式化字符串
        /// <summary>
        /// 获取编写人
        /// </summary>
        /// <param name="creater"></param>
        /// <returns></returns>
        protected string ConvertCreater(object creater)
        {
            if (creater != null)
            {
                var user = BLL.UserService.GetUserByUserId(creater.ToString());
                if (user != null)
                {
                    return user.UserName;
                }
            }
            return "";
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="samplingTime"></param>
        /// <returns></returns>
        protected string ConvertState(object samplingTime)
        {
            if (!string.IsNullOrEmpty(samplingTime.ToString()))
            {
                return "已抽检";
            }
            else
            {
                return "未抽检";
            }
        }

        /// <summary>
        /// 获取抽检人
        /// </summary>
        /// <param name="samplinger"></param>
        /// <returns></returns>
        protected string ConvertSamplinger(object samplinger)
        {
            if (samplinger != null)
            {
                var user = BLL.UserService.GetUserByUserId(samplinger.ToString());
                if (user != null)
                {
                    return user.UserName;
                }
            }
            return "";
        }
        #endregion

        #region Grid行点击事件
        /// <summary>
        /// Grid行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            this.HazardRegisterRecordId = Grid1.DataKeys[e.RowIndex][0].ToString();
            //if (e.CommandName == "click")//编辑
            //{
            //    if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_HiddenRectificationRecordMenuId, BLL.Const.BtnSave))
            //    {
            //        if (BLL.HSSE_Hazard_HazardRegisterRecordService.IsModifyHSSELog(log.Date))
            //        {
            //            if (this.CurrUser.UserId == log.Creater && !log.SamplingTime.HasValue)
            //            {
            //                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSELogEdit.aspx?HazardRegisterRecordId={0}&type=0", this.HazardRegisterRecordId, "编辑 - ")));
            //            }
            //            else
            //            {
            //                Alert.ShowInTop("您不是此日志的编辑人或日志已抽检，可点击查看日志！", MessageBoxIcon.Warning);
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            Alert.ShowInTop("只能编辑当天日志！", MessageBoxIcon.Warning);
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            //if (e.CommandName == "check")//抽检
            //{
            //    if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_HiddenRectificationRecordMenuId, BLL.Const.BtnUpCheck))
            //    {
            //        if ((!log.SamplingTime.HasValue || this.CurrUser.UserId == log.Samplinger || this.CurrUser.UserId == Const.GlyId))
            //        {
            //            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HSSELogEdit.aspx?HazardRegisterRecordId={0}&type=1", this.HazardRegisterRecordId, "抽检 - ")));
            //        }
            //        else
            //        {
            //            Alert.ShowInTop("您不是抽检人或单据已抽检！", MessageBoxIcon.Warning);
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            if (e.CommandName == "particular")//查看
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("HiddenRectificationRecordEdit.aspx?HazardRegisterRecordId={0}&type=view", this.HazardRegisterRecordId, "查看 - ")));
            }
            if (e.CommandName == "del")//删除
            {
                if (BLL.CommonService.GetAllButtonPowerList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.HSSE_HiddenRectificationRecordMenuId, BLL.Const.BtnDelete))
                {
                    var getD = BLL.HSSE_Hazard_HazardRegisterRecordService.GetHazardRegisterRecordByHazardRegisterRecordId(HazardRegisterRecordId);
                    if (getD != null)
                    {
                        BLL.LogService.AddSys_Log(this.CurrUser, null, getD.HazardRegisterRecordId, BLL.Const.HSSE_HiddenRectificationRecordMenuId, BLL.Const.BtnDelete);
                        BLL.HSSE_Hazard_HazardRegisterRecordService.DeleteHazardRegisterRecordByHazardRegisterRecordId(HazardRegisterRecordId);

                        BindGrid();
                        ShowNotify("删除成功！", MessageBoxIcon.Success);
                    }
                }
                else
                {
                    Alert.ShowInTop("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                    return;
                }
            }
        }
        #endregion
    }
}