<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MillionsMonthlyReport.aspx.cs"
    Inherits="FineUIPro.Web.Information.MillionsMonthlyReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>百万工时安全统计月报表</title>
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
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="True" AutoScroll="true" BodyPadding="10px"
                        EnableCollapse="true" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>
                                    <f:DropDownList ID="drpUnit" AutoPostBack="true" EnableSimulateTree="true" runat="server"
                                        Width="320px" LabelWidth="80px" Label="填报企业" EnableEdit="true" ForceSelection="false"
                                        OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpYear" AutoPostBack="true" EnableSimulateTree="true" runat="server"
                                        Width="150px" LabelWidth="50px" Label="年度" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpMonth" AutoPostBack="true" EnableSimulateTree="true" runat="server"
                                        Width="150px" LabelWidth="50px" Label="月份" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                                    </f:DropDownList>
                                    <f:Button ID="BtnBulletLeft" ToolTip="前一个月" Icon="BulletLeft" runat="server" EnablePostBack="true"
                                        OnClick="BtnBulletLeft_Click">
                                    </f:Button>
                                    <f:Button ID="BtnBulletRight" ToolTip="后一个月" Icon="BulletRight" runat="server" EnablePostBack="true"
                                        OnClick="BulletRight_Click">
                                    </f:Button>
                                     <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                    </f:ToolbarFill>
                                    <f:Button ID="btnSee" ToolTip="查看审批流程" Icon="Find" runat="server" OnClick="btnSee_Click">
                                    </f:Button>
                                    <f:Button ID="btnNew" ToolTip="新增" Icon="Add" Hidden="true" runat="server" OnClick="btnNew_Click">
                                    </f:Button>
                                    <f:Button ID="btnEdit" ToolTip="编辑" Icon="Pencil" Hidden="true" runat="server" OnClick="btnEdit_Click">
                                    </f:Button>
                                    <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" Hidden="true" ConfirmText="确定删除当前数据？"
                                        OnClick="btnDelete_Click" runat="server">
                                    </f:Button>
                                    <f:Button ID="btnAudit1" ToolTip="审核" Icon="Pencil" Hidden="true" runat="server"
                                        OnClick="btnAudit1_Click">
                                    </f:Button>
                                    <f:Button ID="btnAudit2" ToolTip="审批" Icon="Pencil" Hidden="true" runat="server"
                                        OnClick="btnAudit2_Click">
                                    </f:Button>
                                    <f:Button ID="btnUpdata" ToolTip="上报" Icon="PageSave" Hidden="true" runat="server"
                                        OnClick="btnUpdata_Click">
                                    </f:Button>
                                    <f:Button ID="btnImport" ToolTip="导入" Icon="ApplicationGet" Hidden="true" runat="server"
                                        OnClick="btnImport_Click">
                                    </f:Button>
                                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                     <f:Button ID="btnView" ToolTip="查看未报项目" Icon="MagifierZoomOut" runat="server"
                                        OnClick="btnView_Click">
                                    </f:Button>
                                    <f:Button ID="btnPrint" ToolTip="打印" Icon="Printer" Hidden="true" runat="server"
                                        OnClick="btnPrint_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lbUnitName" Hidden="true">
                                    </f:Label>
                                    <f:Label runat="server" ID="lbFillingDate">
                                    </f:Label>
                                    <f:Label runat="server" ID="lbDutyPerson">
                                    </f:Label>
                                    <f:Label runat="server" ID="lbHandleMan">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="MillionsMonthlyReportItemId" AllowCellEditing="true"
                        SortField="SortIndex" ClicksToEdit="1" DataIDField="MillionsMonthlyReportItemId"
                        EnableColumnLines="true">
                        <Columns>
                            <f:TemplateField ColumnID="tfNumber" Width="50px" HeaderText="序号" HeaderTextAlign="Center"
                                TextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblNumber" runat="server" Text='<%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="120px" ColumnID="Affiliation" DataField="Affiliation" FieldType="String"
                                HeaderText="所属单位" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="Name" DataField="Name" FieldType="String"
                                HeaderText="名称" HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:GroupField EnableLock="true" HeaderText="员工总数" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="PostPersonNum" DataField="PostPersonNum" FieldType="String"
                                        HeaderText="在岗员工" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="SnapPersonNum" DataField="SnapPersonNum" FieldType="String"
                                        HeaderText="临时员工" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="70px" ColumnID="ContractorNum" DataField="ContractorNum" FieldType="String"
                                        HeaderText="承包商" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="70px" ColumnID="SumPersonNum" DataField="SumPersonNum" FieldType="String"
                                        HeaderText="合计" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:RenderField Width="120px" ColumnID="TotalWorkNum" DataField="TotalWorkNum" FieldType="String"
                                HeaderText="总工时数（万）" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:GroupField EnableLock="true" HeaderText="总可记录事件" TextAlign="Center">
                                <Columns>
                                    <f:GroupField EnableLock="true" HeaderText="重伤事故" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="70px" ColumnID="SeriousInjuriesNum" DataField="SeriousInjuriesNum"
                                                FieldType="String" HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                            <f:RenderField Width="70px" ColumnID="SeriousInjuriesPersonNum" DataField="SeriousInjuriesPersonNum"
                                                FieldType="String" HeaderText="人数" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                            <f:RenderField Width="80px" ColumnID="SeriousInjuriesLossHour" DataField="SeriousInjuriesLossHour"
                                                FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="轻伤事故" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="70px" ColumnID="MinorAccidentNum" DataField="MinorAccidentNum"
                                                FieldType="String" HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                            <f:RenderField Width="70px" ColumnID="MinorAccidentPersonNum" DataField="MinorAccidentPersonNum"
                                                FieldType="String" HeaderText="人数" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                            <f:RenderField Width="80px" ColumnID="MinorAccidentLossHour" DataField="MinorAccidentLossHour"
                                                FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="其它事故" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="70px" ColumnID="OtherAccidentNum" DataField="OtherAccidentNum"
                                                FieldType="String" HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                            <f:RenderField Width="70px" ColumnID="OtherAccidentPersonNum" DataField="OtherAccidentPersonNum"
                                                FieldType="String" HeaderText="人数" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                            <f:RenderField Width="80px" ColumnID="OtherAccidentLossHour" DataField="OtherAccidentLossHour"
                                                FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="工作受限" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="70px" ColumnID="RestrictedWorkPersonNum" DataField="RestrictedWorkPersonNum"
                                                FieldType="String" HeaderText="人数" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                            <f:RenderField Width="80px" ColumnID="RestrictedWorkLossHour" DataField="RestrictedWorkLossHour"
                                                FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="医疗处置" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="70px" ColumnID="MedicalTreatmentPersonNum" DataField="MedicalTreatmentPersonNum"
                                                FieldType="String" HeaderText="人数" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                            <f:RenderField Width="80px" ColumnID="MedicalTreatmentLossHour" DataField="MedicalTreatmentLossHour"
                                                FieldType="String" HeaderText="损失工时" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="无伤害事故" TextAlign="Center">
                                <Columns>
                                    <f:GroupField EnableLock="true" HeaderText="火灾" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="70px" ColumnID="FireNum" DataField="FireNum" FieldType="String"
                                                HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="爆炸" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="70px" ColumnID="ExplosionNum" DataField="ExplosionNum" FieldType="String"
                                                HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="交通" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="70px" ColumnID="TrafficNum" DataField="TrafficNum" FieldType="String"
                                                HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="机械设备" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="80px" ColumnID="EquipmentNum" DataField="EquipmentNum" FieldType="String"
                                                HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="质量" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="70px" ColumnID="QualityNum" DataField="QualityNum" FieldType="String"
                                                HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                    <f:GroupField EnableLock="true" HeaderText="其它" TextAlign="Center">
                                        <Columns>
                                            <f:RenderField Width="70px" ColumnID="OtherNum" DataField="OtherNum" FieldType="String"
                                                HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                            </f:RenderField>
                                        </Columns>
                                    </f:GroupField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="急救包扎" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="FirstAidDressingsNum" DataField="FirstAidDressingsNum"
                                        FieldType="String" HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="未遂事件" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="AttemptedEventNum" DataField="AttemptedEventNum"
                                        FieldType="String" HeaderText="起数" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:RenderField Width="80px" ColumnID="LossDayNum" DataField="LossDayNum" FieldType="String"
                                HeaderText="损失工日" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="dataload" Handler="onGridDataLoad" />
                        </Listeners>
                    </f:Grid>
                    <f:Form ID="Form3" ShowBorder="False" ShowHeader="False" AutoScroll="true" BodyPadding="10px"
                        EnableCollapse="true" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Rows>
                            <f:FormRow ColumnWidths="20% 19% 23% 19% 19%">
                                <Items>
                                    <f:Label runat="server" ID="lbRecordableIncidentRate">
                                    </f:Label>
                                    <f:Label runat="server" ID="lbLostTimeRate">
                                    </f:Label>
                                    <f:Label runat="server" ID="lbLostTimeInjuryRate">
                                    </f:Label>
                                    <f:Label runat="server" ID="lbDeathAccidentFrequency">
                                    </f:Label>
                                    <f:Label runat="server" ID="lbAccidentMortality">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    <f:Window ID="Window1"  runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" OnClose="Window1_Close"
        Title="编辑百万工时安全统计月报表" CloseAction="HidePostBack" EnableIFrame="true" Height="600px"
        Width="1300px">
    </f:Window>
    <f:Window ID="Window2"  runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" OnClose="Window2_Close"
        Title="导入百万工时安全统计月报表" CloseAction="HidePostBack" EnableIFrame="true" Height="600px"
        Width="900px">
    </f:Window>
    <f:Window ID="Window3" IconUrl="~/res/images/16/11.png" runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" Title="打印百万工时安全统计月报表"
        CloseAction="HidePostBack" EnableIFrame="true" Height="768px" Width="1024px">
    </f:Window>
    <f:Window ID="Window4" Title="查看审核信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" OnClose="Window4_Close"
        Width="1024px" Height="500px">
    </f:Window>
    </form>
    <script>
        function onGridDataLoad(event) {
            this.mergeColumns(['Affiliation']);
        }
    </script>
</body>
</html>
