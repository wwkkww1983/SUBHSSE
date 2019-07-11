<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckSpecialDetailIn.aspx.cs" Inherits="FineUIPro.Web.Check.CheckSpecialDetailIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Toolbars>
            <f:Toolbar ID="Toolbar2" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>                  
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                    <f:Button ID="btnAudit" Icon="ApplicationEdit" runat="server" ToolTip="数据导入" ValidateForms="SimpleForm1"
                        OnClick="btnAudit_Click">
                    </f:Button>  
                    <f:Button ID="btnDownLoad" runat="server" Icon="ApplicationGo" ToolTip="下载模板" OnClick="btnDownLoad_Click">
                    </f:Button>
                     <f:Button ID="btnClose" EnablePostBack="false" runat="server" Icon="SystemClose" MarginRight="10px">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Rows>
            <f:FormRow>
                <Items>
                    <f:FileUpload runat="server" ID="fuAttachUrl" EmptyText="选择要导入的文件" Label="选择要导入的文件"
                        LabelWidth="150px">
                    </f:FileUpload>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        EnableColumnLines="true" BoxFlex="1" DataKeyNames="CheckSpecialDetailId" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="CheckSpecialDetailId" AllowSorting="true" SortField="CheckItemStr"
                        PageSize="50" Height="350px">
                        <Columns>
                            <f:TemplateField Width="50px" HeaderText="序号">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>                               
                            <f:TemplateField Width="120px" HeaderText="检查类型" HeaderTextAlign="Center" TextAlign="Left"
                                ColumnID="CheckItemType">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# ConvertCheckItemType(Eval("CheckItem")) %>'
                                        ToolTip='<%# ConvertCheckItemType(Eval("CheckItem")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:LinkButtonField Width="110px" HeaderText="检查项" ConfirmTarget="Parent" CommandName="click"
                                 TextAlign="Center"  DataTextField="CheckItemStr" ColumnID="CheckItemStr" />
                            <f:RenderField Width="220px" ColumnID="Unqualified" DataField="Unqualified" SortField="Unqualified"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="不合格项描述">
                            </f:RenderField>
                            <f:RenderField Width="140px" ColumnID="Suggestions" DataField="Suggestions" SortField="Suggestions"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="整改要求">
                            </f:RenderField>
                            <f:RenderField Width="120px" ColumnID="WorkArea" DataField="WorkArea" SortField="WorkArea"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="检查区域">
                            </f:RenderField>
                            <f:RenderField Width="220px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="责任单位">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="HandleStepStr" DataField="HandleStepStr" SortField="HandleStepStr"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="处理措施">
                            </f:RenderField>
                            <f:RenderField Width="90px" ColumnID="LimitedDate" DataField="LimitedDate" SortField="LimitedDate"
                                FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="限时时间"
                                HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                             <f:RenderField Width="90px" ColumnID="CompleteStatusName" DataField="CompleteStatusName" SortField="CompleteStatusName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="整改情况">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:HiddenField ID="hdFileName" runat="server">
                    </f:HiddenField>
                    <f:HiddenField ID="hdCheckResult" runat="server">
                    </f:HiddenField>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Label ID="lblBottom" runat="server" Text="说明：1 导入模板为.xls后缀的EXCEL文件，黑体字为必填项。2 对于导入信息中重复记录自动过滤插入一条记录。3 数据导入完成，成功后自动返回，如果有不成功数据页面弹出提示框，列表显示导入成功数据。">
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Window ID="Window1" Title="导入信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="600px">
    </f:Window>
    </form>
    <script>
        function renderCheckResult(value) {
            return value == true ? '合格' : '不合格';
        }

    </script>
</body>
</html>
