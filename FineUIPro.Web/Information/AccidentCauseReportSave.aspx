<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentCauseReportSave.aspx.cs" Async="true"
    Inherits="FineUIPro.Web.Information.AccidentCauseReportSave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true" 
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">        
        <Rows>
            <f:FormRow ColumnWidths="31% 11% 10% 10% 10% 28%">
                <Items>
                    <f:Label runat="server">
                    </f:Label>
                    <f:Label runat="server" Text="职工伤亡事故原因分析">
                    </f:Label>
                    <f:DropDownList ID="drpMonth" AutoPostBack="true" EnableSimulateTree="true" Required="true" 
                        ShowRedStar="true" runat="server" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:DropDownList ID="drpYear" AutoPostBack="false" EnableSimulateTree="true" Required="true"
                        ShowRedStar="true" runat="server">
                    </f:DropDownList>
                    <f:Label ID="Label1" runat="server" Text="报表">
                    </f:Label>
                    <f:Label runat="server">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="8% 25% 37% 30%">
                <Items>
                    <f:Label ID="Label3" runat="server" Text="一、填报单位：">
                    </f:Label>
                    <f:DropDownList ID="drpUnit" AutoPostBack="false" EnableSimulateTree="true" runat="server" Required="true"
                        ShowRedStar="true">
                    </f:DropDownList>
                    <f:Label ID="Label2" runat="server">
                    </f:Label>
                    <f:TextBox ID="txtAccidentCauseReportCode" runat="server" Label="编号" MaxLength="50" FocusOnPageLoad="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 5% 8% 6% 5% 6% 7% 6% 5% 6% 7% 6% 5% 6% 5%">
                <Items>
                    <f:Label ID="Label4" runat="server" Text="二、本月">
                    </f:Label>
                    <f:Label ID="lbMonth1" runat="server">
                    </f:Label>
                    <f:Label ID="Label5" runat="server" Text="份发生死亡事故">
                    </f:Label>
                    <f:NumberBox ID="txtDeathAccident" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label6" runat="server" Text="次，死亡">
                    </f:Label>
                    <f:NumberBox ID="txtDeathToll" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label7" runat="server" Text="人，重伤事故">
                    </f:Label>
                    <f:NumberBox ID="txtInjuredAccident" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label8" runat="server" Text="次，重伤">
                    </f:Label>
                    <f:NumberBox ID="txtInjuredToll" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label9" runat="server" Text="人，轻伤事故">
                    </f:Label>
                    <f:NumberBox ID="txtMinorWoundAccident" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label10" runat="server" Text="次，轻伤">
                    </f:Label>
                    <f:NumberBox ID="txtMinorWoundToll" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label11" runat="server" Text="人。">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 5% 8% 10% 4% 10% 6%">
                <Items>
                    <f:Label ID="Label12" runat="server" Text="三、本月">
                    </f:Label>
                    <f:Label ID="lbMonth2" runat="server">
                    </f:Label>
                    <f:Label ID="Label14" runat="server" Text="份平均工时总数">
                    </f:Label>
                    <f:NumberBox ID="txtAverageTotalHours" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label15" runat="server" Text="，人数">
                    </f:Label>
                    <f:NumberBox ID="txtAverageManHours" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label16" runat="server" Text="人。">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="14% 10% 6% 5% 9% 10% 6%">
                <Items>
                    <f:Label ID="Label21" runat="server" Text="四、本月事故损失工时总数">
                    </f:Label>
                    <f:NumberBox ID="txtTotalLossMan" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label23" runat="server" Text="小时；上月">
                    </f:Label>
                    <f:Label ID="lbLastMonth" runat="server">
                    </f:Label>
                    <f:Label ID="Label24" runat="server" Text="事故损失工时总数">
                    </f:Label>
                    <f:NumberBox ID="txtLastMonthLossHoursTotal" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label25" runat="server" Text="。">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="8% 5% 9% 10% 6%">
                <Items>
                    <f:Label ID="Label13" runat="server" Text="五、伤者在本月">
                    </f:Label>
                    <f:Label ID="lbMonth3" runat="server">
                    </f:Label>
                    <f:Label ID="Label17" runat="server" Text="份内的歇工总日数">
                    </f:Label>
                    <f:NumberBox ID="txtKnockOffTotal" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label20" runat="server" Text="天。">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="9% 10% 6% 10% 5% 10% 6%">
                <Items>
                    <f:Label ID="Label18" runat="server" Text="六、事故直接损失">
                    </f:Label>
                    <f:NumberBox ID="txtDirectLoss" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label22" runat="server" Text="，间接损失">
                    </f:Label>
                    <f:NumberBox ID="txtIndirectLosses" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label26" runat="server" Text="，总损失">
                    </f:Label>
                    <f:NumberBox ID="txtTotalLoss" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label19" runat="server" Text="。">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="10% 10% 6%">
                <Items>
                    <f:Label ID="Label27" runat="server" Text="七、无损失工时总数">
                    </f:Label>
                    <f:NumberBox ID="txtTotalLossTime" runat="server">
                    </f:NumberBox>
                    <f:Label ID="Label28" runat="server" Text="。">
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="AccidentCauseReportItemId" AllowCellEditing="true"
                        ClicksToEdit="1" DataIDField="AccidentCauseReportItemId" EnableColumnLines="true" EnableHeaderMenu="false" Width="1300px" Height="300px">
                        <Columns>
                            <f:RenderField Width="100px" ColumnID="AccidentType" DataField="AccidentType" FieldType="String"
                                HeaderText="事故类别" HeaderTextAlign="Center">
                            </f:RenderField>
                            <f:GroupField EnableLock="true" HeaderText="防护、保险信号等装置缺乏或装置缺乏或有缺陷" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="100px" ColumnID="Death1" DataField="Death1" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtDeath1" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Death1")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="100px" ColumnID="Injuries1" DataField="Injuries1" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtInjuries1" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Injuries1")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="100px" ColumnID="MinorInjuries1" DataField="MinorInjuries1"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtMinorInjuries1" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("MinorInjuries1")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="设备、工具附件有缺陷" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="60px" ColumnID="Death2" DataField="Death2" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtDeath2" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Death2")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="Injuries2" DataField="Injuries2" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtInjuries2" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Injuries2")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="MinorInjuries2" DataField="MinorInjuries2"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtMinorInjuries2" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("MinorInjuries2")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="个人防护用品缺乏或有缺陷" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="60px" ColumnID="Death3" DataField="Death3" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtDeath3" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Death3")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="Injuries3" DataField="Injuries3" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtInjuries3" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Injuries3")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="MinorInjuries3" DataField="MinorInjuries3"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtMinorInjuries3" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("MinorInjuries3")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="光线不足或工作地点及通道情况不良" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="Death4" DataField="Death4" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtDeath4" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Death4")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="Injuries4" DataField="Injuries4" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtInjuries4" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Injuries4")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="MinorInjuries4" DataField="MinorInjuries4"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtMinorInjuries4" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("MinorInjuries4")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="劳动组织不合理" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death5" DataField="Death5" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtDeath5" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Death5")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries5" DataField="Injuries5" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtInjuries5" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Injuries5")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries5" DataField="MinorInjuries5"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtMinorInjuries5" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("MinorInjuries5")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="对现场工作缺乏检查或指导有错误" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="80px" ColumnID="Death6" DataField="Death6" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtDeath6" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Death6")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="Injuries6" DataField="Injuries6" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtInjuries6" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Injuries6")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="80px" ColumnID="MinorInjuries6" DataField="MinorInjuries6"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtMinorInjuries6" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("MinorInjuries6")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="设计有缺陷" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death7" DataField="Death7" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtDeath7" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Death7")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries7" DataField="Injuries7" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtInjuries7" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Injuries7")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries7" DataField="MinorInjuries7"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtMinorInjuries7" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("MinorInjuries7")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="不懂操作技术和知识" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death8" DataField="Death8" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtDeath8" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Death8")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries8" DataField="Injuries8" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtInjuries8" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Injuries8")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries8" DataField="MinorInjuries8"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtMinorInjuries8" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("MinorInjuries8")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="违反操作规程或劳动纪律" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="60px" ColumnID="Death9" DataField="Death9" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtDeath9" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Death9")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="Injuries9" DataField="Injuries9" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtInjuries9" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Injuries9")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="60px" ColumnID="MinorInjuries9" DataField="MinorInjuries9"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtMinorInjuries9" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("MinorInjuries9")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="没有安全操作规程制度或不健全" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="70px" ColumnID="Death10" DataField="Death10" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtDeath10" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Death10")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="70px" ColumnID="Injuries10" DataField="Injuries10" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtInjuries10" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Injuries10")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="70px" ColumnID="MinorInjuries10" DataField="MinorInjuries10"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtMinorInjuries10" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("MinorInjuries10")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                            <f:GroupField EnableLock="true" HeaderText="其他" TextAlign="Center">
                                <Columns>
                                    <f:RenderField Width="50px" ColumnID="Death11" DataField="Death11" FieldType="String"
                                        HeaderText="死" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtDeath11" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Death11")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="Injuries11" DataField="Injuries11" FieldType="String"
                                        HeaderText="重" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtInjuries11" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Injuries11")%>'
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="50px" ColumnID="MinorInjuries11" DataField="MinorInjuries11"
                                        FieldType="String" HeaderText="轻" HeaderTextAlign="Center">
                                        <Editor>
                                            <f:NumberBox ID="txtMinorInjuries11" NoDecimal="true" NoNegative="true" MinValue="0"
                                                Text='<%# Eval("MinorInjuries11")%>' runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:GroupField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox runat="server" Label="填报单位负责人" MaxLength="50" LabelWidth="130px" ID="txtFillCompanyPersonCharge">
                    </f:TextBox>
                    <f:TextBox runat="server" Label="制表人" MaxLength="50" ID="txtTabPeople">
                    </f:TextBox>
                    <f:TextBox runat="server" Label="审核人" MaxLength="50" ID="txtAuditPerson">
                    </f:TextBox>
                    <f:DatePicker runat="server" Label="填报日期" ID="txtFillingDate">
                    </f:DatePicker>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server" Margin="0 50 30 50">
                <Items>
                     <f:Button ID="btnCopy" Icon="Database" runat="server" ToolTip="复制上月数据"
                        ValidateForms="SimpleForm1" OnClick="btnCopy_Click" Hidden = "true">
                    </f:Button>
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" Hidden="true"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnSubmit" Icon="SystemSaveNew" runat="server" Hidden="true" ToolTip="提交" ValidateForms="SimpleForm1"
                        OnClick="btnSubmit_Click">
                    </f:Button>
                    <f:Button ID="btnUpdata" Icon="PageSave" runat="server" Hidden="true" ConfirmText="确定上报？" ToolTip="上报" ValidateForms="SimpleForm1"
                        OnClick="btnUpdata_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    <f:Window ID="Window1" IconUrl="~/res/images/16/11.png" runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" OnClose="Window1_Close"
        Title="办理流程" CloseAction="HidePostBack" EnableIFrame="true" Height="250px"
        Width="500px">
    </f:Window>
    </form>
</body>
</html>
