<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerTotalMonthServerView2.aspx.cs" ValidateRequest="false" Inherits="FineUIPro.Web.Manager.ManagerTotalMonthServerView2" %>
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
                AllowCellEditing="true" ClicksToEdit="2" DataIDField="ID" AllowSorting="true"
                SortField="SortIndex" SortDirection="ASC" OnSort="Grid1_Sort" AllowPaging="false" EnableTextSelection="True">                
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
                    <f:RenderField Width="120px" ColumnID="ExistenceHiddenDanger" DataField="ExistenceHiddenDanger"
                        ExpandUnusedSpace="true" SortField="ExistenceHiddenDanger" FieldType="String" HeaderText="存在隐患"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="CorrectiveActions" DataField="CorrectiveActions"
                        ExpandUnusedSpace="true" SortField="CorrectiveActions" FieldType="String" HeaderText="整改措施"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="PlanCompletedDate" DataField="PlanCompletedDate"
                        ExpandUnusedSpace="true" SortField="PlanCompletedDate" FieldType="String" HeaderText="计划完成时间"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="ResponsiMan" DataField="ResponsiMan"
                        ExpandUnusedSpace="true" SortField="ResponsiMan" FieldType="String" HeaderText="责任人"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="110px" ColumnID="ActualCompledDate" DataField="ActualCompledDate"
                        ExpandUnusedSpace="true" SortField="ActualCompledDate" FieldType="String" HeaderText="实际完成时间"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="Remark" DataField="Remark"
                        ExpandUnusedSpace="true" SortField="Remark" FieldType="String" HeaderText="备注"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                </Columns>
                <Listeners>
                    <f:Listener Event="dataload" Handler="onGridDataLoad" />
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

         function onGridDataLoad(event) {
             this.mergeColumns(['ProjectName']);
         }
    </script>
</body>
</html>
