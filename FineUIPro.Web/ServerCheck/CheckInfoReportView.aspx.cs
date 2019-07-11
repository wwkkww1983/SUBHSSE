using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BLL;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.ServerCheck
{
    public partial class CheckInfoReportView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
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
                this.CheckInfoId = Request.Params["CheckInfoId"];
                var table8 = Funs.DB.Check_CheckInfo_Table8.FirstOrDefault(x => x.CheckInfoId == this.CheckInfoId);
                if (table8 != null)
                {
                    this.txtValues1.Text = table8.Values1;
                    this.txtValues2.Text = table8.Values2;
                    this.txtValues3.Text = table8.Values3;
                    this.txtValues4.Text = table8.Values4;
                    this.txtValues5.Text = table8.Values5;
                    this.txtValues6.Text = table8.Values6;
                    this.txtValues7.Text = table8.Values7;
                }

                // 绑定表格
                this.BindGrid();
            }
        }
        #endregion      

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT ID,Name,Sex,UnitName,PostName,WorkTitle,CheckPostName,CheckDate,CheckInfoId,SortIndex"
                + @" FROM dbo.Check_CheckInfo_Table8Item "
                + @" WHERE CheckInfoId=@CheckInfoId";
            SqlParameter[] parameter = new SqlParameter[]       
                    {                       
                        new SqlParameter("@CheckInfoId",this.CheckInfoId),
                    };

            strSql += "   ORDER BY SortIndex";
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            Grid1.DataSource = tb;
            Grid1.DataBind();
        }   
        #endregion                
    }
}
