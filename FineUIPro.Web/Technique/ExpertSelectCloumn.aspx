<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpertSelectCloumn.aspx.cs"
    Inherits="FineUIPro.Web.Technique.ExpertSelectCloumn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全专家导出选择列</title>
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
                    <f:Button ID="btnImport" Text="导出" runat="server" Icon="SystemSave" OnClick="btnImport_Click"
                        Hidden="true">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:CheckBoxList runat="server" ID="cblColumns" ColumnNumber="3">
                <f:CheckItem Text="编号" Value="编号" Selected="true" />
                <f:CheckItem Text="姓名" Value="姓名" Selected="true" />
                <f:CheckItem Text="单位" Value="单位" Selected="true" />
                <f:CheckItem Text="专家类别" Value="专家类别" Selected="true" />
                <f:CheckItem Text="专业" Value="专业" Selected="true" />
                <f:CheckItem Text="职称" Value="职称" Selected="true" />
                <f:CheckItem Text="资质有效期" Value="资质有效期" Selected="true" />
            </f:CheckBoxList>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
