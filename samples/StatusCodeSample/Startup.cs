using System;
using CustomStatusCodes;
using Microsoft.AspNet.Builder;

namespace StatusCodeSample
{
    public class Startup
    {
	    public void Configure(IApplicationBuilder app)
	    {
            app.UseKittenStatusCodes();
            // app.UsePuppyStatusCodes();
            // app.UseCustomStatusCodes(context => context.Response.WriteAsync(context.Response.StatusCode.ToString()));


            app.Use(next => new StatusCodePage(next, new CustomStatusCodesOptions()).Invoke);
            app.Run(context =>
            {
                context.Response.StatusCode = 404;
                var data = System.Text.Encoding.UTF8.GetBytes("Normal Not Found Page");
                return context.Response.Body.WriteAsync(data,0,data.Length);
            });
	    }
    }
}
