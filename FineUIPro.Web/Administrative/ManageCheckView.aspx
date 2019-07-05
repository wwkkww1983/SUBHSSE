<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageCheckView.aspx.cs"
    Inherits="FineUIPro.Web.Administrative.ManageCheckView" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看行政管理检查记录</title>
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
                    <f:TextBox ID="txtManageCheckCode" runat="server" Label="编号" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckTypeCode" runat="server" Label="检查类别" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="常规检查" EnableCollapse="true"
                        runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="CheckTypeCode"
                        AllowCellEditing="true" ClicksToEdit="2" DataIDField="CheckTypeCode" Hidden="true">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                TextAlign="Center" />
                            <f:TemplateField HeaderText="检查内容" Width="700px" HeaderTextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCheckTypeCode" runat="server" Text='<%# ConvertCheckType(Eval("CheckTypeCode")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="200px" ColumnID="IsCheck" DataField="IsCheck" FieldType="Boolean"
                                RendererFunction="renderIsCheck" HeaderText="是否检查" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSupplyCheck" runat="server" Label="补充检查" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtIsSupplyCheck" runat="server" Label="是否补充检查" LabelAlign="Right"
                        Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtViolationRule" runat="server" Label="违章处理" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:Label ID="Label1" runat="server">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckPerson" runat="server" Label="检查人" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtCheckTime" runat="server" Label="检查日期" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtVerifyPerson" runat="server" Label="验证人" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtVerifyTime" runat="server" Label="验证日期" LabelAlign="Right" Readonly="true">
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
<script type="text/javascript">
    function renderIsCheck(value) {
        return value == true ? '是' : '否';
    }
</script>
