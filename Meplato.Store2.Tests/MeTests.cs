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

namespace Meplato.Store2.Tests
{
    [TestFixture]
    public class MeTests : TestCase
    {
        [Test]
        public async Task TestMe()
        {
            MockFromFile("me.success");

            var service = GetRootService();
            Assert.IsNotNull(service);

            var results = await service.Me().Do();
            Assert.IsNotNull(results);
            Assert.AreEqual("store#me", results.Kind);
        }

        [Test]
        public void TestMeUnauthorized()
        {
            MockFromFile("me.unauthorized");

            var service = GetRootService();
            service.User = "";
            service.Password = "";
            Assert.ThrowsAsync<ServiceException>(() => service.Me().Do());
        }
    }
}