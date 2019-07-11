using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Controls
{
    public partial class SeeQRImage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.Params["PersonId"]))
                {
                    var person = BLL.PersonService.GetPersonById(Request.Params["PersonId"]);
                    if (person != null && !string.IsNullOrEmpty(person.QRCodeAttachUrl))
                    {
                        this.Image1.ImageUrl = "~/" + person.QRCodeAttachUrl;
                    }
                }
                if (!string.IsNullOrEmpty(Request.Params["EquipmentQualityId"]))
                {
                    var equipmentQuality = BLL.EquipmentQualityService.GetEquipmentQualityById(Request.Params["EquipmentQualityId"]);
                    if (equipmentQuality != null && !string.IsNullOrEmpty(equipmentQuality.QRCodeAttachUrl))
                    {
                        this.Image1.ImageUrl = "~/" + equipmentQuality.QRCodeAttachUrl;
                    }
                }
                if (!string.IsNullOrEmpty(Request.Params["GeneralEquipmentQualityId"]))
                {
                    var generalEquipmentQuality = BLL.GeneralEquipmentQualityService.GetGeneralEquipmentQualityById(Request.Params["GeneralEquipmentQualityId"]);
                    if (generalEquipmentQuality != null && !string.IsNullOrEmpty(generalEquipmentQuality.QRCodeAttachUrl))
                    {
                        this.Image1.ImageUrl = "~/" + generalEquipmentQuality.QRCodeAttachUrl;
                    }
                }
                if (!string.IsNullOrEmpty(Request.Params["ConstructSolutionId"]))
                {
                    var constructSolution = BLL.ConstructSolutionService.GetConstructSolutionById(Request.Params["ConstructSolutionId"]);
                    if (constructSolution != null && !string.IsNullOrEmpty(constructSolution.QRCodeAttachUrl))
                    {
                        this.Image1.ImageUrl = "~/" + constructSolution.QRCodeAttachUrl;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Params["PersonId"]))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("QRCodePrint.aspx?PersonId={0}", Request.Params["PersonId"], "打印 - ")));
            }
            if (!string.IsNullOrEmpty(Request.Params["EquipmentQualityId"]))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("QRCodePrint.aspx?EquipmentQualityId={0}", Request.Params["EquipmentQualityId"], "打印 - ")));
            }
            if (!string.IsNullOrEmpty(Request.Params["GeneralEquipmentQualityId"]))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("QRCodePrint.aspx?GeneralEquipmentQualityId={0}", Request.Params["GeneralEquipmentQualityId"], "打印 - ")));
            }
            if (!string.IsNullOrEmpty(Request.Params["ConstructSolutionId"]))
            {
                PageContext.RegisterStartupScript(Window1.GetShowReference(String.Format("QRCodePrint.aspx?ConstructSolutionId={0}", Request.Params["ConstructSolutionId"], "打印 - ")));
            }
        }
    }
}