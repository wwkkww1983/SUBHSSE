<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="FineUIPro.Web.Exchange.Content" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>内容管理</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>
            <f:Region ID="Region3" ShowBorder="false" ShowHeader="false" RegionPosition="Top"
                EnableCollapse="true" BodyPadding="0 5 0 0" Layout="Fit" runat="server">
                <Items>
                    <f:Form runat="server" Title="话题类型" EnableCollapse="true" Collapsed="true">
                        <Rows>
                            <f:FormRow ColumnWidths="5% 83% 4% 4% 4%">
                                <Items>
                                    <f:Label runat="server">
                                    </f:Label>
                                    <f:RadioButtonList runat="server" ID="rblContentType" ColumnNumber="5" AutoPostBack="true"
                                        OnSelectedIndexChanged="rblContentType_SelectedIndexChanged">
                                    </f:RadioButtonList>
                                    <f:Button ID="btnNewType" Icon="Add" Hidden="true" runat="server" OnClick="btnNewType_Click">
                                    </f:Button>
                                    <f:Button ID="btnEditType" Icon="Pencil" Hidden="true" runat="server" OnClick="btnEditType_Click">
                                    </f:Button>
                                    <f:Button ID="btnDeleteType" Icon="Delete" Hidden="true" ConfirmText="确定删除当前数据？" OnClick="btnDelete_Click"
                                        runat="server">
                                    </f:Button>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Region>
            <f:Region ID="Region1" ShowBorder="false" ShowHeader="true" RegionPosition="Left" Title="交流主题"
                BodyPadding="0 5 0 0" Width="300px" Layout="Fit" runat="server" EnableCollapse="true">
                <Items>
                    <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                        EnableCollapse="true" Title="交流主题" TitleToolTip="交流主题" ShowBorder="true"
                        ShowHeader="false" AutoScroll="true" BodyPadding="5px" Layout="VBox">
                        <Items>                          
                             <f:Grid ID="Grid2" ShowBorder="true" ShowHeader="false" ShowGridHeader="false" runat="server" BoxFlex="1"  AllowCellEditing="true"    ClicksToEdit="2"
                                SortDirection="DESC" SortField="Theme" DataKeyNames="ContentId" DataIDField="ContentId" EnableMultiSelect="false" EnableRowSelectEvent="true"
                                OnRowSelect="Grid2_RowSelect" EnableColumnLines="true" AllowSorting="true"   
                                OnRowDoubleClick="Grid2_RowDoubleClick" EnableRowDoubleClickEvent="true">  
                                 <Toolbars>
                                     <f:Toolbar runat="server" Position="Bottom" ToolbarAlign="Right">
                                        <Items>
                                             <f:Button ID="btnAdd" Icon="Add" runat="server" Hidden="true" OnClick="btnAdd_Click">
                                             </f:Button>
                                        </Items>
                                    </f:Toolbar>
                                </Toolbars>
                                <Columns>
                                    <f:TemplateField Width="80px" HeaderText="发帖人" HeaderTextAlign="Center" TextAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("CompileManName") %>' ToolTip='<%#Bind("CompileManName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                    <f:TemplateField Width="200px" HeaderText="主题" HeaderTextAlign="Center" TextAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Theme") %>' ToolTip='<%#Bind("Theme") %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                </Columns>
                                 <Listeners>
                                    <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu" />
                                </Listeners>
                                <%-- <PageItems>
                                    <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server" >
                                    </f:ToolbarSeparator>                                                                
                                </PageItems>--%>
                            </f:Grid>
                        </Items>
                    </f:Panel>                   
                </Items>
            </f:Region>
            <f:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                BoxConfigAlign="Stretch" BoxConfigPosition="Left" runat="server">
                <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
                        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Items>
                            <f:Panel ID="Panel2" runat="server" Height="110px" Layout="HBox" ShowBorder="True"
                                EnableCollapse="true" BodyPadding="5px" BoxConfigChildMargin="0 5 0 0"
                                ShowHeader="True">
                                <Items>
                                    <f:Panel ID="Panel1" Title="面板1" BoxFlex="1" runat="server" Width="50px" BodyPadding="5px"
                                        ShowBorder="true" ShowHeader="false">
                                        <Items>
                                            <f:Form runat="server" ShowBorder="false" ShowHeader="false">
                                                <Rows>
                                                    <f:FormRow>
                                                        <Items>
                                                            <f:Label runat="server" ID="lbCompileMan">
                                                            </f:Label>
                                                        </Items>
                                                    </f:FormRow>
                                                </Rows>
                                                <Rows>
                                                    <f:FormRow>
                                                        <Items>
                                                            <f:Label runat="server" ID="lbCompileDate">
                                                            </f:Label>
                                                        </Items>
                                                    </f:FormRow>
                                                </Rows>
                                            </f:Form>
                                        </Items>
                                    </f:Panel>
                                    <f:Panel ID="Panel4" Title="面板3" BoxFlex="4" runat="server" BodyPadding="5px" Margin="0"
                                        ShowBorder="true" ShowHeader="false">
                                        <Items>
                                            <f:Form ID="Form2" runat="server" ShowBorder="false" ShowHeader="false">
                                                <Rows>
                                                    <f:FormRow ColumnWidths="95% 5%">
                                                        <Items>
                                                            <f:Label runat="server" ID="lbContents">
                                                            </f:Label>
                                                            <f:Button ID="btnSee" ToolTip="查看" Icon="Find" runat="server" OnClick="btnSee_Click">
                                                            </f:Button>
                                                        </Items>
                                                    </f:FormRow>
                                                </Rows>
                                            </f:Form>
                                        </Items>
                                    </f:Panel>
                                </Items>
                            </f:Panel>
                        </Items>
                    </f:Form>
                    <f:Form runat="server" ShowHeader="false" Layout="VBox">
                        <Rows>
                            <f:FormRow ColumnWidths="94% 6%">
                                <Items>
                                <f:Label runat="server"></f:Label>
                                    <f:Button ID="btnNewReContent" Text="回复" runat="server" Hidden="true" OnClick="btnNewReContent_Click">
                                    </f:Button>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                    <f:Grid ID="Grid1" Width="800px" Title="回复内容" ShowBorder="true" runat="server" BoxFlex="1"
                        ShowHeader="false" ShowGridHeader="false" DataKeyNames="ReContentId" AllowCellEditing="true"
                        ClicksToEdit="2" DataIDField="ReContentId" AllowSorting="true" SortField="CompileDate"
                        SortDirection="DESC" OnSort="Grid1_Sort" AllowPaging="true" EnableRowDoubleClickEvent="true"
                        IsDatabasePaging="true" PageSize="10" OnPageIndexChange="Grid1_PageIndexChange" OnRowDoubleClick="Grid1_RowDoubleClick"
                        AllowFilters="true" OnFilterChange="Grid1_FilterChange" >
                        <Columns>
                           <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center" TextAlign="Center"/>
                            <f:TemplateField Width="130px" HeaderText="回复人及时间">
                                <ItemTemplate>
                                    <asp:Label ID="lbCompileManName" runat="server" Text='<%# Eval("CompileManName")%>'></asp:Label>
                                    <br />
                                    <asp:Label ID="lblCompileDate" runat="server" Text='<%# ConvertCompileDate(Eval("CompileDate")) %>'></asp:Label>
                                </ItemTemplate>
                            </f:TemplateField>
                            <f:RenderField Width="650px" ColumnID="Contents" DataField="Contents" FieldType="String"
                                HeaderTextAlign="Center" TextAlign="Left" HeaderText="回复内容">
                            </f:RenderField>                        
                        </Columns>
                        <Listeners>
                            <f:Listener Event="beforerowcontextmenu" Handler="onRowContextMenu1" />
                        </Listeners>
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
    <f:Window ID="Window1" Title="话题类型" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="true"
        Width="500px" Height="160px">
    </f:Window>
    <f:Window ID="Window2" Title="交流内容" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="true"
        Width="900px" Height="420px">
    </f:Window>
    <f:Window ID="Window3" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" OnClose="Window3_Close" IsModal="true"
        Width="900px" Height="200px">
    </f:Window>
    <f:Menu ID="Menu1" runat="server">
        <f:MenuButton ID="btnNewContent"  OnClick="btnNewContent_Click" 
            EnablePostBack="true" runat="server" Text="增加" Hidden="true" Icon="Add">               
        </f:MenuButton>
        <f:MenuButton ID="btnEditContent" OnClick="btnEditContent_Click" Icon="BulletEdit" Hidden="true"
             EnablePostBack="true" runat="server" Text="编辑">               
        </f:MenuButton>
        <f:MenuButton ID="btnDeleteContent"  Icon="Delete" Hidden="true" ConfirmText="确定删除当前数据？" 
            OnClick="btnDeleteContent_Click" runat="server" Text="删除">           
        </f:MenuButton>
     </f:Menu>
     <f:Menu ID="Menu2" runat="server">
        <f:MenuButton ID="btnEditReContent" OnClick="btnEditReContent_Click" Icon="BulletEdit" Hidden="true"
             EnablePostBack="true" runat="server" Text="编辑">               
        </f:MenuButton>
        <f:MenuButton ID="btnDeleteReContent"  Icon="Delete" Hidden="true" ConfirmText="确定删除当前数据？" 
            OnClick="btnDeleteReContent_Click" runat="server" Text="删除">           
        </f:MenuButton>
     </f:Menu>
    </form>
      <script type="text/javascript">
          var menuID = '<%= Menu1.ClientID %>';
          var menuID2 = '<%= Menu2.ClientID %>';
          // 返回false，来阻止浏览器右键菜单
          function onRowContextMenu(event, rowId) {
              F(menuID).show();  //showAt(event.pageX, event.pageY);
              return false;
          }
          function onRowContextMenu1(event, rowId) {
              F(menuID2).show();  //showAt(event.pageX, event.pageY);
              return false;
          }        
    </script>  
</body>
</html>
