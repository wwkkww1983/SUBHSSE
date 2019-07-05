<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrillPlanHalfYearReport.aspx.cs"
    Inherits="FineUIPro.Web.Information.DrillPlanHalfYearReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>应急演练工作计划半年报表</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
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
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server" Title="报表详细">
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
                                    <f:DropDownList ID="drpHalfYear" AutoPostBack="true" EnableSimulateTree="true" runat="server" Width="150px"
                                        LabelWidth="50px" Label="半年" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                                    </f:DropDownList>
                                    <f:Button ID="BtnBulletLeft" ToolTip="前一半年" Icon="BulletLeft" runat="server" EnablePostBack="true"
                                        OnClick="BtnBulletLeft_Click">
                                     </f:Button>
                                    <f:Button ID="BtnBulletRight" ToolTip="后一半年" Icon="BulletRight" runat="server" EnablePostBack="true"
                                        OnClick="BulletRight_Click"> 
                                    </f:Button>
                                    <f:Button ID="btnSee" ToolTip="查看审批流程" Icon="Find" runat="server" OnClick="btnSee_Click">
                                    </f:Button>
                                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    <f:Button ID="btnNew" ToolTip="新增" Icon="Add" Hidden="true" runat="server" OnClick="btnNew_Click">
                                    </f:Button>
                                    <f:Button ID="btnEdit" ToolTip="编辑" Icon="Pencil" Hidden="true" runat="server" OnClick="btnEdit_Click">
                                    </f:Button>
                                     <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" Hidden="true" ConfirmText="确定删除当前数据？"
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
                                     <f:Label ID="txtUnitName" runat="server" Label="单位" Hidden="true">
                                    </f:Label>
                                     <f:Label ID="txtCompileMan" runat="server" Label="联系人">
                                    </f:Label>
                                    <f:Label ID="txtTel" runat="server" Label="联系电话">
                                    </f:Label>
                                    <f:Label ID="txtCompileDate" runat="server" Label="制表时间">
                                    </f:Label>
                                    <f:Label ID="lbHandleMan" runat="server">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                    <f:Grid ID="Grid1" Width="870px" ShowBorder="true" ShowHeader="false" EnableCollapse="true"
                        runat="server" BoxFlex="1" DataKeyNames="DrillPlanHalfYearReportItemId" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="DrillPlanHalfYearReportItemId" AllowSorting="true"
                        SortField="SortIndex" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="true"
                        IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange"
                        AllowFilters="true" OnFilterChange="Grid1_FilterChange" EnableColumnLines="true">
                        <Columns>
                            <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                            <f:RenderField Width="200px" ColumnID="DrillPlanName" DataField="DrillPlanName" SortField="DrillPlanName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="演练名称">
                            </f:RenderField>
                            <f:RenderField Width="180px" ColumnID="OrganizationUnit" DataField="OrganizationUnit"
                                SortField="OrganizationUnit" FieldType="String" HeaderTextAlign="Center" TextAlign="Left"
                                HeaderText="组织单位">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="DrillPlanDate" DataField="DrillPlanDate" SortField="DrillPlanDate"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="演练时间">
                            </f:RenderField>
                            <f:RenderField  ColumnID="AccidentScene" DataField="AccidentScene" SortField="AccidentScene"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="主要事故情景" ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="ExerciseWay" DataField="ExerciseWay" SortField="ExerciseWay"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="演练方式">
                            </f:RenderField>
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
    <f:Window ID="Window1" Title="编辑应急演练工作计划半年报表" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        CloseAction="HidePostBack" Width="1300px" Height="560px">
    </f:Window>
    <f:Window ID="Window2" IconUrl="~/res/images/16/11.png" runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" OnClose="Window2_Close"
        Title="导入应急演练工作计划半年报表" CloseAction="HidePostBack" EnableIFrame="true" Height="600px"
        Width="900px">
    </f:Window>
    <f:Window ID="Window3" IconUrl="~/res/images/16/11.png" runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" Title="打印应急演练工作计划半年报表"
        CloseAction="HidePostBack" EnableIFrame="true" Height="768px" Width="1024px">
    </f:Window>
    <f:Window ID="Window4" Title="查看审核信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" OnClose="Window4_Close"
        Width="1024px" Height="500px">
    </f:Window>
    </form>
</body>
</html>
