<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCView6.aspx.cs" Inherits="FineUIPro.Web.Manager.MonthReportCView6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server">
    </f:PageManager>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
               <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel42" Layout="Anchor" Title="6.本月项目HSE费用管理" runat="server">
                        <Items>
                            <f:Form ID="Form11" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                <Rows>
                                    <f:FormRow>
                                        <Items>
                                            <f:Label runat="server" ID="Label18" Text="类型">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label13" Text="内容">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label19" Text="当月累计">
                                            </f:Label>
                                            <f:Label runat="server" ID="Label20" Text="当年累计">
                                            </f:Label>
                                        </Items>
                                    </f:FormRow>
                                    <f:FormRow>
                                        <Items>
                                            <f:Panel ID="Panel57" runat="server" ShowBorder="false" Title="" Layout="Table" TableConfigColumns="4"
                                                ShowHeader="false" BodyPadding="1px">
                                                <Items>
                                                    <f:Panel ID="Panel58" Title="Panel1" MarginRight="0" TableRowspan="6" runat="server"
                                                        Width="250px" BodyPadding="1px" ShowBorder="false" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="Label14" Text="安全防护" TableRowspan="6">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel59" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="lblBaseManager" Text="基础管理">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel60" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtSMonthType1" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel61" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtYearSMonthType1" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel62" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="lblIncentiveType12" Text="安全技术">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel63" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtSMonthType2" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel64" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtYearSMonthType2" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel65" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="lblIncentiveType13" Text="职业健康">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel66" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtSMonthType3" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel67" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtYearSMonthType3" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel68" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="lblIncentiveType14" Text="防护措施">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel69" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtSMonthType4" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel70" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtYearSMonthType4" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel1" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="Label16" Text="化工试车">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel2" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtSMonthType5" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel3" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtYearSMonthType5" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel4" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="Label17" Text="教育培训">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel5" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtSMonthType6" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel6" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtYearSMonthType6" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel71" Title="Panel1" MarginRight="0" TableRowspan="2" runat="server"
                                                        Width="250px" BodyPadding="1px" ShowBorder="false" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="Label15" Text="临时设施文明施工和环境保护">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel72" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="lblIncentiveType21" Text="文明施工和环境保护">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel73" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtTMonthType1" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel74" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtYearTMonthType1" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel75" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:Label runat="server" ID="lblIncentiveType22" Text="临时设施">
                                                            </f:Label>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel76" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtTMonthType2" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                    <f:Panel ID="Panel77" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                        Width="250px" ShowHeader="false">
                                                        <Items>
                                                            <f:TextBox ID="txtYearTMonthType2" Readonly="true" runat="server">
                                                            </f:TextBox>
                                                        </Items>
                                                    </f:Panel>
                                                </Items>
                                            </f:Panel>
                                        </Items>
                                    </f:FormRow>
                                </Rows>
                            </f:Form>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    </form>
</body>
</html>
