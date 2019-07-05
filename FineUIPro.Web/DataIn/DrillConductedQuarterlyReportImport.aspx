<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrillConductedQuarterlyReportImport.aspx.cs" Inherits="FineUIPro.Web.DataIn.DrillConductedQuarterlyReportImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>导入应急演练开展情况季报</title>
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
                        BoxFlex="1" DataKeyNames="DrillConductedQuarterlyReportItemId" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="DrillConductedQuarterlyReportItemId" AllowSorting="true"
                        SortField="YearId" PageSize="12" Height="400px">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:TemplateField Width="150px" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# ConvertUnit(Eval("UnitId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="50px" ColumnID="YearId" DataField="YearId" FieldType="Int"
                                HeaderText="年度" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="50px" ColumnID="Quarter" DataField="Quarter" FieldType="Int"
                                HeaderText="季度" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="IndustryType" DataField="IndustryType" FieldType="String"
                                HeaderText="行业类型" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="80px" ColumnID="TotalConductCount" DataField="TotalConductCount" FieldType="Int"
                                HeaderText="举办次数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="TotalPeopleCount" DataField="TotalPeopleCount" FieldType="Int"
                                HeaderText="参演人数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="80px" ColumnID="TotalInvestment" DataField="TotalInvestment" FieldType="Float"
                                HeaderText="直接投入" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="HQConductCount" DataField="HQConductCount" FieldType="Int"
                                HeaderText="举办次数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="HQPeopleCount" DataField="HQPeopleCount" FieldType="Int"
                                HeaderText="参演人数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="HQInvestment" DataField="HQInvestment" FieldType="Float"
                                HeaderText="直接投入" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="BasicConductCount" DataField="BasicConductCount" FieldType="Int"
                                HeaderText="举办次数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="BasicPeopleCount" DataField="BasicPeopleCount" FieldType="Int"
                                HeaderText="参演人数" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="BasicInvestment" DataField="BasicInvestment" FieldType="Float"
                                HeaderText="直接投入" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="ComprehensivePractice" DataField="ComprehensivePractice" FieldType="Int"
                                HeaderText="综合演练" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="CPScene" DataField="CPScene" FieldType="Int"
                                HeaderText="现场" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="CPDesktop" DataField="CPDesktop" FieldType="Int"
                                HeaderText="桌面" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="SpecialDrill" DataField="SpecialDrill" FieldType="Int"
                                HeaderText="专项演练" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                               <f:RenderField Width="90px" ColumnID="SDScene" DataField="SDScene" FieldType="Int"
                                HeaderText="现场" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField> 
                             <f:RenderField Width="90px" ColumnID="SDDesktop" DataField="SDDesktop" FieldType="Int"
                                HeaderText="桌面" HeaderTextAlign="Center" TextAlign="Center">
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
    <%--<f:Form ID="Form2" runat="server">
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
    <f:Window ID="Window1" Title="审核应急演练开展情况季报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="导入应急演练开展情况季报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
   <%-- <f:Window ID="Window3" Title="保存应急演练开展情况季报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window3_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>--%>
    </form>
</body>
</html>
