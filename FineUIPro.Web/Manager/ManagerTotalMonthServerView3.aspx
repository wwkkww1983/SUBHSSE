<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerTotalMonthServerView3.aspx.cs" ValidateRequest="false" Inherits="FineUIPro.Web.Manager.ManagerTotalMonthServerView3" %>
<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>月总结</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
            white-space: normal;
            word-break: break-all;
        }
         .f-grid-row.AliceBlue
        {
            background-color: #ADD8E6;
            font-siz:larger;
            font-weight:bolder;
            text-align:center;
        } 
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AjaxAspnetControls="divSafetyQuarterly" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="Fit" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="月总结" EnableCollapse="true"
                runat="server" BoxFlex="1" EnableColumnLines="true" DataKeyNames="ID"
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="ID" AllowSorting="false"
                SortField="SortIndex" SortDirection="ASC"  AllowPaging="false" EnableTextSelection="True">                
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>                           
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>           
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>                    
                    <f:RenderField Width="200px" ColumnID="ProjectName" DataField="ProjectName" ExpandUnusedSpace="true"
                        SortField="ProjectName" FieldType="String" HeaderText="项目名称" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="ThisUnitPersonNum" DataField="ThisUnitPersonNum"
                        SortField="ThisUnitPersonNum" FieldType="String" HeaderText="公司现场人数"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="ThisUnitHSEPersonNum" DataField="ThisUnitHSEPersonNum"
                        SortField="ThisUnitHSEPersonNum" FieldType="String" HeaderText="公司现场</br>HSE管理人数"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="SubUnitPersonNum" DataField="SubUnitPersonNum"
                        SortField="SubUnitPersonNum" FieldType="String" HeaderText="分包现场人数"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="130px" ColumnID="SubUnitHSEPersonNum" DataField="SubUnitHSEPersonNum"
                        SortField="SubUnitHSEPersonNum" FieldType="String" HeaderText="分包</br>HSE管理人数"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="90px" ColumnID="ManHours" DataField="ManHours"
                        SortField="ManHours" FieldType="String" HeaderText="人工时数"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="HSEManHours" DataField="HSEManHours"
                        SortField="HSEManHours" FieldType="String" HeaderText="安全生产</br>人工时数"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                </Columns>
                <Listeners>                    
                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                </Listeners>               
            </f:Grid>
        </Items>
    </f:Panel>
    </form>
     <script type="text/javascript">        
         // 返回false，来阻止浏览器右键菜单
         function onRowContextMenu(event, rowId) {
             //F(menuID).show();  //showAt(event.pageX, event.pageY);
             return false;
         }

         function reloadGrid() {
             __doPostBack(null, 'reloadGrid');
         }
    </script>
</body>
</html>
