using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace BLL
{
    ////一些基本的判断
    //public class CommCtl : IHttpHandler, IRequiresSessionState
    //{
    //    private static DAL.CommCtl dal = new DAL.CommCtl();

    //    #region IHttpHandler Members

    //    public bool IsReusable
    //    {
    //        // Return false in case your Managed Handler cannot be reused for another request.
    //        // Usually this would be false in case you have some state information preserved per request.
    //        get { return true; }
    //    }

    //    public void ProcessRequest(HttpContext context)
    //    {
    //        //write your handler implementation here.
    //    }

    //    #endregion

    //    //是否已经登录
    //    public bool IsLogin()
    //    {
    //        return HttpContext.Current.Session["internalUserId"] != null ? true : false;
    //    }

    //    //获得用户的二级权限菜单
    //    public string IsPermited(string menuCode)
    //    {
    //        return dal.IsPermited(Convert.ToInt32(HttpContext.Current.Session["internalUserId"]), menuCode);
    //    }
    //}

    //一些基本的判断

    public class CommCtl
    {
        private static DAL.CommCtl dal = new DAL.CommCtl();

        //是否已经登录
        public bool IsLogin()
        {
            return HttpContext.Current.Session["internalUserId"] != null ? true : false;
        }

        //获得用户的二级权限菜单
        public string IsPermited(string menuCode)
        {
            return dal.IsPermited(Convert.ToInt32(HttpContext.Current.Session["internalUserId"]), menuCode);
        }
    }
}
