<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InUnitEdit.aspx.cs" Inherits="FineUIPro.Web.QualityAudit.InUnitEdit"
    ValidateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑采购供货厂家管理</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="采购供货厂家管理" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtInUnitCode" runat="server" Label="编号" LabelAlign="Right" Readonly="true" LabelWidth="150px">
                    </f:TextBox>
                    <f:TextBox ID="txtManufacturerName" runat="server" Label="厂家名称" LabelAlign="Right"
                        MaxLength="100" Required="true" ShowRedStar="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtHSEMan" runat="server" Label="负责人及安全员姓名" LabelAlign="Right" MaxLength="20" LabelWidth="150px">
                    </f:TextBox>
                    <f:TextBox ID="txtHeadTel" runat="server" Label="负责人电话" LabelAlign="Right" MaxLength="20">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtInDate" runat="server" Label="入场时间" EnableEdit="true" LabelAlign="Right" LabelWidth="150px">
                    </f:DatePicker>
                    <f:NumberBox ID="txtPersonCount" runat="server" Label="人员数量" NoDecimal="true" NoNegative="false"
                        LabelAlign="Right">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtTrainNum" runat="server" Label="培训人数" NoDecimal="true" NoNegative="false"
                        LabelAlign="Right" LabelWidth="150px">
                    </f:NumberBox>
                    <f:DatePicker ID="txtOutDate" runat="server" Label="离场时间" EnableEdit="true" LabelAlign="Right">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtBadgesIssued" runat="server" Label="胸卡发放" MaxLength="50" LabelAlign="Right" LabelWidth="150px">
                    </f:TextBox>
                    <f:DatePicker ID="txtJobTicketValidity" runat="server" Label="作业票有效期" EnableEdit="true"
                        LabelAlign="Right">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="btnTrainRecordsUrl" EmptyText="请选择附件" OnFileSelected="btnTrainRecordsUrl_Click"
                        AutoPostBack="true" Label="培训记录" LabelWidth="150px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="培训记录">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divTrainRecordsUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="btnPlanUrl" EmptyText="请选择附件" OnFileSelected="btnPlanUrl_Click"
                        AutoPostBack="true" Label="方案及资质审查" LabelWidth="150px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="方案及资质审查">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divPlanUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="btnTemporaryPersonUrl" EmptyText="请选择附件" OnFileSelected="btnTemporaryPersonUrl_Click"
                        AutoPostBack="true" Label="临时到场人员培训" LabelWidth="150px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel3" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="临时到场人员培训">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divTemporaryPersonUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="btnInPersonTrainUrl" EmptyText="请选择附件" OnFileSelected="btnInPersonTrainUrl_Click"
                        AutoPostBack="true" Label="厂家入场安全人员培训" LabelWidth="150px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel4" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="厂家入场安全人员培训">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divInPersonTrainUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="btnHSEAgreementUrl" EmptyText="请选择附件" OnFileSelected="btnHSEAgreementUrl_Click"
                        AutoPostBack="true" Label="HSE协议" LabelWidth="150px">
                    </f:FileUpload>
                    <f:ContentPanel ID="ContentPanel5" runat="server" ShowHeader="false" ShowBorder="false"
                        Title="HSE协议">
                        <table>
                            <tr style="height: 28px">
                                <td align="left">
                                    <div id="divHSEAgreementUrl" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtAccommodation" runat="server" Label="住宿及上班车辆情况" LabelAlign="Right"
                        MaxLength="500" LabelWidth="150px" Height="60px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtOperationTicket" runat="server" Label="作业区域及作业票办理情况" LabelAlign="Right"
                        MaxLength="500" LabelWidth="150px" Height="60px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtLaborSituation" runat="server" Label="劳保用品租赁情况（押金情况）" LabelAlign="Right"
                        MaxLength="500" LabelWidth="150px" Height="60px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
