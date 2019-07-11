<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpCheckReportEdit.aspx.cs" Inherits="FineUIPro.Web.ServerCheck.UpCheckReportEdit" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业监督检查报告上报</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">   
    <f:PageManager ID="PageManager1" runat="server" />         
      <f:Panel ID="Panel2" runat="server" ShowHeader="false" ShowBorder="false" ColumnWidth="100%" MarginRight="5px">
            <Items>
               <f:TabStrip ID="TabStrip1" CssClass="f-tabstrip-theme-simple" Height="500px" ShowBorder="true"
                TabPosition="Top" MarginBottom="1px" EnableTabCloseMenu="false" runat="server" Width="1160px">              
                   <Tabs>                   
                        <f:Tab ID="Tab1" Title="监督检查报告" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server">
                            <Items>
                                <f:Panel runat="server" ID="panelCenterRegion" RegionPosition="Center" ShowBorder="true"
                                    Layout="VBox" ShowHeader="false" BodyPadding="2px" IconFont="PlusCircle" Title="监督检查报告"
                                    TitleToolTip="监督检查报告" AutoScroll="true">                                     
                                    <Items>
                                         <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="false" AutoScroll="true" EnableCollapse="true" Collapsed="false"
                                                BodyPadding="2px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Height="260px">
                                            <Rows>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:Label ID="lbName" Text="一、检查目的" CssClass="title" runat="server"></f:Label>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>                                                                        
                                                         <f:TextArea ID="txtValues1" runat="server" FocusOnPageLoad="true" Height ="64px"></f:TextArea>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:Label ID="Label1" Text="二、依据" CssClass="title" runat="server"></f:Label>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>                                                                        
                                                         <f:TextArea ID="txtValues2" runat="server" Height ="48px"></f:TextArea>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:Label ID="Label2" Text="三、受检单位（项目）安全管理基本情况" CssClass="title" runat="server"></f:Label>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>                                                                        
                                                         <f:TextArea ID="txtValues3" runat="server" Height ="72px"></f:TextArea>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:Label ID="Label3" Text="四、符合项" CssClass="title" runat="server"></f:Label>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>                                                                        
                                                         <f:TextArea ID="txtValues4" runat="server" Height ="64px"></f:TextArea>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:Label ID="Label4" Text="五、不符合项" CssClass="title" runat="server"></f:Label>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>                                                                        
                                                         <f:TextArea ID="txtValues5" runat="server" Height ="64px"></f:TextArea>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:Label ID="Label5" Text="六、改进意见" CssClass="title" runat="server"></f:Label>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>                                                                        
                                                         <f:TextArea ID="txtValues6" runat="server" Height ="64px"></f:TextArea>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>
                                                        <f:Label ID="Label6" Text="七、检查结论" CssClass="title" runat="server"></f:Label>
                                                    </Items>
                                                </f:FormRow>
                                                <f:FormRow>
                                                    <Items>                                                                        
                                                         <f:TextArea ID="txtValues7" runat="server" Height ="64px"></f:TextArea>
                                                    </Items>
                                                </f:FormRow>
                                             </Rows>
                                        </f:Form>
                                    </Items>                                   
                                    <Items>
                                        <f:Grid ID="gvItem" ShowBorder="true" ShowHeader="false" Title="监督检查报告"  EnableCollapse="false" runat="server"
                                            BoxFlex="1" DataKeyNames="UpCheckReportItemId" AllowCellEditing="true" EnableColumnLines="true" SortDirection="ASC"
                                            ClicksToEdit="1" DataIDField="UpCheckReportItemId" AllowSorting="false"   EnableTextSelection="True">
                                            <Toolbars>     
                                                <f:Toolbar ID="Toolbar5" runat="server" ToolbarAlign="Right">
                                                    <Items>
                                                        <f:Label ID="Label111" runat="server" Text="八、检查工作组人员" CssClass="title"></f:Label>
                                                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                                        <f:Button ID="btnNewItem" ToolTip="新增数据" Icon="Add" EnablePostBack="false" runat="server">
                                                        </f:Button>
                                                         <f:Button ID="btnDeleteItem" ToolTip="删除选中行" Icon="Delete" EnablePostBack="false" runat="server">
                                                        </f:Button>                                    
                                                    </Items>
                                                </f:Toolbar>
                                            </Toolbars>
                                            <Columns>         
                                                <f:RenderField HeaderText="序号" ColumnID="SortIndex" DataField="SortIndex"  
                                                    HeaderTextAlign="Center"  TextAlign="Left" Width="60px">
                                                    <Editor>
                                                        <f:TextBox ID="txtSortIndex" Required="true" runat="server">
                                                        </f:TextBox>
                                                    </Editor>
                                                </f:RenderField>                                                
                                                 <f:RenderField HeaderText="姓名" ColumnID="Name" DataField="Name" 
                                                    HeaderTextAlign="Center"  TextAlign="Left" Width="80px">
                                                    <Editor>
                                                        <f:TextBox ID="txtName" Required="true" runat="server">
                                                        </f:TextBox>
                                                    </Editor>
                                                </f:RenderField>
                                                <f:RenderField HeaderText="性别" ColumnID="Sex" DataField="Sex" 
                                                    HeaderTextAlign="Center"  TextAlign="Left" Width="70px">
                                                    <Editor>
                                                        <f:TextBox ID="txtSex" runat="server">
                                                        </f:TextBox>
                                                    </Editor>
                                                 </f:RenderField>
                                                 <f:RenderField HeaderText="所在单位" ColumnID="UnitName" DataField="UnitName" 
                                                    HeaderTextAlign="Center"  TextAlign="Left" Width="180px" ExpandUnusedSpace="true">
                                                    <Editor>
                                                        <f:TextBox ID="txtUnitName"  runat="server">
                                                        </f:TextBox>
                                                    </Editor>
                                                 </f:RenderField>                 
                                                 <f:RenderField HeaderText="所在单位职务" ColumnID="PostName" DataField="PostName" SortField="PostName"
                                                    HeaderTextAlign="Center"  TextAlign="Left" Width="110px">
                                                    <Editor>
                                                        <f:TextBox ID="txtPostName"  runat="server">
                                                        </f:TextBox>
                                                    </Editor>
                                                 </f:RenderField>
                                                 <f:RenderField HeaderText="职称" ColumnID="WorkTitle" DataField="WorkTitle" SortField="WorkTitle"
                                                    HeaderTextAlign="Center"  TextAlign="Left" Width="90px">
                                                    <Editor>
                                                        <f:TextBox ID="txtWorkTitle"  runat="server">
                                                        </f:TextBox>
                                                    </Editor>
                                                 </f:RenderField>
                                                 <f:RenderField HeaderText="检查工作组职务" ColumnID="CheckPostName" DataField="CheckPostName" SortField="CheckPostName"
                                                    HeaderTextAlign="Center"  TextAlign="Left" Width="120px">
                                                    <Editor>
                                                        <f:TextBox ID="txtCheckPostName"  runat="server">
                                                        </f:TextBox>
                                                    </Editor>
                                                 </f:RenderField>
                                                 <f:RenderField HeaderText="检查日期" ColumnID="CheckDate" DataField="CheckDate" SortField="CheckDate"
                                                    HeaderTextAlign="Center" TextAlign="Left" Width="110px" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd">
                                                    <Editor>
                                                        <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" ID="txtCheckDate">
                                                        </f:DatePicker>
                                                    </Editor>
                                                 </f:RenderField>
                                                 <f:LinkButtonField ColumnID="Delete1" Width="50px" EnablePostBack="false" Icon="Delete" 
                                                    HeaderTextAlign="Center" HeaderText="删除"/>
                                            </Columns>
                                        </f:Grid>
                                    </Items>
                                </f:Panel>
                            </Items>
                        </f:Tab>
                         <f:Tab ID="Tab2" Title="安全监督检查明细表" BodyPadding="5px" Layout="Fit" IconFont="Bookmark" runat="server">
                           <Items>
                                <f:Panel runat="server" ID="panel1" RegionPosition="Center" ShowBorder="true"
                                    Layout="VBox" ShowHeader="false" BodyPadding="2px" IconFont="PlusCircle" Title="安全监督检查明细表"
                                    TitleToolTip="安全监督检查明细表" AutoScroll="true">                                     
                                    <Items>
                                         <f:Form ID="Form2" ShowBorder="true" ShowHeader="false" AutoScroll="true" EnableCollapse="true" Collapsed="false"
                                                BodyPadding="2px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                            <Rows>
                                                <f:FormRow>
                                                    <Items>
                                                         <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="开始时间" ID="txtCheckStartTime">
                                                         </f:DatePicker>
                                                         <f:Label runat="server" ID ="Label7"></f:Label>
                                                         <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="结束时间" ID="txtCheckEndTime">
                                                         </f:DatePicker>
                                                         <f:Label runat="server" ID ="Label8"></f:Label>
                                                    </Items>
                                                </f:FormRow>                                               
                                             </Rows>
                                        </f:Form>
                                    </Items>                                   
                                    <Items>
                                        <f:Grid ID="gvItem2" ShowBorder="true" ShowHeader="false" Title="安全监督检查明细表"  EnableCollapse="false" runat="server"
                                            BoxFlex="1" DataKeyNames="UpCheckReportItem2Id" AllowCellEditing="true" EnableColumnLines="true" SortDirection="ASC"
                                            ClicksToEdit="1" DataIDField="UpCheckReportItem2Id" AllowSorting="false"   EnableTextSelection="True">
                                            <Toolbars>     
                                                <f:Toolbar ID="Toolbar2" runat="server" ToolbarAlign="Right">
                                                    <Items>                                                       
                                                        <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                                        <f:Button ID="btnNewItem2" ToolTip="新增数据" Icon="Add" EnablePostBack="false" runat="server">
                                                        </f:Button>
                                                         <f:Button ID="btnDeleteItem2" ToolTip="删除选中行" Icon="Delete" EnablePostBack="false" runat="server">
                                                        </f:Button>                                    
                                                    </Items>
                                                </f:Toolbar>
                                            </Toolbars>
                                            <Columns>         
                                                <f:RenderField HeaderText="序号" ColumnID="SortIndex" DataField="SortIndex"  
                                                    HeaderTextAlign="Center"  TextAlign="Left" Width="50px">
                                                    <Editor>
                                                        <f:TextBox ID="TextBox1" Required="true" runat="server">
                                                        </f:TextBox>
                                                    </Editor>
                                                </f:RenderField>                                                
                                                 <f:RenderField HeaderText="受检单位（项目）名称" ColumnID="SubjectObject" DataField="SubjectObject" 
                                                    HeaderTextAlign="Center"  TextAlign="Left" Width="180px">
                                                    <Editor>
                                                        <f:TextBox ID="txtSubjectObject" Required="true" runat="server">
                                                        </f:TextBox>
                                                    </Editor>
                                                </f:RenderField>
                                                <f:RenderField HeaderText="受检单位（项目）概况" ColumnID="SubjectObjectInfo" DataField="SubjectObjectInfo" 
                                                    HeaderTextAlign="Center"  TextAlign="Left" Width="180px">
                                                    <Editor>
                                                        <f:TextBox ID="txtSubjectObjectInfo" runat="server">
                                                        </f:TextBox>
                                                    </Editor>
                                                 </f:RenderField>
                                                 <f:GroupField HeaderText="受检单位（项目）" TextAlign="Center" HeaderTextAlign="Center">
                                                    <Columns>
                                                        <f:RenderField Width="80px" ColumnID="UnitMan" DataField="UnitMan" FieldType="String" 
                                                            HeaderText="负责人"  HeaderTextAlign="Center" TextAlign="Center">
                                                           <Editor>
                                                                <f:TextBox ID="txtUnitMan" runat="server">
                                                                </f:TextBox>
                                                            </Editor>
                                                        </f:RenderField>
                                                        <f:RenderField Width="120px" ColumnID="UnitManTel" DataField="UnitManTel" FieldType="String"
                                                           HeaderText="电话" HeaderTextAlign="Center" TextAlign="Center">
                                                            <Editor>
                                                                <f:TextBox ID="txtUnitManTel" runat="server">
                                                                </f:TextBox>
                                                            </Editor>
                                                        </f:RenderField>
                                                    </Columns>
                                                </f:GroupField>
                                                <f:GroupField HeaderText="受检单位(项目)专职安全管理" TextAlign="Center" HeaderTextAlign="Center">
                                                    <Columns>
                                                        <f:RenderField Width="100px" ColumnID="UnitHSSEMan" DataField="UnitHSSEMan" FieldType="String" 
                                                           HeaderText="负责人" HeaderTextAlign="Center" TextAlign="Center">
                                                             <Editor>
                                                                <f:TextBox ID="txtUnitHSSEMan" runat="server">
                                                                </f:TextBox>
                                                            </Editor>
                                                        </f:RenderField>
                                                        <f:RenderField Width="120px" ColumnID="UnitHSSEManTel" DataField="UnitHSSEManTel" FieldType="String"
                                                           HeaderText="电话" HeaderTextAlign="Center" TextAlign="Center">
                                                             <Editor>
                                                                <f:TextBox ID="txtUnitHSSEManTel" runat="server">
                                                                </f:TextBox>
                                                            </Editor>
                                                        </f:RenderField>
                                                    </Columns>
                                                </f:GroupField>
                                                 <f:RenderField HeaderText="检查时间" ColumnID="CheckDate" DataField="CheckDate" SortField="CheckDate"
                                                    HeaderTextAlign="Center" TextAlign="Left" Width="100px" FieldType="Date" Renderer="Date" RendererArgument="yyyy-MM-dd">
                                                    <Editor>
                                                        <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" ID="DatePicker1">
                                                        </f:DatePicker>
                                                    </Editor>
                                                 </f:RenderField>                                                
                                                 <f:RenderField HeaderText="隐患项数" ColumnID="RectifyCount" DataField="RectifyCount"
                                                    HeaderTextAlign="Center"  TextAlign="Left" Width="85px" FieldType="Int">
                                                    <Editor>
                                                        <f:NumberBox ID="txtRectifyCount"  runat="server" NoDecimal="true" NoNegative="true">
                                                        </f:NumberBox>
                                                    </Editor>
                                                 </f:RenderField>
                                                 <f:RenderField HeaderText="整改项数" ColumnID="CompRectifyCount" DataField="CompRectifyCount"
                                                    HeaderTextAlign="Center"  TextAlign="Left" Width="85px" FieldType="Int">
                                                    <Editor>
                                                        <f:NumberBox ID="txtCompRectifyCount"  runat="server" NoDecimal="true" NoNegative="true">
                                                        </f:NumberBox>
                                                    </Editor>
                                                 </f:RenderField> 
                                                 <f:RenderField HeaderText="检查得分" ColumnID="TotalGetScore" DataField="TotalGetScore"
                                                    HeaderTextAlign="Center"  TextAlign="Left" Width="85px" FieldType="Double">
                                                    <Editor>
                                                        <f:NumberBox ID="txtTotalGetScore"  runat="server" NoDecimal="false" NoNegative="true" DecimalPrecision="2">
                                                        </f:NumberBox>
                                                    </Editor>
                                                 </f:RenderField>
                                                  <f:RenderField HeaderText="检查结果等级" ColumnID="ResultLevel" DataField="ResultLevel" 
                                                    HeaderTextAlign="Center"  TextAlign="Left" Width="120px">
                                                    <Editor>
                                                        <f:DropDownList ID="TextBox2" runat="server">
                                                            <f:ListItem Text="合格" Value="合格"/>
                                                            <f:ListItem Text="基本合格" Value="基本合格"/>
                                                            <f:ListItem Text="不合格" Value="不合格"/>
                                                        </f:DropDownList>
                                                    </Editor>
                                                 </f:RenderField> 
                                                 <f:LinkButtonField ColumnID="Delete2" Width="50px" EnablePostBack="false" Icon="Delete" 
                                                    HeaderTextAlign="Center" HeaderText="删除">
                                                 </f:LinkButtonField>
                                            </Columns>
                                        </f:Grid>
                                    </Items>
                                    <Items>
                                         <f:Form ID="Form3" ShowBorder="true" ShowHeader="false" AutoScroll="true" EnableCollapse="true" Collapsed="false"
                                                BodyPadding="2px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                            <Rows>
                                                <f:FormRow>
                                                    <Items>
                                                         <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="填表" ID="txtCompileDate">
                                                         </f:DatePicker>
                                                         <f:Label runat="server" ID ="lbtem"></f:Label>
                                                         <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="审核" ID="txtAuditDate">
                                                         </f:DatePicker>
                                                         <f:Label runat="server" ID ="Label9"></f:Label>
                                                    </Items>
                                                </f:FormRow>                                               
                                             </Rows>
                                        </f:Form>
                                    </Items>  
                                </f:Panel>
                            </Items>
                        </f:Tab>
                    </Tabs>
                </f:TabStrip>
             </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar1" Position="Top" ToolbarAlign="Right" runat="server">
                   <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button> 
                     <f:Button ID="btnSaveUp" Icon="PageSave" runat="server" ToolTip="保存并上报" ValidateForms="SimpleForm1"
                        OnClick="btnSaveUp_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>                                               
                </Items>
              </f:Toolbar>
            </Toolbars>
        </f:Panel>
    </form>
</body>
</html>
