<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeadMonthReportBEdit.aspx.cs"
    Inherits="FineUIPro.Web.ManagementReport.HeadMonthReportBEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑管理月报</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtMonthReportCode" runat="server" Label="编号" MaxLength="50">
                    </f:TextBox>
                    <f:Label runat="server" ID="txtMonths" Label="月报月份">
                    </f:Label>
                    <f:DatePicker runat="server" Label="填报日期" ID="txtReportDate" LabelAlign="right">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtReportUnitName" runat="server" Label="填报单位">
                    </f:TextBox>
                    <f:TextBox ID="txtReportMan" runat="server" Label="填报人">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckMan" runat="server" Label="审核人">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="GridProjectStation" ShowBorder="true" ShowHeader="false" Title="项目施工现场人工时分类统计"
                        runat="server" ClicksToEdit="1" DataIDField="ProjectCode" DataKeyNames="ProjectCode"
                        EnableMultiSelect="false" ShowGridHeader="true" Height="420px" EnableColumnLines="true"
                        EnableSummary="true" SummaryPosition="Flow" AutoScroll="true">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                                <Items>
                                    <f:RadioButtonList runat="server" ID="rblState" ColumnNumber="4" AutoPostBack="true"
                                        OnSelectedIndexChanged="rblState_SelectedIndexChanged">
                                        <f:RadioItem Value="0"  Text="全部" />
                                        <f:RadioItem Value="1" Selected="true" Text="施工" />
                                        <f:RadioItem Value="2" Text="暂停" />
                                        <f:RadioItem Value="3" Text="完工" />
                                    </f:RadioButtonList>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                TextAlign="Center" />
                            <f:RenderField Width="100px" ColumnID="ProjectCode" DataField="ProjectCode" SortField="ProjectCode"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="项目代码">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="ProjectName" DataField="ProjectName" SortField="ProjectName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="项目名称">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="ProjectManager" DataField="ProjectManager"
                                SortField="ProjectManager" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                HeaderText="项目经理">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="ProjectType" DataField="ProjectType" SortField="ProjectType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="项目类型">
                            </f:RenderField>
                            <f:RenderField Width="110px" ColumnID="StartDate" DataField="StartDate" SortField="StartDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="项目开工日期"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="110px" ColumnID="EndDate" DataField="EndDate" SortField="EndDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="项目竣工日期"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:GroupField EnableLock="true" HeaderText="完成工时" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="Manhours" DataField="Manhours" FieldType="String"
                                        HeaderText="当月" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="TotalManhours" DataField="TotalManhours" FieldType="String"
                                        HeaderText="累计" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="项目安全工时" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="HseManhours" DataField="HseManhours" FieldType="String"
                                        HeaderText="当月" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="TotalHseManhours" DataField="TotalHseManhours"
                                        FieldType="String" HeaderText="累计" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:RenderField Width="110px" ColumnID="TotalManNum" DataField="TotalManNum" SortField="TotalManNum"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="当月员工总数">
                            </f:RenderField>
                            <f:GroupField EnableLock="true" HeaderText="事故起数" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="AccidentNum" DataField="AccidentNum" FieldType="String"
                                        HeaderText="当月" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="AccidentTotalNum" DataField="AccidentTotalNum"
                                        FieldType="String" HeaderText="累计" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="损失工时" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="LoseHours" DataField="LoseHours" FieldType="String"
                                        HeaderText="当月" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="TotalLoseHours" DataField="TotalLoseHours"
                                        FieldType="String" HeaderText="累计" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:RenderField Width="100px" ColumnID="AvgA" DataField="AvgA" SortField="AvgA" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Left" HeaderText="可记录事故率">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="AvgB" DataField="AvgB" SortField="AvgB" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Left" HeaderText="损失工时事故率">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="AvgC" DataField="AvgC" SortField="AvgC" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Left" HeaderText="事故伤害严重率">
                            </f:RenderField>
                            <f:RenderField Width="140px" ColumnID="HazardNum" DataField="HazardNum" SortField="HazardNum"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="危险性较大的清单数">
                            </f:RenderField>
                            <f:RenderField Width="240px" ColumnID="IsArgumentHazardNum" DataField="IsArgumentHazardNum"
                                SortField="IsArgumentHazardNum" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                HeaderText="需要专家论证的危险性较大的清单数">
                            </f:RenderField>
                            <f:RenderField Width="130px" ColumnID="PlanCostA" DataField="PlanCostA" SortField="PlanCostA"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="安全生产费计划额">
                            </f:RenderField>
                            <f:RenderField Width="170px" ColumnID="PlanCostB" DataField="PlanCostB" SortField="PlanCostB"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="文明施工措施费计划额">
                            </f:RenderField>
                            <f:GroupField EnableLock="true" HeaderText="实际支出" TextAlign="Center">
                                <Columns>
                                    <f:GroupField EnableLock="true" HeaderText="A-安全生产合计" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="89px" ColumnID="RealCostA" DataField="RealCostA" FieldType="String"
                                                HeaderText="当期" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                            <f:RenderField Width="89px" ColumnID="RealCostYA" DataField="RealCostYA" FieldType="String"
                                                HeaderText="当年合计" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="RealCostPA" DataField="RealCostPA" FieldType="String"
                                                HeaderText="项目累计" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="B-文明施工合计" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="89px" ColumnID="RealCostB" DataField="RealCostB" FieldType="String"
                                                HeaderText="当期" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                            <f:RenderField Width="89px" ColumnID="RealCostYB" DataField="RealCostYB" FieldType="String"
                                                HeaderText="当年合计" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="RealCostPB" DataField="RealCostPB" FieldType="String"
                                                HeaderText="项目累计" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="A+B合计" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="89px" ColumnID="RealCostAB" DataField="RealCostAB" FieldType="String"
                                                HeaderText="当期" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                            <f:RenderField Width="89px" ColumnID="RealCostYAB" DataField="RealCostYAB" FieldType="String"
                                                HeaderText="当年合计" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="RealCostPAB" DataField="RealCostPAB" FieldType="String"
                                                HeaderText="项目累计" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                </Columns>
                            </f:GroupField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel11" Layout="Anchor" Title="事故统计" runat="server">
                        <Items>
                            <f:Form ID="Form10" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow ColumnWidths="5% 10% 5% 10% 10% 10% 10% 10% 10% 10% 10%">
                                        <Items>
                                            <f:Label runat="server" ID="Label5" Text="">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label48" Text="事故类型">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label3" Text="">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label49" Text="发生次数(当月)">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label51" Text="发生次数(累计)">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label52" Text="人数(当月)">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label53" Text="人数(累计)">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label54" Text="损失工时(当月)">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label55" Text="损失工时(累计)">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label56" Text="经济损失（当月）">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label57" Text="经济损失（累计）">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Panel ID="Panel2" runat="server" ShowBorder="false" Layout="Table" TableConfigColumns="10"
                                                ShowHeader="false" BodyPadding="1px">
                                                <Items>
                                                    <f:Panel ID="Panel1" Title="Panel1" TableRowspan="6" MarginRight="0" Width="100px"
                                                        runat="server" BodyPadding="1px" ShowBorder="false" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="Label58" Text="人 身 伤 害 事 故" TableRowspan="6">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel3" Title="Panel1" runat="server" BodyPadding="1px" Width="140px"
                                                        ShowBorder="false" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="lblAccidentType11" Text="死 亡 事 故">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel5" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtNumber11" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel4" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumNumber11" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel6" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtPersonNum11" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel7" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumPersonNum11" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel8" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtLoseHours11" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel9" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumLoseHours11" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel10" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtLoseMoney11" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel11" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumLoseMoney11" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel12" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="140px" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="lblAccidentType12" Text="重 伤 事 故">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel13" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtNumber12" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel14" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumNumber12" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel15" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtPersonNum12" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel16" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumPersonNum12" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel17" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtLoseHours12" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel18" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumLoseHours12" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel19" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtLoseMoney12" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel20" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumLoseMoney12" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel21" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="140px" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="lblAccidentType13" Text="轻 伤 事 故">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel22" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtNumber13" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel23" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumNumber13" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel24" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtPersonNum13" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel25" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumPersonNum13" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel26" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtLoseHours13" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel27" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumLoseHours13" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel28" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtLoseMoney13" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel29" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumLoseMoney13" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel30" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="140px" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="lblAccidentType14" Text="工 作 受 限 事 故">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel31" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtNumber14" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel32" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumNumber14" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel33" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtPersonNum14" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel34" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumPersonNum14" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel35" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtLoseHours14" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel36" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumLoseHours14" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel37" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtLoseMoney14" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel38" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumLoseMoney14" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel39" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="140px" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="lblAccidentType15" Text="医 疗 处 理">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel40" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtNumber15" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel41" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumNumber15" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel42" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtPersonNum15" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel43" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumPersonNum15" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel44" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtLoseHours15" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel45" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumLoseHours15" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel46" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtLoseMoney15" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel47" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumLoseMoney15" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel48" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="140px" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="lblAccidentType16" Text="现场处置（急救）">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel49" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtNumber16" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel50" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumNumber16" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel51" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtPersonNum16" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel52" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumPersonNum16" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel53" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtLoseHours16" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel54" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumLoseHours16" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel55" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtLoseMoney16" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel56" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="122px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox runat="server" ID="txtSumLoseMoney16" Readonly="true">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                </Items>
                                            </f:Panel>
                                        </Items>
                            </f:Panel>
                        </Items>
                    </f:GroupPanel>
            </f:FormRow>
            <f:FormRow ColumnWidths="20% 10% 10% 10% 10% 10% 10% 10% 10%">
                <Items>
                    <f:Label runat="server" ID="lblAccidentType21" Text="未 遂 事 故" TableColspan="2">
                    </f:Label>
                    <f:TextBox runat="server" ID="txtNumber21" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumNumber21" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtPersonNum21" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumPersonNum21" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseHours21" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseHours21" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseMoney21" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseMoney21" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="20% 10% 10% 10% 10% 10% 10% 10% 10%">
                <Items>
                    <f:Label runat="server" ID="lblAccidentType22" Text="火 灾 事 故" TableColspan="2">
                    </f:Label>
                    <f:TextBox runat="server" ID="txtNumber22" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumNumber22" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtPersonNum22" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumPersonNum22" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseHours22" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseHours22" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseMoney22" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseMoney22" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="20% 10% 10% 10% 10% 10% 10% 10% 10%">
                <Items>
                    <f:Label runat="server" ID="lblAccidentType23" Text="爆 炸 事 故" TableColspan="2">
                    </f:Label>
                    <f:TextBox runat="server" ID="txtNumber23" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumNumber23" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtPersonNum23" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumPersonNum23" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseHours23" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseHours23" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseMoney23" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseMoney23" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="20% 10% 10% 10% 10% 10% 10% 10% 10%">
                <Items>
                    <f:Label runat="server" ID="lblAccidentType24" Text="道 路 交 通 事 故" TableColspan="2">
                    </f:Label>
                    <f:TextBox runat="server" ID="txtNumber24" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumNumber24" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtPersonNum24" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumPersonNum24" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseHours24" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseHours24" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseMoney24" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseMoney24" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="20% 10% 10% 10% 10% 10% 10% 10% 10%">
                <Items>
                    <f:Label runat="server" ID="lblAccidentType25" Text="机 械 设 备 事 故" TableColspan="2">
                    </f:Label>
                    <f:TextBox runat="server" ID="txtNumber25" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumNumber25" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtPersonNum25" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumPersonNum25" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseHours25" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseHours25" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseMoney25" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseMoney25" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="20% 10% 10% 10% 10% 10% 10% 10% 10%">
                <Items>
                    <f:Label runat="server" ID="lblAccidentType26" Text="环 境 污 染 事 故" TableColspan="2">
                    </f:Label>
                    <f:TextBox runat="server" ID="txtNumber26" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumNumber26" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtPersonNum26" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumPersonNum26" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseHours26" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseHours26" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseMoney26" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseMoney26" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="20% 10% 10% 10% 10% 10% 10% 10% 10%">
                <Items>
                    <f:Label runat="server" ID="lblAccidentType27" Text="职业病" TableColspan="2">
                    </f:Label>
                    <f:TextBox runat="server" ID="txtNumber27" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumNumber27" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtPersonNum27" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumPersonNum27" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseHours27" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseHours27" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseMoney27" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseMoney27" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="20% 10% 10% 10% 10% 10% 10% 10% 10%">
                <Items>
                    <f:Label runat="server" ID="lblAccidentType28" Text="生产事故" TableColspan="2">
                    </f:Label>
                    <f:TextBox runat="server" ID="txtNumber28" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumNumber28" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtPersonNum28" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumPersonNum28" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseHours28" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseHours28" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseMoney28" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseMoney28" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="20% 10% 10% 10% 10% 10% 10% 10% 10%">
                <Items>
                    <f:Label runat="server" ID="lblAccidentType29" Text="其它事故" TableColspan="2">
                    </f:Label>
                    <f:TextBox runat="server" ID="txtNumber29" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumNumber29" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtPersonNum29" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumPersonNum29" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseHours29" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseHours29" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtLoseMoney29" Readonly="true">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtSumLoseMoney29" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="45% 5% 45%">
                <Items>
                    <f:Label runat="server" ID="Label6" Text="">
                    </f:Label>
                    <f:Label runat="server" ID="Label59" Text="事故数据">
                    </f:Label>
                    <f:Label runat="server" ID="Label10" Text="">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" ID="txtAccidentNum" Readonly="true" Label="可记录事故数" LabelWidth="200px">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtAccidentRateA" Readonly="true" Label="百万工时总可记录事故率"
                        LabelWidth="200px">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtAccidentRateB" Readonly="true" Label="百万工时损失工时率"
                        LabelWidth="200px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" ID="txtAccidentRateC" Readonly="true" Label="百万工时损失工时伤害事故率"
                        LabelWidth="200px">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtAccidentRateD" Readonly="true" Label="百万工时死亡事故频率"
                        LabelWidth="200px">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtAccidentRateE" Readonly="true" Label="百万工时事故死亡率"
                        LabelWidth="200px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="45% 5% 45%">
                <Items>
                    <f:Label runat="server" ID="Label12" Text="">
                    </f:Label>
                    <f:Label runat="server" ID="Label60" Text="事故台账">
                    </f:Label>
                    <f:Label runat="server" ID="Label13" Text="">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="GridAccidentDetailSort" ShowBorder="true" ShowHeader="false" Title="事故台账" runat="server"
                        ClicksToEdit="1" DataIDField="AccidentDetailSortId" DataKeyNames="AccidentDetailSortId" EnableMultiSelect="false"
                        ShowGridHeader="true" Height="220px" EnableColumnLines="true" AutoScroll="true">
                        <Columns>
                            <f:RowNumberField HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:TemplateField Width="100px" HeaderText="项目代码" HeaderTextAlign="Center" TextAlign="Center" ColumnID="ProjectCode"
                                ExpandUnusedSpace="true">
                                <ItemTemplate>
                                    <asp:Label ID="Label14" runat="server" Text='<%# ConvertProjectCode(Eval("MonthReportId")) %>'
                                        ToolTip='<%# ConvertProjectCode(Eval("MonthReportId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="200px" HeaderText="项目名称" HeaderTextAlign="Center" TextAlign="Center" ColumnID="ProjectName"
                                ExpandUnusedSpace="true">
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Text='<%# ConvertProjectName(Eval("MonthReportId")) %>'
                                        ToolTip='<%# ConvertProjectName(Eval("MonthReportId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="90px" HeaderText="项目经理" HeaderTextAlign="Center" TextAlign="Center" ColumnID="ProjectManagerName"
                                ExpandUnusedSpace="true">
                                <ItemTemplate>
                                    <asp:Label ID="Label16" runat="server" Text='<%# ConvertProjectManagerName(Eval("MonthReportId")) %>'
                                        ToolTip='<%# ConvertProjectManagerName(Eval("MonthReportId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="150px" ColumnID="Abstract" DataField="Abstract" SortField="Abstract"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="提要">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="AccidentType" DataField="AccidentType" SortField="AccidentType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="事故类型">
                            </f:RenderField>
                            <f:RenderField Width="50px" ColumnID="PeopleNum" DataField="PeopleNum" SortField="PeopleNum"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="人数">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="WorkingHoursLoss" DataField="WorkingHoursLoss" SortField="WorkingHoursLoss"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="工时损失">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="EconomicLoss" DataField="EconomicLoss" SortField="EconomicLoss"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="经济损失">
                            </f:RenderField>
                            <f:RenderField Width="95px" ColumnID="AccidentDate" DataField="AccidentDate" SortField="AccidentDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="发生时间"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    </Items> </f:GroupPanel> </Items> </f:FormRow>
    <f:FormRow>
        <Items>
            <f:GroupPanel ID="GroupPanel16" Layout="Anchor" Title="项目施工现场安全培训情况统计" runat="server">
                <Items>
                    <f:Grid ID="GridTrainSort" ShowBorder="true" ShowHeader="false" Title="项目施工现场安全培训情况统计"
                        runat="server" ClicksToEdit="1" DataIDField="TrainSortId" DataKeyNames="TrainSortId"
                        EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                        EnableSummary="true" SummaryPosition="Flow" AutoScroll="true">
                        <Columns>
                            <f:RenderField Width="150px" ColumnID="TrainType" DataField="TrainType" SortField="TrainType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="培 训 课 程 类 型">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="TrainNumber" DataField="TrainNumber" SortField="TrainNumber"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="培训次数(当月)">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="TotalTrainNum" DataField="TotalTrainNum" SortField="TotalTrainNum"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="培训次数(累计)">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="TrainPersonNumber" DataField="TrainPersonNumber"
                                SortField="TrainPersonNumber" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                HeaderText="培训人数(当月)">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="TotalTrainPersonNum" DataField="TotalTrainPersonNum"
                                SortField="TotalTrainPersonNum" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                HeaderText="培训人数(累计)">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:GroupPanel>
        </Items>
    </f:FormRow>
    <f:FormRow>
        <Items>
            <f:GroupPanel ID="GroupPanel15" Layout="Anchor" Title="项目施工现场安全会议情况统计" runat="server">
                <Items>
                    <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lb1" Text="安 全 会 议 类 型">
                                    </f:Label>
                                    <f:Label runat="server" ID="Label1" Text="会议次数(当月)">
                                    </f:Label>
                                    <f:Label runat="server" ID="Label2" Text="会议次数(累计)">
                                    </f:Label>
                                    <f:Label runat="server" ID="Label62" Text="参会人数(当月)">
                                    </f:Label>
                                    <f:Label runat="server" ID="Label63" Text="参会人数(累计)">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lblMeetingType1" Text="安 全 周 会">
                                    </f:Label>
                                    <f:TextBox ID="txtMeetingNumber1" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSumMeetingNumber1" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtMeetingPersonNumber1" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSumMeetingPersonNumber1" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lblMeetingType2" Text="安 全 月 会">
                                    </f:Label>
                                    <f:TextBox ID="txtMeetingNumber2" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSumMeetingNumber2" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtMeetingPersonNumber2" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSumMeetingPersonNumber2" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lblMeetingType3" Text="专 项 安 全 会 议">
                                    </f:Label>
                                    <f:TextBox ID="txtMeetingNumber3" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSumMeetingNumber3" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtMeetingPersonNumber3" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSumMeetingPersonNumber3" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="Label7" Text="合 计">
                                    </f:Label>
                                    <f:TextBox ID="txtAllMeetingNumber" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtAllSumMeetingNumber" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtAllMeetingPersonNumber" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtAllSumMeetingPersonNumber" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:GroupPanel>
        </Items>
    </f:FormRow>
    <f:FormRow>
        <Items>
            <f:GroupPanel ID="GroupPanel17" Layout="Anchor" Title="项目施工现场安全检查情况统计" runat="server">
                <Items>
                    <f:Form ID="Form12" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="Label64" Text="安 全 检 查 类 型">
                                    </f:Label>
                                    <f:Label runat="server" ID="Label65" Text="检查次数(当月)">
                                    </f:Label>
                                    <f:Label runat="server" ID="Label66" Text="检查次数(累计)">
                                    </f:Label>
                                    <f:Label runat="server" ID="Label67" Text="违章数量(当月)">
                                    </f:Label>
                                    <f:Label runat="server" ID="Label68" Text="违章数量(累计)">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lblCheckType1" Text="安 全 日 常 巡 查">
                                    </f:Label>
                                    <f:TextBox ID="txtCheckNumber1" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSumCheckNumber1" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtViolationNumber1" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSumViolationNumber1" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lblCheckType2" Text="每 周 安 全 检 查">
                                    </f:Label>
                                    <f:TextBox ID="txtCheckNumber2" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSumCheckNumber2" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtViolationNumber2" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSumViolationNumber2" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lblCheckType3" Text="每 月 安 全 大 检 查">
                                    </f:Label>
                                    <f:TextBox ID="txtCheckNumber3" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSumCheckNumber3" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtViolationNumber3" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSumViolationNumber3" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lblCheckType4" Text="专 项 安 全 检 查">
                                    </f:Label>
                                    <f:TextBox ID="txtCheckNumber4" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSumCheckNumber4" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtViolationNumber4" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtSumViolationNumber4" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="Label72" Text="合 计">
                                    </f:Label>
                                    <f:TextBox ID="txtAllCheckNumber" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtAllSumCheckNumber" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtAllViolationNumber" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                    <f:TextBox ID="txtAllSumViolationNumber" runat="server" Label="" Readonly="true">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:GroupPanel>
        </Items>
    </f:FormRow>
    <f:FormRow>
        <Items>
            <f:GroupPanel ID="GroupPanel18" Layout="Anchor" Title="项目施工现场安全奖惩情况统计" runat="server">
                <Items>
                    <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="Label18" Text="类型">
                                    </f:Label>
                                    <f:Label runat="server" ID="Label8" Text="内容">
                                    </f:Label>
                                    <f:Label runat="server" ID="Label19" Text="当月">
                                    </f:Label>
                                    <f:Label runat="server" ID="Label20" Text="累计">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Panel ID="Panel57" runat="server" ShowBorder="false" Title="" Layout="Table" TableConfigColumns="4"
                                        ShowHeader="false" BodyPadding="1px">
                                        <Items>
                                            <f:Panel ID="Panel58" Title="Panel1" MarginRight="0" TableRowspan="4" runat="server"
                                                Width="300px" BodyPadding="1px" ShowBorder="false" ShowHeader="false">
                                                <Items>
                                                    <f:Label runat="server" ID="Label9" Text="奖 励" TableRowspan="4">
                                                    </f:Label>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel59" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:Label runat="server" ID="lblIncentiveType11" Text="安 全 明 星 奖 （金 额）">
                                                    </f:Label>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel60" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtIncentiveMoney1" Readonly="true" runat="server">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel61" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtSumIncentiveMoney1" Readonly="true" runat="server">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel62" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:Label runat="server" ID="lblIncentiveType12" Text="百 万 工 时 无 事 故 奖 （金 额）">
                                                    </f:Label>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel63" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtIncentiveMoney2" Readonly="true" runat="server">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel64" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtSumIncentiveMoney2" Readonly="true" runat="server">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel65" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:Label runat="server" ID="lblIncentiveType13" Text="安 全 目 标 兑 现 奖（金 额）">
                                                    </f:Label>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel66" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtIncentiveMoney3" Readonly="true" runat="server">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel67" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtSumIncentiveMoney3" Readonly="true" runat="server">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel68" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:Label runat="server" ID="lblIncentiveType14" Text="其 它 奖 励">
                                                    </f:Label>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel69" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtIncentiveMoney4" Readonly="true" runat="server">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel70" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtSumIncentiveMoney4" Readonly="true" runat="server">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel71" Title="Panel1" MarginRight="0" TableRowspan="4" runat="server"
                                                Width="300px" BodyPadding="1px" ShowBorder="false" ShowHeader="false">
                                                <Items>
                                                    <f:Label runat="server" ID="Label11" Text="处 罚">
                                                    </f:Label>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel72" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:Label runat="server" ID="lblIncentiveType21" Text="通 报 批 评 （人/次）">
                                                    </f:Label>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel73" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtIncentiveNumber1" Readonly="true" runat="server">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel74" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtSumIncentiveNumber1" Readonly="true" runat="server">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel75" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:Label runat="server" ID="lblIncentiveType22" Text="开 除 （人/次）">
                                                    </f:Label>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel76" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtIncentiveNumber2" Readonly="true" runat="server">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel77" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtSumIncentiveNumber2" Readonly="true" runat="server">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel78" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:Label runat="server" ID="lblIncentiveType15" Text="罚 款 （金 额）">
                                                    </f:Label>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel79" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtIncentiveMoney5" Readonly="true" runat="server">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                            <f:Panel ID="Panel80" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                Width="300px" ShowHeader="false">
                                                <Items>
                                                    <f:TextBox ID="txtSumIncentiveMoney5" Readonly="true" runat="server">
                                                    </f:TextBox>
                                                </Items>
                                            </f:Panel>
                                        </Items>
                                    </f:Panel>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:GroupPanel>
        </Items>
    </f:FormRow>
    </Rows>
    <toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:HiddenField ID="hdId" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdAttachUrl" runat="server">
                    </f:HiddenField>
                </Items>
            </f:Toolbar>
        </toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
<script type="text/javascript">
    function onGridAfterEdit(event, value, params) {
        updateSummary();
    }

    function updateSummary() {
        // 回发到后台更新
        __doPostBack('', 'UPDATE_SUMMARY');
    }
</script>
