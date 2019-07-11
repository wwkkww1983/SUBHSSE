<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportE.aspx.cs" Inherits="FineUIPro.Web.ManagementReport.MonthReportE" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>海外工程项目月度HSSE统计表</title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="海外工程项目月度HSSE统计表" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="MonthReportId" AllowCellEditing="true"
                ClicksToEdit="2" DataIDField="MonthReportId" AllowSorting="true" SortField="ProjectCode"
                SortDirection="DESC" OnSort="Grid1_Sort" IsDatabasePaging="true"
                EnableColumnLines="true" PageSize="1000" EnableSummary="true" SummaryPosition="Flow"
                AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableTextSelection="True"
                AllowColumnLocking="true" >
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                        <Items>
                            <f:DropDownList ID="drpProject" runat="server" Label="项目" Width="280px" LabelWidth="50px" EmptyText="请选择项目"
                                EnableEdit="true">
                            </f:DropDownList>
                             <f:DropDownList ID="drpYear" runat="server" EnableEdit="true" LabelAlign="Right"
                                Width="150px" LabelWidth="50px" Label="年度">
                            </f:DropDownList>
                            <f:DropDownList ID="drpMonth" runat="server"  EnableEdit="true" LabelAlign="Right"
                                Width="150px" LabelWidth="50px" Label="月份">
                            </f:DropDownList>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="BtnAnalyse" ToolTip="统计" Icon="ChartPie" runat="server" OnClick="BtnAnalyse_Click">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="150px" ColumnID="ProjectName" DataField="ProjectName"
                        SortField="ProjectName" FieldType="String" HeaderText="项目名称" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="CountryCities" DataField="CountryCities"
                        SortField="CountryCities" FieldType="String" HeaderText="所在国别/城市" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="StartEndDate" DataField="StartEndDate"
                        SortField="StartEndDate" FieldType="String" HeaderText="开－竣工日期" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="ContractType" DataField="ContractType"
                        SortField="ContractType" FieldType="String" HeaderText="合同类别" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="110px" ColumnID="ContractAmount" DataField="ContractAmount"
                        SortField="ContractAmount" FieldType="String" HeaderText="合同额(万元)" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ThisMajorWork" DataField="ThisMajorWork"
                        SortField="ThisMajorWork" FieldType="String" HeaderText="本月主要工作" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="NextMajorWork" DataField="NextMajorWork"
                        SortField="NextMajorWork" FieldType="String" HeaderText="下月主要工作" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ThisIncome" DataField="ThisIncome"
                        SortField="ThisIncome" FieldType="String" HeaderText="本月收入(万元)" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="YearIncome" DataField="YearIncome"
                        SortField="YearIncome" FieldType="String" HeaderText="本年累计收入(万元)" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="TotalIncome" DataField="TotalIncome"
                        SortField="TotalIncome" FieldType="String" HeaderText="自开工累计收入(万元)" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ThisImageProgress" DataField="ThisImageProgress"
                        SortField="ThisImageProgress" FieldType="String" HeaderText="本月形象进度" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="YearImageProgress" DataField="YearImageProgress"
                        SortField="YearImageProgress" FieldType="String" HeaderText="本年形象进度" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="TotalImageProgress" DataField="TotalImageProgress"
                        SortField="TotalImageProgress" FieldType="String" HeaderText="自开工累计形象进度" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ThisPersonNum" DataField="ThisPersonNum"
                        SortField="ThisPersonNum" FieldType="String" HeaderText="本月现场员工总数" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="YearPersonNum" DataField="YearPersonNum"
                        SortField="YearPersonNum" FieldType="String" HeaderText="本年累计员工总数" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="TotalPersonNum" DataField="TotalPersonNum"
                        SortField="TotalPersonNum" FieldType="String" HeaderText="自开工累计员工总数" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ThisForeignPersonNum" DataField="ThisForeignPersonNum"
                        SortField="ThisForeignPersonNum" FieldType="String" HeaderText="本月外籍员工人数" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="YearForeignPersonNum" DataField="YearForeignPersonNum"
                        SortField="YearForeignPersonNum" FieldType="String" HeaderText="本月外籍员工人数" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="TotalForeignPersonNum" DataField="TotalForeignPersonNum"
                        SortField="TotalForeignPersonNum" FieldType="String" HeaderText="自开工累计外籍员工人数" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ThisTrainPersonNum" DataField="ThisTrainPersonNum"
                        SortField="ThisTrainPersonNum" FieldType="String" HeaderText="本月HSSE教育培训（人/次）" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="YearTrainPersonNum" DataField="YearTrainPersonNum"
                        SortField="YearTrainPersonNum" FieldType="String" HeaderText="本年累计HSSE教育培训（人/次）" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="TotalTrainPersonNum" DataField="TotalTrainPersonNum"
                        SortField="TotalTrainPersonNum" FieldType="String" HeaderText="自开工累计HSSE教育培训（人/次）" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ThisCheckNum" DataField="ThisCheckNum"
                        SortField="ThisCheckNum" FieldType="String" HeaderText="本月HSSE检查（次）" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="YearCheckNum" DataField="YearCheckNum"
                        SortField="YearCheckNum" FieldType="String" HeaderText="本年累计HSSE检查（次）" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="TotalCheckNum" DataField="TotalCheckNum"
                        SortField="TotalCheckNum" FieldType="String" HeaderText="自开工累计HSSE检查（次）" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ThisViolationNum" DataField="ThisViolationNum"
                        SortField="ThisViolationNum" FieldType="String" HeaderText="本月HSSE隐患排查治理（项）" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="YearViolationNum" DataField="YearViolationNum"
                        SortField="YearViolationNum" FieldType="String" HeaderText="本年累计HSSE隐患排查治理（项）" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="TotalViolationNum" DataField="TotalViolationNum"
                        SortField="TotalViolationNum" FieldType="String" HeaderText="自开工累计HSSE隐患排查治理（项）" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ThisInvestment" DataField="ThisInvestment"
                        SortField="ThisInvestment" FieldType="String" HeaderText="本月HSSE投入" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="YearInvestment" DataField="YearInvestment"
                        SortField="YearInvestment" FieldType="String" HeaderText="本年累计HSSE投入" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="TotalInvestment" DataField="TotalInvestment"
                        SortField="TotalInvestment" FieldType="String" HeaderText="自开工累计HSSE投入" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ThisReward" DataField="ThisReward"
                        SortField="ThisReward" FieldType="String" HeaderText="本月HSSE奖励" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="YearReward" DataField="YearReward"
                        SortField="YearReward" FieldType="String" HeaderText="本年累计HSSE奖励" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="TotalReward" DataField="TotalReward"
                        SortField="TotalReward" FieldType="String" HeaderText="自开工累计HSSE奖励" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ThisPunish" DataField="ThisPunish"
                        SortField="ThisPunish" FieldType="String" HeaderText="本月HSSE处罚" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="YearPunish" DataField="YearPunish"
                        SortField="YearPunish" FieldType="String" HeaderText="本年累计HSSE处罚" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="TotalPunish" DataField="TotalPunish"
                        SortField="TotalPunish" FieldType="String" HeaderText="自开工累计HSSE处罚" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ThisEmergencyDrillNum" DataField="ThisEmergencyDrillNum"
                        SortField="ThisEmergencyDrillNum" FieldType="String" HeaderText="本月HSSE应急演练（次）" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="YearEmergencyDrillNum" DataField="YearEmergencyDrillNum"
                        SortField="YearEmergencyDrillNum" FieldType="String" HeaderText="本年累计HSSE应急演练（次）" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="TotalEmergencyDrillNum" DataField="TotalEmergencyDrillNum"
                        SortField="TotalEmergencyDrillNum" FieldType="String" HeaderText="自开工累计HSSE应急演练（次）" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="ThisHSEManhours" DataField="ThisHSEManhours"
                        SortField="ThisHSEManhours" FieldType="String" HeaderText="本月HSE工时" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="YearHSEManhours" DataField="YearHSEManhours"
                        SortField="YearHSEManhours" FieldType="String" HeaderText="本年累计HSE工时" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="TotalHSEManhours" DataField="TotalHSEManhours"
                        SortField="TotalHSEManhours" FieldType="String" HeaderText="自开工累计HSE工时" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="ThisRecordEvent" DataField="ThisRecordEvent"
                        SortField="ThisRecordEvent" FieldType="String" HeaderText="本月可记录HSE事件" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="YearRecordEvent" DataField="YearRecordEvent"
                        SortField="YearRecordEvent" FieldType="String" HeaderText="本年累计可记录HSE事件" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="TotalRecordEvent" DataField="TotalRecordEvent"
                        SortField="TotalRecordEvent" FieldType="String" HeaderText="自开工累计可记录HSE事件" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="ThisNoRecordEvent" DataField="ThisNoRecordEvent"
                        SortField="ThisNoRecordEvent" FieldType="String" HeaderText="本月不可记录HSE事件" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="YearNoRecordEvent" DataField="YearNoRecordEvent"
                        SortField="YearNoRecordEvent" FieldType="String" HeaderText="本年累计不可记录HSE事件" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="TotalNoRecordEvent" DataField="TotalNoRecordEvent"
                        SortField="TotalNoRecordEvent" FieldType="String" HeaderText="自开工累计不可记录HSE事件" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ProjectManagerName" DataField="ProjectManagerName"
                        SortField="ProjectManagerName" FieldType="String" HeaderText="项目经理姓名" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="ProjectManagerPhone" DataField="ProjectManagerPhone"
                        SortField="ProjectManagerPhone" FieldType="String" HeaderText="项目经理电话" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="HSEManagerName" DataField="HSEManagerName"
                        SortField="HSEManagerName" FieldType="String" HeaderText="HSE总监/经理姓名" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="HSEManagerPhone" DataField="HSEManagerPhone"
                        SortField="HSEManagerPhone" FieldType="String" HeaderText="HSE总监/经理电话" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                </Columns>
            </f:Grid>
        </Items>
    </f:Panel>
    </form>
    <script type="text/javascript">
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
