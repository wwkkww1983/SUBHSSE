<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContentSee.aspx.cs" Inherits="FineUIPro.Web.Exchange.ContentSee" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow ColumnWidths="15% 85%">
                <Items>
                    <f:Label runat="server" ID="lb1" Text="主题">
                    </f:Label>
                    <f:Label runat="server" ID="txtTheme">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="15% 85%">
                <Items>
                    <f:Label runat="server" ID="Label1" Text="话题类型">
                    </f:Label>
                    <f:Label runat="server" ID="txtContentType">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="15% 85%">
                <Items>
                    <f:Label runat="server" ID="Label2" Text="内容">
                    </f:Label>
                    <f:Label runat="server" ID="txtContents">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="15% 35% 15% 35%">
                <Items>
                    <f:Label runat="server" ID="Label3" Text="发帖人">
                    </f:Label>
                    <f:Label runat="server" ID="txtCompileMan">
                    </f:Label>
                    <f:Label runat="server" ID="Label4" Text="发帖时间">
                    </f:Label>
                    <f:Label runat="server" ID="txtCompileDate">
                    </f:Label>
                </Items>
            </f:FormRow>                              
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnUploadResources" Text="附件" ToolTip="附件上传及查看" Icon="SystemNew" runat="server" OnClick="btnUploadResources_Click"
                        ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                 
                    <f:Button ID="btnClose" EnablePostBack="false" Text="关闭" runat="server" Icon="SystemClose">
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
