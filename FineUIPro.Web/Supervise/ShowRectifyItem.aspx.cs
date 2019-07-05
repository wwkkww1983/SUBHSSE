using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.IO;
using System.Text;

namespace FineUIPro.Web.Supervise
{
    public partial class ShowRectifyItem : PageBase
    {
        #region 定义集合
        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<string> list = new List<string>();
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
                list = new List<string>();
                string lists = Request.Params["lists"];
                list = Funs.GetStrListByStr(lists, ',');
                InitTreeMenu();
            }
        }
        #endregion

        #region 加载树
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.trRectify.Nodes.Clear();
            this.trRectify.ShowBorder = false;
            this.trRectify.ShowHeader = false;
            this.trRectify.EnableIcons = true;
            this.trRectify.AutoScroll = true;
            this.trRectify.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "作业类别",
                NodeID = "0",
                Expanded = true
            };
            this.trRectify.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes, "0");
        }

        /// <summary>
        /// 加载树
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="menuId"></param>
        private void BoundTree(TreeNodeCollection nodes, string menuId)
        {
            var dt = BLL.RectifyService.GetRectifyBySupRectifyId(menuId);
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    tn = new TreeNode
                    {
                        Text = dr.RectifyName,
                        NodeID = dr.RectifyId,
                        EnableClickEvent = true
                    };
                    nodes.Add(tn);
                    BoundTree(tn.Nodes, dr.RectifyId);
                }
            }
        }
        #endregion

        #region 树点击事件
        /// <summary>
        /// tree点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trRectify_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 加载Grid
        /// <summary>
        /// 加载Grid
        /// </summary>
        private void BindGrid()
        {
            string strSql = "select * from View_Technique_RectifyItem where RectifyId=@RectifyId and IsPass=@IsPass";
            SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@RectifyId",this.trRectify.SelectedNode.NodeID),
                        new SqlParameter("@IsPass",true)
                    };
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);

            Grid1.DataSource = table;
            Grid1.DataBind();

            if (list.Count() > 0)
            {
                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                     string id = Grid1.DataKeys[i][0].ToString();
                     if (list.Contains(id))
                     {
                         Grid1.Rows[i].Values[0] = true;
                     }
                }
            }
        }
        #endregion

        #region 过滤表头、分页、排序、关闭窗口
        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        /// <summary>
        /// Grid1排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        /// <summary>
        /// 分页下拉选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }

        #endregion

        #region 确认按钮
        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (list.Count == 0)
            {
                ShowNotify("请至少选择一项！",MessageBoxIcon.Warning);
                return;
            }
            string str = string.Empty;            
            foreach (var item in list)
            {
                str += item + ",";
            }
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(str)
                   + ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region Grid行点击事件
        /// <summary>
        /// Grid1行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string rowID = Grid1.DataKeys[e.RowIndex][0].ToString();
            if (e.CommandName == "IsSelected")
            {
                CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                if (checkField.GetCheckedState(e.RowIndex))
                {
                    if (!list.Contains(rowID))
                    {
                        list.Add(rowID);
                    }
                }
                else
                {
                    if (list.Contains(rowID))
                    {
                        list.Remove(rowID);
                    }
                }
            }
        }
        #endregion
    }
}