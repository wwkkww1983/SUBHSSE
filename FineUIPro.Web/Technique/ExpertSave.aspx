<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpertSave.aspx.cs" Inherits="FineUIPro.Web.Technique.ExpertSave"
    Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .userphoto .f-field-label
        {
            margin-top: 0;
        }
        
        .userphoto img
        {
            width: 150px;
            height: 180px;
        }
        
        .uploadbutton .f-btn
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Panel ID="Panel22" runat="server" ShowBorder="false" ShowHeader="false">
        <Items>
            <f:Form ID="SimpleForm1" LabelAlign="Right" MessageTarget="Qtip" RedStarPosition="BeforeText"
                LabelWidth="90px" BodyPadding="5px" ShowBorder="false" ShowHeader="false" runat="server">
                <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server" ToolbarAlign="Right" Position="Bottom">
                    <Items>
                        <f:Label runat="server" ID="lbTemp"></f:Label>
                         <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server" OnClick="btnUploadResources_Click"
                            ValidateForms="SimpleForm1">
                        </f:Button>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                            Hidden="true" OnClick="btnSave_Click">
                        </f:Button>
                        <f:Button ID="btnSaveUp" Icon="PageSave" runat="server" ToolTip="保存并上报" ValidateForms="SimpleForm1"
                            Hidden="true" OnClick="btnSaveUp_Click">
                        </f:Button>
                        <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
                <Items>
                    <f:Panel ID="Panel2" runat="server" ShowBorder="false" ShowHeader="false" Layout="HBox"
                        BoxConfigAlign="StretchMax">
                        <Items>
                            <f:Panel ID="Panel1" Title="面板1" BoxFlex="5" MarginRight="5px" runat="server" ShowBorder="false"
                                Width="200px" ShowHeader="false">
                                <Items>
                                    <f:TextBox ID="tbxExpertName" runat="server" Label="姓名" ShowRedStar="true" Required="true">
                                    </f:TextBox>
                                    <f:DropDownList ID="ddlSex" Label="性别" runat="server" ShowRedStar="true" Required="true"
                                        CompareMessage="性别不能为空！">
                                    </f:DropDownList>
                                    <f:DatePicker ID="dpBirthDay" Label="出生日期" EmptyText="请选择日期" runat="server" EnableEdit="true"
                                        ShowRedStar="true" Required="true">
                                    </f:DatePicker>
                                    <f:NumberBox ID="tbxAge" runat="server" Label="年龄" Required="true" ShowRedStar="true"
                                        CompareMessage="年龄不能为空！">
                                    </f:NumberBox>
                                    <f:DropDownList ID="ddlMarriage" Label="婚姻状况" runat="server">
                                    </f:DropDownList>
                                    <f:DropDownList ID="ddlNation" Label="民族" runat="server">
                                    </f:DropDownList>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel4" runat="server" BoxFlex="5" ShowBorder="false" ShowHeader="false"
                                Width="200px" MarginRight="5px" Layout="VBox">
                                <Items>
                                    <f:TextBox ID="tbxExpertCode" runat="server" Label="编号" ShowRedStar="true" Required="true"
                                        AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                                    </f:TextBox>
                                    <f:TextBox ID="tbxIdentityCard" runat="server" Label="身份证号">
                                    </f:TextBox>
                                    <f:TextBox ID="tbxEmail" runat="server" Label="邮箱">
                                    </f:TextBox>
                                    <f:TextBox ID="tbxTelephone" runat="server" Label="手机号">
                                    </f:TextBox>
                                    <f:DropDownList ID="ddlEducation" Label="文化程度" runat="server">
                                    </f:DropDownList>
                                    <f:TextBox ID="tbxHometown" runat="server" Label="籍贯">
                                    </f:TextBox>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel5" Title="面板1" BoxFlex="3" runat="server" ShowBorder="false" ShowHeader="false"
                                Layout="VBox">
                                <Items>
                                    <f:Image ID="Image1" CssClass="userphoto" ImageUrl="~/res/images/blank_180.png" runat="server"
                                        BoxFlex="1" Width="180px" Height="150px">
                                    </f:Image>
                                </Items>
                            </f:Panel>
                        </Items>
                    </f:Panel>
                    <f:Form ID="Form7" ShowBorder="false" ShowHeader="false" runat="server">
                        <Rows>
                            <f:FormRow ColumnWidths="50% 40% 10%">
                                <Items>
                                    <f:DatePicker runat="server" Label="资质有效期" ID="txtEffectiveDate">
                                    </f:DatePicker>
                                    <f:FileUpload runat="server" ID="filePhoto" EmptyText="请选择照片" Label="照片">
                                    </f:FileUpload>
                                    <f:Button ID="btnPhoto" runat="server" OnClick="btnPhoto_Click" Icon="GroupAdd" ToolTip="确认">
                                    </f:Button>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtUnit" runat="server" Label="单位">
                                    </f:TextBox>
                                    <f:DropDownList ID="ddlExpertType" Label="专家类别" EnableEdit="true" ForceSelection="false"
                                        runat="server">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DropDownList ID="ddlPersonSpecialty" Label="专业" EnableEdit="true" ForceSelection="false"
                                        runat="server">
                                    </f:DropDownList>
                                    <f:DropDownList ID="ddlPostTitle" Label="职称" EnableEdit="true" ForceSelection="false"
                                        runat="server">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtPerformance" runat="server" Label="个人简历">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Form>
        </Items>        
    </f:Panel>
    <br />
    <br />
    </form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
</body>
</html>
