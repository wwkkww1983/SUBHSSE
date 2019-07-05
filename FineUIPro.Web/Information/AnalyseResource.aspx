<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnalyseResource.aspx.cs"
    Inherits="FineUIPro.Web.Information.AnalyseResource" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源来源统计</title> 
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />   
    <style>
        .f-grid-row-summary .f-grid-cell-inner
        {
            font-weight: bold;
            color: red;
        }
    </style> 
</head>
<body>
    <form id="form1" runat="server">
  <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" RegionPosition="Top"
                BodyPadding="0 5 0 0" Width="200px" Layout="Fit" runat="server" EnableCollapse="true">
                <Items>
                    <f:Form ID="Form2" ShowHeader="false" ShowBorder="false" runat="server">
                        <Rows>
                            <f:FormRow ColumnWidths="30% 20% 20% 20% 10%">
                                <Items>
                                     <f:DropDownList ID="drpUnit" runat="server" LabelWidth="50px" Label="单位" OnClearIconClick="drpUnit_ClearIconClick"
                                            EnableMultiSelect="true" AutoShowClearIcon="true" EnableClearIconClickEvent="true" EnableEdit="true" EnableCheckBoxSelect="true">
                                    </f:DropDownList>
                                    <f:TextBox ID="txtUserName" runat="server" LabelWidth="50px" Label="用户"></f:TextBox>
                                    <f:DatePicker runat="server" Label="开始时间" ID="txtStarTime" EnableEdit="true"></f:DatePicker>
                                    <f:DatePicker runat="server" Label="结束时间" ID="txtEndTime" EnableEdit="true"></f:DatePicker> 
                                    <f:Button ID="BtnAnalyse" Text="统计" Icon="ChartPie" runat="server" OnClick="BtnAnalyse_Click"></f:Button>
                               </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Region>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="资源来源统计" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="UserName" AllowCellEditing="true" EnableColumnLines="true"
                        ClicksToEdit="2" DataIDField="UserName" AllowSorting="true" SortField="UnitName,UserName"
                        SortDirection="DESC" OnSort="Grid1_Sort" OnPreRowDataBound="Grid1_PreRowDataBound"
                        AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                        EnableRowDoubleClickEvent="true"  AllowFilters="true"  EnableSummary="true" SummaryPosition="Flow"
                        OnFilterChange="Grid1_FilterChange">               
                        <Columns>
                          <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>                                        
                            <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" EnableFilter="true"
                                SortField="UnitName" FieldType="String" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Left">
                                <Filter EnableMultiFilter="true" ShowMatcher="true">
                                    <Operator>
                                        <f:DropDownList ID="DropDownList1" runat="server">
                                            <f:ListItem Text="等于" Value="equal" />
                                            <f:ListItem Text="包含" Value="contain" Selected="true" />
                                        </f:DropDownList>
                                    </Operator>
                                </Filter>
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="UserName" DataField="UserName" EnableFilter="true"
                                SortField="UserName" FieldType="String" HeaderText="用户" HeaderTextAlign="Center" TextAlign="Left">
                                <Filter EnableMultiFilter="true" ShowMatcher="true">
                                    <Operator>
                                        <f:DropDownList ID="DropDownList3" runat="server">
                                            <f:ListItem Text="等于" Value="equal" />
                                            <f:ListItem Text="包含" Value="contain" Selected="true" />
                                        </f:DropDownList>
                                    </Operator>
                                </Filter>
                            </f:RenderField>                     
                                <f:RenderField Width="80px" ColumnID="TotalCount" DataField="TotalCount" SortField="TotalCount"
                                FieldType="String" HeaderText="总上传数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField> 
                            <f:RenderField Width="80px" ColumnID="TotalUsedCount" DataField="TotalUsedCount" SortField="TotalUsedCount"
                                FieldType="String" HeaderText="总上传数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>    
                            <f:RenderField Width="80px" ColumnID="TotalUsedRate" DataField="TotalUsedRate" SortField="TotalUsedRate"
                                FieldType="String" HeaderText="总采用率" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>    
                            <f:RenderField Width="80px" ColumnID="LawRegulationCount" DataField="LawRegulationCount" SortField="LawRegulationCount"
                                FieldType="String" HeaderText="法律法规" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>  
                                <f:RenderField Width="80px" ColumnID="HSSEStandardListCount" DataField="HSSEStandardListCount" SortField="HSSEStandardListCount"
                                FieldType="String" HeaderText="标准规范" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="RulesRegulationsCount" DataField="RulesRegulationsCount" SortField="RulesRegulationsCount"
                                FieldType="String" HeaderText="规章制度" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>             
                            <f:RenderField Width="80px" ColumnID="ManageRuleCount" DataField="ManageRuleCount" SortField="ManageRuleCount"
                                FieldType="String" HeaderText="管理规定" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="TrainDBCount" DataField="TrainDBCount" SortField="TrainDBCount"
                                FieldType="String" HeaderText="培训教材" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>  
                    
                            <f:RenderField Width="80px" ColumnID="TrainTestDBCount" DataField="TrainTestDBCount" SortField="TrainTestDBCount"
                                FieldType="String" HeaderText="安全试题" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="AccidentCaseCount" DataField="AccidentCaseCount" SortField="AccidentCaseCount"
                                FieldType="String" HeaderText="事故案例" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="KnowledgeDBCount" DataField="KnowledgeDBCount" SortField="KnowledgeDBCount"
                                FieldType="String" HeaderText="应知应会" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="HazardListCount" DataField="HazardListCount" SortField="HazardListCount"
                                FieldType="String" HeaderText="危险源" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="RectifyCount" DataField="RectifyCount" SortField="RectifyCount"
                                FieldType="String" HeaderText="安全隐患" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HAZOPCount" DataField="HAZOPCount" SortField="HAZOPCount"
                                FieldType="String" HeaderText="HAZOP管理" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="80px" ColumnID="AppraiseCount" DataField="AppraiseCount" SortField="AppraiseCount"
                                FieldType="String" HeaderText="安全评价" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="ExpertCount" DataField="ExpertCount" SortField="ExpertCount"
                                FieldType="String" HeaderText="安全专家" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="EmergencyCount" DataField="EmergencyCount" SortField="EmergencyCount"
                                FieldType="String" HeaderText="应急预案" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="SpecialSchemeCount" DataField="SpecialSchemeCount" SortField="SpecialSchemeCount"
                                FieldType="String" HeaderText="专项方案" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField> 
                        </Columns>               
                        <PageItems>
                            <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                            </f:ToolbarSeparator>
                            <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                            </f:ToolbarText>
                            <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <f:ListItem Text="10" Value="10" />
                                <f:ListItem Text="15" Value="15" />
                                <f:ListItem Text="20" Value="20" />
                                <f:ListItem Text="25" Value="25" />
                                <f:ListItem Text="所有行" Value="100000" />
                            </f:DropDownList>
                        </PageItems>
                    </f:Grid>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    </form>
</body>
</html>
