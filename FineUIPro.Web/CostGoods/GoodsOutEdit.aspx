<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsOutEdit.aspx.cs" Inherits="FineUIPro.Web.CostGoods.GoodsOutEdit"
    ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑物资出库管理</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="物资出库管理" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtGoodsOutCode" runat="server" Label="出库单号" LabelAlign="Right" MaxLength="50"
                        Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="40% 1% 9% 50%">
                <Items>
                    <f:TextBox ID="txtGoodsDefId" runat="server" Label="物资名称" LabelAlign="Right" MaxLength="50"
                        Readonly="true" Required="true" ShowRedStar="true">
                    </f:TextBox>
                    <f:HiddenField ID="hdGoodsDefId" runat="server">
                    </f:HiddenField>
                    <f:Button ID="btnSearchGoodsDef" Icon="ShapeSquareSelect" runat="server" OnClick="btnSearchGoodsDef_Click">
                    </f:Button>
                    <f:NumberBox ID="txtCounts" runat="server" Label="数量" LabelAlign="Right" NoDecimal="true"
                        NoNegative="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Required="true"
                        ShowRedStar="true">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtOutPerson" runat="server" Label="领用人" LabelAlign="Right" MaxLength="50">
                    </f:TextBox>
                    <f:DatePicker ID="txtOutDate" runat="server" Label="领用时间" LabelAlign="Right" EnableEdit="true">
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
    <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="800px"
        Height="500px" OnClose="Window1_Close">
    </f:Window>
    </form>
</body>
</html>
