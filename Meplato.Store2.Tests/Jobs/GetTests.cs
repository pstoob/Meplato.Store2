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
    public class GetTests : TestCase
    {
        [Test]
        public async Task TestGet()
        {
            MockFromFile("jobs.get.success");

            var service = GetJobsService();
            Assert.NotNull(service);

            var job = await service.Get().Id("58097dc3-b279-49b5-a5da-23eb1c77d840").Do();
            Assert.NotNull(job);
            Assert.AreEqual("58097dc3-b279-49b5-a5da-23eb1c77d840", job.Id);
        }

        [Test]
        public void TestGetNotFound()
        {
            MockFromFile("jobs.get.not_found");

            var service = GetJobsService();
            Assert.NotNull(service);

            Assert.ThrowsAsync<ServiceException>(() => service.Get().Id("no-such-job").Do());
        }
    }
}