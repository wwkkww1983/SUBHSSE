using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;

namespace FineUIPro.Web.ServerCheck
{
    public partial class CheckNotice : PageBase
    {
        #region 定义项
        /// <summary>
        /// 监督检查主键
        /// </summary>
        public string CheckInfoId
        {
            get
            {
                return (string)ViewState["CheckInfoId"];
            }
            set
            {
                ViewState["CheckInfoId"] = value;
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
                this.InitTreeMenu();
                this.CheckInfoId = string.Empty;
            }
        }
        #endregion

        #region 加载树
        /// <summary>
        /// 加载树
        /// </summary>
        private void InitTreeMenu()
        {
            this.tvControlItem.Nodes.Clear();
            this.tvControlItem.ShowBorder = false;
            this.tvControlItem.ShowHeader = false;
            this.tvControlItem.EnableIcons = true;
            this.tvControlItem.AutoScroll = true;
            this.tvControlItem.EnableSingleClickExpand = true;
            TreeNode rootNode = new TreeNode
            {
                Text = "检查方式-年月",
                NodeID = "0",
                Expanded = true
            };

            this.tvControlItem.Nodes.Add(rootNode);
            var checkInfoLists = (from x in Funs.DB.Check_CheckInfo select x).ToList();
            if (!string.IsNullOrEmpty(this.txtCheckStartTimeS.Text))
            {
                checkInfoLists = checkInfoLists.Where(x => x.CheckStartTime >= Funs.GetNewDateTime(this.txtCheckStartTimeS.Text)).ToList();
            }
            if (!string.IsNullOrEmpty(this.txtCheckEndTimeS.Text))
            {
                checkInfoLists = checkInfoLists.Where(x => x.CheckEndTime <= Funs.GetNewDateTime(this.txtCheckEndTimeS.Text)).ToList();
            }

            var checkTypeList = (from x in checkInfoLists select x.CheckTypeName).Distinct();
            foreach (var item in checkTypeList)
            {
                TreeNode rootUnitNode = new TreeNode
                {
                    Text = item,
                    NodeID = item,
                    Expanded = true,
                    ToolTip = "检查方式"
                };//定义根节点
                rootNode.Nodes.Add(rootUnitNode);
                var checkInfoList = (from x in checkInfoLists where x.CheckTypeName == item select x).ToList();
                this.BindNodes(rootUnitNode, checkInfoList);
            }
        }
        #endregion

        #region 绑定树节点
        /// <summary>
        ///  绑定树节点
        /// </summary>
        /// <param name="node"></param>
        private void BindNodes(TreeNode node, List<Model.Check_CheckInfo> checkInfoList)
        {
            if (node.ToolTip == "检查方式")
            {
                var pointListMonth = (from x in checkInfoList
                                      orderby x.CheckStartTime descending
                                      select string.Format("{0:yyyy-MM}", x.CheckStartTime)).Distinct();
                foreach (var item in pointListMonth)
                {
                    TreeNode newNode = new TreeNode
                    {
                        Text = item,
                        NodeID = item + "|" + node.NodeID,
                        ToolTip = "月份"
                    };
                    node.Nodes.Add(newNode);
                    this.BindNodes(newNode, checkInfoList);
                }
            }
            else if (node.ToolTip == "月份")
            {
                var dReports = from x in checkInfoList
                               where string.Format("{0:yyyy-MM}", x.CheckStartTime) == node.Text
                               orderby x.CheckStartTime descending
                               select x;
                foreach (var item in dReports)
                {
                    TreeNode newNode = new TreeNode();
                    var units = BLL.UnitService.GetUnitByUnitId(item.SubjectUnitId);
                    if (units != null)
                    {
                        newNode.Text = (item.CheckStartTime.Value.Day).ToString().PadLeft(2, '0') + "日：" + units.UnitName;
                    }
                    else
                    {
                        newNode.Text = (item.CheckStartTime.Value.Day).ToString().PadLeft(2, '0') + "日：未知单位";
                    }
                    newNode.NodeID = item.CheckInfoId;
                    newNode.EnableClickEvent = true;
                    node.Nodes.Add(newNode);
                }
            }
        }
        #endregion

        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvControlItem_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            this.CheckInfoId = this.tvControlItem.SelectedNodeID;          
            this.txtCheckType.Text = this.tvControlItem.SelectedNode.ParentNode.ParentNode.Text;
            this.PageInfoLoad(); ///页面输入保存信息
            this.BindGrid1();
            this.BindGrid2();
        }
        #endregion

        #region 加载页面输入保存信息
        /// <summary>
        /// 加载页面输入保存信息
        /// </summary>
        private void PageInfoLoad()
        {
            var checkInfo = Funs.DB.Check_CheckInfo.FirstOrDefault(x => x.CheckInfoId == this.CheckInfoId);
            if (checkInfo != null)
            {
                this.txtCheckStartTime.Text = string.Format("{0:yyyy-MM-dd}", checkInfo.CheckStartTime);
                this.txtCheckEndTime.Text = string.Format("{0:yyyy-MM-dd}", checkInfo.CheckEndTime);
                this.drpSubjectUnit.Text = BLL.UnitService.GetUnitNameByUnitId(checkInfo.SubjectUnitId);
                this.txtCheckType.Text = checkInfo.CheckTypeName;
                this.txtSubjectUnitMan.Text = checkInfo.SubjectUnitMan;
                this.txtSubjectUnitAdd.Text = checkInfo.SubjectUnitAdd;
                this.txtSubjectUnitTel.Text = checkInfo.SubjectUnitTel;
                this.txtSubjectObject.Text = checkInfo.SubjectObject;
                this.txtCheckTeamLeader.Text =checkInfo.CheckTeamLeader;
                this.txtCompileMan.Text = checkInfo.CompileMan;
                this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", checkInfo.CompileDate);
            }
            else
            {
                this.drpSubjectUnit.Text = string.Empty;
                this.txtSubjectObject.Text = string.Empty;
                this.txtSubjectUnitMan.Text = string.Empty;
                this.txtSubjectUnitTel.Text = string.Empty;
                this.txtSubjectUnitAdd.Text = string.Empty;
                this.txtCheckStartTime.Text = string.Empty;
                this.txtCheckEndTime.Text = string.Empty;
                this.txtCheckType.Text = string.Empty;
                this.txtCheckTeamLeader.Text = string.Empty;
                this.txtCompileMan.Text = string.Empty;
                this.txtCompileDate.Text = string.Empty;
                this.CheckInfoId = string.Empty;
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid1()
        {
            string strSql = @"SELECT CheckFileId,CheckInfoId,CheckFileName,SortIndex,Remark FROM dbo.Check_CheckInfo_CheckFile WHERE 1=1 ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND CheckInfoId = @CheckInfoId";
            listStr.Add(new SqlParameter("@CheckInfoId", this.CheckInfoId));           
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid1.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid1.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid2()
        {
            string strSql = @"SELECT CheckTeamId,CheckInfoId,UserName,Sex,UnitName,SortIndex,PostName,WorkTitle,CheckPostName,CheckDate  FROM dbo.Check_CheckInfo_CheckTeam"               
                + @" WHERE 1=1  ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += " AND CheckInfoId = @CheckInfoId";
            listStr.Add(new SqlParameter("@CheckInfoId", this.CheckInfoId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            Grid2.RecordCount = tb.Rows.Count;
            tb = GetFilteredTable(Grid2.FilteredData, tb);
            var table = this.GetPagedDataTable(Grid2, tb);
            Grid2.DataSource = table;
            Grid2.DataBind();
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
            BindGrid1();
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid2();
        }
        #endregion

        #region 查看检查办法
        /// <summary>
        /// 查看检查办法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFind_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Window3.GetShowReference(String.Format("CheckInfoTemplate.aspx")));
        }
        #endregion

        #region 组面板 折叠展开事件
        /// <summary>
        /// 组面板 折叠展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Gridl_Collapse(object sender, EventArgs e)
        {
            if (this.Grid1.Collapsed)
            {
                this.Grid2.Collapsed = false;
            }
        }

        /// <summary>
        /// 组面板 折叠展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid2_Collapse(object sender, EventArgs e)
        {
            if (this.Grid2.Collapsed)
            {
                this.Grid1.Collapsed = false;
            }
        }

        /// <summary>
        /// 组面板 折叠展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Gridl_Expand(object sender, EventArgs e)
        {
            if (this.Grid1.Expanded)
            {
                this.Grid2.Expanded = false;
            }
        }

        /// <summary>
        /// 组面板 折叠展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid2_Expand(object sender, EventArgs e)
        {
            if (this.Grid2.Expanded)
            {
                this.Grid1.Expanded = false;
            }
        }
        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Tree_TextChanged(object sender, EventArgs e)
        {
            this.InitTreeMenu();
            this.BindGrid1();
            this.BindGrid2();
        }
    }
}