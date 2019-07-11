<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendPerson.aspx.cs" Inherits="FineUIPro.Web.SitePerson.SendPerson" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员项目批量转换</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
        
        .f-grid-row .f-grid-cell-inner {
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:Label runat="server" ID="oldProject" Label="原项目"></f:Label>                     
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="70% 30%">
                <Items>
                     <f:DropDownList ID="drpProject" runat="server" Label="新项目" EnableEdit="true" Required="true" ShowRedStar="true">
                    </f:DropDownList> 
                 </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                   <f:DropDownBox runat="server" ID="drpPerson" Label="现场人员" EmptyText="请从下拉表格中选择" MatchFieldWidth="false" LabelAlign="Right"
                        EnableMultiSelect="true">
                        <PopPanel>
                            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" runat="server" DataIDField="PersonId" DataTextField="PersonName"
                                DataKeyNames="PersonId"  AllowSorting="true" SortField="UnitName,PersonName" SortDirection="ASC" EnableColumnLines="true"
                                Hidden="true" Width="800px" Height="420px" EnableMultiSelect="true" KeepCurrentSelection="true" EnableCheckBoxSelect="True" PageSize="10000">
                                <Toolbars>
                                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server">
                                        <Items>
                                            <f:TextBox runat="server" Label="查询" ID="txtPersonName" EmptyText="输入查询条件" 
                                                AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Width="250px" LabelWidth="80px">
                                            </f:TextBox>                                                                     
                                        </Items>
                                    </f:Toolbar>
                                </Toolbars>
                                <Columns>
                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" 
                                        HeaderTextAlign="Center" TextAlign="Center"/>
                                     <f:RenderField Width="200px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                                        FieldType="String" HeaderText="所属单位" HeaderTextAlign="Center" ExpandUnusedSpace="true"
                                        TextAlign="Left">                        
                                    </f:RenderField>
                                    <f:RenderField Width="150px" ColumnID="PersonName" DataField="PersonName" 
                                        SortField="PersonName" FieldType="String" HeaderText="姓名" HeaderTextAlign="Center"
                                        TextAlign="Left">                       
                                    </f:RenderField>
                                    <f:RenderField Width="150px" ColumnID="CardNo" DataField="CardNo" EnableFilter="true"
                                        SortField="CardNo" FieldType="String" HeaderText="卡号" HeaderTextAlign="Center"
                                        TextAlign="Left">                      
                                    </f:RenderField>
                                    <f:RenderField Width="160px" ColumnID="IdentityCard" DataField="IdentityCard" SortField="IdentityCard" FieldType="String"
                                        HeaderText="身份证号码" HeaderTextAlign="Center" TextAlign="Left">
                                    </f:RenderField>
                                </Columns>
                            </f:Grid>
                        </PopPanel>
                    </f:DropDownBox>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Label runat="server" ID="lbTemp"></f:Label>
                    <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
    <script>       
        // 返回false，来阻止浏览器右键菜单
        function onRowContextMenu(event, rowId) {           
            return false;
        }
    </script>
</body>
</html>
