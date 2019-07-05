<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MillionsMonthlyReportSave.aspx.cs"
    Async="true" Inherits="FineUIPro.Web.Information.MillionsMonthlyReportSave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="false"
        Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
        LabelAlign="Right">              
        <Rows>
            <f:FormRow ColumnWidths="29% 20% 25% 20%">
                <Items>
                    <f:DropDownList ID="drpYear" AutoPostBack="false" EnableSimulateTree="true" Required="true"
                        ShowRedStar="true" runat="server" Label="年度">
                    </f:DropDownList>
                    <f:DropDownList ID="drpMonth" AutoPostBack="false" EnableSimulateTree="true" Required="true"
                        ShowRedStar="true" runat="server" Label="月份">
                    </f:DropDownList>
                    <f:DropDownList ID="drpUnit" AutoPostBack="true" EnableSimulateTree="true" runat="server"
                        Label="填报企业" FocusOnPageLoad="true" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:DatePicker runat="server" Label="填报日期" ID="txtFillingDate">
                    </f:DatePicker>
                    <f:TextBox runat="server" Label="负责人" MaxLength="50" ID="txtDutyPerson">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Label="百万工时总可记录事故率" ID="txtRecordableIncidentRate" LabelWidth="170px">
                    </f:TextBox>
                    <f:TextBox runat="server" Label="百万工时损失工时率" ID="txtLostTimeRate" LabelWidth="150px">
                    </f:TextBox>
                    <f:TextBox runat="server" Label="百万工时损失工时伤害事故率" ID="txtLostTimeInjuryRate" LabelWidth="190px">
                    </f:TextBox>
                    <f:TextBox runat="server" Label="百万工时死亡事故频率" ID="txtDeathAccidentFrequency" LabelWidth="160px">
                    </f:TextBox>
                    <f:TextBox runat="server" Label="百万工时事故死亡率" ID="txtAccidentMortality" LabelWidth="150px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="MillionsMonthlyReportItemId" AllowCellEditing="true"
                        ClicksToEdit="1" DataIDField="MillionsMonthlyReportItemId" EnableColumnLines="true"
                        OnRowCommand="Grid1_RowCommand" EnableHeaderMenu="false" Width="1400px" Height="450px">
                        <Columns>
                            <f:LinkButtonField Width="40px" ConfirmTarget="Top" CommandName="Add" Icon="Add"
                                TextAlign="Center" />
                            <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                Icon="Delete" TextAlign="Center" />
                           <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:RenderField Width="100px" ColumnID="Affiliation" DataField="Affiliation" FieldType="String"
                                HeaderText="所属单位" HeaderTextAlign="Center">
                                <Editor>
                                    <f:TextBox runat="server" ID="txtAffiliation" Text='<%# Eval("Affiliation")%>'>
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="Name" DataField="Name" FieldType="String"
                                HeaderText="名称" HeaderTextAlign="Center">
                                <Editor>
                                    <f:TextBox runat="server" ID="txtName" Text='<%# Eval("Name")%>'>
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                            <f:GroupField EnableLock="true" HeaderText="员工总数" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="90px" ColumnID="PostPersonNum" DataField="PostPersonNum" FieldType="String"
                                        HeaderText="在岗员工" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtPostPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("PostPersonNum")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="90px" ColumnID="SnapPersonNum" DataField="SnapPersonNum" FieldType="String"
                                        HeaderText="临时员工" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtSnapPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("SnapPersonNum")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="70px" ColumnID="ContractorNum" DataField="ContractorNum" FieldType="String"
                                        HeaderText="承包商" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtContractorNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("ContractorNum")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:RenderField Width="120px" ColumnID="TotalWorkNum" DataField="TotalWorkNum" FieldType="String"
                                HeaderText="总工时数（万）" HeaderTextAlign="Center">
                                <Editor>
                                   <%-- <f:TextBox runat="server" ID="txtTotalWorkNum" Text='<%# Eval("TotalWorkNum")%>'>
                                    </f:TextBox>--%>
                                    <f:NumberBox ID="txtTotalWorkNum" NoDecimal="false" NoNegative="true" MinValue="0.00" 
                                                Text='<%# Eval("TotalWorkNum")%>' runat="server" DecimalPrecision="4">
                                            </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:GroupField EnableLock="true" HeaderText="总可记录事件" TextAlign="Center">
                                <Columns>
                                    <f:GroupField EnableLock="true" HeaderText="重伤事故" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="50px" ColumnID="SeriousInjuriesNum" DataField="SeriousInjuriesNum"
                                                FieldType="String" HeaderText="起数" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtSeriousInjuriesNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        Text='<%# Eval("SeriousInjuriesNum")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="50px" ColumnID="SeriousInjuriesPersonNum" DataField="SeriousInjuriesPersonNum"
                                                FieldType="String" HeaderText="人数" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtSeriousInjuriesPersonNum" NoDecimal="true" NoNegative="true"
                                                        MinValue="0" Text='<%# Eval("SeriousInjuriesPersonNum")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="75px" ColumnID="SeriousInjuriesLossHour" DataField="SeriousInjuriesLossHour"
                                                FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtSeriousInjuriesLossHour" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        Text='<%# Eval("SeriousInjuriesLossHour")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="轻伤事故" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="50px" ColumnID="MinorAccidentNum" DataField="MinorAccidentNum"
                                                FieldType="String" HeaderText="起数" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtMinorAccidentNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        Text='<%# Eval("MinorAccidentNum")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="50px" ColumnID="MinorAccidentPersonNum" DataField="MinorAccidentPersonNum"
                                                FieldType="String" HeaderText="人数" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtMinorAccidentPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        Text='<%# Eval("MinorAccidentPersonNum")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="75px" ColumnID="MinorAccidentLossHour" DataField="MinorAccidentLossHour"
                                                FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtMinorAccidentLossHour" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        Text='<%# Eval("MinorAccidentLossHour")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="其它事故" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="50px" ColumnID="OtherAccidentNum" DataField="OtherAccidentNum"
                                                FieldType="String" HeaderText="起数" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtOtherAccidentNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        Text='<%# Eval("OtherAccidentNum")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="50px" ColumnID="OtherAccidentPersonNum" DataField="OtherAccidentPersonNum"
                                                FieldType="String" HeaderText="人数" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtOtherAccidentPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        Text='<%# Eval("OtherAccidentPersonNum")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="75px" ColumnID="OtherAccidentLossHour" DataField="OtherAccidentLossHour"
                                                FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtOtherAccidentLossHour" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        Text='<%# Eval("OtherAccidentLossHour")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="工作受限" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="50px" ColumnID="RestrictedWorkPersonNum" DataField="RestrictedWorkPersonNum"
                                                FieldType="String" HeaderText="人数" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtRestrictedWorkPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        Text='<%# Eval("RestrictedWorkPersonNum")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="75px" ColumnID="RestrictedWorkLossHour" DataField="RestrictedWorkLossHour"
                                                FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtRestrictedWorkLossHour" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        Text='<%# Eval("RestrictedWorkLossHour")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="医疗处置" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="50px" ColumnID="MedicalTreatmentPersonNum" DataField="MedicalTreatmentPersonNum"
                                                FieldType="String" HeaderText="人数" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtMedicalTreatmentPersonNum" NoDecimal="true" NoNegative="true"
                                                        MinValue="0" Text='<%# Eval("MedicalTreatmentPersonNum")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="75px" ColumnID="MedicalTreatmentLossHour" DataField="MedicalTreatmentLossHour"
                                                FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtMedicalTreatmentLossHour" NoDecimal="true" NoNegative="true"
                                                        MinValue="0" Text='<%# Eval("MedicalTreatmentLossHour")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="无伤害事故" TextAlign="Center">
                                <Columns>
                                    <f:GroupField EnableLock="true" HeaderText="火灾" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="50px" ColumnID="FireNum" DataField="FireNum" FieldType="String"
                                                HeaderText="起数" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtFireNum" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("FireNum")%>'
                                                        runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="爆炸" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="50px" ColumnID="ExplosionNum" DataField="ExplosionNum" FieldType="String"
                                                HeaderText="起数" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtExplosionNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        Text='<%# Eval("ExplosionNum")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="交通" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="50px" ColumnID="TrafficNum" DataField="TrafficNum" FieldType="String"
                                                HeaderText="起数" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtTrafficNum" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("TrafficNum")%>'
                                                        runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="机械设备" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="75px" ColumnID="EquipmentNum" DataField="EquipmentNum" FieldType="String"
                                                HeaderText="起数" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtEquipmentNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                        Text='<%# Eval("EquipmentNum")%>' runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="质量" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="50px" ColumnID="QualityNum" DataField="QualityNum" FieldType="String"
                                                HeaderText="起数" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtQualityNum" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("QualityNum")%>'
                                                        runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="其它" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="50px" ColumnID="OtherNum" DataField="OtherNum" FieldType="String"
                                                HeaderText="起数" HeaderTextAlign="Center">
                                                <Editor>
                                                    <f:NumberBox ID="txtOtherNum" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("OtherNum")%>'
                                                        runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="急救包扎" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="75px" ColumnID="FirstAidDressingsNum" DataField="FirstAidDressingsNum"
                                        FieldType="String" HeaderText="起数" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtFirstAidDressingsNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("FirstAidDressingsNum")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="未遂事件" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="75px" ColumnID="AttemptedEventNum" DataField="AttemptedEventNum"
                                        FieldType="String" HeaderText="起数" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtAttemptedEventNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("AttemptedEventNum")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:RenderField Width="100px" ColumnID="LossDayNum" DataField="LossDayNum" FieldType="String"
                                HeaderText="损失工日" HeaderTextAlign="Center">
                                <Editor>
                                    <f:NumberBox ID="txtLossDayNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                        Text='<%# Eval("LossDayNum")%>' runat="server">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="MillionsMonthlyReportItemId" DataField="MillionsMonthlyReportItemId"
                                FieldType="String" HeaderText="主键" Hidden="true" HeaderTextAlign="Center">
                                <Editor>
                                    <f:TextBox runat="server" ID="TextBox2" Text='<%# Eval("MillionsMonthlyReportItemId")%>'>
                                    </f:TextBox>
                                </Editor>
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server" Margin="0 50 30 50">
                <Items>
                    <f:Button ID="btnCopy" Icon="Database" runat="server" ToolTip="复制上月数据"
                        ValidateForms="SimpleForm1" OnClick="btnCopy_Click" Hidden = "true">
                    </f:Button>
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" Hidden="true" 
                        ValidateForms="SimpleForm1" OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" Hidden="true" ToolTip="提交"
                        ValidateForms="SimpleForm1" OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnUpdata" Icon="PageSave" runat="server" Hidden="true" ConfirmText="确定上报？"
                        ToolTip="上报" ValidateForms="SimpleForm1" OnClick="btnUpdata_Click">
                    </f:Button>
                                        
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" IconUrl="~/res/images/16/11.png" runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" OnClose="Window1_Close"
        Title="办理流程" CloseAction="HidePostBack" EnableIFrame="true" Height="250px" Width="500px">
    </f:Window>
    </form>
</body>
</html>
