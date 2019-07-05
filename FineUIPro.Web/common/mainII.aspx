<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainII.aspx.cs" Inherits="FineUIPro.Web.common.mainII" %>

<%@ Register Src="~/Controls/ChartControl.ascx" TagName="ChartControl" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>领导岗位首页</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel2" AjaxAspnetControls="divSafetyQuarterly,divAccidentCause,divAccidentRate" />
    <f:Panel ID="Panel2" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="false" ShowBorder="true" Title="左侧显示区" Width="280px" Layout="VBox"
                ShowHeader="false" AutoScroll="true" BodyPadding="2px" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Form BodyPadding="1px" ID="formLeft1" EnableCollapse="false" Layout="VBox" runat="server"
                        Title="安全人工时" IconFont="Tag" ShowHeader="true" Height="140px">
                        <Rows>
                            <f:FormRow Margin="5 0 0 0">
                                <Items>
                                    <f:Label ID="lbTotalWorkNum" runat="server" Label="总工时数（万）" LabelWidth="110px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="lbSumPersonNum" runat="server" Label="员工总数" LabelWidth="70px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="lbLossDayNum" runat="server" Label="损失工日" LabelWidth="70px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                    <f:Form BodyPadding="1px" ID="formLeft2" EnableCollapse="false" Layout="VBox" runat="server"
                        Title="安全投入" IconFont="Tag" ShowHeader="true" Height="110px">
                        <Rows>
                            <f:FormRow Margin="5 0 0 0">
                                <Items>
                                    <f:Label ID="lblProductionSafetyInTotal" runat="server" Label="安全生产投入总额/元" LabelWidth="140px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="lblProductionInput" runat="server" Label="百万工时安全生产投入额/万元" LabelWidth="195px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                    <f:Form BodyPadding="1px" ID="formLeft3" EnableCollapse="false" Layout="VBox" runat="server"
                        Title="事故管理" IconFont="Tag" ShowHeader="true" Height="180px">
                        <Rows>
                            <f:FormRow Margin="5 0 0 0">
                                <Items>
                                    <f:Label ID="lbCount5_17" runat="server" Label="可记录事件数" LabelWidth="100px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="lbcout18_23" runat="server" Label="无伤害事件数" LabelWidth="100px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="lbcout24" runat="server" Label="急救包扎" LabelWidth="90px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="lbcout25" runat="server" Label="未遂事件数" LabelWidth="90px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                    <f:Form BodyPadding="1px" ID="formRight1" EnableCollapse="false" Layout="VBox" runat="server"
                        Title="隐患治理" IconFont="Tag" ShowHeader="true" Height="110px">
                        <Rows>
                            <f:FormRow Margin="5 0 0 0">
                                <Items>
                                    <f:Label ID="lbCheckRectifyCount" runat="server" Label="安全隐患数量" LabelWidth="100px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="lbResponseCount" runat="server" Label="响应数量" LabelWidth="70px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                ShowHeader="false" BodyPadding="2px" IconFont="PlusCircle" Title="居中显示区" AutoScroll="true">
                <Items>
                    <f:Form BodyPadding="1px" ID="formSafetyQuarterly" Layout="VBox" EnableCollapse="false"
                        runat="server" Title="安全工时费用率" IconFont="Anchor" ShowHeader="false" Height="180px">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:ContentPanel ShowHeader="false" runat="server" ID="cpSafetyQuarterly" Margin="0 0 0 0">
                                        <div id="divSafetyQuarterly">
                                            <uc1:ChartControl ID="ChartSafetyQuarterly" runat="server" />
                                        </div>
                                    </f:ContentPanel>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                    <f:Form BodyPadding="1px" ID="form2" Layout="VBox" EnableCollapse="false" runat="server"
                        Title="事故率" IconFont="Anchor" ShowHeader="false" Height="180px">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:ContentPanel ShowHeader="false" runat="server" ID="ContentPanel1" Margin="0 0 0 0">
                                        <div id="divAccidentRate">
                                            <uc1:ChartControl ID="ChartAccidentRate" runat="server" />
                                        </div>
                                    </f:ContentPanel>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                    <f:Form BodyPadding="1px" ID="formAccidentCause" Layout="VBox" EnableCollapse="false"
                        runat="server" Title="事故分析" IconFont="Anchor" ShowHeader="false" Height="180px">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:ContentPanel ShowHeader="false" runat="server" ID="ContentPanel2" Margin="0 0 0 0">
                                        <div id="divAccidentCause">
                                            <uc1:ChartControl ID="ChartAccidentCause" runat="server" />
                                        </div>
                                    </f:ContentPanel>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelRightRegion" RegionPosition="Right" RegionSplit="true"
                EnableCollapse="false" Title="右边显示区" ShowBorder="true" Width="280px" ShowHeader="false"
                AutoScroll="true" BodyPadding="2px" IconFont="ArrowCircleRight">
                <Items>
                    <f:Form BodyPadding="1px" ID="formRightx" EnableCollapse="false" Layout="VBox" runat="server"
                        Title="待办事项" IconFont="Tag" ShowHeader="false" Height="210px">
                        <Rows>
                            <f:FormRow Height="205px">
                                <Items>
                                    <f:Grid ID="GridToDoMatter" ShowBorder="false" ShowHeader="false" Title="待办事项" EnableCollapse="true"
                                        ShowGridHeader="false" runat="server" BoxFlex="1" DataKeyNames="Id" AllowCellEditing="true"
                                        EnableColumnLines="true" ClicksToEdit="2" DataIDField="Id" EnableRowDoubleClickEvent="false"
                                        SortField="Date" SortDirection="DESC" AutoScroll="true">
                                        <Columns>                                           
                                            <f:RenderField Width="270px" ColumnID="Name" DataField="Name" FieldType="String"
                                                HeaderText="待办事项" HeaderTextAlign="Center" TextAlign="Left">
                                            </f:RenderField>
                                            <f:RenderField Width="10px" ColumnID="Url" DataField="Url" FieldType="String" HeaderText="路径"
                                                Hidden="true" HeaderTextAlign="Center">                                              
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                    <f:Form BodyPadding="1px" ID="formRight2" EnableCollapse="false" Layout="VBox" runat="server"
                        Title="应急演练" IconFont="Tag" ShowHeader="true" Height="130px">
                        <Rows>
                            <f:FormRow Margin="5 0 0 0">
                                <Items>
                                    <f:Label ID="lbTotalConductCount" runat="server" Label="举办次数" LabelWidth="70px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="lbTotalPeopleCount" runat="server" Label="参演人数" LabelWidth="70px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="lbTotalInvestment" runat="server" Label="直接投入" LabelWidth="70px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                    
                    <f:Form BodyPadding="1px" ID="form3" EnableCollapse="false" Layout="VBox" runat="server"
                        Title="查询条件" IconFont="Tag" ShowHeader="true" Height="200px">
                        <Rows>
                            <f:FormRow Margin="5 0 0 0">
                                <Items>
                                    <f:Label ID="txtUnitName" runat="server" Label="单位" LabelWidth="70px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DropDownList ID="drpYear" runat="server" Label="年度" LabelWidth="70px" AutoPostBack="true"
                                        EnableEdit="true" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DropDownList ID="drpStartMonth" runat="server" Label="开始月份" LabelWidth="70px"
                                        AutoPostBack="true" EnableEdit="true" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:DropDownList ID="drpEndMonth" runat="server" Label="结束月份" LabelWidth="70px" AutoPostBack="true"
                                        EnableEdit="true" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
