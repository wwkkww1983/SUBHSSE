<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowWorkStage.aspx.cs" Inherits="FineUIPro.Web.Hazard.ShowWorkStage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工作阶段</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true"  Width="300px" Title="工作阶段" TitleToolTip="工作阶段" ShowBorder="true"
                ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:Button ID="btnNew" Text="确定" Icon="Add" runat="server" 
                                OnClick="btnNew_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:CheckBoxList ID="chblWorkStage" runat="server" ColumnNumber="1" AutoColumnWidth="true">
                    </f:CheckBoxList>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
