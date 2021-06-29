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
    public class ScrollTests : TestCase
    {
        [Test]
        public async Task ScrollUpdate()
        {
            var service = GetProductsService();
            Assert.NotNull(service);

            MockFromFile("products.scroll.success.1");
            var response = await service.Scroll().Pin("AD8CCDD5F9").Area("work").Do();
            Assert.NotNull(response);
            Assert.AreEqual("store#products", response.Kind);
            Assert.IsNotNull(response.PageToken);
            Assert.IsNotEmpty(response.PageToken);
            var pageToken = response.PageToken;
            Assert.IsTrue(response.TotalItems > 0);
            if (response.Items != null)
                foreach (var product in response.Items)
                {
                    Assert.NotNull(product);
                    Assert.IsNotNull(product.Id);
                    Assert.IsNotEmpty(product.Id);
                    Assert.IsNotNull(product.Kind);
                    Assert.IsNotEmpty(product.Kind);
                    Assert.IsNotNull(product.SelfLink);
                    Assert.IsNotEmpty(product.SelfLink);
                    Assert.IsNotNull(product.Spn);
                    Assert.IsNotEmpty(product.Spn);
                    Assert.IsNotNull(product.Name);
                    Assert.IsNotEmpty(product.Name);
                    Assert.IsNotNull(product.OrderUnit);
                    Assert.IsNotEmpty(product.OrderUnit);
                    Assert.IsTrue(product.Price > 0);
                    Assert.NotNull(product.Created);
                    Assert.NotNull(product.Updated);
                }

            MockFromFile("products.scroll.success.2");
            response = await service.Scroll().Pin("AD8CCDD5F9").Area("work").PageToken(pageToken).Do();
            Assert.NotNull(response);
            Assert.AreEqual("store#products", response.Kind);
            Assert.IsNotNull(response.PageToken);
            Assert.IsNotEmpty(response.PageToken);
            Assert.IsTrue(response.TotalItems > 0);
            Assert.NotNull(response.Items);
            if (response.Items != null)
                foreach (var product in response.Items)
                {
                    Assert.NotNull(product);
                    Assert.IsNotNull(product.Id);
                    Assert.IsNotEmpty(product.Id);
                    Assert.IsNotNull(product.Kind);
                    Assert.IsNotEmpty(product.Kind);
                    Assert.IsNotNull(product.SelfLink);
                    Assert.IsNotEmpty(product.SelfLink);
                    Assert.IsNotNull(product.Spn);
                    Assert.IsNotEmpty(product.Spn);
                    Assert.IsNotNull(product.Name);
                    Assert.IsNotEmpty(product.Name);
                    Assert.IsNotNull(product.OrderUnit);
                    Assert.IsNotEmpty(product.OrderUnit);
                    Assert.IsTrue(product.Price > 0);
                    Assert.NotNull(product.Created);
                    Assert.NotNull(product.Updated);
                }
        }
        
        [Test]
        public async Task ScrollDifferentialUpdate()
        {
            var service = GetProductsService();
            Assert.NotNull(service);

            MockFromFile("products.scroll.differential.success");
            var response = await service.Scroll().Pin("AD8CCDD5F9").Area("work").Version(3).Mode("diff").Do();
            Assert.NotNull(response);
            Assert.AreEqual("store#products", response.Kind);
            Assert.IsNotNull(response.PageToken);
            Assert.IsNotEmpty(response.PageToken);
            Assert.IsTrue(response.TotalItems > 0);
            if (response.Items != null)
                foreach (var product in response.Items)
                {
                    Assert.NotNull(product);
                    Assert.IsNotNull(product.Id);
                    Assert.IsNotEmpty(product.Id);
                    Assert.IsNotNull(product.Kind);
                    Assert.IsNotEmpty(product.Kind);
                    Assert.IsNotNull(product.SelfLink);
                    Assert.IsNotEmpty(product.SelfLink);
                    Assert.IsNotNull(product.Spn);
                    Assert.IsNotEmpty(product.Spn);
                    Assert.IsNotNull(product.Mode);
                    Assert.IsNotEmpty(product.Mode);
                    Assert.NotNull(product.Created);
                    Assert.NotNull(product.Updated);
                }
        }
    }
}