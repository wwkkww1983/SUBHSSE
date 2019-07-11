<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCEdit8.aspx.cs" Inherits="FineUIPro.Web.Manager.MonthReportCEdit8" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server">
    </f:PageManager>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel48" Layout="Anchor" Title="8.下月工作计划" runat="server">
                        <Items>
                            <f:GroupPanel ID="GroupPanel49" Layout="Anchor" Title="8.1 危险源动态识别及控制" runat="server">
                                <Items>
                                    <f:Grid ID="gvHazard" ShowBorder="true" ShowHeader="false" Title="其他工作情况" runat="server"
                                        AllowCellEditing="true" ClicksToEdit="1" DataIDField="HazardId" DataKeyNames="HazardId"
                                        EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                                         OnRowCommand="gvHazard_RowCommand">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar19" Position="Top" runat="server" ToolbarAlign="Right">
                                                <Items>
                                                    <f:Button ID="btnNewHazard" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewHazard_Click">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Parent" CommandName="Delete"
                                                ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="250px" ColumnID="WorkArea" DataField="WorkArea" FieldType="String"
                                                HeaderText="下月计划施工的区域" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox49">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="250px" ColumnID="Subcontractor" DataField="Subcontractor" FieldType="String"
                                                HeaderText="所属分包商" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox50">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="250px" ColumnID="DangerousSource" DataField="DangerousSource"
                                                FieldType="String" HeaderText="可能存在的危险源" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox51">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="250px" ColumnID="ControlMeasures" DataField="ControlMeasures"
                                                ExpandUnusedSpace="true" FieldType="String" HeaderText="计划采取的控制措施" HeaderTextAlign="Center"
                                                TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox52">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel51" Layout="Anchor" Title="8.2 HSE检查" runat="server">
                                <Items>
                                    <f:Grid ID="gvCheck" ShowBorder="true" ShowHeader="false" Title="HSE检查" runat="server"
                                        AllowCellEditing="true" ClicksToEdit="1" DataIDField="CheckId" DataKeyNames="CheckId"
                                        EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                                         OnRowCommand="gvCheck_RowCommand">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar21" Position="Top" runat="server" ToolbarAlign="Right">
                                                <Items>
                                                    <f:Button ID="btnNewCheck" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewCheck_Click">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Parent" CommandName="Delete"
                                                ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="300px" ColumnID="CheckType" DataField="CheckType" FieldType="String"
                                                HeaderText="下月计划开展的检查类型" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox58">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="Inspectors" DataField="Inspectors" FieldType="String"
                                                HeaderText="参检人员" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox59">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="CheckDate" DataField="CheckDate" FieldType="String"
                                                HeaderText="计划开展时间" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox60">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                                HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox62">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel54" Layout="Anchor" Title="8.3 应急管理（对下月应急管理工作进行描述）" runat="server">
                                <Items>
                                    <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                        <Rows>
                                            <f:FormRow>
                                                <Items>
                                                    <f:TextArea runat="server" ID="txtNextEmergencyResponse" Label="">
                                                    </f:TextArea>
                                                </Items>
                                            </f:FormRow>
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel58" Layout="Anchor" Title="8.4 HSE管理文件/方案修编计划" runat="server">
                                <Items>
                                    <f:Grid ID="gvManageDocPlan" ShowBorder="true" ShowHeader="false" Title="HSE管理文件/方案修编计划"
                                        runat="server" AllowCellEditing="true" ClicksToEdit="1" DataIDField="ManageDocPlanId"
                                        DataKeyNames="ManageDocPlanId" EnableMultiSelect="false" ShowGridHeader="true"
                                        Height="220px" EnableColumnLines="true"  OnRowCommand="gvManageDocPlan_RowCommand">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar26" Position="Top" runat="server" ToolbarAlign="Right">
                                                <Items>
                                                    <f:Button ID="btnNewManageDocPlan" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewManageDocPlan_Click">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Parent" CommandName="Delete"
                                                ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="400px" ColumnID="ManageDocPlanName" DataField="ManageDocPlanName"
                                                FieldType="String" HeaderText="下月计划修编的方案/文件名称" HeaderTextAlign="Center" TextAlign="Left"
                                                ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox78">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="CompileMan" DataField="CompileMan" FieldType="String"
                                                HeaderText="修编人" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox79">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="CompileDate" DataField="CompileDate" FieldType="String"
                                                HeaderText="计划完成时间" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox80">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel59" Layout="Anchor" Title="8.5 其他HSE工作计划" runat="server">
                                <Items>
                                    <f:Grid ID="gvOtherWorkPlan" ShowBorder="true" ShowHeader="false" Title="其他HSE工作计划"
                                        runat="server" AllowCellEditing="true" ClicksToEdit="1" DataIDField="OtherWorkPlanId"
                                        DataKeyNames="OtherWorkPlanId" EnableMultiSelect="false" ShowGridHeader="true"
                                        Height="220px" EnableColumnLines="true"  OnRowCommand="gvOtherWorkPlan_RowCommand">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar27" Position="Top" runat="server" ToolbarAlign="Right">
                                                <Items>
                                                    <f:Button ID="btnNewOtherWorkPlan" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewOtherWorkPlan_Click">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Parent" CommandName="Delete"
                                                ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="400px" ColumnID="WorkContent" DataField="WorkContent" FieldType="String"
                                                HeaderText="计划工作内容描述" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox81">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
