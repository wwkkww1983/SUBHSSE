﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonListEdit.aspx.cs" Inherits="FineUIPro.Web.SitePerson.PersonListEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑人员信息</title>
    <style type="text/css">
        .userphoto .f-field-label
        {
            margin-top: 0;
        }
        
        .userphoto img
        {
            width: 100%;
        }
        
        .uploadbutton .f-btn
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Form ID="Form2" LabelAlign="Right" MessageTarget="Qtip" RedStarPosition="BeforeText"
        LabelWidth="90px" BodyPadding="5px" ShowBorder="false" ShowHeader="false" runat="server"
        AutoScroll="true" EnableCollapse="true">
        <Items>
            <f:Panel ID="Panel2" runat="server" ShowBorder="false" ShowHeader="false" Layout="HBox"
                BoxConfigAlign="StretchMax">
                <Items>
                    <f:Panel ID="Panel1" Title="面板1" BoxFlex="5" MarginRight="5px" runat="server" ShowBorder="false"
                        ShowHeader="false">
                        <Items>
                            <f:TextBox ID="txtPersonName" runat="server" Label="人员姓名" MaxLength="200" LabelAlign="Right"
                                Required="True" ShowRedStar="True" FocusOnPageLoad="true">
                            </f:TextBox>
                            <f:TextBox ID="txtIdentityCard" runat="server" Label="身份证号" MaxLength="50" LabelAlign="Right"
                               AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                            </f:TextBox>
                           <%-- <f:DropDownList ID="drpUnit" runat="server" Label="所属单位" LabelAlign="Right" Required="True"
                                ShowRedStar="True" AutoPostBack="True" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                            </f:DropDownList>--%>
                            <f:TextBox ID="txtUnitName" runat="server" Label="所属单位" LabelAlign="Right" Readonly="true" Required="True" ShowRedStar="True"></f:TextBox>
                            <f:DropDownList ID="drpTeamGroup" runat="server" Label="所属班组" LabelAlign="Right">
                            </f:DropDownList>                           
                            <f:TextBox ID="txtAddress" runat="server" Label="家庭地址" MaxLength="50" LabelAlign="Right">
                            </f:TextBox>
                            <f:DropDownList ID="drpPosition" runat="server" Label="所属职务" LabelAlign="Right" EnableEdit="true">
                            </f:DropDownList>
                            <f:DropDownList ID="drpTitle" runat="server" Label="所属职称" LabelAlign="Right" EnableEdit="true">
                            </f:DropDownList>
                        </Items>
                    </f:Panel>
                    <f:Panel ID="Panel4" runat="server" BoxFlex="3" ShowBorder="false" ShowHeader="false"
                        MarginRight="5px" Layout="VBox">
                        <Items>
                            <f:TextBox ID="txtCardNo" runat="server" Label="卡号" MaxLength="50" LabelAlign="Right" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                            </f:TextBox>
                            <f:Button runat="server" ID="btnReadIdentityCard" Icon="Vcard" Text="读取身份证" MarginLeft="90px"></f:Button>
                            <f:RadioButtonList ID="rblSex" runat="server" Label="性别" LabelAlign="Right" Required="True" ShowRedStar="True">
                                <f:RadioItem Value="1" Text="男" Selected="true" />
                                <f:RadioItem Value="2" Text="女" />
                            </f:RadioButtonList>
                            <f:DropDownList ID="drpPost" runat="server" Label="所属岗位" LabelAlign="Right" Required="True" ShowRedStar="True" EnableEdit="true">
                            </f:DropDownList>
                            <f:DropDownList ID="drpWorkArea" runat="server" Label="作业区域" LabelAlign="Right" EnableEdit="true">
                            </f:DropDownList>
                            <f:TextBox ID="txtTelephone" runat="server" Label="电话" LabelAlign="Right" MaxLength="50">
                            </f:TextBox>
                            <f:RadioButtonList ID="rblIsUsed" runat="server" Label="人员在场" LabelAlign="Right" Required="True" ShowRedStar="True">
                                <f:RadioItem Value="True" Text="是" />
                                <f:RadioItem Value="False" Text="否" />
                            </f:RadioButtonList>
                        </Items>
                    </f:Panel>
                    <f:Panel ID="Panel5" Title="面板1" BoxFlex="2" runat="server" ShowBorder="false" ShowHeader="false"
                        Layout="VBox">
                        <Items>
                            <f:Image ID="imgPhoto" CssClass="userphoto" ImageUrl="~/res/images/blank_150.png"
                                runat="server" BoxFlex="1">
                            </f:Image>
                            <f:FileUpload ID="filePhoto" CssClass="uploadbutton" runat="server" ButtonText="上传照片"
                                ButtonOnly="true" AutoPostBack="true" OnFileSelected="filePhoto_FileSelected">
                            </f:FileUpload>
                        </Items>
                    </f:Panel>
                </Items>
            </f:Panel>
            <f:Form ID="Form7" ShowBorder="false" ShowHeader="false" runat="server">
                <Rows>
                    <f:FormRow>
                        <Items>
                            <f:DropDownList ID="drpCertificate" runat="server" Label="特岗证书" LabelAlign="Right" EnableEdit="true">
                            </f:DropDownList>
                            <f:TextBox ID="txtCertificateCode" runat="server" Label="证书编号" LabelAlign="Right">
                            </f:TextBox>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:DatePicker ID="txtCertificateLimitTime" runat="server" Label="证书有效期" LabelAlign="Right">
                            </f:DatePicker>
                            <f:RadioButtonList ID="rblIsCardUsed" runat="server" Label="考勤卡启用" LabelAlign="Right" Required="True" ShowRedStar="True">
                                <f:RadioItem Value="True" Text="是" />
                                <f:RadioItem Value="False" Text="否" />
                            </f:RadioButtonList>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:DatePicker ID="txtInTime" runat="server" Label="入场时间" LabelAlign="Right" ShowRedStar="true">
                            </f:DatePicker>
                            <f:DatePicker ID="txtOutTime" runat="server" Label="出场时间" LabelAlign="Right">
                            </f:DatePicker>
                        </Items>
                    </f:FormRow>
                    <f:FormRow ColumnWidths="100%">
                        <Items>
                            <f:TextBox ID="txtOutResult" runat="server" Label="出场原因" LabelAlign="Right">
                            </f:TextBox>
                        </Items>
                    </f:FormRow>
                </Rows>
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" runat="server" ToolbarAlign="Right" Position="Bottom">
                        <Items>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="Form2"
                                OnClick="btnSave_Click">
                            </f:Button>
                            <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Form>
        </Items>
    </f:Form>
    </form>
</body>
</html>
