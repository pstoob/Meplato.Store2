#region Copyright and terms of services

// Copyright (c) 2015 Meplato GmbH.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except
// in compliance with the License. You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
// or implied. See the License for the specific language governing permissions and limitations under
// the License.

#endregion

using System.Threading.Tasks;
using Meplato.Store2.Products;
using NUnit.Framework;
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Security.Permissions;

namespace Meplato.Store2.Tests.Products
{
    [TestFixture]
    public class UpsertTests : TestCase
    {
        [Test]
        public async Task TestUpsert()
        {
            //MockFromFile("products.upsert.success");

            var service = GetProductsService();
            Assert.NotNull(service);

            Eclass[] Eclasses = new Eclass[1];
            Eclasses[0] = new Eclass();

            Unspsc[] Unspscs = new Unspsc[1];
            Unspscs[0] = new Unspsc();

            var data = new UpsertProduct();
            
                //Spn = "1000",
                //Name = "Produkt 1000",
                //Price = 4.99,
                //OrderUnit = "PCE"

            data.Spn = "101369";
            data.Name = "10 x 12\" - 6 mil Reclosable Bags, 500 / Box";
            data.Price = 193;
            data.OrderUnit = "PK";
            data.Description = "10 x 12\" - 6 mil Reclosable Bags, 500 / Box";
            data.Categories = new string[] { "Bags and Sheeting", "Reclosable (Ziplock) Bags" };
            data.Mpn = "PB3884";
            data.Manufacturer = "Box Partners, LLC";
            data.Leadtime = 1;
            data.ErpGroupSupplier = "EAB";
            Eclasses[0].Version = "10.1";
            Eclasses[0].Code = "20000000";
            data.Eclasses = Eclasses;
            Unspscs[0].Version = "20.0601";
            Unspscs[0].Code = "30251503";
            data.ContentUnit = "1";

        
             //var response = await service.Upsert().Pin("AD8CCDD5F9").Area("work").Product(data).Do();
             var response = await service.Upsert().Pin("87E3AE036B").Area("work").Product(data).Do();
            
            Assert.NotNull(response);
            Assert.IsNotNull(response.Link);
            Assert.IsNotEmpty(response.Link);
            Assert.AreEqual("store#productsUpsertResponse", response.Kind);
        }
    }
}