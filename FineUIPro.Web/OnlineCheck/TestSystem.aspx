<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSystem.aspx.cs" Inherits="FineUIPro.Web.OnlineCheck.TestSystem" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../res/css/common.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .chk
        {
            font-weight: bold;
            font-size: large;
            letter-spacing: 40px;
             
        }
         .rdo
        {
            font-weight: bold;
            font-size: large;
            width:300px;padding-rigth:30px 
        }
        
        .Lable
        {
            font-weight: bold;
            font-size: 1.5em;
            color: Blue;
            text-align: center;
        }
        
        .group
        {
            font-weight: bold;
            font-size: 1.2em;
            color: Blue;
        }
        .blue
        {
            font-size: 6em;
            color: Blue;
        }
        .redP
        {
            font-size: 6em;
            color:Red;
        }
    </style>
</head>
<body oncontextmenu="return false;"   onselectstart="return false">
    <form id="form1" runat="server" marginleft="30px">
     <f:PageManager ID="PageManager1" OnCustomEvent="PageManager1_CustomEvent" runat="server" />
        <f:Form ID="Form2" runat="server" Height="530px" Width="1024px" ShowBorder="True"  CssClass="mytable"
            Layout="Table" TableConfigColumns="2" ShowHeader="True"
            LabelAlign="Right" LabelWidth="120px">
            <Items>
                <f:Panel ID="Panel1" Title="Panel1" Width="840px" Height="340px" TableRowspan="2"
                    runat="server" BodyPadding="5px" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:GroupPanel ID="GroupPanel1" CssClass="group" Layout="Region"  runat="server"
                            Height="320px">
                            <Items>
                                <f:Image ID="imgTestContent" MarginLeft="5px" ShowEmptyLabel="true" runat="server" />
                            </Items>
                        </f:GroupPanel>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel3"  Width="180px" Height="250px" 
                    runat="server" BodyPadding="5px" ShowBorder="true" ShowHeader="false">
                    <Items>
                       <f:Label ID="lblScore" runat="server" MarginTop="100px" MarginLeft="25px"></f:Label>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel4"  Width="200px" Height="50px"
                    runat="server" BodyPadding="5px" ShowBorder="false" ShowHeader="false">
                    <Items>
                       <f:Button ID="btnEnd" Text="提交试卷查看最后分数"  Height="30px" MarginLeft="10px"  runat="server" OnClick="btnEnd_Click">
                        </f:Button>
                    </Items>
                </f:Panel>
            </Items>
            <Items>
                <f:Panel runat="server"  TableColspan="2" Layout="HBox"  ShowHeader="false" ShowBorder="false">
                    <Items>
                        <f:CheckBoxList ID="chbAnswer" runat="server" CssClass="chk"  MarginLeft="30px"></f:CheckBoxList>
                        <f:RadioButtonList ID="rdbAnswer" runat="server"  MarginLeft="30px" ></f:RadioButtonList>
                        <f:Button ID="btnSubmit" Text="提交" Icon="accept" runat="server" OnClick="btnSubmit_Click"></f:Button>
                        <f:Button ID="btnFirst" Text="第一题" Icon="resultsetfirst" runat="server" OnClick="btnFirst_Click"></f:Button>
                        <f:Button ID="btnPrevious" Text="前一题" Icon="resultsetprevious" runat="server" OnClick="btnPrevious_Click"></f:Button>
                        <f:Button ID="btnNext" Text="后一题" Icon="resultsetnext" runat="server" OnClick="btnNext_Click"></f:Button>
                        <f:Button ID="btnLast" Text="最后一题" Icon="resultsetlast" runat="server" OnClick="btnLast_Click"></f:Button>
                         <f:NumberBox ID="txtskip" runat="server" MarginLeft="30px"  Width="80px" ></f:NumberBox>
                         <f:Button ID="btnGo" Text="GO"  runat="server" OnClick="btnGo_Click"></f:Button>
                    </Items>
                </f:Panel>
            </Items>
            <Items>
                  <f:Panel ID="Panel5" runat="server"  MarginTop="30px" TableColspan="4" Layout="HBox"  ShowHeader="false" ShowBorder="false">
                    <Items>
                        <f:Label runat="server" Text="开始时间" Width="180px" CssClass="Lable" ></f:Label>
                        <f:Label runat="server" Text="结束时间" Width="180px" CssClass="Lable" ></f:Label>
                        <f:Label  runat="server" Text="当前时间" Width="180px" CssClass="Lable" ></f:Label>
                        <f:Label  runat="server" Text="剩余时间" Width="180px" CssClass="Lable" ></f:Label>
                    </Items>
                </f:Panel>
            </Items>
            <Items>
                  <f:Panel ID="Panel2" runat="server"  TableColspan="4" Layout="HBox"   ShowHeader="false" ShowBorder="false">
                    <Items>
                        <f:Timer ID="Timer1" Interval="10" Enabled="false" OnTick="Timer1_Tick" EnableAjaxLoading="false" runat="server"></f:Timer>
                        <f:Label id="lblStartTime" runat="server"  Width="180px" CssClass="Lable" ></f:Label>
                        <f:Label id="lblEndTime" runat="server" Width="180px" CssClass="Lable" ></f:Label>
                        <f:Label id="lblCurretTime" runat="server" Width="180px" CssClass="Lable"></f:Label>
                        <f:Label id="lblLeave" runat="server"  Width="180px" CssClass="Lable" ></f:Label>
                    </Items>
                </f:Panel>
            </Items>
        </f:Form>

    </form>
</body>
</html>
