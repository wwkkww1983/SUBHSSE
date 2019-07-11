<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSafetyDataItemEdit.aspx.cs" 
    Inherits="FineUIPro.Web.SafetyData.ProjectSafetyDataItemEdit" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
     .fontred
        {
            color: #FF7575;
            background-image: none;
            font-size:small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageSafetyData1" AutoSizePanelID="SimpleForm1" runat="server"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
              <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCode" runat="server" Label="编号" Required="true" ShowRedStar="true" Readonly="true">
                    </f:TextBox> 
                    <f:TextBox ID="txtTitle" runat="server" Label="标题"  Required="true" ShowRedStar="true" FocusOnPageLoad="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtCompileDate" runat="server" Label="所属时段" LabelAlign="Right" Required="true" ShowRedStar="true"
                        EnableEdit="true" AutoPostBack="true" OnTextChanged="txtCompileDate_TextChanged">
                    </f:DatePicker>  
                     <f:TextBox ID="txtSubmitDate" runat="server" Label="提交日期"  Required="true" ShowRedStar="true" Readonly="true">
                    </f:TextBox>                   
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:Label ID="Label2" runat="server" CssClass="fontred" Label="说明" LabelAlign="right" LabelWidth="50px"></f:Label>
                        <f:HiddenField ID="hdRealStartDate" runat="server"></f:HiddenField>
                    <f:HiddenField ID="hdRealEndDate" runat="server"></f:HiddenField>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HtmlEditor runat="server" Label="文件内容" ID="txtFileContent" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="380" LabelAlign="Right">
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
                    <f:HiddenField ID="hdSortIndex" runat="server"></f:HiddenField>
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
