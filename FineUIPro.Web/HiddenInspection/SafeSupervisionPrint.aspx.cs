using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.HiddenInspection
{
    public partial class SafeSupervisionPrint : PageBase
    {
        /// <summary>
        /// APP安全督查Id
        /// </summary>
        public string SafeSupervisionId
        {
            get
            {
                return (string)ViewState["SafeSupervisionId"];
            }
            set
            {
                ViewState["SafeSupervisionId"] = value;
            }
        }

        /// <summary>
        /// 页面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SafeSupervisionId = Request.Params["SafeSupervisionId"];
                Model.View_Hazard_SafeSupervision safeSupervision = BLL.HSSE_Hazard_SafeSupervisionService.GetSafeSupervisionViewBySafeSupervisionId(SafeSupervisionId);
                if (safeSupervision != null)
                {
                    this.lbProjectName.Text = safeSupervision.ProjectName;
                    if (safeSupervision.CheckDate != null)
                    {
                        this.lbCheckDate.Text = string.Format("{0:yyyy-MM-dd}", safeSupervision.CheckDate);
                    }
                    this.lbCheckType.Text = safeSupervision.CheckTypeStr;
                    this.lbCheckMan.Text = safeSupervision.CheckMan;
                    //问题明细集合可以参照HiddenRectificationPrint页面后台代码
                    string registerTypesNames = string.Empty;
                    string registerDefs = string.Empty;
                    string rectifications = string.Empty;
                    var q = from x in Funs.DB.View_Hazard_HazardRegister
                            where x.SafeSupervisionId == SafeSupervisionId
                            select x;
                    int i = 1;
                    foreach (var item in q)
                    {
                        registerTypesNames += i.ToString() + "." + item.RegisterTypesName + "。";
                        if (!string.IsNullOrEmpty(item.RegisterDef))
                        {
                            registerDefs += i.ToString() + "." + item.RegisterDef + "。";
                        }
                        if (!string.IsNullOrEmpty(item.Rectification))
                        {
                            rectifications += i.ToString() + "." + item.Rectification + "。";
                        }
                        i++;
                    }
                    this.lbRegisterTypesNames.Text = registerTypesNames;
                    this.lbRegisterDefs.Text = registerDefs;
                    this.lbRectifications.Text = rectifications;
                    bool isAllOK = true;   //是否全部已确认
                    foreach (var item in q)
                    {
                        if (item.SafeSupervisionIsOK != true)
                        {
                            if (item.States != "3")
                            {
                                isAllOK = false;
                            }
                        }
                    }
                    if (isAllOK)
                    {
                        this.lbReCheckStation.Text = "整改完成";
                    }
                    this.lbCheckManConfirm.Text = safeSupervision.CheckMan;
                    this.lbConfirmDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
            }
        }
    }
}