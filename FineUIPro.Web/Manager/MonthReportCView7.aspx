<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCView7.aspx.cs" Inherits="FineUIPro.Web.Manager.MonthReportCView7" %>

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
                    <f:GroupPanel ID="GroupPanel43" Layout="Anchor" Title="7.HSE事故/事件描述" runat="server">
                        <Items>
                            <f:GroupPanel ID="GroupPanel44" Layout="Anchor" Title="7.1 管理绩效数据统计（表一）" runat="server">
                                <Items>
                                    <f:Grid ID="gvAccidentDesciption" ShowBorder="true" ShowHeader="false" Title="管理绩效数据统计"
                                        runat="server" AllowCellEditing="false" ClicksToEdit="1" DataIDField="AccidentDesId"
                                        DataKeyNames="AccidentDesId" EnableMultiSelect="false" ShowGridHeader="true"
                                        Height="220px" EnableColumnLines="true" AutoScroll="true">
                                        <Columns>
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="300px" ColumnID="Matter" DataField="Matter" FieldType="String"
                                                HeaderText="事项" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox45">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="300px" ColumnID="MonthDataNum" DataField="MonthDataNum" FieldType="String"
                                                HeaderText="本月数据" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:NumberBox ID="NumberBox4" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="300px" ColumnID="YearDataNum" DataField="YearDataNum" FieldType="String"
                                                HeaderText="年度累计数据" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:NumberBox ID="NumberBox5" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                                                    </f:NumberBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel45" Layout="Anchor" Title="7.2 管理绩效数据统计（表二，如项目发生事故，请继续填写下表，否则填无）"
                                runat="server">
                                <Items>
                                    <f:Grid ID="gvAccidentDesciptionItem" ShowBorder="true" ShowHeader="false" Title="管理绩效数据统计"
                                        runat="server" AllowCellEditing="false" ClicksToEdit="1" DataIDField="AccidentDesItemId"
                                        DataKeyNames="AccidentDesItemId" EnableMultiSelect="false" ShowGridHeader="true"
                                        Height="220px" EnableColumnLines="true" AutoScroll="true">
                                        <Columns>
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="300px" ColumnID="Matter" DataField="Matter" FieldType="String"
                                                HeaderText="事项" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox46">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="500px" ColumnID="Datas" DataField="Datas" FieldType="String"
                                                HeaderText="数据" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox47">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel46" Layout="Anchor" Title="7.3 事故/事件描述（文字描述）" runat="server">
                                <Items>
                                    <f:TextArea runat="server" ID="txtAccidentDes" Readonly="true">
                                    </f:TextArea>
                                </Items>
                            </f:GroupPanel>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
        </Rows>
    </f:Form>
    </form>
</body>
</html>
