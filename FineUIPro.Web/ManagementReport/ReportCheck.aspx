<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportCheck.aspx.cs" Inherits="FineUIPro.Web.ManagementReport.ReportCheck" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>月报与集团报表校对</title>
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
        
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
        
        .color {
            background-color: #FF0000;
            color: #fff;
            }
            
        .f-grid-colheader-text {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="月报与集团报表校对" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="Id,ProjectCode" AllowCellEditing="true" ClicksToEdit="2"
                DataIDField="Id" AllowSorting="true" SortField="Id" SortDirection="DESC" OnSort="Grid1_Sort"
                EnableColumnLines="true" AllowPaging="false" IsDatabasePaging="true" PageSize="10" OnRowCommand="Grid1_RowCommand"
                OnPageIndexChange="Grid1_PageIndexChange" AllowFilters="true" OnFilterChange="Grid1_FilterChange"
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:DatePicker runat="server" Label="选择月份" ID="txtReportDate" LabelWidth="100px" Width="200px"
                                DateFormatString="yyyy-MM" LabelAlign="right">
                            </f:DatePicker>
                            <f:Button ID="BtnAnalyse" Text="校对" Icon="ChartPie" runat="server" OnClick="BtnAnalyse_Click">
                            </f:Button>
                            <%--<f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>--%>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:GroupField EnableLock="true" HeaderText="项目月报（施工中）" TextAlign="Center">
                        <Columns>
                            <f:RenderField Width="40px" ColumnID="Id" DataField="Id" SortField="Id" FieldType="String"
                                HeaderText="序号" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="75px" ColumnID="ProjectCode" DataField="ProjectCode" SortField="ProjectCode" FieldType="String"
                                HeaderText="项目代号" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="ProjectName" DataField="ProjectName" SortField="ProjectName" FieldType="String"
                                HeaderText="项目名称" HeaderTextAlign="Center" TextAlign="Center" Locked="true">
                            </f:RenderField>
                            <f:RenderField Width="67px" ColumnID="ManHours1" DataField="ManHours1" SortField="ManHours1" FieldType="String"
                                HeaderText="当月完成工时" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="67px" ColumnID="SafeManHours1" DataField="SafeManHours1" SortField="SafeManHours1" FieldType="String"
                                HeaderText="当月安全工时" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="67px" ColumnID="Check1" DataField="Check1" SortField="Check1" FieldType="String"
                                HeaderText="判断" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="67px" ColumnID="ManCount1" DataField="ManCount1" SortField="ManCount1" FieldType="String"
                                HeaderText="当月员工总数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <f:GroupField EnableLock="true" HeaderText="百万工时安全统计表" TextAlign="Center">
                        <Columns>
                            <f:RenderField Width="67px" ColumnID="ManHours2" DataField="ManHours2" SortField="ManHours2" FieldType="String"
                                HeaderText="当月完成工时总数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="67px" ColumnID="Check2" DataField="Check2" SortField="Check2" FieldType="String"
                                HeaderText="判断" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="67px" ColumnID="ManCount2" DataField="ManCount2" SortField="ManCount2" FieldType="String"
                                HeaderText="当月员工总数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="67px" ColumnID="Check3" DataField="Check3" SortField="Check3" FieldType="String"
                                HeaderText="判断" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <f:GroupField EnableLock="true" HeaderText="职工伤亡事故原因分析表" TextAlign="Center">
                        <Columns>
                            <f:RenderField Width="67px" ColumnID="ManHours3" DataField="ManHours3" SortField="ManHours3" FieldType="String"
                                HeaderText="当月完成工时总数" HeaderTextAlign="Center" TextAlign="Center" >
                            </f:RenderField>
                            <f:RenderField Width="67px" ColumnID="Check4" DataField="Check4" SortField="Check4" FieldType="String"
                                HeaderText="判断" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="67px" ColumnID="ManCount3" DataField="ManCount3" SortField="ManCount3" FieldType="String"
                                HeaderText="当月员工总数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="67px" ColumnID="Check5" DataField="Check5" SortField="Check5" FieldType="String"
                                HeaderText="判断" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:GroupField>
                    <f:RenderField Width="150px" ColumnID="AccidentDef" DataField="AccidentDef" SortField="AccidentDef"
                        FieldType="String" HeaderText="事故描述" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:LinkButtonField Width="100px" HeaderText="查看月报" ConfirmTarget="Top" CommandName="View" 
                        TextAlign="Center" ToolTip="查看月报" DataTextField="View" />
                </Columns>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="查看管理月报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true"
        Width="1300px" Height="600px">
    </f:Window>
    </form>
</body>
</html>
