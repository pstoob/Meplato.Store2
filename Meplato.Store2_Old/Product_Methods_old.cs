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
    public class Product_Methods_old
    {
        public async Task Do_Delete(string _Product)
        {
            {
                // Siemens pricing use PLICRD_0 = '15346'
                #region Instantiate Environment and API Policy
                const string pin = "87E3AE036B"; //> Test Catalog Pin //"39EA1EA9C0"; > Main Catalog Pin
                System.Environment.SetEnvironmentVariable("STORE_USER", "d1b1818558c71134"); //API token
                System.Environment.SetEnvironmentVariable("STORE_URL", "https://store.meplato.com/api/v2"); //Base Store URL, not sure this is necessary

                var _Client = new Client();

                var retries = 0;
                var policy = Policy.Handle<Exception>().WaitAndRetryAsync
                (
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


                int successes = 0, failures = 0;
                try
                {
                    // Loop datatable and Update/Create
                    //await policy.ExecuteAsync(async () =>
                    //{
                    //    var catalog = await GetCatalog(client, pin);
                    //    Console.WriteLine(catalog.Name);
                    //    successes++;
                    //});

                    await policy.ExecuteAsync(async () =>
                    {
                        var delete = GetProductsService(_Client);
                        try
                        {
                            await delete.Delete().Pin(pin).Area("work").Spn(_Product).Do();
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

                //catch
                {
                    Console.WriteLine($"{successes} successes, {failures} failures, {retries} retries");
                    //     } // end class 
                }

                Meplato.Store2.Products.Service GetProductsService(Client client)
                {
                    var service = new Meplato.Store2.Products.Service(client)
                    {
                        BaseURL = Environment.GetEnvironmentVariable("STORE_URL"),
                        User = Environment.GetEnvironmentVariable("STORE_USER"),
                        Password = Environment.GetEnvironmentVariable("STORE_PASSWORD"),
                    };
                    return service;
                }
            }
        }
    }

    public class Get_Product
    {
        public async Task Do_Get(string _Product)
        {
            {
                // Siemens pricing use PLICRD_0 = '15346'
                #region Instantiate Environment and API Policy
                const string pin = "87E3AE036B"; //> Test Catalog Pin //"39EA1EA9C0"; > Main Catalog Pin
                System.Environment.SetEnvironmentVariable("STORE_USER", "d1b1818558c71134"); //API token
                System.Environment.SetEnvironmentVariable("STORE_URL", "https://store.meplato.com/api/v2"); //Base Store URL, not sure this is necessary

                var _Http_Client = new Client();

                var retries = 0;
                var policy = Policy.Handle<Exception>().WaitAndRetryAsync
                (
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


                int successes = 0, failures = 0;
                try
                {
                    // Loop datatable and Update/Create
                    //await policy.ExecuteAsync(async () =>
                    //{
                    //    var catalog = await GetCatalog(client, pin);
                    //    Console.WriteLine(catalog.Name);
                    //    successes++;
                    //});

                    await policy.ExecuteAsync(async () =>
                    {
                        var product = await GetProduct(_Http_Client, pin, _Product);
                        Console.WriteLine(
                            $"{product.Spn}; {product.Name}; {product.Price} {product.Currency}; {product.Manufacturer}");
                        successes++;
                    });
                }
                // Update queue on success
                //            }
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

                //catch
                {
                    Console.WriteLine($"{successes} successes, {failures} failures, {retries} retries");
                    //     } // end class 
                }

                  async Task<Meplato.Store2.Products.Product> GetProduct(Client client, string _Pin, string spn)
                {
                    try
                    {
                        var product = await GetProductsService(client).Get().Pin(_Pin).Area("work").Spn(spn).Do();
                        return product;
                    }
                    catch (Meplato.Store2.ServiceException ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                }
                

                Meplato.Store2.Products.Service GetProductsService(Client client)
                {
                    var service = new Meplato.Store2.Products.Service(client)
                    {
                        BaseURL = Environment.GetEnvironmentVariable("STORE_URL"),
                        User = Environment.GetEnvironmentVariable("STORE_USER"),
                        Password = Environment.GetEnvironmentVariable("STORE_PASSWORD"),
                    };
                    return service;
                }
            }
        }
    }
}
            
