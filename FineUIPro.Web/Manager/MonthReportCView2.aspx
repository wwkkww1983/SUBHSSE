<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCView2.aspx.cs" Inherits="FineUIPro.Web.Manager.MonthReportCView2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="SimpleForm1" runat="server">
    </f:PageManager>
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" AutoScroll="true"
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
        <Rows>
           <f:FormRow>
                <Items>
                    <f:GroupPanel ID="GroupPanel9" Layout="Anchor" Title="2.本月项目现场HSE人力投入情况" runat="server">
                        <Items>
                            <f:Grid ID="gvPersonSort" ShowBorder="true" ShowHeader="false" Title="本月项目现场HSE人力投入情况"
                                runat="server" AllowCellEditing="false" ClicksToEdit="1" DataIDField="PersonSortId" 
                                DataKeyNames="PersonSortId,UnitId" EnableMultiSelect="false" ShowGridHeader="true"
                                Height="420px" EnableColumnLines="true" EnableSummary="true"
                                SummaryPosition="Flow">
                                <Columns>
                                    <f:TemplateField Width="250px" HeaderText="单位" HeaderTextAlign="Center" TextAlign="Left"
                                        ColumnID="UnitId">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# ConvertUnitName(Eval("UnitId")) %>'
                                                ToolTip='<%# ConvertUnitName(Eval("UnitId")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </f:TemplateField>
                                    <f:RenderField Width="90px" ColumnID="SumPersonNum" DataField="SumPersonNum" FieldType="Int"
                                        HeaderText="总人数" HeaderTextAlign="Center" TextAlign="Left">
                                        <Editor>
                                            <f:NumberBox ID="nbSumPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="160px" ColumnID="HSEPersonNum" DataField="HSEPersonNum" FieldType="Int"
                                        HeaderText="专职HSE管理人员数量" HeaderTextAlign="Center" TextAlign="Left">
                                        <Editor>
                                            <f:NumberBox ID="nbHSEPersonNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                runat="server">
                                            </f:NumberBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="200px" ColumnID="ContractRange" DataField="ContractRange" FieldType="String"
                                        HeaderText="承包范围" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                        <Editor>
                                            <f:TextBox runat="server" ID="txtContractRange">
                                            </f:TextBox>
                                        </Editor>
                                    </f:RenderField>
                                    <f:RenderField Width="200px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                        HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
                                        <Editor>
                                            <f:TextBox runat="server" ID="txtRemark">
                                            </f:TextBox>
                                        </Editor>
                                    </f:RenderField>
                                </Columns>
                            </f:Grid>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    </form>
</body>
</html>
