<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestDBEdit.aspx.cs" Inherits="FineUIPro.Web.OnlineCheck.TestDBEdit" %>

<!DOCTYPE html>
<html>
<head id="head1" runat="server">
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1"  AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Layout="VBox"  
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                      <f:FileUpload runat="server" ID="testContent" ButtonText="上传试题内容" AcceptFileTypes="image/*" ButtonOnly="true"
                           AutoPostBack="true" OnFileSelected="testContent_FileSelected" ButtonIconFont="Upload">
                       </f:FileUpload>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Rows>            
          <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel1"  CssClass="marginr" Layout="Region" Title="试题内容" runat="server" Height="320px">
                    <Items>
                       <f:Image ID="imgTestContent"  MarginLeft="2px"  ShowEmptyLabel="true" runat="server" />
                   </Items>
                  </f:GroupPanel>
                </Items>
            </f:FormRow>
             <f:FormRow>
                <Items>
                    <f:DropDownList ID="drpTestType" Label="试题类型"  EnableEdit="true"  ForceSelection="false" EnableSimulateTree="true"
                      runat="server" >
                    </f:DropDownList>
                     <f:DropDownList ID="drpItemType" Label="题型"   EnableEdit="true" ForceSelection="false" EnableSimulateTree="true" runat="server" >
                    </f:DropDownList>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:TextBox ID="txtTestNo" runat="server" Label="题号" Required="true" ShowRedStar="true"></f:TextBox>
                    <f:TextBox ID="txtTestKey" runat="server"  Label="答案" Required="true" ShowRedStar="true"></f:TextBox>
                </Items>
            </f:FormRow>

            <f:FormRow>
                <Items>
                    <f:NumberBox ID="txtKeyNumber" runat="server" Label="答案数"  Required="true" 
                        ShowRedStar="true" NoDecimal="True" NoNegative="True"></f:NumberBox>
                </Items>
            </f:FormRow>
        </Rows>
        
    </f:Form>
    </form>
</body>
</html>
