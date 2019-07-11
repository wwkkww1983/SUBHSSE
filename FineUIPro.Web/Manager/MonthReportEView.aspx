<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportEView.aspx.cs" Inherits="FineUIPro.Web.Manager.MonthReportEView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看海外工程项目月度HSSE统计表</title>
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
                    <f:Label ID="txtMonths" runat="server" Label="月报月份">
                    </f:Label>
                    <f:Label ID="txtMonthReportDate" runat="server" Label="报告日期">
                    </f:Label>
                    <f:TextBox ID="txtReportMan" runat="server" Label="报告人" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel2" Layout="Anchor" Title="1.项目情况" runat="server">
                        <Items>
                            <f:Form ID="Form8" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox runat="server" ID="txtProjectName" Readonly="true" Label="项目名称">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox runat="server" ID="txtCountryCities" Label="所在国别/城市" Readonly="true" >
                                            </f:TextBox>
                                            <f:TextBox runat="server" ID="txtStartEndDate" Label="开－竣工日期" LabelWidth="120px" Readonly="true">
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
                    <f:GroupPanel ID="GroupPanel9" Layout="Anchor" Title="2.合同情况" runat="server">
                        <Items>
                            <f:Form ID="Form9" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox runat="server" ID="txtContractType" Label="合同类别" Readonly="true" >
                                            </f:TextBox>
                                            <f:NumberBox runat="server" ID="txtContractAmount" Label="合同额(万元)" NoDecimal="false" Readonly="true" 
                                                NoNegative="true" LabelWidth="120px">
                                            </f:NumberBox>
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
                    <f:GroupPanel ID="GroupPanel10" Layout="Anchor" Title="3.施工生产情况" runat="server">
                        <Items>
                            <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="本月主要工作" runat="server">
                                <Items>
                                    <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:TextArea ID="txtThisMajorWork" runat="server" Height="100px" Readonly="true" >
                                                    </f:TextArea>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel3" Layout="Anchor" Title="下月主要工作" runat="server">
                                <Items>
                                    <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:TextArea ID="txtNextMajorWork" runat="server" Height="100px" Readonly="true" >
                                                    </f:TextArea>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel4" Layout="Anchor" Title="营业收入" runat="server">
                                <Items>
                                    <f:Form ID="Form6" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox runat="server" ID="txtThisIncome" Label="本月收入(万元)" NoDecimal="false" NoNegative="true"
                                                        Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtYearIncome" Label="本年累计收入(万元)" NoDecimal="false" NoNegative="true"
                                                       Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtTotalIncome" Label="自开工累计收入(万元)" NoDecimal="false"
                                                        NoNegative="true" LabelWidth="120px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel5" Layout="Anchor" Title="形象进度" runat="server">
                                <Items>
                                    <f:Form ID="Form7" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:TextBox runat="server" ID="txtThisImageProgress" Label="本月形象进度" Readonly="true">
                                                    </f:TextBox>
                                                    <f:TextBox runat="server" ID="txtYearImageProgress" Label="本年形象进度" Readonly="true">
                                                    </f:TextBox>
                                                    <f:TextBox runat="server" ID="txtTotalImageProgress" Label="自开工累计形象进度" LabelWidth="150px" Readonly="true">
                                                    </f:TextBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel6" Layout="Anchor" Title="项目人员情况" runat="server">
                                <Items>
                                    <f:Form ID="Form13" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox runat="server" ID="txtThisPersonNum" Label="本月现场员工总数" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="140px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtYearPersonNum" Label="本年累计员工总数" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="140px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtTotalPersonNum" Label="自开工累计员工总数" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="170px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox runat="server" ID="txtThisForeignPersonNum" Label="本月外籍员工人数" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="140px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtYearForeignPersonNum" Label="本年外籍员工人数" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="140px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtTotalForeignPersonNum" Label="自开工累计外籍员工人数" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="170px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel11" Layout="Anchor" Title="4.HSSE管理情况" runat="server">
                        <Items>
                            <f:GroupPanel ID="GroupPanel19" Layout="Anchor" Title="HSSE教育培训" runat="server">
                                <Items>
                                    <f:Form ID="Form10" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox runat="server" ID="txtThisTrainPersonNum" Label="本月HSSE教育培训（人/次）" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtYearTrainPersonNum" Label="本年累计HSSE教育培训（人/次）"
                                                        NoDecimal="true" NoNegative="true" LabelWidth="230px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtTotalTrainPersonNum" Label="自开工累计HSSE教育培训（人/次）"
                                                        NoDecimal="true" NoNegative="true" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel20" Layout="Anchor" Title="HSSE检查" runat="server">
                                <Items>
                                    <f:Form ID="Form14" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox runat="server" ID="txtThisCheckNum" Label="本月HSSE检查（次）" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtYearCheckNum" Label="本年累计HSSE检查（次）" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="230px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtTotalCheckNum" Label="自开工累计HSSE检查（次）" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel21" Layout="Anchor" Title="HSSE隐患排查治理" runat="server">
                                <Items>
                                    <f:Form ID="Form15" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox runat="server" ID="txtThisViolationNum" Label="本月HSSE隐患排查治理（项）" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtYearViolationNum" Label="本年累计HSSE隐患排查治理（项）" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="230px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtTotalViolationNum" Label="自开工累计HSSE隐患排查治理（项）"
                                                        NoDecimal="true" NoNegative="true" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel22" Layout="Anchor" Title="HSSE投入（元）" runat="server">
                                <Items>
                                    <f:Form ID="Form16" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox runat="server" ID="txtThisInvestment" Label="本月HSSE投入" NoDecimal="false"
                                                        NoNegative="true" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtYearInvestment" Label="本年累计HSSE投入" NoDecimal="false"
                                                        NoNegative="true" LabelWidth="230px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtTotalInvestment" Label="自开工累计HSSE投入" NoDecimal="false"
                                                        NoNegative="true" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel23" Layout="Anchor" Title="HSSE奖励（元）" runat="server">
                                <Items>
                                    <f:Form ID="Form17" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox runat="server" ID="txtThisReward" Label="本月HSSE奖励" NoDecimal="false"
                                                        NoNegative="true" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtYearReward" Label="本年累计HSSE奖励" NoDecimal="false"
                                                        NoNegative="true" LabelWidth="230px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtTotalReward" Label="自开工累计HSSE奖励" NoDecimal="false"
                                                        NoNegative="true" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel24" Layout="Anchor" Title="HSSE处罚（元）" runat="server">
                                <Items>
                                    <f:Form ID="Form18" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox runat="server" ID="txtThisPunish" Label="本月HSSE处罚" NoDecimal="false"
                                                        NoNegative="true" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtYearPunish" Label="本年累计HSSE处罚" NoDecimal="false"
                                                        NoNegative="true" LabelWidth="230px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtTotalPunish" Label="自开工累计HSSE处罚" NoDecimal="false"
                                                        NoNegative="true" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel25" Layout="Anchor" Title="HSSE应急演练" runat="server">
                                <Items>
                                    <f:Form ID="Form19" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox runat="server" ID="txtThisEmergencyDrillNum" Label="本月HSSE应急演练（次）" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtYearEmergencyDrillNum" Label="本年累计HSSE应急演练（次）"
                                                        NoDecimal="true" NoNegative="true" LabelWidth="230px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtTotalEmergencyDrillNum" Label="自开工累计HSSE应急演练（次）"
                                                        NoDecimal="true" NoNegative="true" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel26" Layout="Anchor" Title="5.HSE绩效情况" runat="server">
                        <Items>
                            <f:GroupPanel ID="GroupPanel27" Layout="Anchor" Title="HSE工时（工时）" runat="server">
                                <Items>
                                    <f:Form ID="Form20" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox runat="server" ID="txtThisHSEManhours" Label="本月HSE工时" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtYearHSEManhours" Label="本年累计HSE工时" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="230px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtTotalHSEManhours" Label="自开工累计HSE工时" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel7" Layout="Anchor" Title="HSE可记录事件" runat="server">
                                <Items>
                                    <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox runat="server" ID="txtThisRecordEvent" Label="本月可记录HSE事件" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtYearRecordEvent" Label="本年累计可记录HSE事件" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="230px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtTotalRecordEvent" Label="自开工累计可记录HSE事件" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel8" Layout="Anchor" Title="HSE不可记录事件" runat="server">
                                <Items>
                                    <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox runat="server" ID="txtThisNoRecordEvent" Label="本月不可记录HSE事件" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtYearNoRecordEvent" Label="本年累计不可记录HSE事件" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="230px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox runat="server" ID="txtTotalNoRecordEvent" Label="自开工累计不可记录HSE事件" NoDecimal="true"
                                                        NoNegative="true" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel12" Layout="Anchor" Title="6.项目HSSE主要人员" runat="server">
                        <Items>
                            <f:GroupPanel ID="GroupPanel13" Layout="Anchor" Title="项目经理" runat="server">
                                <Items>
                                    <f:Form ID="Form11" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:TextBox runat="server" ID="txtProjectManagerName" Label="姓名" Readonly="true">
                                                    </f:TextBox>
                                                    <f:TextBox runat="server" ID="txtProjectManagerPhone" Label="电话" Readonly="true">
                                                    </f:TextBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel14" Layout="Anchor" Title="HSE总监/经理" runat="server">
                                <Items>
                                    <f:Form ID="Form12" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:TextBox runat="server" ID="txtHSEManagerName" Label="姓名" Readonly="true">
                                                    </f:TextBox>
                                                    <f:TextBox runat="server" ID="txtHSEManagerPhone" Label="电话" Readonly="true">
                                                    </f:TextBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
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
