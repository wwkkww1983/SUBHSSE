<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSEStandardListUpload.aspx.cs"
    Inherits="FineUIPro.Web.Law.HSSEStandardListUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>安全标准规范资源上传</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .black
        {
            color: Black;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
    <f:Panel ID="Panel1" runat="server" ShowBorder="false" ShowHeader="false" Layout="Region">
        <Items>
            <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true"
                EnableCollapse="true" Width="200" Title="上传列表" TitleToolTip="上传列表" ShowBorder="true"
                ShowHeader="false" AutoScroll="true" BodyPadding="0 2 0 0" Layout="Fit" IconFont="ArrowCircleLeft">
                <Items>
                    <f:Tree ID="tvUploadResources" Width="220px" EnableCollapse="true" ShowHeader="false"
                        Title="上传列表" OnNodeCommand="tvUploadResources_NodeCommand" AutoLeafIdentification="true"
                        runat="server" EnableIcons="true" AutoScroll="true" EnableSingleClickExpand="true">
                    </f:Tree>
                </Items>
            </f:Panel>
            <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                ShowHeader="false" BodyPadding="5px" IconFont="PlusCircle" Title="资源明细" TitleToolTip="资源明细"
                AutoScroll="true">
                <Items>
                    <f:Form BodyPadding="5px" ID="SimpleForm1" Layout="VBox" EnableCollapse="false" runat="server"
                        Title="资源明细" IconFont="Anchor" ShowHeader="false">
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtStandardNo" runat="server" Label="标准号" Required="true" ShowRedStar="true"
                                        FocusOnPageLoad="true" MaxLength="25">
                                    </f:TextBox>
                                    <f:TextBox ID="txtStandardName" runat="server" Label="标准名称" Required="true" ShowRedStar="true" MaxLength="100" AutoPostBack="true" OnTextChanged="TextBox_TextChanged">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtStandardGrade" runat="server" Label="标准级别" MaxLength="12">
                                    </f:TextBox>
                                    <f:DropDownList ID="drpType" Label="分类" AutoPostBack="false" EnableSimulateTree="true"
                                        runat="server">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <Items>
                                    <f:TextBox ID="txtCompileMan" runat="server" Label="整理人" Readonly="true" MaxLength="50">
                                    </f:TextBox>
                                    <f:DatePicker runat="server" Label="整理时间" ID="txtCompileDate">
                                    </f:DatePicker>
                                </Items>
                            </f:FormRow>
                        </Rows>
                        <Rows>
                            <f:FormRow>
                                <Items>
                                    <f:Label ID="Label26" runat="server" Text="对应HSSE索引" CssClass="labcenter">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                        <Rows>
                            <f:FormRow>
                               <%-- <Items>
                                    <f:Label ID="Label1" runat="server" Width="100px">
                                    </f:Label>
                                </Items>--%>
                                <Items>
                                    <f:Label ID="Label2" runat="server" Text="01地基处理" ToolTip="地基处理">
                                    </f:Label>
                                    <f:Label ID="Label3" runat="server" Text="02打桩" ToolTip="打桩">
                                    </f:Label>
                                    <f:Label ID="Label4" runat="server" Text="03基坑支护与降水工程" ToolTip="基坑支护与降水工程">
                                    </f:Label>
                                    <f:Label ID="Label5" runat="server" Text="04土方开挖工程" ToolTip="土方开挖工程">
                                    </f:Label>
                                    <f:Label ID="Label6" runat="server" Text="05模板工程" ToolTip="模板工程">
                                    </f:Label>
                                    <f:Label ID="Label7" runat="server" Text="06基础施工" ToolTip="基础施工">
                                    </f:Label>
                                    <f:Label ID="Label8" runat="server" Text="07钢筋混凝土结构" ToolTip="钢筋混凝土结构">
                                    </f:Label>
                                    <f:Label ID="Label9" runat="server" Text="08地下管道" ToolTip="地下管道">
                                    </f:Label>
                                    <f:Label ID="Label10" runat="server" Text="09钢结构" ToolTip="钢结构">
                                    </f:Label>
                                    <f:Label ID="Label11" runat="server" Text="10设备安装" ToolTip="设备安装">
                                    </f:Label>
                                    <f:Label ID="Label12" runat="server" Text="11大型起重吊装工程" ToolTip="大型起重吊装工程">
                                    </f:Label>
                                    <f:Label ID="Label13" runat="server" Text="12脚手架工程" ToolTip="脚手架工程">
                                    </f:Label>
                                    <f:Label ID="Label14" runat="server" Text="13机械安装" ToolTip="机械安装">
                                    </f:Label>
                                    <f:Label ID="Label15" runat="server" Text="14管道安装" ToolTip="管道安装">
                                    </f:Label>
                                    <f:Label ID="Label16" runat="server" Text="15电气仪表安装" ToolTip="电气仪表安装">
                                    </f:Label>
                                    <f:Label ID="Label17" runat="server" Text="16防腐保温防火" ToolTip="防腐保温防火">
                                    </f:Label>
                                    <f:Label ID="Label18" runat="server" Text="17拆除" ToolTip="拆除">
                                    </f:Label>
                                    <f:Label ID="Label19" runat="server" Text="18爆破工程" ToolTip="爆破工程">
                                    </f:Label>
                                    <f:Label ID="Label20" runat="server" Text="19试压" ToolTip="试压">
                                    </f:Label>
                                    <f:Label ID="Label21" runat="server" Text="20吹扫" ToolTip="吹扫">
                                    </f:Label>
                                    <f:Label ID="Label22" runat="server" Text="21试车" ToolTip="试车">
                                    </f:Label>
                                    <f:Label ID="Label23" runat="server" Text="22无损检测" ToolTip="无损检测">
                                    </f:Label>
                                    <f:Label ID="Label24" runat="server" Text="23其他危险性较大的工程" ToolTip="其他危险性较大的工程">
                                    </f:Label>
                                    <f:Label ID="Label27" runat="server" Text="24设计" ToolTip="设计">
                                    </f:Label>
                                    <f:Label ID="Label28" runat="server" Text="25工厂运行" ToolTip="工厂运行">
                                    </f:Label>
                                    <f:Label ID="Label25" runat="server" Text="90全部标准" ToolTip="全部标准">
                                    </f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow>
                                <%--<Items>
                                    <f:Label ID="HiddenField1" runat="server" Width="100px">
                                    </f:Label>
                                </Items>--%>
                                <Items>
                                    <f:CheckBox ID="ckb01" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb02" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb03" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb04" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb05" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb06" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb07" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb08" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb09" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb10" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb11" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb12" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb13" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb14" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb15" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb16" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb17" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb18" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb19" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb20" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb21" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb22" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb23" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb24" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb25" runat="server">
                                    </f:CheckBox>
                                    <f:CheckBox ID="ckb90" runat="server">
                                    </f:CheckBox>
                                </Items>
                            </f:FormRow>
                        </Rows>
                        <Rows>
                            <f:FormRow>
                                <Items>
                                <f:Label runat="server" ID="lbAttachUrl" BoxConfigPosition="Left" MarginLeft="120">
                                    </f:Label>
                                    <f:Button ID="btnUploadResources" Text="上传附件" Icon="SystemNew" runat="server" 
                                        OnClick="btnUploadResources_Click" ValidateForms="SimpleForm1">
                                    </f:Button>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                        <Items>
                            <f:Button ID="btnNew"  Icon="Add" ToolTip="新增" runat="server" OnClick="btnNew_Click"
                                Hidden="true">
                            </f:Button>
                            <f:Button ID="btnDelete"  ToolTip="删除" Icon="Delete" ConfirmText="确定删除当前数据？"
                                OnClick="btnDelete_Click" runat="server" Hidden="true">
                            </f:Button>
                            <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                                OnClick="btnSave_Click" Hidden="true">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
     <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true"
            EnableMaximize="true" Target="Parent" EnableResize="true" runat="server"
            IsModal="true" Width="700px" Height="500px">
       </f:Window>
</body>
</html>
