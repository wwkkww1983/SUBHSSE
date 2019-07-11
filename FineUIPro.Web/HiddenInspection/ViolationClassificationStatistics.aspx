<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViolationClassificationStatistics.aspx.cs" Inherits="FineUIPro.Web.HiddenInspection.ViolationClassificationStatistics" %>
<%@ Register Src="~/Controls/ChartControl.ascx" TagName="ChartControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>违章分类占比统计</title>
    <style>
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
        
        .f-grid-colheader-text
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AjaxAspnetControls="divAccidentUnit,divAccidentTime" />
    <f:Panel ID="Panel3" CssClass="blockpanel" runat="server" Height="200px" EnableCollapse="false"
        BodyPadding="10px" ShowBorder="true" ShowHeader="false">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="违章分类占比统计" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="Name" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="Name" AllowSorting="true" SortField="Name" SortDirection="ASC"
                OnSort="Grid1_Sort" PageSize="1000">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:DatePicker runat="server" Label="开始时间" ID="txtStartDate" LabelWidth="80px">
                            </f:DatePicker>
                            <f:DatePicker runat="server" Label="结束时间" ID="txtEndDate" LabelWidth="80px">
                            </f:DatePicker>
                            <f:DropDownList ID="drpChartType" runat="server" Label="图形类型" AutoPostBack="true"
                                OnSelectedIndexChanged="drpChartType_SelectedIndexChanged" Width="250px" LabelWidth="80px">
                                <f:ListItem Value="Column" Text="柱形图"></f:ListItem>
                                <f:ListItem Value="Line" Text="折线图"></f:ListItem>
                                <f:ListItem Value="Pie" Text="饼图" Selected="true"></f:ListItem>
                                <f:ListItem Value="StackedArea" Text="堆积面积图"></f:ListItem>
                                <f:ListItem Value="Spline" Text="样条图"></f:ListItem>
                                <f:ListItem Value="SplineArea" Text="样条面积图"></f:ListItem>
                                <f:ListItem Value="StepLine" Text="阶梯线图"></f:ListItem>
                                <f:ListItem Value="Stock" Text="股价图"></f:ListItem>
                                <f:ListItem Value="Radar" Text="雷达图"></f:ListItem>
                            </f:DropDownList>
                            <f:CheckBox ID="ckbShow" runat="server" LabelWidth="80px" Label="三维效果" AutoPostBack="true"
                                OnCheckedChanged="ckbShow_CheckedChanged" Width="120px">
                            </f:CheckBox>
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
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Panel ID="Panel4" CssClass="blockpanel" runat="server" Height="500px" EnableCollapse="false"
        BodyPadding="10px" ShowBorder="true" ShowHeader="false">
        <Items>
            <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="500px" ShowBorder="true"
                TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server">
                <Tabs>
                    <f:Tab ID="Tab2" Title="统计" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server"
                        TitleToolTip="统计">
                        <Items>
                            <f:ContentPanel ShowHeader="false" runat="server" ID="cpAccidentTime" Margin="0 0 0 0">
                                <div id="divAccidentTime">
                                    <uc1:ChartControl ID="ChartAccidentTime" runat="server" />
                                </div>
                            </f:ContentPanel>
                        </Items>
                    </f:Tab>
                </Tabs>
            </f:TabStrip>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
