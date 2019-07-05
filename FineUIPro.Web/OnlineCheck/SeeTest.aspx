<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeeTest.aspx.cs" Inherits="FineUIPro.Web.OnlineCheck.SeeTest" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Top" runat="server">
                <Items>
                    <f:DropDownList runat="server" ID="ddlWorkPost" EnableSimulateTree="true" Width="250px"
                        Label="岗位" ShowLabel="false">
                    </f:DropDownList>
                    <f:DropDownList runat="server" ID="ddlABVolume" EnableSimulateTree="true" Width="200px"
                        Label="AB卷" ShowLabel="false">
                        <f:ListItem Text="A" Value="A" />
                        <f:ListItem Text="B" Value="B" />
                    </f:DropDownList>
                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                    <f:Button ID="btnSee" ToolTip="查看试卷" Icon="find" runat="server" OnClick="btnSee_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:GroupPanel ID="GroupPanel1"  CssClass="marginr"  Layout="Region" Title="试题内容" runat="server"
                Height="300px">
                <Items>
                    <f:Image ID="imgTestContent" MarginLeft="5px" ShowEmptyLabel="true" runat="server" />
                </Items>
            </f:GroupPanel>
            <f:Grid runat="server" AllowCellEditing="True" ClicksToEdit="1" DataIDField="TestId"
                EnableColumnLines="True" EnableRowLines="True" DataKeyNames="TestId,TestPath" EnableCollapse="True"
                ShowHeader="False" Height="220px" AutoScroll="True"  ID="Grid1" 
                EnableMultiSelect="false" EnableRowSelectEvent="true" OnRowSelect="Grid1_RowSelect"  EnableTextSelection="True">
                <Columns>
                    <f:RenderField runat="server" EnableColumnEdit="False" FieldType="String" DataField="TestCode"
                        ColumnID="TestCode" HeaderText="试卷题号" Width="100px">
                    </f:RenderField>
                    <f:RenderField runat="server" EnableColumnEdit="False" FieldType="String" DataField="TestNo"
                        ColumnID="TestNo" HeaderText="试题库题号" Width="100px">
                    </f:RenderField>
                    <f:RenderField runat="server" EnableColumnEdit="False" FieldType="String" DataField="TestType"
                        ColumnID="TestType" HeaderText="试题类型" Width="120px">
                    </f:RenderField>
                    <f:RenderField runat="server" EnableColumnEdit="False" DataField="ItemType" ColumnID="ItemType"
                        HeaderText="题型" Width="80px">
                    </f:RenderField>
                    <f:RenderField runat="server" EnableColumnEdit="False" FieldType="String" DataField="TestScore"
                        ColumnID="TestScore" HeaderText="分数/题" Width="120px">
                    </f:RenderField>
                    <f:RenderField runat="server" EnableColumnEdit="False" DataField="TestKey" ColumnID="TestKey"
                        HeaderText="答案" Width="80px">
                    </f:RenderField>
                </Columns>
            </f:Grid>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
