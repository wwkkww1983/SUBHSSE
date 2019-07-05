<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Login.aspx.cs" Inherits="FineUIPro.Web.Login"  Async="true" AsyncTimeout="360000" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <meta name="sourcefiles" content="~/Captcha/captcha.ashx;~/Captcha/CaptchaImage.cs" />
    <style type="text/css">
        .imgcaptcha .f-field-label {
            margin: 0;
        }

        .login-image {
            border-width: 0 1px 0 0;
            width: 116px;
            height: 116px;
        }

            .login-image .ui-icon {
                font-size: 96px;
       }
       
       .mybgpanel.bg1 > .f-panel-body {
            background-color: #B1EEEF;
            background-image: url(Images/LoginHSSE.jpg);
        } 
         .text
        {
            font-size:14pt;
        }      
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" ></f:PageManager>
         <f:Window ID="Window1" runat="server" Title="登录表单" IsModal="false" EnableClose="false" IconFont="SignIn" ShowHeader="false"
                    WindowPosition="Center" Layout="VBox" CssClass="mybgpanel bg1" Width="978px" Height="560px">
            <Items>                           
                <f:SimpleForm ID="SimpleForm1" BoxFlex="1" runat="server" ShowBorder="false" BodyPadding="10px" LabelAlign="Right"
                    LabelWidth="70px" ShowHeader="false" Layout="VBox" Height="150px" Width="300px" Margin="260px 0px 0 0px">
                    <Items>                                                
                        <f:TextBox ID="tbxUserName" Required="true" ShowRedStar="true" runat="server" 
                             Margin="0px 330px 0 330px" NextFocusControl="tbxPassword" Label="用户名">
                        </f:TextBox>
                        <f:TextBox ID="tbxPassword" Label="密码" TextMode="Password" Required="true" ShowRedStar="true" runat="server" Margin="2px 330px 0 330px" NextFocusControl="tbxCaptcha">
                        </f:TextBox>
                        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="Stretch" runat="server" Margin="0px 330px 0 330px">
                            <Items>
                                <f:TextBox ID="tbxCaptcha" BoxFlex="1" Margin="2px 5px 0 0" Label="验证码" Required="true" runat="server">
                                </f:TextBox>
                                <f:LinkButton ID="imgCaptcha" CssClass="imgcaptcha" Width="100px" EncodeText="false" runat="server" OnClick="imgCaptcha_Click">
                                </f:LinkButton>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel2" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="Stretch" runat="server" Margin="0px 330px 0 300px">
                            <Items>
                                <f:Button ID="btnLogin" Text="登录" Type="Submit" ValidateForms="SimpleForm1" ValidateTarget="Top"
                                    runat="server" OnClick="btnLogin_Click" Margin="5px 10px 0 90px" >
                                </f:Button>
                                <f:Button ID="btnReset" Text="重置" Type="Reset" EnablePostBack="false"
                                    runat="server" Margin="5px 0 0 0" OnClick="btnReset_Click">
                                </f:Button>
                                <f:CheckBox runat="server" Label="记住" ID="ckRememberMe" Margin="5px 0 0 0"></f:CheckBox>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel4" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="Stretch" runat="server" Margin="100px 200px 0 380px">
                            <Items>   
                                <f:Label ID="lbSubName" runat="server" CssClass="text"></f:Label>                               
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel3" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="Stretch" runat="server"  Margin="0px 200px 0 220px">
                            <Items>                                   
                                <f:Label ID="lbVevion" runat="server" MarginLeft="50px" ToolTip="当前软件运行环境要求及当前系统版本。">                                 
                                </f:Label>                                              
                            </Items>
                        </f:Panel>
                    </Items>
                </f:SimpleForm>
            </Items>           
        </f:Window>
    </form>
</body>
</html>
     
        
