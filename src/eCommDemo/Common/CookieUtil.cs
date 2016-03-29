using System;
using System.Net.Http;
using System.Web;
using eComm.Domain.Models;

namespace eCommDemo.Common
{
    public static class CookieUtil
    {
        public static Guid GetCartToken(bool newCart = false)
        {
            var cookieCartToken =  HttpContext.Current.Request.Cookies.Get("CartToken");


            if (cookieCartToken == null || newCart)
            {
                var request = HttpUtil.CreateRequest("cart", HttpMethod.Post);

                var cartToken = HttpUtil.Send<CartToken>(request);

                HttpContext.Current.Response.Cookies.Add(new HttpCookie("CartToken", cartToken.TokenId.ToString()));

                return cartToken.TokenId;
            }

            return Guid.Parse(cookieCartToken.Value);
        }

    }
}