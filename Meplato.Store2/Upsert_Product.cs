
using System;
using System.Security;
using System.Reflection;
using System.Threading.Tasks;
using Meplato.Store2;
using Meplato.Store2.Catalogs;
using Meplato.Store2.Products;
using Polly;
using System.Security.Permissions;

namespace Meplato.Store2
{
    public class Upsert_Product 
    {
        public async Task Do_Upsert()
        {
            // Siemens pricing use PLICRD_0 = '15346'
            #region Instantiate Environment and API Policy
            const string pin = "6AF4CCF8D0"; //> Test Catalog Pin //"39EA1EA9C0"; > Main Catalog Pin
            System.Environment.SetEnvironmentVariable("STORE_USER", "d1b1818558c71134"); //API token
            System.Environment.SetEnvironmentVariable("STORE_URL", "https://store.meplato.com/api/v2"); //Base Store URL, not sure this is necessary

            var client = new Client();

            var retries = 0;
            var policy = Policy.Handle<Exception>().WaitAndRetryAsync(
                5,
                attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2, attempt)),
                (exception, waitTime) =>
                {
                    retries++;
                    Console.WriteLine($"Exception: {exception.Message}... retry #{retries} with a wait time of {waitTime}");
                }
            );
            // Load queue datatable from Sage

            #endregion

            #region Core functions
            // Load New Rows
            // Load Changed Rows
            // Create new items
            // Update changed items
            {
                try
                {
                    var catalog = await GetCatalogsService(client).Get().Pin(pin).Do();
                    //return catalog;
                }
                catch (Meplato.Store2.ServiceException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            int successes = 0, failures = 0;
            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    var upsert = new UpsertProduct();
      
                    upsert.Spn = "101369";
                    upsert.Name = "10 x 12\" - 6 mil Reclosable Bags, 500 / Box";
                    upsert.Price = 193;
                    upsert.Currency = "USD";
                    upsert.OrderUnit = "PK";
                    upsert.Description = "10 x 12\" - 6 mil Reclosable Bags, 500 / Box";
                    upsert.Categories = new string[] { "Bags and Sheeting", "Reclosable (Ziplock) Bags" };
                    upsert.Mpn = "PB3884";
                    upsert.Manufacturer = "Box Partners, LLC";
                    upsert.Leadtime = 1;
                    upsert.ErpGroupSupplier = "EAB";
                    upsert.Eclasses = new Eclass[1];
                    upsert.Eclasses[0] = new Eclass();
                    upsert.Eclasses[0].Version = "10.1";
                    upsert.Eclasses[0].Code = "20000000";
                    upsert.Unspscs = new Unspsc[1];
                    upsert.Unspscs[0] = new Unspsc();
                    upsert.Unspscs[0].Version = "20.0601";
                    upsert.Unspscs[0].Code = "30251503";
                    upsert.ContentUnit = "1";

                    try
                    {
                        UpsertProductResponse response = await UpsertProduct(client, pin, upsert.Spn, upsert);
                        successes++;
                    }
                    catch (Meplato.Store2.ServiceException ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                });

                //await policy.ExecuteAsync(async () =>
                //{
                //    var product = await GetProduct(client, pin, "100007");
                //    Console.WriteLine(
                //        $"{product.Spn}; {product.Name}; {product.Price} {product.Currency}; {product.Manufacturer}");
                //    successes++;
                //});
                // Update queue on success
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

            // Publish Meplato Catalog
            //await policy.ExecuteAsync(async () =>
            //{
            //    try
            //    {
            //        PublishResponse response = await PublishCatalog(client, pin);
            //    }
            //    catch (Meplato.Store2.ServiceException ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        throw;
            //    }
            //}
            //);

            Console.WriteLine($"{successes} successes, {failures} failures, {retries} retries");
        } // end class 

        #region CatalogsAPI
        private static async Task<Meplato.Store2.Catalogs.Catalog> GetCatalog(Client client, string pin)
        {
            try
            {
                var catalog = await GetCatalogsService(client).Get().Pin(pin).Do();
                return catalog;
            }
            catch (Meplato.Store2.ServiceException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static async Task<Meplato.Store2.Catalogs.PublishResponse> PublishCatalog(Client client, string pin)
        {
            try
            {
                return await GetCatalogsService(client).Publish().Pin(pin).Do();
            }
            catch (Meplato.Store2.ServiceException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static Meplato.Store2.Catalogs.Service GetCatalogsService(Client client)
        {
            var service = new Meplato.Store2.Catalogs.Service(client)
            {
                BaseURL = Environment.GetEnvironmentVariable("STORE_URL"),
                User = Environment.GetEnvironmentVariable("STORE_USER"),
                Password = Environment.GetEnvironmentVariable("STORE_PASSWORD"),
            };
            return service;
        }

        #endregion

        #region ProductsAPI

        private static async Task<Meplato.Store2.Products.Product> GetProduct(Client client, string pin, string spn)
        {
            try
            {
                var product = await GetProductsService(client).Get().Pin(pin).Area("work").Spn(spn).Do();
                return product;
            }
            catch (Meplato.Store2.ServiceException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

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

        private static async Task<CreateProductResponse> CreateProduct(Client client, string pin, string spn, CreateProduct create)
        {
            try
            {
                return await GetProductsService(client).Create().Pin(pin).Area("work").Product(create).Do();
            }
            catch (Meplato.Store2.ServiceException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static Meplato.Store2.Products.Service GetProductsService(Client client)
        {
            
            var service = new Meplato.Store2.Products.Service(client)
            {
                BaseURL = Environment.GetEnvironmentVariable("STORE_URL"),
                User = Environment.GetEnvironmentVariable("STORE_USER"),
                Password = Environment.GetEnvironmentVariable("STORE_PASSWORD"),
            };
            return service;
        }
        #endregion
    }
}
