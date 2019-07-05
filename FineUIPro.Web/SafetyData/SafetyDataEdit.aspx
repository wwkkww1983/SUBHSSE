<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyDataEdit.aspx.cs" Inherits="FineUIPro.Web.SafetyData.SafetyDataEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageSafetyData1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTitle" runat="server" Label="文件类别/名称" LabelWidth="120px" Required="true" ShowRedStar="true" MaxLength="50" FocusOnPageLoad="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtCode" runat="server" Label="编码" LabelWidth="120px" MaxLength="50"  Required="true" ShowRedStar="true">
                    </f:TextBox>
                </Items>
            </f:FormRow>      
            <f:FormRow >                
                <Items>
                    <f:CheckBox ID="chkIsEndLevel" runat="server" Label="末级" LabelWidth="120px" 
                        LabelAlign="Right" OnCheckedChanged="chkIsEndLevel_CheckedChanged" AutoPostBack="true">
                    </f:CheckBox>
                    <f:CheckBox ID="chkIsCheck" runat="server" Label="考核项" LabelWidth="120px" 
                        LabelAlign="Right" OnCheckedChanged="chkIsEndLevel_CheckedChanged" AutoPostBack="true">
                    </f:CheckBox>
                </Items>
            </f:FormRow> 
            <f:FormRow Hidden="true" runat="server" ID="rowScore">
                <Items>
                     <f:NumberBox ID="txtScore" runat="server" Label="分值" DecimalPrecision="1" LabelAlign="Right" LabelWidth="120px">
                    </f:NumberBox>
                    <f:NumberBox ID="txtDigit" runat="server" Label="单据流水号位数"  LabelAlign="Right" NoDecimal="true" LabelWidth="110px">
                    </f:NumberBox>
                </Items>
            </f:FormRow> 
            <f:FormRow Hidden="true" runat="server" ID="rowMenu">
                <Items>
                    <f:DropDownBox runat="server" ID="drpMenu" EmptyText="请选择菜单" Width="300px" AutoShowClearIcon="true" EnableClearIconClickEvent="true"
                        EnableMultiSelect="false" Label="业务菜单" OnClearIconClick="drpMenu_ClearIconClick" LabelWidth="120px">
                        <PopPanel>
                            <f:Tree ID="treeMenu" ShowHeader="false" Hidden="true" runat="server" EnableSingleExpand="true">
                            </f:Tree>
                        </PopPanel>
                    </f:DropDownBox>
                </Items>
            </f:FormRow>
            <f:FormRow Hidden="true" runat="server" ID="rowCheckType">
                <Items>
                     <f:DropDownList ID="drpCheckType" runat="server" Label="考核类型" 
                         LabelAlign="Right" Required="true"
                        ShowRedStar="true"  LabelWidth="120px" AutoPostBack="true" 
                         OnSelectedIndexChanged="drpCheckType_SelectedIndexChanged">
                    </f:DropDownList>
                    <f:NumberBox ID="txtCheckTypeValue1" runat="server" Label="值1[月]"  LabelAlign="Right" NoDecimal="true"
                         LabelWidth="110px">
                    </f:NumberBox>
                    <f:NumberBox ID="txtCheckTypeValue2" runat="server" Label="值2[日]"  LabelAlign="Right" NoDecimal="true"
                        NoNegative="true" LabelWidth="110px">
                    </f:NumberBox>
                </Items>
            </f:FormRow> 
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemark" runat="server" Label="备注" LabelWidth="120px" MaxLength="500" ></f:TextArea>
                </Items>
            </f:FormRow> 
             <f:FormRow runat="server" ID="FormRow1">
                <Items>
                    <f:DropDownBox runat="server" ID="drpSupMenu" EmptyText="请选择上级节点" Width="300px" AutoShowClearIcon="true" EnableClearIconClickEvent="true"
                        EnableMultiSelect="false" Label="上级节点" OnClearIconClick="drpSupMenu_ClearIconClick" LabelWidth="120px" ShowRedStar="true">
                        <PopPanel>
                            <f:Tree ID="treeSupMenu" ShowHeader="false" Hidden="true" runat="server" EnableSingleExpand="true">
                            </f:Tree>
                        </PopPanel>
                    </f:DropDownBox>
                </Items>
            </f:FormRow>   
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
