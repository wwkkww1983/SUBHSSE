<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageCheckEdit.aspx.cs"
    Inherits="FineUIPro.Web.Administrative.ManageCheckEdit" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑行政管理检查记录</title>
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
                    <f:TextBox ID="txtManageCheckCode" runat="server" Label="编号" LabelAlign="Right" MaxLength="30"
                        Readonly="true">
                    </f:TextBox>
                    <f:DropDownList ID="drpCheckTypeCode" runat="server" Label="检查类别" LabelAlign="Right"
                        EnableEdit="true" AutoPostBack="True" OnSelectedIndexChanged="drpCheckTypeCode_SelectedIndexChanged">
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="true" Title="常规检查" EnableCollapse="true"
                        runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="CheckTypeCode"
                        AllowCellEditing="true" ClicksToEdit="2" DataIDField="CheckTypeCode" Hidden="true"
                        OnRowDataBound="Grid1_RowDataBound">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                TextAlign="Center" />
                            <f:TemplateField HeaderText="检查内容" Width="700px" HeaderTextAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCheckTypeCode" runat="server" Text='<%# ConvertCheckType(Eval("CheckTypeCode")) %>'></asp:Label>
                                    <asp:HiddenField ID="hdCheckTypeCode" runat="server" Value='<%#Bind("CheckTypeCode") %>' />
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:TemplateField HeaderText="是否检查" Width="200px" HeaderTextAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpIsCheck" runat="server">
                                        <asp:ListItem Value="True" Text="是"></asp:ListItem>
                                        <asp:ListItem Value="False" Text="否"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdIsCheck" runat="server" Value='<%#Bind("IsCheck") %>' />
                                </ItemTemplate>
                            </f:TemplateField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSupplyCheck" runat="server" Label="补充检查" LabelAlign="Right" MaxLength="150">
                    </f:TextBox>
                    <f:DropDownList ID="drpIsSupplyCheck" runat="server" Label="是否补充检查" LabelAlign="Right"
                        EnableEdit="true" AutoPostBack="True" OnSelectedIndexChanged="drpIsSupplyCheck_SelectedIndexChanged">
                        <f:ListItem Value="True" Text="是" />
                        <f:ListItem Value="False" Text="否" />
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpViolationRule" runat="server" Label="违章处理" LabelAlign="Right"
                        EnableEdit="true">
                    </f:DropDownList>
                    <f:Label ID="Label1" runat="server">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCheckPerson" runat="server" Label="检查人" LabelAlign="Right" MaxLength="50">
                    </f:TextBox>
                    <f:DatePicker ID="txtCheckTime" runat="server" Label="检查日期" LabelAlign="Right" EnableEdit="true">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtVerifyPerson" runat="server" Label="验证人" LabelAlign="Right" MaxLength="50">
                    </f:TextBox>
                    <f:DatePicker ID="txtVerifyTime" runat="server" Label="验证日期" LabelAlign="Right" EnableEdit="true">
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
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
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
