using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meplato.Store2;
using Meplato.Store2.Catalogs;
using Polly;
using Types;

namespace Meplato.Store2
{
    public class Catalog_Publish_MP
    {

        public async  Task<PublishResponse> Do_Publish_Catalog()
        {
            Application _config = new Application();
            var client = new Client();
            string pin = _config.Catalog_Pin;
            Helper_Function _Helper = new Helper_Function();

            var retries = 0;
            var policy = Policy.Handle<Exception>().WaitAndRetryAsync(
                5,
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
                        //PublishResponse response = await GetCatalogsService(_Client).Publish().Pin(pin).Do();
                        PublishResponse response = await PublishCatalog(client, pin);
                        _Helper.PutLog(4, "Catalog_Publish_MP.PublishCatalog", response.Kind);
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
            return null;
        }

        private static async Task<PublishResponse> PublishCatalog(Client client, string pin)
        {
            Helper_Function _Helper = new Helper_Function();
            try
            {
                return await GetCatalogsService(client).Publish().Pin(pin).Do();
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
