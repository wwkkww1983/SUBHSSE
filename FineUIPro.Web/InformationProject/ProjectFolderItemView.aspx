<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectFolderItemView.aspx.cs" 
    Inherits="FineUIPro.Web.InformationProject.ProjectFolderItemView" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
              <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCode" runat="server" Label="编号"  Readonly="true" >
                    </f:TextBox> 
                    <f:TextBox ID="txtTitle" runat="server" Label="标题"  Readonly="true" >
                    </f:TextBox>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="文件内容" ID="txtFileContent" ShowLabel="false" 
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="400" LabelAlign="Right">
                    </f:HtmlEditor>
                </Items>
            </f:FormRow>       
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                     <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"> </f:ToolbarFill>                  
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
