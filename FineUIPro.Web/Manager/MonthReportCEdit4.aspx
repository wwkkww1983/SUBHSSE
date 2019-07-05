<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCEdit4.aspx.cs"
    Inherits="FineUIPro.Web.Manager.MonthReportCEdit4" %>

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
                    <f:GroupPanel ID="GroupPanel11" Layout="Anchor" Title="4.本月项目HSE现场管理" runat="server">
                        <Items>
                            <f:GroupPanel ID="GroupPanel12" Layout="Anchor" Title="4.1 危险源动态识别及控制" runat="server">
                                <Items>
                                    <f:GroupPanel ID="GroupPanel13" Layout="Anchor" Title="4.1.1 管理绩效数据统计" runat="server">
                                        <Items>
                                            <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                                <Rows>
                                                    <f:FormRow>
                                                        <Items>
                                                            <f:NumberBox ID="txtHazardNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                                Label="本月开展的危险源辨识活动次数" LabelWidth="200px">
                                                            </f:NumberBox>
                                                            <f:NumberBox ID="txtYearHazardNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server" Label="本年度累计危险源辨识活动次数" LabelWidth="200px">
                                                            </f:NumberBox>
                                                        </Items>
                                                    </f:FormRow>
                                                </Rows>
                                            </f:Form>
                                        </Items>
                                    </f:GroupPanel>
                                    <f:GroupPanel ID="GroupPanel14" Layout="Anchor" Title="4.1.2 危险源辨识活动情况描述（对危险源辨识活动情况进行书面说明 ）"
                                        runat="server">
                                        <Items>
                                            <f:Grid ID="gvHazardSort" ShowBorder="true" ShowHeader="false" Title="危险源辨识活动情况描述"
                                                runat="server" AllowCellEditing="true" ClicksToEdit="1" DataIDField="HazardSortId"
                                                DataKeyNames="HazardSortId" EnableMultiSelect="false" ShowGridHeader="true" Height="220px"
                                                EnableColumnLines="true" AutoScroll="true" OnRowCommand="gvHazardSort_RowCommand">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar2" Position="Top" runat="server" ToolbarAlign="Right">
                                                        <Items>
                                                            <f:Button ID="btnNewHazardSort" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewHazardSort_Click">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                                        ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="250px" ColumnID="HazardName" DataField="HazardName" FieldType="String"
                                                        HeaderText="危险源" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="txtHazardName">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="250px" ColumnID="UnitAndArea" DataField="UnitAndArea" FieldType="String"
                                                        HeaderText="存在单位及区域" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="txtUnitAndArea">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="250px" ColumnID="StationDef" DataField="StationDef" FieldType="String"
                                                        HeaderText="现场情况描述" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="txtStationDef">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="250px" ColumnID="HandleWay" DataField="HandleWay" FieldType="String"
                                                        HeaderText="控制措施" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="txtHandleWay">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                        </Items>
                                    </f:GroupPanel>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="4.2 HSE培训" runat="server">
                                <Items>
                                    <f:GroupPanel ID="GroupPanel3" Layout="Anchor" Title="4.2.1 管理绩效数据统计" runat="server">
                                        <Items>
                                            <f:Grid ID="gvTrainSort" ShowBorder="true" ShowHeader="false" Title="管理绩效数据统计" runat="server"
                                                AllowCellEditing="true" ClicksToEdit="1" DataIDField="TrainSortId" DataKeyNames="TrainSortId"
                                                EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                                                AutoScroll="true" OnRowCommand="gvTrainSort_RowCommand">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar3" Position="Top" runat="server" ToolbarAlign="Right">
                                                        <Items>
                                                            <f:Button ID="btnNewTrainSort" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewTrainSort_Click">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                                        ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="250px" ColumnID="TrainContent" DataField="TrainContent" FieldType="String"
                                                        HeaderText="培训内容" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="txtTrainContent">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="TeachHour" DataField="TeachHour" FieldType="String"
                                                        HeaderText="学时数" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:NumberBox ID="nbTeachHour" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="200px" ColumnID="TeachMan" DataField="TeachMan" FieldType="String"
                                                        HeaderText="讲师/教材" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="txtTeachMan">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" FieldType="String"
                                                        HeaderText="受训单位" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox4">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="PersonNum" DataField="PersonNum" FieldType="String"
                                                        HeaderText="受训人数" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:NumberBox ID="nbPersonNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                        </Items>
                                    </f:GroupPanel>
                                    <f:GroupPanel ID="GroupPanel4" Layout="Anchor" Title="4.2.2 培训活动情况说明" runat="server">
                                        <Items>
                                            <f:Grid ID="gvTrainActivitySort" ShowBorder="true" ShowHeader="false" Title="培训活动情况说明"
                                                runat="server" AllowCellEditing="true" ClicksToEdit="1" DataIDField="TrainActivitySortId"
                                                DataKeyNames="TrainActivitySortId" EnableMultiSelect="false" ShowGridHeader="true"
                                                Height="220px" EnableColumnLines="true" AutoScroll="true" OnRowCommand="gvTrainActivitySort_RowCommand">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar4" Position="Top" runat="server" ToolbarAlign="Right">
                                                        <Items>
                                                            <f:Button ID="btnNewTrainActivitySort" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewTrainActivitySort_Click">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                                        ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="350px" ColumnID="ActivityName" DataField="ActivityName" FieldType="String"
                                                        HeaderText="主要培训活动名称" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="txtActivityName">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="TrainDate" DataField="TrainDate" FieldType="String"
                                                        HeaderText="培训时间" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="txtTrainDate">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="250px" ColumnID="TrainEffect" DataField="TrainEffect" FieldType="String"
                                                        HeaderText="培训效果（参加人数，所达到的效果）" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="txtTrainEffect">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                        </Items>
                                    </f:GroupPanel>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel5" Layout="Anchor" Title="4.3 HSE检查" runat="server">
                                <Items>
                                    <f:GroupPanel ID="GroupPanel6" Layout="Anchor" Title="4.3.1 管理绩效数据统计" runat="server">
                                        <Items>
                                            <f:Grid ID="gvCheckSort" ShowBorder="true" ShowHeader="false" Title="管理绩效数据统计" runat="server"
                                                AllowCellEditing="true" ClicksToEdit="1" DataIDField="CheckSortId" DataKeyNames="CheckSortId"
                                                EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                                                AutoScroll="true" OnRowCommand="gvCheckSort_RowCommand">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar5" Position="Top" runat="server" ToolbarAlign="Right">
                                                        <Items>
                                                            <f:Button ID="btnNewCheckSort" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewCheckSort_Click">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                                        ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="150px" ColumnID="CheckType" DataField="CheckType" FieldType="String"
                                                        HeaderText="检查类型" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="txtCheckType">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="150px" ColumnID="CheckNumber" DataField="CheckNumber" FieldType="String"
                                                        HeaderText="本月开展次数" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:NumberBox ID="nbCheckNumber" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="150px" ColumnID="YearCheckNum" DataField="YearCheckNum" FieldType="String"
                                                        HeaderText="年度累计次数" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:NumberBox ID="nbYearCheckNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="160px" ColumnID="TotalCheckNum" DataField="TotalCheckNum" FieldType="String"
                                                        HeaderText="项目总累计次数" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:NumberBox ID="nbTotalCheckNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="170px" ColumnID="ViolationNumber" DataField="ViolationNumber"
                                                        FieldType="String" HeaderText="发现及整改隐患数量" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:NumberBox ID="nbViolationNumber" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="180px" ColumnID="YearViolationNum" DataField="YearViolationNum"
                                                        FieldType="String" HeaderText="年度隐患整改累计数量" HeaderTextAlign="Center" TextAlign="Left"
                                                        ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:NumberBox ID="nbYearViolationNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                        </Items>
                                    </f:GroupPanel>
                                    <f:GroupPanel ID="GroupPanel7" Layout="Anchor" Title="4.3.2 检查活动情况说明（对检查开展的情况进行文字说明，包括检查效果等）"
                                        runat="server">
                                        <Items>
                                            <f:Grid ID="gvCheckDetailSort" ShowBorder="true" ShowHeader="false" Title="检查活动情况说明"
                                                runat="server" AllowCellEditing="true" ClicksToEdit="1" DataIDField="CheckDetailSortId"
                                                DataKeyNames="CheckDetailSortId" EnableMultiSelect="false" ShowGridHeader="true"
                                                Height="220px" EnableColumnLines="true" AutoScroll="true" OnRowCommand="gvCheckDetailSort_RowCommand">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar6" Position="Top" runat="server" ToolbarAlign="Right">
                                                        <Items>
                                                            <f:Button ID="btnNewCheckDetailSort" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewCheckDetailSort_Click">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                                        ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="200px" ColumnID="CheckType" DataField="CheckType" FieldType="String"
                                                        HeaderText="检查类型" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="checktype">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="CheckTime" DataField="CheckTime" FieldType="String"
                                                        HeaderText="检查时间" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="txtCheckTime">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="300px" ColumnID="JoinUnit" DataField="JoinUnit" FieldType="String"
                                                        HeaderText="参加单位" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox3">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="350px" ColumnID="StateDef" DataField="StateDef" FieldType="String"
                                                        HeaderText="检查情况描述" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox1">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                        </Items>
                                    </f:GroupPanel>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel8" Layout="Anchor" Title="4.4 HSE会议" runat="server">
                                <Items>
                                    <f:GroupPanel ID="GroupPanel15" Layout="Anchor" Title="4.4.1 管理绩效数据统计" runat="server">
                                        <Items>
                                            <f:Form ID="Form3" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                                <Rows>
                                                    <f:FormRow>
                                                        <Items>
                                                            <f:NumberBox ID="txtMeetingNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                                Label="本月组织/参加会议次数" LabelWidth="200px">
                                                            </f:NumberBox>
                                                            <f:NumberBox ID="txtYearMeetingNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server" Label="本年度累计组织/参加会议次数" LabelWidth="200px">
                                                            </f:NumberBox>
                                                        </Items>
                                                    </f:FormRow>
                                                </Rows>
                                            </f:Form>
                                        </Items>
                                    </f:GroupPanel>
                                    <f:GroupPanel ID="GroupPanel16" Layout="Anchor" Title="4.4.2 会议情况说明" runat="server">
                                        <Items>
                                            <f:Grid ID="gvMeetingSort" ShowBorder="true" ShowHeader="false" Title="会议情况说明" runat="server"
                                                AllowCellEditing="true" ClicksToEdit="1" DataIDField="MeetingSortId" DataKeyNames="MeetingSortId"
                                                EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                                                AutoScroll="true" OnRowCommand="gvMeetingSort_RowCommand">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar7" Position="Top" runat="server" ToolbarAlign="Right">
                                                        <Items>
                                                            <f:Button ID="btnNewMeetingSort" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewMeetingSort_Click">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                                        ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="150px" ColumnID="MeetingType" DataField="MeetingType" FieldType="String"
                                                        HeaderText="会议主题" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox2">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="MeetingHours" DataField="MeetingHours" FieldType="String"
                                                        HeaderText="时数" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:NumberBox ID="NumberBox1" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="MeetingHostMan" DataField="MeetingHostMan"
                                                        FieldType="String" HeaderText="主持人" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox5">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="MeetingDate" DataField="MeetingDate" FieldType="String"
                                                        HeaderText="召开时间" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox6">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="AttentPerson" DataField="AttentPerson" FieldType="String"
                                                        HeaderText="参会人员" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox7">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="200px" ColumnID="MainContent" DataField="MainContent" FieldType="String"
                                                        HeaderText="会议主要内容" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox8">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                        </Items>
                                    </f:GroupPanel>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel17" Layout="Anchor" Title="4.5 HSE宣传" runat="server">
                                <Items>
                                    <f:GroupPanel ID="GroupPanel18" Layout="Anchor" Title="4.5.1 管理绩效数据统计" runat="server">
                                        <Items>
                                            <f:Form ID="Form4" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                                <Rows>
                                                    <f:FormRow>
                                                        <Items>
                                                            <f:NumberBox ID="txtPromotionalActiviteNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server" Label="本月开展的宣传活动次数" LabelWidth="200px">
                                                            </f:NumberBox>
                                                            <f:NumberBox ID="txtYearPromotionalActiviteNum" NoDecimal="true" NoNegative="true"
                                                                MinValue="0" runat="server" Label="本年度累计宣传活动次数" LabelWidth="200px">
                                                            </f:NumberBox>
                                                        </Items>
                                                    </f:FormRow>
                                                </Rows>
                                            </f:Form>
                                        </Items>
                                    </f:GroupPanel>
                                    <f:GroupPanel ID="GroupPanel19" Layout="Anchor" Title="4.5.2 宣传活动情况说明" runat="server">
                                        <Items>
                                            <f:Grid ID="gvPromotionalActiviteSort" ShowBorder="true" ShowHeader="false" Title="宣传活动情况说明"
                                                runat="server" AllowCellEditing="true" ClicksToEdit="1" DataIDField="PromotionalActiviteSortId"
                                                DataKeyNames="PromotionalActiviteSortId" EnableMultiSelect="false" ShowGridHeader="true"
                                                Height="220px" EnableColumnLines="true" AutoScroll="true" OnRowCommand="gvPromotionalActiviteSort_RowCommand">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar8" Position="Top" runat="server" ToolbarAlign="Right">
                                                        <Items>
                                                            <f:Button ID="btnNewPromotionalActiviteSort" ToolTip="新增" Icon="Add" runat="server"
                                                                OnClick="btnNewPromotionalActiviteSort_Click">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                                        ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="250px" ColumnID="PromotionalActivitiesName" DataField="PromotionalActivitiesName"
                                                        FieldType="String" HeaderText="活动主题" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox9">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="CompileDate" DataField="CompileDate" FieldType="String"
                                                        HeaderText="活动时间" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox10">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="250px" ColumnID="ParticipatingUnits" DataField="ParticipatingUnits"
                                                        FieldType="String" HeaderText="参加单位" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox11">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="250px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                                        HeaderText="活动效果描述" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox12">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                        </Items>
                                    </f:GroupPanel>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel20" Layout="Anchor" Title="4.6 应急管理" runat="server">
                                <Items>
                                    <f:GroupPanel ID="GroupPanel21" Layout="Anchor" Title="4.6.1 管理绩效数据统计" runat="server">
                                        <Items>
                                            <f:Form ID="Form5" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                                <Rows>
                                                    <f:FormRow>
                                                        <Items>
                                                            <f:Label ID="Label1" runat="server" Text="统计类型">
                                                            </f:Label>
                                                            <f:Label ID="Label2" runat="server" Text="本月">
                                                            </f:Label>
                                                            <f:Label ID="Label3" runat="server" Text="年度累计">
                                                            </f:Label>
                                                        </Items>
                                                    </f:FormRow>
                                                    <f:FormRow>
                                                        <Items>
                                                            <f:Label ID="Label5" runat="server" Text="项目综合应急预案修编（发布）次数">
                                                            </f:Label>
                                                            <f:NumberBox ID="txtComplexEmergencyNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server">
                                                            </f:NumberBox>
                                                            <f:NumberBox ID="txtYearComplexEmergencyNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server">
                                                            </f:NumberBox>
                                                        </Items>
                                                    </f:FormRow>
                                                    <f:FormRow>
                                                        <Items>
                                                            <f:Label ID="Label6" runat="server" Text="项目专项应急预案修编（发布）">
                                                            </f:Label>
                                                            <f:NumberBox ID="txtSpecialEmergencyNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server">
                                                            </f:NumberBox>
                                                            <f:NumberBox ID="txtYearSpecialEmergencyNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server">
                                                            </f:NumberBox>
                                                        </Items>
                                                    </f:FormRow>
                                                    <f:FormRow>
                                                        <Items>
                                                            <f:Label ID="Label7" runat="server" Text="应急演练活动次数">
                                                            </f:Label>
                                                            <f:NumberBox ID="txtDrillRecordNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server">
                                                            </f:NumberBox>
                                                            <f:NumberBox ID="txtYearDrillRecordNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server">
                                                            </f:NumberBox>
                                                        </Items>
                                                    </f:FormRow>
                                                </Rows>
                                            </f:Form>
                                        </Items>
                                    </f:GroupPanel>
                                    <f:GroupPanel ID="GroupPanel22" Layout="Anchor" Title="4.6.2 应急预案修编情况" runat="server">
                                        <Items>
                                            <f:Grid ID="gvEmergencySort" ShowBorder="true" ShowHeader="false" Title="应急预案修编情况"
                                                runat="server" AllowCellEditing="true" ClicksToEdit="1" DataIDField="EmergencySortId"
                                                DataKeyNames="EmergencySortId" EnableMultiSelect="false" ShowGridHeader="true"
                                                Height="220px" EnableColumnLines="true" AutoScroll="true" OnRowCommand="gvEmergencySort_RowCommand">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar9" Position="Top" runat="server" ToolbarAlign="Right">
                                                        <Items>
                                                            <f:Button ID="btnNewEmergencySort" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewEmergencySort_Click">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                                        ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="250px" ColumnID="EmergencyName" DataField="EmergencyName" FieldType="String"
                                                        HeaderText="修编预案名称" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox13">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="150px" ColumnID="ModefyPerson" DataField="ModefyPerson" FieldType="String"
                                                        HeaderText="修编人员" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox14">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="150px" ColumnID="ReleaseDate" DataField="ReleaseDate" FieldType="String"
                                                        HeaderText="发布时间" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox15">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="250px" ColumnID="StateRef" DataField="StateRef" FieldType="String"
                                                        HeaderText="修编情况描述" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox16">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                        </Items>
                                    </f:GroupPanel>
                                    <f:GroupPanel ID="GroupPanel23" Layout="Anchor" Title="4.6.3 应急演练活动情况" runat="server">
                                        <Items>
                                            <f:Grid ID="gvDrillSort" ShowBorder="true" ShowHeader="false" Title="应急演练活动情况" runat="server"
                                                AllowCellEditing="true" ClicksToEdit="1" DataIDField="DrillSortId" DataKeyNames="DrillSortId"
                                                EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                                                AutoScroll="true" OnRowCommand="gvDrillSort_RowCommand">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar10" Position="Top" runat="server" ToolbarAlign="Right">
                                                        <Items>
                                                            <f:Button ID="btnNewDrillSort" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewDrillSort_Click">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                                        ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="250px" ColumnID="DrillContent" DataField="DrillContent" FieldType="String"
                                                        HeaderText="演练内容" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox17">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="150px" ColumnID="DrillDate" DataField="DrillDate" FieldType="String"
                                                        HeaderText="演练时间" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox18">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="250px" ColumnID="JointUnit" DataField="JointUnit" FieldType="String"
                                                        HeaderText="参加单位" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox19">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="JoinPerson" DataField="JoinPerson" FieldType="String"
                                                        HeaderText="参加人数" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox20">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                        </Items>
                                    </f:GroupPanel>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel24" Layout="Anchor" Title="4.7 HSE许可管理" runat="server">
                                <Items>
                                    <f:GroupPanel ID="GroupPanel25" Layout="Anchor" Title="4.7.1 管理绩效数据统计" runat="server">
                                        <Items>
                                            <f:Form ID="Form6" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                                <Rows>
                                                    <f:FormRow>
                                                        <Items>
                                                            <f:Label ID="Label8" runat="server" Text="统计类型">
                                                            </f:Label>
                                                            <f:Label ID="Label9" runat="server" Text="本月办理/验收数量">
                                                            </f:Label>
                                                            <f:Label ID="Label10" runat="server" Text="年度累计">
                                                            </f:Label>
                                                        </Items>
                                                    </f:FormRow>
                                                    <f:FormRow>
                                                        <Items>
                                                            <f:Label ID="Label11" runat="server" Text="各类作业票的办理及审批">
                                                            </f:Label>
                                                            <f:NumberBox ID="txtLicenseNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                                                            </f:NumberBox>
                                                            <f:NumberBox ID="txtYearLicenseNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server">
                                                            </f:NumberBox>
                                                        </Items>
                                                    </f:FormRow>
                                                    <f:FormRow>
                                                        <Items>
                                                            <f:Label ID="Label12" runat="server" Text="现场机具、安全设施检查验收数量">
                                                            </f:Label>
                                                            <f:NumberBox ID="txtEquipmentNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server">
                                                            </f:NumberBox>
                                                            <f:NumberBox ID="txtYearEquipmentNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server">
                                                            </f:NumberBox>
                                                        </Items>
                                                    </f:FormRow>
                                                </Rows>
                                            </f:Form>
                                        </Items>
                                    </f:GroupPanel>
                                    <f:GroupPanel ID="GroupPanel26" Layout="Anchor" Title="4.7.2 许可管理情况说明" runat="server">
                                        <Items>
                                            <f:TextArea runat="server" ID="txtLicenseRemark" Label="本月主要作业票类型及办理情况说明">
                                            </f:TextArea>
                                            <f:TextArea runat="server" ID="txtEquipmentRemark" Label="本月机具、安全设施检查验收情况说明">
                                            </f:TextArea>
                                        </Items>
                                    </f:GroupPanel>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel27" Layout="Anchor" Title="4.8 HSE奖励与处罚" runat="server">
                                <Items>
                                    <f:GroupPanel ID="GroupPanel28" Layout="Anchor" Title="4.8.1 HSE奖励情况统计一览表" runat="server">
                                        <Items>
                                            <f:Grid ID="gvRewardSort" ShowBorder="true" ShowHeader="false" Title="HSE奖励情况统计一览表"
                                                runat="server" AllowCellEditing="true" ClicksToEdit="1" DataIDField="IncentiveSortId"
                                                DataKeyNames="IncentiveSortId" EnableMultiSelect="false" ShowGridHeader="true"
                                                Height="220px" EnableColumnLines="true" AutoScroll="true" OnRowCommand="gvRewardSort_RowCommand">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar11" Position="Top" runat="server" ToolbarAlign="Right">
                                                        <Items>
                                                            <f:Button ID="btnNewRewardSort" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewRewardSort_Click">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                                        ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="300px" ColumnID="IncentiveUnit" DataField="IncentiveUnit" FieldType="String"
                                                        HeaderText="被奖励单位或被奖励人/所属单位" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox21">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="250px" ColumnID="IncentiveType" DataField="IncentiveType" FieldType="String"
                                                        HeaderText="奖励类型（现金或实物）" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox22">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="IncentiveDate" DataField="IncentiveDate" FieldType="String"
                                                        HeaderText="奖励时间" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox23">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="180px" ColumnID="IncentiveMoney" DataField="IncentiveMoney"
                                                        FieldType="String" HeaderText="折算金额合计（元）" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:NumberBox ID="NumberBox2" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                            <f:Form ID="Form7" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                                <Rows>
                                                    <f:FormRow ColumnWidths="22% 22% 28% 28%">
                                                        <Items>
                                                            <f:NumberBox ID="txtRewardNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                                Label="本月累计奖励次数" LabelWidth="150px" >
                                                            </f:NumberBox>
                                                            <f:NumberBox ID="txtYearRewardNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server" Label="年度累计奖励次数" LabelWidth="150px" >
                                                            </f:NumberBox>
                                                            <f:NumberBox ID="txtRewardMoney" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server" Label="本月累计奖励金额（元）" LabelWidth="180px" >
                                                            </f:NumberBox>
                                                            <f:NumberBox ID="txtYearRewardMoney" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server" Label="年度累计奖励金额（元）" LabelWidth="180px" >
                                                            </f:NumberBox>
                                                        </Items>
                                                    </f:FormRow>
                                                </Rows>
                                            </f:Form>
                                        </Items>
                                    </f:GroupPanel>
                                    <f:GroupPanel ID="GroupPanel29" Layout="Anchor" Title="4.8.2 HSE处罚情况统计一览表" runat="server">
                                        <Items>
                                            <f:Grid ID="gvPunishSort" ShowBorder="true" ShowHeader="false" Title="HSE处罚情况统计一览表"
                                                runat="server" AllowCellEditing="true" ClicksToEdit="1" DataIDField="IncentiveSortId"
                                                DataKeyNames="IncentiveSortId" EnableMultiSelect="false" ShowGridHeader="true"
                                                Height="220px" EnableColumnLines="true" AutoScroll="true" OnRowCommand="gvPunishSort_RowCommand">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar12" Position="Top" runat="server" ToolbarAlign="Right">
                                                        <Items>
                                                            <f:Button ID="btnNewPunishSort" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewPunishSort_Click">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                                        ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="300px" ColumnID="IncentiveUnit" DataField="IncentiveUnit" FieldType="String"
                                                        HeaderText="被处罚单位或被处罚人/所属单位" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox24">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="170px" ColumnID="IncentiveType" DataField="IncentiveType" FieldType="String"
                                                        HeaderText="处罚类型（现金或实物）" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox25">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="IncentiveDate" DataField="IncentiveDate" FieldType="String"
                                                        HeaderText="处罚时间" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox26">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="150px" ColumnID="IncentiveMoney" DataField="IncentiveMoney"
                                                        FieldType="String" HeaderText="折算金额合计（元）" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:NumberBox ID="NumberBox3" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="130px" ColumnID="IncentiveReason" DataField="IncentiveReason"
                                                        FieldType="String" HeaderText="处罚原因" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox27">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="150px" ColumnID="IncentiveBasis" DataField="IncentiveBasis"
                                                        FieldType="String" HeaderText="处罚依据" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox28">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                            <f:Form ID="Form8" ShowBorder="false" ShowHeader="false" AutoScroll="true" BodyPadding="10px"
                                                runat="server" RedStarPosition="BeforeText" LabelAlign="Right">
                                                <Rows>
                                                    <f:FormRow ColumnWidths="22% 22% 28% 28%">
                                                        <Items>
                                                            <f:NumberBox ID="txtPunishNum" NoDecimal="true" NoNegative="true" MinValue="0" runat="server"
                                                                Label="本月累计处罚次数" LabelWidth="150px">
                                                            </f:NumberBox>
                                                            <f:NumberBox ID="txtYearPunishNum" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server" Label="年度累计处罚次数" LabelWidth="150px">
                                                            </f:NumberBox>
                                                            <f:NumberBox ID="txtPunishMoney" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server" Label="本月累计处罚金额（元）" LabelWidth="180px">
                                                            </f:NumberBox>
                                                            <f:NumberBox ID="txtYearPunishMoney" NoDecimal="true" NoNegative="true" MinValue="0"
                                                                runat="server" Label="年度累计处罚金额（元）" LabelWidth="180px">
                                                            </f:NumberBox>
                                                        </Items>
                                                    </f:FormRow>
                                                </Rows>
                                            </f:Form>
                                        </Items>
                                    </f:GroupPanel>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel30" Layout="Anchor" Title="4.9 其他HSE管理活动" runat="server">
                                <Items>
                                    <f:GroupPanel ID="GroupPanel31" Layout="Anchor" Title="4.9.1 管理绩效数据统计" runat="server">
                                        <Items>
                                            <f:Grid ID="gvOtherActiveSort" ShowBorder="true" ShowHeader="false" Title="管理绩效数据统计"
                                                runat="server" AllowCellEditing="true" ClicksToEdit="1" DataIDField="OtherActiveSortId"
                                                DataKeyNames="OtherActiveSortId" EnableMultiSelect="false" ShowGridHeader="true"
                                                Height="220px" EnableColumnLines="true" AutoScroll="true" OnRowCommand="gvOtherActiveSort_RowCommand">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar13" Position="Top" runat="server" ToolbarAlign="Right">
                                                        <Items>
                                                            <f:Button ID="btnNewOtherActiveSort" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewOtherActiveSort_Click">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                                        ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="300px" ColumnID="ActiveName" DataField="ActiveName" FieldType="String"
                                                        HeaderText="活动名称" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox29">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="200px" ColumnID="Num" DataField="Num" FieldType="String" HeaderText="本月开展次数"
                                                        HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox30">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="200px" ColumnID="YearNum" DataField="YearNum" FieldType="String"
                                                        HeaderText="年度累计次数" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox31">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                        </Items>
                                    </f:GroupPanel>
                                    <f:GroupPanel ID="GroupPanel32" Layout="Anchor" Title="4.9.2 活动情况说明" runat="server">
                                        <Items>
                                            <f:Grid ID="gvActivityDes" ShowBorder="true" ShowHeader="false" Title="活动情况说明" runat="server"
                                                AllowCellEditing="true" ClicksToEdit="1" DataIDField="ActivityDesId" DataKeyNames="ActivityDesId"
                                                EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                                                AutoScroll="true" OnRowCommand="gvActivityDes_RowCommand">
                                                <Toolbars>
                                                    <f:Toolbar ID="Toolbar14" Position="Top" runat="server" ToolbarAlign="Right">
                                                        <Items>
                                                            <f:Button ID="btnNewActivityDes" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewActivityDes_Click">
                                                            </f:Button>
                                                        </Items>
                                                    </f:Toolbar>
                                                </Toolbars>
                                                <Columns>
                                                    <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                                        ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="300px" ColumnID="ActivityTheme" DataField="ActivityTheme" FieldType="String"
                                                        HeaderText="活动主题" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox32">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="100px" ColumnID="ActivityDate" DataField="ActivityDate" FieldType="String"
                                                        HeaderText="活动时间" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox33">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" FieldType="String"
                                                        HeaderText="参加单位" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox34">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="300px" ColumnID="EffectDes" DataField="EffectDes" FieldType="String"
                                                        HeaderText="活动效果描述" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox35">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                        </Items>
                                    </f:GroupPanel>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel33" Layout="Anchor" Title="4.10 HSE现场其他管理情况" runat="server">
                                <Items>
                                    <f:Grid ID="gvOtherManagement" ShowBorder="true" ShowHeader="false" Title="HSE现场其他管理情况"
                                        runat="server" AllowCellEditing="true" ClicksToEdit="1" DataIDField="OtherManagementId"
                                        DataKeyNames="OtherManagementId" EnableMultiSelect="false" ShowGridHeader="true"
                                        Height="220px" EnableColumnLines="true" AutoScroll="true" OnRowCommand="gvOtherManagement_RowCommand">
                                        <Toolbars>
                                            <f:Toolbar ID="Toolbar131" Position="Top" runat="server" ToolbarAlign="Right">
                                                <Items>
                                                    <f:Button ID="btnNewOtherManagement" ToolTip="新增" Icon="Add" runat="server" OnClick="btnNewOtherManagement_Click">
                                                    </f:Button>
                                                </Items>
                                            </f:Toolbar>
                                        </Toolbars>
                                        <Columns>
                                            <f:LinkButtonField Width="40px" ConfirmText="删除选中行？" ConfirmTarget="Top" CommandName="Delete"
                                                ToolTip="删除" Icon="Delete" TextAlign="Center" />
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="300px" ColumnID="ManagementDes" DataField="ManagementDes" FieldType="String"
                                                HeaderText="管理内容描述" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox291">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                        </Items>
                    </f:GroupPanel>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>
                    <f:Button ID="btnSave" Icon="SystemSave" runat="server" ToolTip="保存" ValidateForms="SimpleForm1"
                        OnClick="btnSave_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>
    </form>
</body>
</html>
