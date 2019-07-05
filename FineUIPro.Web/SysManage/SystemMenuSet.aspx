<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemMenuSet.aspx.cs" Inherits="FineUIPro.Web.SysManage.SystemMenuSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>功能菜单设置</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">   
    <f:PageManager ID="PageManager1" runat="server" />         
      <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%" MarginRight="5px">
            <Items>
               <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="540px" ShowBorder="true"
                TabPosition="Top" MarginBottom="5px" EnableTabCloseMenu="false" runat="server">              
                   <Tabs>                   
                        <f:Tab ID="Tab1" Title="菜单模式" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                            <Items>
                                 <f:RadioButtonList ID="rbMenuModel" Label="菜单模式" runat="server" AutoColumnWidth="true"
                                    OnSelectedIndexChanged="rbMenuModel_CheckedChanged" AutoPostBack="true">  
                                    <f:RadioItem Text="A模式" Value="A" Selected="true"/>
                                    <f:RadioItem Text="B模式" Value="B" />
                                </f:RadioButtonList>                                
                            </Items>
                            <Items>
                                <f:Image ID="ImageMenu" ImageUrl="~/Images/MenuProjectA.png" runat="server" Height="350px" Width="900px"></f:Image>
                            </Items>
                            <Toolbars>
                                <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                                   <Items>
                                    <f:Button ID="btnTab1Save" Icon="TableRefresh" runat="server" ToolTip="更新菜单" ValidateForms="SimpleForm1"
                                        OnClick="btnTab1Save_Click">
                                    </f:Button>                                     
                                </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>
                        <f:Tab ID="Tab2" Title="单位菜单选择" BodyPadding="5px" Layout="VBox" IconFont="Bookmark" runat="server">
                            <Items>   
                                 <f:CheckBoxList ID="ckMenuType"  Label="菜单类型" runat="server" AutoColumnWidth="true"
                                   LabelAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="ckMenuType_OnSelectedIndexChanged">
                                    <Items>
                                        <f:CheckItem Text="本部管理" Value="Menu_Server" />
                                        <f:CheckItem Text="项目现场" Value="Menu_Project"  Selected="true"/>
                                        <f:CheckItem Text="公共资源" Value="Menu_Resource" />
                                        <f:CheckItem Text="基础信息" Value="Menu_BaseInfo" />
                                        <f:CheckItem Text="系统设置" Value="Menu_SystemSet" />
                                    </Items>
                                    <Listeners>
                                        <f:Listener Event="change" Handler="onCheckBoxListChange" />
                                    </Listeners>
                                </f:CheckBoxList>                                                             
                            </Items>
                            <Items>
                                  <f:Tree ID="tvMenu" EnableCollapse="true" ShowHeader="false" Title="系统菜单" Height="420px" ShowBorder="false"
                                        AutoLeafIdentification="true" runat="server" EnableIcons ="true" AutoScroll="true"
                                         EnableSingleClickExpand ="true" OnNodeCheck="tvMenu_NodeCheck" >
                                 </f:Tree>
                            </Items>
                             <Toolbars>
                                <f:Toolbar ID="Toolbar2" Position="Bottom" ToolbarAlign="Right" runat="server">
                                   <Items>
                                    <f:Button ID="btnTab2Save" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                                        OnClick="btnTab2Save_Click">
                                    </f:Button>                                     
                                </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Tab>                                                
                    </Tabs>
                </f:TabStrip>
              </Items>
        </f:Panel>
    </form>
    <script type="text/javascript">
        // 同时只能选中一项
        function onCheckBoxListChange(event, checkbox, isChecked) {
            var me = this;
            // 当前操作是：选中
            if (isChecked) {
                // 仅选中这一项
                me.setValue(checkbox.getInputValue());
            }
            // __doPostBack('', 'CheckBoxList1Change');
        }
    </script>
</body>
</html>
