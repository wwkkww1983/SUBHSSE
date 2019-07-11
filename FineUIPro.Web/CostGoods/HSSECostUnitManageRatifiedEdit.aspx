<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSECostUnitManageRatifiedEdit.aspx.cs" Inherits="FineUIPro.Web.CostGoods.HSSECostUnitManageRatifiedEdit"
    ValidateRequest="false" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>核定安全分包费用</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="安全分包费用" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Left"  Layout="VBox">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true"
                        Layout="VBox">
                        <Rows>
                             <f:FormRow ColumnWidths="60% 40%">
                                <Items>
                                    <f:Label ID="lbUnit" runat="server" Label="单位" LabelWidth="100px">
                                    </f:Label>
                                    <f:Label ID="lbYearMonth" runat="server" Label="月份" LabelWidth="100px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="30% 30% 40%">
                                <Items>
                                    <f:Label runat="server" ID="txtCompileMan" Label="编制人" LabelWidth="100px"></f:Label>
                                    <f:Label ID="txtCompileDate" runat="server" Label="填报日期" LabelWidth="100px">
                                    </f:Label> 
                                     <f:DropDownList runat="server" ID="drpAuditManId" Label="总包财务经理" LabelWidth="120px">
                                     </f:DropDownList>
                                </Items>
                            </f:FormRow>
                        </Rows>
                   </f:Form>
                </Items>
            </f:FormRow>             
             <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="Label58" Text="编号">
                    </f:Label>
                       <f:Label runat="server" ID="Label4" Text="类型">
                    </f:Label>
                    <f:Label runat="server" ID="Label1" Text="费用（万元）">
                    </f:Label>
                    <f:Label runat="server" ID="Label22" Text="审核费用">
                    </f:Label>
                    <f:Label runat="server" ID="Label24" Text="核定费用">
                    </f:Label>
                    <f:Label runat="server" ID="Label3" Text="费用明细">
                    </f:Label>
                    <f:Label runat="server" ID="Label23" Text="审核状态">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow >
                <Items>
                    <f:Label runat="server" ID="Label5" Text="A1" MarginLeft="30">
                    </f:Label>
                    <f:Label runat="server" ID="Label6" Text="基础管理" >
                    </f:Label>
                    <f:NumberBox ID="txtA1" DecimalPrecision="4" MinValue="0" runat="server" 
                        Readonly="true" >
                    </f:NumberBox>
                     <f:NumberBox ID="txtAuditCostA1" DecimalPrecision="4" MinValue="0" runat="server" 
                        Readonly="true" >
                    </f:NumberBox>
                    <f:NumberBox ID="txtRatifiedCostA1" DecimalPrecision="4" MinValue="0" runat="server" 
                        Readonly="true" >
                    </f:NumberBox>
                    <f:Button ID="btnA1" Text="核定" ToolTip="费用维护及查看" Icon="TableCell" runat="server"
                        OnClick="btnUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button> 
                     <f:Label runat="server" ID="lbA1">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow >
                <Items>
                    <f:Label runat="server" ID="Label2" Text="A2" MarginLeft="30">
                    </f:Label>
                    <f:Label runat="server" ID="Label7" Text="安全奖励" >
                    </f:Label>
                    <f:NumberBox ID="txtA2" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                    <f:NumberBox ID="txtAuditCostA2" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                    <f:NumberBox ID="txtRatifiedCostA2" DecimalPrecision="4" MinValue="0" runat="server" 
                        Readonly="true" >
                    </f:NumberBox>
                    <f:Button ID="btnA2" Text="核定" ToolTip="费用维护及查看" Icon="TableCell" runat="server"
                        OnClick="btnUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button> 
                    <f:Label runat="server" ID="lbA2">
                    </f:Label>
                </Items>
            </f:FormRow> 
             <f:FormRow >
                <Items>
                    <f:Label runat="server" ID="Label8" Text="A3" MarginLeft="30">
                    </f:Label>
                    <f:Label runat="server" ID="Label9" Text="安全技术" >
                    </f:Label>
                    <f:NumberBox ID="txtA3" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                     <f:NumberBox ID="txtAuditCostA3" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                    <f:NumberBox ID="txtRatifiedCostA3" DecimalPrecision="4" MinValue="0" runat="server" 
                        Readonly="true" >
                    </f:NumberBox>
                    <f:Button ID="btnA3" Text="核定" ToolTip="费用维护及查看" Icon="TableCell" runat="server"
                        OnClick="btnUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button> 
                    <f:Label runat="server" ID="lbA3">
                    </f:Label>
                </Items>
            </f:FormRow> 
             <f:FormRow >
                <Items>
                    <f:Label runat="server" ID="Label10" Text="A4" MarginLeft="30">
                    </f:Label>
                    <f:Label runat="server" ID="Label11" Text="职业健康">
                    </f:Label>
                    <f:NumberBox ID="txtA4" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                     <f:NumberBox ID="txtAuditCostA4" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                    <f:NumberBox ID="txtRatifiedCostA4" DecimalPrecision="4" MinValue="0" runat="server" 
                        Readonly="true" >
                    </f:NumberBox>
                    <f:Button ID="btnA4" Text="核定" ToolTip="费用维护及查看" Icon="TableCell" runat="server"
                        OnClick="btnUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button> 
                    <f:Label runat="server" ID="lbA4">
                    </f:Label>
                </Items>
            </f:FormRow>
             <f:FormRow >
                <Items>
                        <f:Label runat="server" ID="Label12" Text="A5" MarginLeft="30">
                    </f:Label>
                    <f:Label runat="server" ID="Label13" Text="防护措施" >
                    </f:Label>
                    <f:NumberBox ID="txtA5" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                     <f:NumberBox ID="txtAuditCostA5" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                    <f:NumberBox ID="txtRatifiedCostA5" DecimalPrecision="4" MinValue="0" runat="server" 
                        Readonly="true" >
                    </f:NumberBox>
                    <f:Button ID="btnA5" Text="核定" ToolTip="费用维护及查看" Icon="TableCell" runat="server"
                        OnClick="btnUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button> 
                    <f:Label runat="server" ID="lbA5">
                    </f:Label>
                </Items>
            </f:FormRow>
             <f:FormRow >
                <Items>
                        <f:Label runat="server" ID="Label14" Text="A6" MarginLeft="30">
                    </f:Label>
                    <f:Label runat="server" ID="Label15" Text="应急管理" >
                    </f:Label>
                    <f:NumberBox ID="txtA6" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                     <f:NumberBox ID="txtAuditCostA6" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                    <f:NumberBox ID="txtRatifiedCostA6" DecimalPrecision="4" MinValue="0" runat="server" 
                        Readonly="true" >
                    </f:NumberBox>
                    <f:Button ID="btnA6" Text="核定" ToolTip="费用维护及查看" Icon="TableCell" runat="server"
                        OnClick="btnUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button> 
                    <f:Label runat="server" ID="lbA6">
                    </f:Label>
                </Items>
            </f:FormRow>
             <f:FormRow >
                <Items>
                        <f:Label runat="server" ID="Label16" Text="A7" MarginLeft="30">
                    </f:Label>
                    <f:Label runat="server" ID="Label17" Text="化工试车" >
                    </f:Label>
                    <f:NumberBox ID="txtA7" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                     <f:NumberBox ID="txtAuditCostA7" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                    <f:NumberBox ID="txtRatifiedCostA7" DecimalPrecision="4" MinValue="0" runat="server" 
                        Readonly="true" >
                    </f:NumberBox>
                    <f:Button ID="btnA7" Text="核定" ToolTip="费用维护及查看" Icon="TableCell" runat="server"
                        OnClick="btnUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button> 
                    <f:Label runat="server" ID="lbA7">
                    </f:Label>
                </Items>
            </f:FormRow>
             <f:FormRow >
                <Items>
                    <f:Label runat="server" ID="Label18" Text="A8" MarginLeft="30">
                    </f:Label>
                    <f:Label runat="server" ID="Label19" Text="教育培训" >
                    </f:Label>
                    <f:NumberBox ID="txtA8" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                     <f:NumberBox ID="txtAuditCostA8" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                    <f:NumberBox ID="txtRatifiedCostA8" DecimalPrecision="4" MinValue="0" runat="server" 
                        Readonly="true" >
                    </f:NumberBox>
                    <f:Button ID="btnA8" Text="核定" ToolTip="费用维护及查看" Icon="TableCell" runat="server"
                        OnClick="btnUrl_Click" ValidateForms="SimpleForm1">
                    </f:Button>
                    <f:Label runat="server" ID="lbA8">
                    </f:Label>
                </Items>
            </f:FormRow>
             <f:FormRow >
                <Items>
                     <f:Label runat="server" ID="Label20" Text="∑A" MarginLeft="30">
                    </f:Label>
                    <f:Label runat="server" ID="Label21" Text="安全生产费用合计">
                    </f:Label>
                    <f:NumberBox ID="txtAAll"  MinValue="0" runat="server" Label="" Readonly="true">
                    </f:NumberBox>
                     <f:NumberBox ID="txtAuditCostAAll" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                    <f:NumberBox ID="txtRatifiedCostAAll" DecimalPrecision="4" MinValue="0" runat="server"
                        Readonly="true" >
                    </f:NumberBox>
                   <f:Label runat="server" ID="lbtempA"></f:Label>
                </Items>
            </f:FormRow>             
             <f:FormRow runat="server" Hidden="true">
                <Items>
                    <f:ContentPanel ID="ContentPanel1" runat="server" ShowHeader="false" EnableCollapse="true"
                        BodyPadding="0px">
                        <uc1:FlowOperateControl ID="ctlAuditFlow" runat="server" />
                    </f:ContentPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server" Margin="0 50 30 50">
                <Items>     
                    <f:HiddenField runat="server" ID="hdHSSECostUnitManageId"></f:HiddenField>
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
   <f:Window ID="Window1" Title="费用明细维护" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" OnClose="Window1_Close"
        Width="1150px" Height="550px">
   </f:Window>
    </form>
</body>
</html>
