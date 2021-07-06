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
    //-------------------------------
    // Not used
    //-------------------------------
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

        //-------------------------------
        //
        //-------------------------------
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
            catch(Exception _ex)
            {
                _Helper.PutLog(1, "Product_Base_Methods.GetProductsService", _ex.Message);
                throw _ex;
            }
        }
    }

    //-------------------------------
    //Not used
    //-------------------------------
    public class Delete_Product_MP
    {
        Helper_Function _Helper { get; set; }

        //-------------------------------
        //
        //-------------------------------
        public async Task Do_Delete(string _Product)
        {
            {
  
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

    //-------------------------------
    // insert or update
    //-------------------------------
    public class Upsert_Product_MP
    {

        public int successes { get; set; }
        public int failures { get; set; }

        //------------------------------------
        //
        //------------------------------------
        public async  Task<UpsertProductResponse> Do_Upsert(
            Types.Product.ProductsRow _ProductRow
            )
        {

            #region Instantiate Environment and API Policy
            Application _config = new Application();
            string pin = _config.Catalog_Pin;

             Helper_Function _Helper = new Helper_Function();

            var client = new Client();

            var retries = 0;
 
            var _Policy = Policy.Handle<Exception>().WaitAndRetryAsync
                (
                    4,
                    attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2, attempt)),
                    (exception, waitTime) =>
                    {
                        retries++;
                        _Helper.PutLog(1, "Do_Upsert.Polly.Retry.AsyncRetryPolicy", $"Exception: {exception.Message}... retry #{retries} with a wait time of {waitTime}");
                    }
                );
            #endregion

            #region Core functions
  
            try
            {
                await _Policy.ExecuteAsync(async () =>
                {
                    var upsert = new UpsertProduct();

                    upsert.AutoConfigure = false;
                    if (_ProductRow.MimeInfo != "")
                    {
                        upsert.Blobs = new Blob[1];
                        upsert.Blobs[0] = new Blob();
                        upsert.Blobs[0].Kind = "normal";
                        upsert.Blobs[0].Language = "en";
                        upsert.Blobs[0].Source = _ProductRow.MimeInfo;
                        upsert.Blobs[0].Url = _ProductRow.MimeInfo;
                    }
                    if (_ProductRow.QtyScale != 0 & _ProductRow.ScalePrice != 0)
                    {
                        upsert.ScalePrices = new ScalePrice[2];
                        upsert.ScalePrices[0] = new ScalePrice();
                        upsert.ScalePrices[0].Lbound = _ProductRow.QuantityMin;
                        upsert.ScalePrices[0].Price = _ProductRow.Price;
                        upsert.ScalePrices[1] = new ScalePrice();
                        upsert.ScalePrices[1].Lbound = _ProductRow.QtyScale;
                        upsert.ScalePrices[1].Price = _ProductRow.ScalePrice;
                    }
                    else
                    {
                        upsert.ScalePrices = new ScalePrice[1];
                        upsert.ScalePrices[0] = new ScalePrice();
                        upsert.ScalePrices[0].Lbound = _ProductRow.QuantityMin;
                        upsert.ScalePrices[0].Price = _ProductRow.Price;
                    }

                    //if (_ProductRow.Spn== "103785") { }
                    upsert.Spn = _ProductRow.Spn;
                    upsert.Mpn = _ProductRow.Spn;
                    if (_ProductRow.Name.Length > 80)
                    { _ProductRow.Name = _ProductRow.Name.Substring(0, 80); }
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
                    upsert.Bpn = _ProductRow.Mpn;
                    upsert.Manufacturer = _ProductRow.Manufacturer;
                    upsert.Leadtime = _ProductRow.LeadTime;
                    upsert.ErpGroupSupplier = "EAB";

                    upsert.Eclasses = new Eclass[1];
                    upsert.Eclasses[0] = new Eclass();
                    upsert.Eclasses[0].Version = "10.1";
                    upsert.Eclasses[0].Code = "20000000";

                    if (_ProductRow._Unspscs_Code != "")
                    {
                        upsert.Unspscs = new Unspsc[1];
                        upsert.Unspscs[0] = new Unspsc();
                        upsert.Unspscs[0].Version = "20.0601";
                        upsert.Unspscs[0].Code = _ProductRow._Unspscs_Code;
                    }
                    upsert.Visible = true;
              
                    upsert.Image =  _ProductRow.MimeInfo;

                    if (_ProductRow.CuPerOu > 0)  // stock unit not used
                    {
                        //upsert.ContentUnit = _ProductRow.ContentUnit;
                        //upsert.CuPerOu = _ProductRow.CuPerOu;
                    }
                    else
                    {
                        //upsert.ContentUnit = _ProductRow.OrderUnit;
                        //upsert.CuPerOu = _ProductRow.PriceQuantity;
                    }

                    try
                    {
                        UpsertProductResponse response = await UpsertProduct(client, pin, upsert.Spn, upsert);
                        successes++;
                        return response;
                    }
                    catch (Meplato.Store2.ServiceException _ex)
                    {
                        _Helper.PutLog(1, "Product_Methods.Do_Upsert.ServiceException.throw " + _ProductRow.Spn, _ex.Message);
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
                failures++;
                throw _ex;
            }
            #endregion

            //_Helper.PutLog(4, $"{successes} successes, {failures} failures, {retries} retries", _ProductRow.Spn);
            return null;
        }

        //-------------------------------
        //
        //-------------------------------
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

        //-------------------------------
        //
        //-------------------------------
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

    //-------------------------------
    // Not used yet
    //-------------------------------
    public class Get_Product_MP
    {

        //-------------------------------
        //
        //-------------------------------
        public async static Task Do_Get_Product(string _Product)
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

        //-------------------------------
        //
        //-------------------------------
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

    //-------------------------------
    // Not used
    //-------------------------------
    public class Update_Product_MP
    {

        //-------------------------------
        //
        //-------------------------------
        public static async  Task Do_Update_Product(Types.Product.ProductsRow _ProductRow)
        {
            {
                // Siemens pricing use PLICRD_0 = '15346'

                #region Instantiate Environment and API Policy
                Application _config = new Application();
                string pin = _config.Catalog_Pin;
                Helper_Function _Helper = new Helper_Function();

                var _Client = new Client();
                //Product_Base_Methods _Product_Methods = new Product_Base_Methods();
                var retries = 0;
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

                int successes = 0, failures = 0;
                try
                {
                    await _Policy.ExecuteAsync(async () =>
                    {
                        try
                        {
                            string _Product;
                            var _UpdateProduct = new Products.UpdateProduct();

                            _Product = _ProductRow.Spn;
                            //_UpdateProduct.ResetImage();
                            _UpdateProduct.Image = _ProductRow.MimeInfo;
                            UpdateProductResponse response = await UpdateProduct(_Client, pin, _Product, _UpdateProduct);

                            //await _Prod_Service.Get().Pin(pin).Area("work").Spn(_Product).Do();
                            successes++;
                            return response;
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

        //-------------------------------
        //
        //-------------------------------
        private static async Task<UpdateProductResponse> UpdateProduct(Client client, string pin, string spn, UpdateProduct update)
        {
            try
            {
                return await GetProductsService(client).Update().Pin(pin).Area("work").Spn(spn).Product(update).Do();
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






