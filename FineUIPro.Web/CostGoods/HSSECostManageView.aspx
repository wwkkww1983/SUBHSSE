<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSECostManageView.aspx.cs" Inherits="FineUIPro.Web.CostGoods.HSSECostManageView" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员信息</title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
          .f-grid-row.Red
        {
            background-color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
             <f:Panel runat="server" ID="panel2" RegionPosition="Top" RegionSplit="true"  ShowHeader="false"
                EnableCollapse="true"  AutoScroll="true" BodyPadding="5px" Layout="VBox" Height="600px">                 
                <Items>                    
                    <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                            BodyPadding="1px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" 
                            Layout="VBox"> 
                        <Rows>
                            <f:FormRow>
                                <Items>
                                   <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                                    BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" EnableTableStyle="true"
                                    Layout="VBox">                      
                                    <Rows>
                                         <f:FormRow>
                                           <Items>
                                                <f:TextBox ID="txtMonth" runat="server" Label="月份"  LabelWidth="100px" Readonly="true">
                                                </f:TextBox>
                                                <f:TextBox ID="txtCode" runat="server" Label="编号" LabelWidth="100px" Readonly="true">
                                                </f:TextBox>
                                                <f:TextBox ID="txtReportDate" runat="server" Label="填报日期" LabelWidth="100px" Readonly="true">
                                                </f:TextBox>                       
                                            </Items>
                                        </f:FormRow>
                                  </Rows>
                                </f:Form>
                          </Items>
                         </f:FormRow>
                            <f:FormRow>                                
                                <Items>
                                    <f:Panel ID="Panel10" runat="server" ShowBorder="false" Layout="Table" TableConfigColumns="4"
                                    ShowHeader="false" BodyPadding="1px">
                                        <Items>                                      
                                        <f:Panel ID="Panel12"  runat="server" BodyPadding="1px" Width="280px"
                                            TableRowspan="2"  ShowHeader="false" Height="60px"  ShowBorder="false">
                                            <Items>
                                                <f:Label runat="server" ID="Label42" Text="类别" MarginLeft="100px">
                                                </f:Label>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel13" runat="server" BodyPadding="1px"  ShowBorder="false"
                                            TableColspan="2" Width="600px" ShowHeader="false" Height="30px">
                                            <Items>
                                                <f:Label runat="server" ID="Label43" Text="费用（万元）"  MarginLeft="250px">
                                                </f:Label>
                                            </Items>
                                        </f:Panel>         
                                         <f:Panel ID="Panel11" runat="server" BodyPadding="1px" Width="300px"  ShowBorder="false"
                                            TableRowspan="2"  ShowHeader="false" Height="60px">
                                            <Items>
                                                <f:Label runat="server" ID="Label41" Text="备注"  MarginLeft="10px">
                                                </f:Label>
                                            </Items>
                                        </f:Panel>
                                        <f:Panel ID="Panel14"  runat="server" BodyPadding="1px"  ShowBorder="false"
                                            Width="300px" ShowHeader="false" Height="30px">
                                            <Items>
                                                <f:Label runat="server" ID="Label44" Text="当期"  MarginLeft="10px">
                                                </f:Label>
                                            </Items>
                                        </f:Panel>                                     
                                        <f:Panel ID="Panel16"  runat="server" BodyPadding="1px"  ShowBorder="false"
                                            Width="300px" ShowHeader="false" Height="30px">
                                            <Items>
                                                <f:Label runat="server" ID="Label46" Text="项目累计"  MarginLeft="10px">
                                                </f:Label>
                                            </Items>
                                        </f:Panel>
                                        </Items>
                                    </f:Panel>
                                </Items>
                            </f:FormRow> 
                            <f:FormRow>
                                <Items>
                                    <f:Label runat="server" ID="Label48" Text="主营业务收入" MarginLeft="20px">
                                    </f:Label>
                                    <f:Label ID="txtMainIncome" runat="server"  LabelWidth="0" Width="300px" >
                                    </f:Label>                                   
                                    <f:Label ID="txtProjectMainIncome" runat="server" LabelWidth="0" Width="300px">
                                    </f:Label>
                                    <f:Label ID="txtRemark1" runat="server" LabelWidth="0" Width="300px">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow >
                                <Items>
                                    <f:Label runat="server" ID="Label50" Text="施工收入" MarginLeft="20px">
                                    </f:Label>
                                    <f:Label ID="txtConstructionIncome" runat="server"  Label="" >
                                    </f:Label>
                                    <f:Label ID="txtProjectConstructionIncome" runat="server" LabelWidth="0" Width="300px">
                                    </f:Label>
                                    <f:Label ID="txtRemark2"   runat="server" Label="">
                                    </f:Label>
                                </Items>
                            </f:FormRow> 
                            <f:FormRow >
                                <Items>                                     
                                    <f:Label runat="server" ID="Label52" Text="东华项目部已投入的安全生产费用" MarginLeft="20px">
                                    </f:Label>
                                    <f:Label ID="txtSafetyCosts"  runat="server" Label="">
                                    </f:Label>
                                    <f:Label ID="txtProjectSafetyCosts" runat="server" LabelWidth="0" Width="300px">
                                    </f:Label>
                                    <f:Label ID="txtRemark3"  runat="server" Label="">
                                    </f:Label>
                                </Items>
                            </f:FormRow> 
                            <f:FormRow >
                                <Items>                                     
                                    <f:DropDownList ID="drpUnitId" runat="server" Label="填报单位" EnableEdit="true" 
                                        Required="true" ShowRedStar="true" AutoPostBack="true" OnSelectedIndexChanged="drpUnitId_SelectedIndexChanged">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow> 
                            <f:FormRow >
                                <Items>                                     
                                    <f:Label runat="server" ID="Label45" Text="当月已支付工程款"  MarginLeft="20px">
                                    </f:Label>
                                    <f:Label ID="txtEngineeringCost"  runat="server" Label="">
                                    </f:Label>
                                    <f:Label ID="txtProjectEngineeringCost" runat="server" LabelWidth="0" Width="300px">
                                    </f:Label>
                                    <f:Label ID="txtUnitRemark1"  runat="server" Label="">
                                    </f:Label>
                                </Items>
                            </f:FormRow> 
                            <f:FormRow >
                                <Items>                                     
                                    <f:Label runat="server" ID="Label47" Text="已支付分承包商安全生产费用"  MarginLeft="20px">
                                    </f:Label>
                                    <f:Label ID="txtSubUnitCost"  runat="server" Label="" >
                                    </f:Label>
                                    <f:Label ID="txtProjectSubUnitCost" runat="server" LabelWidth="0" Width="300px">
                                    </f:Label>
                                    <f:Label ID="txtUnitRemark2"  runat="server" Label="">
                                    </f:Label>
                                </Items>
                            </f:FormRow> 
                            <f:FormRow >
                                <Items>                                     
                                    <f:Label runat="server" ID="Label49" Text="已审核分承包商安全生产费用"  MarginLeft="20px">
                                    </f:Label>
                                    <f:Label ID="txtAuditedSubUnitCost"  runat="server" Label="" >
                                    </f:Label>
                                    <f:Label ID="txtProjectAuditedSubUnitCost" runat="server" 
                                        LabelWidth="0" Width="300px">
                                    </f:Label>
                                    <f:Label ID="txtUnitRemark3" runat="server" Label="">
                                    </f:Label>
                                </Items>
                            </f:FormRow> 
                            <f:FormRow >
                                <Items>                                     
                                    <f:Label runat="server" ID="Label51" Text="承包商安全生产费用占比"  MarginLeft="20px">
                                    </f:Label>
                                    <f:Label ID="txtCostRatio" runat="server" Label="">
                                    </f:Label>
                                    <f:Label ID="txtProjectCostRatio" runat="server" LabelWidth="0" Width="300px">
                                    </f:Label>
                                    <f:Label ID="txtUnitRemark4" runat="server" Label="">
                                    </f:Label>
                                </Items>
                            </f:FormRow> 
                            <f:FormRow>
                                <Items>
                                    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="安全分包费用" AutoScroll="true"
                                        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Left"  Layout="VBox">
                                        <Rows>                           
                                            <f:FormRow>
                                                <Items>
                                                    <f:Panel ID="Panel3" runat="server" ShowBorder="false" Layout="Table" TableConfigColumns="5"
                                                        ShowHeader="false" BodyPadding="1px">
                                                        <Items>
                                                            <f:Panel ID="Panel4" Title="Panel1" TableRowspan="2" Height="60px" Width="200px"
                                                                runat="server" BodyPadding="1px" ShowBorder="false" ShowHeader="false" >
                                                                <Items>
                                                                    <f:Label runat="server" ID="Label58" Text="编号" MarginLeft="50px">
                                                                    </f:Label>
                                                                </Items>
                                                            </f:Panel>
                                                            <f:Panel ID="Panel5" Title="Panel1" runat="server"  Width="200px"
                                                                TableRowspan="2" ShowBorder="false" ShowHeader="false" Height="60px">
                                                                <Items>
                                                                    <f:Label runat="server" ID="lblAccidentType11" Text="类别" MarginLeft="50px">
                                                                    </f:Label>
                                                                </Items>
                                                            </f:Panel>
                                                            <f:Panel ID="Panel6" Title="Panel1" runat="server"  ShowBorder="false"
                                                                TableColspan="3"  ShowHeader="false" Height="30px" Width="600px">
                                                                <Items>
                                                                    <f:Label runat="server" ID="Label1" Text="费用（万元）" MarginLeft="200px">
                                                                    </f:Label>
                                                                </Items>
                                                            </f:Panel>                        
                                                            <f:Panel ID="Panel7" Title="Panel1" runat="server"  ShowBorder="false"
                                                                ShowHeader="false" Height="30px" Width="200px">
                                                                <Items>
                                                                    <f:Label runat="server" ID="Label3" Text=" 当期" MarginLeft="90px">
                                                                    </f:Label>
                                                                </Items>
                                                            </f:Panel>
                                                            <f:Panel ID="Panel8" Title="Panel1" runat="server" ShowBorder="false"
                                                                 ShowHeader="false" Height="30px" Width="200px">
                                                                <Items>
                                                                    <f:Label runat="server" ID="Label32" Text=" 费用明细" MarginLeft="90px">
                                                                    </f:Label>
                                                                </Items>
                                                            </f:Panel>
                                                            <f:Panel ID="Panel9" Title="Panel1" runat="server" BodyPadding="1px" ShowBorder="false"
                                                                 ShowHeader="false" Height="30px" Width="180px">
                                                                <Items>
                                                                    <f:Label runat="server" ID="Label4" Text=" 项目累计" MarginLeft="90px">
                                                                    </f:Label>
                                                                </Items>
                                                            </f:Panel>
                                                        </Items>
                                                    </f:Panel>
                                                </Items>
                                            </f:FormRow> 
                                            <f:FormRow>
                                                <Items>
                                                    <f:Label runat="server" ID="Label5" Text="A1" MarginLeft="30">
                                                    </f:Label>
                                                    <f:Label runat="server" ID="Label6" Text="基础管理" >
                                                    </f:Label>
                                                    <f:Label ID="txtA1" runat="server"  Label="" >
                                                    </f:Label>
                                                    <f:Button ID="btnA1" Text="费用明细" ToolTip="费用明细上传及查看" Icon="TableCell" runat="server"
                                                        OnClick="btnUrl_Click" >
                                                    </f:Button> 
                                                    <f:Label ID="txtProjectA1"   runat="server" Label="" >
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow>
                                            <f:FormRow>
                                                <Items>
                                                    <f:Label runat="server" ID="Label2" Text="A2" MarginLeft="30">
                                                    </f:Label>
                                                    <f:Label runat="server" ID="Label7" Text="安全奖励" >
                                                    </f:Label>
                                                    <f:Label ID="txtA2" runat="server"  Label="" >
                                                    </f:Label>
                                                    <f:Button ID="btnA2" Text="费用明细" ToolTip="费用明细上传及查看" Icon="TableCell" runat="server"
                                                        OnClick="btnUrl_Click" >
                                                    </f:Button> 
                                                    <f:Label ID="txtProjectA2"   runat="server" Label="">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow> 
                                            <f:FormRow >
                                                <Items>
                                                     <f:Label runat="server" ID="Label8" Text="A3" MarginLeft="30">
                                                    </f:Label>
                                                    <f:Label runat="server" ID="Label9" Text="安全技术" >
                                                    </f:Label>
                                                    <f:Label ID="txtA3"  runat="server" Label="">
                                                    </f:Label>
                                                    <f:Button ID="btnA3" Text="费用明细" ToolTip="费用明细上传及查看" Icon="TableCell" runat="server"
                                                        OnClick="btnUrl_Click" >
                                                    </f:Button> 
                                                    <f:Label ID="txtProjectA3"  runat="server" Label="">
                                                    </f:Label>
                                                </Items>
                                            </f:FormRow> 
                                            <f:FormRow >
                                            <Items>
                                                    <f:Label runat="server" ID="Label10" Text="A4" MarginLeft="30">
                                                </f:Label>
                                                <f:Label runat="server" ID="Label11" Text="职业健康">
                                                </f:Label>
                                                <f:Label ID="txtA4"  runat="server" Label="">
                                                </f:Label>
                                                <f:Button ID="btnA4" Text="费用明细" ToolTip="费用明细上传及查看" Icon="TableCell" runat="server"
                                                    OnClick="btnUrl_Click" >
                                                </f:Button> 
                                                <f:Label ID="txtProjectA4"  runat="server" Label="">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                            <f:FormRow >
                                            <Items>
                                                    <f:Label runat="server" ID="Label12" Text="A5" MarginLeft="30">
                                                </f:Label>
                                                <f:Label runat="server" ID="Label13" Text="防护措施" >
                                                </f:Label>
                                                <f:Label ID="txtA5"  runat="server" Label="">
                                                </f:Label>
                                                <f:Button ID="btnA5" Text="费用明细" ToolTip="费用明细上传及查看" Icon="TableCell" runat="server"
                                                    OnClick="btnUrl_Click" >
                                                </f:Button> 
                                                <f:Label ID="txtProjectA5"  runat="server" Label="">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                            <f:FormRow >
                                            <Items>
                                                    <f:Label runat="server" ID="Label14" Text="A6" MarginLeft="30">
                                                </f:Label>
                                                <f:Label runat="server" ID="Label15" Text="应急管理" >
                                                </f:Label>
                                                <f:Label ID="txtA6"  runat="server" Label="">
                                                </f:Label>
                                                <f:Button ID="btnA6" Text="费用明细" ToolTip="费用明细上传及查看" Icon="TableCell" runat="server"
                                                    OnClick="btnUrl_Click" >
                                                </f:Button> 
                                                <f:Label ID="txtProjectA6"  runat="server" Label="">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                            <f:FormRow >
                                            <Items>
                                                    <f:Label runat="server" ID="Label16" Text="A7" MarginLeft="30">
                                                </f:Label>
                                                <f:Label runat="server" ID="Label17" Text="化工试车" >
                                                </f:Label>
                                                <f:Label ID="txtA7"  runat="server" Label="">
                                                </f:Label>
                                                <f:Button ID="btnA7" Text="费用明细" ToolTip="费用明细上传及查看" Icon="TableCell" runat="server"
                                                    OnClick="btnUrl_Click" >
                                                </f:Button> 
                                                <f:Label ID="txtProjectA7"  runat="server" Label="">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                            <f:FormRow >
                                            <Items>
                                                    <f:Label runat="server" ID="Label18" Text="A8" MarginLeft="30">
                                                </f:Label>
                                                <f:Label runat="server" ID="Label19" Text="教育培训" >
                                                </f:Label>
                                                <f:Label ID="txtA8"  runat="server" Label="">
                                                </f:Label>
                                                <f:Button ID="btnA8" Text="费用明细" ToolTip="费用明细上传及查看" Icon="TableCell" runat="server"
                                                    OnClick="btnUrl_Click" >
                                                </f:Button> 
                                                <f:Label ID="txtProjectA8"  runat="server" Label="">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>
                                            <f:FormRow >
                                            <Items>
                                                <f:Label runat="server" ID="Label20" Text="∑A" MarginLeft="30">
                                                </f:Label>
                                                <f:Label runat="server" ID="Label21" Text="安全生产费用合计">
                                                </f:Label>
                                                <f:Label ID="txtAAll"   runat="server" Label="">
                                                </f:Label>
                                                <f:Label runat="server" ID="lbtempA"></f:Label>
                                                <f:Label ID="txtProjectAAll"  runat="server" Label="">
                                                </f:Label>
                                            </Items>
                                        </f:FormRow>                                            
                                        </Rows>
                                    </f:Form>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Panel>                      
        </Items>
    </f:Panel>
     <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="1200px"
        Height="500px">
    </f:Window>
    </form>
    <script type="text/javascript">      
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
           // F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }

        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
