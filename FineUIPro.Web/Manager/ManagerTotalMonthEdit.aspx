<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerTotalMonthEdit.aspx.cs" Inherits="FineUIPro.Web.Manager.ManagerTotalMonthEdit"  ValidateRequest="false" %>
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑HSSE月总结</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" Title="安全月总结"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                     <f:TextBox ID="txtCode" runat="server" Label="编号" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtTitle" runat="server" Label="标题" LabelAlign="Right" MaxLength="50"
                        Required="true" ShowRedStar="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>           
            <f:FormRow>
                <Items>
                    <f:TabStrip ID="TabStrip1" Height="280px" ShowBorder="true" TabPosition="Top"
                        EnableTabCloseMenu="false" runat="server" AutoScroll="false" >
                        <Tabs>
                            <f:Tab ID="Tab1" Title="本月HSE工作基本情况内容" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                     <f:HtmlEditor runat="server" ID="txtMonthContent" ShowLabel="true"
                                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="260" LabelAlign="Right">
                                    </f:HtmlEditor>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab2" Title="本月主要完成HSE工作量数据统计" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                     <f:HtmlEditor runat="server" ID="txtMonthContent2" ShowLabel="false"
                                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="260" LabelAlign="Right">
                                    </f:HtmlEditor>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab3" Title="本月具体HSE工作开展情况(包括不可接受风险与控制情况)" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                     <f:HtmlEditor runat="server" ID="txtMonthContent3" ShowLabel="false"
                                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="260" LabelAlign="Right">
                                    </f:HtmlEditor>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab4" Title="本月HSE工作存在问题与处理(或拟采取对策）" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                     <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="四、本月HSE工作存在问题与处理（或拟采取对策）(双击编辑)"
                                        EnableCollapse="false" runat="server" BoxFlex="1" DataKeyNames="ManagerTotalMonthItemId"
                                        AllowCellEditing="true" EnableColumnLines="true" ClicksToEdit="2" DataIDField="ManagerTotalMonthItemId"
                                        AllowSorting="true" SortField="ActualCompledDate" 
                                        OnSort="Grid1_Sort" EnableTextSelection="True">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar2" runat="server">
                                                <Items>
                                                    <f:Label ID="Label8" runat="server" Text="(双击编辑)">
                                                    </f:Label>
                                                    <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                                    </f:ToolbarFill>
                                                    <f:Button ID="btnNew" Icon="Add" EnablePostBack="false" runat="server">
                                                    </f:Button>
                                                    <f:Button ID="btnDelete" Icon="Delete" runat="server" OnClick="btnDelete_OnClick">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:TemplateField ColumnID="Number" HeaderText="序号" Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </f:TemplateField>
                                            <f:RenderField Width="200px" ColumnID="ExistenceHiddenDanger" DataField="ExistenceHiddenDanger"
                                                HeaderText="存在隐患" ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox ID="txtExistenceHiddenDanger" runat="server" MaxLength="150">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="CorrectiveActions" DataField="CorrectiveActions"
                                                HeaderText="整改措施">
                                                <Editor>
                                                    <f:TextBox ID="txtCorrectiveActions" runat="server" MaxLength="150">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="120px" ColumnID="PlanCompletedDate" DataField="PlanCompletedDate"
                                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="计划完成时间">
                                                <Editor>
                                                    <f:DatePicker ID="txtPlanCompletedDate" Required="true" runat="server">
                                                    </f:DatePicker>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="100px" ColumnID="ResponsiMan" DataField="ResponsiMan" HeaderText="责任人">
                                                <Editor>
                                                    <f:TextBox ID="txtResponsiMan" runat="server" MaxLength="50">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="120px" ColumnID="ActualCompledDate" DataField="ActualCompledDate"
                                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="实际完成时间">
                                                <Editor>
                                                    <f:DatePicker ID="txtActualCompledDate" Required="true" runat="server">
                                                    </f:DatePicker>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="300px" ColumnID="Remark" DataField="Remark" HeaderText="备注">
                                                <Editor>
                                                    <f:TextBox ID="txtRemark" runat="server" MaxLength="200">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                           <%-- <f:LinkButtonField ColumnID="Delete" Width="50px" EnablePostBack="false" Icon="Delete"
                                                HeaderTextAlign="Center" HeaderText="删除" />--%>
                                           <f:RenderField Width="1px" ColumnID="ManagerTotalMonthItemId" DataField="ManagerTotalMonthItemId" 
                                                FieldType="String" HeaderText="主键"  Hidden="true" HeaderTextAlign="Center">                                        
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab5" Title="下月工作计划" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                     <f:HtmlEditor runat="server" ID="txtMonthContent5" ShowLabel="false"
                                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="260" LabelAlign="Right">
                                    </f:HtmlEditor>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab6" Title="其他" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                     <f:HtmlEditor runat="server" ID="txtMonthContent6" ShowLabel="false"
                                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="260" LabelAlign="Right">
                                    </f:HtmlEditor>
                                </Items>
                            </f:Tab>
                        </Tabs>
                    </f:TabStrip>                   
                </Items>
            </f:FormRow>            
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtCompileDate" runat="server" Label="整理日期" LabelAlign="Right" EnableEdit="true">
                    </f:DatePicker>
                    <f:DropDownList ID="drpCompileMan" runat="server" Label="整理人" LabelAlign="Right">
                    </f:DropDownList>
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
    </form>
</body>
</html>
