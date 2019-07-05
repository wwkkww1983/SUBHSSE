<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MillionsMonthlyReportImport.aspx.cs"
    Inherits="FineUIPro.Web.DataIn.MillionsMonthlyReportImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入百万工时安全统计月报表</title>
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
                        BoxFlex="1" DataKeyNames="MillionsMonthlyReportItemId" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="MillionsMonthlyReportItemId" AllowSorting="true"
                        SortField="Year" PageSize="12" Height="400px">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:TemplateField Width="200px" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# ConvertUnit(Eval("UnitId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="50px" ColumnID="Year" DataField="Year" FieldType="Int" HeaderText="年度"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="50px" ColumnID="Month" DataField="Month" FieldType="Int" HeaderText="月份"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="DutyPerson" DataField="DutyPerson" FieldType="String"
                                HeaderText="负责人" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="180px" ColumnID="RecordableIncidentRate" DataField="RecordableIncidentRate"
                                FieldType="Float" HeaderText="百万工时总可记录事故率" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="180px" ColumnID="LostTimeRate" DataField="LostTimeRate" FieldType="Float"
                                HeaderText="百万工时损失工时率" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="180px" ColumnID="LostTimeInjuryRate" DataField="LostTimeInjuryRate"
                                FieldType="Float" HeaderText="百万工时损失工时伤害事故率" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="180px" ColumnID="DeathAccidentFrequency" DataField="DeathAccidentFrequency"
                                FieldType="Float" HeaderText="百万工时死亡事故频率" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="180px" ColumnID="AccidentMortality" DataField="AccidentMortality"
                                FieldType="Float" HeaderText="百万工时事故死亡率" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="Affiliation" DataField="Affiliation" FieldType="String"
                                HeaderText="所属单位" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="Name" DataField="Name" FieldType="String"
                                HeaderText="名称" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="PostPersonNum" DataField="PostPersonNum" FieldType="Int"
                                HeaderText="在岗员工数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="SnapPersonNum" DataField="SnapPersonNum" FieldType="Int"
                                HeaderText="临时员工数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="ContractorNum" DataField="ContractorNum" FieldType="Int"
                                HeaderText="承包商数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="SumPersonNum" DataField="SumPersonNum" FieldType="Int"
                                HeaderText="员工总数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="TotalWorkNum" DataField="TotalWorkNum" FieldType="Float"
                                HeaderText="总工时数（万）" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="SeriousInjuriesNum" DataField="SeriousInjuriesNum"
                                FieldType="Int" HeaderText="重伤事故起数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="SeriousInjuriesPersonNum" DataField="SeriousInjuriesPersonNum"
                                FieldType="Int" HeaderText="重伤事故人数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="SeriousInjuriesLossHour" DataField="SeriousInjuriesLossHour"
                                FieldType="Int" HeaderText="重伤事故损失工时" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="MinorAccidentNum" DataField="MinorAccidentNum"
                                FieldType="Int" HeaderText="轻伤事故起数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="MinorAccidentPersonNum" DataField="MinorAccidentPersonNum"
                                FieldType="Int" HeaderText="轻伤事故人数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="MinorAccidentLossHour" DataField="MinorAccidentLossHour"
                                FieldType="Int" HeaderText="轻伤事故损失工时" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="OtherAccidentNum" DataField="OtherAccidentNum"
                                FieldType="Int" HeaderText="其它事故起数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="OtherAccidentPersonNum" DataField="OtherAccidentPersonNum"
                                FieldType="Int" HeaderText="其它事故人数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="OtherAccidentLossHour" DataField="OtherAccidentLossHour"
                                FieldType="Int" HeaderText="其它事故损失工时" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="RestrictedWorkPersonNum" DataField="RestrictedWorkPersonNum"
                                FieldType="Int" HeaderText="工作受限人数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="RestrictedWorkLossHour" DataField="RestrictedWorkLossHour"
                                FieldType="Int" HeaderText="工作受限损失工时" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="MedicalTreatmentPersonNum" DataField="MedicalTreatmentPersonNum"
                                FieldType="Int" HeaderText="医疗处置人数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="MedicalTreatmentLossHour" DataField="MedicalTreatmentLossHour"
                                FieldType="Int" HeaderText="医疗处置损失工时" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="FireNum" DataField="FireNum" FieldType="Int"
                                HeaderText="火灾起数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="ExplosionNum" DataField="ExplosionNum" FieldType="Int"
                                HeaderText="爆炸起数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="TrafficNum" DataField="TrafficNum" FieldType="Int"
                                HeaderText="交通起数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="140px" ColumnID="EquipmentNum" DataField="EquipmentNum" FieldType="Int"
                                HeaderText="机械设备起数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="QualityNum" DataField="QualityNum" FieldType="Int"
                                HeaderText="质量起数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="OtherNum" DataField="OtherNum" FieldType="Int"
                                HeaderText="其它起数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="FirstAidDressingsNum" DataField="FirstAidDressingsNum"
                                FieldType="Int" HeaderText="急救包扎起数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="AttemptedEventNum" DataField="AttemptedEventNum"
                                FieldType="Int" HeaderText="未遂事件起数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="LossDayNum" DataField="LossDayNum" FieldType="Int"
                                HeaderText="损失工日" HeaderTextAlign="Center" TextAlign="Center">
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
                    <f:Label ID="lblBottom" runat="server" Text="说明：单位、年份、月份为必填项！">
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
   <%-- <f:Form ID="Form2" runat="server">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnOut" Icon="Pencil" runat="server" Text="导出" ValidateForms="SimpleForm1"
                        OnClick="btnOut_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Rows>
            <f:FormRow>
                <Items>
                    <f:Grid ID="gvErrorInfo" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" AllowCellEditing="true" EnableColumnLines="true">
                        <Columns>
                            <f:TemplateField Width="50px" HeaderText="序号">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="200px" ColumnID="Row" DataField="Row" FieldType="String" HeaderText="错误行号"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="Column" DataField="Column" FieldType="String"
                                HeaderText="错误列" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="400px" ColumnID="Reason" DataField="Reason" FieldType="String"
                                HeaderText="错误类型" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>--%>
    <f:Window ID="Window1" Title="审核百万工时安全统计月报表" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="导入百万工时安全统计月报表" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
  <%--  <f:Window ID="Window3" Title="保存百万工时安全统计月报表" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window3_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>--%>
    </form>
</body>
</html>
