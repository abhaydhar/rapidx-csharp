using ArtHandler.Interface;
using ArtHandler.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ART.Models
{
    public class ReCaptchaClass
    {

        public static string Validate(string EncodedResponse)
        {
            var client = new System.Net.WebClient();
            IConfigProvider objConfig = new ConfigProvider();

            string PrivateKey = objConfig.GetConfigValue("GoogleCaptchaSecureKey");
            string verifyUrl = objConfig.GetConfigValue("GoogleCaptchaVerifyUrl");

            //string PrivateKey = "6LcaPSgUAAAAAMKVSPWyQ8el6UEjq5RkYqiIbuF6";

            var GoogleReply = client.DownloadString(string.Format(verifyUrl + "?secret={0}&response={1}", PrivateKey, EncodedResponse));

            var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaClass>(GoogleReply);

            return captchaResponse.Success;
        }

        [JsonProperty("success")]
        public string Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        private string m_Success;
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }


        private List<string> m_ErrorCodes;
    }

}