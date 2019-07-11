<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlackList.aspx.cs" Inherits="FineUIPro.Web.SitePerson.BlackList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>黑名单</title>
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
                        DataIDField="PersonId" DataKeyNames="PersonId" EnableMultiSelect="false"
                        ShowGridHeader="true" Height="400px" EnableColumnLines="true" 
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:DropDownList ID="drpUnit" Label="单位" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpUnit_TextChanged"
                                        EnableEdit="true" LabelWidth="60px" Width="300px">
                                    </f:DropDownList>
                                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" Text="导出" Icon="TableGo"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:TemplateField ColumnID="tfNumber" HeaderText="序号" Width="60px" HeaderTextAlign="Center"
                                TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField HeaderText="卡号" ColumnID="CardNo" DataField="CardNo" SortField="CardNo"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="100px">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="PersonName" DataField="PersonName" SortField="PersonName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="人员姓名">
                            </f:RenderField>
                            <f:RenderField HeaderText="岗位名称" ColumnID="WorkPostName" DataField="WorkPostName" SortField="WorkPostName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="140px">
                            </f:RenderField>
                            <f:RenderField HeaderText="单位名称" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" Width="200px">
                            </f:RenderField>
                            <f:RenderField HeaderText="所在班组" ColumnID="TeamGroupName" DataField="TeamGroupName"
                                SortField="TeamGroupName" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                Width="140px">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    </form>
</body>
</html>
