using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UIF
{
    /// <summary>
    /// Summary description for SslControl
    /// </summary>
    public class SslControl : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}