<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonListThisUnitEdit.aspx.cs" Inherits="FineUIPro.Web.SitePerson.PersonListThisUnitEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>人员信息</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="人员信息" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtPersonName" runat="server" Label="人员姓名" MaxLength="200" LabelAlign="Right"
                                Required="True" ShowRedStar="True" FocusOnPageLoad="true">
                            </f:TextBox>
                    <f:RadioButtonList ID="rblSex" runat="server" Label="性别" LabelAlign="Right" Required="True" ShowRedStar="True">
                                <f:RadioItem Value="1" Text="男" Selected="true" />
                                <f:RadioItem Value="2" Text="女" />
                            </f:RadioButtonList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtUnitName" runat="server" Label="所属单位" LabelAlign="Right" Readonly="true" Required="True" ShowRedStar="True"></f:TextBox>
                    <f:DropDownList ID="drpPost" runat="server" Label="所属岗位" LabelAlign="Right" Required="True" ShowRedStar="True" EnableEdit="true">
                            </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpDepart" runat="server" Label="所属部门" LabelAlign="Right" Required="True" ShowRedStar="True" EnableEdit="true">
                    </f:DropDownList>
                    <f:TextBox ID="txtTelephone" runat="server" Label="电话" LabelAlign="Right" MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
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
    </form>
</body>
</html>
