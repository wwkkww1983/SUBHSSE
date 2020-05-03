<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Login1.aspx.cs" Inherits="FineUIPro.Web.Login1"  Async="true" AsyncTimeout="360000" %>

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
       
        .mybgpanel.bg1 > .f-panel-bodyct > .f-panel-body  {
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
                    LabelWidth="70px" ShowHeader="false" Layout="VBox" Height="150px" Width="300px" Margin="250px 0px 0 0px">
                    <Items>                                                
                        <f:TextBox ID="tbxUserName" Required="true" ShowRedStar="true" runat="server"  FocusOnPageLoad="true"
                             Margin="0px 330px 0 330px" NextFocusControl="tbxPassword" Label="用户名">
                        </f:TextBox>
                        <f:TextBox ID="tbxPassword" Label="密码" TextMode="Password" Required="true" ShowRedStar="true" runat="server" Margin="2px 330px 0 330px" NextFocusControl="tbxCaptcha">
                        </f:TextBox>
                        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="Stretch" runat="server" Margin="2px 330px 0 330px">
                            <Items>
                                <f:TextBox ID="tbxCaptcha" BoxFlex="1" Label="验证码" Required="true" runat="server" >
                                </f:TextBox>
                                <f:LinkButton ID="imgCaptcha" CssClass="imgcaptcha" EncodeText="false" runat="server" Width="100px"
                                    MarginLeft="2px" OnClick="imgCaptcha_Click" Height="30px">
                                </f:LinkButton>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel2" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="Stretch" runat="server" Margin="0px 330px 0 300px">
                            <Items>
                                <f:Button ID="btnLogin" Text="登录" Type="Submit" ValidateForms="SimpleForm1" ValidateTarget="Parent"
                                    runat="server" OnClick="btnLogin_Click" Margin="5px 10px 0 110px" >
                                </f:Button>
                                <f:Button ID="btnReset" Text="重置" Type="Reset" EnablePostBack="false"
                                    runat="server" Margin="5px 0 0 0" OnClick="btnReset_Click">
                                </f:Button>
                                <f:CheckBox runat="server" Label="记住" ID="ckRememberMe" Margin="5px 0 0 0"></f:CheckBox>                                
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel4" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="Stretch" runat="server" Margin="70px 160px 0 400px">
                            <Items>   
                                <f:Label ID="lbSubName" runat="server" CssClass="text"></f:Label>                               
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel3" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="Stretch" runat="server" MarginLeft="200px">
                            <Items>                                   
                                <f:Label ID="lbVevion" runat="server" MarginLeft="50px" ToolTip="当前软件运行环境要求及当前系统版本。" Width="800px">                                 
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
     
        
