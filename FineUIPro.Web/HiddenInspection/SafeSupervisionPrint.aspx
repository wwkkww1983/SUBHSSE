<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SafeSupervisionPrint.aspx.cs" Inherits="FineUIPro.Web.HiddenInspection.SafeSupervisionPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>基层安全承包点工作记录表</title>
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
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0" align="center"
        border="0">
        <tr>
            <td style="width: 100%; background: url('../../Images/bg-1.gif')">
                <div id="div1">
                    <table id="tabbtn" runat="server" width="100%" style="background: url('../../Images/bg-1.gif')"
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" valign="middle" style="width: 30%; font-size: 11pt; font-weight: bold">
                            </td>
                            <td align="right" valign="middle" style="width: 70%; height: 30px; border-width: 0px;">
                                <img src="../../Images/PageSetup.gif" runat="server" id="Img2" onclick="document.all.WebBrowser.ExecWB(8,1)"
                                    alt="页面设置" style="cursor: pointer" />
                                <img src="../../Images/PrintSetup.gif" runat="server" id="btnPrint" onclick="document.all.WebBrowser.ExecWB(6,1)"
                                    alt="打印设置" style="cursor: pointer" />
                                <img src="../../Images/PrintPreview.gif" runat="server" id="Img1" onclick="printpr()"
                                    alt="打印预览" style="cursor: pointer" />
                                <img src="../../Images/Print.gif" runat="server" id="Img3" onclick="print()" alt="打印"
                                    style="cursor: pointer" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table5" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0"
                    bordercolor="#000000">
                    <tr>
                        <td>
                            <table id="Table6" runat="server" width="100%" cellpadding="0" cellspacing="0" border="0"
                                bordercolor="#000000" bordercolordark="#000000" bordercolorlight="#000000">
                                <tr>
                                    <td colspan="4" align="center" style="vertical-align: middle;
                                        font-weight: bold; font-size: 21px;">
                                        基层安全承包点工作记录表
                                    </td>
                                </tr>
                                <tr align="left" style="height: 30px;">
                                   <td colspan="3">
                                  
                                   </td>
                                   <td align="right">
                                   编号： <asp:Label runat="server" ID="Label1"></asp:Label>
                                   </td>
                                </tr>
                                <tr style="height: 30px;">
                                   <td align="center" style="border: 1px solid #000000; border-right:none;">
                                   被承包单位：
                                   </td>
                                   <td align="left" style="border: 1px solid #000000; border-left:none;">
                                    <asp:Label runat="server" ID="lbProjectName"></asp:Label>
                                   </td>
                                   <td align="center" style="border: 1px solid #000000; border-right:none; border-left:none;">
                                   工作时间：
                                   </td>
                                   <td align="left" style="border: 1px solid #000000; border-left:none;">
                                    <asp:Label runat="server" ID="lbCheckDate"></asp:Label>
                                   </td>
                                </tr>
                                <tr style="height: 30px;">
                                   <td align="center" style="border: 1px solid #000000; border-top:none;">
                                        类别
                                   </td>
                                   <td colspan="3" align="center" style="border: 1px solid #000000; border-top:none; border-left:none;">
                                   <asp:Label runat="server" ID="lbCheckType"></asp:Label>
                                   </td>
                                </tr>
                                <tr style="height: 90px;">
                                   <td align="center" style="border: 1px solid #000000; border-top:none;">
                                        工作内容
                                   </td>
                                   <td colspan="3" align="center" style="border: 1px solid #000000; border-top:none; border-left:none;">
                                   <asp:Label runat="server" ID="lbRegisterTypesNames"></asp:Label>
                                   </td>
                                </tr>
                                <tr style="height: 90px;">
                                   <td align="center" style="border: 1px solid #000000; border-top:none;">
                                        发现问题
                                   </td>
                                   <td colspan="3" align="center" style="border: 1px solid #000000; border-top:none; border-left:none;">
                                   <asp:Label runat="server" ID="lbRegisterDefs"></asp:Label>
                                   </td>
                                </tr>
                                <tr style="height: 90px;">
                                   <td align="center" style="border: 1px solid #000000; border-top:none;">
                                        整改意见
                                   </td>
                                   <td colspan="3" align="center" style="border: 1px solid #000000; border-top:none; border-left:none;">
                                   <asp:Label runat="server" ID="lbRectifications"></asp:Label>
                                   </td>
                                </tr>
                                <tr style="height: 30px;">
                                   <td align="center" style="border: 1px solid #000000; border-top:none;">
                                        被承包单位确认
                                   </td>
                                   <td align="center" style="border: 1px solid #000000; border-top:none; border-left:none;">
                                   <asp:Label runat="server" ID="Label2"></asp:Label>
                                   </td>
                                   <td align="center" style="border: 1px solid #000000; border-top:none; border-left:none;">
                                        检查人
                                   </td>
                                   <td align="center" style="border: 1px solid #000000; border-top:none; border-left:none;">
                                   <asp:Label runat="server" ID="lbCheckMan"></asp:Label>
                                   </td>
                                </tr>
                                <tr>
                                   <td rowspan="3" align="center" style="border: 1px solid #000000; border-top:none;">
                                        整改意见落实情况
                                   </td>
                                   <td colspan="3" align="center" 
                                        style="border: 1px solid #000000; border-top:none; border-left:none; border-bottom:none;" 
                                        class="style1">
                                   <asp:Label runat="server" ID="lbReCheckStation"></asp:Label>
                                   </td>
                                </tr>
                                <tr style="height: 30px;">
                                   <td colspan="2" ></td>
                                   <td align="center" style="border-right: 1px solid #000000;">
                                   检查人：<asp:Label runat="server" ID="lbCheckManConfirm"></asp:Label>
                                   </td>
                                </tr>
                                <tr style="height: 30px;">
                                   <td colspan="2" style="border-bottom: 1px solid #000000;"></td>
                                   <td align="center" style="border-right: 1px solid #000000; border-bottom: 1px solid #000000;">
                                   日期：<asp:Label runat="server" ID="lbConfirmDate"></asp:Label>
                                   </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                    </td>
                                    <td style="width: 35%;">
                                    </td>
                                    <td style="width: 15%;">
                                    </td>
                                    <td style="width: 35%;">
                                    </td>
                                </tr>
                            </table>
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
