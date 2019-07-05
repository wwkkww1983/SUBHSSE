<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardListSelectCloumn.aspx.cs"
    Inherits="FineUIPro.Web.Technique.HazardListSelectCloumn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>危险源清单导出选择列</title>
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
                <f:CheckItem Text="危险源代码" Value="危险源代码" Selected="true" />
                <f:CheckItem Text="危险源类别编号" Value="危险源类别编号" Selected="true" />
                <f:CheckItem Text="危险因素明细" Value="危险因素明细" Selected="true" />
                <f:CheckItem Text="缺陷类型" Value="缺陷类型" Selected="true" />
                <f:CheckItem Text="可能导致的事故" Value="可能导致的事故" Selected="true" />
                <f:CheckItem Text="辅助方法" Value="辅助方法" Selected="true" />
                <f:CheckItem Text="危险评价(L)" Value="危险评价(L)" Selected="true" />
                <f:CheckItem Text="危险评价(E)" Value="危险评价(E)" Selected="true" />
                <f:CheckItem Text="危险评价(C)" Value="危险评价(C)" Selected="true" />
                <f:CheckItem Text="危险评价(D)" Value="危险评价(D)" Selected="true" />
                <f:CheckItem Text="危险级别" Value="危险级别" Selected="true" />
                <f:CheckItem Text="控制措施" Value="控制措施" Selected="true" />
            </f:CheckBoxList>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
