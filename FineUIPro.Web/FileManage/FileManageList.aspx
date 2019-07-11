<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileManageList.aspx.cs" Inherits="FineUIPro.Web.FileManage.FileManageList" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style>
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1"  runat="server" />
    <f:Panel ID="Panel1" runat="server" Margin="5px" BodyPadding="5px" ShowBorder="false" 
       ShowHeader="false"  Layout="VBox" BoxConfigAlign="Stretch" >
        <Items>
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="文件管理" 
                EnableCollapse="true" runat="server" BoxFlex="1" 
                DataKeyNames="WED_ID" AllowCellEditing="true"  DataIDField="FileId"
                AllowSorting="true" SortField="FileName" SortDirection="ASC"  OnSort="Grid1_Sort"
                OnRowCommand="Grid1_RowCommand" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                AllowPaging="true" IsDatabasePaging="true"  PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" 

                AllowFilters="true"  OnFilterChange="Grid1_FilterChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                        <Items>
                            <f:Button ID="btnNew" ToolTip="新增" Icon="Add" EnablePostBack="false" runat="server">
                            </f:Button>
                            <f:Button ID="btnFind" ToolTip="查询" Icon="find"  runat="server" 
                                OnClick="btnFind_Click">
                            </f:Button>
                            <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？" OnClick="btnDelete_Click"
                                runat="server">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                    <f:RenderField Width="120px" ColumnID="FileCode" DataField="FileCode" SortField="FileCode" FieldType="String"
                        HeaderText="文件编号">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="FileName" DataField="FileName" SortField="FileName"
                        FieldType="String" HeaderText="文件名称" EnableFilter="true" ExpandUnusedSpace="true">
                        <Filter EnableMultiFilter="true" ShowMatcher="true" >
                            <Operator>
                                <f:DropDownList ID="DropDownList1" runat="server">
                                    <f:ListItem Text="等于" Value="equal" />
                                    <f:ListItem Text="包含" Value="contain" Selected="true" />
                                </f:DropDownList>
                            </Operator>
                        </Filter>
                    </f:RenderField> 
                    <f:RenderField Width="80px" ColumnID="BigType" DataField="BigType" SortField="BigType" FieldType="String"
                        HeaderText="文件类型">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="FileSize" DataField="FileSize" SortField="FileSize" FieldType="String"
                        HeaderText="文件大小">
                    </f:RenderField>
                    <f:RenderField Width="120px" ColumnID="FileCreate" DataField="FileCreate" FieldType="String"
                        HeaderText="创建人">
                    </f:RenderField>
                    <f:LinkButtonField Width="50px" ConfirmText="删除选中行？" ConfirmTarget="Parent" CommandName="Delete"
                        Icon="Delete" />
                </Columns>

              
            <PageItems>
                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                </f:ToolbarSeparator>
                <f:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                </f:ToolbarText>
                <f:DropDownList runat="server" ID="ddlPageSize" Width="80px" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                    <f:ListItem Text="10" Value="10" />
                    <f:ListItem Text="15" Value="15" />
                    <f:ListItem Text="20" Value="20" />
                    <f:ListItem Text="25" Value="25" />
                    <f:ListItem Text="所有行" Value="100000" />
                </f:DropDownList>

            </PageItems>
            </f:Grid>
           
        </Items>
    </f:Panel> 
         <f:Window ID="Window1" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
                Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
                Width="600px" Height="480px">
            </f:Window>

        
    </form>
   
</body>
</html>
