<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckColligationDetailWHEdit.aspx.cs"
    Inherits="FineUIPro.Web.Check.CheckColligationDetailWHEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑综合检查明细（五环）</title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
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
                    <f:TextArea ID="txtUnqualified" runat="server" Label="隐患内容" ShowRedStar="true" Required="true">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="35% 15% 50%">
                <Items>
                    <f:TextBox ID="txtWorkArea" runat="server" Label="检查区域" MaxLength="3000" ShowRedStar="true"
                        Required="true">
                    </f:TextBox>
                    <f:DropDownList ID="drpWorkArea" runat="server" Label="" AutoPostBack="true" OnSelectedIndexChanged="drpWorkArea_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:DatePicker ID="txtLimitedDate" runat="server" Label="整改期限" EnableEdit="false">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtHiddenDangerType" runat="server" Label="隐患类型" ShowRedStar="true"
                        Required="true">
                    </f:TextBox><f:DropDownList ID="drpHiddenDangerLevel" runat="server" Label="隐患级别" EnableEdit="false" Required="true" ShowRedStar="true"></f:DropDownList>
                    <%--<f:TextBox ID="txtHiddenDangerLevel" runat="server" Label="隐患级别" ShowRedStar="true"
                        Required="true">
                    </f:TextBox>--%>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpUnit" runat="server" Label="责任单位" ShowRedStar="true" Required="true"
                        AutoPostBack="true" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:DropDownList ID="drpPerson" runat="server" Label="责任人" ShowRedStar="true" Required="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSuggestions" runat="server" Label="整改要求">
                    </f:TextBox>
                    <f:DropDownList ID="drpHandleStep" runat="server" Label="处理措施" ShowRedStar="true"
                        Required="true" EnableMultiSelect="true" EnableCheckBoxSelect="true" AutoSelectFirstItem="false">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label ID="Label1" runat="server" Text="说明：检查区域可从下拉框选择也可手动编辑。" CssClass="lab" MarginLeft="5px">
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
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
