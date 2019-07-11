<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckDayXAView.aspx.cs" Inherits="FineUIPro.Web.Check.CheckDayXAView" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>查看日常巡检</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
        
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckDayCode" runat="server" Label="检查编号" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckDate" runat="server" Label="检查日期" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckMan" runat="server" Label="检查人" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtNotOKNum" runat="server" Label="不合格数" Readonly="true" MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow runat="server" ID="trUnit" Hidden="true">
                <Items>
                    <f:TextBox ID="drpDutyUnitIds" runat="server" Label="受检单位" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow runat="server" ID="trTeamGroup" Hidden="true">
                <Items>
                    <f:TextBox ID="drpDutyTeamGroupIds" runat="server" Label="受检班组" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="drpWorkAreaIds" runat="server" Label="检查区域" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtUnqualified" runat="server" Readonly="true" Label="不合格项描述">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtHandleStation" runat="server" Readonly="true" Label="整改情况">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtIsOK" runat="server" Label="是否闭环" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp"></f:Label>
                     <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server" OnClick="btnAttachUrl_Click"
                        ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:Button ID="btnHandleAttachUrl" Text="整改情况附件" ToolTip="整改情况附件上传及查看" Icon="TableCell" runat="server" OnClick="btnHandleAttachUrl_Click"
                        ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:HiddenField ID="hdId" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdAttachUrl" runat="server">
                    </f:HiddenField>
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
