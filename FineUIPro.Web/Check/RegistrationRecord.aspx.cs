using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class RegistrationRecord : PageBase
    {
        #region 定义项
        /// <summary>
        /// 隐患巡检记录主键
        /// </summary>
        public string RegistrationRecordId
        {
            get
            {
                return (string)ViewState["RegistrationRecordId"];
            }
            set
            {
                ViewState["RegistrationRecordId"] = value;
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
                ////权限按钮方法
                this.GetButtonPower();
                this.btnMenuDelete.ConfirmText = String.Format("你确定要删除选中的&nbsp;<b><script>{0}</script></b>&nbsp;行数据吗？", Grid1.GetSelectedCountReference());
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                this.drpCheckMan.DataTextField = "UserName";
                this.drpCheckMan.DataValueField = "UserId";
                this.drpCheckMan.DataSource = (from x in Funs.DB.View_Inspection_Registration
                                               join y in Funs.DB.Sys_User
                                               on x.CheckManId equals y.UserId
                                               where x.ProjectId == this.CurrUser.LoginProjectId
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
            var registrationList = (from x in db.View_Inspection_Registration
                                    where x.ProjectId == this.CurrUser.LoginProjectId
                                    select x).ToList();
            var checkManList = (from x in registrationList
                                select x.CheckManId).Distinct().ToList();   //获取巡检人集合
            var checkDateList = (from x in registrationList
                                 select x.CheckTime.Value.Date).Distinct().ToList();    //获取巡检日期集合
            foreach (var checkMan in checkManList)
            {
                foreach (var checkDate in checkDateList)
                {
                    Model.Inspection_RegistrationRecord oldRecord = BLL.RegistrationRecordService.GetRegisterRecordByCheckManAndDate(checkMan, checkDate);
                    if (oldRecord == null)
                    {
                        var list = from x in registrationList
                                   where x.CheckManId == checkMan && x.CheckTime.Value.Date == checkDate
                                   orderby x.CheckTime
                                   select x;
                        if (list.Count() > 0)
                        {
                            Model.Inspection_RegistrationRecord record = new Model.Inspection_RegistrationRecord
                            {
                                RegistrationRecordId = SQLHelper.GetNewID(typeof(Model.Inspection_RegistrationRecord)),
                                ProjectId = this.CurrUser.LoginProjectId
                            };
                            string registrationIds = string.Empty;
                            foreach (var item in list)
                            {
                                registrationIds += item.RegistrationId + ",";
                            }
                            if (!string.IsNullOrEmpty(registrationIds))
                            {
                                registrationIds = registrationIds.Substring(0, registrationIds.LastIndexOf(","));
                            }
                            record.RegistrationIds = registrationIds;
                            record.CheckDate = checkDate;
                            record.CheckPerson = checkMan;
                            record.CompileMan = this.CurrUser.UserId;
                            record.CompileDate = DateTime.Now;
                            db.Inspection_RegistrationRecord.InsertOnSubmit(record);
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
            string strSql = "select * from View_RegistrationRecord  Where ProjectId=@ProjectId ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@ProjectId", this.CurrUser.LoginProjectId));
            if (this.drpCheckMan.SelectedValue != BLL.Const._Null)
            {
                strSql += " And CheckPerson=@CheckPerson ";
                listStr.Add(new SqlParameter("@CheckPerson", this.drpCheckMan.SelectedValue));
            }
            if (!string.IsNullOrEmpty(this.txtDate.Text.Trim()))
            {
                strSql += " And Date like @Date ";
                listStr.Add(new SqlParameter("@Date", "%" + this.txtDate.Text.Trim() + "%"));
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
                        var RegistrationRecord = BLL.RegistrationRecordService.GetRegisterRecordByRegisterRecordId(rowID);
                        if (RegistrationRecord != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, null, RegistrationRecord.RegistrationRecordId, BLL.Const.RegistrationRecordMenuId, BLL.Const.BtnDelete);

                            BLL.RegistrationRecordService.DeleteRegisterRecordByRegisterRecordId(rowID);

                            BindGrid();
                            ShowNotify("删除数据成功!（表格数据已重新绑定）", MessageBoxIcon.Success);
                        }
                    }
                }
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

        #region Grid双击事件
        /// <summary>
        /// Grid行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            btnMenuSee_Click(null, null);
        }
        #endregion

        #region 查看
        /// <summary>
        /// 查看按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuSee_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string RegistrationRecordId = Grid1.SelectedRowID;
            var registrationRecord = BLL.RegistrationRecordService.GetRegisterRecordByRegisterRecordId(RegistrationRecordId);
            if (registrationRecord != null)
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("RegistrationRecordView.aspx?RegistrationRecordId={0}", RegistrationRecordId, "查看 - ")));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.RegistrationRecordMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnMenuDelete.Hidden = false;
                }
            }
        }
        #endregion
    }
}