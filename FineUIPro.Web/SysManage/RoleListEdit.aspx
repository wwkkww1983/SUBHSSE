<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleListEdit.aspx.cs" Inherits="FineUIPro.Web.SysManage.RoleListEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑角色</title>
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
                     <f:TextBox ID="txtRoleName" runat="server" Label="角色名称" Required="true" ShowRedStar="true" MaxLength="50"
                         AutoPostBack="true" OnTextChanged="TextBox_TextChanged" FocusOnPageLoad="true">
                    </f:TextBox>
                     <f:DropDownList ID="drpRoleType" runat="server" Label="角色类型" EnableEdit="true"
                        ForceSelection="false" Required="true" ShowRedStar="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>           
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRoleCode" runat="server" Label="排列序号" MaxLength="50">
                    </f:TextBox>
                    <f:CheckBox  ID="chkIsAuditFlow" MarginLeft="40px" runat="server" Text="参与审批">
                    </f:CheckBox>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="85% 7% 8%">
                <Items>
                    <f:DropDownList ID="drpRole" runat="server" Label="可授权角色" EnableEdit="true" EnableMultiSelect="true"
                        ForceSelection="false" MaxLength="4000" EnableCheckBoxSelect="true">
                    </f:DropDownList>
                    <f:Button ID="SelectALL" runat="server" Text="全选" OnClick="SelectALL_Click"></f:Button>
                    <f:Button ID="SelectNoALL" runat="server" Text="全不选" 
                        OnClick="SelectNoALL_Click"></f:Button>
                </Items>
            </f:FormRow>  
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtDef" runat="server" Label="备注" MaxLength="100"></f:TextArea>
                </Items>
            </f:FormRow>                    
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click" Hidden="true">
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
