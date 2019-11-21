<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipmentSafetyListEdit.aspx.cs"
    Inherits="FineUIPro.Web.License.EquipmentSafetyListEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑施工机具、安全设施检查验收</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtEquipmentSafetyListCode" runat="server" Label="编号" LabelAlign="Right"
                        MaxLength="50" Readonly="true" LabelWidth="90px">
                    </f:TextBox>
                      <f:TextBox ID="txtEquipmentSafetyListName" runat="server" Label="名称" Required="true"
                        ShowRedStar="true" LabelAlign="Right" MaxLength="50" LabelWidth="90px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="申请单位" LabelAlign="Right" Required="true"
                        ShowRedStar="true" ForceSelection="false" LabelWidth="90px" EnableEdit="true">
                    </f:DropDownList>
                      <f:DropDownList ID="drpWorkAreaId" runat="server" Label="所属区域" LabelAlign="Right"
                        LabelWidth="90px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpCompileMan" runat="server" Label="申请人" LabelAlign="Right"
                        LabelWidth="90px" EnableEdit="true">
                    </f:DropDownList>
                      <f:DatePicker ID="txtCompileDate" runat="server" Label="编制日期" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="90px">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                       <f:TextBox ID="txtSendMan" runat="server" Label="发证人" LabelAlign="Right" LabelWidth="90px">
                    </f:TextBox>
                    <f:NumberBox ID="txtEquipmentSafetyListCount" runat="server" Label="数量" NoDecimal="true"
                        NoNegative="true" LabelAlign="Right" LabelWidth="90px">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
