using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data.SqlClient;
using System.Data;

namespace FineUIPro.Web.Manager
{
    public partial class ManagerTotalMonthView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        private string ManagerTotalMonthId
        {
            get
            {
                return (string)ViewState["ManagerTotalMonthId"];
            }
            set
            {
                ViewState["ManagerTotalMonthId"] = value;
            }
        }
        #endregion

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
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                this.ManagerTotalMonthId = Request.Params["ManagerTotalMonthId"];
                if (!string.IsNullOrEmpty(this.ManagerTotalMonthId))
                {
                    Model.Manager_ManagerTotalMonth ManagerTotalMonth = BLL.ManagerTotalMonthService.GetManagerTotalMonthById(this.ManagerTotalMonthId);
                    if (ManagerTotalMonth != null)
                    { ///读取编号
                        this.txtCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.ManagerTotalMonthId);
                        this.txtTitle.Text = ManagerTotalMonth.Title;
                        this.txtCompileDate.Text = string.Format("{0:yyyy-MM-dd}", ManagerTotalMonth.CompileDate);
                        this.drpCompileMan.Text = BLL.UserService.GetUserNameByUserId(ManagerTotalMonth.CompileMan);
                        this.txtMonthContent.Text = HttpUtility.HtmlDecode(ManagerTotalMonth.MonthContent);
                        this.txtMonthContent2.Text = HttpUtility.HtmlDecode(ManagerTotalMonth.MonthContent2);
                        this.txtMonthContent3.Text = HttpUtility.HtmlDecode(ManagerTotalMonth.MonthContent3);
                        //this.txtMonthContent4.Text = HttpUtility.HtmlDecode(ManagerTotalMonth.MonthContent4);
                        this.txtMonthContent5.Text = HttpUtility.HtmlDecode(ManagerTotalMonth.MonthContent5);
                        this.txtMonthContent6.Text = HttpUtility.HtmlDecode(ManagerTotalMonth.MonthContent6);
                    }     
                }

                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectManagerTotalMonthMenuId;
                this.ctlAuditFlow.DataId = this.ManagerTotalMonthId;

                #region 本月HSE工作存在问题与处理（或拟采取对策）
                // 绑定表格
                this.BindGrid();
                #endregion
            }
        }
        #endregion

        #region 本月HSE工作存在问题与处理（或拟采取对策）数据绑定
        /// <summary>
        /// 本月HSE工作存在问题与处理（或拟采取对策）数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @" SELECT ManagerTotalMonthItemId"
                          + @" ,ManagerTotalMonthId"
                          + @" ,ExistenceHiddenDanger"
                          + @" ,CorrectiveActions"
                          + @" ,PlanCompletedDate"
                          + @" ,ResponsiMan"
                          + @" ,ActualCompledDate"
                          + @" ,Remark"
                          + @" FROM Manager_ManagerTotalMonthItem"
                          + @" WHERE ManagerTotalMonthId = @ManagerTotalMonthId";
            SqlParameter[] parameter = new SqlParameter[]       
                    {                       
                        new SqlParameter("@ManagerTotalMonthId",this.ManagerTotalMonthId),
                    };
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.DataSource = tb;
            Grid1.DataBind();
        }
        #endregion
    }
}