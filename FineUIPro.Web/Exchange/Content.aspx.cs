using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace FineUIPro.Web.Exchange
{
    public partial class Content : PageBase
    {
        /// <summary>
        /// 附件路径
        /// </summary>
        public string FullAttachUrl
        {
            get
            {
                return (string)ViewState["FullAttachUrl"];
            }
            set
            {
                ViewState["FullAttachUrl"] = value;
            }
        }

        private static bool isPosts, IsReplies, IsDeletePosts;   //发帖，回帖，删帖的权限

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetButtonPower();
                this.rblContentType.DataTextField = "ContentTypeName";
                rblContentType.DataValueField = "ContentTypeId";
                rblContentType.DataSource = BLL.ContentTypeService.GetContentTypeListAndNew();
                rblContentType.DataBind();
                rblContentType.SelectedIndex = 0;
                //this.Region2.Visible = false;
                // 表头过滤
                //FilterDataRowItem = FilterDataRowItemImplement;

                //btnNew.OnClientClick = Window1.GetShowReference("HAZOPEdit.aspx") + "return false;";
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                if (this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    isPosts = true;
                    IsReplies = true;
                    IsDeletePosts = true;
                }
                else
                {
                    isPosts = Convert.ToBoolean(this.CurrUser.IsPosts);
                    IsReplies = Convert.ToBoolean(this.CurrUser.IsReplies);
                    IsDeletePosts = Convert.ToBoolean(this.CurrUser.IsDeletePosts);
                }

                this.BindGrid2();
                this.ShowGrid1();
                this.BindGrid1();

            }
        }

        /// <summary>
        /// 加载Grid2
        /// </summary>
        private void BindGrid2()
        {
            if (this.rblContentType.SelectedValue == null)
            {
                return;
            }
            string contentTypeId = this.rblContentType.SelectedValue;
            if (!string.IsNullOrEmpty(contentTypeId))
            {
                string strSql = string.Empty;
                SqlParameter[] parameter = new SqlParameter[] { };
                if (contentTypeId == "最新")
                {
                    strSql = "select * from View_Exchange_Content where dateadd(month,1,CompileDate)>=getdate() order by CompileDate desc";
                    parameter = null;
                }
                else
                {
                    strSql = "select * from dbo.View_Exchange_Content where ContentTypeId = @ContentTypeId order by CompileDate desc";
                    parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@ContentTypeId",contentTypeId),
                    };
                }
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid2.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid2.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid2, tb);

                Grid2.DataSource = table;
                Grid2.DataBind();
                if (Grid2.Rows.Count > 0)
                {
                    Grid2.SelectedRowIndex = 0;
                }
            }
        }

        /// <summary>
        /// 加载Grid1
        /// </summary>
        private void BindGrid1()
        {
            if (Grid2.SelectedRowIndex < 0)
            {
                return;
            }
            string contentId = this.Grid2.SelectedRow.DataKeys[0].ToString();
            if (!string.IsNullOrEmpty(contentId))
            {
                string strSql = "select * from dbo.View_Exrchange_ReContent where ContentId= @ContentId order by CompileDate desc";
                SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@ContentId",contentId),
                    };
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
                Grid1.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, tb);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
        }

        /// <summary>
        /// 选择单位筛选HAZOP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid2_RowSelect(object sender, GridRowSelectEventArgs e)
        {
            //this.Region2.Visible = true;
            this.ShowGrid1();
            this.BindGrid1();
        }

        /// <summary>
        /// 显示主题
        /// </summary>
        private void ShowGrid1()
        {
            if (Grid2.SelectedRowIndex < 0)
            {
                return;
            }
            Model.Exchange_Content content = BLL.ContentService.GetContentById(Grid2.SelectedRow.DataKeys[0].ToString());
            if (content != null)
            {
                this.Panel2.Title = content.Theme;
                lbContents.Text = Funs.GetSubStr(content.Contents, 150);
                lbContents.ToolTip = content.Contents;
                Model.Sys_User u = BLL.UserService.GetUserByUserId(content.CompileMan);
                if (u != null)
                {
                    lbCompileMan.Text = u.UserName;
                }
                if (content.CompileDate != null)
                {
                    lbCompileDate.Text = string.Format("{0:yyyy-MM-dd}", content.CompileDate);
                }
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            rblContentType.DataSource = BLL.ContentTypeService.GetContentTypeListAndNew();
            rblContentType.DataBind();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            this.BindGrid2();
            this.BindGrid1();
            this.ShowGrid1();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Window3_Close(object sender, WindowCloseEventArgs e)
        {
            BindGrid1();
        }

        /// <summary>
        /// 表头过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid1();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid1();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid2_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid2.PageIndex = e.NewPageIndex;
            BindGrid2();
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            Model.Exchange_ReContent reContent = BLL.ReContentService.GetReContentById(rowID);
            if (reContent.CompileMan == this.CurrUser.UserId)
            {
                PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("ReContentSave.aspx?ReContentId={0}", rowID, "编辑 - ")));
            }
            else
            {
                ShowNotify("您不是回复人，无法编辑！", MessageBoxIcon.Warning);
            }
        }

        protected void btnEditReContent_Click(object sender, EventArgs e)
        {
            string rowID = Grid1.SelectedRowID;
            Model.Exchange_ReContent reContent = BLL.ReContentService.GetReContentById(rowID);
            if (reContent.CompileMan == this.CurrUser.UserId)
            {
                PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("ReContentSave.aspx?ReContentId={0}", rowID, "编辑 - ")));
            }
            else
            {
                ShowNotify("您不是回复人，无法编辑！", MessageBoxIcon.Warning);
            }
        }

        protected void btnDeleteReContent_Click(object sender, EventArgs e)
        {
            string rowID = Grid1.SelectedRowID;
            Model.Exchange_ReContent reContent = BLL.ReContentService.GetReContentById(rowID);
            if (reContent.CompileMan == this.CurrUser.UserId)
            {
                BLL.ReContentService.DeleteReContentById(rowID);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "删除回帖信息");
                BindGrid1();
                ShowNotify("删除数据成功!", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("您不是回复人，无法删除！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid1();
        }

        /// <summary>
        /// 分页显示条数下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid1();
        }

        #region 获取权限按钮
        /// <summary>
        /// 获取按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private void GetButtonPower()
        {
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ContentMenuId);
            if (buttonList.Count > 0)
            {
                if (IsReplies || this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    this.btnNewReContent.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    this.btnNewType.Hidden = false;
                    if (isPosts || this.CurrUser.UserId == BLL.Const.sysglyId)
                    {
                        this.btnNewContent.Hidden = false;
                        this.btnAdd.Hidden = false;
                    }
                }
                if (buttonList.Contains(BLL.Const.BtnModify) || this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    this.btnEditType.Hidden = false;
                    if (isPosts || this.CurrUser.UserId == BLL.Const.sysglyId)
                    {
                        this.btnEditContent.Hidden = false;
                        this.btnEditReContent.Hidden = false;
                    }
                }
                if (buttonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.UserId == BLL.Const.sysglyId)
                {
                    this.btnDeleteType.Hidden = false;
                    if (IsDeletePosts || this.CurrUser.UserId == BLL.Const.sysglyId)
                    {
                        this.btnDeleteContent.Hidden = false;
                        this.btnDeleteReContent.Hidden = false;
                    }
                }
            }
        }
        #endregion

        protected void btnNewType_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ContentTypeSave.aspx", "编辑 - ")));
        }



        protected void btnEditType_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("ContentTypeSave.aspx?ContentTypeId={0}", this.rblContentType.SelectedValue, "编辑 - ")));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsAllowDeleteContentType(this.rblContentType.SelectedValue))
            {
                BLL.ContentTypeService.DeleteContentType(this.rblContentType.SelectedValue);
                rblContentType.DataSource = BLL.ContentTypeService.GetContentTypeListAndNew();
                rblContentType.DataBind();
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "删除交流类型信息");
            }
            else
            {
                ShowNotify("该交流类型下存在主题，请先删除对应帖子信息！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 把时间转换为文字形式
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertCompileDate(object CompileDate)
        {
            if (CompileDate != null)
            {
                return string.Format("{0:yyyy-MM-dd}", CompileDate);
            }
            return "";
        }

        /// <summary>
        /// 是否允许删除话题类型
        /// </summary>
        /// <param name="contentTypeId"></param>
        /// <returns></returns>
        private bool IsAllowDeleteContentType(string contentTypeId)
        {
            return BLL.ContentService.IsExitContentType(contentTypeId);
        }

        protected void btnNewContent_Click(object sender, EventArgs e)
        {
            if (BLL.ContentTypeService.GetContentTypeLists().Count > 0)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("ContentSave.aspx", "编辑 - ")));
            }
            else
            {
                ShowNotify("请先增加话题类型！", MessageBoxIcon.Warning);
            }
        }

        #region 修改帖子
        /// <summary>
        /// 修改帖子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid2_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }

        protected void btnEditContent_Click(object sender, EventArgs e)
        {
            this.EditData();
        }
        private void EditData()
        {
            if (this.Grid2.SelectedRow != null)
            {
                Model.Exchange_Content content = BLL.ContentService.GetContentById(Grid2.SelectedRow.DataKeys[0].ToString());
                if (content != null)
                {
                    if (content.CompileMan == this.CurrUser.UserId || this.CurrUser.UserId == BLL.Const.sysglyId)
                    {
                        PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("ContentSave.aspx?ContentId={0}", Grid2.SelectedRow.DataKeys[0].ToString(), "编辑 - ")));
                    }
                    else
                    {
                        ShowNotify("只能修改自己发布的帖子信息！", MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                ShowNotify("请选择需要修改的主题！", MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 删除帖子
        /// <summary>
        /// 删除帖子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteContent_Click(object sender, EventArgs e)
        {
            if (this.Grid2.SelectedRow != null)
            {
                if (IsAllowDeleteContent(Grid2.SelectedRow.DataKeys[0].ToString()))
                {
                    BLL.ReContentService.DeleteAllReContentsById(Grid2.SelectedRow.DataKeys[0].ToString());
                    BLL.ContentService.DeleteContentById(Grid2.SelectedRow.DataKeys[0].ToString());
                    BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "删除帖子信息");
                    BindGrid2();
                    this.Grid1.DataSource = null;
                    this.Grid1.DataBind();
                    this.Panel2.Title = string.Empty;
                    lbContents.Text = string.Empty;
                    lbContents.ToolTip = string.Empty;
                    lbCompileMan.Text = string.Empty;
                    lbCompileDate.Text = string.Empty;
                }
            }
            else
            {
                ShowNotify("请选择需要删除的主题！", MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 是否允许删除主题帖子
        /// </summary>
        /// <param name="contentTypeId"></param>
        /// <returns></returns>
        private bool IsAllowDeleteContent(string contentId)
        {
            return true;
        }
        #endregion

        protected void rblContentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Region2.Visible = false;
            BindGrid2();
        }

        /// <summary>
        /// 查看帖子详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSee_Click(object sender, EventArgs e)
        {
            if (this.Grid2.SelectedRow != null)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("ContentSee.aspx?ContentId={0}", Grid2.SelectedRow.DataKeys[0].ToString(), "查看 - ")));
            }
            else
            {
                ShowNotify("请选择要查看的主题！", MessageBoxIcon.Warning);
            }
        }

        protected void btnNewReContent_Click(object sender, EventArgs e)
        {
            if (this.Grid2.SelectedRow != null)
            {
                PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("ReContentSave.aspx?ContentId={0}", Grid2.SelectedRow.DataKeys[0].ToString(), "编辑 - ")));
            }
            else
            {
                ShowNotify("请选择要回复的主题！", MessageBoxIcon.Warning);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (BLL.ContentTypeService.GetContentTypeLists().Count > 0)
            {
                PageContext.RegisterStartupScript(Window2.GetShowReference(String.Format("ContentSave.aspx", "编辑 - ")));
            }
            else
            {
                ShowNotify("请先增加话题类型！", MessageBoxIcon.Warning);
            }
        }
    }
}