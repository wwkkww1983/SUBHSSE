<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsManageEdit.aspx.cs"
    Inherits="FineUIPro.Web.CostGoods.GoodsManageEdit" ValidateRequest="false" %>  
<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑物资管理</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="物资管理" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtGoodsCode" runat="server" Label="物资编号" LabelAlign="Right" MaxLength="50"
                        Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtGoodsName" runat="server" Label="物资名称" LabelAlign="Right" MaxLength="30"
                        Required="true" ShowRedStar="true" FocusOnPageLoad="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpUnitId" runat="server" Label="所属单位" LabelAlign="Right" EnableEdit="true"
                        Required="true" ShowRedStar="true">
                    </f:DropDownList>
                    <f:DropDownList ID="drpGoodsCategory" runat="server" Label="物资类别" LabelAlign="Right"
                        EnableEdit="true" Required="true" ShowRedStar="true">                       
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSizeModel" runat="server" Label="规格型号" LabelAlign="Right" MaxLength="30">
                    </f:TextBox>
                    <f:TextBox ID="txtFactoryCode" runat="server" Label="出厂编号" LabelAlign="Right" MaxLength="30">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtCheckDate" runat="server" Label="报验日期" LabelAlign="Right" EnableEdit="true">
                    </f:DatePicker>
                    <f:NumberBox ID="txtEnableYear" runat="server" Label="有效年限" LabelAlign="Right" NoDecimal="true"
                        NoNegative="true" Required="true" ShowRedStar="true">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpCheckPerson" runat="server" Label="检查/审批人" LabelAlign="Right"
                        EnableEdit="true">
                    </f:DropDownList>
                    <f:DatePicker ID="txtInTime" runat="server" Label="入场时间" LabelAlign="Right" EnableEdit="true">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" LabelAlign="Right" MaxLength="150">
                    </f:TextArea>
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
