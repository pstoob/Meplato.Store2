using System;
using System.Threading.Tasks;
using Meplato.Store2.Products;
using System.Runtime.Remoting;
using Meplato.Store2;
using System.Security;
using System.Security.Permissions;
using System.Configuration;
using meplato;



namespace Meplato_Base
{
    class Program
    {
        static void Main(string[] args)
        {
            //Meplato.Store2.Tests.Products.UpsertTests _prod;

            Meplato_Product _Product = new Meplato_Product();
            Sage_Access _Sage = new Sage_Access();

            _Sage.Get_Product();

            _Product.Delete_Product("101369");
            _Product.Upsert_product();

            _Product.Get_Product("101369");




        }
    }
}
