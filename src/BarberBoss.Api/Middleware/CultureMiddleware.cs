﻿using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace BarberBoss.Api.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            var requestedCulture = context.Request.Headers["Accept-Language"].ToString();

            var cultureInfo = new CultureInfo("en");

            if (string.IsNullOrWhiteSpace(requestedCulture) == false && supportedLanguages.Exists(language => language.Name.Equals(requestedCulture)))
            {
                cultureInfo = new CultureInfo(requestedCulture);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
