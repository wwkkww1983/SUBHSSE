<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSECostUnitManageRatifiedItem.aspx.cs" Inherits="FineUIPro.Web.CostGoods.HSSECostUnitManageRatifiedItem" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>分包费用审核</title>
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
     <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="分包费用审核" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow runat ="server" ID="tr1" Hidden="true">
                <Items>
                  <f:TextArea runat="server" ID="txtCostContent" Label="费用内容" LabelWidth="80px"
                       Height="30px" FocusOnPageLoad="true" Readonly="true">
                  </f:TextArea>
                    <f:HiddenField runat="server" ID="hdHSSECostUnitManageItemId"></f:HiddenField>
                </Items>
            </f:FormRow>
            <f:FormRow runat ="server" ID="tr2" Hidden="true">
                <Items>
                    <f:NumberBox ID="txtRatifiedQuantity" runat="server" Label="工程量" DecimalPrecision="4"
                        NoDecimal="true" NoNegative="true" Width="180" LabelWidth="80px" AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged">
                    </f:NumberBox>
                     <f:NumberBox ID="txtRatifiedPrice" runat="server" Label="单价" DecimalPrecision="4"
                         NoNegative="true" Width="180" LabelWidth="80px" AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged">
                    </f:NumberBox>
                    <f:NumberBox ID="txtRatifiedTotalPrice" runat="server" Label="总价" DecimalPrecision="4" LabelWidth="80px"
                         NoNegative="true" required="true" ShowRedStar="true" Width="180">
                    </f:NumberBox>
                </Items>
            </f:FormRow>
            <f:FormRow runat ="server" ID="tr3" Hidden="true">
                <Items>
                  <f:TextArea runat="server" ID="txtRatifiedExplain" Label="核定意见" LabelWidth="80px"
                       Height="30px" FocusOnPageLoad="true">
                  </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false"  EnableCollapse="true" EnableColumnLines="true" 
                        EnableColumnMove="true" runat="server" BoxFlex="1" DataKeyNames="HSSECostUnitManageItemId" 
                        DataIDField="HSSECostUnitManageItemId" AllowSorting="true" SortField="SortIndex" 
                        SortDirection="ASC" AllowPaging="true" EnableTextSelection="True" Height="400px">   
                         <Toolbars>                                   
                             <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                 <Items>
                                     <f:Label runat="server" Text="右键核定工程量、单价、总价"></f:Label>
                                    <f:ToolbarFill runat="server"></f:ToolbarFill>    
                                    <f:Button ID="btnSure" Icon="Accept" runat="server"  ValidateForms="SimpleForm1" 
                                        OnClick="btnSure_Click" ToolTip="确认" Hidden="true">
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
                            <f:RenderField Width="200px" ColumnID="CostContent" DataField="CostContent"
                                FieldType="String" HeaderText="费用内容"  HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="60px" ColumnID="Quantity" DataField="Quantity" 
                                FieldType="Double" HeaderText="工程量"  HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                            <f:RenderField Width="75px" ColumnID="Metric" DataField="Metric" 
                                FieldType="String" HeaderText="计量单位"  HeaderTextAlign="Center" TextAlign="Left">
                            </f:RenderField>
                            <f:RenderField Width="60px" ColumnID="Price" DataField="Price" 
                                FieldType="Double" HeaderText="单价"  HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                            <f:RenderField Width="60px" ColumnID="TotalPrice" DataField="TotalPrice" 
                                FieldType="Double" HeaderText="总价"  HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                              <f:RenderField Width="75px" ColumnID="IsAgreeName" DataField="IsAgreeName" 
                                FieldType="String" HeaderText="审核意见"  HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                             <f:RenderField Width="75px" ColumnID="AuditQuantity" DataField="AuditQuantity" 
                                FieldType="Double" HeaderText="工程量"  HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                            <f:RenderField Width="75px" ColumnID="AuditPrice" DataField="AuditPrice" 
                                FieldType="Double" HeaderText="审核单价"  HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                            <f:RenderField Width="75px" ColumnID="AuditTotalPrice" DataField="AuditTotalPrice" 
                                FieldType="Double" HeaderText="审核总价"  HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                          
                              <f:RenderField Width="75px" ColumnID="IsRatifiedName" DataField="IsRatifiedName" 
                                FieldType="String" HeaderText="核定意见"  HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                            <f:RenderField Width="75px" ColumnID="RatifiedQuantity" DataField="RatifiedQuantity" 
                                FieldType="Double" HeaderText="工程量"  HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                            <f:RenderField Width="75px" ColumnID="RatifiedPrice" DataField="RatifiedPrice" 
                                FieldType="Double" HeaderText="核定单价"  HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                            <f:RenderField Width="75px" ColumnID="RatifiedTotalPrice" DataField="RatifiedTotalPrice" 
                                FieldType="Double" HeaderText="核定总价"  HeaderTextAlign="Center" TextAlign="Right">
                            </f:RenderField>
                          
                              <f:WindowField TextAlign="Center" Width="75px" WindowID="WindowAtt" HeaderText="附件"
                                Text="发票单据" ToolTip="附件上传查看" DataIFrameUrlFields="HSSECostUnitManageItemId" DataIFrameUrlFormatString="../AttachFile/webuploader.aspx?toKeyId={0}&type=-1&path=FileUpload/HSSECostUnitManageItem&menuId=6488F005-95F2-4D49-BC95-6DF4C9B23F3D"
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
      <f:MenuButton ID="btnIsAgree" OnClick="btnIsAgree_Click" EnablePostBack="true"
            runat="server" Text="同意" Icon="Accept">
        </f:MenuButton>
         <f:MenuButton ID="btnNoAgree" OnClick="btnNoAgree_Click" EnablePostBack="true"
            runat="server" Text="不同意" Icon="Cross">
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
