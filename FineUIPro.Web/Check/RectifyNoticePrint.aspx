<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RectifyNoticePrint.aspx.cs"
    Inherits="FineUIPro.Web.Check.RectifyNoticePrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function pagesetup_null() {
            try {
                var RegWsh = new ActiveXObject("WScript.Shell")
                hkey_key = "header"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
                hkey_key = "footer"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
            } catch (e) { }
        }
        //设置网页打印的页眉页脚为默认值
        function pagesetup_default() {
            try {
                var RegWsh = new ActiveXObject("WScript.Shell")
                hkey_key = "header"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "&w&b页码，&p/&P")
                hkey_key = "footer"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "&u&b&d")
            } catch (e) { }
        }
        function printpr() //预览函数
        {
            pagesetup_null(); //预览之前去掉页眉，页脚
            document.getElementById("div1").style.display = "none";
            var a = window.open('about:blank', 'Print');
            a.document.write('<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>' + document.getElementById('Table5').innerHTML);
            //ar WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';
            a.document.all.WebBrowser1.ExecWB(7, 1);
            a.close();
            //document.body.insertAdjacentHTML('beforeEnd', WebBrowser); //在body标签内加入html（WebBrowser activeX控件）
            //WebBrowser1.ExecWB(7, 1); //打印预览
            //WebBrowser1.outerHTML = ""; //从代码中清除插入的html代码
            pagesetup_default(); //预览结束后页眉页脚恢复默认值
            document.getElementById("div1").style.display = "block";
        }
        function print() //打印函数
        {
            pagesetup_null(); //打印之前去掉页眉，页脚
            document.getElementById("div1").style.display = "none";

            var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';
            document.body.insertAdjacentHTML('beforeEnd', WebBrowser); //在body标签内加入html（WebBrowser activeX控件）
            WebBrowser1.ExecWB(6, 6); //打印
            WebBrowser1.outerHTML = ""; //从代码中清除插入的html代码
            pagesetup_default(); //打印结束后页眉页脚恢复默认值
            document.getElementById("div1").style.display = "block";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table2" runat="server" width="100%" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td style="width: 100%; background: url('../Images/bg-1.gif'); border-width: 0px;
                border-width: 0px;">
                <div id="div1">
                    <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" valign="middle" style="width: 50%; font-size: 11pt; font-weight: bold;
                                border-width: 0px;">
                                &nbsp;
                            </td>
                            <td align="right" valign="middle" style="width: 50%; height: 30px; border-width: 0px;
                                border-width: 0px;">
                                <img src="../Images/PageSetup.gif" runat="server" id="Img2" onclick="document.all.WebBrowser.ExecWB(8,1)"
                                    alt="页面设置" style="cursor: pointer" />
                                <img src="../Images/PrintSetup.gif" runat="server" id="btnPrint" onclick="document.all.WebBrowser.ExecWB(6,1)"
                                    alt="打印设置" style="cursor: pointer" />
                                <img src="../Images/PrintPreview.gif" runat="server" id="Img1" onclick="printpr()"
                                    alt="打印预览" style="cursor: pointer" />
                                <img src="../Images/Print.gif" runat="server" id="Img3" onclick="print()" alt="打印"
                                    style="cursor: pointer" />
                            </td>
                            <td align="right" valign="middle" style="width: 50%; height: 30px; border-width: 0px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table id="Table5" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
        bordercolor="#000000">
        <tr>
            <td>
                <table id="Table11" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                    bordercolor="#000000">
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="Label1" runat="server" Font-Size="22px" Font-Bold="true" Text="隐患整改及处理通知单"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 50%; height: 45px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label4" runat="server" Font-Size="15px" Text="编号："></asp:Label>
                            <asp:Label ID="txtRectifyNoticeCode" runat="server" Font-Size="15px"></asp:Label>&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0" border="0"
                    frame="vsides" bordercolor="#000000">
                    <tr style="height: 32px;">
                        <td align="center" style="border: 1px solid #000000; border-bottom: none; border-right: none;">
                            <asp:Label ID="txt111" runat="server" Font-Size="15px" Text="发往单位"></asp:Label>
                        </td>
                        <td align="left" style="border: 1px solid #000000; border-bottom: none; border-right: none;">
                            &nbsp;<asp:Label ID="txtUnitName" runat="server" Font-Size="15px"></asp:Label>
                        </td>
                        <td align="center" style="border: 1px solid #000000; border-bottom: none; border-right: none;">
                            <asp:Label ID="Label6" runat="server" Font-Size="15px" Text="接收人"></asp:Label>
                        </td>
                        <td align="left" style="border: 1px solid #000000; border-bottom: none; border-right: none;">
                            &nbsp;<asp:Label ID="txtDutyPerson1" runat="server" Font-Size="15px"></asp:Label>
                        </td>
                        <td align="center" style="border: 1px solid #000000; border-bottom: none; border-right: none;">
                            <asp:Label ID="txt1aa" runat="server" Font-Size="15px" Text="日期"></asp:Label>
                        </td>
                        <td align="left" style="border: 1px solid #000000; border-bottom: none;">
                            &nbsp;<asp:Label ID="txtCheckedDate" runat="server" Font-Size="15px"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 32px">
                        <td colspan="6" align="left" style="border: 1px solid #000000; border-bottom: none;">
                            <asp:Label ID="txtUnitNameProject" runat="server" Font-Size="15px"></asp:Label><br />
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label9" runat="server" Font-Size="15px" Text="在"></asp:Label><asp:Label
                                ID="txtCheckedDate2" runat="server" Font-Size="15px"></asp:Label>
                            <asp:Label ID="Label8" runat="server" Font-Size="15px" Text="TCC对你单位施工安全检查中，发现贵单位施工作业中存在以下问题："></asp:Label><br />
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="txtWrongContent" runat="server" Font-Size="15px"></asp:Label><br />
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label10" runat="server" Font-Size="15px" Text="以上问题必须在整改内容要求的时间内整改完毕，并附照片回复。"></asp:Label><br />
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label11" runat="server" Font-Size="15px" Text="请贵单位高度重视项目安全管理，自觉维护施工形象，保证施工安全。"></asp:Label><br />
                            <br />
                            <asp:Label ID="Label12" runat="server" Font-Size="15px" Text="签发人："></asp:Label>
                            <asp:Label ID="txtSignPerson" runat="server" Font-Size="15px"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label13" runat="server" Font-Size="15px" Text="批准人："></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label14" runat="server" Font-Size="15px" Text="日期："></asp:Label>
                            <asp:Label ID="txtSignDate" runat="server" Font-Size="15px"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 32px">
                        <td colspan="6" align="left" style="border: 1px solid #000000; border-bottom: none;">
                            <asp:Label ID="tadda" runat="server" Font-Size="15px" Text="下栏内容由整改单位填写，并在以上整改内容要求的时间内，将此表交TCC HSE工程师，否则将加重处罚。"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 32px">
                        <td colspan="6" align="left" style="border: 1px solid #000000; border-bottom: none;">
                            <asp:Label ID="Label15" runat="server" Font-Size="15px" Text="施工单位回复整改措施和完成情况："></asp:Label><br />
                            <asp:Label ID="txtCompleteStatus" runat="server" Font-Size="15px"></asp:Label><br />
                            <asp:Label ID="Label17" runat="server" Font-Size="15px" Text="整改负责人："></asp:Label>
                            <asp:Label ID="txtDutyPerson2" runat="server" Font-Size="15px"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label21" runat="server" Font-Size="15px" Text="日期："></asp:Label>
                            <asp:Label ID="txtCompleteDate" runat="server" Font-Size="15px"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 32px">
                        <td colspan="6" align="left" style="border: 1px solid #000000; border-bottom: none;">
                            <asp:Label ID="Label18" runat="server" Font-Size="15px" Text="TCC检查结果："></asp:Label><br />
                            <br />
                            <br />
                            <br />
                            <asp:Label ID="Label20" runat="server" Font-Size="15px" Text="检查负责人："></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label23" runat="server" Font-Size="15px" Text="日期："></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 32px">
                        <td colspan="6" align="left" style="border: 1px solid #000000; border-bottom: none;">
                            <asp:Label ID="Label19" runat="server" Font-Size="15px" Text="TCC根据上述情况，对违规单位作如下处理："></asp:Label><br />
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="免予处罚" /><br />
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="批评和警告" /><br />
                            <asp:CheckBox ID="CheckBox3" runat="server" Text="向违规单位上级通报" /><br />
                            <asp:CheckBox ID="CheckBox4" runat="server" Text="罚款人民币___________元" /><br />
                            <asp:CheckBox ID="CheckBox5" runat="server" Text="其它" /><br />
                            <br />
                            <asp:Label ID="Label22" runat="server" Font-Size="15px" Text="签发人："></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label25" runat="server" Font-Size="15px" Text="批准人："></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label27" runat="server" Font-Size="15px" Text="日期："></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 32px">
                        <td colspan="6" align="left" style="border: 1px solid #000000; border-bottom: none; border-left: none; border-right: none;">
                            <div runat="server" id="div3">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <object id="WebBrowser" width="0" height="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2">
    </object>
    </form>
</body>
</html>
