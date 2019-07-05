<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportDView.aspx.cs" Inherits="FineUIPro.Web.Manager.MonthReportDView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server">
    </f:PageManager>
    <f:Panel ID="RegionPanel1" Layout="Region" ShowBorder="false" ShowHeader="false"
        runat="server">
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                <Items>
                    <f:TextBox ID="txtMonthReportCode" runat="server" Label="编号" Readonly="true" MaxLength="50"
                        LabelAlign="right" Width="300px" LabelWidth="100px">
                    </f:TextBox>
                    <f:Label ID="txtReportMonths" runat="server" Label="月报月份" Width="300px" LabelAlign="right"
                        LabelWidth="140px">
                    </f:Label>
                    <f:Label ID="txtMonthReportDate" runat="server" Label="报告日期" Width="300px" LabelAlign="right"
                        LabelWidth="140px">
                    </f:Label>
                    <f:TextBox ID="txtReportMan" runat="server" Label="报告人" Readonly="true" LabelAlign="right"
                        Width="300px" LabelWidth="100px">
                    </f:TextBox>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                <Rows>
                    <f:FormRow>
                        <Items>
                            <f:GroupPanel ID="GroupPanel21" Layout="Anchor" Title="安全工时" runat="server" AutoScroll="true">
                                <Items>
                                    <f:Form ID="Form22" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox ID="txtThisUnitPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="本公司现场人数(人月)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtThisUnitHSEPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="本公司现场HSE管理人数(人月)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtSubUnitPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="分包商现场人数(人月)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox ID="txtSubUnitHSEPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="分包商HSE管理人数(人月)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtManHours" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                        Label="人工时数(人工时)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtHSEManHours" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="安全生产人工时数(人工时)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="33% 33% 33%">
                                                <Items>
                                                    <f:NumberBox ID="txtLossHours" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                        Label="损失工时数(人工时)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtLossDay" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                        Label="损失工作日(日)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="NumberBox3" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                        Label="" LabelWidth="240px" Hidden="true" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                        </Items>
                    </f:FormRow>
                </Rows>
                <Rows>
                    <f:FormRow>
                        <Items>
                            <f:GroupPanel ID="GroupPanel2" Layout="Anchor" Title="人身伤害事故" runat="server" AutoScroll="true">
                                <Items>
                                    <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox ID="txtDeathNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                        Label="死亡起数" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtDeathPersonNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                        Label="死亡人数" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtSeriousInjuredNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="重伤起数" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtSeriousInjuriesPersonNum" NoDecimal="true" NoNegative="true"
                                                        MinValue="0" runat="server" Label="重伤人数" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                <f:NumberBox ID="txtSeriousInjuriesLossHour" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="重伤损失工时" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtMinorInjuredNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="轻伤起数" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtMinorAccidentPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="轻伤人数" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtMinorAccidentLossHour" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="轻伤损失工时" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox ID="txtOtherNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                        Label="其它事故起数" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtOtherAccidentPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="其它事故人数" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtOtherAccidentLossHour" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="其它事故损失工时" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtWorkLimitNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="工作受限工伤人数" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox ID="txtRestrictedWorkLossHour" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="工作受限损失工时" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtMedicalTreatmentNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="医疗处置人数" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtMedicalTreatmentLossHour" NoDecimal="true" NoNegative="true"
                                                        MinValue="0" runat="server" Label="医疗处置损失工时" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtFirstAidNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="急救包扎事故起数" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="25% 25% 25% 25%">
                                                <Items>
                                                    <f:NumberBox ID="txtOccupationalDiseasesNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="职业病起数" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtAttemptedAccidentNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="未遂事故起数" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtPersonInjuredLossMoney" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Label="人身伤害事故经济损失(万元)" LabelWidth="200px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="NumberBox2" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                        Label="" LabelWidth="240px" Hidden="true" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                        </Items>
                    </f:FormRow>
                </Rows>
                <Rows>
                    <f:FormRow>
                        <Items>
                            <f:GroupPanel ID="GroupPanel3" Layout="Anchor" Title="财产损失事故" runat="server" AutoScroll="true">
                                <Items>
                                    <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox ID="txtFireNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                        Label="火灾事故(起数)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtExplosionNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="爆炸事故(起数)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtTrafficNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                        Label="交通事故(起数)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox ID="txtEquipmentNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="机械设备事故(起数)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtSiteEnvironmentNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="现场环境事故(起数)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtTheftCaseNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="现场发生设备材料盗窃案件(起数)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="33% 33% 33%">
                                                <Items>
                                                    <f:NumberBox ID="txtPropertyLossMoney" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Label="财产损失(万元)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="NumberBox9" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                        Label="" LabelWidth="240px" Hidden="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="NumberBox10" NoDecimal="false" NoNegative="true" MinValue="0" runat="server"
                                                        Label="" LabelWidth="240px" Hidden="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                        </Items>
                    </f:FormRow>
                </Rows>
                <Rows>
                    <f:FormRow>
                        <Items>
                            <f:GroupPanel ID="GroupPanel4" Layout="Anchor" Title="安全生产费用" runat="server" AutoScroll="true">
                                <Items>
                                    <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox ID="txtMainBusinessIncome" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Label="主营业务收入(亿元)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtConstructionIncome" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Label="施工收入(亿元)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtProjectVolume" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Label="建安工程量(费)(万元)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox ID="txtPaidForMoney" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Label="已支付分承包商安全生产费用(万元)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtApprovedChargesMoney" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Label="已审核分承包商安全生产费用(万元)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtHasBeenChargedMoney" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Label="项目部已投入的安全生产费用(万元)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                        </Items>
                    </f:FormRow>
                </Rows>
                <Rows>
                    <f:FormRow>
                        <Items>
                            <f:GroupPanel ID="GroupPanel5" Layout="Anchor" Title="教育培训" runat="server" AutoScroll="true">
                                <Items>
                                    <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow ColumnWidths="33% 33% 33%">
                                                <Items>
                                                    <%--<f:NumberBox ID="txtWeekMeetingNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="HSE每周会议(次)" LabelWidth="240px">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtCommitteeMeetingNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="HSE委员会会议(次)" LabelWidth="240px">
                                                    </f:NumberBox>--%>
                                                    <f:NumberBox ID="txtTrainPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="人员培训(人次)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="NumberBox1" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                        Label="" LabelWidth="240px" Hidden="true" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="NumberBox5" NoDecimal="false" NoNegative="true" MinValue="0" runat="server"
                                                        Label="" LabelWidth="240px" Hidden="true" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                        </Items>
                    </f:FormRow>
                </Rows>
                <Rows>
                    <f:FormRow>
                        <Items>
                            <f:GroupPanel ID="GroupPanel6" Layout="Anchor" Title="安全检查" runat="server" AutoScroll="true">
                                <Items>
                                    <f:Form ID="Form6" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <%--<f:FormRow>
                                                <Items>
                                                    <f:NumberBox ID="txtWeekCheckNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="项目领导周安全检查次数(次)" LabelWidth="240px">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtHSECheckNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="施工现场HSE联合检查(次)" LabelWidth="240px">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtSpecialCheckNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="专项安全检查次数(次)" LabelWidth="240px">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox ID="txtEquipmentHSEInspectionNum" NoDecimal="true" NoNegative="true"
                                                        MinValue="0" runat="server" Label="设备HSE检验(台/辆次)" LabelWidth="240px">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtLicenseNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                        Label="安全作业许可证审批数量(份)" LabelWidth="240px">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtSolutionNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="审批专项施工方案数量(项)" LabelWidth="240px">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>--%>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox ID="txtReleaseRectifyNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="发布HSE整改通知书(份)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtCloseRectifyNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="关闭HSE整改通知书(份)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtReleasePunishNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="发布HSE处罚书(份)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="33% 33% 33%">
                                                <Items>
                                                    <f:NumberBox ID="txtPunishMoney" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Label="罚款金额(元)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtIncentiveMoney" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Label="奖励金额(元)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="NumberBox4" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                        Label="" LabelWidth="240px" Hidden="true" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                        </Items>
                    </f:FormRow>
                </Rows>
                <Rows>
                    <f:FormRow>
                        <Items>
                            <f:GroupPanel ID="GroupPanel7" Layout="Anchor" Title="应急管理" runat="server" AutoScroll="true">
                                <Items>
                                    <f:Form ID="Form7" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:NumberBox ID="txtEmergencyDrillNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="应急演练次数(次)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtParticipantsNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        runat="server" Label="参演人次数(人次)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:NumberBox ID="txtDrillInput" NoDecimal="false" NoNegative="true" MinValue="0"
                                                        runat="server" Label="演练直接投入(万元)" LabelWidth="240px" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:TextArea ID="txtDrillTypes" runat="server" MaxLength="500" Label="演练类型与方式" LabelWidth="120px" Readonly="true">
                                                    </f:TextArea>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
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
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Form>
        </Items>
    </f:Panel>
    </form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
</body>
</html>
<script type="text/javascript">
    function updateSummary() {
        // 回发到后台更新
        __doPostBack('', 'UPDATE_SUMMARY');
    }
</script>