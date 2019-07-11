<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSafeReport.aspx.cs" Inherits="FineUIPro.Web.Manager.ProjectSafeReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全文件上报</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                Layout="Fit" EnableCollapse="true" Width="250" Title="安全文件上报" TitleToolTip="安全文件上报"
                ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Tree ID="trSafeReport" Width="200px" Title="安全文件上报" ShowHeader="false" OnNodeCommand="trSafeReport_NodeCommand"
                         AutoLeafIdentification="true" runat="server" AutoScroll="true" >
                    </f:Tree>
                </Items>                
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="安全文件明细"
                TitleToolTip="安全文件明细" AutoScroll="true">
                <Items> 
                     <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" EnableTableStyle="false"
                        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Hidden="true">
                        <Rows>
                             <f:FormRow ColumnWidths="45% 40% 15%"> 
                                <Items>
                                    <f:TextBox ID="txtSafeReportCode" runat="server" Label="编号"  Readonly="true">
                                    </f:TextBox>    
                                    <f:TextBox ID="txtRequestTime" runat="server" Label="要求上报时间" Readonly="true" LabelWidth="120px">
                                    </f:TextBox>
                                    <f:Button ID="btnTemplateView" Text="标准模板" Icon="TableCell" runat="server"
                                            OnClick="btnTemplateView_Click">
                                    </f:Button>
                                </Items>
                            </f:FormRow> 
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtSafeReportName" runat="server" Label="标题"  Readonly="true">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextArea runat="server" ID="txtRequirement" Label="上报要求" Height="64px" Readonly="true"></f:TextArea>
                                </Items>
                            </f:FormRow>
                             <f:FormRow>
                                <Items>
                                    <f:TextArea runat="server" ID="txtReportContent" Label="上报内容" Height="64px" Required="true"
                                     MaxLength="4000" CssClass="result" FocusOnPageLoad="true" ShowRedStar="true"></f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DropDownList ID="drpReportManId" runat="server" Label="填报人" EnableEdit="true" ShowRedStar="true" Required="true">
                                    </f:DropDownList>
                                    <f:DatePicker ID="txtReportTime" runat="server" Label="填报时间" ShowRedStar="true" Required="true">
                                    </f:DatePicker>
                                    
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="txtUpReportTime" runat="server" Label="上报时间" Readonly="true">
                                    </f:Label>
                                   <f:Label ID="lbState" runat="server" Label="状态" Readonly="true">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                         <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                                <Items>
                                     <f:Label runat="server" ID="lbTemp">
                                    </f:Label>
                                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                                        OnClick="btnAttachUrl_Click">
                                    </f:Button>
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>                                   
                                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm2"
                                        OnClick="btnSave_Click">
                                    </f:Button>
                                     <f:Button ID="btnSubmit" Icon="PageSave" runat="server" ToolTip="保存并上报" ValidateForms="SimpleForm2"
                                        OnClick="btnSubmit_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                    </f:Form>                   
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
    <script type="text/javascript">       
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
