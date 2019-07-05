<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonPerfomanceView.aspx.cs"
    Inherits="FineUIPro.Web.Perfomance.PersonPerfomanceView" %>

<%@ Register Src="~/Controls/FlowOperateControl.ascx" TagName="FlowOperateControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看个人绩效评价</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="个人绩效评价" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtPersonPerfomanceCode" runat="server" Label="评价编号" LabelAlign="Right"
                        Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtUnitName" runat="server" Label="分包单位" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTeamGroupName" runat="server" Label="班组" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtPersonName" runat="server" Label="人员姓名" LabelAlign="Right" Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtSubContractNum" runat="server" Label="分包合同号" LabelAlign="Right"
                        Readonly="true">
                    </f:TextBox>
                    <f:TextBox ID="txtEvaluationDate" runat="server" Label="评价时间" LabelAlign="Right"
                        Readonly="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TabStrip ID="TabStrip1" Width="850px" Height="350px" ShowBorder="true" TabPosition="Top"
                        EnableTabCloseMenu="false" runat="server">
                        <Tabs>
                            <f:Tab ID="Tab1" Title="模式一" BodyPadding="5px" Layout="Fit" runat="server">
                                <Items>
                                    <f:Form ID="Form2" runat="server" ShowHeader="false" BodyPadding="5px">
                                        <Items>
                                            <f:FormRow>
                                                <Items>
                                                    <f:TextArea ID="txtEvaluationDef" runat="server" Label="评价描述" Readonly="true">
                                                    </f:TextArea>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:TextBox ID="txtRewardOrPunish" runat="server" Label="奖/罚" Readonly="true">
                                                    </f:TextBox>
                                                    <f:NumberBox ID="txtRPMoney" runat="server" Label="金额" Readonly="true">
                                                    </f:NumberBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:TextBox ID="txtAssessmentGroup" runat="server" Label="考评组" Readonly="true">
                                                    </f:TextBox>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:ContentPanel ID="ContentPanel2" runat="server" ShowHeader="false" ShowBorder="false"
                                                        Title="附件">
                                                        <table>
                                                            <tr style="height: 28px">
                                                                <td align="left">
                                                                    <div id="divFile1" runat="server">
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </f:ContentPanel>
                                                </Items>
                                            </f:FormRow>
                                        </Items>
                                    </f:Form>
                                </Items>
                            </f:Tab>
                            <f:Tab ID="Tab2" Title="模式二" BodyPadding="5px" runat="server" AutoScroll="true">
                                <Items>
                                    <f:Form ID="Form3" runat="server" ShowHeader="false" BodyPadding="5px">
                                        <Items>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label1" runat="server" Text="序号">
                                                    </f:Label>
                                                    <f:Label ID="Label2" runat="server" Text="评价内容">
                                                    </f:Label>
                                                    <f:Label ID="Label3" runat="server" Text="表现情况">
                                                    </f:Label>
                                                    <f:Label ID="Label4" runat="server" Text="标准分">
                                                    </f:Label>
                                                    <f:Label ID="Label5" runat="server" Text="实得分">
                                                    </f:Label>
                                                    <f:Label ID="Label66" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="lblSort_1" runat="server" Text="1">
                                                    </f:Label>
                                                    <f:Label ID="Label6" runat="server" Text="具有注册安全工程师证或地方及行业安全员资质证书，持证上岗">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_1" runat="server" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label ID="Label7" runat="server" Text="10">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_1" runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label68" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label8" runat="server" Text="2">
                                                    </f:Label>
                                                    <f:Label ID="Label9" runat="server" Text="具有丰富项目HSE 内页管理经验，单独编制项目HSE 计划、应急预案等文件">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_2" runat="server" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label ID="Label10" runat="server" Text="10">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_2" runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label69" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label11" runat="server" Text="3">
                                                    </f:Label>
                                                    <f:Label ID="Label12" runat="server" Text="具有丰富项目HSE 现场管理经验，快速准确查出安全隐患，按隐患级别限期整改合格">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_3" runat="server" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label ID="Label13" runat="server" Text="10">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_3" runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label70" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label14" runat="server" Text="4">
                                                    </f:Label>
                                                    <f:Label ID="Label15" runat="server" Text="热爱HSE 工作，强烈的责任心和事业感，敢说、敢管、敢处理">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_4" runat="server" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label ID="Label16" runat="server" Text="10">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_4" runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label71" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label17" runat="server" Text="5">
                                                    </f:Label>
                                                    <f:Label ID="Label18" runat="server" Text="积极参加业主、监理公司和PEC 总承包方召开的有关HSE 会议，观点鲜明">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_5" runat="server" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label ID="Label19" runat="server" Text="10">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_5" runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label72" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label20" runat="server" Text="6">
                                                    </f:Label>
                                                    <f:Label ID="Label21" runat="server" Text="主动协调与业主、监理和总承包方的关系，征得其支持和帮助，促进本身工作开展">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_6" runat="server" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label ID="Label22" runat="server" Text="10">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_6" runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label73" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label23" runat="server" Text="7">
                                                    </f:Label>
                                                    <f:Label ID="Label24" runat="server" Text="按时保质保量编写HSE 周报月报，准时上报业主、监理公司和总承包方">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_7" runat="server" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label ID="Label25" runat="server" Text="10">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_7" runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label74" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label26" runat="server" Text="8">
                                                    </f:Label>
                                                    <f:Label ID="Label27" runat="server" Text="准时参加业主、监理及总承包方组织的专检、联检和大检查，发现隐患，提出建议">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_8" runat="server" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label ID="Label28" runat="server" Text="10">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_8" runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label75" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label29" runat="server" Text="9">
                                                    </f:Label>
                                                    <f:Label ID="Label30" runat="server" Text="前瞻性眼光，提前反映诸如脚手架材料、安全网、专职安全员不足等问题，以求主动">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_9" runat="server" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label ID="Label31" runat="server" Text="10">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_9" runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label76" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="5% 30% 35% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label32" runat="server" Text="10">
                                                    </f:Label>
                                                    <f:Label ID="Label33" runat="server" Text="高度重视项目安全文化建设，充分利用班前会、三级安全培训会、板报、安全警示牌等机会大力宣传项目安全文化">
                                                    </f:Label>
                                                    <f:TextBox ID="txtBehavior_10" runat="server" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label ID="Label34" runat="server" Text="10">
                                                    </f:Label>
                                                    <f:NumberBox ID="txtScore_10" runat="server" Readonly="true">
                                                    </f:NumberBox>
                                                    <f:Label ID="Label77" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow ColumnWidths="10% 60% 15% 10% 5%">
                                                <Items>
                                                    <f:Label ID="Label65" runat="server" Text="综合评语">
                                                    </f:Label>
                                                    <f:TextBox ID="txtTotalJudging" runat="server" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label ID="Label67" runat="server" Text="最终得分">
                                                    </f:Label>
                                                    <f:TextBox ID="txtTotalScore" runat="server" Readonly="true">
                                                    </f:TextBox>
                                                    <f:Label ID="Label88" runat="server">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                        </Items>
                                    </f:Form>
                                </Items>
                            </f:Tab>
                        </Tabs>
                    </f:TabStrip>
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
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
