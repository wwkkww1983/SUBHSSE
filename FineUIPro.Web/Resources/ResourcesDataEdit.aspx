﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResourcesDataEdit.aspx.cs" Inherits="FineUIPro.Web.Resources.ResourcesDataEdit"   ValidateRequest="false"%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑参考资料</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow ColumnWidths="35% 65%">
                <Items>
                    <f:TextBox ID="txtFileCode" runat="server" Label="资料编号"  MaxLength="50" FocusOnPageLoad="true">
                    </f:TextBox>
                    <f:TextBox ID="txtFileName" runat="server" Label="资料名称" Required="true" ShowRedStar="true"  MaxLength="500">
                    </f:TextBox>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:TextBox ID="txtRemark" runat="server" Label="说明" MaxLength="500">
                    </f:TextBox>
                 </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="资料内容" ID="txtFileContent" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="350" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:FormRow> 
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="10px">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"> </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
     <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>