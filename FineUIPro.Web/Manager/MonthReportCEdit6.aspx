<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCEdit6.aspx.cs" Inherits="FineUIPro.Web.Manager.MonthReportCEdit6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>本月项目HSE费用管理</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .font_style
        {
            font-weight: bold;
        }
        
        .tablehtml td.label1, .tablehtml td.label2
        {
            width: 80px;
            text-align: center;
        }
        
        .tablehtml td.label
        {
            width: 120px;
            text-align: center;
        }
        
        .tablehtml td.label3
        {
            width: 80px;
            text-align: center;
        }
        
        .tablehtml
        {
            width: 100%;
            border-collapse: collapse;
            border-spacing: 0;
        }
        
        .tablehtml td
        {
            border-width: 1px;
            border-style: solid;
            padding: 0;
        }
        
        .tablehtml td.label .redstar
        {
            color: red;
        }
        
        .tablehtml td > div
        {
            display: block;
            vertical-align: top;
        }
        
        .tablehtml .f-field
        {
            width: 100%;
            margin-bottom: 0 !important;
        }
        
        .tablehtml .f-field-textbox, .tablehtml .f-field-label
        {
            border-radius: 0;
            margin: 0;
            border-width: 0;
            padding: 7px 6px;
        }
        
        .tablehtml .f-field-checkbox-icon
        {
            margin-left: 6px;
        }
        
        .tablehtml .f-field-body-checkboxlabel
        {
            margin-left: 26px;
        }
        
        .tablehtml td.label
        {
            width: 100px;
            text-align: center;
        }
        
        .tablehtml td.content
        {
            width: 200px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server">
    </f:PageManager>
    <f:Panel ID="Panel1" runat="server" Width="970px" ShowBorder="True" EnableCollapse="true"
        CssClass="mytable" Layout="Table" TableConfigColumns="3" ShowHeader="True" Title="6 本月项目HSE费用管理（单位：万元）">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:ContentPanel ID="ContentPanel1" IsFluid="true" CssClass="blockpanel" runat="server"
                ShowBorder="false" ShowHeader="false">
                <table class="tablehtml">
                    <tr>
                        <td class="f-widget-content label1" rowspan="2">
                            序号
                        </td>
                        <td class="f-widget-content label2" rowspan="2">
                            投入项目
                        </td>
                        <td class="f-widget-content label" colspan="2">
                            五环工程
                        </td>
                        <td class="f-widget-content label" colspan="2">
                           施工分包商
                        </td>
                        <td class="f-widget-content label" colspan="2">
                           建安产值
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label1" >
                            本月
                        </td>
                        <td class="f-widget-content label2" >
                            项目累计
                        </td>
                        <td class="f-widget-content label" >
                            本月
                        </td>
                        <td class="f-widget-content label" >
                           项目累计
                        </td>
                        <td class="f-widget-content label" >
                           本月
                        </td>
                        <td class="f-widget-content label" >
                           项目累计
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            1
                        </td>
                        <td class="f-widget-content" >
                            基础管理
                        </td>
                        <td class="f-widget-content" >
                            <f:NumberBox ID="nbMainCost1" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbMainProjectCost1" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubCost1" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubProjectCost1" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                         <td class="f-widget-content" rowspan="8">
                           <f:NumberBox ID="nbJianAnCost" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" rowspan="8">
                           <f:NumberBox ID="nbJianAnProjectCost" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            2
                        </td>
                        <td class="f-widget-content" >
                            安全技术
                        </td>
                        <td class="f-widget-content" >
                            <f:NumberBox ID="nbMainCost2" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbMainProjectCost2" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubCost2" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubProjectCost2" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            3
                        </td>
                        <td class="f-widget-content" >
                            职业健康
                        </td>
                        <td class="f-widget-content" >
                            <f:NumberBox ID="nbMainCost3" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbMainProjectCost3" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubCost3" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubProjectCost3" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            4
                        </td>
                        <td class="f-widget-content" >
                            安全防护
                        </td>
                        <td class="f-widget-content" >
                            <f:NumberBox ID="nbMainCost4" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbMainProjectCost4" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubCost4" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubProjectCost4" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            5
                        </td>
                        <td class="f-widget-content" >
                            化工试车
                        </td>
                        <td class="f-widget-content" >
                            <f:NumberBox ID="nbMainCost5" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbMainProjectCost5" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubCost5" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubProjectCost5" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            6
                        </td>
                        <td class="f-widget-content" >
                            教育培训
                        </td>
                        <td class="f-widget-content" >
                            <f:NumberBox ID="nbMainCost6" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbMainProjectCost6" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubCost6" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubProjectCost6" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            7
                        </td>
                        <td class="f-widget-content" >
                            文明施工环境保护
                        </td>
                        <td class="f-widget-content" >
                            <f:NumberBox ID="nbMainCost7" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbMainProjectCost7" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubCost7" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubProjectCost7" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label" colspan="2">
                            合计
                        </td>
                        <td class="f-widget-content" >
                            <f:NumberBox ID="nbMainCost" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbMainProjectCost" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubCost" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" >
                                                    </f:NumberBox>
                        </td>
                        <td class="f-widget-content" >
                           <f:NumberBox ID="nbSubProjectCost" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                        </td>
                    </tr>
                </table>
            </f:ContentPanel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
