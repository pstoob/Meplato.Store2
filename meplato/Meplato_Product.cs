using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meplato.Store2.Products;
using System.Runtime.Remoting;
using Meplato.Store2;
using System.Security;
using System.Security.Permissions;
using System.Configuration;

namespace Meplato_Base
{
    class Meplato_Product
    {

        public void Upsert_product()
        {
            try
            {
                Meplato.Store2.Upsert_Product _Test = new Meplato.Store2.Upsert_Product();

                Task _Task = _Test.Do_Upsert();

                if (_Task.IsCompletedSuccessfully == false)
                    _Task.Wait();
            }
            catch
            { 
            }
         }

        public void Delete_Product(string _Product)
        {
            try
            {

                Meplato.Store2.Delete_Product2 _Delete_Product = new Meplato.Store2.Delete_Product2();

                Task _Task = _Delete_Product.Do_Delete(_Product);
            }
            catch (Exception _e)
            {
            
            }
        }


        public void Get_Product(string _Product)
        {
            try
            {

                Meplato.Store2.Get_Product _Get_Product = new Meplato.Store2.Get_Product();

                Task _Task = _Get_Product.Do_Get(_Product);

                _Task.Wait();
            }
            catch
            {
            }
        }
    }
}
