using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using XNSC.DD;
using XNSC.Net;

namespace WorkerScreen.Utils
{
    internal class ImateHelper
    {
        /// <summary>
        /// API Profile
        /// </summary>
        public ApiProfile Profile { get; set; }

        /// <summary>
        /// Single Tone
        /// </summary>
        /// <param name="reCreate"></param>
        /// <returns></returns>
        public static ImateHelper GetSingleTone(bool reCreate = false)
        {
            if (!reCreate && singleToneObj == null)
                singleToneObj = new ImateHelper();

            return singleToneObj;
        }
        private static ImateHelper singleToneObj = null;

        //-------------------------------------------------------------------------------

        /// <summary>
        /// Imate Adapter
        /// </summary>
        public ImateAdapter Adapter { get; set; }

        /// <summary>
        /// 파일 업로드
        /// </summary>
        public ImateFileUpload FileUpload { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
        public ImateHelper()
        {
            CryptKeyTableManager.LoadKeyTable(Properties.Resources.Profile);
            var keyData = CryptKeyTableManager.GetCryptKey(0);

            Profile = JsonSerializer.Deserialize<ApiProfile>(keyData);

            Adapter = new ImateAdapter(Profile.ApiUrl, Profile.Secret, Profile.ApiUserId, Profile.ApiPassword, true, false);
            //파일 업로드
            FileUpload = new ImateFileUpload(Profile.ApiUrl, Profile.ApiUserId, Profile.ApiPassword);

            //Adapter = new ImateAdapter("https://192.168.3.37/iMATEWebAPIB4", apiProfile.Secret, "iacm_system", "a#12!08@", true, false);
            //FileUpload = new ImateFileUpload("https://192.168.3.37/iMATEWebAPIB4", "iacm_system", "a#12!08@");
        }
    }
}
