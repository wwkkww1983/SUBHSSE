<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportAuditSee.aspx.cs" Inherits="FineUIPro.Web.Information.ReportAuditSee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看审核信息</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     <style>
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false"
        ShowHeader="false" Layout="VBox" BoxConfigAlign="Stretch">
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="审核信息" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="FlowOperateId" 
                EnableColumnLines="true" DataIDField="FlowOperateId" AllowSorting="true"
                SortField="SortIndex" SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true"
                IsDatabasePaging="true" PageSize="100" 
                 AllowFilters="true"
                OnFilterChange="Grid1_FilterChange">
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="90px" ColumnID="OperaterName" DataField="OperaterName" SortField="OperaterName"
                        FieldType="String" HeaderText="办理人" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="90px" ColumnID="OperaterTime" DataField="OperaterTime" SortField="OperaterTime"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="办理时间"
                        HeaderTextAlign="Center" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="StateName" DataField="StateName" FieldType="String"
                        HeaderText="操作步骤" TextAlign="Center">
                    </f:RenderField>
                    <f:RenderField Width="670px" ColumnID="Opinion" DataField="Opinion" FieldType="String"
                        HeaderText="意见" TextAlign="Center">
                    </f:RenderField>
                </Columns>
            </f:Grid>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
