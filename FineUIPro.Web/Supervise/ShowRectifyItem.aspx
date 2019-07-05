<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowRectifyItem.aspx.cs"
    Inherits="FineUIPro.Web.Supervise.ShowRectifyItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>安全隐患</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="250" Title="安全隐患" TitleToolTip="安全隐患" ShowBorder="true"
                ShowHeader="true" AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Tree ID="trRectify" Width="200px" EnableCollapse="true" ShowHeader="true" Title="作业类别"
                        OnNodeCommand="trRectify_NodeCommand" AutoLeafIdentification="true" runat="server">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="安全隐患明细" TitleToolTip="安全隐患明细"
                Layout="VBox">
                <Items>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="RectifyItemId" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="RectifyItemId" AllowSorting="true" SortField="RectifyName"
                        SortDirection="ASC" OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand" AllowPaging="true"
                        IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                        AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:Button ID="btnSave" ToolTip="确认" Icon="Pencil" runat="server" OnClick="btnSave_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:CheckBoxField ColumnID="ckbIsSelected" Width="70px" RenderAsStaticField="false"
                                AutoPostBack="true" CommandName="IsSelected" HeaderText="选择" HeaderTextAlign="Center" />
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:RenderField Width="120px" ColumnID="RectifyName" DataField="RectifyName" SortField="RectifyName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="作业类别">
                            </f:RenderField>
                            <f:TemplateField Width="180px" HeaderText="隐患源点" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("HazardSourcePoint") %>' ToolTip='<%#Bind("HazardSourcePoint") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="200px" HeaderText="风险分析" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("RiskAnalysis") %>' ToolTip='<%#Bind("RiskAnalysis") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="200px" HeaderText="风险防范" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("RiskPrevention") %>' ToolTip='<%#Bind("RiskPrevention") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField Width="200px" HeaderText="同类风险" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("SimilarRisk") %>' ToolTip='<%#Bind("SimilarRisk") %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                        </Columns>
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
        </Items>
    </f:Panel>
    </form>
</body>
</html>
