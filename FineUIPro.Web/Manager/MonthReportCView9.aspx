<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonthReportCView9.aspx.cs" Inherits="FineUIPro.Web.Manager.MonthReportCView9" %>

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
                    <f:GroupPanel ID="GroupPanel48" Layout="Anchor" Title="9.下月工作计划" runat="server">
                        <Items>
                            <f:GroupPanel ID="GroupPanel49" Layout="Anchor" Title="9.1 危险源动态识别及控制" runat="server">
                                <Items>
                                    <f:Grid ID="gvHazard" ShowBorder="true" ShowHeader="false" Title="其他工作情况" runat="server"
                                        AllowCellEditing="false" ClicksToEdit="1" DataIDField="HazardId" DataKeyNames="HazardId"
                                        EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                                        AutoScroll="true" >
                                        
                                        <Columns>
                                            
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="250px" ColumnID="WorkArea" DataField="WorkArea" FieldType="String"
                                                HeaderText="下月计划施工的区域" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox49">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="250px" ColumnID="Subcontractor" DataField="Subcontractor" FieldType="String"
                                                HeaderText="所属分包商" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox50">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="250px" ColumnID="DangerousSource" DataField="DangerousSource"
                                                FieldType="String" HeaderText="可能存在的危险源" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox51">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="250px" ColumnID="ControlMeasures" DataField="ControlMeasures"
                                                ExpandUnusedSpace="true" FieldType="String" HeaderText="计划采取的控制措施" HeaderTextAlign="Center"
                                                TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox52">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel50" Layout="Anchor" Title="9.2 HSE培训" runat="server">
                                <Items>
                                    <f:Grid ID="gvTrain" ShowBorder="true" ShowHeader="false" Title="HSE培训" runat="server"
                                        AllowCellEditing="false" ClicksToEdit="1" DataIDField="TrainId" DataKeyNames="TrainId"
                                        EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                                        AutoScroll="true" >
                                       
                                        <Columns>
                                          
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="300px" ColumnID="TrainName" DataField="TrainName" FieldType="String"
                                                HeaderText="下月计划开展的培训名称" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox53">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="120px" ColumnID="TrainMan" DataField="TrainMan" FieldType="String"
                                                HeaderText="培训人" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox55">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="120px" ColumnID="TrainDate" DataField="TrainDate" FieldType="String"
                                                HeaderText="培训时间" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox56">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="TrainObject" DataField="TrainObject" FieldType="String"
                                                HeaderText="计划参培对象" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox54">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                                HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox57">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel51" Layout="Anchor" Title="9.3 HSE检查" runat="server">
                                <Items>
                                    <f:Grid ID="gvCheck" ShowBorder="true" ShowHeader="false" Title="HSE检查" runat="server"
                                        AllowCellEditing="false" ClicksToEdit="1" DataIDField="CheckId" DataKeyNames="CheckId"
                                        EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                                        AutoScroll="true" >
                                        
                                        <Columns>
                                           
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="300px" ColumnID="CheckType" DataField="CheckType" FieldType="String"
                                                HeaderText="下月计划开展的检查类型" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox58">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="Inspectors" DataField="Inspectors" FieldType="String"
                                                HeaderText="参检人员" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox59">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="CheckDate" DataField="CheckDate" FieldType="String"
                                                HeaderText="计划开展时间" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox60">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                                HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox62">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel52" Layout="Anchor" Title="9.4 HSE会议" runat="server">
                                <Items>
                                    <f:Grid ID="gvMeeting" ShowBorder="true" ShowHeader="false" Title="HSE会议" runat="server"
                                        AllowCellEditing="false" ClicksToEdit="1" DataIDField="MeetingId" DataKeyNames="MeetingId"
                                        EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                                        AutoScroll="true" >
                                       
                                        <Columns>
                                           
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="300px" ColumnID="MeetingTitle" DataField="MeetingTitle" FieldType="String"
                                                HeaderText="下月计划召开的会议主题" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox61">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="MeetingDate" DataField="MeetingDate" FieldType="String"
                                                HeaderText="会议时间" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox63">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="Host" DataField="Host" FieldType="String"
                                                HeaderText="主持人" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox64">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="Participants" DataField="Participants" FieldType="String"
                                                HeaderText="计划参会人员" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox65">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel53" Layout="Anchor" Title="9.5 HSE活动（包含宣传活动及其他活动）" runat="server">
                                <Items>
                                    <f:Grid ID="gvActivities" ShowBorder="true" ShowHeader="false" Title="HSE活动" runat="server"
                                        AllowCellEditing="false" ClicksToEdit="1" DataIDField="ActivitiesId" DataKeyNames="ActivitiesId"
                                        EnableMultiSelect="false" ShowGridHeader="true" Height="220px" EnableColumnLines="true"
                                        AutoScroll="true" >
                                       
                                        <Columns>
                                          
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="300px" ColumnID="ActivitiesTitle" DataField="ActivitiesTitle"
                                                FieldType="String" HeaderText="计划开展的活动主题" HeaderTextAlign="Center" TextAlign="Left"
                                                ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox66">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="ActivitiesDate" DataField="ActivitiesDate"
                                                FieldType="String" HeaderText="活动时间" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox67">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="Unit" DataField="Unit" FieldType="String"
                                                HeaderText="参加单位" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox68">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="200px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                                HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox69">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel54" Layout="Anchor" Title="9.6 应急管理" runat="server">
                                <Items>
                                    <f:GroupPanel ID="GroupPanel55" Layout="Anchor" Title="9.6.1 应急预案修编" runat="server">
                                        <Items>
                                            <f:Grid ID="gvEmergencyPlan" ShowBorder="true" ShowHeader="false" Title="应急预案修编"
                                                runat="server" AllowCellEditing="false" ClicksToEdit="1" DataIDField="EmergencyPlanId"
                                                DataKeyNames="EmergencyPlanId" EnableMultiSelect="false" ShowGridHeader="true"
                                                Height="220px" EnableColumnLines="true" AutoScroll="true" >
                                               
                                                <Columns>
                                                   
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="300px" ColumnID="EmergencyName" DataField="EmergencyName" FieldType="String"
                                                        HeaderText="计划修编的预案名称" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox70">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="150px" ColumnID="CompileMan" DataField="CompileMan" FieldType="String"
                                                        HeaderText="修编人员" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox71">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="150px" ColumnID="CompileDate" DataField="CompileDate" FieldType="String"
                                                        HeaderText="计划完成时间" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox72">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="200px" ColumnID="Remark" DataField="Remark" FieldType="String"
                                                        HeaderText="备注" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox73">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                        </Items>
                                    </f:GroupPanel>
                                    <f:GroupPanel ID="GroupPanel56" Layout="Anchor" Title="9.6.2 应急演练活动" runat="server">
                                        <Items>
                                            <f:Grid ID="gvEmergencyExercises" ShowBorder="true" ShowHeader="false" Title="应急演练活动"
                                                runat="server" AllowCellEditing="false" ClicksToEdit="1" DataIDField="EmergencyExercisesId"
                                                DataKeyNames="EmergencyExercisesId" EnableMultiSelect="false" ShowGridHeader="true"
                                                Height="220px" EnableColumnLines="true" AutoScroll="true" >
                                                
                                                <Columns>
                                                
                                                    <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                        TextAlign="Center" />
                                                    <f:RenderField Width="300px" ColumnID="DrillContent" DataField="DrillContent" FieldType="String"
                                                        HeaderText="计划演练内容" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox74">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="150px" ColumnID="DrillDate" DataField="DrillDate" FieldType="String"
                                                        HeaderText="计划演练时间" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox75">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="250px" ColumnID="UnitName" DataField="UnitName" FieldType="String"
                                                        HeaderText="参加单位" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:TextBox runat="server" ID="TextBox76">
                                                            </f:TextBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="200px" ColumnID="ParticipantsNum" DataField="ParticipantsNum"
                                                        FieldType="Int" HeaderText="参加人数" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:NumberBox ID="NumberBox6" NoDecimal="true" NoNegative="true" MinValue="0" runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:Grid>
                                        </Items>
                                    </f:GroupPanel>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel57" Layout="Anchor" Title="9.7 HSE费用投入计划" runat="server">
                                <Items>
                                    <f:Grid ID="gvCostInvestmentPlan" ShowBorder="true" ShowHeader="false" Title="HSE费用投入计划"
                                        runat="server" AllowCellEditing="false" ClicksToEdit="1" DataIDField="CostInvestmentPlanId"
                                        DataKeyNames="CostInvestmentPlanId" EnableMultiSelect="false" ShowGridHeader="true"
                                        Height="220px" EnableColumnLines="true" AutoScroll="true">
                                        <Columns>
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="200px" ColumnID="InvestmentProject" DataField="InvestmentProject"
                                                FieldType="String" HeaderText="投入项目" HeaderTextAlign="Center" TextAlign="Left"
                                                ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox77">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:GroupField EnableLock="true" HeaderText="计划费用（万元）" TextAlign="Center">
                                                <Columns>
                                                    <f:RenderField Width="200px" ColumnID="MainPlanCost" DataField="MainPlanCost" FieldType="String"
                                                        HeaderText="本单位" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:NumberBox ID="NumberBox7" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                    <f:RenderField Width="200px" ColumnID="SubPlanCost" DataField="SubPlanCost" FieldType="String"
                                                        HeaderText="施工分包商" HeaderTextAlign="Center" TextAlign="Left">
                                                        <Editor>
                                                            <f:NumberBox ID="NumberBox8" NoDecimal="false" NoNegative="true" MinValue="0" runat="server">
                                                            </f:NumberBox>
                                                        </Editor>
                                                    </f:RenderField>
                                                </Columns>
                                            </f:GroupField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel58" Layout="Anchor" Title="9.8 HSE管理文件/方案修编计划" runat="server">
                                <Items>
                                    <f:Grid ID="gvManageDocPlan" ShowBorder="true" ShowHeader="false" Title="HSE管理文件/方案修编计划"
                                        runat="server" AllowCellEditing="false" ClicksToEdit="1" DataIDField="ManageDocPlanId"
                                        DataKeyNames="ManageDocPlanId" EnableMultiSelect="false" ShowGridHeader="true"
                                        Height="220px" EnableColumnLines="true" AutoScroll="true" >
                                        
                                        <Columns>
                                           
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="400px" ColumnID="ManageDocPlanName" DataField="ManageDocPlanName"
                                                FieldType="String" HeaderText="下月计划修编的方案/文件名称" HeaderTextAlign="Center" TextAlign="Left"
                                                ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox78">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="CompileMan" DataField="CompileMan" FieldType="String"
                                                HeaderText="修编人" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox79">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                            <f:RenderField Width="150px" ColumnID="CompileDate" DataField="CompileDate" FieldType="String"
                                                HeaderText="计划完成时间" HeaderTextAlign="Center" TextAlign="Left">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox80">
                                                    </f:TextBox>
                                                </Editor>
                                            </f:RenderField>
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:GroupPanel>
                            <f:GroupPanel ID="GroupPanel59" Layout="Anchor" Title="9.9 其他HSE工作计划" runat="server">
                                <Items>
                                    <f:Grid ID="gvOtherWorkPlan" ShowBorder="true" ShowHeader="false" Title="其他HSE工作计划"
                                        runat="server" AllowCellEditing="false" ClicksToEdit="1" DataIDField="OtherWorkPlanId"
                                        DataKeyNames="OtherWorkPlanId" EnableMultiSelect="false" ShowGridHeader="true"
                                        Height="220px" EnableColumnLines="true" AutoScroll="true" >
                                        
                                        <Columns>
                                          
                                            <f:RowNumberField EnablePagingNumber="true" HeaderText="序号" Width="50px" HeaderTextAlign="Center"
                                                TextAlign="Center" />
                                            <f:RenderField Width="400px" ColumnID="WorkContent" DataField="WorkContent" FieldType="String"
                                                HeaderText="计划工作内容描述" HeaderTextAlign="Center" TextAlign="Left" ExpandUnusedSpace="true">
                                                <Editor>
                                                    <f:TextBox runat="server" ID="TextBox81">
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
    </f:Form>
    </form>
</body>
</html>
