<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentCauseReportImport.aspx.cs"
    Inherits="FineUIPro.Web.DataIn.AccidentCauseReportImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入职工伤亡事故原因分析报表</title>
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
                    <%--<f:Button ID="btnOut" Icon="Pencil" runat="server" Text="导出" ToolTip="导出错误列表" ValidateForms="SimpleForm1"
                        OnClick="btnOut_Click" Hidden="true"></f:Button>--%>
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
                        BoxFlex="1" DataKeyNames="AccidentCauseReportItemId" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="AccidentCauseReportItemId" AllowSorting="true"
                        SortField="Year" PageSize="12" Height="400px">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:TemplateField Width="200px" HeaderText="填报单位" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# ConvertUnit(Eval("UnitId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="90px" ColumnID="AccidentCauseReportCode" DataField="AccidentCauseReportCode"
                                FieldType="String" HeaderText="编号" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="50px" ColumnID="Year" DataField="Year" FieldType="Int" HeaderText="年度"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="50px" ColumnID="Month" DataField="Month" FieldType="Int" HeaderText="月份"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="DeathAccident" DataField="DeathAccident" FieldType="Int"
                                HeaderText="死亡事故数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="DeathToll" DataField="DeathToll" FieldType="Int"
                                HeaderText="死亡人数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="InjuredAccident" DataField="InjuredAccident"
                                FieldType="Int" HeaderText="重伤事故" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="InjuredToll" DataField="InjuredToll" FieldType="Int"
                                HeaderText="重伤人数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="MinorWoundAccident" DataField="MinorWoundAccident"
                                FieldType="Int" HeaderText="轻伤事故" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="MinorWoundToll" DataField="MinorWoundToll"
                                FieldType="Int" HeaderText="轻伤人数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="AverageTotalHours" DataField="AverageTotalHours"
                                FieldType="Int" HeaderText="平均工时总数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="AverageManHours" DataField="AverageManHours"
                                FieldType="Int" HeaderText="平均工时人数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="TotalLossMan" DataField="TotalLossMan" FieldType="Int"
                                HeaderText="损失工时总数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="LastMonthLossHoursTotal" DataField="LastMonthLossHoursTotal"
                                FieldType="Int" HeaderText="上月损失工时总数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="KnockOffTotal" DataField="KnockOffTotal" FieldType="Int"
                                HeaderText="歇工总日数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="DirectLoss" DataField="DirectLoss" FieldType="Int"
                                HeaderText="直接损失" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="IndirectLosses" DataField="IndirectLosses"
                                FieldType="Int" HeaderText="间接损失" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="TotalLoss" DataField="TotalLoss" FieldType="Int"
                                HeaderText="总损失" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="TotalLossTime" DataField="TotalLossTime" FieldType="Int"
                                HeaderText="无损失工时总数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="FillCompanyPersonCharge" DataField="FillCompanyPersonCharge"
                                FieldType="String" HeaderText="填报单位负责人" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="AccidentType" DataField="AccidentType" FieldType="String"
                                HeaderText="事故类别" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:GroupField EnableLock="true" HeaderText="合计" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="TotalDeath" DataField="TotalDeath" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="TotalInjuries" DataField="TotalInjuries" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="TotalMinorInjuries" DataField="TotalMinorInjuries"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="防护、保险信号等装置缺乏或装置缺乏或有缺陷" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death1" DataField="Death1" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries1" DataField="Injuries1" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries1" DataField="MinorInjuries1"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="设备、工具附件有缺陷" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death2" DataField="Death2" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries2" DataField="Injuries2" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries2" DataField="MinorInjuries2"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="个人防护用品缺乏或有缺陷" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death3" DataField="Death3" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries3" DataField="Injuries3" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries3" DataField="MinorInjuries3"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="光线不足或工作地点及通道情况不良" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death4" DataField="Death4" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries4" DataField="Injuries4" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries4" DataField="MinorInjuries4"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="劳动组织不合理" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death5" DataField="Death5" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries5" DataField="Injuries5" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries5" DataField="MinorInjuries5"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="对现场工作缺乏检查或指导有错误" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death6" DataField="Death6" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries6" DataField="Injuries6" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries6" DataField="MinorInjuries6"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="设计有缺陷" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death7" DataField="Death7" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries7" DataField="Injuries7" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries7" DataField="MinorInjuries7"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="不懂操作技术和知识" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death8" DataField="Death8" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries8" DataField="Injuries8" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries8" DataField="MinorInjuries8"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="违反操作规程或劳动纪律" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death9" DataField="Death9" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries9" DataField="Injuries9" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries9" DataField="MinorInjuries9"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="没有安全操作规程制度或不健全" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death10" DataField="Death10" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries10" DataField="Injuries10" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries10" DataField="MinorInjuries10"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="其他" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death11" DataField="Death11" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries11" DataField="Injuries11" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries11" DataField="MinorInjuries11"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
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
                    <f:Label ID="lblBottom" runat="server" Text="说明：填报单位、编号、年份、月份、事故类别为必填项！">
                    </f:Label>
                </Items>
            </f:FormRow>
            <%-- <f:FormRow>
                <Items>
                    <f:Grid ID="gvErrorInfo" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="Row" DataIDField="Row" AllowCellEditing="true"
                        EnableColumnLines="true" Hidden="true">
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
            </f:FormRow>--%>
        </Rows>
    </f:Form>
    <f:Window ID="Window1" Title="审核职工伤亡事故原因分析报表" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="导入职工伤亡事故原因分析报表" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    </form>
</body>
</html>
