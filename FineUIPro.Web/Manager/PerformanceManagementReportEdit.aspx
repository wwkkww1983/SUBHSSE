<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformanceManagementReportEdit.aspx.cs"
    Inherits="FineUIPro.Web.Manager.PerformanceManagementReportEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑项目HSE绩效管理报告</title>
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
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Panel ID="Panel1" runat="server" Width="1170px" ShowBorder="True" EnableCollapse="true"
        CssClass="mytable" Layout="Table" TableConfigColumns="3" ShowHeader="False" Title="项目HSE绩效管理报告">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <f:DatePicker ID="txtDate" runat="server" Label="报告日期" LabelAlign="Right" LabelWidth="80px">
                    </f:DatePicker>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:ContentPanel ID="ContentPanel1" IsFluid="true" CssClass="blockpanel" runat="server"
                ShowBorder="false" ShowHeader="false">
                <table class="tablehtml">
                    <tr>
                        <td class="f-widget-content label" colspan="6">
                            项目HSE绩效指标统计表
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label1" >
                            序号
                        </td>
                        <td class="f-widget-content label2">
                            指标类别
                        </td>
                        <td class="f-widget-content label" colspan="2">
                            指标
                        </td>
                        <td class="f-widget-content label">
                           当月绩效数据
                        </td>
                        <td class="f-widget-content label">
                           绩效指标
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            1
                        </td>
                        <td class="f-widget-content" rowspan="3">
                            先导性指标
                        </td>
                        <td class="f-widget-content" >
                            不安全行为指数
                        </td>
                        <td class="f-widget-content" >
                            不安全行为数
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance1" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtPerformanceIndex1" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            2
                        </td>
                        <td class="f-widget-content">
                            未遂事件报告数
                        </td>
                        <td class="f-widget-content">
                            件数
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance2" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtPerformanceIndex2" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            3
                        </td>
                        <td class="f-widget-content" >
                            急救包扎事故人数
                        </td>
                        <td class="f-widget-content">
                            人数
                        </td>
                        <td class="f-widget-content">
                            <f:TextBox ID="txtMonthPerformance3" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                        <td class="f-widget-content">
                            <f:TextBox ID="txtPerformanceIndex3" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            4
                        </td>
                        <td class="f-widget-content" rowspan="6">
                            过程性指标
                        </td>
                        <td class="f-widget-content" rowspan="2">
                            项目HSE策划符合性
                        </td>
                        <td class="f-widget-content" >
                            项目HSE策划应得分
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance4" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                        <td class="f-widget-content" rowspan="2">
                            <f:TextBox ID="txtPerformanceIndex4" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            5
                        </td>
                        <td class="f-widget-content" >
                            项目HSE策划实得分
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance5" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            6
                        </td>
                        <td class="f-widget-content" rowspan="2">
                            项目HSE检查计划完成率
                        </td>
                        <td class="f-widget-content" >
                            实际检查次数
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance6" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                        <td class="f-widget-content" rowspan="2">
                            <f:TextBox ID="txtPerformanceIndex5" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            7
                        </td>
                        <td class="f-widget-content" >
                            计划检查次数
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance7" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            8
                        </td>
                        <td class="f-widget-content" rowspan="2">
                            项目HSE隐患按时整改率
                        </td>
                        <td class="f-widget-content" >
                            实际按时整改隐患数
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance8" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                        <td class="f-widget-content" rowspan="2">
                            <f:TextBox ID="txtPerformanceIndex6" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            9
                        </td>
                        <td class="f-widget-content" >
                            应按时整改HSE隐患数
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance9" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            10
                        </td>
                        <td class="f-widget-content" rowspan="10">
                            结果性指标
                        </td>
                        <td class="f-widget-content" rowspan="4">
                            百万工时总可记录事件率
                        </td>
                        <td class="f-widget-content" >
                            伤害事故人数
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance10" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                        <td class="f-widget-content" rowspan="4">
                            <f:TextBox ID="txtPerformanceIndex7" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            11
                        </td>
                        <td class="f-widget-content" >
                            损失工作日事故人数
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance11" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            12
                        </td>
                        <td class="f-widget-content" >
                            工作受限人数
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance12" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            13
                        </td>
                        <td class="f-widget-content" >
                            医疗处置人数
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance13" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            14
                        </td>
                        <td class="f-widget-content" rowspan="2">
                            百万工时总可记录事件率
                        </td>
                        <td class="f-widget-content" >
                            伤害事故损失工时
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance14" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                        <td class="f-widget-content" rowspan="2">
                            <f:TextBox ID="txtPerformanceIndex8" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            15
                        </td>
                        <td class="f-widget-content" >
                            损失工作日事故损失工时
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance15" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            16
                        </td>
                        <td class="f-widget-content" rowspan="2">
                            百万工时损失工时伤害事故率
                        </td>
                        <td class="f-widget-content" >
                            伤害事故人数
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance16" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                        <td class="f-widget-content" rowspan="2">
                            <f:TextBox ID="txtPerformanceIndex9" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            17
                        </td>
                        <td class="f-widget-content" >
                            损失工作日事故人数
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance17" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            18
                        </td>
                        <td class="f-widget-content">
                            百万工时死亡事故频率
                        </td>
                        <td class="f-widget-content" >
                            死亡事故起数
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance18" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                        <td class="f-widget-content">
                            <f:TextBox ID="txtPerformanceIndex10" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            19
                        </td>
                        <td class="f-widget-content">
                            百万工时事故死亡率
                        </td>
                        <td class="f-widget-content" >
                            死亡事故人数
                        </td>
                        <td class="f-widget-content" >
                            <f:TextBox ID="txtMonthPerformance19" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                        <td class="f-widget-content">
                            <f:TextBox ID="txtPerformanceIndex11" runat="server" Label=""
                                >
                            </f:TextBox>
                        </td>
                    </tr>
                </table>
            </f:ContentPanel>
        </Items>
    </f:Panel>
    <toolbars>
            <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnAttachUrl" Text="附件" ToolTip="附件上传及查看" Icon="TableCell" runat="server"
                        OnClick="btnAttachUrl_Click" ValidateForms="SimpleForm1" MarginLeft="5px">
                    </f:Button>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server">
                    </f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
    </form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
</body>
</html>
