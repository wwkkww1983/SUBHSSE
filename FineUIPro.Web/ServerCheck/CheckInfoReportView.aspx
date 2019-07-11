<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="CheckInfoReportView.aspx.cs" Inherits="FineUIPro.Web.ServerCheck.CheckInfoReportView" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <title></title>
     <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
        Layout="VBox" ShowHeader="false" BodyPadding="2px" IconFont="PlusCircle" Title="监督检查报告"
        TitleToolTip="监督检查报告" AutoScroll="true">          
        <Items>
            <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" EnableCollapse="true" Collapsed="false"
                BodyPadding="2px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Height="500px">
                <Rows>
                    <f:FormRow>
                        <Items>
                            <f:Label ID="lbName" Text="一、检查目的" CssClass="title" runat="server"></f:Label>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>                                                                        
                                <f:TextArea ID="txtValues1" runat="server" FocusOnPageLoad="true" Height ="80px" Readonly="true"></f:TextArea>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:Label ID="Label1" Text="二、依据" CssClass="title" runat="server"></f:Label>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>                                                                        
                                <f:TextArea ID="txtValues2" runat="server" Height ="64px" Readonly="true"></f:TextArea>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:Label ID="Label2" Text="三、受检单位（项目）安全管理基本情况" CssClass="title" runat="server"></f:Label>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>                                                                        
                                <f:TextArea ID="txtValues3" runat="server" Height ="80px" Readonly="true"></f:TextArea>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:Label ID="Label3" Text="四、符合项" CssClass="title" runat="server"></f:Label>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>                                                                        
                                <f:TextArea ID="txtValues4" runat="server" Height ="100px" Readonly="true"></f:TextArea>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:Label ID="Label4" Text="五、不符合项" CssClass="title" runat="server"></f:Label>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>                                                                        
                                <f:TextArea ID="txtValues5" runat="server" Height ="110px" Readonly="true"></f:TextArea>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:Label ID="Label5" Text="六、改进意见" CssClass="title" runat="server"></f:Label>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>                                                                        
                                <f:TextArea ID="txtValues6" runat="server" Height ="120px" Readonly="true"></f:TextArea>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:Label ID="Label6" Text="七、检查结论" CssClass="title" runat="server"></f:Label>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>                                                                        
                                <f:TextArea ID="txtValues7" runat="server" Height ="40px" Readonly="true"></f:TextArea>
                        </Items>
                    </f:FormRow>
                    <f:FormRow>
                        <Items>
                            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="监督检查报告"  EnableCollapse="false" runat="server"
                                BoxFlex="1" DataKeyNames="ID"  EnableColumnLines="true" SortDirection="ASC" DataIDField="ID" AllowSorting="false" EnableTextSelection="True">
                                <Toolbars>     
                                    <f:Toolbar ID="Toolbar5" runat="server" ToolbarAlign="Right">
                                        <Items>
                                            <f:Label runat="server" Text="八、检查工作组人员" CssClass="title"></f:Label>
                                            <f:ToolbarFill runat="server"></f:ToolbarFill>
                                        </Items>
                                    </f:Toolbar>
                                </Toolbars>
                                <Columns>         
                                    <f:RenderField HeaderText="序号" ColumnID="SortIndex" DataField="SortIndex"  
                                        HeaderTextAlign="Center"  TextAlign="Left" Width="60px">                              
                                    </f:RenderField>                                                
                                        <f:RenderField HeaderText="姓名" ColumnID="Name" DataField="Name" 
                                        HeaderTextAlign="Center"  TextAlign="Left" Width="80px">
                                    </f:RenderField>
                                    <f:RenderField HeaderText="性别" ColumnID="Sex" DataField="Sex" 
                                        HeaderTextAlign="Center"  TextAlign="Left" Width="70px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="所在单位" ColumnID="UnitName" DataField="UnitName" 
                                        HeaderTextAlign="Center"  TextAlign="Left" Width="180px" ExpandUnusedSpace="true">
                                        </f:RenderField>                 
                                        <f:RenderField HeaderText="所在单位职务" ColumnID="PostName" DataField="PostName" SortField="PostName"
                                        HeaderTextAlign="Center"  TextAlign="Left" Width="110px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="职称" ColumnID="WorkTitle" DataField="WorkTitle" SortField="WorkTitle"
                                        HeaderTextAlign="Center"  TextAlign="Left" Width="90px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="检查工作组职务" ColumnID="CheckPostName" DataField="CheckPostName" SortField="CheckPostName"
                                        HeaderTextAlign="Center"  TextAlign="Left" Width="120px">
                                        </f:RenderField>
                                        <f:RenderField HeaderText="检查日期" ColumnID="CheckDate" DataField="CheckDate" SortField="CheckDate"
                                        HeaderTextAlign="Center" TextAlign="Left" Width="110px" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd">                              
                                        </f:RenderField>
                                </Columns>
                            </f:Grid>
                        </Items>
                    </f:FormRow>
                    </Rows>
            </f:Form>
        </Items>  
    </f:Panel>   
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
