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
using System.Web;


namespace Meplato.Store2
{

    //-------------------------------
    //
    //-------------------------------
    public class Catalog_Publish_MP
    {
        //private static readonly System.Net.Http.HttpClient _HttpClient = HttpClientFactory.Create();

        //-------------------------------
        //
        //-------------------------------
        public async Task<PublishResponse> Do_Publish_Catalog()
        {
            Application _config = new Application();
            var client = new Client();
            string pin = _config.Catalog_Pin;
            Helper_Function _Helper = new Helper_Function();
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
                        var service = GetCatalogsService(client);
                        var response = await service.Publish().Pin(pin).Do();
                        return response;
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

    }

    //-------------------------------
    // not used
    //-------------------------------
    public class Catalog_Get_MP
    {

        //-------------------------------
        //
        //-------------------------------
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

        //-------------------------------
        //
        //-------------------------------
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
 
    }
}
