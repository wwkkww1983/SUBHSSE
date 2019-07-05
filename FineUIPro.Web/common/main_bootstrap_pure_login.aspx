<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main_bootstrap_pure_login.aspx.cs" Inherits="FineUIPro.Web.common.main_bootstrap_pure_login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Window ID="Window1" runat="server" Title="登录表单" IsModal="false" EnableClose="false" EnableCollapse="true"
            WindowPosition="GoldenSection" Width="350px" IconFont="SignIn">
            <Items>
                <f:SimpleForm ID="SimpleForm1" runat="server" ShowBorder="false" BodyPadding="10px"
                    LabelWidth="80px" ShowHeader="false">
                    <Items>
                        <f:TextBox ID="tbxUserName" Label="用户名" Required="true" ShowRedStar="true" runat="server">
                        </f:TextBox>
                        <f:TextBox ID="tbxPassword" Label="密码" TextMode="Password" Required="true" ShowRedStar="true" runat="server">
                        </f:TextBox>
                    </Items>
                </f:SimpleForm>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" ToolbarAlign="Right" Position="Bottom">
                    <Items>
                        <f:Button ID="btnLogin" Text="登录" Type="Submit" CssClass="f-btn-success" ValidateForms="SimpleForm1" ValidateTarget="Top"
                            runat="server" OnClick="btnLogin_Click">
                        </f:Button>
                        <f:Button ID="btnReset" Text="重置" CssClass="f-btn-gray" Type="Reset" EnablePostBack="false"
                            runat="server">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>
    </form>
</body>
</html>
