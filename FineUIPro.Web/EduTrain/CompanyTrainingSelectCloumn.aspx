<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyTrainingSelectCloumn.aspx.cs"
    Inherits="FineUIPro.Web.EduTrain.CompanyTrainingSelectCloumn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择公司教材库需要导出的列</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="False" ShowHeader="false" Layout="Anchor"
        BodyPadding="5px">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <f:Button ID="btnClose" EnablePostBack="false" Text="取消" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:Button ID="btnImport" Text="导出" runat="server" Icon="SystemSave" OnClick="btnImport_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:CheckBoxList runat="server" ID="cblColumns" ColumnNumber="4">
                <f:CheckItem Text="教材编号" Value="教材编号" Selected="true" />
                <f:CheckItem Text="教材名称" Value="教材名称" Selected="true" />
                <f:CheckItem Text="整理人" Value="整理人" Selected="true" />
                <f:CheckItem Text="整理时间" Value="整理时间" Selected="true" />
            </f:CheckBoxList>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
