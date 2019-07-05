using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.Emergency
{
    public partial class DrillRecordListView : PageBase
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string DrillRecordListId
        {
            get
            {
                return (string)ViewState["DrillRecordListId"];
            }
            set
            {
                ViewState["DrillRecordListId"] = value;
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
                this.btnClose.OnClientClick = ActiveWindow.GetHideReference();              
                this.DrillRecordListId = Request.Params["DrillRecordListId"];
                if (!string.IsNullOrEmpty(this.DrillRecordListId))
                {
                    Model.Emergency_DrillRecordList DrillRecordList = BLL.DrillRecordListService.GetDrillRecordListById(this.DrillRecordListId);
                    if (DrillRecordList != null)
                    {
                        ///读取编号
                        this.txtDrillRecordCode.Text = BLL.CodeRecordsService.ReturnCodeByDataId(this.DrillRecordListId);
                        this.txtDrillRecordName.Text = DrillRecordList.DrillRecordName;                      
                        this.txtDrillRecordDate.Text = string.Format("{0:yyyy-MM-dd}", DrillRecordList.DrillRecordDate);
                        this.txtUnits.Text = DrillRecordList.UnitNames;
                        if (!string.IsNullOrEmpty(DrillRecordList.DrillRecordType))
                        {
                            Model.Sys_Const c = BLL.ConstValue.drpConstItemList(BLL.ConstValue.Group_DrillRecordType).FirstOrDefault(x => x.ConstValue == DrillRecordList.DrillRecordType);
                            if (c != null)
                            {
                                this.txtDrillRecordType.Text = c.ConstText;
                            }
                        }
                        if (DrillRecordList.JointPersonNum != null)
                        {
                            this.txtJointPersonNum.Text = DrillRecordList.JointPersonNum.ToString();
                        }
                        if (DrillRecordList.DrillCost != null)
                        {
                            this.txtDrillCost.Text = DrillRecordList.DrillCost.ToString();
                        }
                        this.txtDrillRecordContents.Text = HttpUtility.HtmlDecode(DrillRecordList.DrillRecordContents);
                    }
                }
                
                ///初始化审核菜单
                this.ctlAuditFlow.MenuId = BLL.Const.ProjectDrillRecordListMenuId;
                this.ctlAuditFlow.DataId = this.DrillRecordListId;
            }
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
            if (!string.IsNullOrEmpty(this.DrillRecordListId))
            {
                PageContext.RegisterStartupScript(WindowAtt.GetShowReference(String.Format("../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/DrillRecordListAttachUrl&menuId={1}&type=-1", DrillRecordListId, BLL.Const.ProjectDrillRecordListMenuId)));
            }
        }
        #endregion
    }
}