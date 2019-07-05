<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckWorkEdit.aspx.cs"
    Inherits="FineUIPro.Web.Check.CheckWorkEdit" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑开工前检查</title>
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
            <f:FormRow ColumnWidths="50% 30% 20%">
                <Items>
                    <f:TextBox ID="txtCheckWorkCode" runat="server" Label="检查编号" Readonly="true" MaxLength="50">
                    </f:TextBox>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="检查日期" ID="txtCheckDate">
                    </f:DatePicker>
                    <f:CheckBox ID="ckIsAgree" runat="server" Label="同意开工"></f:CheckBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtArea" runat="server" Label="被检查单位、区域或部位" MaxLength="3000" 
                        LabelWidth="180" FocusOnPageLoad="true" ShowRedStar="true" Required="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel3" Layout="Anchor" Title="参加单位及人员" runat="server">
                        <Items>
                            <f:Form ShowHeader="false" ShowBorder="false" runat="server">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                           <f:DropDownList ID="drpThisUnit" runat="server" Label="单位" AutoPostBack="true" OnSelectedIndexChanged="drpThisUnit_SelectedIndexChanged">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpMainUnitPerson" runat="server" Label="人员" EnableCheckBoxSelect="true"
                                                EnableMultiSelect="true">
                                            </f:DropDownList>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:DropDownList ID="drpSubUnits" runat="server" Label="参与单位" EnableCheckBoxSelect="true"
                                                EnableMultiSelect="true" AutoPostBack="true" OnSelectedIndexChanged="drpSubUnits_SelectedIndexChanged">
                                            </f:DropDownList>
                                            <f:DropDownList ID="drpSubUnitPerson" runat="server" Label="人员" EnableCheckBoxSelect="true"
                                                EnableMultiSelect="true">
                                            </f:DropDownList>
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
                        DataIDField="CheckWorkDetailId" DataKeyNames="CheckWorkDetailId" EnableMultiSelect="false"
                        ShowGridHeader="true" Height="200" EnableColumnLines="true" AutoScroll="true" OnRowCommand="Grid1_RowCommand"
                        EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                <Items>
                                   <%-- <f:CheckBox runat="server" ID="ckbIsCompleted" ></f:CheckBox>
                                    <f:Label runat="server" ID="lb1" Text="是否闭环"></f:Label>--%>
                                    <f:Button ID="btnSelect" Icon="ShapeSquareSelect" runat="server" ToolTip="选择" ValidateForms="SimpleForm1"
                                        OnClick="btnSelect_Click">
                                    </f:Button>
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
                            <%--<f:RenderField Width="120px" ColumnID="CheckItemStr" DataField="CheckItemStr" SortField="CheckItemStr"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="检查项">
                            </f:RenderField>--%>
                            <f:LinkButtonField Width="120px" HeaderText="检查项" ConfirmTarget="Top" CommandName="click"
                        TextAlign="Center" ToolTip="点击增加一条相同记录" DataTextField="CheckItemStr" ColumnID="CheckItemStr" />
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
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                        </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtMainUnitDeputy" runat="server" MaxLength="50" LabelWidth="220">
                    </f:TextBox>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="签字日期" ID="txtMainUnitDeputyDate">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSubUnitDeputy" runat="server" Label="施工分包代表" MaxLength="50" LabelWidth="220">
                    </f:TextBox>
                    <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="签字日期" ID="txtSubUnitDeputyDate">
                    </f:DatePicker>
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
                    <f:HiddenField ID="hdId" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdAttachUrl" runat="server">
                    </f:HiddenField>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" Title="编辑检查项明细" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="1100px" Height="520px">
    </f:Window>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
            Icon="Pencil" runat="server" Text="编辑">
        </f:MenuButton>
        <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
            Icon="Delete" ConfirmText="确定删除当前数据？" ConfirmTarget="Top" runat="server" Text="删除"
            >
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

        function onGridDataLoad(event) {
            this.mergeColumns(['CheckItemType']);
        }
    </script>
</body>
</html>
