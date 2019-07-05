<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpertAudit.aspx.cs" Inherits="FineUIPro.Web.Technique.ExpertAudit"
    Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安全专家资源审核</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="安全专家" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="ExpertId" AllowCellEditing="true" EnableColumnLines="true"
                ClicksToEdit="2" DataIDField="ExpertId" AllowSorting="true" SortField="CompileDate"
                SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true"
                PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" EnableRowDoubleClickEvent="true"
                AllowFilters="true" OnFilterChange="Grid1_FilterChange" OnRowCommand="Grid1_RowCommand"
                EnableTextSelection="True">
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="90px" ColumnID="ExpertCode" DataField="ExpertCode" SortField="ExpertCode"
                        FieldType="String" HeaderText="编号" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="ExpertName" DataField="ExpertName" SortField="ExpertName"
                        FieldType="String" HeaderText="姓名" EnableFilter="true" TextAlign="Center">
                        <Filter EnableMultiFilter="true" ShowMatcher="true">
                            <Operator>
                                <f:DropDownList ID="DropDownList1" runat="server">
                                    <f:ListItem Text="等于" Value="equal" />
                                    <f:ListItem Text="包含" Value="contain" Selected="true" />
                                </f:DropDownList>
                            </Operator>
                        </Filter>
                    </f:RenderField>
                    <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="单位" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="106px" ColumnID="ExpertTypeName" DataField="ExpertTypeName"
                        FieldType="String" HeaderText="专家类别" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="PersonSpecialtyName" DataField="PersonSpecialtyName"
                        FieldType="String" HeaderText="专业" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="PostTitleName" DataField="PostTitleName" FieldType="String"
                        HeaderText="职称" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="EffectiveDate" DataField="EffectiveDate" SortField="EffectiveDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="资质有效期">
                    </f:RenderField>
                    <f:WindowField TextAlign="Left" Width="120px" WindowID="WindowAtt" Text="附件上传查看"
                        ToolTip="附件上传查看" DataIFrameUrlFields="ExpertId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/Expert&menuId=05495F29-B583-43D9-89D3-3384D6783A3F"
                         />
                    <f:TemplateField ColumnID="expander" RenderAsRowExpander="true">
                        <ItemTemplate>
                            <div class="expander">
                                <p>
                                    <strong>姓名：</strong><%# Eval("ExpertName")%>
                                </p>
                                <p>
                                    <strong>简介：</strong><%# Eval("Performance")%>
                                </p>
                            </div>
                        </ItemTemplate>
                    </f:TemplateField>
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
    <f:Window ID="Window1" Title="安全专家" Hidden="true" EnableIFrame="true" EnableMaximize="true"
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
