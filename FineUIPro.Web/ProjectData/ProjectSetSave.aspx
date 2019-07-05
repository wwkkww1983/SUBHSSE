<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSetSave.aspx.cs" Inherits="FineUIPro.Web.ProjectData.ProjectSetSave" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <rows>
            <f:FormRow>
                 <Items>
                   <f:TextBox ID="txtProjectName" runat="server" Label="项目名称" Required="true"  MaxLength="100" 
                    ShowRedStar="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"  FocusOnPageLoad="true" ></f:TextBox>
                </Items>
            </f:FormRow>
            <f:FormRow>
               <Items>
                   <f:TextBox ID="txtProjectCode" runat="server" Label="项目号" Required="true" MaxLength="50" 
                    ShowRedStar="true" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"></f:TextBox>
                </Items>
                <Items>
                   <f:TextBox ID="txtShortName" runat="server" Label="简称" MaxLength="200"></f:TextBox>
                </Items>
            </f:FormRow>                     
            <f:FormRow>
                 <Items>               
                   <f:DropDownList ID="drpProjectType" Label="项目类型" runat="server">
                   </f:DropDownList>
                </Items> 
                <Items>
                   <f:TextBox ID="txtProjectState" runat="server" Label="项目状态" Readonly="true"></f:TextBox>
               </Items>                                         
            </f:FormRow>   
            <f:FormRow>
                 <Items>               
                   <f:TextBox ID="txtContractNo" runat="server" Label="合同号"></f:TextBox>
                </Items> 
                <Items>
                   <f:NumberBox runat="server" ID="txtDuration" Label="项目建设工期(月)" NoDecimal="true"
                        NoNegative="true" LabelWidth="120px">
                    </f:NumberBox>
               </Items>                                         
            </f:FormRow>   
             <f:FormRow>
                <Items>
                    <f:DatePicker runat="server" Label="开工日期" ID="txtStartDate" Required="true" ShowRedStar="true" ></f:DatePicker>
                </Items>
               <Items>
                   <f:DatePicker runat="server" Label="竣工日期" ID="txtEndDate"></f:DatePicker>
               </Items>
            </f:FormRow>   
            <f:FormRow>
                <Items>
                   <f:TextBox ID="txtPostCode" runat="server" Label="邮编"></f:TextBox>
               </Items>    
               <Items>
                    <f:DropDownList ID="drpProjectManager" runat="server" Label="项目经理" EnableEdit="true" Required="true" ShowRedStar="true">
                    </f:DropDownList>     
                </Items>
            </f:FormRow>  
            <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpConstructionManager" runat="server" Label="施工经理" EnableEdit="true">
                    </f:DropDownList>     
                </Items>
                <Items>
                    <f:DropDownList ID="drpHSSEManager" runat="server" Label="安全经理" EnableEdit="true">
                    </f:DropDownList>     
                </Items>
            </f:FormRow>  
            <f:FormRow>
                 <Items>
                   <f:TextBox ID="txtProjectAddress" runat="server" Label="项目地址" MaxLength="500" ></f:TextBox>
                </Items>
            </f:FormRow> 
            <f:FormRow>
                 <Items>
                   <f:TextArea ID="txtWorkRange" runat="server" Label="工作范围" MaxLength="500" ></f:TextArea>
                </Items>
            </f:FormRow> 
            <f:FormRow>   
                <Items>
                     <f:CheckBox ID="ckIsUpTotalMonth" runat="server" Label="上报月总结" Checked="true" >
                    </f:CheckBox>
                </Items>
                <Items>
                    <f:Label runat="server"></f:Label>
                </Items>
            </f:FormRow>     
        </rows>
        <toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1" 
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                     <f:HiddenField ID="hdCompileMan" runat="server"></f:HiddenField>
                </Items>
            </f:Toolbar>
        </toolbars>
    </f:Form>
    </form>
</body>
</html>
