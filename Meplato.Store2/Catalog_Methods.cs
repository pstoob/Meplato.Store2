using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meplato.Store2;
using Meplato.Store2.Catalogs;
using Polly;
using Types;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace Meplato.Store2
{
    public class Catalog_Publish_MP
    {
        //private static readonly System.Net.Http.HttpClient _HttpClient = new HttpClient();

        public void Do_Publish()
        {
            //Helper_Function _Helper = new Helper_Function();
            //Thread _thr = new Thread(Do_Publish_Catalog);

            //_thr.Start();
            //_Helper.PutLog(4, "Main_Controler.Do_Publish", _thr.ThreadState.ToString());


        }
        //public async Task<PublishResponse> Do_Publish_Catalog()
            public async Task<PublishResponse> Do_Publish_Catalog()
        {
            Application _config = new Application();
            var client = new Client();
            string pin = _config.Catalog_Pin;
            Helper_Function _Helper = new Helper_Function();
            _Helper.PutLog(4, "Catalog_Publish_MP.PublishCatalog", "Entry");
            var retries = 0;

            var policy = Policy.Handle<Exception>().WaitAndRetryAsync(
                2,
                attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2, attempt)),
                (exception, waitTime) =>
                {
                    retries++;
                    _Helper.PutLog(1, $"Exception: {exception.Message}... retry #{retries} with a wait time of {waitTime}", "");
                }
            );

            //  Publish Meplato Catalog
            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    try
                    {
                        _Helper.PutLog(4, "Catalog_Publish_MP.PublishCatalog", "before Publish");
                        //var service = GetCatalogsService(client);
                        //var response = await service.Publish().Pin(pin).Do();
                        //var response = await GetCatalogsService(client).Publish().Pin(pin).Do();
                        var response = await PublishCatalog(client, pin);
                        //var response = await SendRequest();
                        _Helper.PutLog(4, "Catalog_Publish_MP.PublishCatalog", "After publish");
                        //return response;
                    }
                    catch (Meplato.Store2.ServiceException _ex)
                    {
                        _Helper.PutLog(1, "Catalog_Publish_MP.PublishCatalog", _ex.Message);
                        throw _ex;
                    }
                }
                 );
            }
            catch (ServiceException _ex)
            {
                _Helper.PutLog(1, "Catalog_Publish_MP.PublishCatalog", _ex.Message);
                throw _ex;
            }
            _Helper = null;
            return null;
        }

        private static async Task<PublishResponse> PublishCatalog(Client client, string pin)
        {
            Helper_Function _Helper = new Helper_Function();
            try
            {
                //return await GetCatalogsService(client).Publish().Pin(pin).Do();
                var response = await SendRequest(pin);
                return response.GetBodyJSON<PublishResponse>();
            }
            catch (ServiceException _ex)
            {
                _Helper.PutLog(1, "Catalog_Publish_MP.PublishCatalog", _ex.Message);
                throw _ex;
            }
            finally
            { _Helper = null; }
        }

        private static Meplato.Store2.Catalogs.Service GetCatalogsService(Client client)
        {
            Helper_Function _Helper = new Helper_Function();
            try
            {
                Application _config = new Application();
                var service = new Meplato.Store2.Catalogs.Service(client)
                {
                    BaseURL = _config.Store_Url,
                    User = _config.Store_User,
                    Password = _config.Store_Password
                };
                return service;
            }
            catch (Exception _ex)
            {
                _Helper.PutLog(1, "Catalog_Publish_MP.GetCatalogsService", _ex.Message);
                throw _ex;
            }
            finally
            { _Helper = null; }
        }

        private static async  Task<IResponse>  SendRequest(string pin)


        {
            const string UserAgent = "meplato-api-csharp-client/2.2.0";
            Application _config = new Application();
             HttpClient _HttpClient = HttpClientFactory.Create( HttpMessageHandler(MSGHAND));


            // Always use application/json
            _HttpClient.DefaultRequestHeaders.Accept.Clear();
            _HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x"));

            string url = "https://store.meplato.com/api/v2/catalogs/" + pin + "/publish ";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent("", Encoding.UTF8, "application/json");
            request.Version = new System.Version("1.0");
            request.Headers.Add("Authorization", "'Basic ZDFiMTgxODU1OGM3MTEzNDoiIg=='");
            request.Headers.Add("User-Agent", "'meplato-api-csharp-client/2.2.0'");
           
            //request.Headers.Add("Content-Type", "application/json");
            //request.Headers.Add("Content-Length", "0");
            //request.Headers.Add("Host", "store.meplato.com");
            //request.Headers.Add("Connection", "Keep-Alive");

            try
            {
                //_httpClient.BaseAddress = new Uri( "store.meplato.com");
                
                var httpResponse = await _HttpClient.SendAsync(request);

                //var response = await httpResponse.Content.ReadAsStringAsync();

                var _response = new Response(httpResponse);

                if (httpResponse.IsSuccessStatusCode)
                    return _response;

                throw new Exception("not successful");
            }
            catch (HttpRequestException _re)
            {
                throw _re;
            }
            catch (ServiceException _se)
            {
                throw _se;
            }
            catch (Exception e)
            {
                throw new ServiceException("Request failed", null, e);
            }

        }

        private static HttpMessageHandler MSGHAND()
        {
            throw new NotImplementedException();
        }

        public static DelegatingHandler[] HttpMessageHandler(Func<HttpMessageHandler> MSGHAND)
        {
            

            throw new NotImplementedException();
        }

  
    }


    public class Catalog_Get_MP

    {

        public static async Task<Meplato.Store2.Catalogs.Catalog> GetCatalog()
        {
            try
            {
                Application _config = new Application();
                var _Client = new Client();
                string pin = _config.Catalog_Pin;

                var catalog = await GetCatalogsService(_Client).Get().Pin(pin).Do();
                return catalog;
            }
            catch (Meplato.Store2.ServiceException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public static Meplato.Store2.Catalogs.Service GetCatalogsService(Client client)
        {
            Helper_Function _Helper = new Helper_Function();
            try
            {
                Application _config = new Application();
                var service = new Meplato.Store2.Catalogs.Service(client)
                {
                    BaseURL = _config.Store_Url,
                    User = _config.Store_User,
                    Password = _config.Store_Password
                };
                return service;
            }
            catch (Exception _ex)
            {
                _Helper.PutLog(1, "Product_Base_Methods.GetProductsService", _ex.Message);
                throw _ex;
            }
        }
        //public async Task _Publish_Catalog()
        //{
        //    Application _config = new Application();
        //    var _Client = new Client();
        //    string pin = _config.Catalog_Pin;

        //    var retries = 0;
        //    var policy = Policy.Handle<Exception>().WaitAndRetryAsync(
        //        5,
        //        attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2, attempt)),
        //        (exception, waitTime) =>
        //        {
        //            retries++;
        //            Console.WriteLine($"Exception: {exception.Message}... retry #{retries} with a wait time of {waitTime}");
        //        }
        //    );

        //    //  Publish Meplato Catalog
        //    await policy.ExecuteAsync(async () =>
        //     {
        //         try
        //         {
        //             PublishResponse response = await GetCatalogsService(_Client).Publish().Pin(pin).Do();
        //         }
        //         catch (Meplato.Store2.ServiceException ex)
        //         {
        //             Console.WriteLine(ex.Message);
        //             throw;
        //         }
        //     }
        //     );
        //}
    }
}
