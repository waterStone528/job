using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Bridge : System.Web.UI.Page
{
    public string Scripts = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["succ"] == "Y") {
            Scripts = "window.parent.opener.P9.Pclose();window.parent.opener.P9.Suc();";
        }
        Scripts += "window.close();";
    }
}