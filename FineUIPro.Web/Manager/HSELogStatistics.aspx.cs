using System;
using System.Linq;
using System.Web.UI;
using BLL;
using System.Data;

namespace FineUIPro.Web.Manager
{
    public partial class HSELogStatistics : PageBase
    {
        #region 加载
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpCompileMan.DataValueField = "UserId";
                this.drpCompileMan.DataTextField = "UserName";
                this.drpCompileMan.DataSource = BLL.UserService.GetProjectUserListByProjectId(this.CurrUser.LoginProjectId);
                this.drpCompileMan.DataBind();
                Funs.FineUIPleaseSelect(this.drpCompileMan);

                this.drpCompileMan.SelectedValue = this.CurrUser.UserId;
                this.txtStartDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(-1));
                this.txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                if (this.CurrUser.Account == Const.adminAccount)
                {
                    this.drpCompileMan.Enabled = true;
                }
                else
                {
                    this.drpCompileMan.Enabled = false;
                }
            }
        }
        #endregion

        #region 统计
        /// <summary>
        /// 统计按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnStatistics_Click(object sender, EventArgs e)
        {
            this.gvHSELog.DataSource = this.GetDataTable();
            this.gvHSELog.DataBind();
        }
        #endregion

        #region 创建DataTable
        /// <summary>
        ///  创建DataTable
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataTable()
        {
            DataTable outputDT = new DataTable();
            if (this.drpCompileMan.SelectedValue != BLL.Const._Null && !string.IsNullOrEmpty(this.txtStartDate.Text) && !string.IsNullOrEmpty(this.txtEndDate.Text))
            {
                outputDT.Columns.Add("序号", typeof(string));
                outputDT.Columns.Add("类别", typeof(string));
                outputDT.Columns.Add("填写要求", typeof(string));

                var hseLogDate = from x in Funs.DB.Manager_HSSELog
                                 where x.ProjectId == this.CurrUser.LoginProjectId && x.CompileMan == this.drpCompileMan.SelectedValue && x.CompileDate >= Convert.ToDateTime(this.txtStartDate.Text) && x.CompileDate <= Convert.ToDateTime(this.txtEndDate.Text)
                                 orderby x.CompileDate
                                 select new { x.CompileDate, x.HSSELogId, x.Weather };
                foreach (var incol in hseLogDate)
                {
                    outputDT.Columns.Add(string.Format("{0:yyyy-MM-dd}", incol.CompileDate), typeof(string));
                }
                DataRow row = outputDT.NewRow();
                row["序号"] = string.Empty;
                row["类别"] = string.Empty;
                row["填写要求"] = string.Empty;
                foreach (var item in hseLogDate)
                {
                    var w1 = Funs.DB.Sys_Const.FirstOrDefault(x => x.GroupId == BLL.ConstValue.Group_Weather && x.ConstValue == item.Weather);
                    if (w1 != null)
                    {
                        row[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = "天气：" + w1.ConstText;
                    }
                    else
                    {
                        row[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = "天气：";
                    }
                }
                outputDT.Rows.Add(row);

                DataRow row10 = outputDT.NewRow();
                row10["序号"] = "一";
                row10["类别"] = "HSE绩效数据统计";
                row10["填写要求"] = "重点记录HSE管理的几个主要数据";
                foreach (var item in hseLogDate)
                {
                    row10[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = string.Empty;
                }
                outputDT.Rows.Add(row10);

                DataRow row11 = outputDT.NewRow();
                row11["序号"] = "1";
                row11["类别"] = "人工日统计";
                row11["填写要求"] = "每日所管辖责任区内的人工日统计情况";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d1 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d1 != null)
                    {
                        row11[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d1.Num11 != null ? d1.Num11.ToString() : string.Empty;
                    }
                }
                outputDT.Rows.Add(row11);

                DataRow row12 = outputDT.NewRow();
                row12["序号"] = "2";
                row12["类别"] = "不安全行为绩效统计";
                row12["填写要求"] = " 不安全行为指数：不安全行书指数=（不安全行为数/审核小时数）×100%";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d2 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d2 != null)
                    {
                        row12[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d2.Contents12;
                    }
                }
                outputDT.Rows.Add(row12);

                DataRow row13 = outputDT.NewRow();
                row13["序号"] = "3";
                row13["类别"] = "事故及未遂事件情况统计";
                row13["填写要求"] = "事故及未遂事件情况统计";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d3 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d3 != null)
                    {
                        row13[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d3.Contents13;
                    }
                }
                outputDT.Rows.Add(row13);

                DataRow row20 = outputDT.NewRow();
                row20["序号"] = "二";
                row20["类别"] = "HSE现场管理";
                row20["填写要求"] = "重点描述完成的工作内容";
                foreach (var item in hseLogDate)
                {
                    row20[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = string.Empty;
                }
                outputDT.Rows.Add(row20);

                DataRow row21 = outputDT.NewRow();
                row21["序号"] = "1";
                row21["类别"] = "HSE检查类型";
                row21["填写要求"] = "描述是日巡检或××专项检查，参加人员";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d4 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d4 != null)
                    {
                        row21[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d4.Contents21;
                    }
                }
                outputDT.Rows.Add(row21);

                DataRow row22 = outputDT.NewRow();
                row22["序号"] = string.Empty;
                row22["类别"] = "检查次数";
                row22["填写要求"] = "各类检查的次数，日巡检计1次；次数同存档文件对应";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d5 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d5 != null)
                    {
                        row22[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d5.Num21 != null ? d5.Num21.ToString() : string.Empty;
                    }
                }
                outputDT.Rows.Add(row22);

                DataRow row23 = outputDT.NewRow();
                row23["序号"] = "2";
                row23["类别"] = "隐患整改情况";
                row23["填写要求"] = "存在的隐患、整改要求及安排";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d6 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d6 != null)
                    {
                        row23[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d6.Contents22;
                    }
                }
                outputDT.Rows.Add(row23);

                DataRow row24 = outputDT.NewRow();
                row24["序号"] = string.Empty;
                row24["类别"] = "隐患整改数量";
                row24["填写要求"] = "今日督促整改，并且已经整改完成的隐患数量，同存档文件对应";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d7 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d7 != null)
                    {
                        row24[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d7.Num22 != null ? d7.Num22.ToString() : string.Empty;
                    }
                }
                outputDT.Rows.Add(row24);

                DataRow row25 = outputDT.NewRow();
                row25["序号"] = "3";
                row25["类别"] = "作业许可情况";
                row25["填写要求"] = "各类作业许可证办理、检查工作情况";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d8 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d8 != null)
                    {
                        row25[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d8.Contents23;
                    }
                }
                outputDT.Rows.Add(row25);

                DataRow row26 = outputDT.NewRow();
                row26["序号"] = string.Empty;
                row26["类别"] = "作业票数量";
                row26["填写要求"] = "今日经手办理的各类作业许可证数量，同存档文件对应";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d9 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d9 != null)
                    {
                        row26[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d9.Num23 != null ? d9.Num23.ToString() : string.Empty;
                    }
                }
                outputDT.Rows.Add(row26);

                DataRow row27 = outputDT.NewRow();
                row27["序号"] = "4";
                row27["类别"] = "施工机具、安全设施检查、验收情况";
                row27["填写要求"] = "各类施工机具、安全设施的检查、检验等工作，包括施工机械报审";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d10 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d10 != null)
                    {
                        row27[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d10.Contents24;
                    }
                }
                outputDT.Rows.Add(row27);

                DataRow row28 = outputDT.NewRow();
                row28["序号"] = string.Empty;
                row28["类别"] = "检查验收数量";
                row28["填写要求"] = "各类施工机具、安全设施的检查数量，同存档文件对应";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d11 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d11 != null)
                    {
                        row28[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d11.Num24 != null ? d11.Num24.ToString() : string.Empty;
                    }
                }
                outputDT.Rows.Add(row28);

                //DataRow row29 = outputDT.NewRow();
                //row29["序号"] = "5";
                //row29["类别"] = "危险源辨识工作情况";
                //row29["填写要求"] = "对危险源的动态识别工作情况，重点描述工作内容及成果";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d12 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d12 != null)
                //    {
                //        row29[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d12.Contents25;
                //    }
                //}
                //outputDT.Rows.Add(row29);

                //DataRow row200 = outputDT.NewRow();
                //row200["序号"] = string.Empty;
                //row200["类别"] = "危险源辨识活动次数（同存档文件对应）";
                //row200["填写要求"] = "开展的危险源辨识活动次数，同危险源辨识记录存档数量相对应";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d13 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d13 != null)
                //    {
                //        row200[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d13.Num25 != null ? d13.Num25.ToString() : string.Empty;
                //    }
                //}
                //outputDT.Rows.Add(row200);

                //DataRow row201 = outputDT.NewRow();
                //row201["序号"] = "6";
                //row201["类别"] = "应急计划修编、演练及物资准备情况";
                //row201["填写要求"] = "各类应急计划的编制、升版工作情况，预案演练活动情况，应急物资准备情况等。";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d14 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d14 != null)
                //    {
                //        row201[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d14.Contents26;
                //    }
                //}
                //outputDT.Rows.Add(row201);

                //DataRow row202 = outputDT.NewRow();
                //row202["序号"] = string.Empty;
                //row202["类别"] = "应急活动次数（同存档文件对应）";
                //row202["填写要求"] = "开展的应急预案修编、演练等活动次数，同存档文件对应";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d15 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d15 != null)
                //    {
                //        row202[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d15.Num26 != null ? d15.Num26.ToString() : string.Empty;
                //    }
                //}
                //outputDT.Rows.Add(row202);

                //DataRow row203 = outputDT.NewRow();
                //row203["序号"] = "7";
                //row203["类别"] = "HSE教育培训情况";
                //row203["填写要求"] = "次数、参与人员、内容、课时等";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d16 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d16 != null)
                //    {
                //        row203[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d16.Contents27;
                //    }
                //}
                //outputDT.Rows.Add(row203);

                //DataRow row204 = outputDT.NewRow();
                //row204["序号"] = string.Empty;
                //row204["类别"] = "HSE培训人次";
                //row204["填写要求"] = "参加各类HSE培训的人次，同存档文件对应";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d17 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d17 != null)
                //    {
                //        row204[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d17.Num27 != null ? d17.Num27.ToString() : string.Empty;
                //    }
                //}
                //outputDT.Rows.Add(row204);

                //DataRow row205 = outputDT.NewRow();
                //row205["序号"] = "8";
                //row205["类别"] = "HSE会议情况";
                //row205["填写要求"] = "类型、主题、参与方等";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d18 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d18 != null)
                //    {
                //        row205[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d18.Contents28;
                //    }
                //}
                //outputDT.Rows.Add(row205);

                //DataRow row206 = outputDT.NewRow();
                //row206["序号"] = string.Empty;
                //row206["类别"] = "HSE会议次数";
                //row206["填写要求"] = "召开的各类HSE会议的数量";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d19 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d19 != null)
                //    {
                //        row206[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d19.Num28 != null ? d19.Num28.ToString() : string.Empty;
                //    }
                //}
                //outputDT.Rows.Add(row206);

                //DataRow row207 = outputDT.NewRow();
                //row207["序号"] = "9";
                //row207["类别"] = "HSE宣传工作情况";
                //row207["填写要求"] = "与HSE相关的各类宣传活动进行情况";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d20 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d20 != null)
                //    {
                //        row207[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d20.Contents29;
                //    }
                //}
                //outputDT.Rows.Add(row207);

                //DataRow row208 = outputDT.NewRow();
                //row208["序号"] = string.Empty;
                //row208["类别"] = "HSE宣传活动次数";
                //row208["填写要求"] = "开展的各类HSE宣传活动的数量";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d21 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d21 != null)
                //    {
                //        row208[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d21.Num29 != null ? d21.Num29.ToString() : string.Empty;
                //    }
                //}
                //outputDT.Rows.Add(row208);

                DataRow row209 = outputDT.NewRow();
                row209["序号"] = "5";
                row209["类别"] = "HSE奖惩工作情况";
                row209["填写要求"] = "对不安全行为的违章处罚，对优秀员工的奖励";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d22 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d22 != null)
                    {
                        row209[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d22.Contents210;
                    }
                }
                outputDT.Rows.Add(row209);

                DataRow row210 = outputDT.NewRow();
                row210["序号"] = string.Empty;
                row210["类别"] = "HSE奖励次数";
                row210["填写要求"] = "HSE奖励的数量（每奖励队伍一次计1次，奖励人员按人次计算），同存档文件对应";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d23 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d23 != null)
                    {
                        row210[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d23.Num210 != null ? d23.Num210.ToString() : string.Empty;
                    }
                }
                outputDT.Rows.Add(row210);

                DataRow row211 = outputDT.NewRow();
                row211["序号"] = string.Empty;
                row211["类别"] = "HSE处罚次数";
                row211["填写要求"] = "HSE处罚的数量（每处罚队伍一次计1次，处罚人员按人次计算），同存档文件对应";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d24 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d24 != null)
                    {
                        row211[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d24.Num211 != null ? d24.Num211.ToString() : string.Empty;
                    }
                }
                outputDT.Rows.Add(row211);

                DataRow row30 = outputDT.NewRow();
                row30["序号"] = "三";
                row30["类别"] = "HSE内业管理";
                row30["填写要求"] = "重点描述完成的工作内容";
                foreach (var item in hseLogDate)
                {
                    row30[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = string.Empty;
                }
                outputDT.Rows.Add(row30);

                DataRow row31 = outputDT.NewRow();
                row31["序号"] = "1";
                row31["类别"] = "HSE文件修编情况";
                row31["填写要求"] = "各类HSE实施计划、方案、措施等的编制、审核，包括分包商的HSE体系文件审核工作";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d25 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d25 != null)
                    {
                        row31[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d25.Contents31;
                    }
                }
                outputDT.Rows.Add(row31);

                DataRow row32 = outputDT.NewRow();
                row32["序号"] = string.Empty;
                row32["类别"] = "HSE文件修编数量";
                row32["填写要求"] = "HSE体系文件修编、审核的数量，同存档文件对应";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d26 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d26 != null)
                    {
                        row32[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d26.Num31 != null ? d26.Num31.ToString() : string.Empty;
                    }
                }
                outputDT.Rows.Add(row32);

                DataRow row33 = outputDT.NewRow();
                row33["序号"] = "2";
                row33["类别"] = "HSE文件审核情况";
                row33["填写要求"] = "五环公司及各分包商企业、人员资质核查、HSE费用核查等";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d27 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d27 != null)
                    {
                        row33[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d27.Contents32;
                    }
                }
                outputDT.Rows.Add(row33);

                DataRow row34 = outputDT.NewRow();
                row34["序号"] = string.Empty;
                row34["类别"] = "HSE文件审核数量";
                row34["填写要求"] = "五环公司及各分包商企业、人员资质核查的数量，同存档文件对应";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d28 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d28 != null)
                    {
                        row34[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d28.Num32 != null ? d28.Num32.ToString() : string.Empty;
                    }
                }
                outputDT.Rows.Add(row34);

                //DataRow row35 = outputDT.NewRow();
                //row35["序号"] = "3";
                //row35["类别"] = "HSE费用使用、审核情况";
                //row35["填写要求"] = "HSE费用发生核查、申请审核等方面的工作情况";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d29 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d29 != null)
                //    {
                //        row35[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d29.Contents33;
                //    }
                //}
                //outputDT.Rows.Add(row35);

                //DataRow row36 = outputDT.NewRow();
                //row36["序号"] = string.Empty;
                //row36["类别"] = "HSE费用核查次数";
                //row36["填写要求"] = "HSE费用的核查次数，每核查或审核一次就计1次，但需同存档文件对应";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d30 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d30 != null)
                //    {
                //        row36[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d30.Num33 != null ? d30.Num33.ToString() : string.Empty;
                //    }
                //}
                //outputDT.Rows.Add(row36);

                //DataRow row37 = outputDT.NewRow();
                //row37["序号"] = "4";
                //row37["类别"] = "文件资料归档数量";
                //row37["填写要求"] = "归档的各类文件数量";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d31 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d31 != null)
                //    {
                //        row37[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d31.Num34 != null ? d31.Num34.ToString() : string.Empty;
                //    }
                //}
                //outputDT.Rows.Add(row37);

                DataRow row40 = outputDT.NewRow();
                row40["序号"] = "四";
                row40["类别"] = "每周总结";
                row40["填写要求"] = "重点描述完成的工作内容";
                foreach (var item in hseLogDate)
                {
                    row40[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = string.Empty;
                }
                outputDT.Rows.Add(row40);

                DataRow row41 = outputDT.NewRow();
                row41["序号"] = "1";
                row41["类别"] = "每周工作小结";
                row41["填写要求"] = "对本周的工作要点进行总结";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d32 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d32 != null)
                    {
                        row41[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d32.Contents41;
                    }
                }
                outputDT.Rows.Add(row41);

                DataRow row42 = outputDT.NewRow();
                row42["序号"] = "2";
                row42["类别"] = "下周/下阶段工作计划";
                row42["填写要求"] = "提出下周或下阶段的工作要点";
                foreach (var item in hseLogDate)
                {
                    Model.Manager_HSSELog d33 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                    if (d33 != null)
                    {
                        row42[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d33.Contents42;
                    }
                }
                outputDT.Rows.Add(row42);

                //DataRow row43 = outputDT.NewRow();
                //row43["序号"] = "3";
                //row43["类别"] = "其它";
                //row43["填写要求"] = string.Empty;
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d34 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d34 != null)
                //    {
                //        row43[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d34.Contents43;
                //    }
                //}
                //outputDT.Rows.Add(row43);

                //DataRow row50 = outputDT.NewRow();
                //row50["序号"] = "五";
                //row50["类别"] = "总结";
                //row50["填写要求"] = "重点描述完成的工作内容";
                //foreach (var item in hseLogDate)
                //{
                //    row50[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = string.Empty;
                //}
                //outputDT.Rows.Add(row50);

                //DataRow row51 = outputDT.NewRow();
                //row51["序号"] = "1";
                //row51["类别"] = "当日工作小结";
                //row51["填写要求"] = "对今日的工作要点进行总结";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d35 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d35 != null)
                //    {
                //        row51[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d35.Contents51;
                //    }
                //}
                //outputDT.Rows.Add(row51);

                //DataRow row52 = outputDT.NewRow();
                //row52["序号"] = "2";
                //row52["类别"] = "明日/下阶段工作计划";
                //row52["填写要求"] = "提出明日或下阶段的工作要点";
                //foreach (var item in hseLogDate)
                //{
                //    Model.Manager_HSSELog d36 = BLL.HSSELogService.GetHSSELogByHSSELogId(item.HSSELogId);
                //    if (d36 != null)
                //    {
                //        row52[string.Format("{0:yyyy-MM-dd}", item.CompileDate)] = d36.Contents52;
                //    }
                //}
                //outputDT.Rows.Add(row52);
            }

            return outputDT;
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, EventArgs e)
        {
            DataTable thisTable = this.GetDataTable();
            if (thisTable != null)
            {
                this.gvHSELog.DataSource = this.GetDataTable();
                this.gvHSELog.DataBind();

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");

                string filename = this.drpCompileMan.SelectedItem.Text + this.txtStartDate.Text + "至" + this.txtEndDate.Text + "HSSE日志暨管理数据收集";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ".xls");
                Response.ContentType = "application/ms-excel";
                this.EnableViewState = false;
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

                this.gvHSELog.RenderControl(oHtmlTextWriter);
                Response.Write(oStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        #endregion
    }
}