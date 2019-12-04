<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PauseNoticeEdit.aspx.cs"
    Inherits="FineUIPro.Web.Check.PauseNoticeEdit" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工程暂停令</title>
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
                    <f:TextBox ID="txtPauseNoticeCode" runat="server" Label="编号" Readonly="true" Required="true"
                        ShowRedStar="true" MaxLength="50" LabelWidth="100px">
                    </f:TextBox>
                    <f:DropDownList runat="server" EnableSimulateTree="True" Label="受检单位" ID="drpUnit"
                        AutoPostBack="true" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:TextBox ID="txtProjectPlace" runat="server" Label="工程部位" Required="true" ShowRedStar="true"
                        MaxLength="200">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpSignMan" runat="server" Label="签发人" LabelAlign="Right">
                    </f:DropDownList>
                    <f:DropDownList ID="drpApproveMan" runat="server" Label="批准人" LabelAlign="Right">
                    </f:DropDownList>
                    <f:TextBox ID="txtSignPerson" runat="server" Label="编制人" MaxLength="50">
                    </f:TextBox>
                    <f:DatePicker runat="server" Label="编制时间" ID="txtComplieDate" Required="true" ShowRedStar="true"
                        EnableEdit="true">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="65% 35%">
                <Items>
                    <f:TextBox ID="txtWrongContent" runat="server" Label="鉴于贵公司在" Required="true" ShowRedStar="true"
                        MaxLength="150" LabelWidth="120px" Width="250px">
                    </f:TextBox>
                    <f:Label runat="server" ID="lb1" Text="已构成重大安全隐患和质量隐患。详见附图：">
                    </f:Label>
                </Items>
            </f:FormRow>
            <%--<f:FormRow ColumnWidths="2% 98%">
                <Items>
                    <f:Label runat="server" ID="Label12" Text=""></f:Label>
                    <f:Label runat="server" ID="Label11" Text="等施工 ，已构成重大安全隐患和质量隐患，详见附图。"></f:Label>
                </Items>
            </f:FormRow>--%>
            <f:FormRow ColumnWidths="23% 2% 6% 2% 8% 2% 6% 25% 25%">
                <Items>
                    <f:NumberBox ID="txtYear" runat="server" Label="现通知你方必须于" NoDecimal="true" LabelWidth="150px">
                    </f:NumberBox>
                    <f:Label runat="server" ID="Label1" Text="年">
                    </f:Label>
                    <f:NumberBox ID="txtMonth" runat="server" NoDecimal="true" LabelWidth="0px">
                    </f:NumberBox>
                    <f:Label runat="server" ID="Label13" Text="月">
                    </f:Label>
                    <f:NumberBox ID="txtDay" runat="server" NoDecimal="true" LabelWidth="0px">
                    </f:NumberBox>
                    <f:Label runat="server" ID="Label14" Text="日">
                    </f:Label>
                    <f:NumberBox ID="txtHour" runat="server" NoDecimal="true" LabelWidth="0px">
                    </f:NumberBox>
                    <f:TextBox ID="txtPauseContent" runat="server" Label="时起,对本工程的" MaxLength="150" LabelWidth="130px">
                    </f:TextBox>
                    <f:Label runat="server" ID="Label18" Text="作业实施暂停施工，并按下述要求做好各项工作：">
                    </f:Label>
                </Items>
            </f:FormRow>
            <%--<f:FormRow ColumnWidths="2% 98%">
                <Items>
                    <f:Label runat="server" ID="Label22" Text=""></f:Label>
                    <f:Label runat="server" ID="Label4" Text="并按下述要求做好各项工作："></f:Label>
                </Items>
            </f:FormRow>--%>
            <f:FormRow ColumnWidths="2% 19% 50% 29%">
                <Items>
                    <f:Label runat="server" ID="Label23" Text="">
                    </f:Label>
                    <f:Label runat="server" ID="Label3" Text="1、 立即组织足够的人力和机具对">
                    </f:Label>
                    <f:TextBox ID="txtOneContent" runat="server" Label="" MaxLength="150" LabelWidth="0px">
                    </f:TextBox>
                    <f:Label runat="server" ID="Label16" Text="进行安全可靠的保护措施，防止隐患进一步扩大。">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="2% 15% 53% 30%">
                <Items>
                    <f:Label runat="server" ID="Label24" Text="">
                    </f:Label>
                    <f:Label runat="server" ID="Label19" Text="2、 立即组织相关专家编制对">
                    </f:Label>
                    <f:TextBox ID="txtSecondContent" runat="server" Label="" MaxLength="150" LabelWidth="0px">
                    </f:TextBox>
                    <f:Label runat="server" ID="Label20" Text="整改方案，交由总包、监理、业主审查批准后实施。">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="2% 6% 56% 36%">
                <Items>
                    <f:Label runat="server" ID="Label25" Text="">
                    </f:Label>
                    <f:Label runat="server" ID="Label21" Text="3、 在对">
                    </f:Label>
                    <f:TextBox ID="txtThirdContent" runat="server" Label="" MaxLength="150" LabelWidth="0px">
                    </f:TextBox>
                    <f:Label runat="server" ID="Label4" Text="进行整改过程中，要采取必要的防护措施，保护人员、机构和设备安全。">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="8% 42% 50%">
                <Items>
                    <f:Label runat="server" ID="lbFile1" Text="附件：" MarginLeft="50">
                    </f:Label>
                    <f:FileUpload runat="server" ID="btnFile1" EmptyText="请选择附件" OnFileSelected="btnFile1_Click"
                        AutoPostBack="true">
                    </f:FileUpload>
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
                    <f:TextBox ID="txtProjectHeadConfirm" runat="server" Label="项目经理签字" MaxLength="50"
                        LabelWidth="110px" Enabled="false">
                    </f:TextBox>
                    <f:DatePicker runat="server" Label="签字确认日期" ID="txtConfirmDate" LabelWidth="110px"
                        EnableEdit="true" Enabled="false">
                    </f:DatePicker>
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
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
