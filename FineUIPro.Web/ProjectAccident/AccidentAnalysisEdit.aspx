<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccidentAnalysisEdit.aspx.cs"
    Inherits="FineUIPro.Web.ProjectAccident.AccidentAnalysisEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑事故统计</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .labcenter
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server" />
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
            <f:FormRow ColumnWidths="50% 25% 25%">
                <Items>
                    <f:DropDownList ID="ddlProjectId" runat="server" Label="项目" LabelWidth="60px" Width="400px" 
                        ShowRedStar="True" FocusOnPageLoad="true" Required="true">
                    </f:DropDownList>
                     <f:DatePicker runat="server" DateFormatString="yyyy-MM-dd" Label="时间" LabelWidth="60px"  ID="dpkCompileDate" EnableEdit="true">
                    </f:DatePicker>
                      <f:TextBox ID="txtCompileMan" runat="server" Label="处理人" MaxLength="50" LabelWidth="60px" >
                    </f:TextBox>
                </Items>
            </f:FormRow>          
            <f:FormRow>
                <Items>
                    <f:TextArea ID="txtRemarks" runat="server" Height="70px" Label="备注" MaxLength="2000" LabelWidth="60px" >
                    </f:TextArea>
                </Items>
            </f:FormRow>
            <f:FormRow>
                <Items>
                    <f:Grid ID="Grid1" ShowBorder="true" ShowHeader="false" EnableCollapse="true" runat="server"
                        BoxFlex="1" DataKeyNames="AccidentAnalysisItemId" AllowCellEditing="true"
                        ClicksToEdit="1" DataIDField="AccidentAnalysisItemId" EnableColumnLines="true">
                        <Columns>
                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="100px" HeaderTextAlign="Center" TextAlign="Center" />
                            <f:RenderField Width="100px" ColumnID="AccidentType" DataField="AccidentType" FieldType="String"
                                HeaderText="事故类别" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                            </f:RenderField>
                            <f:RenderField Width="160px" ColumnID="Death" DataField="Death" FieldType="String"
                                HeaderText="死" HeaderTextAlign="Center">
                                <Editor>
                                    <f:NumberBox ID="txtDeath" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Death")%>'
                                        runat="server">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="160px" ColumnID="Injuries" DataField="Injuries" FieldType="String"
                                HeaderText="重" HeaderTextAlign="Center">
                                <Editor>
                                    <f:NumberBox ID="txtInjuries" NoDecimal="true" NoNegative="true" MinValue="0" Text='<%# Eval("Injuries")%>'
                                        runat="server">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                            <f:RenderField Width="160px" ColumnID="MinorInjuries" DataField="MinorInjuries" FieldType="String"
                                HeaderText="轻" HeaderTextAlign="Center">
                                <Editor>
                                    <f:NumberBox ID="txtMinorInjuries" NoDecimal="true" NoNegative="true" MinValue="0"
                                        Text='<%# Eval("MinorInjuries")%>' runat="server">
                                    </f:NumberBox>
                                </Editor>
                            </f:RenderField>
                        </Columns>
                    </f:Grid>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server"  ValidateForms="SimpleForm1" Hidden="true"
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

        function onGridDataLoad(event) {
            this.mergeColumns(['RectifyName']);
        }
    </script>
</body>
</html>
