<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialSchemeAudit.aspx.cs"
    Inherits="FineUIPro.Web.Technique.SpecialSchemeAudit" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>专项方案资源审核</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="专项方案" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="SpecialSchemeId" AllowCellEditing="true"
                EnableColumnLines="true" ClicksToEdit="2" DataIDField="SpecialSchemeId" AllowSorting="true"
                SortField="CompileDate" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                EnableRowDoubleClickEvent="true" AllowFilters="true" OnFilterChange="Grid1_FilterChange"
                OnRowCommand="Grid1_RowCommand" EnableTextSelection="True">
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="110px" ColumnID="SpecialSchemeCode" DataField="SpecialSchemeCode"
                        SortField="SpecialSchemeCode" FieldType="String" HeaderText="编号" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="150px" ColumnID="SpecialSchemeName" DataField="SpecialSchemeName"
                        SortField="SpecialSchemeName" FieldType="String" HeaderText="名称" HeaderTextAlign="Center"
                        EnableFilter="true" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="SpecialSchemeTypeName" DataField="SpecialSchemeTypeName"
                        SortField="SpecialSchemeTypeName" FieldType="String" HeaderText="类型" HeaderTextAlign="Center"
                        TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="190px" ColumnID="Summary" DataField="Summary" SortField="Summary"
                        FieldType="String" HeaderText="摘要" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:WindowField TextAlign="Left" Width="120px" WindowID="WindowAtt" Text="附件上传查看"
                        ToolTip="附件上传查看" DataIFrameUrlFields="SpecialSchemeId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/SpecialScheme&menuId=3E2F2FFD-ED2E-4914-8370-D97A68398814"
                         />
                    <f:RenderField Width="90px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                        FieldType="String" HeaderText="整理人" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="CompileDate" DataField="CompileDate" SortField="CompileDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整理日期"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
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
                        <f:ListItem Text="所有行" Value="100000" />
                    </f:DropDownList>
                </PageItems>
            </f:Grid>
        </Items>
    </f:Panel>
    <f:Window ID="Window1" Title="专项方案" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="760px" Height="380px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="670px"
        Height="460px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnPass" OnClick="btnPass_Click" EnablePostBack="true" runat="server"
            Hidden="true" Text="采用">
        </f:MenuButton>
        <f:MenuButton ID="btnUpPass" OnClick="btnUpPass_Click" EnablePostBack="true" runat="server"
            Hidden="true" Text="采用并上报">
        </f:MenuButton>
        <f:MenuButton ID="btnNoPass" OnClick="btnNoPass_Click" EnablePostBack="true" runat="server"
            Hidden="true" Text="不采用">
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
    </script>
</body>
</html>
