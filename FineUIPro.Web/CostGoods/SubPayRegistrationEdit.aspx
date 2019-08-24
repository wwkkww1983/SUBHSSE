<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubPayRegistrationEdit.aspx.cs" Inherits="FineUIPro.Web.CostGoods.SubPayRegistrationEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑分包商安全费用投入登记</title>
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
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Panel ID="Panel1" runat="server" Width="1170px" ShowBorder="True" EnableCollapse="true"
        CssClass="mytable" Layout="Table" TableConfigColumns="3" ShowHeader="False" Title="分包商安全费用投入登记">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <f:DatePicker ID="txtDate" runat="server" Label="日期" LabelAlign="Right" LabelWidth="60px">
                    </f:DatePicker>
                    <f:DropDownList ID="drpUnit" runat="server" Label="单位" ShowRedStar="true" Required="true" LabelWidth="60px" Width="300px">
                    </f:DropDownList>
                    <f:TextBox ID="txtContractNum" runat="server" Label="合同号" LabelWidth="70px">
                    </f:TextBox>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Panel ID="Panel2" Title="费用投入明细" Width="1170px" runat="server" BodyPadding="1px"
                ShowBorder="true" ShowHeader="false" EnableCollapse="false">
                <Items>
                     <f:Form runat="server" ID="Form2" Title="1.基础管理" ShowHeader="true" ShowBorder="true"
                        BodyPadding="1px" EnableCollapse="true">
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label23" runat="server" Text="">
                                    </f:Label>
                                    <f:Label ID="Label31" runat="server" Text="本阶段投入费用额度（元）">
                                    </f:Label>
                                    <f:Label ID="Label32" runat="server" Text="中国五环审核额（元）">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label3" runat="server" Text="内业管理">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType1" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType1" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label1" runat="server" Text="检测器材">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType2" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType2" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label2" runat="server" Text="警示警戒">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType3" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType3" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label6" runat="server" Text="安全奖励">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType4" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType4" runat="server" Label="" LabelAlign="Right"
                                      AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged"  Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label7" runat="server" Text="其他">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType5" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType5" runat="server" Label="" LabelAlign="Right"
                                      AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged"  Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 9% 21%">
                                <Items>
                                    <f:Label ID="Label21" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthTypeTotal1" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label22" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMainApproveTypeTotal1" runat="server" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                    <f:Form runat="server" ID="Form3" Title="2.安全技术" ShowHeader="true" ShowBorder="true"
                        BodyPadding="1px" EnableCollapse="true">
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label4" runat="server" Text="">
                                    </f:Label>
                                    <f:Label ID="Label5" runat="server" Text="本阶段投入费用额度（元）">
                                    </f:Label>
                                    <f:Label ID="Label8" runat="server" Text="中国五环审核额（元）">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label25" runat="server" Text="安全技术">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType6" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType6" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 9% 21%">
                                <Items>
                                    <f:Label ID="Label29" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthTypeTotal2" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label30" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMainApproveTypeTotal2" runat="server" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                    <f:Form runat="server" ID="Form4" Title="3.职业健康" ShowHeader="true" ShowBorder="true"
                        BodyPadding="1px" EnableCollapse="true">
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label9" runat="server" Text="">
                                    </f:Label>
                                    <f:Label ID="Label10" runat="server" Text="本阶段投入费用额度（元）">
                                    </f:Label>
                                    <f:Label ID="Label11" runat="server" Text="中国五环审核额（元）">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label12" runat="server" Text="工业卫生">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType7" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType7" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 9% 21%">
                                <Items>
                                    <f:Label ID="Label13" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthTypeTotal3" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label14" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMainApproveTypeTotal3" runat="server" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                    <f:Form runat="server" ID="Form5" Title="4.防护措施" ShowHeader="true" ShowBorder="true"
                        BodyPadding="1px" EnableCollapse="true">
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label15" runat="server" Text="">
                                    </f:Label>
                                    <f:Label ID="Label16" runat="server" Text="本阶段投入费用额度（元）">
                                    </f:Label>
                                    <f:Label ID="Label17" runat="server" Text="中国五环审核额（元）">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label18" runat="server" Text="安全用电">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType8" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType8" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label24" runat="server" Text="高处作业及基坑">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType9" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType9" runat="server" Label="" LabelAlign="Right"
                                      AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged"  Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label26" runat="server" Text="临边洞口防护">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType10" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType10" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label27" runat="server" Text="受限空间内作业">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType11" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType11" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label28" runat="server" Text="动火作业">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType12" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType12" runat="server" Label="" LabelAlign="Right"
                                      AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged"  Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label33" runat="server" Text="机械装备防护">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType13" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType13" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label34" runat="server" Text="吊装运输和起重">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType14" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType14" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label35" runat="server" Text="硼砂作业">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType15" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType15" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label36" runat="server" Text="拆除工程">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType16" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType16" runat="server" Label="" LabelAlign="Right"
                                      AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged"  Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label37" runat="server" Text="试压试车与有害介质作业">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType17" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType17" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label38" runat="server" Text="特种作业防护">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType18" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType18" runat="server" Label="" LabelAlign="Right"
                                      AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged"  Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label39" runat="server" Text="应急管理">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType19" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType19" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label40" runat="server" Text="非常措施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType20" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType20" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label41" runat="server" Text="其他安全措施">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType21" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType21" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 9% 21%">
                                <Items>
                                    <f:Label ID="Label19" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthTypeTotal4" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label20" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMainApproveTypeTotal4" runat="server" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                    <f:Form runat="server" ID="Form6" Title="5.化工试车" ShowHeader="true" ShowBorder="true"
                        BodyPadding="1px" EnableCollapse="true">
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label42" runat="server" Text="">
                                    </f:Label>
                                    <f:Label ID="Label43" runat="server" Text="本阶段投入费用额度（元）">
                                    </f:Label>
                                    <f:Label ID="Label44" runat="server" Text="中国五环审核额（元）">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label45" runat="server" Text="装置区封闭管理">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType22" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType22" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label48" runat="server" Text="防爆施工器具">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType23" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType23" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label49" runat="server" Text="标识标签与锁定">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType24" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType24" runat="server" Label="" LabelAlign="Right"
                                      AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged"  Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label50" runat="server" Text="关键场所封闭">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType25" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType25" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label51" runat="server" Text="催化剂加装还原">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType26" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType26" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label52" runat="server" Text="联动和化工试车">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType27" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType27" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 9% 21%">
                                <Items>
                                    <f:Label ID="Label46" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthTypeTotal5" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label47" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMainApproveTypeTotal5" runat="server" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                    <f:Form runat="server" ID="Form7" Title="6.教育培训" ShowHeader="true" ShowBorder="true"
                        BodyPadding="1px" EnableCollapse="true">
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label53" runat="server" Text="">
                                    </f:Label>
                                    <f:Label ID="Label54" runat="server" Text="本阶段投入费用额度（元）">
                                    </f:Label>
                                    <f:Label ID="Label55" runat="server" Text="中国五环审核额（元）">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label56" runat="server" Text="教育培训">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType28" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType28" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 9% 21%">
                                <Items>
                                    <f:Label ID="Label57" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthTypeTotal6" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label58" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMainApproveTypeTotal6" runat="server" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                    <f:Form runat="server" ID="Form8" Title="7.文明施工和环境保护" ShowHeader="true" ShowBorder="true"
                        BodyPadding="1px" EnableCollapse="true">
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label59" runat="server" Text="">
                                    </f:Label>
                                    <f:Label ID="Label60" runat="server" Text="本阶段投入费用额度（元）">
                                    </f:Label>
                                    <f:Label ID="Label61" runat="server" Text="中国五环审核额（元）">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 30%">
                                <Items>
                                    <f:Label ID="Label62" runat="server" Text="防护控制和排放">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthType29" runat="server" Label="" LabelAlign="Right"
                                        AutoPostBack="true" OnTextChanged="SMonthTypeText_TextChanged">
                                    </f:NumberBox>
                                    <f:NumberBox ID="txtSMainApproveType29" runat="server" Label="" LabelAlign="Right"
                                       AutoPostBack="true" OnTextChanged="SMainApproveTypeText_TextChanged" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 9% 21%">
                                <Items>
                                    <f:Label ID="Label63" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthTypeTotal7" runat="server" Label="费用小计" LabelAlign="Right" Readonly="true">
                                    </f:NumberBox>
                                    <f:Label ID="Label64" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMainApproveTypeTotal7" runat="server" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                        <Items>
                            <f:FormRow ColumnWidths="40% 30% 9% 21%">
                                <Items>
                                    <f:Label ID="Label65" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMonthTypeTotal" runat="server" Label="本阶段费用累计" LabelAlign="Right" Readonly="true" LabelWidth="130px">
                                    </f:NumberBox>
                                    <f:Label ID="Label66" runat="server">
                                    </f:Label>
                                    <f:NumberBox ID="txtSMainApproveTypeTotal" runat="server" Readonly="true">
                                    </f:NumberBox>
                                </Items>
                            </f:FormRow>
                        </Items>
                    </f:Form>
                </Items>
            </f:Panel>
        </Items>

        <Toolbars>
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
        </Toolbars>
    </f:Panel>
    
    </form>
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
</body>
</html>
