<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HAZOPSelectCloumn.aspx.cs"
    Inherits="FineUIPro.Web.Technique.HAZOPSelectCloumn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HAZOP管理导出选择列</title>
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
                <f:CheckItem Text="标题" Value="标题" Selected="true" />
                <f:CheckItem Text="摘要" Value="摘要" Selected="true" />
                <f:CheckItem Text="时间" Value="时间" Selected="true" />
                <f:CheckItem Text="整理人" Value="整理人" Selected="true" />
                <f:CheckItem Text="整理日期" Value="整理日期" Selected="true" />
            </f:CheckBoxList>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
