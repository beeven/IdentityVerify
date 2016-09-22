using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


namespace IdentityVerifyLib
{
    public class TokenService : IDisposable
    {
        private string _accessToken;
        public string AccessToken
        {
            get
            {
                
                    RenewAccessToken();
                
                return _accessToken;
            }
        }
        private DateTime tokenExpireAt;
        private string clientId;
        private string clientSecret;
        private string serviceBaseUrl;
        private HttpClient httpClient;

        public TokenService(Options options): this(options.ClientId, options.ClientSecret, options.ServiceBaseUrl)
        {
        }

        public TokenService(string clientId, string clientSecret, string serviceBaseUrl)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.serviceBaseUrl = serviceBaseUrl;
            httpClient = new HttpClient() { BaseAddress = new Uri(this.serviceBaseUrl) };
        }

        public class AccessTokenResponse
        {
            public int ret_code { get; set; }
            public int expire_seconds { get; set; }
            public string access_token { get; set; }
        }

        private async Task<AccessTokenResponse> RefreshAccessTokenAsync()
        {
            string response = await httpClient.GetStringAsync($"refreshaccesstoken?access_token={_accessToken}");
            return JsonConvert.DeserializeObject<AccessTokenResponse>(response);
        }
        private async Task<AccessTokenResponse> GetAccessTokenAsync()
        {
            string response = await httpClient.GetStringAsync($"getaccesstoken?client_id={clientId}&client_secret={clientSecret}");
            return JsonConvert.DeserializeObject<AccessTokenResponse>(response);
        }

        private string RenewAccessToken()
        {
            AccessTokenResponse ret;
            if (DateTime.Now > tokenExpireAt || _accessToken == null)
            {
                var task = GetAccessTokenAsync();
                task.Wait();
                ret = task.Result;
            } 
            else if(tokenExpireAt - DateTime.Now < TimeSpan.FromSeconds(50))
            {
                var task = RefreshAccessTokenAsync();
                task.Wait();
                ret = task.Result;
            }
            else
            {
                return _accessToken;
            }
            
            if (ret.ret_code == 0)
            {
                this._accessToken = ret.access_token;
                this.tokenExpireAt = DateTime.Now.AddSeconds(ret.expire_seconds - 10);
                return this._accessToken;
            }
            else
            {
                throw new AccessTokenException(ret.ret_code);
            }
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (httpClient != null)
                    {
                        httpClient.Dispose();
                    }

                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TokenService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

}
