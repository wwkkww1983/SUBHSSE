<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowGoodsDef.aspx.cs" Inherits="FineUIPro.Web.ShowDialog.ShowGoodsDef" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查询物资类别</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="200" Title="物资类别" ShowBorder="true" ShowHeader="false"
                AutoScroll="true" BodyPadding="5px" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Tree ID="tvGoodsDef" KeepCurrentSelection="true" ShowHeader="false" OnNodeCommand="tvGoodsDef_NodeCommand"
                        runat="server" ShowBorder="false" EnableSingleClickExpand="true">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                Layout="VBox" ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="物资类别"
                AutoScroll="true">
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="GoodsDefId" AllowCellEditing="true" ClicksToEdit="2" EnableColumnLines="true"
                        DataIDField="GoodsDefId" AllowColumnLocking="true" EnableTextSelection="True">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>
                                    <f:HiddenField runat="server" ID="hdGoodsDefId">
                                    </f:HiddenField>
                                    <f:Button ID="btnSure" ToolTip="确定" Icon="Accept" runat="server" OnClick="btnSure_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:TemplateField HeaderText="选择" runat="server" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbGoodsDefId" runat="server" AutoPostBack="True" OnCheckedChanged="ckbGoodsDefId_CheckedChanged" />
                                    <asp:Label ID="lblGoodsDefId" runat="server" Text='<%# Bind("GoodsDefId") %>' Visible="False"></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>

                            <f:TemplateField ID="tfGoodsDefCode" HeaderText="物资类别" runat="server" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# ConvertGoodsDefCode(Eval("GoodsDefCode")) %>' ToolTip='<%# ConvertGoodsDefCode(Eval("GoodsDefCode")) %>' ></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>

                            <%--<f:RenderField ColumnID="GoodsDefCode" DataField="GoodsDefCode" SortField="GoodsDefCode"
                                FieldType="String" HeaderText="物资类别" HeaderTextAlign="Center" TextAlign="left"
                                ExpandUnusedSpace="true">
                            </f:RenderField>--%>
                            <f:RenderField ColumnID="GoodsDefName" DataField="GoodsDefName" SortField="GoodsDefName"
                                FieldType="String" HeaderText="物资名称" HeaderTextAlign="Center" TextAlign="left"
                                ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:TemplateField HeaderText="当前库存" TextAlign="Left" HeaderTextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="La1" runat="server" Text='<%# GetNum(Eval("GoodsDefId")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
