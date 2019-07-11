<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafeSupervisionParticular.aspx.cs" Inherits="FineUIPro.Web.HiddenInspection.SafeSupervisionParticular" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看领导督查</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
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
        Layout="VBox" BodyPadding="10px" runat="server" RedStarPosition="BeforeText"
        LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSafeSupervisionCode" runat="server" Label="编号" LabelAlign="Right" LabelWidth="70px" Readonly="true">
                                    </f:TextBox>
                    <f:TextBox ID="txtCheckType" runat="server" Label="检查类型" LabelAlign="Right" LabelWidth="70px" Readonly="true">
                                    </f:TextBox>
                    <f:TextBox ID="txtCheckMan" runat="server" Label="检查人" LabelAlign="Right" LabelWidth="70px" Readonly="true">
                                    </f:TextBox>
                    <f:DatePicker ID="txtCheckDate" runat="server" Label="检查日期" LabelAlign="Right" Required="True" ShowRedStar="true"
                        Width="200px" LabelWidth="90px" Readonly="true">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="检查明细(双击查看明细)" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="HazardRegisterId" DataIDField="HazardRegisterId"
                        AllowSorting="true" SortDirection="ASC" SortField="RectificationTime" EnableColumnLines="true"
                        Height="360px" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                        IsDatabasePaging="true" PageSize="100">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                TextAlign="Center" />
                            <f:RenderField Width="150px" ColumnID="RegisterTypesName" DataField="RegisterTypesName"
                                SortField="RegisterTypesName" FieldType="String" HeaderText="检查项" TextAlign="Left"
                                HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="RegisterDef" DataField="RegisterDef" SortField="RegisterDef"
                                FieldType="String" HeaderText="问题描述" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="Rectification" DataField="Rectification" SortField="Rectification"
                                FieldType="String" HeaderText="采取措施" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="ResponsibilityManName" DataField="ResponsibilityManName"
                                SortField="ResponsibilityManName" FieldType="String" HeaderText="责任人" TextAlign="Left"
                                HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="140px" ColumnID="RectificationPeriod" DataField="RectificationPeriod"
                                SortField="RectificationPeriod" HeaderText="整改期限" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="CheckManName" DataField="CheckManName" SortField="CheckManName"
                                FieldType="String" HeaderText="检查人" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="140px" ColumnID="CheckTime" DataField="CheckTime" SortField="CheckTime"
                                HeaderText="检查时间" TextAlign="Center" HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="140px" ColumnID="RectificationTime" DataField="RectificationTime"
                                SortField="RectificationTime" HeaderText="整改时间" TextAlign="Center" HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="StatesStr" DataField="StatesStr" SortField="StatesStr"
                                FieldType="String" HeaderText="状态" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:TemplateField ColumnID="tfImageUrl1" Width="150px" HeaderText="整改前图片" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbImageUrl" runat="server" Text='<%# ConvertImageUrlByImage(Eval("HazardRegisterId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField ColumnID="tfImageUrl2" Width="150px" HeaderText="整改后图片" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# ConvertImgUrlByImage(Eval("HazardRegisterId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnClose" Text="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="1024px"
        OnClose="Window1_Close" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="检查记录明细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1300px"
        Height="500px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="400px"
        Height="200px">
    </f:Window>
    </form>
</body>
</html>
