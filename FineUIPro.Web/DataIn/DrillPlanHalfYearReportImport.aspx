<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrillPlanHalfYearReportImport.aspx.cs"
    Inherits="FineUIPro.Web.DataIn.DrillPlanHalfYearReportImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>导入应急演练工作计划半年报表</title>
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
                        BoxFlex="1" DataKeyNames="DrillPlanHalfYearReportItemId" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="DrillPlanHalfYearReportItemId" AllowSorting="true"
                        SortField="YearId" PageSize="12" Height="400px">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:TemplateField Width="200px" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# ConvertUnit(Eval("UnitId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="90px" ColumnID="YearId" DataField="YearId" FieldType="Int"
                                HeaderText="年度" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <%-- <f:RenderField Width="90px" ColumnID="HalfYearId" DataField="HalfYearId" FieldType="Int"
                                HeaderText="季度" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>--%>
                            <f:TemplateField Width="90px" HeaderText="季度" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# ConvertHalfYear(Eval("HalfYearId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="120px" ColumnID="Telephone" DataField="Telephone" FieldType="String"
                                HeaderText="联系电话" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="150px" ColumnID="DrillPlanName" DataField="DrillPlanName" FieldType="String"
                                HeaderText="演练名称" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="180px" ColumnID="OrganizationUnit" DataField="OrganizationUnit" FieldType="String"
                                HeaderText="组织单位" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="DrillPlanDate" DataField="DrillPlanDate" FieldType="String"
                                HeaderText="演练时间" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="200px" ColumnID="AccidentScene" DataField="AccidentScene" FieldType="String"
                                HeaderText="主要事故情景" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="150px" ColumnID="ExerciseWay" DataField="ExerciseWay" FieldType="String"
                                HeaderText="演练方式" HeaderTextAlign="Center" TextAlign="Center">
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
    <f:Window ID="Window1" Title="审核应急演练工作计划半年报表" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="导入应急演练工作计划半年报表" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    <%--<f:Window ID="Window3" Title="保存应急演练工作计划半年报表" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window3_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>--%>
    </form>
</body>
</html>
