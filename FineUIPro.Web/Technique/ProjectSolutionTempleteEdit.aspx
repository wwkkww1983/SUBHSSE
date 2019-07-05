<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSolutionTempleteEdit.aspx.cs"
    Inherits="FineUIPro.Web.Technique.ProjectSolutionTempleteEdit" ValidateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑现场施工方案模板</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="现场施工方案模板" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTempleteCode" runat="server" Label="编号" LabelAlign="Right" MaxLength="50"
                        Required="true" ShowRedStar="true" FocusOnPageLoad="true">
                    </f:TextBox>
                    <f:TextBox ID="txtTempleteName" runat="server" Label="名称" Required="true" ShowRedStar="true"
                        LabelAlign="Right" MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpTempleteType" runat="server" Label="类型" LabelAlign="Right"
                        EnableEdit="true" Required="true" ShowRedStar="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpCompileMan" runat="server" Label="编制人" LabelAlign="Right">
                    </f:DropDownList>
                    <f:DatePicker ID="txtCompileDate" runat="server" Label="编制日期" LabelAlign="Right"
                        EnableEdit="false">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="方案模板" ID="txtFileContents" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="400" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
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
