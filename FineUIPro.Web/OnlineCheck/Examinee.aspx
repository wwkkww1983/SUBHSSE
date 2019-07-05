<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Examinee.aspx.cs" Inherits="FineUIPro.Web.OnlineCheck.Examinee" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowHeader="false"
            Layout="HBox">
            <Items>
                 <f:Grid ID="Grid1" ShowHeader="false" EnableCollapse="true" PageSize="10" ShowBorder="true" 
            AllowPaging="true" IsDatabasePaging="true" runat="server"  Width="850px" 
            DataKeyNames="ExamineeId"  DataIDField="ExamineeId" OnPageIndexChange="Grid1_PageIndexChange"
            EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick" TitleToolTip="双击查看考试明细" EnableTextSelection="True">
            <Columns>
               <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                <f:RenderField Width="100px" ColumnID="Account" DataField="Account" FieldType="String"
                    HeaderText="考生帐户">
                </f:RenderField>
                <f:RenderField Width="100px" ColumnID="UserName" DataField="UserName" FieldType="String" HeaderText="考生姓名">
                </f:RenderField>
                <f:RenderField Width="100px" ColumnID="WorkPostName" DataField="WorkPostName" FieldType="String" HeaderText="所考岗位">
                </f:RenderField>
                <f:RenderField Width="100px" ColumnID="ABVolume" DataField="ABVolume" FieldType="String" HeaderText="所考AB卷">
                </f:RenderField>
                <f:RenderCheckField Width="100px" ColumnID="IsChecked" DataField="IsChecked"  HeaderText="是否已考">
                </f:RenderCheckField>
                <f:RenderField Width="100px" ColumnID="StartTime" DataField="StartTime" FieldType="Date"
                      Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="考试日期">
                </f:RenderField>
                <f:RenderField Width="100px" ColumnID="UserTime" DataField="UserTime" FieldType="String"  HeaderText="用时">
                </f:RenderField>
                <f:RenderField Width="80px" ColumnID="TotalScore" DataField="TotalScore" FieldType="Int" HeaderText="成绩">
                </f:RenderField>
                <f:RenderField Width="100px" ColumnID="UserId" DataField="UserId" FieldType="String" Hidden="true" >
                </f:RenderField>
                  <f:RenderField Width="100px" ColumnID="WorkPostId" DataField="WorkPostId" FieldType="String" Hidden="true" >
                </f:RenderField>
            </Columns>
             <Listeners>
                  <f:Listener Event="rowselect" Handler="onGridRowSelect" />
             </Listeners>
            <PageItems>
                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                </f:ToolbarSeparator>
                <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                </f:ToolbarText>
                <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                    <f:ListItem Text="10" Value="10" />
                    <f:ListItem Text="15" Value="15" />
                    <f:ListItem Text="20" Value="20" />
                    <f:ListItem Text="25" Value="25" />
                    <f:ListItem Text="所有行" Value="100000" />
                </f:DropDownList>

            </PageItems>
        </f:Grid>
                <f:SimpleForm ID="SimpleForm1" runat="server" ShowHeader="true" Title="考生所考信息设置" ShowBorder="true" LabelWidth="80px" BodyPadding="5px" Width="240px">
                    <Items>
                        <f:HiddenField ID="hfFormID" runat="server">
                        </f:HiddenField>
                        <f:DropDownList ID="drpUser" Label="考生" EnableSimulateTree="true" CompareMessage="考生不能为空！"
                            runat="server" CompareType="String" CompareValue="null" CompareOperator="NotEqual">
                        </f:DropDownList>
                        <f:DropDownList runat="server" ID="ddlWorkPost" Label="所考岗位" EnableSimulateTree="true" 
                            CompareType="String" CompareValue="null" CompareOperator="NotEqual">
                        </f:DropDownList>
                        <f:DropDownList runat="server" ID="ddlABVolume" Label="所考AB卷" EnableSimulateTree="true" 
                             CompareType="String" CompareValue="null" CompareOperator="NotEqual">
                            <f:ListItem Text="A" Value="A" />
                            <f:ListItem Text="B" Value="B" />
                        </f:DropDownList>
                    </Items>
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" Position="Bottom"  runat="server">
                            <Items>
                                <f:Button ID="btnNew"  Icon="Add" ToolTip="新增" EnablePostBack="false" runat="server">
                                    <Listeners>
                                        <f:Listener Event="click" Handler="onNewButtonClick"/>
                                    </Listeners>
                                </f:Button>
                                <f:Button ID="btnDelete" Enabled="false" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnDelete_Click" runat="server">
                                </f:Button>
                                <f:Button ID="btnSave"
                                    Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1" OnClick="btnSave_Click">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                </f:SimpleForm>
            </Items>
        </f:Panel>
        <br />
          <f:Window ID="Window1" Title="查看试卷" Hidden="true" EnableIFrame="true" EnableMaximize="true"
                Target="Self" EnableResize="true" runat="server"  IsModal="true" AutoScroll="false"
                Width="800px" Height="480px">
            </f:Window>
    </form>
    <script>
        var gridClientID = '<%= Grid1.ClientID %>';
        var btnDeleteClientID = '<%= btnDelete.ClientID %>';
        var btnSaveClientID = '<%= btnSave.ClientID %>';
        var formClientID = '<%= SimpleForm1.ClientID %>';
        var hfFormIDClientID = '<%= hfFormID.ClientID %>';
        var drpUserClientID = '<%= drpUser.ClientID %>';
        var ddlWorkPostClientID = '<%= ddlWorkPost.ClientID %>';
        var ddlABVolumeClientID = '<%= ddlABVolume.ClientID %>';

        function onGridRowSelect(event, rowId) {
            var grid = F(gridClientID);

            // 启用删除按钮
            F(btnDeleteClientID).enable();

            // 当前行数据
            var rowValue = grid.getRowValue(rowId);

            // 使用当前行数据填充表单字段
            F(hfFormIDClientID).setValue(rowId);
            F(drpUserClientID).setValue(rowValue['UserId']);
            F(ddlWorkPostClientID).setValue(rowValue['WorkPostId']);
            F(ddlABVolumeClientID).setValue(rowValue['ABVolume']);

            // 更新保存按钮文本
            F(btnSaveClientID).setText('保存数据');
        }

        function onNewButtonClick() {
            // 重置表单字段
            F(formClientID).reset();
            // 清空表格选中行
            F(gridClientID).clearSelections();
            // 禁用删除按钮
            F(btnDeleteClientID).disable();

            // 更新保存按钮文本
            F(btnSaveClientID).setText('保存数据');
        }
    </script>
</body>
</html>
