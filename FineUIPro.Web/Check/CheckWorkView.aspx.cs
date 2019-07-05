using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FineUIPro.Web.Check
{
    public partial class CheckWorkView : PageBase
    {
        #region 定义变量
        /// <summary>
        /// 主键
        /// </summary>
        public string CheckWorkId
        {
            get
            {
                return (string)ViewState["CheckWorkId"];
            }
            set
            {
                ViewState["CheckWorkId"] = value;
            }
        }

        /// <summary>
        /// 定义集合
        /// </summary>
        private static List<Model.View_Check_CheckWorkDetail> checkWorkDetails = new List<Model.View_Check_CheckWorkDetail>();
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
                hdAttachUrl.Text = string.Empty;
                hdId.Text = string.Empty;
                btnClose.OnClientClick = ActiveWindow.GetHideReference();
                List<Model.Base_Unit> thisUnit = BLL.UnitService.GetThisUnitDropDownList();
                string thisUnitId = string.Empty;
                string thisUnitName = string.Empty;
                if (thisUnit.Count > 0)
                {
                    thisUnitId = thisUnit[0].UnitId;
                    this.txtThisUnit.Text = thisUnit[0].UnitName;
                    this.txtMainUnitDeputy.Label = thisUnit[0].UnitName;
                }

                checkWorkDetails.Clear();

                this.CheckWorkId = Request.Params["CheckWorkId"];
                var checkWork = BLL.Check_CheckWorkService.GetCheckWorkByCheckWorkId(this.CheckWorkId);
                if (checkWork != null)
                {
                    this.txtCheckWorkCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.CheckWorkId);
                    if (checkWork.CheckTime != null)
                    {
                        this.txtCheckDate.Text = string.Format("{0:yyyy-MM-dd}", checkWork.CheckTime);
                    }
                    this.txtArea.Text = checkWork.Area;
                    if (!string.IsNullOrEmpty(checkWork.MainUnitPerson))
                    {
                        string personNames = string.Empty;
                        string[] unitIds = checkWork.MainUnitPerson.Split(',');
                        foreach (var item in unitIds)
                        {
                            Model.Sys_User user = BLL.UserService.GetUserByUserId(item);
                            if (user != null)
                            {
                                personNames += user.UserName + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(personNames))
                        {
                            personNames = personNames.Substring(0, personNames.LastIndexOf(","));
                        }
                        this.txtMainUnitPerson.Text = personNames;
                    }
                    if (!string.IsNullOrEmpty(checkWork.SubUnits))
                    {
                        string unitNames = string.Empty;
                        foreach (var item in checkWork.SubUnits.Split(','))
                        {
                            unitNames += BLL.UnitService.GetUnitNameByUnitId(item) + ",";
                        }
                        if (!string.IsNullOrEmpty(unitNames))
                        {
                            this.txtSubUnits.Text = unitNames.Substring(0, unitNames.LastIndexOf(','));
                        }

                        if (!string.IsNullOrEmpty(checkWork.SubUnitPerson))
                        {
                            string personNames = string.Empty;
                            foreach (var item in checkWork.SubUnitPerson.Split(','))
                            {
                                personNames += BLL.UserService.GetUserNameByUserId(item) + ",";
                            }
                            if (!string.IsNullOrEmpty(personNames))
                            {
                                this.txtSubUnitPerson.Text = personNames.Substring(0, personNames.LastIndexOf(","));
                            }
                        }
                    }
                   
                    if (checkWork.IsCompleted == true)
                    {
                        this.lbIsCompleted.Text = "已闭环";
                    }
                    else
                    {
                        this.lbIsCompleted.Text = "未闭环";
                    }
                    this.txtMainUnitDeputy.Text = checkWork.MainUnitDeputy;
                    if (checkWork.MainUnitDeputyDate != null)
                    {
                        this.txtMainUnitDeputyDate.Text = string.Format("{0:yyyy-MM-dd}", checkWork.MainUnitDeputyDate);
                    }
                    this.txtSubUnitDeputy.Text = checkWork.SubUnitDeputy;
                    if (checkWork.SubUnitDeputyDate != null)
                    {
                        this.txtSubUnitDeputyDate.Text = string.Format("{0:yyyy-MM-dd}", checkWork.SubUnitDeputyDate);
                    }
                    checkWorkDetails = (from x in Funs.DB.View_Check_CheckWorkDetail where x.CheckWorkId == this.CheckWorkId orderby x.CheckItem select x).ToList();
                }
                Grid1.DataSource = checkWorkDetails;
                Grid1.DataBind();
                ChangeGridColor();
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectCheckWorkMenuId;
                this.ctlAuditFlow.DataId = this.CheckWorkId;
            }
        }
        #endregion

        #region 改变Grid颜色
        private void ChangeGridColor()
        {
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(Grid1.Rows[i].Values[5].ToString()))
                {
                    Grid1.Rows[i].RowCssClass = "red";
                }
                else if (string.IsNullOrEmpty(Grid1.Rows[i].Values[6].ToString()))
                {
                    Grid1.Rows[i].RowCssClass = "yellow";
                }
            }
        }
        #endregion

        #region 获取检查类型
        /// <summary>
        /// 获取检查类型
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected string ConvertCheckItemType(object CheckItem)
        {
            return BLL.Check_ProjectCheckItemSetService.ConvertCheckItemType(CheckItem);
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAttachUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CheckWorkId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/CheckWork&menuId={1}&type=-1", this.CheckWorkId, BLL.Const.ProjectCheckWorkMenuId)));
            }

        }
        #endregion
    }
}