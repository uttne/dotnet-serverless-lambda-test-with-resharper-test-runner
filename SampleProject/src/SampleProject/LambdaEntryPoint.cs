using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SampleProject
{
    public class LambdaEntryPoint : Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction, IDisposable
    {
        private IHost _webHost;
        protected override void Init(IWebHostBuilder builder)
        {
            builder
                .UseStartup<Startup>();
        }

        protected override void Init(IHostBuilder builder)
        {
        }

        // Host が作成された際に PostCreateHost が呼び出されるので field に Host を保存しておく
        protected override void PostCreateHost(IHost webHost)
        {
            _webHost = webHost;
            base.PostCreateHost(webHost);
        }

        // LambdaEntryPoint が不要になったタイミングで解放するために Dispose を実装する
        public void Dispose()
        {
            _webHost?.Dispose();
            _webHost = null;
        }
    }
}