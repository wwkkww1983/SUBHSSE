<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContentSave.aspx.cs" Inherits="FineUIPro.Web.Exchange.ContentSave" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
                   <f:TextBox ID="txtTheme" runat="server" Label="主题" MaxLength="100" Required="true" ShowRedStar="true" FocusOnPageLoad="true"></f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                   <f:DropDownList ID="drpContentType" Label="话题类型" AutoPostBack="false" EnableSimulateTree="true" Required="true" ShowRedStar="true"
                        runat="server">
                    </f:DropDownList>
                </Items> 
            </f:FormRow>
            <f:FormRow>
                <Items>               
                <f:TextArea ID="txtContents" runat="server" MaxLength="500" Label="内容"></f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                   <f:TextBox ID="txtCompileMan" runat="server" Label="发帖人" Readonly="true"></f:TextBox>
                   <f:DatePicker runat="server" Label="发帖时间" ID="txtCompileDate" ></f:DatePicker>
                </Items>
            </f:FormRow>    
            <f:FormRow>
                <Items>
                   
                </Items>
            </f:FormRow>    
            <f:FormRow>
                 <Items>               
                    <f:FileUpload runat="server" ID="fuAttachUrl" EmptyText="请上传附件" Label="附件" >
                    </f:FileUpload>                             
                </Items>                                          
            </f:FormRow>                             
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnUploadResources" Text="附件" ToolTip="附件上传及查看" Icon="SystemNew" runat="server" OnClick="btnUploadResources_Click"
                        ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  Text="保存数据" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" Text="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                     <f:HiddenField ID="hdCompileMan" runat="server"></f:HiddenField>
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
