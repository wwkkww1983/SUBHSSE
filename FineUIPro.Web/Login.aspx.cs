namespace FineUIPro.Web
{
    using System;
    using System.Configuration;
    using System.Linq;
    using BLL;

    public partial class Login : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                string userName = Request.QueryString["Account"];
                if (!string.IsNullOrEmpty(userName)) ///单点登陆
                {
                    if (BLL.LoginService.UserLogOn(userName, this.ckRememberMe.Checked, this.Page))
                    {
                        this.CurrUser.LoginProjectId = null;
                        if (tbxUserName.Text == BLL.Const.adminAccount)
                        {
                            try
                            {
                                /////创建客户端服务
                                var poxy = Web.ServiceProxy.CreateServiceClient();
                                poxy.GetSys_VersionToSUBCompleted += new EventHandler<HSSEService.GetSys_VersionToSUBCompletedEventArgs>(poxy_GetSys_VersionToSUBCompleted);
                                poxy.GetSys_VersionToSUBAsync();
                            }
                            catch
                            {
                            }
                        }

                        BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "登录成功！");
                        PageContext.Redirect("~/default.aspx");
                        // Response.Redirect("~/index.aspx");
                    }
                    else
                    {
                        Alert.ShowInParent("您没有这个权限，请与管理员联系！", MessageBoxIcon.Warning);
                    }
                }

                //this.tbxUserName.Focus();
                var unit = BLL.CommonService.GetIsThisUnit();
                if (unit != null && !string.IsNullOrEmpty(unit.UnitName))
                {
                    this.lbSubName.Text = unit.UnitName;
                }
                this.LoadData();
                if (Request.Cookies["UserInfo"] != null)
                {
                    if (Request.Cookies["UserInfo"]["username"] != null)
                    {
                        this.tbxUserName.Text = Request.Cookies["UserInfo"]["username"].ToString();
                    }
                    if (Request.Cookies["UserInfo"]["password"] != null)
                    {
                        this.tbxPassword.Text = Request.Cookies["UserInfo"]["password"].ToString();
                    }

                    this.ckRememberMe.Checked = true;
                }
                string sysVersion = ConfigurationManager.AppSettings["SystemVersion"];
                this.lbVevion.Text = "请使用IE10以上版本浏览器 系统版本号：" + sysVersion;               
            }
        }

        /// <summary>
        /// 生成图片
        /// </summary>
        private void LoadData()
        {
            this.InitCaptchaCode();
        }

        /// <summary>
        /// 初始化验证码
        /// </summary>
        private void InitCaptchaCode()
        {
            // 创建一个 6 位的随机数并保存在 Session 对象中
            Session["CaptchaImageText"] = GenerateRandomCode();
            imgCaptcha.Text = String.Format("<img src=\"{0}\" />", ResolveUrl("~/Captcha/captcha.ashx?w=100&h=26&t=" + DateTime.Now.Ticks));
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

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbxCaptcha.Text != Session["CaptchaImageText"].ToString())
            {
                ShowNotify("验证码错误！", MessageBoxIcon.Error);
                return;
            }

            if (BLL.LoginService.UserLogOn(tbxUserName.Text, this.tbxPassword.Text, this.ckRememberMe.Checked, this.Page))
            {
                this.CurrUser.LoginProjectId = null; 
                if (tbxUserName.Text == BLL.Const.adminAccount)
                {
                    try
                    {
                        /////创建客户端服务
                        var poxy = Web.ServiceProxy.CreateServiceClient();
                        poxy.GetSys_VersionToSUBCompleted += new EventHandler<HSSEService.GetSys_VersionToSUBCompletedEventArgs>(poxy_GetSys_VersionToSUBCompleted);
                        poxy.GetSys_VersionToSUBAsync();
                    }
                    catch
                    {                        
                    }
                }

                BLL.LogService.AddLog(this.CurrUser.LoginProjectId,this.CurrUser.UserId, "登录成功！");
                PageContext.Redirect("~/default.aspx");               
            }
            else
            {
                ShowNotify("用户名或密码错误！", MessageBoxIcon.Error);
            }           
        }

        #region 版本信息从集团公司提取
        /// <summary>
        /// 版本信息从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetSys_VersionToSUBCompleted(object sender, HSSEService.GetSys_VersionToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var versionItems = e.Result;
                if (versionItems.Count() > 0)
                {
                    count = versionItems.Count();
                    foreach (var item in versionItems)
                    {
                        var version = Funs.DB.Sys_Version.FirstOrDefault(x => x.VersionId == item.VersionId);
                        if (version == null)
                        {
                            Model.Sys_Version newVersion = new Model.Sys_Version
                            {
                                VersionId = item.VersionId,
                                VersionName = item.VersionName,
                                VersionDate = item.VersionDate,
                                CompileMan = item.CompileMan,
                                AttachUrl = item.AttachUrl,
                                IsSub = item.IsSub
                            };
                            Funs.DB.Sys_Version.InsertOnSubmit(newVersion);
                            Funs.DB.SubmitChanges();                            
                        }
                    }
                }
            }
            if (e.Error == null)
            {
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "【版本信息】从集团提取" + count.ToString() + "条数据；");
            }
            else
            {
                BLL.LogService.AddLog(this.CurrUser.LoginProjectId, this.CurrUser.UserId, "【版本信息】从集团提取失败；");
            }            
        }
        #endregion 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.tbxUserName.Text = string.Empty;
            this.tbxPassword.Text = string.Empty;
            this.ckRememberMe.Checked = false;

        }
    }
}
