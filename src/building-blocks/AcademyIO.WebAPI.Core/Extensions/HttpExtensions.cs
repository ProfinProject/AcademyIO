using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Utilities;
using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace AcademyIO.WebAPI.Core.Extensions
{
    public static class HttpExtensions
    {
        public static IHttpClientBuilder AllowSelfSignedCertificate(this IHttpClientBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            return builder.ConfigurePrimaryHttpMessageHandler(_ => ConfigureClientHandler());
        }

        public static HttpClientHandler ConfigureClientHandler()
        {
            var path = Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path");
            var certPass = Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Password");

            if (path.IsPresent())
                return new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (_, cert, chain, _) =>
                    {
                        chain.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;
                        var certificate = new X509Certificate2(File.ReadAllBytes(path!), certPass);
                        chain.ChainPolicy.CustomTrustStore.Add(certificate);

                        return chain.Build(cert);
                    }
                };

            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
        }
    }
}
