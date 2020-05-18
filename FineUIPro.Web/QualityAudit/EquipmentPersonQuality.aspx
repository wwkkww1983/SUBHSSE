<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipmentPersonQuality.aspx.cs" Inherits="FineUIPro.Web.QualityAudit.EquipmentPersonQuality" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>特种设备作业人员资质</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="10px" BodyPadding="10px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch" AutoScroll="true">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="特种设备作业人员资质" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="PersonId"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="PersonId" AllowSorting="true"
                SortField="UnitCode,WorkPostCode,PersonName" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" EnableTextSelection="True" EnableSummary="true" SummaryPosition="Flow">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:TextBox runat="server" Label="编号" ID="txtCardNo" EmptyText="输入查询条件"
                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px"
                                LabelAlign="right">
                            </f:TextBox>
                            <f:DropDownList ID="drpUnitId" runat="server" Label="单位" AutoPostBack="true" OnSelectedIndexChanged="TextBox_TextChanged"  LabelWidth="70px" Width="250px">
                            </f:DropDownList>
                            <f:TextBox runat="server" Label="人员姓名" ID="txtPersonName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px" LabelAlign="right">
                            </f:TextBox>
                            <f:TextBox runat="server" Label="岗位" ID="txtWorkPostName" EmptyText="输入查询条件" AutoPostBack="true"
                                OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px" LabelAlign="right">
                            </f:TextBox>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>                                                     
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="100px" ColumnID="UnitCode" DataField="UnitCode"
                        SortField="UnitCode" FieldType="String" HeaderText="单位代码" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName"
                        SortField="UnitName" FieldType="String" HeaderText="单位名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField Width="90px" ColumnID="PersonName" DataField="PersonName" 
                        SortField="PersonName" FieldType="String" HeaderText="人员姓名" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>  
                    <f:RenderField Width="100px" ColumnID="WorkPostName" DataField="WorkPostName" 
                        SortField="WorkPostName" FieldType="String" HeaderText="岗位" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="CertificateName" DataField="CertificateName"
                         SortField="CertificateName" FieldType="String" HeaderText="特岗证书"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="180px" ColumnID="CertificateNo" DataField="CertificateNo" 
                        SortField="CertificateNo" FieldType="String" HeaderText="证书编号" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="SendDate" DataField="SendDate" SortField="SendDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="发证时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="LimitDate" DataField="LimitDate" SortField="LimitDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="有效期至"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="220px" ColumnID="SendUnit" DataField="SendUnit" 
                        SortField="SendUnit" FieldType="String" HeaderText="发证单位" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="AuditorName" DataField="AuditorName" 
                        SortField="AuditorName" FieldType="String" HeaderText="审核人" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="AuditDate" DataField="AuditDate" SortField="AuditDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="审核时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                 <%--   <f:RenderField Width="100px" ColumnID="Grade" DataField="Grade" 
                        SortField="Grade" FieldType="String" HeaderText="操作类别" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>--%>
                    
                    <f:WindowField TextAlign="Center" Width="80px" WindowID="WindowAtt" HeaderText="扫描件"
                        Text="查看" ToolTip="附件查看" DataIFrameUrlFields="EquipmentPersonQualityId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/EquipmentPersonQualityAttachUrl&menuId=EBEA762D-1F46-47C5-9EAD-759E13D9B41C&type=-1"
                        HeaderTextAlign="Center" ColumnID="attWindow" />
                </Columns>
                <Listeners>
                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                </Listeners>
                <PageItems>
                    <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                    </f:ToolbarSeparator>
                    <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                    </f:ToolbarText>
                    <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <f:ListItem Text="10" Value="10" />
                        <f:ListItem Text="15" Value="15" />
                        <f:ListItem Text="20" Value="20" />
                        <f:ListItem Text="25" Value="25" />
                        <f:ListItem Text="所有行" Value="10000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="特种设备作业人员资质" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1000px"
        Height="650px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="附件页面" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" Icon="TableEdit" EnablePostBack="true"
            Hidden="true" runat="server" Text="编辑">
        </f:MenuButton>      
    </f:Menu>
    </form>
    <script type="text/javascript">
        var menuID = '<%= Menu1.ClientID %>';
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
