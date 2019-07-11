<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerPerformance.aspx.cs" Inherits="FineUIPro.Web.HiddenInspection.ManagerPerformance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管理人员履职情况统计</title>
    <style>
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
        
        .f-grid-colheader-text {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" RegionPosition="Top"
                BodyPadding="0 5 0 0" Layout="VBox" runat="server" EnableCollapse="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Bottom" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:DatePicker runat="server" Label="开始时间" ID="txtStartDate" LabelWidth="80px">
                            </f:DatePicker>
                            <f:DatePicker runat="server" Label="结束时间" ID="txtEndDate" LabelWidth="80px">
                            </f:DatePicker>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="BtnAnalyse" Text="统计" Icon="ChartPie" runat="server" OnClick="BtnAnalyse_Click">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" Text="导出" Icon="TableGo"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Region>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="管理人员履职情况统计" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="Name" AllowCellEditing="true" EnableColumnLines="true"
                        ClicksToEdit="2" DataIDField="Name" AllowSorting="true" SortField="Name" 
                        SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                        PageSize="1000">
                    </f:Grid>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    </form>
</body>
</html>
