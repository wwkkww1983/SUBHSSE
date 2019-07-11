<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonPunishRecordSearch.aspx.cs" Inherits="FineUIPro.Web.SitePerson.PersonPunishRecordSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>扣分记录查询</title>
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
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1"
                        DataIDField="PunishRecordId" DataKeyNames="PunishRecordId" EnableMultiSelect="false"
                        ShowGridHeader="true" Height="400px" EnableColumnLines="true" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                        EnableSummary="true" SummaryPosition="Flow">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:DropDownList ID="drpUnit" Label="单位" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpUnit_TextChanged"
                                        EnableEdit="true" LabelWidth="60px" Width="300px">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpPerson" Label="人员" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpPerson_TextChanged"
                                        EnableEdit="true" LabelWidth="60px" Width="200px">
                                    </f:DropDownList>
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" Text="导出" Icon="TableGo"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:TemplateField ColumnID="tfNumber" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField HeaderText="卡号" ColumnID="CardNo" DataField="CardNo" SortField="CardNo"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="PersonName" DataField="PersonName" SortField="PersonName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="人员姓名">
                            </f:RenderField>
                            <f:RenderField HeaderText="岗位名称" ColumnID="PostName" DataField="PostName" SortField="PostName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="120px">
                            </f:RenderField>
                            <f:RenderField HeaderText="单位名称" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="190px">
                            </f:RenderField>
                            <f:RenderField HeaderText="所在班组" ColumnID="TeamGroupName" DataField="TeamGroupName"
                                SortField="TeamGroupName" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="140px">
                            </f:RenderField>
                            <f:RenderField HeaderText="处罚内容" ColumnID="PunishItemContent" DataField="PunishItemContent"
                                SortField="PunishItemContent" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="200px">
                            </f:RenderField>
                            <f:RenderField HeaderText="处罚原因" ColumnID="PunishReason" DataField="PunishReason"
                                SortField="PunishReason" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="150px">
                            </f:RenderField>
                            <f:RenderField HeaderText="扣分" ColumnID="Deduction" DataField="Deduction" SortField="Deduction"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="70px">
                            </f:RenderField>
                            <f:RenderField HeaderText="罚款" ColumnID="PunishMoney" DataField="PunishMoney" SortField="PunishMoney"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="70px">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="PunishDate" DataField="PunishDate" SortField="PunishDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="处罚日期"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Window ID="Window1" Title="编辑扣分记录明细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="600px" Height="420px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <%--<f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true" 
            Icon="Pencil" runat="server" Text="编辑">
        </f:MenuButton>--%>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="确定删除当前数据？" ConfirmTarget="Parent" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script>
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
    </script>
</body>
</html>
