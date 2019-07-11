<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HazardRegisterTypesSupervisionEdit.aspx.cs" Inherits="FineUIPro.Web.BaseInfo.HazardRegisterTypesSupervisionEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑领导督查类型</title>
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
                    <f:TextBox ID="txtTypeCode" runat="server" Label="编号" MaxLength="100" FocusOnPageLoad="true" LabelWidth="120px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpGroupType" runat="server" Label="分类" LabelAlign="Right" ShowRedStar="true" Required="true" LabelWidth="120px">
                        <f:ListItem Text="常规内容" Value="1" />
                        <f:ListItem Text="专项内容" Value="2" />
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRegisterTypesName" runat="server" Label="工作重点内容" Required="true" LabelWidth="120px"
                        MaxLength="200" ShowRedStar="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRemark" runat="server" Label="检查方法" LabelWidth="120px"
                        MaxLength="200" >
                    </f:TextBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" Text="保存数据" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" Text="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
