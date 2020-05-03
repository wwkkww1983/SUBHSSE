<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Login.aspx.cs" Inherits="FineUIPro.Web.Login"  Async="true" AsyncTimeout="360000" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
        <meta name="sourcefiles" content="~/Captcha/captcha.ashx;~/Captcha/CaptchaImage.cs" />
    <style type="text/css">
        .mybgpanel > .f-panel-body {
            background-position: right bottom;
            background-repeat: no-repeat;
        }

        .mybgpanel.bg1 > .f-panel-bodyct > .f-panel-body {
            background-color: #B1EEEF;
            background-image: url(Images/LoginPage/SeDin.jpg);
        }
          .mybgpanel.bg2 > .f-panel-bodyct > .f-panel-body {
            background-color: #B1EEEF;
            background-image: url(Images/LoginPage/SeDin_Right.jpg);
        }
        .login-user .f-field-fieldlabel-cell {
            background: url(Images/LoginName.jpg) no-repeat left 5px;
            background-size: 25px 25px;
            padding-left: 18px;
        }
        .login-pwd .f-field-fieldlabel-cell {
            background: url(Images/PassWord.jpg) no-repeat left 5px;
            background-size: 25px 25px;
            padding-left: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1"  runat="server" />
        <f:Panel ID="Panel1" runat="server"  AutoScroll="true" ShowBorder="false" EnableCollapse="false"
            BodyPadding="0px" Layout="Column" ShowHeader="false" ColumnWidth="100%"  
            CssClass="mybgpanel bg2"   Height="630px" >
                <Items>                  
                        <f:Panel ID="Panel28"  runat="server" ShowBorder="false"  Layout="VBox"  
                            CssClass="mybgpanel bg1"  Width="1300px" Height="630px"  
                            ShowHeader="false">
                        <Items>   
                          <f:SimpleForm ID="SimpleForm1" BoxFlex="1" runat="server" ShowBorder="false" BodyPadding="10px"  LabelAlign="Right"
                                LabelWidth="70px" ShowHeader="false" Layout="VBox" Height="200px" Width="300px" Margin="225px 0px 0  0px">
                                <Items>     
                                    <f:TextBox ID="tbxUserName"  Required="true" ShowRedStar="true" runat="server"  
                                            Label="用户名" FocusOnPageLoad="true"  Margin="0px 60px 0 960px" NextFocusControl="tbxPassword"   ShowLabel="true"  >
                                    </f:TextBox>
                                    <f:TextBox ID="tbxPassword"  TextMode="Password"  Required="true"  Label="密码"  ShowLabel="true"
                                        ShowRedStar="true" runat="server" Margin="10px 60px 0 960px" NextFocusControl="tbxCaptcha">
                                    </f:TextBox>                                  
                                    <f:Panel ID="Panel2" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="Stretch" runat="server" 
                                        Margin="10px 60px 0 960px">
                                        <Items>
                                            <f:TextBox ID="tbxCaptcha" BoxFlex="1"  Label="验证码"  ShowLabel="true"
                                                Required="true" runat="server" >
                                            </f:TextBox>
                                              <f:LinkButton ID="imgCaptcha" CssClass="imgcaptcha" EncodeText="false" runat="server" Width="100px"
                                                    MarginLeft="2px" OnClick="imgCaptcha_Click" Height="30px">
                                                </f:LinkButton>
                                        </Items>
                                    </f:Panel>
                                    <f:Panel ID="Panel3" ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigAlign="Stretch" runat="server" 
                                        Margin="0px 200px 0 900px">
                                        <Items>
                                            <f:Button ID="btnLogin" Text="登&nbsp;&nbsp;&nbsp;&nbsp;录" Type="Submit" ValidateForms="SimpleForm1" ValidateTarget="Parent"
                                                runat="server" Margin="10px 10px 0 110px"  EnablePress="true" Pressed="true" Size="Medium" OnClick="btnLogin_Click" >
                                            </f:Button>   
                                            <f:CheckBox runat="server"  ShowLabel="false" ID="ckRememberMe" MarginLeft="5px" MarginTop="10px"
                                                LabelAlign="Left" Text="记住登录名"></f:CheckBox>            
                                        </Items>
                                    </f:Panel>                                    
                                </Items>
                            </f:SimpleForm>
                        </Items>
                    </f:Panel>
                    </Items>
        </f:Panel>
              <%--<f:Panel ID="Panel4"  runat="server" ShowBorder="false"  Layout="VBox"  Height="50px"  
                            ShowHeader="false">
                        <Items>  
                              <f:Label ID="lbVevion" runat="server" MarginLeft="50px" ToolTip="当前软件运行环境要求及当前系统版本。" Width="800px">                                 
                                </f:Label> 
                        </Items>
                  </f:Panel>--%>
    </form>    
</body>
</html>