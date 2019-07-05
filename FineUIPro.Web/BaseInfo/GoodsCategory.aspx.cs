using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class GoodsCategory : PageBase
    {
        #region 加载
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
                ddlPageSize.SelectedValue = Grid1.PageSize.ToString();
                // 绑定表格
                BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            var q = from x in Funs.DB.Base_GoodsCategory orderby x.GoodsCategoryCode select x;
            Grid1.RecordCount = q.Count();
            // 2.获取当前分页数据
            var table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        private List<Model.Base_GoodsCategory> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.Base_GoodsCategory> source = (from x in BLL.Funs.DB.Base_GoodsCategory orderby x.GoodsCategoryCode select x).ToList();
            List<Model.Base_GoodsCategory> paged = new List<Model.Base_GoodsCategory>();

            int rowbegin = pageIndex * pageSize;
            int rowend = (pageIndex + 1) * pageSize;
            if (rowend > source.Count())
            {
                rowend = source.Count();
            }

            for (int i = rowbegin; i < rowend; i++)
            {
                paged.Add(source[i]);
            }

            return paged;
        }

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
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        #region 分页下拉选择
        /// <summary>
        /// 分页下拉选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            BindGrid();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BLL.GoodsCategoryService.DeleteGoodsCategoryById(hfFormID.Text);
            BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除物资类别");
            // 重新绑定表格，并模拟点击[新增按钮]
            BindGrid();
            PageContext.RegisterStartupScript("onNewButtonClick();");

        }

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
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();

                    BLL.GoodsCategoryService.DeleteGoodsCategoryById(rowID);
                    BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "删除物资类别");
                }

                BindGrid();
                PageContext.RegisterStartupScript("onNewButtonClick();");
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            this.EditData();
        }

        /// <summary>
        /// 编辑数据方法
        /// </summary>
        private void EditData()
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！", MessageBoxIcon.Warning);
                return;
            }
            string Id = Grid1.SelectedRowID;
            var GoodsCategory = BLL.GoodsCategoryService.GetGoodsCategoryById(Id);
            if (GoodsCategory != null)
            {
                this.txtGoodsCategoryCode.Text = GoodsCategory.GoodsCategoryCode;
                this.txtGoodsCategoryName.Text = GoodsCategory.GoodsCategoryName;
                this.txtRemark.Text = GoodsCategory.Remark;
                hfFormID.Text = Id;
                this.btnDelete.Enabled = true;
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strRowID = hfFormID.Text;
            Model.Base_GoodsCategory GoodsCategory = new Model.Base_GoodsCategory
            {
                GoodsCategoryCode = this.txtGoodsCategoryCode.Text.Trim(),
                GoodsCategoryName = this.txtGoodsCategoryName.Text.Trim(),
                Remark = txtRemark.Text.Trim()
            };
            if (string.IsNullOrEmpty(strRowID))
            {
                GoodsCategory.GoodsCategoryId = SQLHelper.GetNewID(typeof(Model.Base_GoodsCategory));
                BLL.GoodsCategoryService.AddGoodsCategory(GoodsCategory);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加物资类别");
            }
            else
            {
                GoodsCategory.GoodsCategoryId = strRowID;
                BLL.GoodsCategoryService.UpdateGoodsCategory(GoodsCategory);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改物资类别");
            }
            this.SimpleForm1.Reset();
            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, GoodsCategory.GoodsCategoryId));
        }
        #endregion

        #region 验证费用类别名称、编号是否存在
        /// <summary>
        /// 验证费用类别名称、编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Base_GoodsCategory.FirstOrDefault(x => x.GoodsCategoryCode == this.txtGoodsCategoryCode.Text.Trim() && (x.GoodsCategoryId != hfFormID.Text || (hfFormID.Text == null && x.GoodsCategoryId != null)));
            if (q != null)
            {
                ShowNotify("输入的类别编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Base_GoodsCategory.FirstOrDefault(x => x.GoodsCategoryName == this.txtGoodsCategoryName.Text.Trim() && (x.GoodsCategoryId != hfFormID.Text || (hfFormID.Text == null && x.GoodsCategoryId != null)));
            if (q2 != null)
            {
                ShowNotify("输入的类别名称已存在！", MessageBoxIcon.Warning);
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.GoodsCategoryMenuId);
            if (buttonList.Count() > 0)
            {
                if (buttonList.Contains(BLL.Const.BtnAdd))
                {
                    this.btnNew.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnModify))
                {
                    this.btnMenuEdit.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnDelete))
                {
                    this.btnDelete.Hidden = false;
                    this.btnMenuDelete.Hidden = false;
                }
                if (buttonList.Contains(BLL.Const.BtnSave))
                {
                    this.btnSave.Hidden = false;
                }
            }
        }
        #endregion        
    }
}