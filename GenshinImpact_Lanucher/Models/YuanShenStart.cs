
using Fiddler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenshinImpact_Lanucher.Models
{
    public static class YuanShenStart
    {
        static int port = 11451;

        public static void Start()
        {
            if(MessageBox.Show("此操作需要原神私服服务器、JAVA环境包、MongoDBCompass数据库才可运行哦！","提示") == DialogResult.No)
            {
                return;
            }
            if (!CertMaker.rootCertIsTrusted())
            {
                CertMaker.createRootCert();
                if (!CertMaker.trustRootCert())
                {
                    Console.WriteLine("开启失败！");
                    return;
                }
            }

            FiddlerApplication.Startup(port,
                FiddlerCoreStartupFlags.Default);

            Console.WriteLine($"Started at {port}");


            FiddlerApplication.BeforeRequest += BeforeReq;
            FiddlerApplication.AfterSessionComplete += AfterReq;
            FiddlerApplication.OnValidateServerCertificate += CheckCert;



            Console.ReadLine();


        }
        static void CheckCert(object sender, ValidateServerCertificateEventArgs e)
        {
            if (SslPolicyErrors.None == e.CertificatePolicyErrors)
            {
                return;
            }

            //DialogResult oResult = 
            //    MessageBox.Show("Accept invalid certificate\nYOUR DETAILS HERE", "Certificate Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            Console.WriteLine("Accept invalid certificate;");

            DialogResult oResult = DialogResult.Yes;
            if (DialogResult.Yes == oResult)
            {
                //e.ExpectedCN
                e.ValidityState = CertificateValidity.ForceValid;
            }
            else
            {
                e.ValidityState = CertificateValidity.ForceInvalid;
            }
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


        static string[] domains = new string[]
        {
        "yuanshen.com",
        "hoyoverse.com",
        "mihoyo.com"
        };

        static void AfterReq(Session s)
        {
            //Console.WriteLine($"Captured a Resp from {s.ResponseHeaders}");

            //Console.WriteLine($"Captured a Resp from {s.ResponseBody.ToString()}");

        }



        static void BeforeReq(Session s)
        {
            string host = "127.0.0.1";
            if (s.hostname.EndsWith(".yuanshen.com") |
                s.hostname.EndsWith(".hoyoverse.com") |
                s.hostname.EndsWith(".mihoyo.com"))
            {

                Console.WriteLine($"Captured a request from {s.fullUrl}");
                Console.WriteLine($"Captured a request from {s.RequestBody.ToString()}");
                Console.WriteLine($"Captured a request from {s.RequestHeaders}");
                s.oFlags["X-OverrideCertCN"] = s.hostname;


                s.hostname = host;
                //s.host = $"{host}:443";
            }


            //if (oS.host.EndsWith(".yuanshen.com") || oS.host.EndsWith(".hoyoverse.com") || oS.host.EndsWith(".mihoyo.com"))
            //{
            //    oS.host = "ubuntu.local"; // This can also be replaced with another IP address.
            //}
        }
    }
}
