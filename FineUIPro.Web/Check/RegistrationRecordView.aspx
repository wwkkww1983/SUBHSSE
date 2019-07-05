<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationRecordView.aspx.cs" Inherits="FineUIPro.Web.Check.RegistrationRecordView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看隐患巡检记录</title>
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
                    <f:Label runat="server" ID="txtCheckDate" Label="巡检日期">
                    </f:Label>
                    <f:Label runat="server" ID="txtCheckMan" Label="巡检人">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="巡检明细(双击查看明细)" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="RegistrationId" DataIDField="RegistrationId"
                        AllowSorting="true" SortDirection="ASC" SortField="CheckTime" EnableColumnLines="true"
                        Height="475px" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                        IsDatabasePaging="true" PageSize="500">
                        <Columns>
                            <f:TemplateField ColumnID="tfPageIndex" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center" EnableLock="true" Locked="False">
                        <ItemTemplate>
                            <asp:Label ID="lblPageIndex" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:RenderField Width="220px" ColumnID="ResponsibilityUnitName" DataField="ResponsibilityUnitName"
                        SortField="ResponsibilityUnitName" FieldType="String" HeaderText="责任单位" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="WorkAreaName" DataField="WorkAreaName" SortField="WorkAreaName"
                        FieldType="String" HeaderText="区域" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    
                    <f:RenderField Width="75px" ColumnID="ProblemTypes" DataField="ProblemTypes" SortField="ProblemTypes"
                        FieldType="String" HeaderText="问题类型" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="200px" ColumnID="ProblemDescription" DataField="ProblemDescription"
                        SortField="ProblemDescription" FieldType="String" HeaderText="问题描述" TextAlign="Left"
                        HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="TakeSteps" DataField="TakeSteps" SortField="TakeSteps"
                        FieldType="String" HeaderText="采取措施" TextAlign="Left" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="RectificationPeriod" DataField="RectificationPeriod"
                        SortField="RectificationPeriod" FieldType="Date" Renderer="Date" HeaderText="整改期限"
                        TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="CheckTime" DataField="CheckTime" SortField="CheckTime"
                        HeaderText="检查时间" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="140px" ColumnID="RectificationTime" DataField="RectificationTime"
                        SortField="RectificationTime" HeaderText="整改时间" TextAlign="Center" HeaderTextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="70px" ColumnID="States" DataField="States" SortField="States"
                        FieldType="String" HeaderText="状态" HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:TemplateField ColumnID="tfImageUrl1" Width="150px" HeaderText="整改前图片" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbImageUrl" runat="server" Text='<%# ConvertImageUrlByImage(Eval("RegistrationId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField ColumnID="tfImageUrl2" Width="150px" HeaderText="整改后图片" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# ConvertImgUrlByImage(Eval("RegistrationId")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                    <%--<f:TemplateField ColumnID="tfImageUrl" Width="170px" HeaderText="整改前图片" HeaderTextAlign="Center"
                        TextAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnImageUrl" runat="server" Text='<%# ConvertImageUrl(Eval("RegistrationId")) %>'></asp:LinkButton>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:TemplateField ColumnID="tfRectificationImageUrl" Width="170px" HeaderText="整改后图片"
                        HeaderTextAlign="Center" TextAlign="Left">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnRectificationImageUrl" runat="server" Text='<%#ConvertImgUrl(Eval("RegistrationId")) %>'></asp:LinkButton>
                        </ItemTemplate>
                    </f:TemplateField>--%>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="dataload" Handler="onGridDataLoad" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" Width="1024px"
        OnClose="Window1_Close" Height="600px">
    </f:Window>
    <f:Window ID="Window2" Title="巡检记录明细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1300px"
        Height="500px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="400px"
        Height="200px">
    </f:Window>
    </form>
    <script type="text/javascript">
        function onGridDataLoad(event) {
            this.mergeColumns(['ResponsibilityUnitName']);
            this.mergeColumns(['WorkAreaName']);
        }
    </script>
</body>
</html>
