using System;
using System.Threading.Tasks;
using CustomStatusCodes;
using Microsoft.AspNet.Http;

namespace Microsoft.AspNet.Builder
{
    public static class CustomStatusCodesExtensions
    {
        public static IApplicationBuilder UseKittenStatusCodes(this IApplicationBuilder app)
        {
            return app.UseCustomStatusCodes(new LinkedImagePageGenerator(KittenLinks.Create()).GenerateAsync);
        }

        public static IApplicationBuilder UseKittenStatusCodes(this IApplicationBuilder app, CustomStatusCodesOptions options)
        {
            if (options.ResponseGenerator == null)
            {
                options.ResponseGenerator = new LinkedImagePageGenerator(KittenLinks.Create()).GenerateAsync;
            }
            return app.UseCustomStatusCodes(options);
        }

        public static IApplicationBuilder UsePuppyStatusCodes(this IApplicationBuilder app)
        {
            return app.UseCustomStatusCodes(new LinkedImagePageGenerator(PuppyLinks.Create()).GenerateAsync);
        }

        public static IApplicationBuilder UsePuppyStatusCodes(this IApplicationBuilder app, CustomStatusCodesOptions options)
        {
            if (options.ResponseGenerator == null)
            {
                options.ResponseGenerator = new LinkedImagePageGenerator(PuppyLinks.Create()).GenerateAsync;
            }
            return app.UseCustomStatusCodes(options);
        }

        public static IApplicationBuilder UseCustomStatusCodes(this IApplicationBuilder app, Func<HttpContext, Task> responseGenerator)
        {
            return app.UseCustomStatusCodes(new CustomStatusCodesOptions() { ResponseGenerator = responseGenerator });
        }

        public static IApplicationBuilder UseCustomStatusCodes(this IApplicationBuilder app, CustomStatusCodesOptions options)
        {
            return app.Use(next => new CustomStatusCodesMiddleware(next, options).Invoke);
        }
    }
}
