<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentCauseReport.aspx.cs"
    Inherits="FineUIPro.Web.Information.AccidentCauseReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>职工伤亡事故原因分析报表</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
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
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="True" AutoScroll="true" BodyPadding="10px"
                        EnableCollapse="true" runat="server" RedStarPosition="BeforeText" LabelAlign="Right"  Height="250px">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>
                                    <f:DropDownList ID="drpUnit" AutoPostBack="true" EnableSimulateTree="true" runat="server"
                                        Width="320px" LabelWidth="80px" EnableEdit="true" ForceSelection="false" Label="填报单位"
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
                                     <f:ToolbarFill runat="server">
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
                                    <f:Label runat="server" ID="lb1">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lb2">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lb3">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lb4">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lb5">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lb6">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="90% 10%">
                                <Items>
                                    <f:Label runat="server" ID="lb7">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="lbFillCompanyPersonCharge">
                                    </f:Label>
                                    <f:Label runat="server" ID="lbTabPeople">
                                    </f:Label>
                                    <f:Label runat="server" ID="lbAuditPerson">
                                    </f:Label>
                                    <f:Label runat="server" ID="lbFillingDate">
                                    </f:Label>
                                    <f:Label runat="server" ID="lbHandleMan">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="AccidentCauseReportItemId" AllowCellEditing="true"
                        ClicksToEdit="1" DataIDField="AccidentCauseReportItemId" EnableColumnLines="true">
                        <Columns>
                            <f:RenderField Width="100px" ColumnID="AccidentType" DataField="AccidentType" FieldType="String"
                                HeaderText="事故类别" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:GroupField EnableLock="true" HeaderText="合计" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="TotalDeath" DataField="TotalDeath" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="TotalInjuries" DataField="TotalInjuries" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="TotalMinorInjuries" DataField="TotalMinorInjuries"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="防护、保险信号等装置</br>缺乏或装置缺乏或有缺陷" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="60px" ColumnID="Death1" DataField="Death1" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="Injuries1" DataField="Injuries1" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="MinorInjuries1" DataField="MinorInjuries1"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="设备、工具附件有缺陷" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="60px" ColumnID="Death2" DataField="Death2" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="Injuries2" DataField="Injuries2" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="MinorInjuries2" DataField="MinorInjuries2"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="个人防护用品</br>缺乏或有缺陷" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="60px" ColumnID="Death3" DataField="Death3" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="Injuries3" DataField="Injuries3" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="MinorInjuries3" DataField="MinorInjuries3"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="光线不足或工作地点</br>及通道情况不良" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="60px" ColumnID="Death4" DataField="Death4" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="Injuries4" DataField="Injuries4" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="MinorInjuries4" DataField="MinorInjuries4"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="劳动组织不合理" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death5" DataField="Death5" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries5" DataField="Injuries5" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries5" DataField="MinorInjuries5"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="对现场工作缺乏</br>检查或指导有错误" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="60px" ColumnID="Death6" DataField="Death6" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="Injuries6" DataField="Injuries6" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="MinorInjuries6" DataField="MinorInjuries6"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="设计有缺陷" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death7" DataField="Death7" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries7" DataField="Injuries7" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries7" DataField="MinorInjuries7"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="不懂操作技术和知识" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="60px" ColumnID="Death8" DataField="Death8" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="Injuries8" DataField="Injuries8" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="MinorInjuries8" DataField="MinorInjuries8"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="违反操作规程</br>或劳动纪律" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="60px" ColumnID="Death9" DataField="Death9" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="Injuries9" DataField="Injuries9" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="MinorInjuries9" DataField="MinorInjuries9"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="没有安全操作</br>规程制度或不健全" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="60px" ColumnID="Death10" DataField="Death10" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="Injuries10" DataField="Injuries10" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="MinorInjuries10" DataField="MinorInjuries10"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="其他" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death11" DataField="Death11" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries11" DataField="Injuries11" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries11" DataField="MinorInjuries11"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center" TextAlign="Center">
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="afteredit" Handler="onGridAfterEdit" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    <f:Window ID="Window2" IconUrl="~/res/images/16/11.png" runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" OnClose="Window2_Close"
        Title="编辑职工伤亡事故原因分析报表" CloseAction="HidePostBack" EnableIFrame="true" Height="680px"
        Width="1300px">
    </f:Window>
    <f:Window ID="Window1" Title="导入职工伤亡事故原因分析报表" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="false"
        CloseAction="HidePostBack" Width="1024px" Height="600px">
    </f:Window>
    <f:Window ID="Window3" IconUrl="~/res/images/16/11.png" runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" Title="打印职工伤亡事故原因分析报表"
        CloseAction="HidePostBack" EnableIFrame="true" Height="768px" Width="1024px">
    </f:Window>
    <f:Window ID="Window4" Title="查看审核信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" OnClose="Window4_Close"
        Width="1024px" Height="650px">
    </f:Window>
    </form>
    <script>
        function updateSummary() {
            // 回发到后台更新
            __doPostBack('', 'UPDATE_SUMMARY');
        }

        function onGridAfterEdit(event, value, params) {
            var me = this, columnId = params.columnId, rowId = params.rowId, rowIndex = params.rowIndex;
            if (rowIndex > 0) {
                //                if (columnId === 'SelectNumber' || columnId === 'TestScore' || columnId === 'Testnum') {
                //                    var selectNumber = me.getCellValue(rowId, 'SelectNumber');
                //                    var testScore = me.getCellValue(rowId, 'TestScore');
                //                    var testnum = me.getCellValue(rowId, 'Testnum');

                //                    if (selectNumber > testnum) {
                //                        me.updateCellValue(rowId, 'SelectNumber', '');
                //                        alert("选题数不能大于题库数!")
                //                        return;
                //                    }

                //                    if (selectNumber.toString() != "" && testScore.toString() != "") {
                //                        me.updateCellValue(rowId, 'TestTotalScore', selectNumber * testScore);
                //                    }
                //                }
                updateSummary();
            }
        }
    </script>
</body>
</html>
