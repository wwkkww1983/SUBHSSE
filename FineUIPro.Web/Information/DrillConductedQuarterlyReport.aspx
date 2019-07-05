<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrillConductedQuarterlyReport.aspx.cs"
    Inherits="FineUIPro.Web.Information.DrillConductedQuarterlyReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>应急演练开展情况季报表</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .f-grid-row-summary .f-grid-cell-inner
        {
            font-weight: bold;
            color: red;
        }
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>     
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>           
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server" Title="">
                <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="true" AutoScroll="true" BodyPadding="10px"
                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items> 
                                    <f:DropDownList ID="drpUnit" AutoPostBack="true" EnableSimulateTree="true" runat="server" Width="350px"
                                        LabelWidth="70px" Label="单位" EnableEdit="true" ForceSelection="false" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpYear" AutoPostBack="true" EnableSimulateTree="true" runat="server" Width="150px"
                                        LabelWidth="50px" Label="年度" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpQuarter" AutoPostBack="true" EnableSimulateTree="true" runat="server" Width="150px"
                                        LabelWidth="50px" Label="季度" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                                    </f:DropDownList>
                                    <f:Button ID="BtnBulletLeft" ToolTip="前一季度" Icon="BulletLeft" runat="server" EnablePostBack="true"
                                        OnClick="BtnBulletLeft_Click">
                                     </f:Button>
                                    <f:Button ID="BtnBulletRight" ToolTip="后一季度" Icon="BulletRight" runat="server" EnablePostBack="true"
                                        OnClick="BulletRight_Click"> 
                                    </f:Button>
                                    <f:Button ID="btnSee" ToolTip="查看审批流程" Icon="Find" runat="server" OnClick="btnSee_Click">
                                    </f:Button>
                                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    <f:Button ID="btnNew" ToolTip="新增" Hidden="true" Icon="Add" runat="server" OnClick="btnNew_Click">
                                    </f:Button>
                                    <f:Button ID="btnEdit" ToolTip="编辑" Hidden="true" Icon="Pencil" runat="server" OnClick="btnEdit_Click">
                                    </f:Button>
                                     <f:Button ID="btnDelete" ToolTip="删除" Hidden="true" Icon="Delete" ConfirmText="确定删除当前数据？"
                                        OnClick="btnDelete_Click" runat="server">
                                    </f:Button>
                                    <f:Button ID="btnAudit1" ToolTip="审核" Icon="Pencil" Hidden="true" runat="server" OnClick="btnAudit1_Click">
                                    </f:Button>
                                    <f:Button ID="btnAudit2" ToolTip="审批" Icon="Pencil" Hidden="true" runat="server" OnClick="btnAudit2_Click">
                                    </f:Button>
                                    <f:Button ID="btnUpdata" ToolTip="上报" Icon="PageSave" Hidden="true" runat="server" OnClick="btnUpdata_Click">
                                    </f:Button>
                                    <f:Button ID="btnImport" ToolTip="导入" Icon="ApplicationGet" Hidden="true" runat="server"
                                        OnClick="btnImport_Click">
                                    </f:Button>
                                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                    <f:Button ID="btnPrint" ToolTip="打印" Icon="ApplicationGo" Hidden="true" runat="server"
                                        OnClick="btnPrint_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Rows>
                            <f:FormRow>
                                <Items>                                   
                                    <f:Label ID="txtUnitName" runat="server" Hidden="true">
                                    </f:Label>                                 
                                    <f:Label ID="txtQuarter" runat="server">
                                    </f:Label>                                    
                                    <f:Label ID="txtCompileDate" runat="server">
                                    </f:Label>
                                    <f:Label ID="lbHandleMan" runat="server">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="DrillConductedQuarterlyReportItemId"
                        AllowCellEditing="true" ClicksToEdit="2" DataIDField="DrillConductedQuarterlyReportItemId"
                        AllowSorting="true" SortField="SortIndex" SortDirection="ASC" OnSort="Grid1_Sort"
                        AllowPaging="true" IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                        AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableSummary="true" EnableColumnLines="true"
                        SummaryPosition="Flow">
                        <Columns>
                          <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                                TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="120px" ColumnID="IndustryType" DataField="IndustryType" SortField="IndustryType"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="行业类别">
                            </f:RenderField>
                            <f:GroupField HeaderText="总体情况" TextAlign="Center" HeaderTextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="TotalConductCount" DataField="TotalConductCount"
                                        FieldType="Int" HeaderText="举办次数" HeaderToolTip="举办次数" HeaderTextAlign="Center"
                                        TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="TotalPeopleCount" DataField="TotalPeopleCount"
                                        FieldType="Int" HeaderText="参演人数" HeaderToolTip="参演人数" TextAlign="Center" HeaderTextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="TotalInvestment" DataField="TotalInvestment"
                                        FieldType="Double" HeaderText="直接投入" HeaderToolTip="直接投入" TextAlign="Center"
                                        HeaderTextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField HeaderText="企业总部" TextAlign="Center" HeaderTextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="HQConductCount" DataField="HQConductCount"
                                        FieldType="Int" HeaderText="举办次数" HeaderToolTip="举办次数" TextAlign="Center" HeaderTextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="HQPeopleCount" DataField="HQPeopleCount" FieldType="Int"
                                        HeaderText="参演人数" HeaderToolTip="参演人数" TextAlign="Center" HeaderTextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="HQInvestment" DataField="HQInvestment" FieldType="Double"
                                        HeaderText="直接投入" HeaderToolTip="直接投入" TextAlign="Center" HeaderTextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField HeaderText="基层单位" TextAlign="Center" HeaderTextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="BasicConductCount" DataField="BasicConductCount"
                                        FieldType="Int" HeaderText="举办次数" HeaderToolTip="举办次数" TextAlign="Center" HeaderTextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="BasicPeopleCount" DataField="BasicPeopleCount"
                                        FieldType="Int" HeaderText="参演人数" HeaderToolTip="参演人数" TextAlign="Center" HeaderTextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="BasicInvestment" DataField="BasicInvestment"
                                        FieldType="Double" HeaderText="直接投入" HeaderToolTip="直接投入" TextAlign="Center"
                                        HeaderTextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:RenderField Width="150px" ColumnID="ComprehensivePractice" DataField="ComprehensivePractice"
                                SortField="ComprehensivePractice" FieldType="String" HeaderTextAlign="Center"
                                TextAlign="Center" HeaderText="综合演练">
                            </f:RenderField>
                            <f:GroupField HeaderText="其中" TextAlign="Center" HeaderTextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="120px" ColumnID="CPScene" DataField="CPScene" FieldType="String"
                                        HeaderText="现场" HeaderToolTip="现场" TextAlign="Center" HeaderTextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="120px" ColumnID="CPDesktop" DataField="CPDesktop" FieldType="String"
                                        HeaderText="桌面" HeaderToolTip="桌面" TextAlign="Center" HeaderTextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:RenderField Width="150px" ColumnID="SpecialDrill" DataField="SpecialDrill" SortField="SpecialDrill"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="专项演练">
                            </f:RenderField>
                            <f:GroupField HeaderText="其中" TextAlign="Center" HeaderTextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="120px" ColumnID="SDScene" DataField="SDScene" FieldType="String"
                                        HeaderText="现场" HeaderToolTip="现场" TextAlign="Center" HeaderTextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="120px" ColumnID="SDDesktop" DataField="SDDesktop" FieldType="String"
                                        HeaderText="桌面" HeaderToolTip="桌面" TextAlign="Center" HeaderTextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
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
            </f:Region>
        </Regions>
    </f:RegionPanel>
    <f:Window ID="Window1" Title="编辑应急演练开展情况季报表" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="false"
        CloseAction="HidePostBack" Width="1300px" Height="500px">
    </f:Window>
    <f:Window ID="Window2" IconUrl="~/res/images/16/11.png" runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" OnClose="Window2_Close"
        Title="导入应急演练开展情况季报" CloseAction="HidePostBack" EnableIFrame="true" Height="600px"
        Width="900px">
    </f:Window>    
     <f:Window ID="Window3" IconUrl="~/res/images/16/11.png" runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true"
        Title="打印应急演练开展情况季报" CloseAction="HidePostBack" EnableIFrame="true" Height="768px"
        Width="1024px">
    </f:Window>
    <f:Window ID="Window4" Title="查看审核信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" OnClose="Window4_Close"
        Width="1024px" Height="500px">
    </f:Window>
    </form>
</body>
</html>
