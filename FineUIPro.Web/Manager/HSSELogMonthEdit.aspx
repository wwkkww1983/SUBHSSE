<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSELogMonthEdit.aspx.cs" Inherits="FineUIPro.Web.Manager.HSSELogMonthEdit" ValidateRequest="false" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>编辑HSE经理暨HSE工程师细则</title>
    <link href="../Styles/Style.css" rel="stylesheetasp" type="text/css" />
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        .BackColor
        {
            color: Red;
            background-color: Silver;
        }
        .titler
        {
            color: Black;
            font-size: large;
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>
    <f:Panel ID="Panel2" runat="server" Height="80px" Width="980px" ShowBorder="false"
        Title="HSE经理暨HSE工程师细则表头" Layout="Table" TableConfigColumns="3" ShowHeader="false" BodyPadding="1px">
        <Items>
            <f:Panel ID="Panel1" Title="Panel1" Width="200px" Height="80px" MarginRight="0" TableRowspan="3"
                runat="server" BodyPadding="1px" ShowBorder="false" ShowHeader="false">
                <Items>
                    <f:Image ID="Image1" runat="server" ImageUrl="../Images/Null.jpg" LabelAlign="right"></f:Image>
                </Items>
            </f:Panel>
            <f:Panel ID="Panel3" Title="Panel1" Width="500px" Height="30px" runat="server" BodyPadding="1px"
                ShowBorder="false" ShowHeader="false">
                <Items>
                    <f:Label ID="lblProjectName" runat="server" CssClass="titler" Margin="5 0 0 10">
                    </f:Label>
                </Items>
            </f:Panel>
            <f:Panel ID="Panel5" Title="Panel1"  Height="30px" runat="server" BodyPadding="1px"
                ShowBorder="false" ShowHeader="false">
                <Items>
                    <f:TextBox ID="lblProjectCode" runat="server" LabelAlign="Right" Label="项目号" Readonly="true" LabelWidth="80px" Width="200px">
                    </f:TextBox>
                </Items>
            </f:Panel>
            <f:Panel ID="Panel4" Title="Panel1" Width="350px" Height="30px" TableRowspan="2"
                runat="server" BodyPadding="1px" ShowBorder="false" ShowHeader="false">
                <Items>
                    <f:Label ID="lbTitleName" runat="server" CssClass="titler" Margin="5 0 0 40">
                    </f:Label>
                </Items>
            </f:Panel>
            <f:Panel ID="Panel7" Title="Panel1"  Height="30px" runat="server" BodyPadding="1px"
                ShowBorder="false" ShowHeader="false">
                <Items>
                     <f:TextBox ID="lbHSSELogMonthCode" runat="server" LabelAlign="Right" Label="编号"
                        Required="true" ShowRedStar="true" Readonly="true" LabelWidth="60px" Width="200px">
                    </f:TextBox>            
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    <f:Panel ID="Panel6" runat="server" Width="980px" ShowBorder="false" Layout="VBox"  ShowHeader="false" BodyPadding="1px"
        Title="HSE经理暨HSE工程师细则" >
        <Items>
            <f:Form ID="Form3" runat="server" ShowHeader="false" AutoScroll="true">
                <Items>
                    <f:FormRow >
                        <Items>
                            <f:DatePicker ID="txtMonths" runat="server" Label="月份" LabelAlign="Right" Required="true" ShowRedStar="true"
                                    EnableEdit="true" DateFormatString="yyyy-MM"  FocusOnPageLoad="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                             </f:DatePicker>
                             <f:DatePicker ID="txtCompileDate" runat="server" Label="编制日期" LabelAlign="Right" Required="true" ShowRedStar="true"
                                    EnableEdit="true">
                             </f:DatePicker>
                            <f:TextBox ID="txtCompileMan" runat="server" Label="责任人" LabelAlign="Right" Readonly="true" Required="true" ShowRedStar="true">
                            </f:TextBox>
                        </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items>
                            <f:Label ID="Label1" runat="server" CssClass="titler" Text="1.项目信息" LabelAlign="Left">
                            </f:Label>
                        </Items>
                    </f:FormRow> 
                    <f:FormRow >
                        <Items>                           
                            <f:TextBox ID="txtProjectName" runat="server" Label="项目名称" LabelAlign="Right" Readonly="true">
                            </f:TextBox>
                            <f:TextBox ID="txtProjectRange" runat="server" Label="工程范围" LabelAlign="Right" Readonly="true">
                            </f:TextBox>
                        </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items>
                            <f:Label ID="Label2" runat="server" CssClass="titler" Text="2.HSE人工日统计" LabelAlign="Left">
                            </f:Label>
                        </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items>
                            <f:Label ID="Label3" runat="server" Text="本月责任范围内人工日" Margin="0 0 0 50">
                            </f:Label>
                            <f:Label ID="Label4" runat="server" Text="难度系数" Margin="0 0 0 70">
                            </f:Label>
                            <f:Label ID="Label5" runat="server" Text="本月折合人工日" Margin="0 0 0 50">
                            </f:Label>
                            <f:Label ID="Label6" runat="server" Text="年度累计人工日" Margin="0 0 0 50">
                            </f:Label>
                        </Items>
                    </f:FormRow> 
                    <f:FormRow >
                        <Items>                           
                            <f:NumberBox ID="txtManHour" runat="server" LabelAlign="Left"  NoDecimal="true" NoNegative="true" AutoPostBack="true" OnTextChanged="Value_TextChanged"></f:NumberBox>
                            <f:NumberBox ID="txtRate" runat="server" LabelAlign="Left"  NoNegative="true" AutoPostBack="true" OnTextChanged="Value_TextChanged"></f:NumberBox>
                            <f:NumberBox ID="txtRealManHour" runat="server" LabelAlign="Left"  NoNegative="true"></f:NumberBox>
                            <f:NumberBox ID="txtTotalManHour" runat="server" LabelAlign="Left"  NoNegative="true"></f:NumberBox>                           
                        </Items>
                    </f:FormRow> 
                    <f:FormRow >
                        <Items>
                            <f:Label ID="Label7" runat="server" CssClass="titler" Text="3.HSE现场管理" LabelAlign="Left">
                            </f:Label>
                        </Items>
                    </f:FormRow> 
                    <f:FormRow >
                        <Items>                           
                            <f:TextBox ID="txtNum1" runat="server" Label="HSE检查次数" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                         </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items> 
                            <f:TextBox ID="txtNum2" runat="server" Label="隐患整改数量" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                     </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items> 
                            <f:TextBox ID="txtNum3" runat="server" Label="作业票办理数量" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                             </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items> 
                            <f:TextBox ID="txtNum4" runat="server" Label="机械、安全设施验收数量" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                             </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items> 
                            <f:TextBox ID="txtNum5" runat="server" Label="危险源辨识活动次数" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                             </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items> 
                            <f:TextBox ID="txtNum6" runat="server" Label="应急计划方案修编、应急活动次数" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                             </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items> 
                            <f:TextBox ID="txtNum7" runat="server" Label="HSE培训人次" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                             </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items> 
                            <f:TextBox ID="txtNum8" runat="server" Label="HSE会议数量" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                             </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items> 
                            <f:TextBox ID="txtNum9" runat="server" Label="HSE宣传活动次数" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                             </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items> 
                            <f:TextBox ID="txtNum10" runat="server" Label="HSE奖励次数" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                             </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items> 
                            <f:TextBox ID="txtNum11" runat="server" Label="HSE处罚次数" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                        </Items>
                    </f:FormRow> 
                    <f:FormRow >
                        <Items>
                            <f:Label ID="Label8" runat="server" CssClass="titler" Text="4.HSE内业管理" LabelAlign="Left">
                            </f:Label>
                        </Items>
                    </f:FormRow> 
                    <f:FormRow >
                        <Items>                           
                            <f:TextBox ID="txtNum12" runat="server" Label="HSE体系文件修编数量" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                        </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items>                           
                            <f:TextBox ID="txtNum13" runat="server" Label="HSE资质方案核查数量" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                        </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items>                           
                            <f:TextBox ID="txtNum14" runat="server" Label="HSE费用核查次数" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                        </Items>
                    </f:FormRow>
                    <f:FormRow >
                        <Items>                           
                            <f:TextBox ID="txtNum15" runat="server" Label="HSE文件资料分类上档数量" LabelAlign="Right" Readonly="true" LabelWidth="250px">
                            </f:TextBox>
                        </Items>
                    </f:FormRow>                       
                </Items>
            </f:Form>
        </Items>
    </f:Panel>        
    <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
        <Items>
            <f:HiddenField ID="hdTotalManHour" runat="server"></f:HiddenField>
            <f:ToolbarFill ID="ToolbarFill1" runat="server">
            </f:ToolbarFill>
            <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                OnClick="btnSave_Click">
            </f:Button>            
            <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
            </f:Button>
        </Items>
    </f:Toolbar>
    </form>
</body>
</html>
