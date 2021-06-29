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

namespace Meplato.Store2.Tests.Catalogs
{
    [TestFixture]
    public class GetTests : TestCase
    {
        [Test]
        public async Task TestGet()
        {
            MockFromFile("catalogs.get.success");

            var service = GetCatalogsService();
            Assert.NotNull(service);

            var catalog = await service.Get().Pin("5094310527").Do();
            Assert.NotNull(catalog);
            Assert.Greater(catalog.Id, 0);
            Assert.AreEqual("5094310527", catalog.Pin);
        }

        [Test]
        public void TestGetNotFound()
        {
            MockFromFile("catalogs.get.not_found");

            var service = GetCatalogsService();
            Assert.NotNull(service);

            Assert.ThrowsAsync<ServiceException>(() => service.Get().Pin("no-such-catalog").Do());
        }
    }
}