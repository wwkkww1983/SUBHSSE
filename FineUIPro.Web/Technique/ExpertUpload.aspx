<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpertUpload.aspx.cs" Inherits="FineUIPro.Web.Technique.ExpertUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全专家资源上传</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .black
        {
            color: Black;
            font-weight: bold;
        }
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
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="200" Title="上传列表" TitleToolTip="上传列表" ShowBorder="true"
                ShowHeader="false" AutoScroll="true" BodyPadding="0 2 0 0" Layout="Fit" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Tree ID="tvUploadResources" Width="220px" EnableCollapse="true" ShowHeader="false"
                        Title="上传列表" OnNodeCommand="tvUploadResources_NodeCommand" AutoLeafIdentification="true"
                        runat="server" EnableIcons="true" AutoScroll="true" EnableSingleClickExpand="true">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                ShowHeader="false" BodyPadding="20px" IconFont="PlusCircle" Title="资源明细" TitleToolTip="资源明细"
                AutoScroll="true">
                <Items>
                    <f:Panel ID="Panel" runat="server" ShowBorder="false" ShowHeader="false" Layout="HBox"
                        BoxConfigAlign="StretchMax">
                        <Items>
                            <f:Panel ID="Panel2" Title="面板1" BoxFlex="5" MarginRight="5px" runat="server" ShowBorder="false"
                                Width="200px" ShowHeader="false">
                                <Items>
                                    <f:TextBox ID="tbxExpertName" runat="server" Label="姓名" ShowRedStar="true" Required="true"
                                        MaxLength="50" LabelAlign="Right">
                                    </f:TextBox>
                                    <f:DropDownList ID="ddlSex" Label="性别" runat="server" ShowRedStar="true" Required="true"
                                        CompareMessage="性别不能为空！" LabelAlign="Right">
                                    </f:DropDownList>
                                    <f:DatePicker ID="dpBirthDay" Label="出生日期" EmptyText="请选择日期" runat="server" EnableEdit="true"
                                        ShowRedStar="true" Required="true" LabelAlign="Right">
                                    </f:DatePicker>
                                    <f:NumberBox ID="tbxAge" runat="server" Label="年龄" Required="true" ShowRedStar="true"
                                        CompareMessage="年龄不能为空！" LabelAlign="Right">
                                    </f:NumberBox>
                                    <f:DropDownList ID="ddlMarriage" Label="婚姻状况" runat="server" MaxLength="20" LabelAlign="Right">
                                    </f:DropDownList>
                                    <f:DropDownList ID="ddlNation" Label="民族" runat="server" MaxLength="20" LabelAlign="Right">
                                    </f:DropDownList>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel4" runat="server" BoxFlex="5" ShowBorder="false" ShowHeader="false"
                                Width="200px" MarginRight="5px" Layout="VBox">
                                <Items>
                                    <f:TextBox ID="tbxExpertCode" runat="server" Label="编号" ShowRedStar="true" Required="true"
                                        MaxLength="50" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" LabelAlign="Right">
                                    </f:TextBox>
                                    <f:TextBox ID="tbxIdentityCard" runat="server" Label="身份证号" MaxLength="50" LabelAlign="Right">
                                    </f:TextBox>
                                    <f:TextBox ID="tbxEmail" runat="server" Label="邮箱" MaxLength="100" LabelAlign="Right">
                                    </f:TextBox>
                                    <f:TextBox ID="tbxTelephone" runat="server" Label="手机号" MaxLength="50" LabelAlign="Right">
                                    </f:TextBox>
                                    <f:DropDownList ID="ddlEducation" Label="文化程度" runat="server" MaxLength="20" LabelAlign="Right">
                                    </f:DropDownList>
                                    <f:TextBox ID="tbxHometown" runat="server" Label="籍贯" MaxLength="50" LabelAlign="Right">
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
                                    <f:DatePicker runat="server" Label="资质有效期" ID="txtEffectiveDate" LabelAlign="Right">
                                    </f:DatePicker>
                                    <f:FileUpload runat="server" ID="filePhoto" EmptyText="请选择照片" Label="照片" LabelAlign="Right">
                                    </f:FileUpload>
                                    <f:Button ID="btnPhoto" runat="server" OnClick="btnPhoto_Click" Icon="GroupAdd" ToolTip="确认" Hidden="true">
                                    </f:Button>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtUnit" runat="server" Label="单位" LabelAlign="Right">
                                    </f:TextBox>
                                    <f:DropDownList ID="ddlExpertType" Label="专家类别" EnableEdit="true" ForceSelection="false"
                                        runat="server" LabelAlign="Right">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DropDownList ID="ddlPersonSpecialty" Label="专业" EnableEdit="true" ForceSelection="false"
                                        runat="server" LabelAlign="Right">
                                    </f:DropDownList>
                                    <f:DropDownList ID="ddlPostTitle" Label="职称" EnableEdit="true" ForceSelection="false"
                                        runat="server" LabelAlign="Right">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtPerformance" runat="server" Label="个人简历" MaxLength="2000" LabelAlign="Right">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lbAttachUrl" CssClass="labcenter">
                                    </f:Label>
                                    <f:Toolbar ID="Toolbar2" runat="server" ToolbarAlign="Right" Position="Bottom">
                                        <Items>
                                            <f:Button ID="btnUploadResources" Text="上传附件" Icon="SystemNew" runat="server" OnClick="btnUploadResources_Click"
                                                ValidateForms="SimpleForm1">
                                            </f:Button>
                                        </Items>
                                    </f:Toolbar>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtCompileMan" runat="server" Label="整理人" LabelAlign="Right">
                                    </f:Label>
                                    <f:Label runat="server" Label="整理时间" ID="txtCompileDate" LabelAlign="Right">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                        <Items>
                            <f:Button ID="btnNew" Icon="Add" ToolTip="新增" runat="server" OnClick="btnNew_Click"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" Hidden="true"
                                OnClick="btnDelete_Click" runat="server">
                            </f:Button>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server" ValidateForms="SimpleForm1"
                                Hidden="true" OnClick="btnSave_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
</body>
</html>
