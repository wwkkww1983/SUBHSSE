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
                    <f:GroupPanel ID="GroupPanel47" Layout="Anchor" Title="8.其他工作情况" runat="server">
                        <Items>
                            <f:Grid ID="gvOtherWork" ShowBorder="true" ShowHeader="false" Title="其他工作情况" runat="server"
                                AllowCellEditing="true" ClicksToEdit="1" DataIDField="OtherWorkId" DataKeyNames="OtherWorkId"
                                EnableMultiSelect="false" ShowGridHeader="true" Height="420px" EnableColumnLines="true"
                                AutoScroll="true" OnRowCommand="gvOtherWork_RowCommand">
                                <Toolbars>
                                    <f:Toolbar ID="Toolbar18" Position="Top" runat="server" ToolbarAlign="Right">
                                        <Items>
                                            <f:Button ID="btnNewOtherWork" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewOtherWork_Click">
                                            </f:Button>
                                        </Items>
                                    </f:Toolbar>
                                </Toolbars>
                                <Columns>
                                    <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                        ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                        TextAlign="Center" />
                                    <f:RenderField Width="300px" ColumnID="WorkContentDes" DataField="WorkContentDes"
                                        FieldType="String" HeaderText="工作内容描述" HeaderTextAlign="Center" TextAlign="Left"
                                        ExpandUnusedSpace="true">
                                        <Editor>
                                            <f:TextBox runat="server" ID="TextBox48">
                                            </f:TextBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:Grid>
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
