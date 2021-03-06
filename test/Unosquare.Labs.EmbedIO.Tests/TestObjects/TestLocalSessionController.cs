﻿namespace Unosquare.Labs.EmbedIO.Tests.TestObjects
{
    using Constants;
    using Modules;
#if NET47
    using System.Net;
#else
    using Net;
#endif

    public class TestLocalSessionController : WebApiController
    {
        public const string DeleteSession = "deletesession";
        public const string PutData = "putdata";
        public const string GetData = "getdata";
        public const string MyData = "MyData";
        public const string GetCookie = "GetCookie";
        public const string CookieName = "MyCookie";

        [WebApiHandler(HttpVerbs.Get, "/getcookie")]
        public bool GetCookieC(WebServer server, HttpListenerContext context)
        {
            var myCookie = new System.Net.Cookie(CookieName, CookieName);
            context.Response.Cookies.Add(myCookie);

            return context.JsonResponse(context.Response.Cookies[CookieName]);
        }

        [WebApiHandler(HttpVerbs.Get, "/deletesession")]
        public bool DeleteSessionC(WebServer server, HttpListenerContext context)
        {
            server.DeleteSession(context);

            return context.JsonResponse("Deleted");
        }

        [WebApiHandler(HttpVerbs.Get, "/putdata")]
        public bool PutDataSession(WebServer server, HttpListenerContext context)
        {
            server.GetSession(context).Data.TryAdd("sessionData", MyData);

            return context.JsonResponse(server.GetSession(context).Data["sessionData"].ToString());
        }

        [WebApiHandler(HttpVerbs.Get, "/getdata")]
        public bool GetDataSession(WebServer server, HttpListenerContext context)
        {
            object data;

            if (server.GetSession(context).Data.TryGetValue("sessionData", out data))
                return context.JsonResponse(server.GetSession(context).Data["sessionData"].ToString());

            return context.JsonResponse("");
        }
        
        [WebApiHandler(HttpVerbs.Get, "/geterror")]
        public bool GetError(WebServer server, HttpListenerContext context)
        {
            return false;
        }
    }
}
