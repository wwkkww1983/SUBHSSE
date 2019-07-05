<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="SupervisionNoticeView.aspx.cs"
    Inherits="FineUIPro.Web.Check.SupervisionNoticeView" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看监理整改通知单</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSupervisionNoticeCode" runat="server" Label="编号" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtUnitName" runat="server" Label="责任单位" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtWorkAreaName" runat="server" Label="检查区域" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckedDate" runat="server" Label="受检时间" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtWrongContent" runat="server" Label="安全隐患内容及整改意见" Readonly="true">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSignPerson" runat="server" Label="签发人" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtSignDate" runat="server" Label="日期" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="下栏内容由整改单位填写，并在以上整改内容要求的时间内，将此表交本部"
                        runat="server">
                        <Items>
                            <f:TextArea ID="txtCompleteStatus" runat="server" Label="整改措施和完成情况" Readonly="true">
                            </f:TextArea>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtDutyPerson" runat="server" Label="责任人" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCompleteDate" runat="server" Label="日期" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtIsRectify" runat="server" Label="检查是否完成整改" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckPerson" runat="server" Label="检查人" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnUploadResources_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
