<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubUploadWeekEdit.aspx.cs"
    Inherits="FineUIPro.Web.Manager.SubUploadWeekEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑分包商上传周报</title>
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
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Width="1170px" ShowBorder="True" EnableCollapse="true"
        CssClass="mytable" Layout="Table" TableConfigColumns="3" ShowHeader="False" Title="分包商上传周报">
        <Items>
            <f:ContentPanel ID="ContentPanel1" IsFluid="true" CssClass="blockpanel" runat="server"
                ShowBorder="false" ShowHeader="false">
                <table class="tablehtml" style="width:1070px;">
                    <tr>
                        <td class="f-widget-content label" colspan="8">
                            项目HSE周报
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label1">
                            项目名称
                        </td>
                        <td class="f-widget-content label2" colspan="3">
                            <f:Label runat="server" ID="lbProjectName">
                            </f:Label>
                        </td>
                        <td class="f-widget-content label">
                            报告日期
                        </td>
                        <td class="f-widget-content label">
                            <f:DatePicker ID="txtStartDate" runat="server" LabelAlign="Right" Width="150px">
                            </f:DatePicker>
                        </td>
                        <td class="f-widget-content label">
                            至
                        </td>
                        <td class="f-widget-content label">
                            <f:DatePicker ID="txtEndDate" runat="server" LabelAlign="Right" Width="150px">
                            </f:DatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label" colspan="8">
                            安全人工时统计
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label1" colspan="2">
                            单位名称
                        </td>
                        <td class="f-widget-content label2">
                            本周人数
                        </td>
                        <td class="f-widget-content label">
                            人工时
                        </td>
                        <td class="f-widget-content label" >
                            累计人工时
                        </td>
                        <td class="f-widget-content label" >
                            开工日期
                        </td>
                        <td class="f-widget-content label" >
                            中交日期
                        </td>
                        <td class="f-widget-content label" >
                            备注
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label1" colspan="2">
                            <f:Label runat="server" ID="lbUnitName">
                            </f:Label>
                        </td>
                        <td class="f-widget-content label2">
                            <f:NumberBox ID="nbPersonWeekNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label">
                            <f:NumberBox ID="nbManHours" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label">
                            <f:NumberBox ID="nbTotalManHours" NoDecimal="false" NoNegative="true" MinValue="0"
                                runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label">
                            <f:DatePicker ID="txtStartWorkDate" runat="server" LabelAlign="Right">
                            </f:DatePicker>
                        </td>
                        <td class="f-widget-content label">
                            <f:DatePicker ID="txtEndWorkDate" runat="server" LabelAlign="Right">
                            </f:DatePicker>
                        </td>
                        <td class="f-widget-content label">
                            <f:TextBox runat="server" ID="txtRemark">
                            </f:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label" colspan="8">
                            HSE绩效统计
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label1">
                            统计类别
                        </td>
                        <td class="f-widget-content label1">
                            未遂事件
                        </td>
                        <td class="f-widget-content label2">
                            急救包扎事故
                        </td>
                        <td class="f-widget-content label">
                            医疗处置事故
                        </td>
                        <td class="f-widget-content label" >
                            工作受限事故
                        </td>
                        <td class="f-widget-content label" >
                            损失工作日事故
                        </td>
                        <td class="f-widget-content label" >
                            死亡事故
                        </td>
                        <td class="f-widget-content label" >
                            事故累计
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label1">
                            发生起数
                        </td>
                        <td class="f-widget-content label1">
                            <f:NumberBox ID="nbHappenNum1" NoDecimal="true" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label2">
                            <f:NumberBox ID="nbHappenNum2" NoDecimal="true" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label">
                            <f:NumberBox ID="nbHappenNum3" NoDecimal="true" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbHappenNum4" NoDecimal="true" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbHappenNum5" NoDecimal="true" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbHappenNum6" NoDecimal="true" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbHappenNumTotal" NoDecimal="true" NoNegative="true" MinValue="0"
                                Readonly="true" runat="server">
                            </f:NumberBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label1">
                            伤害人数
                        </td>
                        <td class="f-widget-content label1">
                            <f:NumberBox ID="nbHurtPersonNum1" NoDecimal="true" NoNegative="true" MinValue="0" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label2">
                            <f:NumberBox ID="nbHurtPersonNum2" NoDecimal="true" NoNegative="true" MinValue="0" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label">
                            <f:NumberBox ID="nbHurtPersonNum3" NoDecimal="true" NoNegative="true" MinValue="0" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbHurtPersonNum4" NoDecimal="true" NoNegative="true" MinValue="0" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbHurtPersonNum5" NoDecimal="true" NoNegative="true" MinValue="0" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbHurtPersonNum6" NoDecimal="true" NoNegative="true" MinValue="0" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbHurtPersonNumTotal" NoDecimal="true" NoNegative="true" MinValue="0"
                                Readonly="true" runat="server">
                            </f:NumberBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label1">
                            损失工时
                        </td>
                        <td class="f-widget-content label1">
                            <f:NumberBox ID="nbLossHours1" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label2">
                            <f:NumberBox ID="nbLossHours2" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label">
                            <f:NumberBox ID="nbLossHours3" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbLossHours4" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbLossHours5" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbLossHours6" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbLossHoursTotal" NoDecimal="false" NoNegative="true" MinValue="0"
                                Readonly="true" runat="server">
                            </f:NumberBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label1">
                            直接经济损失
                        </td>
                        <td class="f-widget-content label1">
                            <f:NumberBox ID="nbLossMoney1" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label2">
                            <f:NumberBox ID="nbLossMoney2" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label">
                            <f:NumberBox ID="nbLossMoney3" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbLossMoney4" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbLossMoney5" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbLossMoney6" NoDecimal="false" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbLossMoneyTotal" NoDecimal="false" NoNegative="true" MinValue="0"
                                Readonly="true" runat="server">
                            </f:NumberBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label" colspan="8">
                            HSE工作信息统计
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label1">
                            统计类别
                        </td>
                        <td class="f-widget-content label1">
                            专职HSE人员（名）
                        </td>
                        <td class="f-widget-content label2">
                            HSE检查（次）
                        </td>
                        <td class="f-widget-content label">
                            应急演练（次）
                        </td>
                        <td class="f-widget-content label" >
                            作业许可证（份）
                        </td>
                        <td class="f-widget-content label" >
                            HSE教育培训（人次）
                        </td>
                        <td class="f-widget-content label" >
                            危险源辨识（次）
                        </td>
                        <td class="f-widget-content label" >
                            HSE会议（次）
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label1">
                            数量
                        </td>
                        <td class="f-widget-content label1">
                            <f:NumberBox ID="nbHSEPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label2">
                            <f:NumberBox ID="nbCheckNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label">
                            <f:NumberBox ID="nbEmergencyDrillNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbLicenseNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbTrainNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbHazardNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbMeetingNum" NoDecimal="true" NoNegative="true" MinValue="0" 
                                runat="server">
                            </f:NumberBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label" colspan="8">
                            HSE隐患整改统计
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label" colspan="4">
                            隐患数
                        </td>
                        <td class="f-widget-content label" colspan="4">
                            整改数
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label1">
                            上级检查
                        </td>
                        <td class="f-widget-content label1">
                            总包商检查
                        </td>
                        <td class="f-widget-content label2">
                            分包商检查
                        </td>
                        <td class="f-widget-content label">
                            隐患累计
                        </td>
                        <td class="f-widget-content label" >
                            上级检查
                        </td>
                        <td class="f-widget-content label" >
                            总包商检查
                        </td>
                        <td class="f-widget-content label" >
                            分包商检查
                        </td>
                        <td class="f-widget-content label" >
                            整改累计
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label1">
                            <f:NumberBox ID="nbHiddenDanger1" NoDecimal="true" NoNegative="true" MinValue="0" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label1">
                            <f:NumberBox ID="nbHiddenDanger2" NoDecimal="true" NoNegative="true" MinValue="0" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label2">
                            <f:NumberBox ID="nbHiddenDanger3" NoDecimal="true" NoNegative="true" MinValue="0" AutoPostBack="true" OnTextChanged="Text_TextChanged"
                                runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label">
                            <f:NumberBox ID="nbHiddenDangerTotal" NoDecimal="true" NoNegative="true" MinValue="0"
                                Readonly="true" runat="server">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbRectifyNum1" NoDecimal="true" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbRectifyNum2" NoDecimal="true" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbRectifyNum3" NoDecimal="true" NoNegative="true" MinValue="0" runat="server" AutoPostBack="true" OnTextChanged="Text_TextChanged">
                            </f:NumberBox>
                        </td>
                        <td class="f-widget-content label" >
                            <f:NumberBox ID="nbRectifyNumTotal" NoDecimal="true" NoNegative="true" MinValue="0"
                                Readonly="true" runat="server">
                            </f:NumberBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label" colspan="8">
                            工作总结与计划
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            本周工作总结
                        </td>
                        <td class="f-widget-content label" colspan="7">
                            <f:TextArea ID="txtThisWorkSummary" runat="server" MaxLength="1000">
                            </f:TextArea>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            下周工作计划
                        </td>
                        <td class="f-widget-content label" colspan="7">
                            <f:TextArea ID="txtNextWorkPlan" runat="server" MaxLength="1000">
                            </f:TextArea>
                        </td>
                    </tr>
                    <tr>
                        <td class="f-widget-content label">
                            其他存在的问题和应报告事项
                        </td>
                        <td class="f-widget-content label" colspan="7">
                            <f:TextArea ID="txtOtherProblems" runat="server" MaxLength="1000">
                            </f:TextArea>
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
