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
using NUnit.Framework;

namespace Meplato.Store2.Tests.Products
{
    [TestFixture]
    public class SearchTests : TestCase
    {
        [Test]
        public async Task SearchUpdate()
        {
            var service = GetProductsService();
            Assert.NotNull(service);

            MockFromFile("products.search.success");
            var response = await service.Search().Pin("AD8CCDD5F9").Area("work").Q("toner").Skip(0).Take(30).Do();
            Assert.NotNull(response);
            Assert.AreEqual("store#products", response.Kind);
            Assert.IsTrue(response.TotalItems > 0);
            Assert.NotNull(response.Items);
            foreach (var product in response.Items)
            {
                Assert.IsNotNull(product);
                Assert.IsNotNull(product.Id);
                Assert.IsNotEmpty(product.Id);
                Assert.AreEqual("store#product", product.Kind);
                Assert.IsNotNull(product.SelfLink);
                Assert.IsNotEmpty(product.SelfLink);
                Assert.IsNotNull(product.Spn);
                Assert.IsNotEmpty(product.Spn);
                Assert.IsNotNull(product.Name);
                Assert.IsNotEmpty(product.Name);
                Assert.IsNotNull(product.OrderUnit);
                Assert.IsNotEmpty(product.OrderUnit);
                Assert.IsTrue(product.Price > 0);
                Assert.IsNotNull(product.Created);
                Assert.IsNotNull(product.Updated);
            }
        }

        [Test]
        public void TestSearchUnauthorized()
        {
            MockFromFile("products.search.unauthorized");

            var service = GetProductsService();
            Assert.NotNull(service);
            service.User = "";
            service.Password = "";

            Assert.ThrowsAsync<ServiceException>(
                () => service.Search().Pin("AD8CCDD5F9").Area("work").Q("toner").Skip(0).Take(30).Do());
        }
    }
}