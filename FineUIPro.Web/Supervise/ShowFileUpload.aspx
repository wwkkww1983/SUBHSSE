<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowFileUpload.aspx.cs"
    Inherits="FineUIPro.Web.Supervise.ShowFileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
        LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="fuAttachUrl" EmptyText="请上传附件" Label="附件">
                    </f:FileUpload>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="lbAttachUrl" CssClass="labcenter" Label="附件详细">
                    </f:Label>                    
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="70% 10% 10% 10%">
                <Items>
                    <f:Label runat="server" ID="templb"></f:Label>
                     <f:Button ID="btnUpFile" Icon="Tick" runat="server" OnClick="btnUpFile_Click" ValidateForms="SimpleForm1"
                        ToolTip="上传附件">
                    </f:Button>
                    <f:Button ID="btnDelete" Icon="Delete" runat="server" OnClick="btnDelete_Click" ToolTip="删除附件">
                    </f:Button>
                    <f:Button ID="btnSee" Icon="Find" runat="server" OnClick="btnSee_Click" ToolTip="附件查看">
                    </f:Button>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
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
