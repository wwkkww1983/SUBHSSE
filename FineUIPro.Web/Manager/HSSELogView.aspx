<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HSSELogView.aspx.cs" Inherits="FineUIPro.Web.Manager.HSSELogView" ValidateRequest="false" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查看HSSE日志暨管理数据收集</title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
     </head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="SimpleForm1"/>    
    <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" Title="HSE日志暨管理数据收集" AutoScroll="true" 
        BodyPadding="10px" runat="server" RedStarPosition="BeforeText" LabelAlign="Right" Layout="VBox" Width="990px">
        <Rows>            
            <f:FormRow>
                <Items>
                    <f:Label ID="txtCompileDate" runat="server" Label="日志日期" LabelAlign="Right">
                    </f:Label>
                    <f:Label ID="drpCompileMan" runat="server" Label="编制人" LabelAlign="Right">
                    </f:Label>
                    <f:Label ID="drpWeather" runat="server" Label="天气" LabelAlign="Right" >
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label1" runat="server" Text="序号" ></f:Label>
                    <f:Label ID="Label2" runat="server" Text="类别" ></f:Label>
                    <f:Label ID="Label3" runat="server" Text="填写要求"></f:Label>
                    <f:Label ID="Label4" runat="server" Text="内容"></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label5" runat="server" Text="一"></f:Label>
                    <f:Label ID="Label6" runat="server" Text="HSE绩效数据统计"></f:Label>  
                    <f:Label ID="Label10" runat="server" Text="重点记录HSE管理的几个主要数据"></f:Label>
                   <f:Label runat="server" ></f:Label>
                </Items>
            </f:FormRow>   
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label7" runat="server" Text="1"></f:Label>
                    <f:Label ID="Label8" runat="server" Text="人工日统计"></f:Label>  
                    <f:Label ID="Label9" runat="server" Text="每日所管辖责任区内的人工日统计情况"></f:Label>
                   <f:Label ID="txtNum11" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
             <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label11" runat="server" Text="2"></f:Label>
                    <f:Label ID="Label12" runat="server" Text="不安全行为绩效统计"></f:Label>  
                    <f:Label ID="Label13" runat="server" Text="依据《施工现场不安全行为管理绩效》每日统计，注明被考核单位今日得分"></f:Label>
                   <f:Label ID="txtContents12" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label14" runat="server" Text="3"></f:Label>
                    <f:Label ID="Label15" runat="server" Text="事故情况统计"></f:Label>  
                    <f:Label ID="Label16" runat="server" Text="依据填写事故发生情况"></f:Label>
                   <f:Label ID="txtContents13" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label17" runat="server" Text="二"></f:Label>
                    <f:Label ID="Label18" runat="server" Text="HSE现场管理"></f:Label>  
                    <f:Label ID="Label19" runat="server" Text="重点描述完成的工作内容"></f:Label>
                   <f:Label ID="Label20" runat="server" ></f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label21" runat="server" Text="1"></f:Label>
                    <f:Label ID="Label22" runat="server" Text="HSE检查情况"></f:Label>  
                    <f:Label ID="Label23" runat="server" Text="检查类型、参与人员及检查的基本情况"></f:Label>
                   <f:Label ID="txtContents21" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label24" runat="server" ></f:Label>
                    <f:Label ID="Label25" runat="server" Text="检查次数"></f:Label>  
                    <f:Label ID="Label26" runat="server" Text="各类检查的次数，日巡检计1次；次数同存档文件对应"></f:Label>
                   <f:Label ID="txtNum21" runat="server" LabelAlign="Left"  ></f:Label>
                </Items>
            </f:FormRow>   
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label27" runat="server" Text="2"></f:Label>
                    <f:Label ID="Label28" runat="server" Text="隐患整改情况"></f:Label>  
                    <f:Label ID="Label29" runat="server" Text="存在的隐患类型、整改要求及安排"></f:Label>
                   <f:Label ID="txtContents22" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label30" runat="server" ></f:Label>
                    <f:Label ID="Label31" runat="server" Text="隐患整改数量"></f:Label>  
                    <f:Label ID="Label32" runat="server" Text="今日督促整改，并且已经整改完成的隐患数量，同存档文件对应"></f:Label>
                   <f:Label ID="txtNum22" runat="server" LabelAlign="Left"  ></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label33" runat="server" Text="3"></f:Label>
                    <f:Label ID="Label34" runat="server" Text="作业许可情况"></f:Label>  
                    <f:Label ID="Label35" runat="server" Text="各类作业许可证办理、检查工作情况"></f:Label>
                   <f:Label ID="txtContents23" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label36" runat="server" ></f:Label>
                    <f:Label ID="Label37" runat="server" Text="作业票数量"></f:Label>  
                    <f:Label ID="Label38" runat="server" Text="今日经手办理的各类作业许可证数量，同存档文件对应"></f:Label>
                   <f:Label ID="txtNum23" runat="server" LabelAlign="Left"  ></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label39" runat="server" Text="4"></f:Label>
                    <f:Label ID="Label40" runat="server" Text="施工机具、安全设施检查、验收情况"></f:Label>  
                    <f:Label ID="Label41" runat="server" Text="各类施工机具、安全设施的检查、检验等工作，包括施工机械报审"></f:Label>
                   <f:Label ID="txtContents24" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label42" runat="server" ></f:Label>
                    <f:Label ID="Label43" runat="server" Text="检查验收数量"></f:Label>  
                    <f:Label ID="Label44" runat="server" Text="各类施工机具、安全设施的检查数量，同存档文件对应"></f:Label>
                   <f:Label ID="txtNum24" runat="server" LabelAlign="Left"  ></f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label45" runat="server" Text="5"></f:Label>
                    <f:Label ID="Label46" runat="server" Text="危险源辨识工作情况"></f:Label>  
                    <f:Label ID="Label47" runat="server" Text="对危险源的动态识别工作情况，重点描述工作内容及成果"></f:Label>
                   <f:Label ID="txtContents25" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label48" runat="server" ></f:Label>
                    <f:Label ID="Label49" runat="server" Text="危险源辨识活动次数（同存档文件对应）"></f:Label>  
                    <f:Label ID="Label50" runat="server" Text="开展的危险源辨识活动次数，同危险源辨识记录存档数量相对应"></f:Label>
                   <f:Label ID="txtNum25" runat="server" LabelAlign="Left"  ></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label51" runat="server" Text="6"></f:Label>
                    <f:Label ID="Label52" runat="server" Text="应急计划修编、演练及物资准备情况"></f:Label>  
                    <f:Label ID="Label53" runat="server" Text="各类应急计划的编制、升版工作情况，预案演练活动情况，应急物资准备情况等。"></f:Label>
                   <f:Label ID="txtContents26" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label54" runat="server" ></f:Label>
                    <f:Label ID="Label55" runat="server" Text="应急活动次数（同存档文件对应）"></f:Label>  
                    <f:Label ID="Label56" runat="server" Text="开展的应急预案修编、演练等活动次数，同存档文件对应"></f:Label>
                   <f:Label ID="txtNum26" runat="server" LabelAlign="Left"  ></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label57" runat="server" Text="7"></f:Label>
                    <f:Label ID="Label58" runat="server" Text="HSE教育培训情况"></f:Label>  
                    <f:Label ID="Label59" runat="server" Text="次数、参与人员、内容、课时等"></f:Label>
                   <f:Label ID="txtContents27" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label60" runat="server" ></f:Label>
                    <f:Label ID="Label61" runat="server" Text="HSE培训人次"></f:Label>  
                    <f:Label ID="Label62" runat="server" Text="参加各类HSE培训的人次，同存档文件对应"></f:Label>
                   <f:Label ID="txtNum27" runat="server" LabelAlign="Left"  ></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label63" runat="server" Text="8"></f:Label>
                    <f:Label ID="Label64" runat="server" Text="HSE会议情况"></f:Label>  
                    <f:Label ID="Label65" runat="server" Text="类型、主题、参与方等"></f:Label>
                   <f:Label ID="txtContents28" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label66" runat="server" ></f:Label>
                    <f:Label ID="Label67" runat="server" Text="HSE会议次数"></f:Label>  
                    <f:Label ID="Label68" runat="server" Text="召开的各类HSE会议的数量"></f:Label>
                   <f:Label ID="txtNum28" runat="server" LabelAlign="Left" ></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label69" runat="server" Text="9"></f:Label>
                    <f:Label ID="Label70" runat="server" Text="HSE宣传工作情况"></f:Label>  
                    <f:Label ID="Label71" runat="server" Text="与HSE相关的各类宣传活动进行情况"></f:Label>
                   <f:Label ID="txtContents29" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label72" runat="server" ></f:Label>
                    <f:Label ID="Label73" runat="server" Text="HSE宣传活动次数"></f:Label>  
                    <f:Label ID="Label74" runat="server" Text="开展的各类HSE宣传活动的数量"></f:Label>
                   <f:Label ID="txtNum29" runat="server" LabelAlign="Left"  ></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label75" runat="server" Text="10"></f:Label>
                    <f:Label ID="Label76" runat="server" Text="HSE奖惩工作情况"></f:Label>  
                    <f:Label ID="Label77" runat="server" Text="对不安全行为的违章处罚，对优秀员工的奖励"></f:Label>
                   <f:Label ID="txtContents210" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label78" runat="server" ></f:Label>
                    <f:Label ID="Label79" runat="server" Text="HSE奖励次数"></f:Label>  
                    <f:Label ID="Label80" runat="server" Text="HSE奖励的数量（每奖励队伍一次计1次，奖励人员按人次计算），同存档文件对应"></f:Label>
                   <f:Label ID="txtNum210" runat="server" LabelAlign="Left"  ></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label81" runat="server" ></f:Label>
                    <f:Label ID="Label82" runat="server" Text="HSE处罚次数"></f:Label>  
                    <f:Label ID="Label83" runat="server" Text="HSE处罚的数量（每处罚队伍一次计1次，处罚人员按人次计算），同存档文件对应"></f:Label>
                   <f:Label ID="txtNum211" runat="server" LabelAlign="Left"  ></f:Label>
                </Items>
            </f:FormRow>  
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label84" runat="server" Text="三"></f:Label>
                    <f:Label ID="Label85" runat="server" Text="HSE内业管理"></f:Label>  
                    <f:Label ID="Label86" runat="server" Text="重点描述完成的工作内容"></f:Label>
                   <f:Label ID="Label87" runat="server" ></f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label88" runat="server" Text="1"></f:Label>
                    <f:Label ID="Label89" runat="server" Text="HSE体系文件修编情况"></f:Label>  
                    <f:Label ID="Label90" runat="server" Text="各类HSE实施计划、方案、措施等的编制、审核，包括分包商的HSE体系文件审核工作"></f:Label>
                   <f:Label ID="txtContents31" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label91" runat="server" ></f:Label>
                    <f:Label ID="Label92" runat="server" Text="HSE体系文件修编数量"></f:Label>  
                    <f:Label ID="Label93" runat="server" Text="HSE体系文件修编、审核的数量，同存档文件对应"></f:Label>
                   <f:Label ID="txtNum31" runat="server" LabelAlign="Left"  ></f:Label>
                </Items>
            </f:FormRow>  
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label94" runat="server" Text="2"></f:Label>
                    <f:Label ID="Label95" runat="server" Text="HSE资质、方案核查工作情况"></f:Label>  
                    <f:Label ID="Label96" runat="server" Text="本公司及各分包商企业、人员资质核查等"></f:Label>
                   <f:Label ID="txtContents32" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label97" runat="server" ></f:Label>
                    <f:Label ID="Label98" runat="server" Text="HSE资质、方案核查数量"></f:Label>  
                    <f:Label ID="Label99" runat="server" Text="本公司及各分包商企业、人员资质核查的数量，同存档文件对应"></f:Label>
                   <f:Label ID="txtNum32" runat="server" LabelAlign="Left"  ></f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label100" runat="server" Text="3"></f:Label>
                    <f:Label ID="Label101" runat="server" Text="HSE费用使用、审核情况"></f:Label>  
                    <f:Label ID="Label102" runat="server" Text="HSE费用发生核查、申请审核等方面的工作情况"></f:Label>
                   <f:Label ID="txtContents33" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label103" runat="server" ></f:Label>
                    <f:Label ID="Label104" runat="server" Text="HSE费用核查次数"></f:Label>  
                    <f:Label ID="Label105" runat="server" Text="HSE费用的核查次数，每核查或审核一次就计1次，但需同存档文件对应"></f:Label>
                   <f:Label ID="txtNum33" runat="server" LabelAlign="Left"  ></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label106" runat="server" Text="4"></f:Label>
                    <f:Label ID="Label107" runat="server" Text="文件资料归档数量"></f:Label>  
                    <f:Label ID="Label108" runat="server" Text="归档的各类文件数量"></f:Label>
                   <f:Label ID="txtNum34" runat="server" LabelAlign="Left"  ></f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label109" runat="server" Text="四"></f:Label>
                    <f:Label ID="Label110" runat="server" Text="其它管理"></f:Label>  
                    <f:Label ID="Label111" runat="server" Text="重点描述完成的工作内容"></f:Label>
                   <f:Label ID="Label112" runat="server" ></f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label113" runat="server" Text="1"></f:Label>
                    <f:Label ID="Label114" runat="server" Text="HSE工程师工作安排"></f:Label>  
                    <f:Label ID="Label115" runat="server" Text="HSE经理填写，对工程师的工作安排"></f:Label>
                   <f:Label ID="txtContents41" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label116" runat="server" Text="2"></f:Label>
                    <f:Label ID="Label117" runat="server" Text="治安保卫工作情况"></f:Label>  
                    <f:Label ID="Label118" runat="server" Text="项目治安保卫工作情况"></f:Label>
                   <f:Label ID="txtContents42" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label119" runat="server" Text="3"></f:Label>
                    <f:Label ID="Label120" runat="server" Text="其它"></f:Label>  
                    <f:Label ID="Label121" runat="server" ></f:Label>
                   <f:Label ID="txtContents43" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label122" runat="server" Text="五"></f:Label>
                    <f:Label ID="Label123" runat="server" Text="总结"></f:Label>  
                    <f:Label ID="Label124" runat="server" Text="重点描述完成的工作内容"></f:Label>
                   <f:Label ID="Label125" runat="server" ></f:Label>
                </Items>
            </f:FormRow> 
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label126" runat="server" Text="1"></f:Label>
                    <f:Label ID="Label127" runat="server" Text="当日工作小结"></f:Label>  
                    <f:Label ID="Label128" runat="server" Text="对今日的工作要点进行总结"></f:Label>
                   <f:Label ID="txtContents51" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow>
            <f:FormRow ColumnWidths="5% 15% 30% 50%">
                <Items>
                    <f:Label ID="Label129" runat="server" Text="2"></f:Label>
                    <f:Label ID="Label130" runat="server" Text="明日/下阶段工作计划"></f:Label>  
                    <f:Label ID="Label131" runat="server" Text="提出明日或下阶段的工作要点"></f:Label>
                   <f:Label ID="txtContents52" runat="server" LabelAlign="Left" >
                    </f:Label>
                </Items>
            </f:FormRow>
        </Rows>
        <Toolbars>
            <f:Toolbar ID="Toolbar1" Position="Bottom" ToolbarAlign="Right" runat="server">
                <Items>                                      
                    <f:ToolbarFill runat="server"> </f:ToolbarFill>                
                    <f:Button ID="btnClose" EnablePostBack="false" ToolTip="关闭" runat="server" Icon="SystemClose">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
    </f:Form>    
    <f:Window ID="WindowAtt" Title="弹出窗体" Hidden="true" EnableIFrame="true" EnableMaximize="true"
        Target="Parent" EnableResize="true" runat="server" IsModal="true" Width="700px"
        Height="500px">
    </f:Window>
    </form>
</body>
</html>
