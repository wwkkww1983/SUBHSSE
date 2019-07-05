using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Check
{
    public partial class ShowCheckItem : PageBase
    {
        #region 定义集合
        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<string> list = new List<string>();
        private static List<string> parentIds = new List<string>();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                list = new List<string>();
                parentIds = new List<string>();
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                CheckItemSetDataBind();//加载树
            }
        }

        private void BindGrid()
        {
            string ids = string.Empty;

            this.Grid1.DataSource = null;
            this.Grid1.DataBind();
            string strSql = @"select * from Check_ProjectCheckItemDetail where CheckItemSetId=@CheckItemSetId";
            List<SqlParameter> listStr = new List<SqlParameter>();
            listStr.Add(new SqlParameter("@CheckItemSetId", this.tvCheckItemSet.SelectedNodeID));
            if (!string.IsNullOrEmpty(Request.Params["CheckDayId"]))
            {
                List<Model.View_Check_CheckDayDetail> details = (from x in Funs.DB.View_Check_CheckDayDetail where x.CheckDayId == Request.Params["CheckDayId"] select x).ToList();
                if (details.Count() > 0)
                {
                    for (int i = 0; i < details.Count(); i++)
                    {
                        if (i == 0)
                        {
                            strSql += " AND CheckItemDetailId not in (@Ids" + i;
                        }
                        else
                        {
                            strSql += ",@Ids" + i;
                        }
                        listStr.Add(new SqlParameter("@Ids" + i, details[i].CheckItem));
                    }
                    strSql += ")";
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["CheckSpecialId"]))
            {
                List<Model.View_Check_CheckSpecialDetail> details = (from x in Funs.DB.View_Check_CheckSpecialDetail where x.CheckSpecialId == Request.Params["CheckSpecialId"] select x).ToList();
                if (details.Count() > 0)
                {
                    for (int i = 0; i < details.Count(); i++)
                    {
                        if (i == 0)
                        {
                            strSql += " AND CheckItemDetailId not in (@Ids" + i;
                        }
                        else
                        {
                            strSql += ",@Ids" + i;
                        }
                        listStr.Add(new SqlParameter("@Ids" + i, details[i].CheckItem));
                    }
                    strSql += ")";
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["CheckColligationId"]))
            {
                List<Model.View_Check_CheckColligationDetail> details = (from x in Funs.DB.View_Check_CheckColligationDetail where x.CheckColligationId == Request.Params["CheckColligationId"] select x).ToList();
                if (details.Count() > 0)
                {
                    for (int i = 0; i < details.Count(); i++)
                    {
                        if (i == 0)
                        {
                            strSql += " AND CheckItemDetailId not in (@Ids" + i;
                        }
                        else
                        {
                            strSql += ",@Ids" + i;
                        }
                        listStr.Add(new SqlParameter("@Ids" + i, details[i].CheckItem));
                    }
                    strSql += ")";
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["CheckWorkId"]))
            {
                List<Model.View_Check_CheckWorkDetail> details = (from x in Funs.DB.View_Check_CheckWorkDetail where x.CheckWorkId == Request.Params["CheckWorkId"] select x).ToList();
                if (details.Count() > 0)
                {
                    for (int i = 0; i < details.Count(); i++)
                    {
                        if (i == 0)
                        {
                            strSql += " AND CheckItemDetailId not in (@Ids" + i;
                        }
                        else
                        {
                            strSql += ",@Ids" + i;
                        }
                        listStr.Add(new SqlParameter("@Ids" + i, details[i].CheckItem));
                    }
                    strSql += ")";
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["CheckHolidayId"]))
            {
                List<Model.View_Check_CheckHolidayDetail> details = (from x in Funs.DB.View_Check_CheckHolidayDetail where x.CheckHolidayId == Request.Params["CheckHolidayId"] select x).ToList();
                if (details.Count() > 0)
                {
                    for (int i = 0; i < details.Count(); i++)
                    {
                        if (i == 0)
                        {
                            strSql += " AND CheckItemDetailId not in (@Ids" + i;
                        }
                        else
                        {
                            strSql += ",@Ids" + i;
                        }
                        listStr.Add(new SqlParameter("@Ids" + i, details[i].CheckItem));
                    }
                    strSql += ")";
                }
            }
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.RecordCount = tb.Rows.Count;
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

        #region 绑定树节点
        /// <summary>
        /// 绑定树节点
        /// </summary>
        private void CheckItemSetDataBind()
        {
            this.tvCheckItemSet.Nodes.Clear();
            this.tvCheckItemSet.SelectedNodeID = string.Empty;
            TreeNode rootNode = new TreeNode
            {
                Text = "检查项",
                Expanded = true,
                NodeID = "0"
            };//定义根节点

            this.tvCheckItemSet.Nodes.Add(rootNode);
            this.GetNodes(rootNode.Nodes, null);
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(TreeNodeCollection nodes, string parentId)
        {
            List<Model.Check_ProjectCheckItemSet> checkItemSet = null;
            if (parentId == null)
            {
                checkItemSet = (from x in BLL.Funs.DB.Check_ProjectCheckItemSet where x.SupCheckItem == "0" && x.CheckType == Request.Params["checkType"] && x.ProjectId == this.CurrUser.LoginProjectId orderby x.SortIndex select x).ToList();
            }
            else
            {
                checkItemSet = (from x in BLL.Funs.DB.Check_ProjectCheckItemSet where x.SupCheckItem == parentId && x.CheckType == Request.Params["checkType"] && x.ProjectId == this.CurrUser.LoginProjectId orderby x.SortIndex select x).ToList();
            }

            foreach (var q in checkItemSet)
            {
                var isEnd = BLL.Check_ProjectCheckItemSetService.IsEndLevel(q.CheckItemSetId);
                TreeNode newNode = new TreeNode
                {
                    Text = q.CheckItemName,
                    NodeID = q.CheckItemSetId,
                    EnableClickEvent = true,
                    EnableCheckBox = true
                };
                nodes.Add(newNode);
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                GetNodes(nodes[i].Nodes, nodes[i].NodeID);
            }
        }
        #endregion
        #endregion

        #region 点击TreeView
        /// <summary>
        /// 点击TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvCheckItemSet_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            if (this.tvCheckItemSet.SelectedNodeID != "0" && this.tvCheckItemSet.SelectedNode != null)
            {
                this.Grid1.DataSource = null;
                this.Grid1.DataBind();
                BindGrid();
            }
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

        #region 确认按钮
        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> newParentIds = new List<string>();
            TreeNode[] nodes = this.tvCheckItemSet.GetCheckedNodes();
            foreach (var item in nodes)
            {
                if (item.Nodes.Count == 0)
                {
                    parentIds.Add(item.NodeID);   //集合中增加末级节点
                    if (item.ParentNode != null)
                    {
                        newParentIds.Add(item.ParentNode.NodeID);    //记录已增加的末级节点的父级节点集合
                    }
                }
            }
            foreach (var item in nodes)
            {
                if (item.Nodes.Count > 0)
                {
                    if (!newParentIds.Contains(item.NodeID))
                    {
                        parentIds.Add(item.NodeID);
                    }
                }
            }
            if (parentIds.Count == 0 && list.Count == 0)
            {
                ShowNotify("请至少选择一项！", MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(Request.Params["CheckDayId"]))
            {
                foreach (var item in parentIds)
                {
                    Model.Check_CheckDayDetail detail = new Model.Check_CheckDayDetail
                    {
                        CheckDayDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckDayDetail)),
                        CheckDayId = Request.Params["CheckDayId"],
                        CheckItem = item
                    };
                    Model.Check_ProjectCheckItemDetail checkItemDetail = BLL.Check_ProjectCheckItemDetailService.GetCheckItemDetailById(item);
                    if (checkItemDetail != null)
                    {
                        detail.CheckContent = checkItemDetail.CheckContent;
                    }
                    detail.Unqualified = "隐患";
                    detail.Suggestions = "整改";
                    detail.CompleteStatus = true;
                    BLL.Check_CheckDayDetailService.AddCheckDayDetail(detail);
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["CheckSpecialId"]))
            {
                foreach (var item in parentIds)
                {
                    Model.Check_CheckSpecialDetail detail = new Model.Check_CheckSpecialDetail
                    {
                        CheckSpecialDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckSpecialDetail)),
                        CheckSpecialId = Request.Params["CheckSpecialId"],
                        CheckItem = item
                    };
                    Model.Check_ProjectCheckItemDetail checkItemDetail = BLL.Check_ProjectCheckItemDetailService.GetCheckItemDetailById(item);
                    if (checkItemDetail != null)
                    {
                        detail.CheckContent = checkItemDetail.CheckContent;
                    }
                    detail.Unqualified = "隐患";
                    detail.Suggestions = "整改";
                    detail.CompleteStatus = true;
                    BLL.Check_CheckSpecialDetailService.AddCheckSpecialDetail(detail);
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["CheckColligationId"]))
            {
                foreach (var item in parentIds)
                {
                    Model.Check_CheckColligationDetail detail = new Model.Check_CheckColligationDetail
                    {
                        CheckColligationDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckColligationDetail)),
                        CheckColligationId = Request.Params["CheckColligationId"],
                        CheckItem = item
                    };
                    Model.Check_ProjectCheckItemDetail checkItemDetail = BLL.Check_ProjectCheckItemDetailService.GetCheckItemDetailById(item);
                    if (checkItemDetail != null)
                    {
                        detail.CheckContent = checkItemDetail.CheckContent;
                    }
                    detail.Unqualified = "隐患";
                    detail.Suggestions = "整改";
                    detail.CompleteStatus = true;
                    BLL.Check_CheckColligationDetailService.AddCheckColligationDetail(detail);
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["CheckWorkId"]))
            {
                foreach (var item in parentIds)
                {
                    Model.Check_CheckWorkDetail detail = new Model.Check_CheckWorkDetail
                    {
                        CheckWorkDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckWorkDetail)),
                        CheckWorkId = Request.Params["CheckWorkId"],
                        CheckItem = item
                    };
                    Model.Check_ProjectCheckItemDetail checkItemDetail = BLL.Check_ProjectCheckItemDetailService.GetCheckItemDetailById(item);
                    if (checkItemDetail != null)
                    {
                        detail.CheckContent = checkItemDetail.CheckContent;
                    }
                    detail.CheckResult = "隐患";
                    detail.CheckOpinion = "整改";
                    BLL.Check_CheckWorkDetailService.AddCheckWorkDetail(detail);
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["CheckHolidayId"]))
            {
                foreach (var item in parentIds)
                {
                    Model.Check_CheckHolidayDetail detail = new Model.Check_CheckHolidayDetail
                    {
                        CheckHolidayDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckHolidayDetail)),
                        CheckHolidayId = Request.Params["CheckHolidayId"],
                        CheckItem = item
                    };
                    Model.Check_ProjectCheckItemDetail checkItemDetail = BLL.Check_ProjectCheckItemDetailService.GetCheckItemDetailById(item);
                    if (checkItemDetail != null)
                    {
                        detail.CheckContent = checkItemDetail.CheckContent;
                    }
                    detail.CheckResult = "隐患";
                    detail.CheckOpinion = "整改";
                    BLL.Check_CheckHolidayDetailService.AddCheckHolidayDetail(detail);
                }
            }

            if (!string.IsNullOrEmpty(Request.Params["CheckDayId"]))
            {
                foreach (var item in list)
                {
                    Model.Check_CheckDayDetail detail = new Model.Check_CheckDayDetail
                    {
                        CheckDayDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckDayDetail)),
                        CheckDayId = Request.Params["CheckDayId"],
                        CheckItem = item
                    };
                    Model.Check_ProjectCheckItemSet checkItemSet = BLL.Check_ProjectCheckItemSetService.GetCheckItemSetById(item);
                    if (checkItemSet != null)
                    {
                        detail.CheckContent = checkItemSet.CheckItemName;
                    }
                    detail.Unqualified = "隐患";
                    detail.Suggestions = "整改";
                    detail.CompleteStatus = true;
                    BLL.Check_CheckDayDetailService.AddCheckDayDetail(detail);
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["CheckSpecialId"]))
            {
                foreach (var item in list)
                {
                    Model.Check_CheckSpecialDetail detail = new Model.Check_CheckSpecialDetail
                    {
                        CheckSpecialDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckSpecialDetail)),
                        CheckSpecialId = Request.Params["CheckSpecialId"],
                        CheckItem = item
                    };
                    Model.Check_ProjectCheckItemSet checkItemSet = BLL.Check_ProjectCheckItemSetService.GetCheckItemSetById(item);
                    if (checkItemSet != null)
                    {
                        detail.CheckContent = checkItemSet.CheckItemName;
                    }
                    detail.Unqualified = "隐患";
                    detail.Suggestions = "整改";
                    detail.CompleteStatus = true;
                    BLL.Check_CheckSpecialDetailService.AddCheckSpecialDetail(detail);
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["CheckColligationId"]))
            {
                foreach (var item in list)
                {
                    Model.Check_CheckColligationDetail detail = new Model.Check_CheckColligationDetail
                    {
                        CheckColligationDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckColligationDetail)),
                        CheckColligationId = Request.Params["CheckColligationId"],
                        CheckItem = item
                    };
                    Model.Check_ProjectCheckItemSet checkItemSet = BLL.Check_ProjectCheckItemSetService.GetCheckItemSetById(item);
                    if (checkItemSet != null)
                    {
                        detail.CheckContent = checkItemSet.CheckItemName;
                    }
                    detail.Unqualified = "隐患";
                    detail.Suggestions = "整改";
                    detail.CompleteStatus = true;
                    BLL.Check_CheckColligationDetailService.AddCheckColligationDetail(detail);
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["CheckWorkId"]))
            {
                foreach (var item in list)
                {
                    Model.Check_CheckWorkDetail detail = new Model.Check_CheckWorkDetail
                    {
                        CheckWorkDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckWorkDetail)),
                        CheckWorkId = Request.Params["CheckWorkId"],
                        CheckItem = item
                    };
                    Model.Check_ProjectCheckItemSet checkItemSet = BLL.Check_ProjectCheckItemSetService.GetCheckItemSetById(item);
                    if (checkItemSet != null)
                    {
                        detail.CheckContent = checkItemSet.CheckItemName;
                    }
                    detail.CheckResult = "隐患";
                    detail.CheckOpinion = "整改";
                    BLL.Check_CheckWorkDetailService.AddCheckWorkDetail(detail);
                }
            }
            else if (!string.IsNullOrEmpty(Request.Params["CheckHolidayId"]))
            {
                foreach (var item in list)
                {
                    Model.Check_CheckHolidayDetail detail = new Model.Check_CheckHolidayDetail
                    {
                        CheckHolidayDetailId = SQLHelper.GetNewID(typeof(Model.Check_CheckHolidayDetail)),
                        CheckHolidayId = Request.Params["CheckHolidayId"],
                        CheckItem = item
                    };
                    Model.Check_ProjectCheckItemSet checkItemSet = BLL.Check_ProjectCheckItemSetService.GetCheckItemSetById(item);
                    if (checkItemSet != null)
                    {
                        detail.CheckContent = checkItemSet.CheckItemName;
                    }
                    detail.CheckResult = "隐患";
                    detail.CheckOpinion = "整改";
                    BLL.Check_CheckHolidayDetailService.AddCheckHolidayDetail(detail);
                }
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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
            //string parentId = Grid1.DataKeys[e.RowIndex][1].ToString();
            if (e.CommandName == "IsSelected")
            {
                CheckBoxField checkField = (CheckBoxField)Grid1.FindColumn("ckbIsSelected");
                if (checkField.GetCheckedState(e.RowIndex))
                {
                    if (!list.Contains(rowID))
                    {
                        list.Add(rowID);
                    }
                    //if (!parentIds.Contains(parentId + "," + rowID))
                    //{
                    //    parentIds.Add(parentId + "," + rowID);
                    //}
                }
                else
                {
                    if (list.Contains(rowID))
                    {
                        list.Remove(rowID);
                    }
                    //if (parentIds.Contains(parentId + "," + rowID))
                    //{
                    //    parentIds.Remove(parentId + "," + rowID);
                    //}
                }
            }
        }
        #endregion
    }
}