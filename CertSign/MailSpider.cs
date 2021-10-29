using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace CertSign
{
    class MailSpider
    {
        public MailSpider() { 
        }
        ///<summary>
        /// Process the web response.
        ///</summary>
        ///<param name="webRequest">The request object.</param>
        ///<returns>The response data.</returns>
        public static string WebResponseGet(HttpWebResponse rsp)
        {
            StreamReader responseReader = null;
            string responseData = "";

            try
            {
                responseReader = new StreamReader(rsp.GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
                return "连接错误";
            }
            finally
            {
                rsp.GetResponseStream().Close();
                responseReader.Close();
                responseReader = null;
            }

            return responseData;
        }
        public int dl_mail(ref string otpcode) 
        {

            HttpPostRequestClient httpReq = new HttpPostRequestClient();
            httpReq.SetField("destination", "https://mail.realtek.com/owa");
            httpReq.SetField("flags", "4");
            httpReq.SetField("forcedownlevel", "0");
            httpReq.SetField("username", "jingdong_qiu@realtek.com");
            httpReq.SetField("password", "lgm168313,");
            httpReq.SetField("isUtf8", "1");
            HttpWebResponse rsp= httpReq.Post("https://mail.realtek.com/owa/auth.owa");
            string resData = WebResponseGet(rsp);
            Console.WriteLine(resData);
            string regexStr = string.Empty;
            regexStr = "<input type=\"checkbox\" name=\"chkmsg\" value=\"(?<id>[^>]+)\" title=\"[^>]+\" onclick=\"[^>]+\">&nbsp;</td><td nowrap( class=\"frst\")?>motp@realtek.com&nbsp;<[^>]+>";   // ""匹配"
            Match mat = Regex.Match(resData, regexStr);
            for (int i = 0; i < mat.Groups.Count; i++)
            {
                Console.WriteLine("第" + i + "组：" + mat.Groups[i].Value);
            }

            bool bFindMOTP = false;
            string tokenVal = "";

            if (mat.Success) {
                tokenVal = mat.Groups["id"].Value;
                Console.WriteLine("=> {0}", tokenVal);
                bFindMOTP = true;
            }
            else {
                MessageBox.Show("not found");
            }
            string s = HttpUtility.UrlEncode(tokenVal);
            //Console.WriteLine("url encoding {0}", s);
            if (bFindMOTP) {
                string token = HttpUtility.UrlEncode(tokenVal);
                if (token.Equals(string.Empty)) MessageBox.Show(" url encoding token failed");
                // "https://mail.realtek.com/owa/?ae=Item&t=IPM.Note&id=RgAAAACZpfPyFZDkTIBBuJbamineBwB3JuyQcogkSZTumckl5UoQAAAApGXlAABmstg4X0%2bIQL1SPLvPP4CPAAFbGrjJAAAJ"
                string url = "https://mail.realtek.com/owa/?ae=Item&t=IPM.Note&id=" + token;
                HttpGetRequestClient d2httpReq = new HttpGetRequestClient();
                d2httpReq.SetCookie(httpReq.CookieContainer);
                HttpWebResponse d2rsp = d2httpReq.Get(url);
                string d2resData = WebResponseGet(d2rsp);
                Console.WriteLine(d2resData);
                //<font color="#0000FF">995253</font>
                regexStr = @"<font color=""#0000FF"">(?<otp>\d{6})</font>";
                Match d2mat = Regex.Match(d2resData, regexStr);
                if (d2mat.Success) {
                    otpcode = d2mat.Groups["otp"].Value;
                    Console.WriteLine("otp 码：{0}", otpcode);

                    return 0;
                }
            }
            return -1;

        }
    }
}
