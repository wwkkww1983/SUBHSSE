<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarManagerEdit.aspx.cs" Inherits="FineUIPro.Web.Administrative.CarManagerEdit" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>现场车辆管理</title>
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
                    <f:TextBox ID="txtCarManagerCode" runat="server" Label="编号" LabelAlign="Right"
                        MaxLength="50" Required="true" ShowRedStar="true" Readonly="true" LabelWidth="120px">
                    </f:TextBox>
                    <f:TextBox ID="txtCarName" runat="server" Label="车牌号" LabelAlign="Right" MaxLength="50"
                        LabelWidth="120px" FocusOnPageLoad="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>            
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCarModel" runat="server" Label="车型" LabelAlign="Right" MaxLength="50"
                        LabelWidth="120px">
                    </f:TextBox>
                    <f:DatePicker ID="txtBuyDate" runat="server" Label="购买日期" LabelAlign="Right" EnableEdit="true"
                        LabelWidth="120px">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:DatePicker ID="txtLastYearCheckDate" runat="server" Label="年检有效期" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="120px">
                    </f:DatePicker>
                    <f:DatePicker ID="txtInsuranceDate" runat="server" Label="保险有效期" LabelAlign="Right"
                        EnableEdit="true" LabelWidth="120px">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" LabelAlign="Right" MaxLength="500"
                        LabelWidth="120px">
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
