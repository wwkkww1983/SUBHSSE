<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonStatistic.aspx.cs"
    Inherits="FineUIPro.Web.SitePerson.PersonStatistic" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>现场人员统计</title>
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
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>            
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="现场人员统计" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="Number" AllowCellEditing="true" ClicksToEdit="2"
                DataIDField="Number" AllowSorting="true" SortField="Number" SortDirection="ASC"  EnableColumnLines="true"
                EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DatePicker ID="txtStartDate" runat="server" Label="开始日期">
                            </f:DatePicker>
                            <f:DatePicker ID="txtEndDate" runat="server" Label="结束日期">
                            </f:DatePicker>
                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                            <f:Button ID="btnSearch" runat="server" Icon="BrickMagnify" ToolTip="查询" OnClick="btnSearch_Click">
                            </f:Button>
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
                            <asp:Label ID="labNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="140px" ColumnID="UnitName" DataField="UnitName" ExpandUnusedSpace="true"
                        SortField="UnitName" FieldType="String" HeaderText="单位名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="220px" ColumnID="WorkAreaName" DataField="WorkAreaName"
                        SortField="WorkAreaName" FieldType="String" HeaderText="区域名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="220px" ColumnID="WorkPostName" DataField="WorkPostName"
                        SortField="WorkPostName" FieldType="String" HeaderText="岗位名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="PersonCount" DataField="PersonCount"
                        SortField="PersonCount" FieldType="Int" HeaderText="人数" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <%-- <f:TemplateField Width="120px" HeaderText="总人数" HeaderTextAlign="Center" TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("PersonCount") %>' ToolTip='<%#Bind("PersonCount") %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>--%>
                </Columns>
            </f:Grid>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
