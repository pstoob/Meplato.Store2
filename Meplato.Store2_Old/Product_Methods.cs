using System;
using System.Security;
using System.Reflection;
using System.Threading.Tasks;
using Meplato.Store2;
using Meplato.Store2.Catalogs;
using Meplato.Store2.Products;
using Polly;
using System.Security.Permissions;
using System.Configuration.Internal;
using Types;

namespace Meplato.Store2
{
    public class Product_Base_Methods

    {
        public Helper_Function _Helper { get; set; }

        public static Polly.Retry.AsyncRetryPolicy _Policy_Def(int retries)
        {
            Helper_Function _Helper = new Helper_Function();

            return   Policy.Handle<Exception>().WaitAndRetryAsync
                (
                    5,
                    attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2, attempt)),
                    (exception, waitTime) =>
                     {

                         retries++;
                         _Helper.PutLog(1, "Product_Base_Methods.Polly.Retry.AsyncRetryPolicy", $"Exception: {exception.Message}... retry #{retries} with a wait time of {waitTime}");
                         //Console.WriteLine($"Exception");
                     }
                );

        }


        public static Meplato.Store2.Products.Service GetProductsService(Client client)
        {
            Helper_Function _Helper = new Helper_Function();
            try
            {
                Application _config = new Application();

                var service = new Meplato.Store2.Products.Service(client)
                {
                    BaseURL = _config.Store_Url, // Environment.GetEnvironmentVariable("STORE_URL"),
                    User = _config.Store_User,   // Environment.GetEnvironmentVariable("STORE_USER"),
                    Password = _config.Store_Password //  Environment.GetEnvironmentVariable("STORE_PASSWORD"),
                };
                return service;
            }
            catch(Exception _ex)
            {
                _Helper.PutLog(1, "Product_Base_Methods.GetProductsService", _ex.Message);
                throw _ex;
            }
        }
    }

    public class Delete_Product_MP
    {
        Helper_Function _Helper { get; set; }

        public async Task Do_Delete(string _Product)
        {
            {
                // Siemens pricing use PLICRD_0 = '15346'

                #region Instantiate Environment and API Policy
                Application _config = new Application();
                string pin = _config.Catalog_Pin;  //   "87E3AE036B"; //> Test Catalog Pin //"39EA1EA9C0"; > Main Catalog Pin
                this._Helper = new Helper_Function();
                var _Client = new Client();

                var retries = 0;
                var _Policy = Product_Base_Methods._Policy_Def(retries);

                #endregion

                #region Core functions

                int successes = 0, failures = 0;
                try
                {
                    await _Policy.ExecuteAsync(async () =>
                    {
                        var _Prod_Service = Product_Base_Methods.GetProductsService(_Client);
                        try
                        {
                            await _Prod_Service.Delete().Pin(pin).Area("work").Spn(_Product).Do();
                            successes++;
                        }
                        catch (Meplato.Store2.ServiceException _ex)
                        {
                            _Helper.PutLog(1, "Product_Methods.Do_Delete", _ex.Message);
                            throw _ex;
                        }
                    }
                    );
                }
                catch (ServiceException _ex)
                {
                    failures++;
                    _Helper.PutLog(1, "Product_Methods.Do_Delete", _ex.Message);
                    throw _ex;

                    // Update queue on failure
                }
                catch (Exception _ex)
                {
                    _Helper.PutLog(1, "Product_Methods.Do_Delete", _ex.Message);
                    // Update queue on failure
                }
                #endregion
                //{
                //    Console.WriteLine($"{successes} successes, {failures} failures, {retries} retries");
                //}
            }
        }
    }

    public class Upsert_Product_MP
    {
        //------------------------------------
        //
        //------------------------------------
        public async static Task Do_Upsert(
            Types.Product.ProductsRow _ProductRow
            , int successes
            , int failures)
        {

            // Siemens pricing use PLICRD_0 = '15346'
            #region Instantiate Environment and API Policy
            Application _config = new Application();
            string pin = _config.Catalog_Pin;

            //System.Environment.SetEnvironmentVariable("STORE_USER", _config.Store_User); //API token
            //System.Environment.SetEnvironmentVariable("STORE_URL", _config.Store_Url); //Base Store URL, not sure this is necessary

            Helper_Function _Helper = new Helper_Function();

            var client = new Client();

            var retries = 0;
            //Product_Base_Methods _Product_Base = new Product_Base_Methods();
            //var _Policy = Product_Base_Methods._Policy_Def(retries);
            var _Policy = Policy.Handle<Exception>().WaitAndRetryAsync
                (
                    6,
                    attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2, attempt)),
                    (exception, waitTime) =>
                    {

                        retries++;
                        _Helper.PutLog(1, "Do_Upsert.Polly.Retry.AsyncRetryPolicy", $"Exception: {exception.Message}... retry #{retries} with a wait time of {waitTime}");
                        //Console.WriteLine($"Exception");
                    }
                );
            #endregion

            #region Core functions
            _Helper.PutLog(4, "Product_Methods.process product", _ProductRow.Spn);

            try
            {
                await _Policy.ExecuteAsync(async () =>
                {
                    var upsert = new UpsertProduct();

                    upsert.AutoConfigure = true;

                    upsert.ScalePrices = new ScalePrice[2];
                    upsert.ScalePrices[0] = new ScalePrice();
                    upsert.ScalePrices[0].Lbound = _ProductRow.QuantityMin;
                    upsert.ScalePrices[0].Price= _ProductRow.Price ;
                    upsert.ScalePrices[1] = new ScalePrice();
                    upsert.ScalePrices[1].Lbound = _ProductRow.QtyScale;
                    upsert.ScalePrices[1].Price = _ProductRow.ScalePrice;

                    upsert.Spn = _ProductRow.Spn;
                    upsert.Name = _ProductRow.Name;
                    upsert.Price = _ProductRow.Price;
                    upsert.PriceQty = _ProductRow.PriceQuantity;
                    upsert.QuantityMin = _ProductRow.QuantityMin;
                    upsert.QuantityInterval = _ProductRow.QuantityInterval;
                    
                    //upsert.QuantityMax = _ProductRow.QuantityMax;
                    upsert.Currency = _ProductRow.Currency;
                    upsert.OrderUnit = _ProductRow.OrderUnit;
                    upsert.Description = _ProductRow.Description;
                    upsert.Categories = new string[] { _ProductRow.Category1, _ProductRow.Category2 };
                    upsert.Mpn = _ProductRow.Mpn;
                    upsert.Manufacturer = _ProductRow.Manufacturer;
                    upsert.Leadtime = _ProductRow.LeadTime;
                    upsert.ErpGroupSupplier = "EAB";

                    //upsert.Eclasses = new Eclass[1];
                    //upsert.Eclasses[0] = new Eclass();
                    //upsert.Eclasses[0].Version = "10.1";
                    //upsert.Eclasses[0].Code = "20000000";

                    if (_ProductRow._Unspscs_Code != "")
                    {
                        upsert.Unspscs = new Unspsc[1];
                        upsert.Unspscs[0] = new Unspsc();
                        upsert.Unspscs[0].Version = "20.0601";
                        upsert.Unspscs[0].Code = _ProductRow._Unspscs_Code;
                    }
                    upsert.Visible = false;
                    //upsert.CatalogManaged = true
                    //upsert.Safetysheet = _ProductRow.MimeInfo;
                    upsert.Image = "";// _ProductRow.MimeInfo;
                    upsert.ImageURL =  _ProductRow.MimeInfo;
                    upsert.ContentUnit = _ProductRow.ContentUnit;

                    try
                    {
                        //_Helper.PutLog(4, "Product_Methods.Do_Upsert.beforeupsert", _ProductRow.Spn);
                        //var _ProdService = GetProductsService(client);
                        //UpsertProductResponse response = await _ProdService.Upsert().Pin(pin).Area("work").Product(upsert).Do();
                        UpsertProductResponse response = await UpsertProduct(client, pin, upsert.Spn, upsert);

                        //_Helper.PutLog(4, "response.Link" + response.Link, _ProductRow.Spn);
                        successes++;
                        //return response;
                    }
                    catch (Meplato.Store2.ServiceException _ex)
                    {
                        _Helper.PutLog(1, "Product_Methods.Do_Upsert.ServiceException.throw", _ex.Message);
                        throw _ex;
                    }
                });
            }
            catch (ServiceException _ex)
            {
                _Helper.PutLog(1, "Product_Methods.Do_Upsert.ServiceException", _ex.Message);
                failures++;
                throw _ex;
                // Update queue on failure
            }
            catch (Exception _ex)
            {
                _Helper.PutLog(1, "Product_Methods.Do_Upsert.Exception", _ex.Message);
                // Update queue on failure
                throw _ex;
            }
            #endregion

            //_Helper.PutLog(4, $"{successes} successes, {failures} failures, {retries} retries", _ProductRow.Spn);
            //return null;
        }

        private static async Task<UpsertProductResponse> UpsertProduct(Client client, string pin, string spn, UpsertProduct upsert)
        {
            try
            {
                return await GetProductsService(client).Upsert().Pin(pin).Area("work").Product(upsert).Do();
            }
            catch (Meplato.Store2.ServiceException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public static Meplato.Store2.Products.Service GetProductsService(Client client)
        {
            Helper_Function _Helper = new Helper_Function();
            try
            {
                Application _config = new Application();

                var service = new Meplato.Store2.Products.Service(client)
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

    public class Get_Product_MP
    { 
        public async Task Do_Get_Product(string _Product)
        {
            {
                // Siemens pricing use PLICRD_0 = '15346'

                #region Instantiate Environment and API Policy
                Application _config = new Application();
                string pin = _config.Catalog_Pin;  

                var _Client = new Client();
                Product_Base_Methods _Product_Methods = new Product_Base_Methods();
                var retries = 0;
                var _Policy = Product_Base_Methods._Policy_Def(retries);

                #endregion

                #region Core functions

                int successes = 0, failures = 0;
                try
                {
                    await _Policy.ExecuteAsync(async () =>
                    {
                        var _Prod_Service = Product_Base_Methods.GetProductsService(_Client);
                        try
                        {
                            await _Prod_Service.Get().Pin(pin).Area("work").Spn(_Product).Do();
                            successes++;
                        }
                        catch (Meplato.Store2.ServiceException ex)
                        {
                            Console.WriteLine(ex.Message);
                            throw;
                        }
                    }
                    );
                }
                catch (ServiceException e)
                {
                    Console.WriteLine($"ServiceException: {e.Message}");
                    failures++;
                    // Update queue on failure
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unknown Exception: {e.Message}");
                    // Update queue on failure
                }
                #endregion

                {
                    Console.WriteLine($"{successes} successes, {failures} failures, {retries} retries");
                }
            }
        }

        public static Meplato.Store2.Products.Service GetProductsService(Client client)
        {
            Helper_Function _Helper = new Helper_Function();
            try
            {
                Application _config = new Application();

                var service = new Meplato.Store2.Products.Service(client)
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
