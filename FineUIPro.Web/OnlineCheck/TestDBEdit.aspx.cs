using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace FineUIPro.Web.OnlineCheck
{
    public partial class TestDBEdit : PageBase
    {
        /// <summary>
        /// 附件路径
        /// </summary>
        public string TestPath
        {
            get
            {
                return (string)ViewState["TestPath"];
            }
            set
            {
                ViewState["TestPath"] = value;
            }
        }

        private string rootPath = "~/FileUpload/Image/OnlineCheck/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();

                this.drpTestType.DataTextField = "TestType";
                drpTestType.DataValueField = "TestType";
                drpTestType.DataSource = BLL.TestDBService.GetTestTypeList();
                drpTestType.DataBind();
                Funs.FineUIPleaseSelect(drpTestType);

                this.drpItemType.DataTextField = "ItemType";
                drpItemType.DataValueField = "ItemType";
                drpItemType.DataSource = BLL.TestDBService.GetItemTypeList();
                drpItemType.DataBind();
                Funs.FineUIPleaseSelect(drpItemType);
                imgTestContent.ImageUrl = Funs.RootPath + "Images\\Null.jpg";

                string testId = Request.QueryString["TestId"];
                if (!String.IsNullOrEmpty(testId))
                {
                    var q = BLL.TestDBService.GetTestDBByTestId(testId);
                    this.drpTestType.SelectedValue = q.TestType;
                    drpItemType.SelectedValue = q.ItemType;
                    this.txtTestNo.Text = q.TestNo;
                    this.txtTestKey.Text = q.TestKey;
                    this.txtKeyNumber.Text = q.KeyNumber.ToString();
                    imgTestContent.ImageUrl = rootPath + q.TestPath;
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.imgTestContent.ImageUrl))
            {
                imgTestContent.MarkInvalid("请先上传试题内容！");
                ShowNotify("请先上传试题内容！");
                return;
            }

            if (String.IsNullOrEmpty(drpTestType.Text) && drpTestType.SelectedText == "- 请选择 -")
            {
                ShowNotify("请为试题类型提供有效值！");
                return;
            }
            
            if (String.IsNullOrEmpty(drpItemType.Text) && drpItemType.SelectedText == "- 请选择 -")
            {
                ShowNotify("请为题型提供有效值！");
                return;
            }

            Model.Edu_Online_TestDB test = new Model.Edu_Online_TestDB();
            if (drpTestType.SelectedItem != null)
            {
                test.TestType = drpTestType.SelectedItem.Text;
            }
            else
            {
                test.TestType = drpTestType.Text;
            }

            if (this.drpItemType.SelectedItem != null)
            {
                test.ItemType = drpItemType.SelectedItem.Text;
            }
            else
            {
                test.ItemType = drpItemType.Text;
            }

            test.TestNo = this.txtTestNo.Text.Trim();
            test.TestKey = this.txtTestKey.Text.Trim();
            test.KeyNumber = Convert.ToInt32(this.txtKeyNumber.Text.Trim());
            test.TestPath = TestPath;

            if (!String.IsNullOrEmpty(Request.QueryString["TestId"]))
            {
                var q = BLL.TestDBService.GetTestDBByTestId(Request.QueryString["TestId"]);
                test.TestId = q.TestId;

                if (TestPath != null)
                {
                    string fullPath = Server.MapPath("~/") + "FileUpload\\Image\\OnlineCheck\\" + q.TestPath;
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                }
                else 
                {
                    test.TestPath = q.TestPath;
                }

                BLL.TestDBService.UpdateTestDB(test);
                BLL.LogService.AddSys_Log(this.CurrUser, test.TestNo, test.TestId, BLL.Const.TestDBMenuId, Const.BtnModify);
            }
            else
            {
                BLL.TestDBService.AddTestDB(test);
                BLL.LogService.AddSys_Log(this.CurrUser, test.TestNo, test.TestId, BLL.Const.TestDBMenuId, Const.BtnAdd);
            }

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 试题上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void testContent_FileSelected(object sender, EventArgs e)
        {
            if (testContent.HasFile)
            {
                string fileName = testContent.ShortFileName;

                if (!ValidateFileType(fileName))
                {
                    ShowNotify("无效的文件类型！");
                    return;
                }

                fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                fileName = DateTime.Now.Ticks.ToString() + "_" + fileName;
                TestPath = fileName;
                testContent.SaveAs(Server.MapPath(rootPath + fileName));

                imgTestContent.ImageUrl = rootPath + fileName;

                // 清空文件上传组件
                testContent.Reset();
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
        }
    }
}