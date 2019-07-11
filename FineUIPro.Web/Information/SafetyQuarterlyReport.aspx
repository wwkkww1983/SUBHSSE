<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafetyQuarterlyReport.aspx.cs"
    Inherits="FineUIPro.Web.Information.SafetyQuarterlyReport" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>安全生产数据季报</title>
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
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" />
    <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server" Margin="5px">
        <Regions>           
            <f:Region ID="Region2" ShowHeader="false" Position="Center" Layout="VBox" BoxConfigAlign="Stretch"
                BoxConfigPosition="Left" runat="server" Title="" AutoScroll="True" RegionSplit="True"
                Split="True">
                <Items>
                    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="true" AutoScroll="true" BodyPadding="10px"
                        runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                        <Toolbars>
                            <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                                <Items>
                                    <f:DropDownList ID="drpUnit" AutoPostBack="true" EnableSimulateTree="true" runat="server" Width="350px"
                                        LabelWidth="70px" Label="单位" EnableEdit="true" ForceSelection="false" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpYear" AutoPostBack="true" EnableSimulateTree="true" runat="server" Width="150px"
                                        LabelWidth="50px" Label="年度" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                                    </f:DropDownList>
                                    <f:DropDownList ID="drpQuarter" AutoPostBack="true" EnableSimulateTree="true" runat="server" Width="150px"
                                        LabelWidth="50px" Label="季度" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
                                    </f:DropDownList>                                   
                                    <f:Button ID="BtnBulletLeft" ToolTip="前一季度" Icon="BulletLeft" runat="server" EnablePostBack="true"
                                        OnClick="BtnBulletLeft_Click">
                                    </f:Button>
                                    <f:Button ID="BtnBulletRight" ToolTip="后一季度" Icon="BulletRight" runat="server" EnablePostBack="true"
                                        OnClick="BulletRight_Click">
                                    </f:Button>
                                    <f:ToolbarFill runat="server"></f:ToolbarFill>
                                    <f:Button ID="btnSee" ToolTip="查看审批流程" Icon="Find" runat="server" OnClick="btnSee_Click">
                                    </f:Button>                                  
                                    <f:Button ID="btnNew" ToolTip="新增" Icon="Add" Hidden="true" runat="server" OnClick="btnNew_Click">
                                    </f:Button>
                                    <f:Button ID="btnEdit" ToolTip="编辑" Icon="TableEdit" Hidden="true" runat="server" OnClick="btnEdit_Click">
                                    </f:Button>
                                    <f:Button ID="btnDelete" ToolTip="删除" Icon="Delete" Hidden="true" ConfirmText="确定删除当前数据？"
                                        OnClick="btnDelete_Click" runat="server">
                                    </f:Button>
                                    <f:Button ID="btnAudit1" ToolTip="审核" Icon="ZoomIn" Hidden="true" runat="server" OnClick="btnAudit1_Click">
                                    </f:Button>
                                    <f:Button ID="btnAudit2" ToolTip="审批" Icon="MagnifierZoomIn" Hidden="true" runat="server" OnClick="btnAudit2_Click">
                                    </f:Button>
                                    <f:Button ID="btnUpdata" ToolTip="上报" Icon="PageSave" Hidden="true" runat="server" OnClick="btnUpdata_Click">
                                    </f:Button>
                                    <f:Button ID="btnImport" ToolTip="导入" Icon="ApplicationGo" Hidden="true" runat="server"
                                        OnClick="btnImport_Click">
                                    </f:Button>
                                     <f:Button ID="btnOut" OnClick="btnOut_Click" runat="server" ToolTip="导出" Icon="FolderUp"
                                        EnableAjax="false" DisableControlBeforePostBack="false">
                                    </f:Button>
                                    <f:Button ID="btnPrint" ToolTip="打印" Icon="Printer" Hidden="true" runat="server"
                                        OnClick="btnPrint_Click">
                                    </f:Button>
                                </Items>
                            </f:Toolbar>
                        </Toolbars>
                        <Rows>
                            <f:FormRow >
                                <Items>
                                    <f:Label ID="lblUnitName" runat="server" Label="单位" Hidden="true">
                                    </f:Label>
                                    <f:Label ID="lblYearId" runat="server" Label="年度">
                                    </f:Label>
                                    <f:Label ID="lblQuarters" runat="server" Label="季度">
                                    </f:Label>
                                    <f:Label ID="lblHandleMan" runat="server">
                                    </f:Label>
                                </Items>
                            </f:FormRow>                           
                             <f:FormRow>
                                <Items>
                                    <f:TextArea ID="txtValue" runat="server" Height="330px" Width="1000px"></f:TextArea>                                   
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="fAttach1" runat="server" Hidden="false" ColumnWidths="30% 60% 10%">
                                <Items>
                                    <f:Label ID="Label3" runat="server" Label="安全专职人员(附件)" LabelWidth="180px">
                                    </f:Label>
                                    <f:Label runat="server" ID="lbAttachUrl1" BoxConfigPosition="Left">
                                     </f:Label>
                                     <f:Button ID="btnSee1" Icon="Find" runat="server" OnClick="btnSee1_Click" ToolTip="附件查看" EnableAjax="false" DisableControlBeforePostBack="false">
                                     </f:Button>                                       
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="fAttach2" runat="server" Hidden="false" ColumnWidths="30% 60% 10%">
                                <Items>
                                    <f:Label ID="Label4" runat="server" Label="项目经理人员(附件)" LabelWidth="180px">
                                    </f:Label>
                                    <f:Label runat="server" ID="lbAttachUrl2" BoxConfigPosition="Left">
                                     </f:Label>
                                     <f:Button ID="btnSee2" Icon="Find" runat="server" OnClick="btnSee2_Click" ToolTip="附件查看" EnableAjax="false" DisableControlBeforePostBack="false">
                                     </f:Button>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    <f:Window ID="Window1" Title="编辑安全生产数据季报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window1_Close" IsModal="false"
        CloseAction="HidePostBack" Width="900px" Height="620px">
    </f:Window>
    <f:Window ID="Window2" Title="导入安全生产数据季报" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" OnClose="Window2_Close" IsModal="false"
        CloseAction="HidePostBack" Width="1024px" Height="600px">
    </f:Window>
    <f:Window ID="Window3" IconUrl="~/res/images/16/11.png" runat="server" Hidden="true"
        IsModal="false" Target="Parent" EnableMaximize="true" EnableResize="true" Title="打印安全生产数据季报"
        CloseAction="HidePostBack" EnableIFrame="true" Height="768px" Width="1024px">
    </f:Window>
    <f:Window ID="Window4" Title="查看审核信息" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Self" EnableResize="true" runat="server" IsModal="true" OnClose="Window4_Close"
        Width="1024px" Height="500px">
    </f:Window>   
    </form>
</body>
</html>
