<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCEdit5.aspx.cs"
    Inherits="FineUIPro.Web.Manager.MonthReportCEdit5" %>

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
                    <f:GroupPanel ID="GroupPanel34" Layout="Anchor" Title="5.本月项目HSE内业管理" runat="server">
                        <Items>
                            <f:GroupPanel ID="GroupPanel37" Layout="Anchor" Title="5.1 本月文件、方案修编情况说明（修编文件、方案名称，修编基本情况等）"
                                runat="server">
                                <Items>
                                    <f:Grid ID="gvMonthPlan" ShowBorder="true" ShowHeader="false" Title="本月文件、方案修编情况说明"
                                        runat="server" AllowCellEditing="true" ClicksToEdit="1" DataIDField="PlanId"
                                        DataKeyNames="PlanId" EnableMultiSelect="false" ShowGridHeader="true" Height="220px"
                                        EnableColumnLines="true" OnRowCommand="gvMonthPlan_RowCommand">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar15" Position="Top" runat="server" ToolbarAlign="Right">
                                                <Items>
                                                    <f:Button ID="btnNewMonthPlan" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewMonthPlan_Click">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Parent" CommandName="Delete"
                                                ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="300px" ColumnID="PlanName" DataField="PlanName" FieldType="String"
                                                HeaderText="本月修编的方案/文件名称" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox36">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="CompileMan" DataField="CompileMan" FieldType="String"
                                                HeaderText="修编人" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox37">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="CompileDate" DataField="CompileDate" FieldType="String"
                                                HeaderText="发布时间" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox38">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel38" Layout="Anchor" Title="5.2 HSE资质、方案审查记录" runat="server">
                                <Items>
                                    <f:Grid ID="gvReviewRecord" ShowBorder="true" ShowHeader="false" Title="详细审查记录" runat="server"
                                        AllowCellEditing="true" ClicksToEdit="1" DataIDField="ReviewRecordId" DataKeyNames="ReviewRecordId"
                                        EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                                        OnRowCommand="gvReviewRecord_RowCommand">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar16" Position="Top" runat="server" ToolbarAlign="Right">
                                                <Items>
                                                    <f:Button ID="btnNewReviewRecord" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewReviewRecord_Click">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Parent" CommandName="Delete"
                                                ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="300px" ColumnID="ReviewRecordName" DataField="ReviewRecordName"
                                                FieldType="String" HeaderText="本月审查方案、资质文件名称" HeaderTextAlign="Center" TextAlign="Left"
                                                ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox39">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="ReviewMan" DataField="ReviewMan" FieldType="String"
                                                HeaderText="审查人" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox40">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="ReviewDate" DataField="ReviewDate" FieldType="String"
                                                HeaderText="审查时间" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox41">
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
