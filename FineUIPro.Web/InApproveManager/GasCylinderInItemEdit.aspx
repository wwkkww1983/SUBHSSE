<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GasCylinderInItemEdit.aspx.cs"
    Inherits="FineUIPro.Web.InApproveManager.GasCylinderInItemEdit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑气瓶基本情况</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpGasCylinderId" runat="server" Label="气瓶类型" Required="true"
                        ShowRedStar="true" LabelAlign="Right" LabelWidth="140px">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtGasCylinderNum" runat="server" Label="数量" LabelAlign="Right"
                        NoDecimal="true" NoNegative="true" LabelWidth="140px">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpPM_IsFull" runat="server" Label="瓶帽是否齐全" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="140px">
                        <f:ListItem Value="True" Text="是" />
                        <f:ListItem Value="False" Text="否" />
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpFZQ_IsFull" runat="server" Label="防震圈是否齐全" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="140px">
                        <f:ListItem Value="True" Text="是" />
                        <f:ListItem Value="False" Text="否" />
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpIsSameCar" runat="server" Label="是否同车运行" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="140px">
                        <f:ListItem Value="True" Text="是" />
                        <f:ListItem Value="False" Text="否" />
                    </f:DropDownList>
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
