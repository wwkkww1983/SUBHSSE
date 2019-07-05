<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyQuarterlyReportImport.aspx.cs"
    Inherits="FineUIPro.Web.DataIn.SafetyQuarterlyReportImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAudit" Icon="ApplicationEdit" runat="server" Text="审核" ValidateForms="SimpleForm1"
                        OnClick="btnAudit_Click">
                    </f:Button>
                    <f:Button ID="btnImport" Icon="ApplicationGet" runat="server" Text="导入" ValidateForms="SimpleForm1"
                        OnClick="btnImport_Click">
                    </f:Button>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnDownLoad" runat="server" Icon="ApplicationGo" Text="下载模板" OnClick="btnDownLoad_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Rows>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="fuAttachUrl" EmptyText="选择要导入的文件" Label="选择要导入的文件"
                        LabelWidth="150px">
                    </f:FileUpload>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="SafetyQuarterlyReportId" AllowCellEditing="true" ClicksToEdit="2"
                        DataIDField="SafetyQuarterlyReportId" AllowSorting="true" SortField="YearId"
                        PageSize="12" Height="400px">
                        <Columns>
                           <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:TemplateField Width="200px" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# ConvertUnit(Eval("UnitId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="50px" ColumnID="YearId" DataField="YearId" FieldType="Int"
                                HeaderText="年份" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="50px" ColumnID="Quarters" DataField="Quarters" FieldType="Int"
                                HeaderText="季度" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="TotalInWorkHours" DataField="TotalInWorkHours"
                                FieldType="Int" HeaderText="总投入工时数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="TotalInWorkHoursRemark" DataField="TotalInWorkHoursRemark"
                                FieldType="String" HeaderText="总投入工时数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="TotalOutWorkHours" DataField="TotalOutWorkHours"
                                FieldType="Int" HeaderText="总损失工时数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="TotalOutWorkHoursRemark" DataField="TotalOutWorkHoursRemark"
                                FieldType="String" HeaderText="总损失工时数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="WorkHoursLossRate" DataField="WorkHoursLossRate"
                                FieldType="Float" HeaderText="百万工时损失率" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="WorkHoursLossRateRemark" DataField="WorkHoursLossRateRemark"
                                FieldType="String" HeaderText="百万工时损失率备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="WorkHoursAccuracy" DataField="WorkHoursAccuracy"
                                FieldType="Float" HeaderText="工时统计准确率" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="WorkHoursAccuracyRemark" DataField="WorkHoursAccuracyRemark"
                                FieldType="String" HeaderText="工时统计准确率备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="MainBusinessIncome" DataField="MainBusinessIncome"
                                FieldType="Float" HeaderText="主营业务收入/亿元" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="MainBusinessIncomeRemark" DataField="MainBusinessIncomeRemark"
                                FieldType="String" HeaderText="主营业务收入/亿元备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="ConstructionRevenue" DataField="ConstructionRevenue"
                                FieldType="Float" HeaderText="施工收入/亿元" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="ConstructionRevenueRemark" DataField="ConstructionRevenueRemark"
                                FieldType="String" HeaderText="施工收入/亿元备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="UnitTimeIncome" DataField="UnitTimeIncome"
                                FieldType="Float" HeaderText="单位工时收入/元" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="UnitTimeIncomeRemark" DataField="UnitTimeIncomeRemark"
                                FieldType="String" HeaderText="单位工时收入/元备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="BillionsOutputMortality" DataField="BillionsOutputMortality"
                                FieldType="Float" HeaderText="百亿产值死亡率" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="BillionsOutputMortalityRemark" DataField="BillionsOutputMortalityRemark"
                                FieldType="String" HeaderText="百亿产值死亡率备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="MajorFireAccident" DataField="MajorFireAccident"
                                FieldType="Int" HeaderText="重大火灾事故报告数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="MajorFireAccidentRemark" DataField="MajorFireAccidentRemark"
                                FieldType="String" HeaderText="重大火灾事故报告数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="MajorEquipAccident" DataField="MajorEquipAccident"
                                FieldType="Int" HeaderText="重大机械设备事故报告数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="MajorEquipAccidentRemark" DataField="MajorEquipAccidentRemark"
                                FieldType="String" HeaderText="重大机械设备事故报告数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="AccidentFrequency" DataField="AccidentFrequency"
                                FieldType="Float" HeaderText="事故发生频率（占总收入之比）" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="AccidentFrequencyRemark" DataField="AccidentFrequencyRemark"
                                FieldType="String" HeaderText="事故发生频率（占总收入之比）备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="SeriousInjuryAccident" DataField="SeriousInjuryAccident"
                                FieldType="Int" HeaderText="重伤以上事故报告数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="SeriousInjuryAccidentRemark" DataField="SeriousInjuryAccidentRemark"
                                FieldType="String" HeaderText="重伤以上事故报告数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="FireAccident" DataField="FireAccident" FieldType="Int"
                                HeaderText="火灾事故统计报告数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="FireAccidentRemark" DataField="FireAccidentRemark"
                                FieldType="String" HeaderText="火灾事故统计报告数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="EquipmentAccident" DataField="EquipmentAccident"
                                FieldType="Int" HeaderText="装备事故统计报告数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="EquipmentAccidentRemark" DataField="EquipmentAccidentRemark"
                                FieldType="String" HeaderText="装备事故统计报告数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="PoisoningAndInjuries" DataField="PoisoningAndInjuries"
                                FieldType="Int" HeaderText="中毒及职业伤害报告数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="PoisoningAndInjuriesRemark" DataField="PoisoningAndInjuriesRemark"
                                FieldType="String" HeaderText="中毒及职业伤害报告数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="ProductionSafetyInTotal" DataField="ProductionSafetyInTotal"
                                FieldType="Int" HeaderText="安全生产投入总额/元" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="ProductionSafetyInTotalRemark" DataField="ProductionSafetyInTotalRemark"
                                FieldType="String" HeaderText="安全生产投入总额/元备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="ProtectionInput" DataField="ProtectionInput"
                                FieldType="Float" HeaderText="安全防护投入/元" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="ProtectionInputRemark" DataField="ProtectionInputRemark"
                                FieldType="String" HeaderText="安全防护投入备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="LaboAndHealthIn" DataField="LaboAndHealthIn"
                                FieldType="Float" HeaderText="劳动保护及职业健康投入/元" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="LaborAndHealthInRemark" DataField="LaborAndHealthInRemark"
                                FieldType="String" HeaderText="劳动保护及职业健康投入备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="TechnologyProgressIn" DataField="TechnologyProgressIn"
                                FieldType="Float" HeaderText="安全技术进步投入/元" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="TechnologyProgressInRemark" DataField="TechnologyProgressInRemark"
                                FieldType="String" HeaderText="安全技术进步投入备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="EducationTrainIn" DataField="EducationTrainIn"
                                FieldType="Float" HeaderText="安全教育培训投入/元" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="EducationTrainInRemark" DataField="EducationTrainInRemark"
                                FieldType="String" HeaderText="安全教育培训投入备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="ProjectCostRate" DataField="ProjectCostRate"
                                FieldType="Float" HeaderText="工程造价占比(%)" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="ProjectCostRateRemark" DataField="ProjectCostRateRemark"
                                FieldType="String" HeaderText="工程造价占比(%)备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="ProductionInput" DataField="ProductionInput"
                                FieldType="Float" HeaderText="百万工时安全生产投入额/万元" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="ProductionInputRemark" DataField="ProductionInputRemark"
                                FieldType="String" HeaderText="百万工时安全生产投入额备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="Revenue" DataField="Revenue" FieldType="Float"
                                HeaderText="安全生产投入占施工收入之比" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="RevenueRemark" DataField="RevenueRemark" FieldType="String"
                                HeaderText="安全生产投入占施工收入之比备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="FullTimeMan" DataField="FullTimeMan" FieldType="Int"
                                HeaderText="安全专职人员总数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="FullTimeManRemark" DataField="FullTimeManRemark"
                                FieldType="String" HeaderText="安全专职人员总数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="PMMan" DataField="PMMan" FieldType="Int" HeaderText="项目经理人员总数"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="PMManRemark" DataField="PMManRemark" FieldType="String"
                                HeaderText="项目经理人员总数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="CorporateDirectorEdu" DataField="CorporateDirectorEdu"
                                FieldType="Int" HeaderText="企业负责人安全生产继续教育数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="CorporateDirectorEduRemark" DataField="CorporateDirectorEduRemark"
                                FieldType="String" HeaderText="企业负责人安全生产继续教育数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="ProjectLeaderEdu" DataField="ProjectLeaderEdu"
                                FieldType="Int" HeaderText="项目负责人安全生产继续教育数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="ProjectLeaderEduRemark" DataField="ProjectLeaderEduRemark"
                                FieldType="String" HeaderText="项目负责人安全生产继续教育数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="FullTimeEdu" DataField="FullTimeEdu" FieldType="Int"
                                HeaderText="安全专职人员安全生产继续教育数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="FullTimeEduRemark" DataField="FullTimeEduRemark"
                                FieldType="String" HeaderText="安全专职人员安全生产继续教育数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="ThreeKidsEduRate" DataField="ThreeKidsEduRate"
                                FieldType="Float" HeaderText="安全生产三类人员继续教育覆盖率" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="ThreeKidsEduRateRemark" DataField="ThreeKidsEduRateRemark"
                                FieldType="String" HeaderText="安全生产三类人员继续教育覆盖率备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="UplinReportRate" DataField="UplinReportRate"
                                FieldType="Float" HeaderText="上行报告履行率" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="UplinReportRateRemark" DataField="UplinReportRateRemark"
                                FieldType="String" HeaderText="上行报告履行率备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="KeyEquipmentTotal" DataField="KeyEquipmentTotal"
                                FieldType="Int" HeaderText="重点装备总数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="KeyEquipmentTotalRemark" DataField="KeyEquipmentTotalRemark"
                                FieldType="String" HeaderText="重点装备总数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="KeyEquipmentReportCount" DataField="KeyEquipmentReportCount"
                                FieldType="Int" HeaderText="重点装备安全控制检查报告数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="KeyEquipmentReportCountRemark" DataField="KeyEquipmentReportCountRemark"
                                FieldType="String" HeaderText="重点装备安全控制检查报告数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="ChemicalAreaProjectCount" DataField="ChemicalAreaProjectCount"
                                FieldType="Int" HeaderText="化工界区施工作业项目数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="ChemicalAreaProjectCountRemark" DataField="ChemicalAreaProjectCountRemark"
                                FieldType="String" HeaderText="化工界区施工作业项目数备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="HarmfulMediumCoverCount" DataField="HarmfulMediumCoverCount"
                                FieldType="Int" HeaderText="化工界区施工作业有害介质检测复测覆盖数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="HarmfulMediumCoverCountRemark" DataField="HarmfulMediumCoverCountRemark"
                                FieldType="String" HeaderText="化工界区施工作业有害介质检测复测覆盖数备注" HeaderTextAlign="Center"
                                TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="HarmfulMediumCoverRate" DataField="HarmfulMediumCoverRate"
                                FieldType="Float" HeaderText="施工作业安全技术交底覆盖率" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="HarmfulMediumCoverRateRemark" DataField="HarmfulMediumCoverRateRemark"
                                FieldType="String" HeaderText="施工作业安全技术交底覆盖率备注" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HiddenField ID="hdFileName" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdCheckResult" runat="server">
                    </f:HiddenField>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label ID="lblBottom" runat="server" Text="说明：单位、年份、季度为必填项！">
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>  
    <f:Window ID="Window1" Title="审核安全生产数据季报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="导入安全生产数据季报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    </form>
</body>
</html>
