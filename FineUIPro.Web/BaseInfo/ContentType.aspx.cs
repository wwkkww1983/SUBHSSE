using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.BaseInfo
{
    public partial class ContentType : PageBase
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
            var q = from x in Funs.DB.Exchange_ContentType orderby x.ContentTypeCode select x;
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
        private List<Model.Exchange_ContentType> GetPagedDataTable(int pageIndex, int pageSize)
        {
            List<Model.Exchange_ContentType> source = (from x in BLL.Funs.DB.Exchange_ContentType orderby x.ContentTypeCode select x).ToList();
            List<Model.Exchange_ContentType> paged = new List<Model.Exchange_ContentType>();

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
            if (judgementDelete(hfFormID.Text, true))
            {
                var getV = BLL.ContentTypeService.GetContentType(hfFormID.Text);
                if (getV != null)
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, getV.ContentTypeCode, getV.ContentTypeId, BLL.Const.ContentTypeMenuId, BLL.Const.BtnDelete);
                    BLL.ContentTypeService.DeleteContentType(hfFormID.Text);

                    // 重新绑定表格，并模拟点击[新增按钮]
                    BindGrid();
                    PageContext.RegisterStartupScript("onNewButtonClick();");
                }
            }
        }

        /// <summary>
        /// 右键删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuDelete_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length > 0)
            {
                foreach (int rowIndex in Grid1.SelectedRowIndexArray)
                {
                    string rowID = Grid1.DataKeys[rowIndex][0].ToString();
                    if (judgementDelete(rowID, true))
                    {
                        var getV = BLL.ContentTypeService.GetContentType(rowID);
                        if (getV != null)
                        {
                            BLL.LogService.AddSys_Log(this.CurrUser, getV.ContentTypeCode, getV.ContentTypeId, BLL.Const.ContentTypeMenuId, BLL.Const.BtnDelete);
                            BLL.ContentTypeService.DeleteContentType(rowID);
                        }
                    }
                }
                BindGrid();
                PageContext.RegisterStartupScript("onNewButtonClick();");
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        private bool judgementDelete(string id, bool isShow)
        {
            string content = string.Empty;
            if (Funs.DB.Exchange_Content.FirstOrDefault(x => x.ContentTypeId == id) != null)
            {
                content = "该交流话题类型已在【内容管理】中使用，不能删除！";
            }
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

        #region 编辑
        /// <summary>
        /// 右键编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMenuEdit_Click(object sender, EventArgs e)
        {
            if (Grid1.SelectedRowIndexArray.Length == 0)
            {
                Alert.ShowInTop("请至少选择一条记录！");
                return;
            }
            string Id = Grid1.SelectedRowID;
            var contentType = BLL.ContentTypeService.GetContentType(Id);
            if (contentType != null)
            {
                this.txtContentTypeCode.Text = contentType.ContentTypeCode;
                this.txtContentTypeName.Text = contentType.ContentTypeName;
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
            Model.Exchange_ContentType depart = new Model.Exchange_ContentType
            {
                ContentTypeCode = this.txtContentTypeCode.Text.Trim(),
                ContentTypeName = this.txtContentTypeName.Text.Trim()
            };
            if (string.IsNullOrEmpty(strRowID))
            {
                depart.ContentTypeId = SQLHelper.GetNewID(typeof(Model.Exchange_ContentType));
                BLL.ContentTypeService.AddContentType(depart);
                BLL.LogService.AddSys_Log(this.CurrUser, depart.ContentTypeCode, depart.ContentTypeId, BLL.Const.ContentTypeMenuId, BLL.Const.BtnAdd);
            }
            else
            {
                depart.ContentTypeId = strRowID;
                BLL.ContentTypeService.UpdateContentType(depart);
                BLL.LogService.AddSys_Log(this.CurrUser, depart.ContentTypeCode, depart.ContentTypeId, BLL.Const.ContentTypeMenuId, BLL.Const.BtnModify);
            }
            this.SimpleForm1.Reset();
            // 重新绑定表格，并点击当前编辑或者新增的行
            BindGrid();
            PageContext.RegisterStartupScript(String.Format("F('{0}').selectRow('{1}');", Grid1.ClientID, depart.ContentTypeId));
        }
        #endregion

        #region 验证交流话题类型名称、编号是否存在
        /// <summary>
        /// 验证交流话题类型名称、编号是否存在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            var q = Funs.DB.Exchange_ContentType.FirstOrDefault(x => x.ContentTypeCode == this.txtContentTypeCode.Text.Trim() && (x.ContentTypeId != hfFormID.Text || (hfFormID.Text == null && x.ContentTypeId != null)));
            if (q != null)
            {
                ShowNotify("输入的类别编号已存在！", MessageBoxIcon.Warning);
            }

            var q2 = Funs.DB.Exchange_ContentType.FirstOrDefault(x => x.ContentTypeName == this.txtContentTypeName.Text.Trim() && (x.ContentTypeId != hfFormID.Text || (hfFormID.Text == null && x.ContentTypeId != null)));
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
            var buttonList = BLL.CommonService.GetAllButtonList(this.CurrUser.LoginProjectId, this.CurrUser.UserId, BLL.Const.ContentTypeMenuId);
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