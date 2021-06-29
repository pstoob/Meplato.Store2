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

namespace Meplato.Store2.Tests.Jobs
{
    [TestFixture]
    public class SearchTests : TestCase
    {
        [Test]
        public async Task TestSearch()
        {
            MockFromFile("jobs.search.success");

            var service = GetJobsService();
            Assert.NotNull(service);

            var response = await service.Search().Skip(0).Take(10).Do();
            Assert.NotNull(response);
            Assert.AreEqual("store#jobs", response.Kind);
            Assert.IsTrue(response.TotalItems > 0);
            Assert.NotNull(response.Items);
            foreach (var job in response.Items)
            {
                Assert.NotNull(job);
                Assert.AreNotEqual("", job.SelfLink);
                Assert.NotNull(job.Id);
                Assert.AreNotEqual("", job.Id);
                Assert.IsNotNull(job.MerchantName);
                Assert.IsNotEmpty(job.MerchantName);
                Assert.NotNull(job.Created);
            }
        }

        [Test]
        public void TestSearchUnauthorized()
        {
            MockFromFile("jobs.search.unauthorized");

            var service = GetJobsService();
            Assert.NotNull(service);
            service.User = "";
            service.Password = "";
            Assert.ThrowsAsync<ServiceException>(() => service.Search().Skip(0).Take(10).Do());
        }
    }
}