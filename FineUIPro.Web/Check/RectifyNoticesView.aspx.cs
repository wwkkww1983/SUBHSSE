using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace FineUIPro.Web.Check
{
    public partial class RectifyNoticesView1 : PageBase
    {
        public string RectifyNoticesId
        {
            get
            {
                return (string)ViewState["RectifyNoticesId"];
            }
            set
            {
                ViewState["RectifyNoticesId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                txtProjectName.Text = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.LoginProjectId).ProjectName;
                ////自动生成编码
                this.txtRectifyNoticesCode.Text = BLL.CodeRecordsService.ReturnCodeByMenuIdProjectId(BLL.Const.ProjectRectifyNoticesMenuId, this.CurrUser.LoginProjectId, this.CurrUser.UnitId);
                //受检单位            
                BLL.UnitService.InitUnitByProjectIdUnitTypeDropDownList(this.drpUnitId, this.CurrUser.LoginProjectId, Const.ProjectUnitType_2, false);
                //区域
                BLL.WorkAreaService.InitWorkAreaDropDownList(this.drpWorkAreaId, this.CurrUser.LoginProjectId, true);
                ///检察人员
                BLL.UserService.InitFlowOperateControlUserDropDownList(this.drpCheckPerson, this.CurrUser.LoginProjectId, BLL.CommonService.GetIsThisUnitId(), true);
                RectifyNoticesId = Request.Params["RectifyNoticesId"];
                if (!string.IsNullOrEmpty(RectifyNoticesId))
                {                  
                    Model.Check_RectifyNotices RectifyNotices = RectifyNoticesService.GetRectifyNoticesById(RectifyNoticesId);
                    if (!string.IsNullOrEmpty(RectifyNotices.UnitId))
                    {
                        this.drpUnitId.SelectedValue = RectifyNotices.UnitId;
                    }
                    if (!string.IsNullOrEmpty(RectifyNotices.WorkAreaId))
                    {
                        this.drpWorkAreaId.SelectedValue = RectifyNotices.WorkAreaId;
                    }
                    if (!string.IsNullOrEmpty(RectifyNotices.CheckManIds))
                    {
                        this.drpCheckPerson.SelectedValueArray = RectifyNotices.CheckManIds.Split(',');
                    }
                    this.txtRectifyNoticesCode.Text = RectifyNotices.RectifyNoticesCode;
                    this.txtCompleteDate.Text = RectifyNotices.CompleteDate.ToString();
                    if (!string.IsNullOrEmpty(RectifyNotices.HiddenHazardType))
                    {
                        this.drpHiddenHazardType.SelectedValue = RectifyNotices.HiddenHazardType;
                    }
                    this.txtReCheckOpinion.Text = RectifyNotices.ReCheckOpinion;
                    BindGrid1();
                    BindGrid();
                }
            }
        }

        public void BindGrid1()
        {
            string strSql = @"select RectifyNoticesItemId, RectifyNoticesId, WrongContent, Requirement, LimitTime, RectifyResults, (case IsRectify when 'True' then '合格' when 'False' then '不合格' else '' end) As IsRectify  from [dbo].[Check_RectifyNoticesItem] ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where RectifyNoticesId = @RectifyNoticesId";
            listStr.Add(new SqlParameter("@RectifyNoticesId", RectifyNoticesId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(Grid1, tb);
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        public void BindGrid()
        {
            string strSql = @"select FlowOperateId, RectifyNoticesId, OperateName, OperateManId, OperateTime, IsAgree, Opinion,S.UserName from Check_RectifyNoticesFlowOperate C left join Sys_User S on C.OperateManId=s.UserId ";
            List<SqlParameter> listStr = new List<SqlParameter>();
            strSql += "where RectifyNoticesId= @RectifyNoticesId";
            listStr.Add(new SqlParameter("@RectifyNoticesId", RectifyNoticesId));
            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);
            var table = this.GetPagedDataTable(gvFlowOperate, tb);
            gvFlowOperate.DataSource = table;
            gvFlowOperate.DataBind();
        }
    }
}