<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckHolidayView.aspx.cs" Inherits="FineUIPro.Web.Check.CheckHolidayView" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看季节性/节假日检查</title>
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
        
        .f-grid-row.yellow
        {
            background-color: yellow;
            background-image: none;
        }
        
        .f-grid-row.red
        {
            background-color: #FF7575;
            background-image: none;
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
                    <f:TextBox ID="txtCheckHolidayCode" runat="server" Label="检查编号" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckDate" runat="server" Label="检查日期" Readonly="true" MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtArea" runat="server" Label="被检查单位、区域或部位" MaxLength="200" LabelWidth="180">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel3" Layout="Anchor" Title="参加单位及人员" runat="server">
                        <Items>
                            <f:Form ID="Form2" ShowHeader="false" ShowBorder="false" runat="server">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtThisUnit" runat="server" Label="单位" Readonly="true" MaxLength="50">
                                            </f:TextBox>
                                            <f:TextBox ID="txtMainUnitPerson" runat="server" Label="人员" Readonly="true">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:TextBox ID="txtSubUnits" runat="server" Label="参与单位" Readonly="true">
                                            </f:TextBox>
                                            <f:TextBox ID="txtSubUnitPerson" runat="server" Label="人员" Readonly="true" MaxLength="500">
                                            </f:TextBox>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" ClicksToEdit="1"
                        DataIDField="CheckHolidayDetailId" DataKeyNames="CheckHolidayDetailId" EnableMultiSelect="false"
                        ShowGridHeader="true" Height="175" EnableColumnLines="true" AutoScroll="true"
                        >
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                    <f:Label runat="server" ID="lbIsCompleted" Text=""></f:Label>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RowNumberField HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:TemplateField Width="120px" HeaderText="检查类型" HeaderTextAlign="Center" TextAlign="Left"
                                ColumnID="CheckItemType">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# ConvertCheckItemType(Eval("CheckItem")) %>'
                                        ToolTip='<%# ConvertCheckItemType(Eval("CheckItem")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="120px" ColumnID="CheckItemStr" DataField="CheckItemStr" SortField="CheckItemStr"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="检查项">
                            </f:RenderField>
                            <f:RenderField Width="280px" ColumnID="CheckResult" DataField="CheckResult" SortField="CheckResult"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="检查结果"
                                ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField Width="170px" ColumnID="CheckOpinion" DataField="CheckOpinion" SortField="CheckOpinion"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="处理意见">
                            </f:RenderField>
                            <f:RenderField Width="170px" ColumnID="HandleResult" DataField="HandleResult" SortField="HandleResult"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="整改结果">
                            </f:RenderField>
                            <f:RenderField Width="170px" ColumnID="CheckStation" DataField="CheckStation" SortField="CheckStation"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="验证情况">
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
                    <f:TextBox ID="txtMainUnitDeputy" runat="server" Label="" Readonly="true" MaxLength="50" LabelWidth="220">
                    </f:TextBox>
                    <f:TextBox ID="txtMainUnitDeputyDate" runat="server" Label="签字日期" Readonly="true" MaxLength="50">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSubUnitDeputy" runat="server" Label="分包商代表" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:TextBox ID="txtSubUnitDeputyDate" runat="server" Label="签字日期" Readonly="true" MaxLength="50">
                    </f:TextBox>
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
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                    <f:HiddenField ID="hdId" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdAttachUrl" runat="server">
                    </f:HiddenField>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
    <script>
       
        function onGridDataLoad(event) {
            this.mergeColumns(['CheckItemType']);
        }
    </script>
</body>
</html>
