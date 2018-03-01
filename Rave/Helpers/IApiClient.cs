using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RaveDotNet.Helpers 
{
    public interface IApiClient
    {
        Task<TClass> Get<TClass>(string url, Dictionary<string, string> headers = null, AuthenticationHeaderValue authorization = null) where TClass: class;
        Task<TClass> Post<TClass>(string url, HttpContent body, Dictionary<string, string> headers = null, AuthenticationHeaderValue authorization = null) where TClass : class;
        Task<TClass> Put<TClass>(string url, HttpContent body, Dictionary<string, string> headers = null, AuthenticationHeaderValue authorization = null) where TClass : class;
        Task<TClass> Delete<TClass>(string url, Dictionary<string, string> headers = null, AuthenticationHeaderValue authorization = null) where TClass : class;
    }
}