<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCView8.aspx.cs" Inherits="FineUIPro.Web.Manager.MonthReportCView8" %>

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
                    <f:GroupPanel ID="GroupPanel47" Layout="Anchor" Title="8.其他工作情况" runat="server">
                        <Items>
                            <f:Grid ID="gvOtherWork" ShowBorder="true" ShowHeader="false" Title="其他工作情况" runat="server"
                                AllowCellEditing="false" ClicksToEdit="1" DataIDField="OtherWorkId" DataKeyNames="OtherWorkId"
                                EnableMultiSelect="false" ShowGridHeader="true" Height="420px" EnableColumnLines="true"
                                AutoScroll="true" >
                                <Columns>
                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                        TextAlign="Center" />
                                    <f:RenderField Width="300px" ColumnID="WorkContentDes" DataField="WorkContentDes"
                                        FieldType="String" HeaderText="工作内容描述" HeaderTextAlign="Center" TextAlign="Left"
                                        ExpandUnusedSpace="true">
                                        <Editor>
                                            <f:TextBox runat="server" ID="TextBox48">
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
