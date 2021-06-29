#region Copyright and terms of services

// Copyright (c) 2013-present Meplato GmbH.
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
using Meplato.Store2.Catalogs;
using NUnit.Framework;

namespace Meplato.Store2.Tests.Catalogs
{
    [TestFixture]
    public class CreateTests : TestCase
    {
        [Test]
        public async Task TestCreate()
        {
            MockFromFile("catalogs.create.success");

            var service = GetCatalogsService();
            Assert.NotNull(service);
            
            var create = new CreateCatalog()
            {
                MerchantId = 1,
                Name = "test2",
                // Description = "",
                // ProjectID = 0,
                ProjectMpcc = "meplato",
                ValidFrom = null,
                ValidUntil = null,
                Country = "DE",
                Currency = "EUR",
                Language = "de",
                Target = "mall",
                Type = "CC",
                SageNumber = "",
                SageContract = ""
            };

            var response = await service.Create().Catalog(create).Do();
            Assert.NotNull(response);
            Assert.AreEqual("store#catalog", response.Kind);
            Assert.AreEqual(81, response.Id);
            Assert.AreEqual("48F31F33AD", response.Pin);
            Assert.AreEqual("CC", response.Type);
        }
    }
}