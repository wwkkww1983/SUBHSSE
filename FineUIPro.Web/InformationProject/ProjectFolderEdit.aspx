<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectFolderEdit.aspx.cs" Inherits="FineUIPro.Web.InformationProject.ProjectFolderEdit" %>

<!DOCTYPE html>

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
                    <f:TextBox ID="txtTitle" runat="server" Label="文件夹名称" LabelWidth="120px" Required="true" ShowRedStar="true" MaxLength="50" FocusOnPageLoad="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCode" runat="server" Label="文件夹编码" LabelWidth="120px" MaxLength="50"  Required="true" ShowRedStar="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>      
            <f:FormRow >                
                <Items>
                    <f:CheckBox  ID="chkIsEndLevel"  runat="server" Label="是否末级" LabelWidth="120px" LabelAlign="Right">
                    </f:CheckBox>
                </Items>
            </f:FormRow>    
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
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
