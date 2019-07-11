<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSECostUnitManageItem.aspx.cs" Inherits="FineUIPro.Web.CostGoods.HSSECostUnitManageItem" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>分包费用</title>
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
     <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="分包费用" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                  <f:TextArea runat="server" ID="txtCostContent" Label="费用内容" LabelWidth="80px"
                      required="true" ShowRedStar="true" Height="40px" FocusOnPageLoad="true">
                  </f:TextArea>
                    <f:HiddenField runat="server" ID="hdHSSECostUnitManageItemId"></f:HiddenField>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>       
                    <f:NumberBox ID="txtSortIndex" runat="server" Label="序号" Required="true" ShowRedStar="true"
                        NoDecimal="true" NoNegative="true" Width="180" LabelWidth="80px">
                    </f:NumberBox>
                    <f:DatePicker ID="txtReportTime" runat="server" Label="日期" required="true" 
                        ShowRedStar="true" Width="180" LabelWidth="80px">
                    </f:DatePicker>
                    <f:TextBox ID="txtMetric" runat="server" Label="计价单位" Width="180" LabelWidth="80px">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                      <f:NumberBox ID="txtQuantity" runat="server" Label="工程量" DecimalPrecision="4"
                         NoNegative="true" Width="180" LabelWidth="80px" AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged">
                    </f:NumberBox>
                     <f:NumberBox ID="txtPrice" runat="server" Label="单价" DecimalPrecision="4"
                         NoNegative="true" Width="180" LabelWidth="80px" AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged">
                    </f:NumberBox>
                    <f:NumberBox ID="txtTotalPrice" runat="server" Label="总价" DecimalPrecision="4" LabelWidth="80px"
                         NoNegative="true" required="true" ShowRedStar="true" Width="180">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" 
                        EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="HSSECostUnitManageItemId" 
                        DataIDField="HSSECostUnitManageItemId" AllowSorting="true" SortField="SortIndex" 
                       SortDirection="ASC" AllowPaging="true" EnableTextSelection="True"  Height="400px"
                       EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick">   
                         <Toolbars>                                   
                             <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                 <Items>
                                    <f:ToolbarFill runat="server"></f:ToolbarFill>    
                                    <f:Button ID="btnSure" Icon="Accept" runat="server"  ValidateForms="SimpleForm1" 
                                        OnClick="btnSure_Click" ToolTip="确认">
                                    </f:Button> 
                                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                                    </f:Button>
                                 </Items>
                             </f:Toolbar>
                        </Toolbars>
                        <Columns>
                            <f:RenderField Width="50px" ColumnID="SortIndex" DataField="SortIndex"
                                FieldType="String" HeaderText="序号"  HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>                            
                            <f:RenderField Width="90px" ColumnID="ReportTime" DataField="ReportTime"
                                SortField="ReportTime" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd"
                                HeaderText="日期" HeaderTextAlign="Center" TextAlign="Center">
                            </f:RenderField>
                            <f:RenderField Width="200px" ColumnID="CostContent" DataField="CostContent" ExpandUnusedSpace="true"
                                FieldType="String" HeaderText="费用内容"  HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="Quantity" DataField="Quantity" 
                                FieldType="Double" HeaderText="工程量"  HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="Metric" DataField="Metric" 
                                FieldType="String" HeaderText="计量单位"  HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="Price" DataField="Price" 
                                FieldType="Double" HeaderText="单价"  HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                            <f:RenderField Width="80px" ColumnID="TotalPrice" DataField="TotalPrice" 
                                FieldType="Double" HeaderText="总价"  HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                              <f:WindowField TextAlign="Center" Width="100px" WindowID="WindowAtt" HeaderText="附件"
                                Text="发票单据" ToolTip="附件上传查看" DataIFrameUrlFields="HSSECostUnitManageItemId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&path=FileUpload/HSSECostUnitManageItem&menuId=6488F005-95F2-4D49-BC95-6DF4C9B23F3D"
                                Title="附件" /> 
                        </Columns>
                            <Listeners>
                                <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                            </Listeners>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    <f:Window ID="WindowAtt" Title="附件" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnMenuEdit" OnClick="btnMenuEdit_Click" EnablePostBack="true"
        runat="server" Text="修改" Icon="TableEdit">
    </f:MenuButton>
    <f:MenuButton ID="btnMenuDelete" OnClick="btnMenuDelete_Click" EnablePostBack="true"
         ConfirmText="删除选中行？" ConfirmTarget="Parent" runat="server" Text="删除"
        Icon="Delete">
    </f:MenuButton>
</f:Menu>
    </form>
    <script type="text/javascript">      
         var menuID = '<%= Menu1.ClientID %>';
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {
            F(menuID).show();  //showAt(event.pageX, event.pageY);
            return false;
        }
        function reloadGrid() {
            __doPostBack(null, 'reloadGrid');
        }
    </script>
</body>
</html>
