using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace FineUIPro.Web.common
{
    public partial class main_bootstrap_pure : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCustomIcon_Click(object sender, EventArgs e)
        {
            if (btnCustomIcon.IconFont == IconFont.VolumeUp)
            {
                btnCustomIcon.IconFont = IconFont.VolumeDown;
            }
            else if (btnCustomIcon.IconFont == IconFont.VolumeDown)
            {
                btnCustomIcon.IconFont = IconFont.VolumeOff;
            }
            else
            {
                btnCustomIcon.IconFont = IconFont.VolumeUp;
            }
        }
    }
}
