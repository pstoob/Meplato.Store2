using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Sage;
using Types;


namespace Main_Controler
{
    public class Main_Controler
    {
        Helper_Function _Helper { get; set; }


        //---------------------------------
        // Fetch the products from Sage and use upsert for maplato
        // at the end publish the catalog
        //---------------------------------
        public void Do_Work ()
        
        {
            string _CurProduct = null;
            try
            {
                _Helper = new Helper_Function();
                Main _Config = new Main();
                Sage_Data _Sage_Data = new Sage_Data();
                Types.Product _Product;
                var _watch = new System.Diagnostics.Stopwatch();
                long _minTime = _Config.MinTime;
                int _Records = 0;
                Meplato.Store2.Upsert_Product_MP _Do_Upsert_Product = new Meplato.Store2.Upsert_Product_MP();
                Meplato.Store2.Catalog_Publish_MP _Catalog_Publish = new Meplato.Store2.Catalog_Publish_MP();

                _Do_Upsert_Product.successes = 0;
                _Do_Upsert_Product.failures = 0;

                Console.WriteLine("Start update of Meplato");
                _Helper.PutLog(4, "Main_Controler.Do_Work", "Start update of Meplato");
                _CurProduct = "";

                _Product = _Sage_Data.Get_Siemens_Product(_CurProduct);

                if (_Product != null)
                {
                    while (_Product.Products.Rows.Count > 0)
                    {
                        foreach (Types.Product.ProductsRow _ProductsRow in _Product.Products.Rows)
                        {
                            try
                            {
                                _watch.Restart();
                                Task _Rsp = _Do_Upsert_Product.Do_Upsert(_ProductsRow);
                                _watch.Stop();
                                if (_watch.ElapsedMilliseconds < _minTime)
                                { Thread.Sleep(Convert.ToInt32(_minTime - _watch.ElapsedMilliseconds)); }
                                _Records ++;
                            }
                            catch
                            { }
                            finally
                            { _CurProduct = _ProductsRow.Spn; }
                        }

                        Console.WriteLine("Get next batch of products: " + _CurProduct);
                        _Helper.PutLog(4, "Main_Controler.Do_Work", "Get next batch of products: " + _CurProduct);

                        _Product = null;
                        _Product = _Sage_Data.Get_Siemens_Product(_CurProduct);
                        if (_Product == null)
                        { break; }
                    }
                    Thread.Sleep(_Config.WaitForEnd); // wait that all threads are finished
                    _Helper.PutLog(4, "Main_Controler.Do_Work", "Products processed success: " + _Records.ToString() + " failures: " + _Do_Upsert_Product.failures.ToString());
                }
                else
                {
                    _Helper.PutLog(1, "Main_Controler.Do_Work", "Product dataset from Sage is null");
                }

                Task  _pub = _Catalog_Publish.Do_Publish_Catalog();
                Thread.Sleep(_Config.WaitAfterPublish);  // wait for the end before killing the object 
            }
            catch (Exception _ex)
                {
                _Helper.PutLog(1, "Main_Controler.Do_Work", _ex.Message, "Current product: " + _CurProduct);   
                }
            finally 
            {
                Console.WriteLine("End update of Meplato");
                _Helper.PutLog(4, "Main_Controler.Do_Work", "End update of Meplato");
            }
        }

    }
}
