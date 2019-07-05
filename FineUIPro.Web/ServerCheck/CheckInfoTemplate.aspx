<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="CheckInfoTemplate.aspx.cs" Inherits="FineUIPro.Web.ServerCheck.CheckInfoTemplate" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <title></title>
     <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>           
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="2px" IconFont="PlusCircle" Title="管理办法"
                TitleToolTip="管理办法" AutoScroll="true">  
                <Toolbars>     
                    <f:Toolbar ID="Toolbar1" runat="server" ToolbarAlign="Right">
                        <Items>                                   
                            <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>   
                             <f:Button ID="imgbtnUpload" runat="server" Icon="DiskDownload" Text="下载"  
                                OnClick="imgbtnUpload_Click" EnableAjax="false"></f:Button>                                                            
                            <f:Button ID="btnClose" Icon="SystemClose" runat="server" Text="关闭" OnClick="btnClose_Click">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Items>
                    <f:SimpleForm ID="SimpleForm2" BodyPadding="5px" runat="server" LabelAlign="Top" EnableCollapse="true"
                        Title="表单" Width="850px"  ShowHeader="false">
                        <Items>
                             <f:HtmlEditor runat="server" Label="安全监督检查管理办法" ID="HtmlEditor1" ShowLabel="false"
                                Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="400px">
                                <Options>
                                    <f:OptionItem Key="lang" Value="en" />
                                    <f:OptionItem Key="toolbar"
                                        Value="['bold italic underline strikethrough |', 'insertorderedlist insertunorderedlist |', 'justifyleft justifycenter justifyright |', 'link unlink |', 'source|','print preview|','selectall fullscreen']"
                                        PersistOriginal="true" />
                                </Options>
                            </f:HtmlEditor>
                        </Items>
                    </f:SimpleForm>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>   
   </form>
    <script type="text/javascript">
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
           // F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
