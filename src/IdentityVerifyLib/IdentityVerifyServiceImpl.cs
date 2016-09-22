using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IdentityVerifyLib.Data;
using System.Text;

namespace IdentityVerifyLib
{
    public class IdentityVerifyServiceImpl : IIdentityVerifyService, IDisposable
    {
        private readonly TokenService tokenService;

        private HttpClient httpClient;

        public IdentityVerifyServiceImpl(TokenService tokenService, Options options)
        {
            this.tokenService = tokenService;
            httpClient = new HttpClient() { BaseAddress = new Uri(options.ServiceBaseUrl) };
        }

        public AuthResult VerifyId(string idNum, string fullName)
        {
            var task = VerifyIdAsync(idNum, fullName);
            task.Wait();
            return task.Result;
        }

        public async Task<AuthResult> VerifyIdAsync(string idNum, string fullName)
        {
            var accessToken = tokenService.AccessToken;
            var authRequest = new AuthRequest()
            {
                AccessToken = accessToken,
                FullName = fullName,
                IdNumber = idNum,
                Mode = 64
            };
            var response = await httpClient.PostAsync("auth", new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(authRequest))));
            var ret = await response.Content.ReadAsStringAsync();
            var r = JsonConvert.DeserializeObject<AuthResponse>(ret);
            return r.ToAuthResult();
        }

        public AuthResult VerifyIdWithImage(string idNum, string fullName, string image)
        {
            var task = VerifyIdWithImageAsync(idNum, fullName, image);
            task.Wait();
            return task.Result;
        }

        public async Task<AuthResult> VerifyIdWithImageAsync(string idNum, string fullName, string image)
        {
            var accessToken = tokenService.AccessToken;
            var authRequest = new AuthRequest()
            {
                AccessToken = accessToken,
                FullName = fullName,
                IdNumber = idNum,
                Mode = 66,
                Portrait = image
            };
            var body = JsonConvert.SerializeObject(authRequest);
            ByteArrayContent bc = new ByteArrayContent(Encoding.UTF8.GetBytes(body));
            bc.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
   
            var response = await httpClient.PostAsync("auth", bc);
            var ret = await response.Content.ReadAsStringAsync();
            var r = JsonConvert.DeserializeObject<AuthResponse>(ret);
            return r.ToAuthResult();
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
                    this.httpClient.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~IdentityVerifyServiceImpl() {
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
