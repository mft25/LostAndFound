﻿using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Listings.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Formatters.Add(new BrowserJsonFormatter());
        }
    }

    public class BrowserJsonFormatter : JsonMediaTypeFormatter
    {
        public BrowserJsonFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}
