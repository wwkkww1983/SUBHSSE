<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CodeTemplateRule.aspx.cs" Inherits="FineUIPro.Web.ProjectData.CodeTemplateRule" ValidateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
     <f:Form ID="SimpleForm2" ShowBorder="true" ShowHeader="false" AutoScroll="true" BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow ColumnWidths="60% 40%">
                <Items>                                               
                    <f:DropDownBox runat="server" ID="drpMenu" Values="henan" EmptyText="请选择菜单"
                        EnableMultiSelect="false" Label="业务菜单" AutoPostBack="true" OnTextChanged="drpMenu_TextChanged">
                        <PopPanel>
                            <f:Tree ID="treeMenu" ShowHeader="false" Hidden="true" runat="server" EnableSingleExpand="true">
                            </f:Tree>
                        </PopPanel>
                    </f:DropDownBox>             
                    <f:Label runat="server" ID="lbTemp"></f:Label>                                                          
                </Items>
             </f:FormRow> 
            <f:FormRow ColumnWidths="10% 10% 20% 15% 20% 20%">
                <Items>
                    <f:Label ID="LabelName" runat="server" Label="施工编码"></f:Label>  
                    <f:CheckBox runat="server" ID="ckProjectCode" Label="加项目号"></f:CheckBox>
                    <f:TextBox runat="server" ID="txtPrefix" Label="前缀" MaxLength="50" EmptyText="请输入前缀"></f:TextBox>
                    <f:CheckBox runat="server" ID="ckUnitCode" Label="加单位代号" LabelWidth="150px"></f:CheckBox>
                    <f:NumberBox runat="server" ID="txtDigit" Label="流水号位数" NoDecimal="true" NoNegative="true"></f:NumberBox>
                    <f:TextBox runat="server" ID="txtSymbol" Label="编码间隔符" MaxLength="50" ShowRedStar="true" Required="true"></f:TextBox>
                </Items>
                </f:FormRow> 
                <f:FormRow ColumnWidths="10% 10% 20% 15% 20% 20%">
                <Items>
                    <f:Label ID="Label1" runat="server" Label="业主编码"></f:Label>  
                    <f:CheckBox runat="server" ID="ckProjectCodeOwer" Label="加项目号"></f:CheckBox> 
                    <f:TextBox runat="server" ID="txtPrefixOwer" Label="前缀" MaxLength="50" EmptyText="请输入前缀"></f:TextBox> 
                    <f:CheckBox runat="server" ID="ckUnitCodeOwer" Label="加单位代号" LabelWidth="150px"></f:CheckBox>
                    <f:NumberBox runat="server" ID="txtDigitOwer" Label="流水号位数" NoDecimal="true" NoNegative="true"></f:NumberBox>
                    <f:TextBox runat="server" ID="txtSymbolOwer" Label="编码间隔符" MaxLength="50" ShowRedStar="true" Required="true"></f:TextBox>
                </Items>
                </f:FormRow>                                         
                <f:FormRow>
                    <Items>
                        <f:HtmlEditor runat="server" Label="详细" ID="txtTemplate" ShowLabel="false"
                        Editor="UMEditor" BasePath="~/res/umeditor/" ToolbarSet="Full" Height="390px">
                    </f:HtmlEditor> 
                </Items>
            </f:FormRow>                                                                                                  
        </Rows>
         <Toolbars>
            <f:Toolbar ID="Toolbar3" Position="Top" ToolbarAlign="Right" runat="server">
                <Items>  
                 <f:Button ID="btnPageWhiteRefresh" Icon="FolderUp" runat="server" ToolTip="按新规则更新编码" ValidateForms="SimpleForm2"
                        OnClick="btnPageWhiteRefresh_Click">
                    </f:Button>                                                                                           
                    <f:Button ID="btnDatabaseRefresh" Icon="PageRefresh" runat="server" ToolTip="按新规则重新排序并生成编码" ValidateForms="SimpleForm2"
                        OnClick="btnDatabaseRefresh_Click">
                    </f:Button>
                     <f:Button ID="btnReset" Icon="DatabaseRefresh" runat="server" ToolTip="重置规则/模板，恢复初始数据"
                        OnClick="btnReset_Click" ConfirmText="确认重置规则/模板，恢复初始数据"></f:Button>
                <f:ToolbarFill runat="server"></f:ToolbarFill>                
                <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm2"
                    OnClick="btnSave_Click">
                </f:Button>                                     
            </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
