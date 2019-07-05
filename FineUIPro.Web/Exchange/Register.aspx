<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="FineUIPro.Web.Exchange.Register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>注册管理</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>           
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                <Items>
                    <f:Grid ID="Grid1" Title="用户" ShowBorder="true" EnableColumnLines="true"
                        runat="server" BoxFlex="1" DataKeyNames="UserId" AllowCellEditing="true" ClicksToEdit="2"
                        DataIDField="UserId" AllowSorting="true" SortField="UserCode" SortDirection="ASC"
                        OnSort="Grid1_Sort" OnRowCommand="Grid1_RowCommand" AllowPaging="true" IsDatabasePaging="true"
                        PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" 
                         AllowFilters="true" OnFilterChange="Grid1_FilterChange">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                             <f:RenderField Width="100px" ColumnID="UserCode" DataField="UserCode" SortField="UserCode"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="用户编码">
                            </f:RenderField>
                            <f:RenderField Width="100px" ColumnID="UserName" DataField="UserName" SortField="UserName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Center" HeaderText="姓名">
                            </f:RenderField>
                            <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="单位" ExpandUnusedSpace="true">
                            </f:RenderField>
                             <f:RenderField Width="120px" ColumnID="RoleTypeName" DataField="RoleTypeName" SortField="RoleTypeName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="角色类型">
                            </f:RenderField>
                             <f:RenderField Width="150px" ColumnID="RoleName" DataField="RoleName" SortField="RoleName"
                                FieldType="String" HeaderTextAlign="Center" TextAlign="Left" HeaderText="角色">
                            </f:RenderField>
                            <f:CheckBoxField ColumnID="ckbIsPosts" Width="90px" RenderAsStaticField="false" DataField="IsPosts"
                                AutoPostBack="true" CommandName="IsPosts" HeaderText="发帖" />
                            <f:CheckBoxField ColumnID="ckbIsReplies" Width="90px" RenderAsStaticField="false" DataField="IsReplies"
                                AutoPostBack="true" CommandName="IsReplies" HeaderText="回帖" />
                            <f:CheckBoxField ColumnID="ckbIsDeletePosts" Width="90px" RenderAsStaticField="false" DataField="IsDeletePosts"
                                AutoPostBack="true" CommandName="IsDeletePosts" HeaderText="删帖" />
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
            </f:Region>
        </Regions>
    </f:RegionPanel>
    </form>
</body>
</html>
