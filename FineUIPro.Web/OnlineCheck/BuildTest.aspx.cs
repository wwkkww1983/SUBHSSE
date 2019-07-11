using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json.Linq;
using BLL;

namespace FineUIPro.Web.OnlineCheck
{
    public partial class BuildTest : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                BLL.WorkPostService.InitWorkPostDropDownList(this.ddlWorkPost, true);
                Funs.FineUIPleaseSelect(ddlABVolume, "-请选择AB卷-");
                // 绑定表格
                BindGrid();
            }

            else
            {
                if (GetRequestEventArgument() == "UPDATE_SUMMARY")
                {
                    // 页面要求重新计算合计行的值
                    OutputSummaryData();
                }
            }
        }

        private void BindGrid()
        {
            SqlParameter[] parameter = new SqlParameter[]       
                    {
                        new SqlParameter("@WorkPostId",this.ddlWorkPost.SelectedValue),
                        new SqlParameter("@ABVolume",this.ddlABVolume.SelectedValue),
                    };
            DataTable tb = SQLHelper.GetDataTableRunProc("sp_GetTestCondition", parameter);
            Grid1.DataSource = tb;
            Grid1.DataBind();
            OutputSummaryData();
        }

        #region OutputSummaryData

        private void OutputSummaryData()
        {
            int selectNumber = 0;
            int testTotalScore = 0;
            foreach (JObject mergedRow in Grid1.GetMergedData())
            {
                JObject values = mergedRow.Value<JObject>("values");

                if (values["SelectNumber"].ToString() != "")
                {
                    selectNumber += values.Value<int>("SelectNumber");
                }

                if (values["TestTotalScore"].ToString() != "")
                {
                    testTotalScore += values.Value<int>("TestTotalScore");
                }
            }

            JObject summary = new JObject();
            summary.Add("TestType", "合计：");
            summary.Add("SelectNumber", selectNumber);
            summary.Add("TestTotalScore", testTotalScore);

            Grid1.SummaryData = summary;
        }

        #endregion

         /// <summary>
         /// 保存试卷的条件
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         protected void btnSave_Click(object sender, EventArgs e)
         {

            Model.Edu_Online_TestCondition tc = new Model.Edu_Online_TestCondition
            {
                WorkPostId = this.ddlWorkPost.SelectedValue,
                ABVolume = this.ddlABVolume.SelectedValue
            };

            // 生成试卷条件前先删除原来的试卷及试卷的条件
            BLL.BuildTestService.DeleteTest(tc.WorkPostId, tc.ABVolume);
             BLL.BuildTestService.DeleteTestCondition(tc.WorkPostId, tc.ABVolume);

             for (int i = 0; i < Grid1.Rows.Count(); i++)
             {
                 if (Grid1.Rows[i].Values[5] != null && Grid1.Rows[i].Values[5].ToString() != "")
                 {
                     tc.TestType = (Grid1.Rows[i].Values[1]).ToString();
                     tc.ItemType = (Grid1.Rows[i].Values[2]).ToString();

                     tc.SelectNumber = Convert.ToInt32(Grid1.Rows[i].Values[4]);

                     if (Grid1.Rows[i].Values[5] != null && Grid1.Rows[i].Values[5].ToString() != "")
                     {
                         tc.TestScore = Convert.ToInt32(Grid1.Rows[i].Values[5]);
                     }

                     BLL.BuildTestService.AddTestCondition(tc);
                 }
             }

             BindGrid();
             ShowNotify("生成试卷的条件保存成功！");
         }
         
         /// <summary>
         /// 生成试卷
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         protected void btnBuild_Click(object sender, EventArgs e)
         {
             if (BLL.BuildTestService.GetTestCondition(this.ddlWorkPost.SelectedValue, this.ddlABVolume.SelectedValue) != null)
             {
                 // 生成试卷前先删除原来的试卷
                 BLL.BuildTestService.DeleteTest(this.ddlWorkPost.SelectedValue, this.ddlABVolume.SelectedValue);
                 for (int i = 0; i < Grid1.Rows.Count(); i++)
                 {
                     if (Grid1.Rows[i].Values[4] != null && Grid1.Rows[i].Values[4].ToString() != "")
                     {
                         string rowID = Grid1.DataKeys[i][0].ToString();
                         if (rowID != "")
                         {

                             var con = BLL.BuildTestService.GetTestCondition(rowID);
                             var q = from x in Funs.DB.Edu_Online_TestDB
                                     where x.TestType == con.TestType && x.ItemType == con.ItemType
                                     select x;
                             if (Convert.ToInt32(Grid1.Rows[i].Values[3]) == Convert.ToInt32(Grid1.Rows[i].Values[4]))
                             {
                                 foreach (var t in q)
                                 {
                                    Model.Edu_Online_Test test = new Model.Edu_Online_Test
                                    {
                                        TestDBId = t.TestId,
                                        TestConditionId = rowID
                                    };
                                    //test.TestCode = this.GetMaxId();
                                    BLL.BuildTestService.AddTest(test);
                                 }
                             }
                             else
                             {
                                 int[] index = GetRamdomNum(Convert.ToInt32(Grid1.Rows[i].Values[3]), Convert.ToInt32(Grid1.Rows[i].Values[4]));
                                 int j = 1;
                                 foreach (var t in q)
                                 {
                                     if (index.Contains(j))
                                     {
                                        Model.Edu_Online_Test test = new Model.Edu_Online_Test
                                        {
                                            TestDBId = t.TestId,
                                            TestConditionId = rowID
                                        };
                                        //test.TestCode = this.GetMaxId();
                                        BLL.BuildTestService.AddTest(test);
                                     }
                                     j++;
                                 }

                             }
                         }
                     }
                 }

                 // 生成试卷号
                 var code = from x in BLL.Funs.DB.Edu_Online_Test
                            join y in BLL.Funs.DB.Edu_Online_TestCondition on x.TestConditionId equals y.TestConditionId
                            orderby y.ItemType
                            select x;
                 if (code.Count() > 0)
                 {
                     foreach (var c in code)
                     {
                         BLL.BuildTestService.UpdateTest(c.TestId, this.GetMaxId());
                     }
                 }
                 ShowNotify("生成试卷成功！");
             }

             else
             {
                 ShowNotify("先保存生成试卷的条件，再生成试卷！");
             }
         }

         protected void btnDelete_Click(object sender, EventArgs e)
         {
             BLL.BuildTestService.DeleteTest(this.ddlWorkPost.SelectedValue, this.ddlABVolume.SelectedValue);
             BLL.BuildTestService.DeleteTestCondition(this.ddlWorkPost.SelectedValue, this.ddlABVolume.SelectedValue);
             
             BindGrid();
         }

         protected void ddlABVolume_SelectedIndexChanged(object sender, EventArgs e)
         {
             BindGrid();
         }

         /// <summary>
         /// 获取随机数
         /// </summary>
         /// <param name="testNumber">最大范围</param>
         /// <param name="selectNumber">随机个数</param>
         /// <returns>随机数组</returns>
         private int[] GetRamdomNum(int testNumber, int selectNumber)
         {
             int[] index = new int[testNumber];
             //用来保存随机生成的不重复的选题个数 
             int[] result = new int[selectNumber];

             for (int i = 0; i < testNumber; i++)
             {
                 index[i] = i;
             }

             Random r = new Random();

             int site = testNumber;//设置上限 
             int id;
             for (int j = 0; j < selectNumber; j++)
             {
                 id = r.Next(1, site - 1);
                 //在随机位置取出一个数，保存到结果数组 
                 result[j] = index[id];
                 //最后一个数复制到当前位置 
                 index[id] = index[site - 1];
                 //位置的上限减少一 
                 site--;
             }
             return result;
         }

         /// <summary>
         /// 有条件获取最大值
         /// </summary>
         /// <returns>最大值</returns>
         private int GetMaxId()
         {
             int maxId = 0;
             string str = "SELECT (ISNULL(MAX(TestCode),0)+1) from dbo.Edu_Online_Test t left join dbo.Edu_Online_TestCondition c on c.TestConditionId=t.TestConditionId where c.WorkPostId='" + this.ddlWorkPost.SelectedValue + "' and c.ABVolume='" + this.ddlABVolume.SelectedValue + "' ";
             maxId = BLL.SQLHelper.GetIntValue(str);
             return maxId;
         }
    }
}