using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FineUIPro.Web.EduTrain
{
    public partial class TestRecordPrint : PageBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string TestRecordId
        {
            get
            {
                return (string)ViewState["TestRecordId"];
            }
            set
            {
                ViewState["TestRecordId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.TestRecordId = Request.Params["TestRecordId"];
                var testRecord = BLL.TestRecordService.GetTestRecordById(this.TestRecordId);
                if (testRecord != null)
                {
                    if (!string.IsNullOrEmpty(testRecord.TestType))
                    {
                        this.Namea = testRecord.TestType + "          ";
                    }
                    var testMan = BLL.PersonService.GetPersonByUserId(testRecord.TestManId);
                    if (testMan != null)
                    {
                        this.Namea += "考生：" + UnitService.GetUnitNameByUnitId(testMan.UnitId) + "  " + testMan.PersonName + "。              ";
                    }
                    this.Namea += "时间：" + string.Format("{0:yyyy-MM-dd HH:mm}", testRecord.TestStartTime) + " 至 "+ string.Format("{0:yyyy-MM-dd HH:mm}", testRecord.TestEndTime)+"   ";
                    if (testRecord.TestScores.HasValue)
                    {
                        this.Namea += "成绩：" + testRecord.TestScores.ToString() + "          ";
                    }
                    var attachFile = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == this.TestRecordId);
                    if (attachFile != null && !string.IsNullOrEmpty(attachFile.AttachUrl))
                    {
                        List<string> listUrl = Funs.GetStrListByStr(attachFile.AttachUrl, ',');
                        int count = listUrl.Count();
                        if (count > 0)
                        {
                            this.timg1.Src = "../" + listUrl[0];
                            this.timg2.Src = "../" + listUrl[0];
                            if (count >= 2)
                            {
                                int cout2 = count / 2;
                                this.timg2.Src = "../" + listUrl[cout2];
                            }
                            this.timg3.Src = "../" + listUrl[count - 1];
                        }
                    }                   
                }
                this.BindGrid();
            }
        }

        protected string Namea = string.Empty;

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGrid()
        {
            string strSql = @"SELECT TestRecordItemId,TestRecordId,TrainingItemName,Abstracts,AttachUrl"
                         + @",('A. '+AItem) AS AItem,('B. '+BItem) AS BItem,(CASE WHEN CItem IS NOT NULL THEN ('C. '+CItem) ELSE NULL END) AS CItem"
                         + @",(CASE WHEN DItem IS NULL OR DItem='' THEN NULL ELSE 'D. '+DItem END) AS DItem,(CASE WHEN EItem IS NULL  OR EItem='' THEN NULL ELSE 'E. '+EItem END) AS EItem"
                         + @",'答案：'+Replace(Replace(Replace(Replace(Replace(AnswerItems,'1','A'),'2', 'B'),'3', 'C'),'4', 'D'),'5', 'E') AS AnswerItems"
                         + @" ,('分值：'+CAST(Score AS varchar(10))) AS Score,('得分：'+CAST(ISNULL(SubjectScore,0) AS varchar(10))) AS SubjectScore"
                         + @",(' ( '+ ISNULL(Replace(Replace(Replace(Replace(Replace(SelectedItem,'1','A'),'2', 'B'),'3', 'C'),'4', 'D'),'5', 'E'),'_') +' )') AS SelectedItem"
                         + @",TestType,TrainingItemCode,(CASE WHEN TestType = '1' THEN '单选题' WHEN TestType = '2' THEN '多选题' ELSE '判断题' END) AS TestTypeName"
                         + @" FROM Training_TestRecordItem "
                         + @" WHERE TestRecordId= '" + this.TestRecordId + "' ORDER BY TestType,TrainingItemCode";
            List<SqlParameter> listStr = new List<SqlParameter>();           

            SqlParameter[] parameter = listStr.ToArray();
            DataTable tb = SQLHelper.GetDataTableRunText(strSql, parameter);

            gvTest.DataSource = tb;
            gvTest.DataBind();           
        }

        /// <summary>
        /// 在控件被绑定后激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTest_DataBound(object sender, EventArgs e)
        {          
        }        
    }
}