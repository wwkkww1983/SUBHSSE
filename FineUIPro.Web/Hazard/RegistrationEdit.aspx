<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationEdit.aspx.cs" Inherits="FineUIPro.Web.Hazard.RegistrationEdit" %>
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
                    <f:TextArea ID="txtProblemDescription" runat="server" MaxLength="800" Label="问题描述" 
                        LabelWidth="100px" Required="true" ShowRedStar="true" Height="50px" FocusOnPageLoad="true">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtTakeSteps" runat="server" MaxLength="500" Label="采取措施" LabelWidth="100px" Height="50px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="8% 42% 10% 40%">
                <Items>
                    <f:Label runat="server" ID="lbFile1" Label="整改前图片">
                    </f:Label>
                    <f:FileUpload runat="server" ID="btnFile1" EmptyText="请选择附件" OnFileSelected="btnFile1_Click"
                        AutoPostBack="true">
                    </f:FileUpload>
                    <f:Button ID="btnClear" runat="server" Text="清空图片" OnClick="btnClear_Click"></f:Button>
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
                    <f:DropDownList runat="server" EnableSimulateTree="True" ShowRedStar="true" Label="责任单位" ID="drpUnit"
                        AutoPostBack="true" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged"  EnableEdit="true">
                    </f:DropDownList>
                    <f:DropDownList runat="server" EnableSimulateTree="True" Label="责任人" ID="drpResponsibilityMan" EnableEdit="true" Required="true" ShowRedStar="true">
                    </f:DropDownList>
                     <f:DropDownList runat="server" EnableSimulateTree="True" ShowRedStar="true" Label="受检区域" ID="drpWorkArea" EnableEdit="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRegistrationCode" runat="server" Label="编号" Readonly="true" Required="true" EmptyText="选择责任单位后自动生成编号"
                        ShowRedStar="true" MaxLength="50" LabelWidth="100px">
                    </f:TextBox>
                      <f:TextBox ID="txtCheckMan" runat="server" Label="检查人" Readonly="true" MaxLength="50" LabelWidth="100px">
                    </f:TextBox>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="检查日期" ID="txtCheckTime">
                    </f:DatePicker>
                   
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtProblemTypes" runat="server" Label="问题类型"
                        MaxLength="200" LabelWidth="100px">
                    </f:TextBox>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="整改期限" ID="txtRectificationPeriod">
                    </f:DatePicker>
                      <f:RadioButtonList ID="rblIsEffective" runat="server" Label="是否有效" LabelWidth="100px" >
                    </f:RadioButtonList>
                </Items>
            </f:FormRow>                     
            <f:FormRow ColumnWidths="8% 3% 25% 30% 30%">
                <Items>
                    <f:Label runat="server" Label="危险源类型"></f:Label>
                    <f:Button ID="btnSelect" Icon="ShapeSquareSelect" runat="server" ToolTip="选择危险源类型" 
                        OnClick="btnSelect_Click">
                    </f:Button>
                    <f:TextBox ID="txtHazardCode" runat="server" Label="危险源编号" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtDefectsType" runat="server" Label="缺陷类型" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtMayLeadAccidents" runat="server" Label="可能导致的事故" MaxLength="100" LabelWidth="120px">
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
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:HiddenField runat="server" ID="hdSelect"></f:HiddenField>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window2" Title="危险源清单" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="1100px" Height="700px">
    </f:Window>
    </form>
</body>
</html>
