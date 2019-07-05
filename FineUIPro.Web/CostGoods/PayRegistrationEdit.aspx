<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayRegistrationEdit.aspx.cs"
    Inherits="FineUIPro.Web.CostGoods.PayRegistrationEdit" ValidateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑安全费用投入登记</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        .font_style
        {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Width="1170px" ShowBorder="True" EnableCollapse="true"
        CssClass="mytable" Layout="Table" TableConfigColumns="3" ShowHeader="False" Title="安全费用投入登记">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <f:DatePicker ID="txtDate" runat="server" Label="日期" LabelAlign="Right">
                    </f:DatePicker>
                    <f:Label ID="lblUnitName" runat="server" Label="单位" LabelAlign="Right">
                    </f:Label>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Panel ID="Panel2" Title="安全防护" Width="1170px" runat="server" BodyPadding="1px"
                ShowBorder="true" ShowHeader="true" EnableCollapse="false">
                <Items>
                    <f:Form runat="server" ID="Form2" Title="1.基础管理" ShowHeader="true" ShowBorder="true"
                        BodyPadding="1px" EnableCollapse="true">
                        <Items>
                            <f:Label ID="Lable2" runat="server" Text="内业管理" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label3" runat="server" Text="安全生产规章制度、安全手册、应急预案等的编制、印刷">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_1" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_1" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_1" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label1" runat="server" Text="施工现场和特殊界区管理出入证、通行证制证、制卡">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_2" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_2" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_2" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label2" runat="server" Text="安全、环保、应急管理文档汇集、编辑、分析">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_3" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_3" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_3" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label6" runat="server" Text="安全检测、监测、评定、评价">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_4" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_4" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_4" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label7" runat="server" Text="报刊、标语、参考书、宣传画、音像制品等宣传品和现场宣传栏">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_5" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_5" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_5" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label4" runat="server" Text="检测器材" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label5" runat="server" Text="员工进出场信息采集识别管理系统（含摄录存取及分析器材）购置、折旧或租赁费">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_6" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_6" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_6" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label8" runat="server" Text="射线、风速、噪声、温湿度、粉尘、空气质量检测仪器购置、折旧或租赁费用">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_7" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_7" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_7" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label9" runat="server" Text="气液成分、电气安全、力学特性、热工特性和几何量检测仪器购置、折旧或租赁费">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_8" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_8" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_8" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label10" runat="server" Text="监测、检测辅助器具">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_9" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_9" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_9" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label11" runat="server" Text="警戒警示通讯器材（对讲机、望远镜、测距仪）">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_10" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_10" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_10" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label12" runat="server" Text="监测检测计量器具执行检定和维修费用">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_11" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_11" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_11" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label13" runat="server" Text="警示警戒" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label14" runat="server" Text="风险突出处安全警示标志牌、警示灯、警戒线、提示牌等">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_12" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_12" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_12" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label15" runat="server" Text="各工种、各类施工机械的安全操作规程牌">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_13" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_13" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_13" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label16" runat="server" Text="特殊标识、标识设置">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_14" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_14" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_14" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label17" runat="server" Text="安全奖励" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label18" runat="server" Text="表彰安全先进集体、个人的奖励">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_15" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_15" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_15" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label19" runat="server" Text="其他" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label20" runat="server" Text="其它安全生产管理直接相关的支出">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1_16" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType1_16" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1_16" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 9% 11% 9% 11%">
                                <Items>
                                    <f:Label ID="Label21" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label22" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSYearType1" runat="server" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label23" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMainApproveType1" runat="server" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                    <f:Form runat="server" ID="Form3" Title="2.安全技术" ShowHeader="true" ShowBorder="true"
                        BodyPadding="1px" EnableCollapse="true">
                        <Items>
                            <f:Label ID="Label24" runat="server" Text="安全技术" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label25" runat="server" Text="专项方案中非常规安全措施费用">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType2_1" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType2_1" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType2_1" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label26" runat="server" Text="与安全相关的专项方案专家论证审查费用">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType2_2" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType2_2" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType2_2" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label27" runat="server" Text="各类安全技术方案的编制和咨询费用">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType2_3" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType2_3" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType2_3" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label28" runat="server" Text="安全技术进步专项费用">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType2_4" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType2_4" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType2_4" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 9% 11% 9% 11%">
                                <Items>
                                    <f:Label ID="Label29" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType2" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label30" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSYearType2" runat="server" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label31" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMainApproveType2" runat="server" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                    <f:Form runat="server" ID="Form4" Title="3.职业健康" ShowHeader="true" ShowBorder="true"
                        BodyPadding="1px" EnableCollapse="true">
                        <Items>
                            <f:Label ID="Label32" runat="server" Text="工业卫生" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label33" runat="server" Text="通风、降温、保暖、除尘、防眩光设施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType3_1" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType3_1" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType3_1" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label34" runat="server" Text="职业病预防措施和有害作业工种保健费">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType3_2" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType3_2" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType3_2" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label35" runat="server" Text="特殊环境作业和特殊要求行业人员体检费">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType3_3" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType3_3" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType3_3" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label36" runat="server" Text="女工休息室、特殊作业人员休息室">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType3_4" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType3_4" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType3_4" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label37" runat="server" Text="水泥等其他易飞扬颗粒建筑材料封闭放置和遮盖措施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType3_5" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType3_5" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType3_5" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label38" runat="server" Text="边角余料，废旧材料清理回收措施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType3_6" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType3_6" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType3_6" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 9% 11% 9% 11%">
                                <Items>
                                    <f:Label ID="Label39" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType3" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label40" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSYearType3" runat="server" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label41" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMainApproveType3" runat="server" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                    <f:Form runat="server" ID="Form5" Title="4.防护措施" ShowHeader="true" ShowBorder="true"
                        BodyPadding="1px" EnableCollapse="true">
                        <Items>
                            <f:Label ID="Label42" runat="server" Text="安全用电" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label43" runat="server" Text="漏电保护器、开关箱等">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_1" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_1" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_1" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label47" runat="server" Text="保护接地装置，大型机具设备的防雷接地">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_2" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_2" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_2" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label48" runat="server" Text="受限空间使用的低压照明设备（隔离变压器、低压照明灯、专用配电箱）">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_3" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_3" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_3" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label57" runat="server" Text="高处作业及基坑" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label49" runat="server" Text="基坑及安全措施费隐蔽工程动土安全措施费">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_4" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_4" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_4" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label50" runat="server" Text="孔、洞、井、临边防护用钢管、跳板、安全网、防护栏杆、踢脚板">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_5" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_5" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_5" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label51" runat="server" Text="有防坠物要求的棚房设施建筑物临边和施工通道的隔离防护棚">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_6" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_6" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_6" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label52" runat="server" Text="作业面操作平台、格栅板、护栏、爬梯、马道和通道及生命线搭设">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_7" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_7" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_7" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label53" runat="server" Text="管廊作业面通道、防护栏杆、踢脚板、生命线等措施，管道防止滚动措施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_8" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_8" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_8" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label54" runat="server" Text="钢结构安装时脚手架等安全措施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_9" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_9" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_9" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label55" runat="server" Text="高处作业下方区域警戒围护">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_10" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_10" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_10" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label56" runat="server" Text="其他高处作业安全措施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_11" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_11" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_11" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label58" runat="server" Text="临边洞口防护" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label59" runat="server" Text="为建构筑物、钢构、设备施工安全搭设的操作平台的防护栏杆和踢脚板；为安全操作加固脚手架；敷设平网、立网（密网）；附于脚手架上的操作平台及安全通道">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_12" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_12" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_12" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label60" runat="server" Text="受限空间内作业" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label61" runat="server" Text="通风、降温、防触电和消防设施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_13" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_13" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_13" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label62" runat="server" Text="安全电压照明系统">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_14" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_14" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_14" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label63" runat="server" Text="支护作业平台及防坠落、防滑设施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_15" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_15" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_15" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label64" runat="server" Text="动火作业" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label65" runat="server" Text="气瓶固定、防晒、防砸措施（气瓶笼或气瓶架）；气瓶检漏措施；防回火设施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_16" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_16" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_16" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label66" runat="server" Text="高处动火的接火措施、挡火措施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_17" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_17" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_17" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label67" runat="server" Text="火源及溅落区附件设备、电缆、管道、电气、仪表等覆盖保护措施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_18" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_18" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_18" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label68" runat="server" Text="机械装备防护" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label69" runat="server" Text="中小型机具安全附件维护，使用保护（安全锁钩、护套）">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_19" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_19" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_19" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label70" runat="server" Text="塔吊、吊车、物料提升机、施工电梯等的各种防护装置和保险装置（如安全门、安全钩、限位器、限制器、安全制动器、安全监控器等）检查维护费用">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_20" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_20" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_20" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label71" runat="server" Text="机械设备、电器设备等传动部分为安全增设的安全防护装置及自动开关配置费用">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_21" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_21" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_21" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label72" runat="server" Text="锅炉、压力容器、压缩机及各种有爆炸危险的保险装置检查维护费用">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_22" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_22" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_22" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label73" runat="server" Text="为安全生产采取的信号装置、报警装置维护检查费用">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_23" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_23" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_23" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label74" runat="server" Text="吊装运输和起重" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label75" runat="server" Text="现场拆封、检查、安装准备工作所需要脚手架平台">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_24" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_24" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_24" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label76" runat="server" Text="硼砂作业" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label77" runat="server" Text="吸尘、降尘系统">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_25" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_25" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_25" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label78" runat="server" Text="拆除工程" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label79" runat="server" Text="封固、隔离、保护设施及临时平台、通道搭设">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_26" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_26" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_26" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label80" runat="server" Text="试压试车与有害介质作业" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label81" runat="server" Text="动土作业时的人工探挖、探查等措施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_27" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_27" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_27" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label82" runat="server" Text="车辆阻火器和施工机具、临时用电设备、照明设备、锤击工具防爆设施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_28" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_28" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_28" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label83" runat="server" Text="地沟、阀门井、排污井等封闭、冲洗">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_29" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_29" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_29" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label84" runat="server" Text="施工区域与生产的空间隔离和系统隔离及警戒措施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_30" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_30" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_30" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label85" runat="server" Text="清污、限污所用器材专用安全防护器材和隔离设施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_31" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_31" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_31" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label86" runat="server" Text="消音及噪声隔离设施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_32" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_32" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_32" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label87" runat="server" Text="特种作业防护" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label88" runat="server" Text="特种作业防护服，绝缘鞋，酸碱，绝缘手套，焊工面罩，鞋盖，护膝，护袖，披肩，各种专用防护眼镜，面罩，绝缘靴，自主呼吸器，防毒面具等，安全帽、安全带等">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_33" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_33" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_33" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label89" runat="server" Text="应急管理" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label90" runat="server" Text="灭火器、灭火器箱、水带、消防池、消防铲、消防桶、太平斧、消防器材架等">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_34" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_34" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_34" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label91" runat="server" Text="防火毯、防火布、接火盆、挡火板、挡风用三防布、临时消防水管安装、拆除">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_35" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_35" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_35" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label92" runat="server" Text="应急器材及演练器材动用费用、消耗费用和工时损失费用等">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_36" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_36" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_36" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label93" runat="server" Text="应急淋浴和洗眼器、酸碱灼伤专用药品等现场医务室应急器材和消防、救护车辆">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_37" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_37" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_37" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label95" runat="server" Text="非常措施" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label94" runat="server" Text="风雨季、沙尘暴、雷击、地质灾害、大水体防护和防洪特殊环境及临时处置费用">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_38" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_38" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_38" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label96" runat="server" Text="雨季、台风、沙尘暴等恶劣天气下，加固临时设施、大型施工机具等费用">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_39" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_39" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_39" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label97" runat="server" Text="其他安全措施" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label98" runat="server" Text="其他">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4_40" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType4_40" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4_40" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 9% 11% 9% 11%">
                                <Items>
                                    <f:Label ID="Label44" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label45" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSYearType4" runat="server" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label46" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMainApproveType4" runat="server" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                    <f:Form runat="server" ID="Form6" Title="5.化工试车" ShowHeader="true" ShowBorder="true"
                        BodyPadding="1px" EnableCollapse="true">
                        <Items>
                            <f:Label ID="Label99" runat="server" Text="装置区封闭管理" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label100" runat="server" Text="装置区域封闭用围挡">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType5_1" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType5_1" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType5_1" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label101" runat="server" Text="防爆施工器具" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label102" runat="server" Text="防爆电箱、防爆插头插座、防爆灯具、防爆施工机具器具">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType5_2" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType5_2" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType5_2" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label103" runat="server" Text="标识标签与锁定" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label104" runat="server" Text="盲板管理用货架、专用锁具、专用标签与警示标志">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType5_3" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType5_3" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType5_3" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label105" runat="server" Text="关键场所封闭" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label106" runat="server" Text="专区封闭管理措施费用">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType5_4" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType5_4" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType5_4" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label107" runat="server" Text="催化剂加装还原" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label108" runat="server" Text="防毒、放辐射措施和加氢点警戒、监护">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType5_5" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType5_5" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType5_5" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:Label ID="Label109" runat="server" Text="联动和化工试车" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label110" runat="server" Text="其他专项措施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType5_6" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType5_6" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType5_6" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 9% 11% 9% 11%">
                                <Items>
                                    <f:Label ID="Label111" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType5" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label112" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSYearType5" runat="server" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label113" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMainApproveType5" runat="server" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                    <f:Form runat="server" ID="Form7" Title="6.教育培训" ShowHeader="true" ShowBorder="true"
                        BodyPadding="1px" EnableCollapse="true">
                        <Items>
                            <f:Label ID="Label114" runat="server" Text="教育培训" CssClass="font_style">
                            </f:Label>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label115" runat="server" Text="安全教育培训工时占用费（入场教育、专项培训、违章停工教育）">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType6_1" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType6_1" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType6_1" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label116" runat="server" Text="师资费用">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType6_2" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType6_2" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType6_2" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                <Items>
                                    <f:Label ID="Label117" runat="server" Text="安全培训教育器材、教材">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType6_3" runat="server" Label="当月累计" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSYearType6_3" runat="server" Label="当年累计" LabelAlign="Right"
                                        Readonly="true">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType6_3" runat="server" Label="总包审核值" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 20% 9% 11% 9% 11%">
                                <Items>
                                    <f:Label ID="Label118" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType6" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label119" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSYearType6" runat="server" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label120" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMainApproveType6" runat="server" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                </Items>
                <Items>
                    <f:Panel ID="Panel3" Title="临时设施,文明施工和环境保护" Width="1170px" runat="server" BodyPadding="1px"
                        ShowBorder="true" ShowHeader="true" EnableCollapse="false">
                        <Items>
                            <f:Form runat="server" ID="Form8" Title="1.文明施工和环境保护" ShowHeader="true" ShowBorder="true"
                                BodyPadding="1px" EnableCollapse="true">
                                <Items>
                                    <f:Label ID="Label121" runat="server" Text="防护控制和排放" CssClass="font_style">
                                    </f:Label>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label122" runat="server" Text="减震与降低噪音设施">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType1_1" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType1_1" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType1_1" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label123" runat="server" Text="射线防护的设施、措施">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType1_2" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType1_2" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType1_2" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label124" runat="server" Text="声、光、尘、有害物质防逸散控制措施">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType1_3" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType1_3" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType1_3" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label125" runat="server" Text="现场出入口和特定场所车辆、器材、人员清洗盥洗设施或设备">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType1_4" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType1_4" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType1_4" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label126" runat="server" Text="土方覆盖遮挡与洒水及其他施工扬尘控制设施设备">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType1_5" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType1_5" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType1_5" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label127" runat="server" Text="运输车辆及输送装置封闭或覆盖设施">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType1_6" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType1_6" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType1_6" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label128" runat="server" Text="消纳施工污水的设施、措施（沟渠、槽池、管线、机泵及附属临建）">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType1_7" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType1_7" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType1_7" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label129" runat="server" Text="易燃易爆、有毒有害、高腐蚀物质的使用保管、运输、回收的设施、措施">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType1_8" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType1_8" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType1_8" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label130" runat="server" Text="现场毒害物质使用、消纳及意外应急相关设施">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType1_9" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType1_9" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType1_9" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label131" runat="server" Text="危险物质特性说明书展示牌">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType1_10" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType1_10" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType1_10" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label132" runat="server" Text="施工废弃物、生活垃圾分类存放和消纳的设施、措施">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType1_11" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType1_11" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType1_11" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 9% 11% 9% 11%">
                                        <Items>
                                            <f:Label ID="Label133" runat="server">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType1" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label ID="Label134" runat="server">
                                            </f:Label>
                                            <f:NumberBox ID="txtTYearType1" runat="server" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label ID="Label135" runat="server">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMainApproveType1" runat="server" Readonly="true">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                            </f:Form>
                        </Items>
                        <Items>
                            <f:Form runat="server" ID="Form9" Title="2.临时设施" ShowHeader="true" ShowBorder="true"
                                BodyPadding="1px" EnableCollapse="true" Hidden="true">
                                <Items>
                                    <f:Label ID="Label136" runat="server" Text="现场临设" CssClass="font_style">
                                    </f:Label>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label137" runat="server" Text="工人休息室，仓库">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType2_1" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType2_1" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType2_1" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label138" runat="server" Text="大门入口处职工车辆停放棚">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType2_2" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType2_2" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType2_2" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label139" runat="server" Text="施工机械防雨棚；气瓶存放棚和防爆墙；现场厕所">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType2_3" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType2_3" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType2_3" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label140" runat="server" Text="现场吸烟与饮水休息棚">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType2_4" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType2_4" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType2_4" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label141" runat="server" Text="现场临时用电系统">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType2_5" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType2_5" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType2_5" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:Label ID="Label142" runat="server" Text="生活设施" CssClass="font_style">
                                    </f:Label>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label143" runat="server" Text="宿舍、食堂、文化室、浴室、厕所、道路、生活用水电、取暖降温设施等">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType2_6" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType2_6" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType2_6" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label144" runat="server" Text="洗衣、消毒等设施和日常保洁费用">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType2_7" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType2_7" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType2_7" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:Label ID="Label145" runat="server" Text="办公设施" CssClass="font_style">
                                    </f:Label>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label146" runat="server" Text="办公室、会议室、资料室等办公用设施">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType2_8" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType2_8" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType2_8" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 20% 20%">
                                        <Items>
                                            <f:Label ID="Label147" runat="server" Text="消毒等日常保洁用品">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType2_9" runat="server" Label="当月累计" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTYearType2_9" runat="server" Label="当年累计" LabelAlign="Right"
                                                Readonly="true">
                                            </f:NumberBox>
                                            <f:NumberBox ID="txtTMainApproveType2_9" runat="server" Label="总包审核值" LabelAlign="Right"
                                                AutoPostBack="true" OnTextChanged="MainApproveTypeText__TextChanged">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 9% 11% 9% 11%">
                                        <Items>
                                            <f:Label ID="Label148" runat="server">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMonthType2" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label ID="Label149" runat="server">
                                            </f:Label>
                                            <f:NumberBox ID="txtTYearType2" runat="server" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label ID="Label150" runat="server">
                                            </f:Label>
                                            <f:NumberBox ID="txtTMainApproveType2" runat="server" Readonly="true">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                                <Items>
                                    <f:FormRow ColumnWidths="40% 20% 9% 11% 9% 11%">
                                        <Items>
                                            <f:Label ID="Label151" runat="server">
                                            </f:Label>
                                            <f:NumberBox ID="txtMonthType" runat="server" Label="费用累计" LabelAlign="Right" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label ID="Label152" runat="server">
                                            </f:Label>
                                            <f:NumberBox ID="txtYearType" runat="server" Readonly="true">
                                            </f:NumberBox>
                                            <f:Label ID="Label153" runat="server">
                                            </f:Label>
                                            <f:NumberBox ID="txtMainApproveType" runat="server" Readonly="true">
                                            </f:NumberBox>
                                        </Items>
                                    </f:FormRow>
                                </Items>
                            </f:Form>
                        </Items>
                    </f:Panel>
                </Items>
            </f:Panel>
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
