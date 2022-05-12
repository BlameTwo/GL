using Fiddler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GrassCutter_Proxy.Common
{
    public static class Global
    {
        public static ProxyController controller { get; set; }
    }

    public class ProxyController
    {
        private string port;
        private string host;
        public ProxyController(string port, string host)
        {
            this.port = port;
            this.host = host;
            FiddlerApplication.BeforeRequest += BeforeReq;
            FiddlerApplication.OnValidateServerCertificate += CheckCert;
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
            FiddlerApplication.Shutdown();
            UninstallCertificate();
        }

        public static bool UninstallCertificate()
        {
            if (CertMaker.rootCertIsTrusted())
            {
                if (!CertMaker.removeFiddlerGeneratedCerts(true))
                    return false;
            }
            return true;
        }



        void CheckCert(object sender, ValidateServerCertificateEventArgs e)
        {
            //if (SslPolicyErrors.None == e.CertificatePolicyErrors)
            //{
            //    return;
            //}

            //e.ValidityState = CertificateValidity.ForceValid;
            if (SslPolicyErrors.None == e.CertificatePolicyErrors)
            {
                return;
            }



            if (e.Session.hostname==this.host)
            {
                e.ValidityState = CertificateValidity.ForceValid;

            }


            //bool oResult = true;
            //if (oResult)
            //{
            //    //e.ExpectedCN
            //    e.ValidityState = CertificateValidity.ForceValid;
            //}
            //else
            //{
            //    e.ValidityState = CertificateValidity.ForceInvalid;
            //}

        }
        private void BeforeReq(Session s)
        {
            if (s.hostname.EndsWith(".yuanshen.com") |
                s.hostname.EndsWith(".hoyoverse.com") |
                s.hostname.EndsWith(".mihoyo.com"))
            {

                s.oFlags["X-OverrideCertCN"] = s.hostname;


                s.hostname = host;
                //s.host = $"{host}:443";
            }

        }
        private void EnsureCert()
        {
            if (!CertMaker.rootCertIsTrusted())
            {
                CertMaker.createRootCert();
                if (!CertMaker.trustRootCert())
                {
                    MessageBox.Show("开启失败！");
                    return;
                }
            }
        }


    }
}
