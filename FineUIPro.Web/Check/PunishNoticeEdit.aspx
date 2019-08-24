<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PunishNoticeEdit.aspx.cs"
    Inherits="FineUIPro.Web.Check.PunishNoticeEdit" ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑处罚通知单</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="处罚通知单" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtPunishNoticeCode" runat="server" Label="编号" LabelAlign="Right"
                        MaxLength="50" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:DatePicker ID="txtPunishNoticeDate" runat="server" Label="处罚时间" LabelAlign="Right"
                        EnableEdit="true" Required="true" ShowRedStar="true">
                    </f:DatePicker>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="受罚单位" LabelAlign="Right" EnableEdit="true"
                        Required="true" ShowRedStar="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpSignMan" runat="server" Label="签发人" LabelAlign="Right" LabelWidth="120px">
                    </f:DropDownList>
                    <f:DropDownList ID="drpApproveMan" runat="server" Label="批准人" LabelAlign="Right" LabelWidth="120px">
                    </f:DropDownList>
                    <f:TextBox ID="txtContractNum" runat="server" Label="合同号" LabelAlign="Right" MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                <f:TextBox ID="txtIncentiveReason" runat="server" Label="处罚原因" LabelAlign="Right" MaxLength="300"
                        LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtBasicItem" runat="server" Label="处罚根据" LabelAlign="Right" MaxLength="300"
                        LabelWidth="120px">
                    </f:TextBox>
                    
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="50% 20% 30%">
                <Items>
                    <f:NumberBox runat="server" ID="txtPunishMoney" Label="处罚金额" OnBlur="txtPunishMoney_Blur"
                        EnableBlurEvent="true" NoNegative="true" LabelWidth="120px">
                    </f:NumberBox>
                    <f:TextBox runat="server" ID="txtCurrency" Label="币种" MaxLength="50" LabelWidth="60px">
                    </f:TextBox>
                    <f:TextBox runat="server" ID="txtBig" Label="大写" Readonly="true" LabelWidth="60px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="处罚原因/决定" ID="txtFileContents" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="220" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                     <f:Button ID="btnPunishNoticeUrl" Text="通知单" ToolTip="通知单上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnPunishNoticeUrl_Click" ValidateForms="SimpleForm1" >
                    </f:Button>
                    <f:Button ID="btnAttachUrl" Text="回执单" ToolTip="回执单上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnUploadResources_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
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
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
