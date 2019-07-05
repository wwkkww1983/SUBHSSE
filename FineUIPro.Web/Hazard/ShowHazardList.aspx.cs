using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;

namespace FineUIPro.Web.Hazard
{
    public partial class ShowHazardList : PageBase
    {
        #region 定义集合
        /// <summary>
        /// 定义集合
        /// </summary>
        private static string id = string.Empty;
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
                id = string.Empty;
                InitTreeMenu();
            }
        }
        #endregion

        #region 加载树
        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeMenu()
        {
            trHazardListType.Nodes.Clear();
            trHazardListType.ShowBorder = false;
            trHazardListType.ShowHeader = false;
            trHazardListType.EnableIcons = true;
            trHazardListType.AutoScroll = true;
            trHazardListType.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "危险源清单",
                NodeID = "0",
                Expanded = true
            };
            this.trHazardListType.Nodes.Add(rootNode);
            BoundTree(rootNode.Nodes, "0");
        }

        private void BoundTree(TreeNodeCollection nodes, string menuId)
        {
            var dt = GetNewHazardListType(menuId);
            if (dt.Count() > 0)
            {
                TreeNode tn = null;
                foreach (var dr in dt)
                {
                    tn = new TreeNode
                    {
                        Text = dr.HazardListTypeName,
                        ToolTip = dr.HazardListTypeName,
                        NodeID = dr.HazardListTypeId,
                        EnableClickEvent = true
                    };
                    nodes.Add(tn);
                    BoundTree(tn.Nodes, dr.HazardListTypeId);
                }
            }
        }
        #endregion

        #region 得到菜单方法
        /// <summary>
        /// 得到菜单方法
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<Model.Technique_HazardListType> GetNewHazardListType(string parentId)
        {
            return (from x in Funs.DB.Technique_HazardListType where x.SupHazardListTypeId == parentId orderby x.HazardListTypeCode select x).ToList(); ;
        }
        #endregion

        #region 点击树节点
        /// <summary>
        /// 点击树节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trHazardListType_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region 绑定Grid
        /// <summary>
        /// 绑定Grid
        /// </summary>
        private void BindGrid()
        {
            if (this.trHazardListType.SelectedNode != null)
            {
                string strSql = "select * from View_Technique_HazardList where HazardListTypeId=@HazardListTypeId and IsPass=@IsPass ";

                List<SqlParameter> listStr = new List<SqlParameter>();
                listStr.Add(new SqlParameter("@HazardListTypeId", this.trHazardListType.SelectedNode.NodeID));
                listStr.Add(new SqlParameter("@IsPass", true));
                if (!string.IsNullOrEmpty(this.HazardCode.Text.Trim()))
                {
                    strSql += " AND HazardCode LIKE @HazardCode";
                    listStr.Add(new SqlParameter("@HazardCode", "%" + this.HazardCode.Text.Trim() + "%"));
                }
                if (!string.IsNullOrEmpty(this.HazardListTypeCode.Text.Trim()))
                {
                    strSql += " AND HazardListTypeCode LIKE @HazardListTypeCode";
                    listStr.Add(new SqlParameter("@HazardListTypeCode", "%" + this.HazardListTypeCode.Text.Trim() + "%"));
                }
                SqlParameter[] parameter = listStr.ToArray();
                DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

                Grid1.RecordCount = tb.Rows.Count;
                tb = GetFilteredTable(Grid1.FilteredData, tb);
                var table = this.GetPagedDataTable(Grid1, tb);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
        }
        #endregion

        #region 文本框查询事件
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion

        #region 表头过滤、分页、排序
        /// <summary>
        /// 表头过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
            BindGrid();
        }

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

        /// <summary>
        /// Grid排序
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
                    if (string.IsNullOrEmpty(id))
                    {
                        id = rowID;
                    }
                    else
                    {
                        //this.Grid1.SelectedRowID = null;
                        //this.Grid1.Rows[e.RowIndex].Values[0] = false;
                        Alert.ShowInTop("只能选择一项！", MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
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
            if (string.IsNullOrEmpty(id))
            {
                ShowNotify("请选择一项！", MessageBoxIcon.Warning);
                return;
            }
            string ids = string.Empty;
            Model.Technique_HazardList hazardList = BLL.HazardListService.GetHazardListById(id);
            if (hazardList != null)
            {
                ids = id + "," + hazardList.DefectsType + "," + hazardList.MayLeadAccidents;
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(ids)
             + ActiveWindow.GetHidePostBackReference());
        }
        #endregion
    }
}