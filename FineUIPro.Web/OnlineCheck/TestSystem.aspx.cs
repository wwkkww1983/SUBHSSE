using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.OnlineCheck
{
    public partial class TestSystem : PageBase
    {
        Dictionary<int,string> answer = new Dictionary<int,string>() 
        {
          {1,"A"},
          {2,"B"},
          {3,"C"},
          {4,"D"},
          {5,"E"},
          {6,"F"}
        };

        /// <summary>
        /// 剩余时间
        /// </summary>
        public string LeaveTime
        {
            get
            {
                return (string)ViewState["LeaveTime"];
            }
            set
            {
                ViewState["LeaveTime"] = value;
            }
        }

        public int TestIndex
        {
            get
            {
                return (int)ViewState["TestIndex"];
            }
            set
            {
                ViewState["TestIndex"] = value;
            }
        }

        public DataTable DT
        {
            get
            {
                return (DataTable)ViewState["DT"];
            }
            set
            {
                ViewState["DT"] = value;
            }
        }

        private string rootPath = "~/FileUpload/Image/OnlineCheck/";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgTestContent.ImageUrl = Funs.RootPath + "Images\\Null.jpg";
                string userId = this.CurrUser.UserId;
                var q = BLL.ExamineeService.GetExaminee(userId);
                if (q != null)
                {
                    // 未提交试卷删除明细
                    if (q.IsChecked == false || q.IsChecked == null)
                    {
                        BtnEnabel(true);
                        BLL.ExamineeService.DeleteExamineeDetails(q.ExamineeId);
                    }
                    else
                    {
                        GetTotalScore();
                        BtnEnabel(false);
                        ShowNotify("您已完成考试!");
                        return;
                    }

                    var w = Funs.DB.Base_WorkPost.FirstOrDefault(y => y.WorkPostId == q.WorkPostId);
                    SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@WorkPostId",q.WorkPostId),
                        new SqlParameter("@ABVolume",q.ABVolume),
                    };
                    DT = SQLHelper.GetDataTableRunProc("sp_GetTest", parameter);
                    TestIndex = 0;

                    if (DT.Rows.Count > 0)
                    {
                        ShowTest(DT, TestIndex);
                    }

                    Form2.Title = "考生帐号：" + this.CurrUser.Account + "      考生姓名：" + this.CurrUser.UserName + "      所考岗位：" + w.WorkPostName + "      AB卷：" + q.ABVolume;
                    Timer1.Enabled = true;
                    LeaveTime = DateTime.Now.AddHours(1).ToString();
                    lblStartTime.Text = DateTime.Now.ToLongTimeString();
                    lblEndTime.Text = DateTime.Now.AddHours(1).ToLongTimeString();
                    lblCurretTime.Text = DateTime.Now.ToLongTimeString();
                    lblLeave.Text = "60:00";
                }
                else
                {
                    BtnEnabel(false);
                    ShowNotify("您不在考生中!");
                    return;
                }

            }
        }

        protected void PageManager1_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventArgument == "Confirm_OK")
            {
                EndSubmit();
            }
            else if (e.EventArgument == "Confirm_Cancel")
            {
                // AJAX回发
                ShowNotify("您已取消了提交试卷！");
            }
        }


        /// <summary>
        /// 显现试题
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="index"></param>
        private void ShowTest(DataTable tb,int index)
        {
            this.chbAnswer.Items.Clear();
            this.rdbAnswer.Items.Clear();
            rdbAnswer.CssClass = "chk";
            var q = BLL.ExamineeService.GetExaminee(this.CurrUser.UserId);
            bool isExist = BLL.ExamineeService.IsExistExamineeDetail(q.ExamineeId, Convert.ToInt32(DT.Rows[TestIndex]["TestCode"]));
            string str = "";
            if (isExist)
            {
                str = BLL.ExamineeService.GetExamineeAnswer(q.ExamineeId, Convert.ToInt32(DT.Rows[TestIndex]["TestCode"]));
            }

            if (index == 0)
            {
                this.btnPrevious.Enabled = false;
                this.btnNext.Enabled = true;
            }
            if (index > 0 && index < tb.Rows.Count - 1)
            {
                this.btnPrevious.Enabled = true;
                this.btnNext.Enabled = true;
            }
            if (index == tb.Rows.Count-1)
            {
                this.btnPrevious.Enabled = true;
                this.btnNext.Enabled = false;
            }

            if (tb.Rows[index]["ItemType"].ToString().Contains("单选"))
            {
                this.chbAnswer.Hidden = true;
                rdbAnswer.Hidden = false;
                for (int i = 1; i <= Convert.ToInt32(tb.Rows[index]["KeyNumber"]); i++)
                {
                    rdbAnswer.Items.Add(answer[i], answer[i]);
                    
                }
                rdbAnswer.SelectedValue = str;
            }
            if (tb.Rows[index]["ItemType"].ToString().Contains("多选"))
            {
                this.rdbAnswer.Hidden = true;
                chbAnswer.Hidden = false;
                for (int i = 1; i <= Convert.ToInt32(tb.Rows[index]["KeyNumber"]); i++)
                {
                    this.chbAnswer.Items.Add(answer[i], answer[i]);
                }

                if (!string.IsNullOrEmpty(str))
                {
                    string[] strGroup = str.Split(',');
                    chbAnswer.SelectedValueArray = strGroup;
                }
            }

            if (tb.Rows[index]["ItemType"].ToString().Contains("判断"))
            {
                this.rdbAnswer.Hidden = false;
                chbAnswer.Hidden = true;
                this.rdbAnswer.Items.Add("A、正确", "A");
                this.rdbAnswer.Items.Add("B、错误", "B");
                rdbAnswer.SelectedValue = str;
                rdbAnswer.CssClass = "rdo";
            }

            this.GroupPanel1.Title = tb.Rows[index]["ItemType"].ToString() + "(第" + tb.Rows[index]["TestCode"].ToString() + "题)  分数：" + tb.Rows[index]["TestScore"].ToString() + "分";
            imgTestContent.ImageUrl = rootPath + tb.Rows[index]["TestPath"].ToString();
        }
        
        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            lblCurretTime.Text = DateTime.Now.ToLongTimeString();

            int m = (Convert.ToDateTime(LeaveTime) - DateTime.Now).Minutes;
            int s = (Convert.ToDateTime(LeaveTime) - DateTime.Now).Seconds;
            lblLeave.Text = m.ToString() + ":" + s.ToString();

            if (DateTime.Now > Convert.ToDateTime(lblEndTime.Text))
            {
                EndSubmit();
            }
        }

        protected void btnEnd_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            var q = BLL.ExamineeService.GetExaminee(this.CurrUser.UserId);
            var details = BLL.ExamineeService.GetExamineeDetails(q.ExamineeId);
            var exams=from x in details select x.TestCode;

            var t = from x in Funs.DB.Edu_Online_Test
                    join y in Funs.DB.Edu_Online_TestCondition on x.TestConditionId equals y.TestConditionId
                    where y.WorkPostId == q.WorkPostId && y.ABVolume == q.ABVolume orderby x.TestCode
                    select x.TestCode;
           
            if (details.Count() > 0)
            {
                if (details.Count() < t.Count())
                {
                    foreach (var n in t)
                    {

                        if (!exams.Contains(n))
                        {
                            str = str + n + ",";
                            
                        }
                    }

                    str = str.Substring(0, str.Length - 1);
                    str = "您还有" + str + "题未答,点击确定按钮提交，点击取消按钮检查后再提交";
                }

                else
                {
                    str = "试题已答完，点击确定按钮提交，点击取消按钮检查后再提交";
                }
            }
            else
            {
                str = "您还未答题，点击确定按钮提交，点击取消按钮检查后再提交";
            }

            PageContext.RegisterStartupScript(Confirm.GetShowReference(str,
            String.Empty,
            MessageBoxIcon.Question,
            PageManager1.GetCustomEventReference(false, "Confirm_OK"), // 第一个参数 false 用来指定当前不是AJAX请求
            PageManager1.GetCustomEventReference("Confirm_Cancel")));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (TestIndex < DT.Rows.Count)
            {
                var q = BLL.ExamineeService.GetExaminee(this.CurrUser.UserId);
                Model.Edu_Online_ExamineeDetail test = new Model.Edu_Online_ExamineeDetail
                {
                    ExamineeId = q.ExamineeId,
                    TestCode = Convert.ToInt32(DT.Rows[TestIndex]["TestCode"]),
                    TestType = DT.Rows[TestIndex]["TestType"].ToString(),
                    ItemType = DT.Rows[TestIndex]["ItemType"].ToString(),
                    TestScore = Convert.ToInt32(DT.Rows[TestIndex]["TestScore"]),
                    TestKey = DT.Rows[TestIndex]["TestKey"].ToString()
                };

                if (DT.Rows[TestIndex]["ItemType"].ToString().Contains("单选") || DT.Rows[TestIndex]["ItemType"].ToString().Contains("判断"))
                {
                    if (rdbAnswer.SelectedValue != null)
                    {
                        test.AnswerKey = rdbAnswer.SelectedValue;
                    }
                }
                else
                {
                    if (chbAnswer.SelectedValueArray.Length > 0)
                    {
                        test.AnswerKey = GetArrayString(chbAnswer.SelectedValueArray);
                    }
                }

                if (BLL.ExamineeService.IsExistExamineeDetail(q.ExamineeId, test.TestCode))
                {
                    BLL.ExamineeService.UpdateExamineeDetail(q.ExamineeId, test.TestCode, test.AnswerKey);
                }
                else
                {
                    BLL.ExamineeService.AddExamineeDetail(test);
                }

                TestIndex++;
            }
            else
            {
                ShowNotify("已是最后一题了！");
                return;
            }
            
            // 提交后进入下一题
            if (TestIndex < DT.Rows.Count)
            {
                ShowTest(DT, TestIndex);
            }
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            TestIndex = 0;
            ShowTest(DT, TestIndex);
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            TestIndex--;
            if (TestIndex >= 0)
            {
                ShowTest(DT, TestIndex);
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            TestIndex++;
            if (TestIndex < DT.Rows.Count)
            {
                ShowTest(DT, TestIndex);
            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            TestIndex = DT.Rows.Count - 1;
            ShowTest(DT, TestIndex);
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.txtskip.Text) <= DT.Rows.Count)
            {
                TestIndex = Convert.ToInt32(this.txtskip.Text) - 1;
                ShowTest(DT, TestIndex);
            }
        }

        private string GetArrayString(string[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in array)
            {
                sb.Append(item);
                sb.Append(",");
            }
            return sb.ToString().TrimEnd(',');
        }

        private void BtnEnabel(bool isEnabel)
        {
            this.btnFirst.Enabled = isEnabel;
            this.btnPrevious.Enabled = isEnabel;
            this.btnNext.Enabled = isEnabel;
            this.btnLast.Enabled = isEnabel;
            this.btnSubmit.Enabled = isEnabel;
            this.btnGo.Enabled = isEnabel;
            this.btnEnd.Enabled = isEnabel;
        }

        private int? GetTotalScore()
        {
            var q = BLL.ExamineeService.GetExaminee(this.CurrUser.UserId);
            var details = BLL.ExamineeService.GetExamineeDetails(q.ExamineeId);
            int? totalScore = 0;

            if (details.Count() > 0)
            {
                foreach (var d in details)
                {
                    if (d.TestKey == d.AnswerKey)
                    {
                        totalScore = totalScore + (d.TestScore != null ? d.TestScore : 0);
                    }
                }

            }

            this.lblScore.Text = totalScore.ToString();
            if (totalScore < 60)
            {
                this.lblScore.CssClass = "redP";
            }
            else
            {
                this.lblScore.CssClass = "blue";
            }

            return totalScore;
        }

        /// <summary>
        /// 考完提交
        /// </summary>
        private void EndSubmit()
        {
            var q = BLL.ExamineeService.GetExaminee(this.CurrUser.UserId);
            int? s = GetTotalScore();

            DateTime startTime = Convert.ToDateTime(lblStartTime.Text);
            DateTime endTime = DateTime.Now;
            BLL.ExamineeService.UpdateExamineeCheck(q.ExamineeId, s, startTime, endTime);
            GetTotalScore();
            BtnEnabel(false);
            ShowNotify("您已完成考试!");
        }
    }
}