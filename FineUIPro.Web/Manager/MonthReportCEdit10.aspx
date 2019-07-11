<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCEdit10.aspx.cs" Inherits="FineUIPro.Web.Manager.MonthReportCEdit10" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server">
    </f:PageManager>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel60" Layout="Anchor" Title="10.项目现场影像照片"
                        runat="server">
                        <Items>
                            <f:HtmlEditor runat="server" Label="" ID="txtPhotoContents" ShowLabel="false"
                            Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="420" LabelAlign="Right">
                        </f:HtmlEditor>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
