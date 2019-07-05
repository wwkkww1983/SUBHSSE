<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Synchronization.aspx.cs"
    Inherits="FineUIPro.Web.SysManage.Synchronization" Async="true" AsyncTimeout="36000" %>

<%@ Register Src="~/Controls/ChartControl.ascx" TagName="ChartControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据同步</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" AjaxAspnetControls="divCostUnit,divCostTime" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region1" ShowBorder="false" ShowHeader="false" RegionPosition="Top"
                BodyPadding="0 5 0 0" Width="200px" Layout="Fit" runat="server" EnableCollapse="true">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:Button ID="btnAll" Text="全选" Icon="BulletTick" EnablePostBack="true" runat="server"
                                OnClick="btnAll_Click">
                            </f:Button>
                            <f:Button ID="btnUnAll" Text="全不选" Icon="CheckError" EnablePostBack="true" runat="server"
                                OnClick="btnUnAll_Click">
                            </f:Button>
                            <f:Button ID="btnSyn" Text="同步" Icon="ArrowRefresh" EnablePostBack="true" runat="server"
                                OnClick="BtnSyn_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Region>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                <Items>
                    <f:TabStrip ID="TabStrip" CssClass="f-tabstrip-theme-simple" Height="500px" ShowBorder="true"
                        TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server">
                        <Tabs>
                            <f:Tab ID="formTab" Title="数据提取" BodyPadding="5px" Layout="Fit" IconFont="Bookmark"
                                runat="server" TitleToolTip="数据从集团公司提取" Margin="0 5 0 5">
                                <Items>
                                    <f:Form runat="server" Layout="VBox">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:CheckBox ID="cbFromVersion" runat="server" Label="版本信息提取" LabelWidth="150px" LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromUnit" runat="server" Label="单位信息提取" LabelWidth="150px" LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromUrgeReport" runat="server" Label="催报信息提取" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:Label ID="Label1" runat="server">
                                                    </f:Label>
                                                    <f:Label ID="Label4" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:CheckBox ID="cbFromLawRegulation" runat="server" Label="法律法规提取" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromHSSEStandard" runat="server" Label="标准规范提取" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromRulesRegulations" runat="server" Label="政府部门安全规章" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromManageRule" runat="server" Label="安全管理规定" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:Label ID="Label2" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:CheckBox ID="cbFromTraining" runat="server" Label="培训教材库类别" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromTrainingItem" runat="server" Label="培训教材库明细" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromTrainTestDB" runat="server" Label="安全试题库类别" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromTrainTestDBItem" runat="server" Label="安全试题库明细" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:Label ID="Label5" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:CheckBox ID="cbFromAccidentCase" runat="server" Label="事故案例库类别" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromAccidentCaseItem" runat="server" Label="事故案例库明细" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromKnowledge" runat="server" Label="应知应会库类别" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromKnowledgeItem" runat="server" Label="应知应会库明细" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:Label ID="Label6" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:CheckBox ID="cbFromHazardListType" runat="server" Label="危险源清单类别" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromHazardList" runat="server" Label="危险源清单明细" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromRectify" runat="server" Label="安全隐患类别" LabelWidth="150px" LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromRectifyItem" runat="server" Label="安全隐患明细" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:Label ID="Label3" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:CheckBox ID="cbFromHAZOP" runat="server" Label="HAZOP管理" LabelWidth="150px" LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromAppraise" runat="server" Label="安全评价" LabelWidth="150px" LabelAlign="Right">
                                                    </f:CheckBox>
                                                   <%-- <f:CheckBox ID="cbFromExpert" runat="server" Label="安全专家信息提取" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>--%>
                                                    <f:CheckBox ID="cbFromEmergency" runat="server" Label="应急预案" LabelWidth="150px" LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromSpecialScheme" runat="server" Label="专项方案" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>  
                                                    <f:Label ID="Label13" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>                                                                                                     
                                                    <f:CheckBox ID="cbFromSubUnitReport" runat="server" Label="企业安全文件上报" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbFromSubUnitReportItem" runat="server" Label="企业安全文件上报明细" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                     <f:CheckBox ID="cbFromCheckRectify" runat="server" Label="集团检查整改" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                      <f:CheckBox ID="cbFromCheckInfo_Table8" runat="server" Label="集团检查报告" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:Label ID="Label12" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>                                           
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="toTab" Title="数据上报" BodyPadding="5px" Layout="Fit" IconFont="Bookmark"
                                runat="server" TitleToolTip="数据上报到集团公司">
                                <Items>
                                    <f:Form ID="Form2" runat="server" Layout="VBox">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:CheckBox ID="cbToLawRegulation" runat="server" Label="法律法规上报" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToHSSEStandard" runat="server" Label="标准规范上报" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToRulesRegulations" runat="server" Label="政府部门安全规章" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToManageRule" runat="server" Label="安全管理规定" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:Label ID="Label7" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:CheckBox ID="cbToTrainingItem" runat="server" Label="培训教材库明细" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToTrainTestDBItem" runat="server" Label="安全试题库明细" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToAccidentCaseItem" runat="server" Label="事故案例库明细" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToKnowledgeItem" runat="server" Label="应知应会库明细" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:Label ID="Label14" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:CheckBox ID="cbToHazardList" runat="server" Label="危险源清单明细" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToRectifyItem" runat="server" Label="安全隐患明细" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToHAZOP" runat="server" Label="HAZOP管理" LabelWidth="150px" LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToAppraise" runat="server" Label="安全评价" LabelWidth="150px" LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:Label ID="Label15" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                   <%-- <f:CheckBox ID="cbToExpert" runat="server" Label="安全专家" LabelWidth="150px" LabelAlign="Right">
                                                    </f:CheckBox>--%>
                                                    <f:CheckBox ID="cbToEmergency" runat="server" Label="应急预案" LabelWidth="150px" LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToSpecialScheme" runat="server" Label="专项方案" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToCheckRectify" runat="server" Label="集团检查整改" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToSubUnitReportItem" runat="server" Label="企业安全文件上报" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToHSSEManage" runat="server" Label="安全管理机构" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:CheckBox ID="cbToMillionsMonthlyReport" runat="server" Label="百万工时安全统计月报" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToAccidentCauseReport" runat="server" Label="职工伤亡事故原因分析" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToSafetyQuarterlyReport" runat="server" Label="安全生产数据季报" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToDrillConductedQuarterlyReport" runat="server" Label="应急演练开展情况季报"
                                                        LabelWidth="150px" LabelAlign="Right">
                                                    </f:CheckBox>
                                                    <f:CheckBox ID="cbToDrillPlanHalfYearReport" runat="server" Label="应急演练计划半年报" LabelWidth="150px"
                                                        LabelAlign="Right">
                                                    </f:CheckBox>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:Tab>
                        </Tabs>
                    </f:TabStrip>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    </form>
</body>
</html>
