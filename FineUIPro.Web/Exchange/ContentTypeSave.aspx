<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContentTypeSave.aspx.cs" Inherits="FineUIPro.Web.Exchange.ContentTypeSave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                   <f:TextBox ID="txtContentTypeCode" runat="server" MaxLength="50" Label="编号" Required="true" ShowRedStar="true" FocusOnPageLoad="true"></f:TextBox>
                    
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                   <f:TextBox ID="txtContentTypeName" runat="server" MaxLength="50" Label="类别名称" Required="true" ShowRedStar="true"></f:TextBox>
                </Items>
            </f:FormRow>            
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" Hidden="true"  ValidateForms="SimpleForm1"
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
