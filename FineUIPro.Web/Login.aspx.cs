namespace FineUIPro.Web
{
    using BLL;
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Web;

    public partial class Login : PageBase
    {
        #region 页面加载
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitCaptchaCode();
                if (Request.Cookies["UserInfo"] != null)
                {
                    if (Request.Cookies["UserInfo"]["username"] != null)
                    {
                        this.tbxUserName.Text = HttpUtility.UrlDecode(Request.Cookies["UserInfo"]["username"].ToString());
                    }
                    if (Request.Cookies["UserInfo"]["password"] != null)
                    {
                        this.tbxPassword.Text = Request.Cookies["UserInfo"]["password"].ToString();
                    }

                   // this.ckRememberMe.Checked = true;
                }
                //string sysVersion = ConfigurationManager.AppSettings["SystemVersion"];
                //this.lbVevion.Text = "请使用IE10以上版本浏览器 版本号：" + sysVersion;           
               // GetDataService.CorrectingPersonInOutNumber("37e343bc-7ea3-4ee8-8944-3b61a845c5aa");
            }
        }
        #endregion

        #region 图片验证
        /// <summary>
        /// 初始化验证码
        /// </summary>
        private void InitCaptchaCode()
        {
            // 创建一个 6 位的随机数并保存在 Session 对象中
            Session["CaptchaImageText"] = GenerateRandomCode();
            imgCaptcha.Text = String.Format("<img src=\"{0}\" />", ResolveUrl("~/Captcha/captcha.ashx?w=100&h=30&t=" + DateTime.Now.Ticks));
        }

        /// <summary>
        /// 创建一个 6 位的随机数
        /// </summary>
        /// <returns></returns>
        private string GenerateRandomCode()
        {
            string s = String.Empty;
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                s += random.Next(10).ToString();
            }
            return s;
        }

        /// <summary>
        ///  重置图片验证码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgCaptcha_Click(object sender, EventArgs e)
        {
            InitCaptchaCode();
        }
        #endregion

        #region 登录验证
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Session["CaptchaImageText"] != null)
            {
                if (tbxCaptcha.Text != Session["CaptchaImageText"].ToString())
                {
                    ShowNotify("验证码错误！", MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ShowNotify("验证码过期请刷新！", MessageBoxIcon.Error);
                return;
            }

            if (BLL.LoginService.UserLogOn(tbxUserName.Text, this.tbxPassword.Text, true, this.Page))
            {
                this.CurrUser.LoginProjectId = null;
                PageContext.Redirect("~/default.aspx");
                BLL.LogService.AddSys_Log(this.CurrUser, this.CurrUser.UserName, string.Empty, BLL.Const.UserMenuId, BLL.Const.BtnLogin);
            }
            else
            {
                ShowNotify("用户名或密码错误！", MessageBoxIcon.Error);
            }
        }
        #endregion        
    }
}
