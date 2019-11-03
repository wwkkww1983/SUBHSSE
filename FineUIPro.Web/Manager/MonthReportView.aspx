<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportView.aspx.cs" Inherits="FineUIPro.Web.Manager.MonthReportView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看管理月报</title>
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
                    <f:TextBox ID="txtMonthReportCode" runat="server" Label="编号" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtReportMonths" runat="server" Label="月报月份" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtMonthReportStartDate" runat="server" Label="报告日期" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtMonthReportDate" runat="server" Label="至" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtReportMan" runat="server" Label="报告人" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel2" Layout="Anchor" Title="项目总体数据统计" runat="server">
                        <Items>
                            <f:TextArea ID="txtAllProjectData" runat="server" Height="100px" Readonly="true">
                            </f:TextArea>
                            <f:TextArea ID="txtAllManhoursData" runat="server" Height="50px" Readonly="true">
                            </f:TextArea>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="教育与培训情况统计" runat="server"
                        ClicksToEdit="1" DataIDField="TrainSortId" DataKeyNames="TrainSortId,TrainTypeName"
                        EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                        >
                        <Columns>
                            <f:RenderField Width="200px" ColumnID="TrainTypeName" DataField="TrainTypeName" ExpandUnusedSpace="true"
                                FieldType="String" HeaderText="安全培训课程类型" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField> 
                            <f:RenderField Width="150px" ColumnID="TrainNumber11" DataField="TrainNumber11" SortField="TrainNumber11"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="本月培训次数">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="TrainNumber12" DataField="TrainNumber12" SortField="TrainNumber12"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="累计次数">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="TrainNumber13" DataField="TrainNumber13" SortField="TrainNumber13"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="本月培训人数">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="TrainNumber14" DataField="TrainNumber14" SortField="TrainNumber14"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="累计人数">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Panel ID="Panel1" Title="会议情况统计" BoxFlex="5" MarginRight="5px" runat="server"
                        ShowBorder="false" ShowHeader="true">
                        <Items>
                            <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:Label runat="server" ID="lb1" Text="安全会议类型">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label1" Text="本月会议次数">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label2" Text="累计次数">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Label runat="server" ID="Label3" Text="安全周会">
                                            </f:Label>
                                            <f:TextBox ID="txtMeetingNumber01" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtMeetingNumber11" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Label runat="server" ID="Label5" Text="安全月会">
                                            </f:Label>
                                            <f:TextBox ID="txtMeetingNumber02" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtMeetingNumber12" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Label runat="server" ID="Label6" Text="专项安全会议">
                                            </f:Label>
                                            <f:TextBox ID="txtMeetingNumber03" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtMeetingNumber13" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Label runat="server" ID="Label7" Text="其它">
                                            </f:Label>
                                            <f:NumberBox ID="txtMeetingNumber04" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtMeetingNumber14" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Readonly="true">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </Items>
                    </f:Panel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Panel ID="Panel2" Title="检查情况统计" BoxFlex="5" MarginRight="5px" runat="server"
                        ShowBorder="false" ShowHeader="true">
                        <Items>
                            <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:Label runat="server" ID="Label8" Text="安全检查类型">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label9" Text="本月检查次数">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label10" Text="累计次数">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label15" Text="本月违章数量">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label16" Text="累计数量">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Label runat="server" ID="Label11" Text="安全日常巡检">
                                            </f:Label>
                                            <f:TextBox ID="txtCheckNumber01" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtCheckNumber02" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtCheckNumber03" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtCheckNumber04" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Label runat="server" ID="Label12" Text="安全专项检查">
                                            </f:Label>
                                            <f:TextBox ID="txtCheckNumber11" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtCheckNumber12" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtCheckNumber13" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtCheckNumber14" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Label runat="server" ID="Label13" Text="安全综合大检查">
                                            </f:Label>
                                            <f:TextBox ID="txtCheckNumber21" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtCheckNumber22" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtCheckNumber23" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtCheckNumber24" runat="server" Label="" Readonly="true">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Label runat="server" ID="Label14" Text="其他">
                                            </f:Label>
                                            <f:NumberBox ID="txtCheckNumber31" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtCheckNumber32" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtCheckNumber33" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtCheckNumber34" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Readonly="true">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </Items>
                    </f:Panel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="true" Title="事故分类统计" runat="server"
                        ClicksToEdit="1" DataIDField="AccidentSortId" DataKeyNames="AccidentSortId,AccidentTypeId"
                        EnableMultiSelect="false" ShowGridHeader="true" Height="300px" EnableColumnLines="true" EnableSummary="true" SummaryPosition="Flow"
                        >
                        <Columns>
                            <f:TemplateField Width="200px" HeaderText="事故类型" HeaderTextAlign="Center" TextAlign="Left"
                                ExpandUnusedSpace="true" ColumnID="TrainTypeId">
                                <ItemTemplate>
                                    <asp:Label ID="Label17" runat="server" Text='<%# ConvertAccidentType(Eval("AccidentTypeId")) %>'
                                        ToolTip='<%# ConvertAccidentType(Eval("AccidentTypeId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="150px" ColumnID="AccidentNumber01" DataField="AccidentNumber01"
                                FieldType="Int" HeaderText="本月发生次数" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:NumberBox ID="txtAccidentNumber01" NoDecimal="true" NoNegative="true" MinValue="0"
                                        runat="server" Required="true" Readonly="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="AccidentNumber02" DataField="AccidentNumber02"
                                FieldType="Int" HeaderText="累计次数" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:NumberBox ID="txtAccidentNumber02" NoDecimal="true" NoNegative="true" MinValue="0"
                                        runat="server" Required="true" Readonly="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Panel ID="Panel3" Title="安全奖惩情况统计" BoxFlex="5" MarginRight="5px" runat="server"
                        ShowBorder="false" ShowHeader="true">
                        <Items>
                            <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="奖励" runat="server">
                                <Items>
                                    <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:Label runat="server" ID="Label18" Text="类型">
                                                    </f:Label>
                                                    <f:Label runat="server" ID="Label19" Text="当月">
                                                    </f:Label>
                                                    <f:Label runat="server" ID="Label20" Text="累计">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:Label runat="server" ID="Label23" Text="安全个人奖（金额）">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtIncentiveNumber01" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtIncentiveNumber11" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:Label runat="server" ID="Label24" Text="施工单位奖（金额）">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtIncentiveNumber02" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtIncentiveNumber12" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:Label runat="server" ID="Label25" Text="其它">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtIncentiveNumber03" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtIncentiveNumber13" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel3" Layout="Anchor" Title="处罚" runat="server">
                                <Items>
                                    <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:Label runat="server" ID="Label21" Text="类型">
                                                    </f:Label>
                                                    <f:Label runat="server" ID="Label22" Text="当月">
                                                    </f:Label>
                                                    <f:Label runat="server" ID="Label26" Text="累计">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:Label runat="server" ID="Label27" Text="通报批评（人/次）">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtIncentiveNumber04" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtIncentiveNumber14" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:Label runat="server" ID="Label28" Text="开除（人/次）">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtIncentiveNumber05" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtIncentiveNumber15" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:Label runat="server" ID="Label29" Text="罚款（金 额）">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtIncentiveNumber06" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtIncentiveNumber16" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:Label runat="server" ID="Label30" Text="其它处罚">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtIncentiveNumber07" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtIncentiveNumber17" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                        </Items>
                    </f:Panel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Panel ID="Panel4" Title="其它" BoxFlex="5" MarginRight="5px" runat="server" ShowBorder="false"
                        ShowHeader="true">
                        <Items>
                            <f:Form ID="Form6" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow ColumnWidths="30% 5% 65%">
                                        <Items>
                                            <f:NumberBox ID="txtHseNumber01" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Label="本月现场施工车辆总共" LabelWidth="160px" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label runat="server" ID="Label31" Text="台。">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label41">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ColumnWidths="30% 30% 5% 35%">
                                        <Items>
                                            <f:NumberBox ID="txtHseNumber02" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Label="本月施工机械设备" LabelWidth="135px" Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtHseNumber03" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Label="台。其中特种设备" LabelWidth="135px" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label runat="server" ID="Label32" Text="台。">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label42">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ColumnWidths="30% 30% 5% 35%">
                                        <Items>
                                            <f:NumberBox ID="txtHseNumber04" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Label="本月隐患整改总共" LabelWidth="135px" Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtHseNumber05" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Label="项，已经整改完成" LabelWidth="135px" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label runat="server" ID="Label33" Text="项。">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label43">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ColumnWidths="30% 30% 5% 35%">
                                        <Items>
                                            <f:NumberBox ID="txtHseNumber06" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Label="本月方案审批数量" LabelWidth="135px" Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtSpecialNumber" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Label="。其中专项方案数量" LabelWidth="150px" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label runat="server" ID="Label34" Text="。">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label44">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:NumberBox ID="txtHseNumber07" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Label="本月共计签发隐患整改单" LabelWidth="175px" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label runat="server" ID="Label35" Text="份。">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:NumberBox ID="txtHseNumber08" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Label="本月共计签发《隐患整改暂时停止作业令》" LabelWidth="290px" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label runat="server" ID="Label36" Text="份。">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:NumberBox ID="txtHseNumber09" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Label="本月共计办理作业许可证" LabelWidth="175px" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label runat="server" ID="Label37" Text="份。">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:NumberBox ID="txtHseNumber00" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server" Label="本月共计签发处罚单" LabelWidth="150px" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label runat="server" ID="Label38" Text="份。">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:NumberBox ID="txtHseNumber10" NoDecimal="false" NoNegative="true" MinValue="0"
                                                runat="server" Label="入场安全培训率（现场参加培训人数与现场全部作业人数的比值）" LabelWidth="420px" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label runat="server" ID="Label39" Text="%">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:NumberBox ID="txtHseNumber11" NoDecimal="false" NoNegative="true" MinValue="0"
                                                runat="server" Label="机械设备检验率（已检机械设备数量与全部在用机械设备数量的比值）" LabelWidth="445px" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label runat="server" ID="Label40" Text="%">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </Items>
                    </f:Panel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel4" Layout="Anchor" Title="机具设备投入情况" runat="server">
                        <Items>
                            <f:TextArea ID="txtEquipmentQualityData" runat="server" Height="100px" Readonly="true">
                            </f:TextArea>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel5" Layout="Anchor" Title="安全费用情况" runat="server">
                        <Items>
                            <f:Form ID="Form7" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtThisMonthSafetyCost" runat="server" Label="本月" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtTotalSafetyCost" runat="server" Label="累计" Readonly="true">
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
                    <f:GroupPanel ID="GroupPanel6" Layout="Anchor" Title="本月监控重点及措施（风险辨识及控制，职业健康及环境卫生等）"
                        runat="server">
                        <Items>
                            <f:TextArea ID="txtThisMonthKeyPoints" runat="server" Height="100px" Readonly="true">
                            </f:TextArea>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel7" Layout="Anchor" Title="本月主要安全活动" runat="server">
                        <Items>
                            <f:TextArea ID="txtThisMonthSafetyActivity" runat="server" Height="100px" Readonly="true">
                            </f:TextArea>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel8" Layout="Anchor" Title="下月工作重点" runat="server">
                        <Items>
                            <f:TextArea ID="txtNextMonthWorkFocus" runat="server" Height="100px" Readonly="true">
                            </f:TextArea>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:HiddenField ID="hdId" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdAttachUrl" runat="server">
                    </f:HiddenField>
                </Items>
            </f:Toolbar>
        </Toolbars>
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