using GenshinImpact_Lanucher.GameNotifys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace GrassCutter_Proxy.Common
{
    //public static class Global
    //{
    //    public static ProxyController controller { get; set; }
    //}

    public class ProxyController
    {



        ProxyServer proxyServer;
        ExplicitProxyEndPoint explicitEndPoint;
        private string port;
        private string fakeHost;

        public ProxyController(string port, string host)
        {
            this.port = port;
            this.fakeHost = host;


        }

        public void Start()
        {
            EnsureCert();

            //FiddlerApplication.Startup(int.Parse(port),
            //    FiddlerCoreStartupFlags.RegisterAsSystemProxy);

            FiddlerApplication.Startup(int.Parse(port),
                FiddlerCoreStartupFlags.RegisterAsSystemProxy |FiddlerCoreStartupFlags.DecryptSSL);
        }



        public void Stop()
        {
            try
            {
                explicitEndPoint.BeforeTunnelConnectRequest -= OnBeforeTunnelConnectRequest;
                proxyServer.BeforeRequest -= OnRequest;
                //proxyServer.BeforeResponse -= OnResponse;
                proxyServer.ServerCertificateValidationCallback -= OnCertificateValidation;

                // proxyServer.ClientCertificateSelectionCallback -= OnCertificateSelection;

            }catch (Exception ex)
            {

            }
            finally
            {
                if (proxyServer.ProxyRunning)
                {
                    proxyServer.Stop();

                }
                else
                {
                }

            }

        }

        public void UninstallCertificate()
        {

            proxyServer.CertificateManager.RemoveTrustedRootCertificate();
            proxyServer.CertificateManager.RemoveTrustedRootCertificateAsAdmin();


        }



        private async Task OnBeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs e)
        {
            string hostname = e.WebSession.Request.RequestUri.Host;
            if (hostname.EndsWith(".yuanshen.com") |
               hostname.EndsWith(".hoyoverse.com") |
               hostname.EndsWith(".mihoyo.com") | hostname.EndsWith(fakeHost))
            {
                e.DecryptSsl = true;
            }
            else
            {

                e.DecryptSsl = false;
            }
        }


        private async Task OnRequest(object sender, SessionEventArgs e)
        {


            string hostname = e.WebSession.Request.RequestUri.Host;
            if (hostname.EndsWith(".yuanshen.com") |
               hostname.EndsWith(".hoyoverse.com") |
               hostname.EndsWith(".mihoyo.com"))
            {

                string oHost = e.WebSession.Request.RequestUri.Host;

                e.HttpClient.Request.Url = e.HttpClient.Request.Url.Replace(oHost, fakeHost);

            }
        }

        // Allows overriding default certificate validation logic
        private Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
        {
            // set IsValid to true/false based on Certificate Errors
            //if (e.SslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            //    e.IsValid = true;
            e.IsValid = true;
            return Task.CompletedTask;
        }

    }
}