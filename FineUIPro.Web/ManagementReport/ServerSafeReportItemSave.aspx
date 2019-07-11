<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServerSafeReportItemSave.aspx.cs" Inherits="FineUIPro.Web.ManagementReport.ServerSafeReportItemSave" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" EnableTableStyle="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
             <f:FormRow>
                <Items>
                   <f:TextBox ID="txtSafeReportName" runat="server" Label="标题" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>           
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpProjects" runat="server" Label="项目" 
                            EnableEdit="true" EnableCheckBoxSelect="true" EnableMultiSelect="true">
                     </f:DropDownList>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpUnits" runat="server" Label="分公司" 
                            EnableEdit="true" EnableCheckBoxSelect="true" EnableMultiSelect="true">
                     </f:DropDownList>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
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
