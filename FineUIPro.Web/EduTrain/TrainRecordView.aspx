<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainRecordView.aspx.cs"
    Inherits="FineUIPro.Web.EduTrain.TrainRecordView" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看培训记录</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
        
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
                    <f:TextBox ID="txtTrainingCode" runat="server" Label="培训编号" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtTrainType" runat="server" Label="培训类型" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtTrainLevel" runat="server" Label="培训级别" Readonly="true" MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtTeachHour" NoDecimal="false" NoNegative="true" MaxValue="100"
                        DecimalPrecision="1" MinValue="0" runat="server" Label="学时" Readonly="true">
                    </f:NumberBox>
                    <f:TextBox ID="txtTrainStartDate" runat="server" Label="培训日期" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtTrainPersonNum" runat="server" Label="培训人数" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="33% 67%">
                <Items>
                    <f:TextBox ID="txtTrainTitle" runat="server" Label="标题" MaxLength="200" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtUnits" runat="server" Label="培训单位" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="33% 67%">
                <Items>
                    <f:TextBox ID="txtTeachMan" runat="server" Label="授课人" MaxLength="50" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtTeachAddress" runat="server" Label="培训地点" MaxLength="100" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtTrainContent" runat="server" Label="培训内容" Readonly="true" LabelAlign="right"
                        Height="80px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1"
                        DataIDField="TrainDetailId" DataKeyNames="TrainDetailId" EnableMultiSelect="false"
                        ShowGridHeader="true" Height="220px" EnableColumnLines="true" >
                        <Columns>
                            <f:RowNumberField HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField Width="180px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                ExpandUnusedSpace="true" FieldType="String" HeaderText="单位" TextAlign="Left"
                                HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="PersonName" DataField="PersonName" SortField="PersonName"
                                FieldType="String" HeaderText="培训人员" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="CheckResult" DataField="CheckResult" FieldType="Boolean"
                                RendererFunction="renderCheckResult" HeaderText="考核结果" HeaderTextAlign="Center"
                                TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="CheckScore" DataField="CheckScore" FieldType="Double"
                                HeaderText="考试成绩" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:NumberBox ID="txtCheckScore" NoDecimal="false" NoNegative="true" MinValue="0"
                                        DecimalPrecision="1" runat="server" Required="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="dataload" Handler="onGridDataLoad" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp">
                    </f:Label>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:Button ID="btnTrainTest" runat="server" ToolTip="培训试卷" Hidden="true"
                        Icon="ApplicationFormEdit" OnClick="btnTrainTest_Click"></f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Window ID="Window2" Title="培训试卷" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true"
        Width="1200px" Height="520px">
    </f:Window>
    </form>
    <script type="text/jscript">
        function renderCheckResult(value) {
            return value == true ? '合格' : '不合格';
        }

        function onGridDataLoad(event) {
            this.mergeColumns(['UnitName']);
        }
    </script>
</body>
</html>
