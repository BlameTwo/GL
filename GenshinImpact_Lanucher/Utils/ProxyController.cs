
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

namespace GenshinImpact_Lanuncher.Utils
{ 
    //public static class Global
    //{
    //    public static ProxyController controller { get; set; }
    //}

    public class ProxyController
    {
        public static ProxyServer proxyServer;
        static ExplicitProxyEndPoint explicitEndPoint;
        public static string port;
        public static string fakeHost;

        private static bool IsRun;

        public static bool _IsRun
        {
            get { return proxyServer.ProxyRunning; }
            set { IsRun = value; }
        }


        public static void Start()
        {
            if (port == null || fakeHost == null)
                return;
            proxyServer = new ProxyServer();
            proxyServer.CertificateManager.EnsureRootCertificate();


            proxyServer.BeforeRequest += OnRequest;
            proxyServer.ServerCertificateValidationCallback += OnCertificateValidation;
            if (String.IsNullOrEmpty( port))
            {
                port = 11451.ToString(); ;
            }

            explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any,int.Parse(port), true)
            {
            };

            // Fired when a CONNECT request is received
            explicitEndPoint.BeforeTunnelConnectRequest += OnBeforeTunnelConnectRequest;

            // An explicit endpoint is where the client knows about the existence of a proxy
            // So client sends request in a proxy friendly manner
            proxyServer.AddEndPoint(explicitEndPoint);
            proxyServer.Start();


            foreach (var endPoint in proxyServer.ProxyEndPoints)
                Console.WriteLine("Listening on '{0}' endpoint at Ip {1} and port: {2} ",
                    endPoint.GetType().Name, endPoint.IpAddress, endPoint.Port);

            // Only explicit proxies can be set as system proxy!
            proxyServer.SetAsSystemHttpProxy(explicitEndPoint);
            proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);
            
        }



        public static void Stop()
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

        public static void UninstallCertificate()
        {
            proxyServer = new ProxyServer();
            proxyServer.CertificateManager.RemoveTrustedRootCertificate();
            proxyServer.CertificateManager.RemoveTrustedRootCertificateAsAdmin();
        }



        private static async Task OnBeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs e)
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


        private static async Task OnRequest(object sender, SessionEventArgs e)
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
        private static Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
        {
            // set IsValid to true/false based on Certificate Errors
            //if (e.SslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            //    e.IsValid = true;
            e.IsValid = true;
            return Task.CompletedTask;
        }

    }
}