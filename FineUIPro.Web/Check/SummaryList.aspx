<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SummaryList.aspx.cs" Inherits="FineUIPro.Web.Check.SummaryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner
        {
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
            <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" Title="汇总清单" EnableCollapse="true"
                runat="server" BoxFlex="1" DataKeyNames="DetailId" AllowCellEditing="true" ClicksToEdit="2"
                DataIDField="DetailId" AllowSorting="true" SortField="CheckTime" SortDirection="Desc" EnableColumnLines="true"
                OnSort="Grid1_Sort" AllowPaging="true" IsDatabasePaging="true" PageSize="10"
                OnPageIndexChange="Grid1_PageIndexChange" EnableTextSelection="True">
                <Toolbars>
                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Left">
                        <Items>
                            <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="开始日期" ID="txtStartTime"
                                LabelAlign="right" Width="200px" LabelWidth="80px">
                            </f:DatePicker>
                            <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="结束日期" ID="txtEndTime"
                                LabelAlign="right" Width="200px" LabelWidth="80px">
                            </f:DatePicker>
                            <f:DropDownList ID="drpCheckType" runat="server" Label="检查类别" LabelWidth="80px" Width="280px">
                                <f:ListItem Text="日常巡检" Value="1" Selected="true" />
                                <f:ListItem Text="专项检查" Value="2" />
                                <f:ListItem Text="综合检查" Value="3" />
                                <f:ListItem Text="开工前检查" Value="4" />
                                <f:ListItem Text="节假日检查" Value="5" />
                            </f:DropDownList>
                            <f:DropDownList runat="server" ID="drpCheckPerson" Label="检查人" LabelWidth="70px" Width="300px"></f:DropDownList>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server">
                            </f:ToolbarFill>
                            <f:Button ID="BtnAnalyse" ToolTip="查询" Icon="ChartPie" runat="server" OnClick="BtnAnalyse_Click">
                            </f:Button>
                            <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                EnableAjax="false" DisableControlBeforePostBack="false">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
                <Columns>
                    <f:TemplateField ColumnID="tfNumber" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                        TextAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="labNumber" runat="server" Text=' <%# Grid1.PageIndex * Grid1.PageSize + Container.DataItemIndex + 1%>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>
                     <f:RenderField Width="250px" ColumnID="Unqualified" DataField="Unqualified" SortField="Unqualified"
                        FieldType="String" HeaderText="问题描述" EnableFilter="true" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="CheckTime" DataField="CheckTime" SortField="CheckTime"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="检查时间"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="120px" ColumnID="HiddenDangerType" DataField="HiddenDangerType" SortField="HiddenDangerType"
                        FieldType="String" HeaderText="隐患类型" EnableFilter="true" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                   <%-- <f:TemplateField Width="120px" HeaderText="检查类型" HeaderTextAlign="Center" TextAlign="Left"
                        ColumnID="CheckItemType">
                        <ItemTemplate>
                            <asp:Label ID="lbCheckType" runat="server" Text='<%# ConvertCheckItemType(Eval("CheckItem")) %>'
                                ToolTip='<%# ConvertCheckItemType(Eval("CheckItem")) %>'></asp:Label>
                        </ItemTemplate>
                    </f:TemplateField>--%>
                    <f:RenderField Width="90px" ColumnID="HiddenDangerLevel" DataField="HiddenDangerLevel" SortField="HiddenDangerLevel"
                        FieldType="String" HeaderText="隐患级别" EnableFilter="true" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="220px" ColumnID="UnitName" DataField="UnitName" SortField="UnitName"
                        FieldType="String" HeaderText="责任单位" EnableFilter="true" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="PersonName" DataField="PersonName" SortField="PersonName"
                        FieldType="String" HeaderText="责任人" EnableFilter="true" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="100px" ColumnID="LimitedDate" DataField="LimitedDate" SortField="LimitedDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="整改期限"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                   <f:RenderField Width="120px" ColumnID="HandleStepStr" DataField="HandleStepStr" SortField="HandleStepStr"
                        FieldType="String" HeaderText="处置措施" EnableFilter="true" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="80px" ColumnID="IsRectifyName" DataField="IsRectifyName" SortField="IsRectifyName"
                        FieldType="String" HeaderText="复查结果" EnableFilter="true" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="100px" ColumnID="ReCheckDate" DataField="ReCheckDate" SortField="ReCheckDate"
                        FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd" HeaderText="复查日期"
                        HeaderTextAlign="Center" TextAlign="Left">
                    </f:RenderField>
                     <f:RenderField Width="80px" ColumnID="ReCheckPersonName" DataField="ReCheckPersonName" SortField="ReCheckPersonName"
                        FieldType="String" HeaderText="复查人" EnableFilter="true" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>
                    <f:RenderField Width="80px" ColumnID="CompleteStatusName" DataField="CompleteStatusName" SortField="CompleteStatusName"
                        FieldType="String" HeaderText="隐患状态" EnableFilter="true" HeaderTextAlign="Center"
                        TextAlign="Left">
                    </f:RenderField>                   
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
    </form>
</body>
</html>
