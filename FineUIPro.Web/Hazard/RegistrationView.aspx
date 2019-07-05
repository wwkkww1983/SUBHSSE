<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationView.aspx.cs" Inherits="FineUIPro.Web.Hazard.RegistrationView" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>危险观察登记</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" AjaxAspnetControls="divFile1" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
        LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtProblemDescription" runat="server" MaxLength="800" Label="问题描述" Readonly="true" LabelWidth="100px" Required="true" ShowRedStar="true" Height="50px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtTakeSteps" runat="server" MaxLength="500" Label="采取措施" Readonly="true" LabelWidth="100px" Height="50px">
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
                    <f:TextBox ID="txtRegistrationCode" runat="server" Label="编号" Readonly="true" Required="true" EmptyText="选择责任单位后自动生成编号"
                        ShowRedStar="true" MaxLength="50" LabelWidth="100px">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckMan" runat="server" Label="检查人" Readonly="true" MaxLength="50" LabelWidth="100px">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckTime" runat="server" Label="检查日期" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>         
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtProblemTypes" runat="server" Label="问题类型" Readonly="true"
                        MaxLength="200" LabelWidth="100px">
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
                    <f:TextBox ID="txtMayLeadAccidents" runat="server" Label="可能导致的事故" MaxLength="100" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
