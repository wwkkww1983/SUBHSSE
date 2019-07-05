<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainRecordEdit.aspx.cs"
    Inherits="FineUIPro.Web.EduTrain.TrainRecordEdit" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑培训记录</title>
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
                    <f:DropDownList ID="drpTrainType" runat="server" Label="培训类型" ShowRedStar="true"
                        Required="true">
                    </f:DropDownList>
                    <f:DropDownList ID="drpTrainLevel" runat="server" Label="培训级别" >
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtTeachHour" NoDecimal="false" NoNegative="true" MaxValue="100"
                        DecimalPrecision="1" MinValue="0" runat="server" Label="学时" ShowRedStar="true" Required="true">
                    </f:NumberBox>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="培训日期" ID="txtTrainStartDate">
                    </f:DatePicker>
                   <f:NumberBox ID="txtTrainPersonNum" NoDecimal="true" NoNegative="true" MinValue="0" Readonly="true" runat="server" Label="培训人数" >
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="33% 67%">
                <Items>
                    <f:TextBox ID="txtTrainTitle" runat="server" Label="标题" MaxLength="200" >
                    </f:TextBox>
                    <f:DropDownList ID="drpUnits" runat="server" Label="培训单位" EnableCheckBoxSelect="true"
                        EnableMultiSelect="true">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="33% 67%">
                <Items>
                    <f:TextBox ID="txtTeachMan" runat="server" Label="授课人" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtTeachAddress" runat="server" Label="培训地点" MaxLength="100">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtTrainContent" runat="server" Label="培训内容" LabelAlign="right" Height="80px">
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1" AllowCellEditing="true"
                        DataIDField="TrainDetailId" DataKeyNames="TrainDetailId" EnableMultiSelect="true"
                        ShowGridHeader="true" Height="220px" EnableColumnLines="true" AutoScroll="true">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:Button ID="btnSelect" Icon="ShapeSquareSelect" runat="server" ToolTip="选择培训人员" ValidateForms="SimpleForm1"
                                        OnClick="btnSelect_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField Width="180px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName" ExpandUnusedSpace="true"
                                FieldType="String" HeaderText="单位" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="150px" ColumnID="PersonName" DataField="PersonName" SortField="PersonName"
                                FieldType="String" HeaderText="培训人员" TextAlign="Left" HeaderTextAlign="Center">
                            </f:RenderField>
                           <%-- <f:TemplateField Width="150px" HeaderText="考核结果" HeaderTextAlign="Center" TextAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpCheckResult" runat="server" Height="23px" SelectedValue='<%# Eval("CheckResult")==null?"True":Eval("CheckResult") %>'
                                        Style="border: 0px;">
                                        <asp:ListItem Value="True">通过</asp:ListItem>
                                        <asp:ListItem Value="False">未通过</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </f:TemplateField>--%>
                            <f:RenderField Width="100px" ColumnID="CheckResult" DataField="CheckResult" FieldType="Int"
                                    RendererFunction="renderGender" HeaderText="考核结果">
                                    <Editor>
                                        <f:DropDownList ID="drpCheckResult" Required="true" runat="server">
                                            <f:ListItem Text="通过" Value="1" />
                                            <f:ListItem Text="未通过" Value="0" />
                                        </f:DropDownList>
                                    </Editor>
                                </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="CheckScore" DataField="CheckScore" FieldType="Double"
                                HeaderText="考试成绩" HeaderTextAlign="Center" TextAlign="Left">
                                <Editor>
                                    <f:NumberBox ID="txtCheckScore" NoDecimal="false" NoNegative="true" MinValue="0"
                                        DecimalPrecision="1" runat="server" Required="true">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="1px" ColumnID="TrainDetailId" DataField="TrainDetailId" 
                                FieldType="String" HeaderText="主键"  Hidden="true" HeaderTextAlign="Center">                                        
                            </f:RenderField>
                        </Columns>
                        <Listeners>
                            <f:Listener Event="dataload" Handler="onGridDataLoad" />
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
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
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="选择培训人员" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1200px" Height="520px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Top" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="确定删除当前数据？" ConfirmTarget="Top" runat="server" Text="删除">
        </f:MenuButton>
    </f:Menu>
    </form>
    <script>
        var menuID = '<%= Menu1.ClientID %>';

        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function renderGender(value) {
            return value == 1 ? '通过' : '不通过';
        }
        function onGridDataLoad(event) {
            this.mergeColumns(['UnitName']);
        }
    </script>
</body>
</html>
