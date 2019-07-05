<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationHandleEdit.aspx.cs"
    Inherits="FineUIPro.Web.Hazard.RegistrationHandleEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>危险观察整改</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AjaxAspnetControls="divFile1,divFile2" />
    <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%"
        MarginRight="5px">
        <Items>
            <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="430px" ShowBorder="true" 
                TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" 
                runat="server" ActiveTabIndex="1">
                <Tabs>
                    <f:Tab ID="Tab1" Title="危险登记" BodyPadding="5px" Layout="VBox" IconFont="Bookmark"
                        runat="server">
                        <Items>
                            <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
                                LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextArea ID="txtProblemDescription" runat="server" MaxLength="800" Label="问题描述"
                                                Readonly="true" LabelWidth="100px" Required="true" ShowRedStar="true" Height="50px">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextArea ID="txtTakeSteps" runat="server" MaxLength="500" Label="采取措施" Readonly="true"
                                                LabelWidth="100px" Height="50px">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ColumnWidths="8% 92%">
                                        <Items>
                                            <f:Label runat="server" ID="lbFile1" Label="整改前图片">
                                            </f:Label>
                                            <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" ShowBorder="false"
                                                Title="附件1">
                                                <table>
                                                    <tr style="height: 28px">
                                                        <td align="left">
                                                            <div id="divFile1" runat="server">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </f:ContentPanel>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtUnit" runat="server" Label="责任单位" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtResponsibilityMan" runat="server" Label="责任人" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtWorkArea" runat="server" Label="受检区域" Readonly="true">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                             <f:TextBox ID="txtRegistrationCode" runat="server" Label="编号" Readonly="true" Required="true"
                                                EmptyText="选择责任单位后自动生成编号" ShowRedStar="true" MaxLength="50" LabelWidth="100px">
                                            </f:TextBox>
                                            <f:TextBox ID="txtCheckMan" runat="server" Label="检查人" Readonly="true" MaxLength="50"
                                                LabelWidth="100px">
                                            </f:TextBox>
                                            <f:TextBox ID="txtCheckTime" runat="server" Label="检查日期" Readonly="true">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtProblemTypes" runat="server" Label="问题类型" Readonly="true" MaxLength="200"
                                                LabelWidth="100px">
                                            </f:TextBox>
                                            <f:TextBox ID="txtRectificationPeriod" runat="server" Label="整改期限" Readonly="true">
                                            </f:TextBox>
                                             <f:TextBox ID="txtIsEffective" runat="server" Label="是否有效" Readonly="true" MaxLength="50">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtHazardCode" runat="server" Label="危险源编号" Readonly="true" MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="txtDefectsType" runat="server" Label="缺陷类型" MaxLength="50" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtMayLeadAccidents" runat="server" Label="可能导致的事故" MaxLength="100"
                                                Readonly="true" LabelWidth="120px">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </Items>
                    </f:Tab>
                    <f:Tab ID="Tab2" Title="整改" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server">
                        <Items>
                            <f:Form ID="SimpleForm2" LabelAlign="Right" MessageTarget="Qtip" RedStarPosition="BeforeText"
                                LabelWidth="90px" BodyPadding="5px" ShowBorder="false" ShowHeader="false" runat="server"
                                AutoScroll="false">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="整改时间" LabelWidth="100px" ID="txtRectificationTime">
                                            </f:DatePicker>
                                            <f:Label runat="server" ID="lb11">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextArea ID="txtRectificationRemark" runat="server" MaxLength="500" Label="整改说明"
                                                LabelWidth="100px" Height="50px">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow ColumnWidths="8% 42% 10% 40%">
                                        <Items>
                                            <f:Label runat="server" ID="Label3" Text="附件：" MarginLeft="50">
                                            </f:Label>
                                            <f:FileUpload runat="server" ID="btnFile2" EmptyText="请选择附件" OnFileSelected="btnFile2_Click"
                                                AutoPostBack="true">
                                            </f:FileUpload>
                                            <f:Button ID="btnClear" runat="server" Text="清空图片" OnClick="btnClear_Click"></f:Button>
                                            <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="false"
                                                Title="附件2">
                                                <table>
                                                    <tr style="height: 28px">
                                                        <td align="left">
                                                            <div id="divFile2" runat="server">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </f:ContentPanel>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow runat="server" ID="tr1" Hidden="true">
                                        <Items>
                                            <f:TextBox ID="txtConfirmMan" runat="server" Label="确认人" Readonly="true" MaxLength="50"
                                                LabelWidth="100px">
                                            </f:TextBox>
                                            <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="确认时间" ID="txtConfirmDate">
                                            </f:DatePicker>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow runat="server" ID="tr2" Hidden="true">
                                        <Items>
                                            <f:RadioButtonList ID="rblState" runat="server" Label="处理结果" LabelWidth="100px" 
                                                AutoPostBack="true" OnSelectedIndexChanged="rblState_SelectedIndexChanged">
                                                <f:RadioItem Value="2" Selected="true" Text="闭环" />
                                                <f:RadioItem Value="3" Text="重新整改" />
                                            </f:RadioButtonList>
                                            <f:Label runat="server" ID="Label1">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow runat="server" ID="tr3" Hidden="true">
                                        <Items>
                                            <f:TextArea ID="txtHandleIdea" runat="server" MaxLength="500" Label="重新整改意见" LabelWidth="100px"
                                                Height="50px">
                                            </f:TextArea>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </Items>
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                                <Items>
                                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm2"
                                        OnClick="btnSave_Click">
                                    </f:Button>
                                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm2"
                                        OnClick="btnSubmit_Click">
                                    </f:Button>
                                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                                    </f:Button>
                                    <f:HiddenField runat="server" ID="hdConfirmMan">
                                    </f:HiddenField>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                    </f:Tab>
                </Tabs>
            </f:TabStrip>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
