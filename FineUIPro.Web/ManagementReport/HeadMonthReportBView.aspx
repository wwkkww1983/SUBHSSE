﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeadMonthReportBView.aspx.cs"
    Inherits="FineUIPro.Web.ManagementReport.HeadMonthReportBView" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看管理月报</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" AjaxAspnetControls="divProject"/>
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                <Items>
                    <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                        EnableAjax="false" DisableControlBeforePostBack="false">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:ContentPanel runat="server" AutoScroll="true" Height="500px" ShowHeader="false"
                ID="ContentPanel1">
                <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0" align="center">
                    <tr>
                        <td>
                            <table id="Table2" runat="server" align="center" valign="top" width="100%" cellpadding="0"
                                cellspacing="0" class="table" style="border-collapse: collapse;" border="1">
                                <tr style="height: 32px;">
                                    <td align="right">
                                        编号：
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:Label ID="txtMonthReportCode" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        月报月份：
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="txtMonths" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        填报日期：
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="txtReportDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="right">
                                        填报单位：
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:Label ID="txtReportUnitName" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        填报人：
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="txtReportMan" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        审核人：
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="txtCheckMan" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <asp:RadioButtonList ID="rblState" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rblState_SelectedIndexChanged">
                                            <asp:ListItem Value="0" >全部</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">施工</asp:ListItem>
                                            <asp:ListItem Value="2">暂停</asp:ListItem>
                                            <asp:ListItem Value="3">完工</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" align="right" colspan="10">
                                      <div runat="server" id="divProject">
                                        <asp:GridView ID="gvProjectStation" runat="server" AutoGenerateColumns="False" onrowcreated="gvProjectStation_RowCreated"
                                            GridLines="Vertical" Width="100%" BorderWidth="1px" AllowPaging="false" AllowSorting="True"
                                            BorderColor="Black" BorderStyle="Solid">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Height="25px">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProjectCode" />
                                                <asp:BoundField DataField="ProjectName"  />
                                                <asp:BoundField DataField="ProjectManager" />
                                                <asp:BoundField DataField="ProjectType" />
                                                <asp:BoundField DataField="StartDate"  DataFormatString="{0:d}" />
                                                <asp:BoundField DataField="EndDate" DataFormatString="{0:d}" />
                                                <%--完成工时--%>
                                                <asp:BoundField DataField="Manhours"  />
                                                <asp:TemplateField HeaderText="岗位">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotalManhours" runat="server" Text='<%# Bind("TotalManhours") %>'
                                                                         Width="98%" ></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20%" />
                                                            </asp:TemplateField>
                                                <%--项目安全工时--%>
                                                <asp:BoundField DataField="HseManhours" />
                                                <asp:BoundField DataField="TotalHseManhours"  />
                                                <asp:BoundField DataField="TotalManNum"  />
                                                <%--事故起数--%>
                                                <asp:BoundField DataField="AccidentNum"  />
                                                <asp:BoundField DataField="AccidentTotalNum"  />
                                                <%--损失工时--%>
                                                <asp:BoundField DataField="LoseHours" />
                                                <asp:BoundField DataField="TotalLoseHours"  />
                                                <asp:BoundField DataField="AvgA"  />
                                                <asp:BoundField DataField="AvgB" />
                                                <asp:BoundField DataField="AvgC" />
                                                <asp:BoundField DataField="HazardNum"  />
                                                <asp:BoundField DataField="IsArgumentHazardNum"  />
                                                <asp:BoundField DataField="PlanCostA"  />
                                                <asp:BoundField DataField="PlanCostB"  />
                                                <%--实际支出 --%>
                                                <%--A-安全生产合计 --%>
                                                <asp:BoundField DataField="RealCostA"  />
                                                <asp:BoundField DataField="RealCostYA"  />
                                                <asp:BoundField DataField="RealCostPA"  />
                                                <%--B-文明施工合计 --%>
                                                <asp:BoundField DataField="RealCostB"  />
                                                <asp:BoundField DataField="RealCostYB" />
                                                <asp:BoundField DataField="RealCostPB" />
                                                <%--A+B合计 --%>
                                                <asp:BoundField DataField="RealCostAB"  />
                                                <asp:BoundField DataField="RealCostYAB"  />
                                                <asp:BoundField DataField="RealCostPAB"  />
                                            </Columns>
                                            <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                            <RowStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                        </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10">
                                        <asp:Label ID="Label3" runat="server" Text="事故统计" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr1" runat="server" style="height: 32px;">
                                    <td colspan="2" align="center" style="width: 20%">
                                        事故类型
                                    </td>
                                    <td align="center">
                                        发生次数（当月）
                                    </td>
                                    <td align="center">
                                        发生次数（累计）
                                    </td>
                                    <td align="center">
                                        人数（当月）
                                    </td>
                                    <td align="center">
                                        人数（累计）
                                    </td>
                                    <td align="center">
                                        损失工时（当月）
                                    </td>
                                    <td align="center">
                                        损失工时（累计）
                                    </td>
                                    <td align="center">
                                        经济损失（当月）
                                    </td>
                                    <td align="center">
                                        经济损失（累计）
                                    </td>
                                </tr>
                                <tr id="Tr2" runat="server" style="height: 32px;">
                                    <td rowspan="6" align="center" style="width: 10%">
                                        人 身 伤 害 事 故
                                    </td>
                                    <td align="center" style="width: 10%">
                                        <asp:Label ID="lblAccidentType11" runat="server" Text="死 亡 事 故"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Label ID="txtNumber11" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Label ID="txtSumNumber11" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Label ID="txtPersonNum11" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Label ID="txtSumPersonNum11" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Label ID="txtLoseHours11" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Label ID="txtSumLoseHours11" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Label ID="txtLoseMoney11" runat="server"></asp:Label>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Label ID="txtSumLoseMoney11" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr3" runat="server" style="height: 32px;">
                                    <td align="center">
                                        <asp:Label ID="lblAccidentType12" runat="server" Text="重 伤 事 故"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNumber12" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumNumber12" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPersonNum12" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumPersonNum12" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseHours12" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseHours12" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseMoney12" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseMoney12" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr4" runat="server" style="height: 32px;">
                                    <td align="center">
                                        <asp:Label ID="lblAccidentType13" runat="server" Text="轻 伤 事 故"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNumber13" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumNumber13" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPersonNum13" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumPersonNum13" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseHours13" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseHours13" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseMoney13" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseMoney13" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr5" runat="server" style="height: 32px;">
                                    <td align="center">
                                        <asp:Label ID="lblAccidentType14" runat="server" Text="工 作 受 限 事 故"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNumber14" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumNumber14" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPersonNum14" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumPersonNum14" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseHours14" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseHours14" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseMoney14" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseMoney14" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr6" runat="server" style="height: 32px;">
                                    <td align="center">
                                        <asp:Label ID="lblAccidentType15" runat="server" Text="医 疗 处 理"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNumber15" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumNumber15" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPersonNum15" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumPersonNum15" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseHours15" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseHours15" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseMoney15" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseMoney15" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr7" runat="server" style="height: 32px;">
                                    <td align="center">
                                        <asp:Label ID="lblAccidentType16" runat="server" Text="现场处置（急救）"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNumber16" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumNumber16" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPersonNum16" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumPersonNum16" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseHours16" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseHours16" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseMoney16" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseMoney16" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr8" runat="server" style="height: 32px;">
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lblAccidentType21" runat="server" Text="未 遂 事 故"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNumber21" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumNumber21" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPersonNum21" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumPersonNum21" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseHours21" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseHours21" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseMoney21" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseMoney21" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr9" runat="server" style="height: 32px;">
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lblAccidentType22" runat="server" Text="火 灾 事 故"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNumber22" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumNumber22" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPersonNum22" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumPersonNum22" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseHours22" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseHours22" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseMoney22" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseMoney22" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr10" runat="server" style="height: 32px;">
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lblAccidentType23" runat="server" Text="爆 炸 事 故"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNumber23" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumNumber23" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPersonNum23" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumPersonNum23" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseHours23" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseHours23" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseMoney23" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseMoney23" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr11" runat="server" style="height: 32px;">
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lblAccidentType24" runat="server" Text="道 路 交 通 事 故"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNumber24" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumNumber24" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPersonNum24" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumPersonNum24" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseHours24" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseHours24" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseMoney24" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseMoney24" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lblAccidentType25" runat="server" Text="机 械 设 备 事 故"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNumber25" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumNumber25" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPersonNum25" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumPersonNum25" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseHours25" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseHours25" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseMoney25" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseMoney25" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lblAccidentType26" runat="server" Text="环 境 污 染 事 故"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNumber26" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumNumber26" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPersonNum26" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumPersonNum26" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseHours26" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseHours26" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseMoney26" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseMoney26" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lblAccidentType27" runat="server" Text="职业病"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNumber27" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumNumber27" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPersonNum27" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumPersonNum27" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseHours27" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseHours27" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseMoney27" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseMoney27" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lblAccidentType28" runat="server" Text="生产事故"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNumber28" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumNumber28" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPersonNum28" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumPersonNum28" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseHours28" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseHours28" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseMoney28" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseMoney28" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lblAccidentType29" runat="server" Text="其它事故"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtNumber29" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumNumber29" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtPersonNum29" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumPersonNum29" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseHours29" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseHours29" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtLoseMoney29" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSumLoseMoney29" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr12" runat="server" style="height: 32px;">
                                    <td id="Td1" runat="server" align="center" colspan="10">
                                        事故数据
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="3" align="right">
                                        可记录事故数
                                    </td>
                                    <td>
                                        <asp:Label ID="txtAccidentNum" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2" align="right">
                                        百万工时总可记录事故率
                                    </td>
                                    <td>
                                        <asp:Label ID="txtAccidentRateA" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2" align="right">
                                        百万工时损失工时率
                                    </td>
                                    <td>
                                        <asp:Label ID="txtAccidentRateB" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="3" align="right">
                                        百万工时损失工时伤害事故率
                                    </td>
                                    <td>
                                        <asp:Label ID="txtAccidentRateC" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2" align="right">
                                        百万工时死亡事故频率
                                    </td>
                                    <td>
                                        <asp:Label ID="txtAccidentRateD" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2" align="right">
                                        百万工时事故死亡率
                                    </td>
                                    <td>
                                        <asp:Label ID="txtAccidentRateE" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="10" align="center">
                                        事故台账
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <asp:GridView ID="GridAccidentDetailSort" runat="server" AllowSorting="True" PageSize="12"
                                            ShowFooter="true" AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="项目代码">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label14" runat="server" Text='<%# ConvertProjectCode(Eval("MonthReportId")) %>'
                                                            ToolTip='<%# ConvertProjectCode(Eval("MonthReportId")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="False" HorizontalAlign="Left" Height="32px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" Height="32px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="项目名称">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label15" runat="server" Text='<%# ConvertProjectName(Eval("MonthReportId")) %>'
                                                            ToolTip='<%# ConvertProjectName(Eval("MonthReportId")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="False" HorizontalAlign="Left" Height="32px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" Height="32px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="项目经理">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label16" runat="server" Text='<%# ConvertProjectManagerName(Eval("MonthReportId")) %>'
                                                            ToolTip='<%# ConvertProjectManagerName(Eval("MonthReportId")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="False" HorizontalAlign="Left" Height="32px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" Height="32px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Abstract" HeaderText="提要" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Left" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Left" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AccidentType" HeaderText="事故类型" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Left" Height="32px" />
                                                    <ItemStyle Width="10%" HorizontalAlign="Left" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PeopleNum" HeaderText="人数" DataFormatString="{0:N0}" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Left" Height="32px" />
                                                    <ItemStyle Width="10%" HorizontalAlign="Left" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WorkingHoursLoss" HeaderText="工时损失" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Left" Height="32px" />
                                                    <ItemStyle Width="10%" HorizontalAlign="Left" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EconomicLoss" HeaderText="经济损失" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Left" Height="32px" />
                                                    <ItemStyle Width="10%" HorizontalAlign="Left" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AccidentDate" HeaderText="发生时间" DataFormatString="{0:d}"
                                                    HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Left" Height="32" />
                                                    <ItemStyle Width="10%" HorizontalAlign="Left" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10">
                                        <asp:Label ID="Label8" runat="server" Text="项目施工现场安全培训情况统计" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <asp:GridView ID="gvTrainSort" runat="server" AllowSorting="True" PageSize="12" ShowFooter="true"
                                            AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:BoundField DataField="TrainType" HeaderText="培训课程类型" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Left" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Left" Height="32px" />
                                                    <FooterStyle Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TrainNumber" HeaderText="培训次数(当月)" DataFormatString="{0:N0}"
                                                    HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Left" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Left" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TotalTrainNum" HeaderText="培训次数(累计)" DataFormatString="{0:N0}"
                                                    HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Left" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Left" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TrainPersonNumber" HeaderText="培训人数(当月)" DataFormatString="{0:N0}"
                                                    HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Left" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Left" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TotalTrainPersonNum" HeaderText="培训人数(累计)" DataFormatString="{0:N0}"
                                                    HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Left" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Left" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="10">
                                        <asp:Label ID="Label1" runat="server" Text="项目施工现场安全会议情况统计" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2">
                                        安全会议类型
                                    </td>
                                    <td colspan="2">
                                        会议次数(当月)
                                    </td>
                                    <td colspan="2">
                                        会议次数(累计)
                                    </td>
                                    <td colspan="2">
                                        参会人数(当月)
                                    </td>
                                    <td colspan="2">
                                        参会人数(累计)
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2">
                                        <asp:Label ID="lblMeetingType1" runat="server" Text="安 全 周 会"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtMeetingNumber1" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumMeetingNumber1" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtMeetingPersonNumber1" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumMeetingPersonNumber1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2">
                                        <asp:Label ID="lblMeetingType2" runat="server" Text="安 全 月 会"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtMeetingNumber2" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumMeetingNumber2" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtMeetingPersonNumber2" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumMeetingPersonNumber2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2">
                                        <asp:Label ID="lblMeetingType3" runat="server" Text="专 项 安 全 会 议"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtMeetingNumber3" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumMeetingNumber3" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtMeetingPersonNumber3" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumMeetingPersonNumber3" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2">
                                        <asp:Label ID="Label10" runat="server" Text="合 计"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtAllMeetingNumber" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtAllSumMeetingNumber" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtAllMeetingPersonNumber" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtAllSumMeetingPersonNumber" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10">
                                        <asp:Label ID="Label11" runat="server" Text="项目施工现场安全检查情况统计" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2">
                                        安 全 检 查 类 型
                                    </td>
                                    <td colspan="2">
                                        检查次数(当月)
                                    </td>
                                    <td colspan="2">
                                        检查次数(累计)
                                    </td>
                                    <td colspan="2">
                                        违章数量(当月)
                                    </td>
                                    <td colspan="2">
                                        违章数量(累计)
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2">
                                        <asp:Label ID="lblCheckType1" runat="server" Text="安 全 日 常 巡 查"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtCheckNumber1" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumCheckNumber1" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtViolationNumber1" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumViolationNumber1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2">
                                        <asp:Label ID="lblCheckType2" runat="server" Text="每 周 安 全 检 查"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtCheckNumber2" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumCheckNumber2" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtViolationNumber2" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumViolationNumber2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2">
                                        <asp:Label ID="lblCheckType3" runat="server" Text="每 月 安 全 大 检 查"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtCheckNumber3" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumCheckNumber3" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtViolationNumber3" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumViolationNumber3" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2">
                                        <asp:Label ID="lblCheckType4" runat="server" Text="专 项 安 全 检 查"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtCheckNumber4" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumCheckNumber4" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtViolationNumber4" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumViolationNumber4" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="2">
                                        <asp:Label ID="Label12" runat="server" Text="合 计"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtAllCheckNumber" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtAllSumCheckNumber" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtAllViolationNumber" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtAllSumViolationNumber" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10">
                                        <asp:Label ID="Label13" runat="server" Text="项目施工现场安全奖惩情况统计" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="3">
                                        类型
                                    </td>
                                    <td align="center" colspan="3">
                                        内容
                                    </td>
                                    <td align="center" colspan="2">
                                        当月
                                    </td>
                                    <td align="center" colspan="2">
                                        累计
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td rowspan="4" align="center" colspan="3">
                                        奖励
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="lblIncentiveType11" runat="server" Text="安 全 明 星 奖 （金 额）"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtIncentiveMoney1" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumIncentiveMoney1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="3">
                                        <asp:Label ID="lblIncentiveType12" runat="server" Text="百 万 工 时 无 事 故 奖 （金 额）"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtIncentiveMoney2" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumIncentiveMoney2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="3">
                                        <asp:Label ID="lblIncentiveType13" runat="server" Text="安 全 目 标 兑 现 奖（金 额）"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtIncentiveMoney3" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumIncentiveMoney3" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="3">
                                        <asp:Label ID="lblIncentiveType14" runat="server" Text="其 它 奖 励"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtIncentiveMoney4" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumIncentiveMoney4" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td rowspan="3" align="center" colspan="3">
                                        处罚
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="lblIncentiveType21" runat="server" Text="通 报 批 评 （人/次）"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtIncentiveNumber1" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumIncentiveNumber1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="3">
                                        <asp:Label ID="lblIncentiveType22" runat="server" Text="开 除 （人/次）"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtIncentiveNumber2" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumIncentiveNumber2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td colspan="3">
                                        <asp:Label ID="lblIncentiveType15" runat="server" Text="罚 款 （金 额）"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtIncentiveMoney5" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txtSumIncentiveMoney5" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </f:ContentPanel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
