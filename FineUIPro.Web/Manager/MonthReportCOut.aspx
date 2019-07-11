<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCOut.aspx.cs"
    Inherits="FineUIPro.Web.Manager.MonthReportCOut" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导出管理月报</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
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
                                    <td align="center" colspan="10">
                                        <asp:Label ID="Label3" runat="server" Text="项目HSE月报告" Font-Bold="True" Font-Size="20px"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="10">
                                        <asp:Label ID="lbReportDate" runat="server" Font-Size="15px"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="10">
                                        <asp:Label ID="lbReportCode" runat="server" Font-Size="15px"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="right">
                                        HSE经理&nbsp;
                                    </td>
                                    <td align="left" colspan="2">
                                        &nbsp;<asp:Label ID="lblReportMan" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        日期&nbsp;
                                    </td>
                                    <td align="left" colspan="4">
                                        &nbsp;<asp:Label ID="lblMonthReportDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label4" runat="server" Text="1" Font-Bold="true"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label1" runat="server" Text="项目概况" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-top: none; border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label2" runat="server" Text="项目名称:"></asp:Label>
                                        <asp:Label ID="lbProjectName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-top: none; border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label5" runat="server" Text="用户名称:"></asp:Label>
                                        <asp:Label ID="lblMainUnitName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-top: none; border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label6" runat="server" Text="项目地址:"></asp:Label>
                                        <asp:Label ID="lblProjectAddress" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-top: none; border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label8" runat="server" Text="项目号:"></asp:Label>
                                        <asp:Label ID="lblProjectCode" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-top: none; border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label7" runat="server" Text="合同号:"></asp:Label>
                                        <asp:Label ID="lblContractNo" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-top: none; border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label9" runat="server" Text="项目类型:"></asp:Label>
                                        <asp:Label ID="lblProjectType" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-top: none; border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label10" runat="server" Text="工作范围:"></asp:Label>
                                        <asp:Label ID="lblWorkRange" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-top: none; border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label11" runat="server" Text="项目建设工期(月):"></asp:Label>
                                        <asp:Label ID="lblDuration" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-top: none; border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label12" runat="server" Text="项目施工计划开工时间:"></asp:Label>
                                        <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-top: none; border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label13" runat="server" Text="工程中间交接时间:"></asp:Label>
                                        <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label14" runat="server" Text="2" Font-Bold="true"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label15" runat="server" Text="本月项目现场HSE人力投入情况" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr1" runat="server" style="height: 32px;">
                                    <td id="Td1" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvPersonSort" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="单位">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# ConvertUnitName(Eval("UnitId")) %>'
                                                            ToolTip='<%# ConvertUnitName(Eval("UnitId")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="20%" Height="32px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SumPersonNum" HeaderText="总人数" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="5%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HSEPersonNum" HeaderText="专职HSE管理人员数量" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="8%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ContractRange" HeaderText="承包范围" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="57%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Remark" HeaderText="备注" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label16" runat="server" Text="3" Font-Bold="true"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label17" runat="server" Text="本月项目现场HSE人工日统计" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="2">
                                        连续安全工作天数
                                    </td>
                                    <td align="center" colspan="3">
                                        HSE人工日
                                    </td>
                                    <td align="center" colspan="3">
                                        安全人工时
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" rowspan="2">
                                        本月连续安全工作天数
                                    </td>
                                    <td align="center" rowspan="2">
                                        累计连续安全工作天数
                                    </td>
                                    <td align="center" rowspan="2">
                                        本月HSE人工日
                                    </td>
                                    <td align="center" rowspan="2">
                                        年度累计HSE人工日
                                    </td>
                                    <td align="center" rowspan="2">
                                        总累计HSE人工日
                                    </td>
                                    <td align="center" colspan="2">
                                        本月安全人工时
                                    </td>
                                    <td align="center" rowspan="2">
                                        累计安全人工时
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center">
                                        五环
                                    </td>
                                    <td align="center">
                                        分包商
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center">
                                        <asp:Label ID="lbMonthHSEDay" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbSumHSEDay" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbMonthHSEWorkDay" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbYearHSEWorkDay" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbSumHSEWorkDay" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbHseManhours" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbSubcontractManHours" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lbTotalHseManhours" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label21" runat="server" Text="备注：一旦项目发生HSE事故，连续安全工作天数将清零重新统计。"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label18" runat="server" Text="4" Font-Bold="true"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label19" runat="server" Text="本月项目HSE现场管理" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label20" runat="server" Text="4.1"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label22" runat="server" Text="本月项目现场主要活动描述（对工程承包范围内的项目现场各装置施工里程碑、试运行活动进行概括性描述）"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="txtMainActivitiesDef" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label23" runat="server" Text="4.2"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label24" runat="server" Text="危险源动态识别及控制（对危险源辨识活动情况进行书面说明）"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr2" runat="server" style="height: 32px;">
                                    <td id="Td2" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvHazardSort" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Font-Bold="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# this.gvHazardSort.Rows.Count + 1%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="HazardName" HeaderText="危险源" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UnitAndArea" HeaderText="存在单位及区域" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="25%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="StationDef" HeaderText="现场情况描述" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="25%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HandleWay" HeaderText="控制措施" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="25%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label25" runat="server" Text="4.2"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label26" runat="server" Text="HSE培训"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label27" runat="server" Text="4.2.1"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label28" runat="server" Text="管理绩效数据统计"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr3" runat="server" style="height: 32px;">
                                    <td id="Td3" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvTrainSort" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Font-Bold="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# this.gvTrainSort.Rows.Count + 1%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="TrainContent" HeaderText="培训内容" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TeachHour" HeaderText="学时数" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="15%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TeachMan" HeaderText="讲师/教材" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="UnitName" HeaderText="受训单位" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PersonNum" HeaderText="受训人数" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label29" runat="server" Text="4.2.2"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label30" runat="server" Text="培训活动情况说明"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr4" runat="server" style="height: 32px;">
                                    <td id="Td4" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvTrainActivitySort" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Font-Bold="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# this.gvTrainActivitySort.Rows.Count + 1%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ActivityName" HeaderText="主要培训活动名称" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="35%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TrainDate" HeaderText="培训时间" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="15%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TrainEffect" HeaderText="培训效果（参加人数，所达到的效果）" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="45%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label31" runat="server" Text="4.3"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label32" runat="server" Text="HSE检查"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label33" runat="server" Text="4.3.1"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label34" runat="server" Text="管理绩效数据统计"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr5" runat="server" style="height: 32px;">
                                    <td id="Td5" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvCheckSort" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:BoundField DataField="CheckType" HeaderText="检查类型" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="15%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CheckNumber" HeaderText="本月开展次数" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="17%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="YearCheckNum" HeaderText="年度累计次数" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="17%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TotalCheckNum" HeaderText="项目总累计次数" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="17%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ViolationNumber" HeaderText="发现及整改隐患数量" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="17%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="YearViolationNum" HeaderText="年度隐患整改累计数量" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="17%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label35" runat="server" Text="备注：专项检查应注明检查类型（如临电检查等）。"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label36" runat="server" Text="4.3.2"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label37" runat="server" Text="检查活动情况说明（对检查开展的情况进行文字说明，包括检查效果等）"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr6" runat="server" style="height: 32px;">
                                    <td id="Td6" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvCheckDetailSort" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Font-Bold="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# this.gvCheckDetailSort.Rows.Count + 1%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CheckType" HeaderText="检查类型" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="15%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CheckTime" HeaderText="检查时间" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="15%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="JoinUnit" HeaderText="参加单位" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="25%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="StateDef" HeaderText="检查情况描述" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="40%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label38" runat="server" Text="4.4"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label39" runat="server" Text="HSE会议"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label40" runat="server" Text="4.4.1"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label41" runat="server" Text="管理绩效数据统计"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="3">
                                        本月组织/参加会议次数
                                    </td>
                                    <td align="center" colspan="5">
                                        本年度累计组织/参加会议次数
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="3">
                                       <asp:Label ID="txtMeetingNum" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" colspan="5">
                                        <asp:Label ID="txtYearMeetingNum" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr7" runat="server" style="height: 32px;">
                                    <td id="Td7" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvMeetingSort" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Font-Bold="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# this.gvMeetingSort.Rows.Count + 1%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="MeetingType" HeaderText="会议主题" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="15%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MeetingHours" HeaderText="时数" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MeetingHostMan" HeaderText="主持人" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MeetingDate" HeaderText="召开时间" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AttentPerson" HeaderText="参会人员" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MainContent" HeaderText="会议主要内容" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="30%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label42" runat="server" Text="4.5"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label43" runat="server" Text="应急管理"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label44" runat="server" Text="4.5.1"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label45" runat="server" Text="管理绩效数据统计"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="2">
                                        统计类型
                                    </td>
                                    <td align="center" colspan="2">
                                        本月
                                    </td>
                                    <td align="center" colspan="2">
                                        年度累计
                                    </td>
                                    <td align="center" colspan="2">
                                        项目累计
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="2">
                                        项目综合应急预案修编（发布）次数
                                    </td>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="txtComplexEmergencyNum" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" colspan="2">
                                       <asp:Label ID="txtYearComplexEmergencyNum" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" colspan="2">
                                       <asp:Label ID="txtTotalComplexEmergencyNum" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="2">
                                        项目专项应急预案修编（发布）
                                    </td>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="txtSpecialEmergencyNum" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" colspan="2">
                                       <asp:Label ID="txtYearSpecialEmergencyNum" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" colspan="2">
                                       <asp:Label ID="txtTotalSpecialEmergencyNum" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="2">
                                        应急演练活动次数
                                    </td>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="txtDrillRecordNum" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" colspan="2">
                                       <asp:Label ID="txtYearDrillRecordNum" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" colspan="2">
                                       <asp:Label ID="txtTotalDrillRecordNum" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label46" runat="server" Text="4.5.2"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label47" runat="server" Text="应急管理工作描述（如本月有）"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="txtEmergencyManagementWorkDef" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label48" runat="server" Text="4.6"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label49" runat="server" Text="HSE许可管理（许可管理情况说明）"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="3">
                                        本月主要作业票类型及办理情况说明
                                    </td>
                                    <td align="center" colspan="5">
                                        本月机具、安全设施检查验收情况说明
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="3">
                                        <asp:Label ID="txtLicenseRemark" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" colspan="5">
                                        <asp:Label ID="txtEquipmentRemark" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label50" runat="server" Text="4.7"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label51" runat="server" Text="HSE奖励与处罚"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label52" runat="server" Text="4.7.1"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label53" runat="server" Text="HSE奖励情况统计一览表"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr8" runat="server" style="height: 32px;">
                                    <td id="Td8" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvRewardSort" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Font-Bold="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# this.gvRewardSort.Rows.Count + 1%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="IncentiveUnit" HeaderText="被奖励单位或被奖励人/所属单位" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="35%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IncentiveType" HeaderText="奖励类型（现金或实物）" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IncentiveDate" HeaderText="奖励时间" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IncentiveMoney" HeaderText="折算金额合计（元）" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="2">
                                        本月累计奖励次数
                                    </td>
                                    <td align="center" colspan="2">
                                        年度累计奖励次数
                                    </td>
                                    <td align="center" colspan="2">
                                        本月累计奖励金额（元）
                                    </td>
                                    <td align="center" colspan="2">
                                        年度累计奖励金额（元）
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="2">
                                        <asp:Label ID="txtRewardNum" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="txtYearRewardNum" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" colspan="2">
                                       <asp:Label ID="txtRewardMoney" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" colspan="2">
                                       <asp:Label ID="txtYearRewardMoney" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label54" runat="server" Text="4.7.2"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label55" runat="server" Text="HSE处罚情况统计一览表"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr9" runat="server" style="height: 32px;">
                                    <td id="Td9" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvPunishSort" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Font-Bold="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# this.gvPunishSort.Rows.Count + 1%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="IncentiveUnit" HeaderText="被处罚单位或被处罚人/所属单位" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="35%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IncentiveDate" HeaderText="处罚时间" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="15%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
<%--                                                <asp:BoundField DataField="IncentiveType" HeaderText="处罚类型（现金或实物）" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="15%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="IncentiveMoney" HeaderText="折算金额合计（元）" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="15%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IncentiveReason" HeaderText="处罚原因" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="15%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="IncentiveBasis" HeaderText="处罚依据" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="15%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="2">
                                        本月累计处罚次数
                                    </td>
                                    <td align="center" colspan="2">
                                        年度累计处罚次数
                                    </td>
                                    <td align="center" colspan="2">
                                        本月累计处罚金额（元）
                                    </td>
                                    <td align="center" colspan="2">
                                        年度累计处罚金额（元）
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="2">
                                        <asp:Label ID="txtPunishNum" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="txtYearPunishNum" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" colspan="2">
                                       <asp:Label ID="txtPunishMoney" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" colspan="2">
                                       <asp:Label ID="txtYearPunishMoney" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label56" runat="server" Text="4.8"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label57" runat="server" Text="HSE现场其他管理情况"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr10" runat="server" style="height: 32px;">
                                    <td id="Td10" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvOtherManagement" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Font-Bold="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# this.gvOtherManagement.Rows.Count + 1%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ManagementDes" HeaderText="管理内容描述" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="95%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label58" runat="server" Text="5" Font-Bold="true"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label59" runat="server" Text="本月项目HSE内业管理" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label60" runat="server" Text="5.1"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label61" runat="server" Text="本月文件、方案修编情况说明（修编文件、方案名称，修编基本情况等）"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr11" runat="server" style="height: 32px;">
                                    <td id="Td11" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvMonthPlan" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Font-Bold="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# this.gvMonthPlan.Rows.Count + 1%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PlanName" HeaderText="本月修编的方案/文件名称" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="55%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CompileMan" HeaderText="修编人" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CompileDate" HeaderText="发布时间" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label62" runat="server" Text="5.2"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label63" runat="server" Text="HSE资质、方案审查记录"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr12" runat="server" style="height: 32px;">
                                    <td id="Td12" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvReviewRecord" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Font-Bold="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# this.gvReviewRecord.Rows.Count + 1%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ReviewRecordName" HeaderText="本月审查方案、资质文件名称" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="55%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ReviewMan" HeaderText="审查人" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ReviewDate" HeaderText="审查时间" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label64" runat="server" Text="6" Font-Bold="true"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label65" runat="server" Text="本月项目HSE费用管理（单位：万元）" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" rowspan="2">
                                        序号
                                    </td>
                                    <td align="center" rowspan="2">
                                        投入项目
                                    </td>
                                    <td align="center" colspan="2">
                                        五环工程
                                    </td>
                                    <td align="center" colspan="2">
                                        施工分包商
                                    </td>
                                    <td align="center" colspan="2">
                                        建安产值
                                    </td>
                                </tr>
                                 <tr style="height: 32px;">
                                    <td align="center" >
                                        本月
                                    </td>
                                    <td align="center" >
                                        项目累计
                                    </td>
                                    <td align="center" >
                                        本月
                                    </td>
                                    <td align="center" >
                                        项目累计
                                    </td>
                                    <td align="center" >
                                        本月
                                    </td>
                                    <td align="center" >
                                        项目累计
                                    </td>
                                </tr>
                                 <tr style="height: 32px;">
                                    <td align="center" >
                                        1
                                    </td>
                                    <td align="center" >
                                        基础管理
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainCost1" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainProjectCost1" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubCost1" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubProjectCost1" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" rowspan="8">
                                        <asp:Label ID="nbJianAnCost" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" rowspan="8">
                                        <asp:Label ID="nbJianAnProjectCost" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" >
                                        2
                                    </td>
                                    <td align="center" >
                                        安全技术
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainCost2" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainProjectCost2" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubCost2" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubProjectCost2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" >
                                         3 
                                    </td>
                                    <td align="center" >
                                        职业健康
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainCost3" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainProjectCost3" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubCost3" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubProjectCost3" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" >
                                         4 
                                    </td>
                                    <td align="center" >
                                        安全防护
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainCost4" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainProjectCost4" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubCost4" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubProjectCost4" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" >
                                         5 
                                    </td>
                                    <td align="center" >
                                        化工试车
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainCost5" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainProjectCost5" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubCost5" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubProjectCost5" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" >
                                         6
                                    </td>
                                    <td align="center" >
                                        教育培训
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainCost6" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainProjectCost6" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubCost6" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubProjectCost6" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" >
                                         7
                                    </td>
                                    <td align="center" >
                                        文明施工环境保护
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainCost7" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainProjectCost7" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubCost7" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubProjectCost7" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="center" colspan="2">
                                        合计
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainCost" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbMainProjectCost" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubCost" runat="server"></asp:Label>
                                    </td>
                                    <td align="center" >
                                        <asp:Label ID="nbSubProjectCost" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label66" runat="server" Text="7" Font-Bold="true"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label67" runat="server" Text="HSE事故/事件描述" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label68" runat="server" Text="7.1"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label69" runat="server" Text="管理绩效数据统计（表一）"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr13" runat="server" style="height: 32px;">
                                    <td id="Td13" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvAccidentDesciption" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:BoundField DataField="Matter" HeaderText="事项" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="30%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MonthDataNum" HeaderText="本月数据" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="30%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="YearDataNum" HeaderText="年度累计数据" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="40%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label70" runat="server" Text="7.2"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label71" runat="server" Text="管理绩效数据统计（表二，如项目发生事故，请继续填写下表，否则填无）"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr14" runat="server" style="height: 32px;">
                                    <td id="Td14" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvAccidentDesciptionItem" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:BoundField DataField="Matter" HeaderText="事项" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="50%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Datas" HeaderText="数据" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="50%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label72" runat="server" Text="7.3"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label73" runat="server" Text="事故/事件描述（文字描述）"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="txtAccidentDes" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label74" runat="server" Text="8" Font-Bold="true"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label75" runat="server" Text="下月工作计划" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label76" runat="server" Text="8.1"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label77" runat="server" Text="危险源动态识别及控制"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr15" runat="server" style="height: 32px;">
                                    <td id="Td15" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvHazard" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Font-Bold="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# this.gvHazard.Rows.Count + 1%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="WorkArea" HeaderText="下月计划施工的区域" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Subcontractor" HeaderText="所属分包商" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="25%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DangerousSource" HeaderText="可能存在的危险源" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="30%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ControlMeasures" HeaderText="计划采取的控制措施" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label78" runat="server" Text="8.2"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label79" runat="server" Text="HSE检查"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr16" runat="server" style="height: 32px;">
                                    <td id="Td16" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvCheck" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Font-Bold="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# this.gvCheck.Rows.Count + 1%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CheckType" HeaderText="下月计划开展的检查类型" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="30%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Inspectors" HeaderText="参检人员" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="25%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CheckDate" HeaderText="计划开展时间" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Remark" HeaderText="备注" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="20%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label80" runat="server" Text="8.3"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label81" runat="server" Text="应急管理（对下月应急管理工作进行描述）"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="txtNextEmergencyResponse" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label82" runat="server" Text="8.4"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label83" runat="server" Text="HSE管理文件/方案修编计划"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr17" runat="server" style="height: 32px;">
                                    <td id="Td17" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvManageDocPlan" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Font-Bold="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# this.gvManageDocPlan.Rows.Count + 1%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ManageDocPlanName" HeaderText="下月计划修编的方案/文件名称" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="45%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CompileMan" HeaderText="修编人" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="25%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CompileDate" HeaderText="计划完成时间" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="25%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label84" runat="server" Text="8.5"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label85" runat="server" Text="其他HSE工作计划"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Tr18" runat="server" style="height: 32px;">
                                    <td id="Td18" runat="server" align="left" colspan="10">
                                        <asp:GridView ID="gvOtherWorkPlan" runat="server" AllowSorting="True" PageSize="100"
                                             AutoGenerateColumns="False" HorizontalAlign="Left" Width="100%">
                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号" InsertVisible="False" HeaderStyle-Font-Bold="false">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# this.gvOtherWorkPlan.Rows.Count + 1%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="WorkContent" HeaderText="计划工作内容描述" HeaderStyle-Font-Bold="false">
                                                    <HeaderStyle HorizontalAlign="Center" Height="32px" />
                                                    <ItemStyle Width="95%" HorizontalAlign="Center" Height="32px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label86" runat="server" Text="9" Font-Bold="true"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label87" runat="server" Text="存在的主要问题及改进措施、 需要项目经理、项目主管、公司相关部门、业主协调解决事宜。（简要说明项目HSE管理存在的主要问题和需要项目经理、项目主管、公司相关部门、业主协调解决事宜，并提出具体改进措施和建议。）" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="txtQuestion" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <%--<tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label88" runat="server" Text="10" Font-Bold="true"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label89" runat="server" Text="项目现场影像照片" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 32px;">
                                    <td align="left" colspan="10" style="border-bottom: none;">
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="txtPhotoContents" runat="server"></asp:Label>
                                    </td>
                                </tr>--%>
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
