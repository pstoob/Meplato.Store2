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

        public void Do_Work ()
        
        {
            string _CurProduct = null;
            try
            {
                _Helper = new Helper_Function();
                Sage_Data _Sage_Data = new Sage_Data();
                Types.Product _Product;
                var _watch = new System.Diagnostics.Stopwatch();
                long _minTime = 6;
                Meplato.Store2.Upsert_Product_MP _Do_Upsert_Product = new Meplato.Store2.Upsert_Product_MP();
                Meplato.Store2.Catalog_Publish_MP _Catalog_Publish = new Meplato.Store2.Catalog_Publish_MP();

                _Do_Upsert_Product.successes = 0;
                _Do_Upsert_Product.failures = 0;

                ////Run_Thread();
                //Publish();
                //Task _Cat = Meplato.Store2.Catalog_Publish_MP.Do_Publish_Catalog();

                //_Helper.PutLog(4, "Main_Controler.Do_Work", "Start update of Meplato");
                //_CurProduct = "";

                //_Product = _Sage_Data.Get_Siemens_Product(_CurProduct);

                //if (_Product != null)
                //{
                //    while (_Product.Products.Rows.Count > 0)
                //    {
                //        foreach (Types.Product.ProductsRow _ProductsRow in _Product.Products.Rows)
                //        {
                //            try
                //            {
                //                _watch.Restart();
                //                Task _Rsp = _Do_Upsert_Product.Do_Upsert(_ProductsRow);
                //                _watch.Stop();
                //                if (_watch.ElapsedMilliseconds < _minTime)
                //                { Thread.Sleep(Convert.ToInt32(_minTime - _watch.ElapsedMilliseconds)); }
                //            }
                //            catch
                //            { }
                //            finally
                //            { _CurProduct = _ProductsRow.Spn; }
                //        }

                //        _Helper.PutLog(4, "Main_Controler.Do_Work", "Get next batch of products: " + _CurProduct);

                //        _Product = null;
                //        _Product = _Sage_Data.Get_Siemens_Product(_CurProduct);
                //        if (_Product == null)
                //        { break; }
                //    }
                //    Thread.Sleep(4000);
                //    _Helper.PutLog(4, "Main_Controler.Do_Work", "Products processed success: " + _Do_Upsert_Product.successes.ToString() + " failures: " + _Do_Upsert_Product.failures.ToString());
                //}
                //else
                //{
                //    _Helper.PutLog(1, "Main_Controler.Do_Work", "Product dataset from Sage is null");
                //}


                //Meplato.Store2.Catalog_Publish_MP.Do_Publish_Catalog();

                Task  _pub = _Catalog_Publish.Do_Publish_Catalog();

            }
            catch (Exception _ex)
                {
                _Helper.PutLog(1, "Main_Controler.Do_Work", _ex.Message, "Current product: " + _CurProduct);   
                }
            finally 
            {
                _Helper.PutLog(4, "Main_Controler.Do_Work", "End update of Meplato");
            }
        }

        //void Do_Publish()
        //{

        //    Thread _thr = new Thread(Meplato.Store2.Catalog_Publish_MP.Do_Publish_Catalog);

        //    _thr.Start();
        //    _Helper.PutLog(4, "Main_Controler.Do_Publish", _thr.ThreadState.ToString()); 


        //}
        // async Task <Meplato.Store2.Catalogs.PublishResponse> Do_Publish()

        //{
        //    Meplato.Store2.Catalog_Publish_MP _Catalog_Publish = new Meplato.Store2.Catalog_Publish_MP();

        //    //Meplato.Store2.Catalogs.PublishResponse _pub = await _Catalog_Publish.Do_Publish_Catalog();

        //    //return _pub;
        //}

    }
}
