using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class GoodsDef : PageBase
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
                BLL.GoodsCategoryService.InitUnitDropDownList(this.drpGoodsDefCode, true);
                // 绑定表格
                this.BindGrid();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            var q = from x in Funs.DB.Base_GoodsDef orderby x.GoodsDefCode select x;
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
        private List<Model.Base_GoodsDef> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.Base_GoodsDef> source = (from x in BLL.Funs.DB.Base_GoodsDef orderby x.GoodsDefCode select x).ToList();
            List<Model.Base_GoodsDef> paged = new List<Model.Base_GoodsDef>();
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
        /// 改变索引事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

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

        /// <summary>
        /// 过滤表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_FilterChange(object sender, EventArgs e)
        {
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
            BLL.GoodsDefService.DeleteGoodsDefById(hfFormID.Text);
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

                    BLL.GoodsDefService.DeleteGoodsDefById(rowID);
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
            var goodsDef = BLL.GoodsDefService.GetGoodsDefById(Id);
            if (goodsDef != null)
            {
                if (!string.IsNullOrEmpty(goodsDef.GoodsDefCode))
                {
                    this.drpGoodsDefCode.SelectedValue = goodsDef.GoodsDefCode;
                }                            
                this.txtGoodsDefName.Text = goodsDef.GoodsDefName;
                this.txtRemark.Text = goodsDef.Remark;
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
            Model.Base_GoodsDef goodsDef = new Model.Base_GoodsDef();
            if (this.drpGoodsDefCode.SelectedValue != BLL.Const._Null)
            {
                goodsDef.GoodsDefCode = this.drpGoodsDefCode.SelectedValue;
            }
            goodsDef.GoodsDefName = this.txtGoodsDefName.Text.Trim();
            goodsDef.Remark = txtRemark.Text.Trim();
            if (string.IsNullOrEmpty(strRowID))
            {
                goodsDef.GoodsDefId = SQLHelper.GetNewID(typeof(Model.Base_GoodsDef));
                BLL.GoodsDefService.AddGoodsDef(goodsDef);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "添加物资名称");
            }
            else
            {
                goodsDef.GoodsDefId = strRowID;
                BLL.GoodsDefService.UpdateGoodsDef(goodsDef);
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "修改物资名称");
            }
            this.SimpleForm1.Reset();
            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, goodsDef.GoodsDefId));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.GoodsDefMenuId);
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

        #region 格式化字符串
        /// <summary>
        /// 获取工作阶段
        /// </summary>
        /// <param name="WorkStage"></param>
        /// <returns></returns>
        protected string ConvertGoodsDefCode(object GoodsDefCode)
        {
            string name = string.Empty;
            if (GoodsDefCode != null)
            {
                var goosC =   BLL.GoodsCategoryService.GetGoodsCategoryById(GoodsDefCode.ToString());  
                if (goosC != null)
                {
                    name = goosC.GoodsCategoryName;
                }
            }
            return name;
        }
        #endregion

        /// <summary>
        /// 选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowClick(object sender, GridRowClickEventArgs e)
        {
            this.EditData();
        }
    }
}