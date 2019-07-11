using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FineUIPro.Web.Controls
{
    public partial class QRCodePrint : PageBase
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var person = BLL.PersonService.GetPersonById(Request.Params["PersonId"]);
                if (person != null && !string.IsNullOrEmpty(person.QRCodeAttachUrl))
                {
                    this.imgPhoto.Src = "../" + person.QRCodeAttachUrl;
                    this.lbWedCode.Text = "人员卡号：" + person.CardNo;
                    this.lbWedName.Text = "人员姓名：" + person.PersonName;
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(person.UnitId);
                    string unitName = string.Empty;
                    if (unit != null)
                    {
                        unitName = unit.UnitName;
                    }
                    this.lbUnitName.Text = "人员单位：" + unitName;
                }
                var equipmentQuality = BLL.EquipmentQualityService.GetEquipmentQualityById(Request.Params["EquipmentQualityId"]);
                if (equipmentQuality != null && !string.IsNullOrEmpty(equipmentQuality.QRCodeAttachUrl))
                {
                    this.imgPhoto.Src = "../" + equipmentQuality.QRCodeAttachUrl;
                    this.lbWedCode.Text = "出厂编号：" + equipmentQuality.FactoryCode;
                    this.lbWedName.Text = "设备名称：" + equipmentQuality.EquipmentQualityName;
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(equipmentQuality.UnitId);
                    string unitName = string.Empty;
                    if (unit != null)
                    {
                        unitName = unit.UnitName;
                    }
                    this.lbUnitName.Text = "所属单位：" + unitName;
                }
                var generalEquipmentQuality = BLL.GeneralEquipmentQualityService.GetGeneralEquipmentQualityById(Request.Params["GeneralEquipmentQualityId"]);
                if (generalEquipmentQuality != null && !string.IsNullOrEmpty(generalEquipmentQuality.QRCodeAttachUrl))
                {
                    this.imgPhoto.Src = "../" + generalEquipmentQuality.QRCodeAttachUrl;
                    this.lbWedCode.Text = "编号：" + BLL.CodeRecordsService.ReturnCodeByDataId(generalEquipmentQuality.GeneralEquipmentQualityId);
                    string equipmentTypeName = string.Empty;
                    Model.Base_SpecialEquipment equipmentType = BLL.SpecialEquipmentService.GetSpecialEquipmentById(generalEquipmentQuality.SpecialEquipmentId);
                    if (equipmentType != null)
                    {
                        equipmentTypeName = equipmentType.SpecialEquipmentName;
                    }
                    this.lbWedName.Text = "设备名称：" + equipmentTypeName;
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(generalEquipmentQuality.UnitId);
                    string unitName = string.Empty;
                    if (unit != null)
                    {
                        unitName = unit.UnitName;
                    }
                    this.lbUnitName.Text = "所属单位：" + unitName;
                }
                var constructSolution = BLL.ConstructSolutionService.GetConstructSolutionById(Request.Params["ConstructSolutionId"]);
                if (constructSolution != null && !string.IsNullOrEmpty(constructSolution.QRCodeAttachUrl))
                {
                    this.imgPhoto.Src = "../" + constructSolution.QRCodeAttachUrl;
                    string code = string.Empty;
                    if (!string.IsNullOrEmpty(constructSolution.ConstructSolutionCode))
                    {
                        code = constructSolution.ConstructSolutionCode;
                    }
                    else
                    {
                        code = BLL.CodeRecordsService.ReturnCodeByDataId(constructSolution.ConstructSolutionId);
                    }
                    this.lbWedCode.Text = "编号：" + code;
                    this.lbWedName.Text = "方案名称：" + constructSolution.ConstructSolutionName;
                    Model.Base_Unit unit = BLL.UnitService.GetUnitByUnitId(constructSolution.UnitId);
                    string unitName = string.Empty;
                    if (unit != null)
                    {
                        unitName = unit.UnitName;
                    }
                    this.lbUnitName.Text = "所属单位：" + unitName;
                }
            }
        }
    }
}