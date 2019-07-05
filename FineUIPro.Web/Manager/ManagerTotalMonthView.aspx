<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerTotalMonthView.aspx.cs"
    Inherits="FineUIPro.Web.Manager.ManagerTotalMonthView" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑安全月总结</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCode" runat="server" Label="编号" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtTitle" runat="server" Label="标题" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TabStrip ID="TabStrip1" Height="280px" ShowBorder="true" TabPosition="Top" EnableTabCloseMenu="false"
                        runat="server" AutoScroll="false">
                        <Tabs>
                            <f:Tab ID="Tab1" Title="本月HSE工作基本情况内容" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                    <f:HtmlEditor runat="server" ID="txtMonthContent" ShowLabel="true" Editor="UMEditor"
                                        BasePath="~/res/umeditor/" ToolbarSet="Full" Height="260" LabelAlign="Right">
                                    </f:HtmlEditor>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab2" Title="本月主要完成HSE工作量数据统计" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                    <f:HtmlEditor runat="server" ID="txtMonthContent2" ShowLabel="false" Editor="UMEditor"
                                        BasePath="~/res/umeditor/" ToolbarSet="Full" Height="260" LabelAlign="Right">
                                    </f:HtmlEditor>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab3" Title="本月具体HSE工作开展情况(包括不可接受风险与控制情况)" BodyPadding="5px" Layout="Fit"
                                runat="server">
                                <Items>
                                    <f:HtmlEditor runat="server" ID="txtMonthContent3" ShowLabel="false" Editor="UMEditor"
                                        BasePath="~/res/umeditor/" ToolbarSet="Full" Height="260" LabelAlign="Right">
                                    </f:HtmlEditor>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab4" Title="本月HSE工作存在问题与处理(或拟采取对策）" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="四、本月HSE工作存在问题与处理（或拟采取对策）"
                                        EnableCollapse="false" runat="server" BoxFlex="1" DataKeyNames="ManagerTotalMonthItemId"
                                        AllowCellEditing="true" EnableColumnLines="true" DataIDField="ManagerTotalMonthItemId"
                                        AllowSorting="true" SortField="ActualCompledDate" EnableTextSelection="True">
                                        <Columns>
                                            <f:TemplateField ColumnID="Number" HeaderText="序号" Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </f:TemplateField>
                                            <f:RenderField Width="200px" ColumnID="ExistenceHiddenDanger" DataField="ExistenceHiddenDanger"
                                                HeaderText="存在隐患">
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="CorrectiveActions" DataField="CorrectiveActions"
                                                HeaderText="整改措施">
                                            </f:RenderField>
                                            <f:RenderField Width="120px" ColumnID="PlanCompletedDate" DataField="PlanCompletedDate"
                                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="计划完成时间">
                                            </f:RenderField>
                                            <f:RenderField Width="100px" ColumnID="ResponsiMan" DataField="ResponsiMan" HeaderText="责任人">
                                            </f:RenderField>
                                            <f:RenderField Width="120px" ColumnID="ActualCompledDate" DataField="ActualCompledDate"
                                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="实际完成时间">
                                            </f:RenderField>
                                            <f:RenderField Width="300px" ColumnID="Remark" DataField="Remark" HeaderText="备注">
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab5" Title="下月工作计划" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                    <f:HtmlEditor runat="server" ID="txtMonthContent5" ShowLabel="false" Editor="UMEditor"
                                        BasePath="~/res/umeditor/" ToolbarSet="Full" Height="260" LabelAlign="Right">
                                    </f:HtmlEditor>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab6" Title="其他" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                    <f:HtmlEditor runat="server" ID="txtMonthContent6" ShowLabel="false" Editor="UMEditor"
                                        BasePath="~/res/umeditor/" ToolbarSet="Full" Height="260" LabelAlign="Right">
                                    </f:HtmlEditor>
                                </Items>
                            </f:Tab>
                        </Tabs>
                    </f:TabStrip>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCompileDate" runat="server" Label="整理日期" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="drpCompileMan" runat="server" Label="整理人" LabelAlign="Right" Readonly="true">
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
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
