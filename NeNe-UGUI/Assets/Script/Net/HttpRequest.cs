using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace NeNe.LGL.Net
{
    public interface IHttpRequest
    {

        /// <summary>
        /// Post请求 需要改造 需要带token
        /// </summary>
        /// <param name="url"></param>
        /// <param name="www"></param>
        /// <returns></returns>
        Task<UnityWebRequest> HttpPost(string url, WWWForm www, Dictionary<string, string> header = null);


        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<UnityWebRequest> HttpGet(string url);

        /// <summary>
        ///判断是否有网
        /// </summary>
        /// <returns></returns>
        bool IsConnectNet();
    }
}
