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
    public class PublishTests : TestCase
    {
        [Test]
        public async Task TestPublish()
        {
            MockFromFile("catalogs.publish.success");

            var service = GetCatalogsService();
            Assert.NotNull(service);

            var response = await service.Publish().Pin("AD8CCDD5F9").Do();
            Assert.NotNull(response);
            Assert.AreEqual("store#catalogPublish", response.Kind);
            Assert.AreNotEqual("", response.SelfLink);
            Assert.AreNotEqual("", response.StatusLink);

            // Here's how to watch for publish status by polling
            /*
            while (true)
            {
                var statusResponse = await service.PublishStatus().Pin("AD8CCDD5F9").Do();
                Assert.NotNull(statusResponse);
                Assert.AreEqual("store#catalogPublishStatus", statusResponse.Kind);
                Assert.AreNotEqual("", statusResponse.SelfLink);
                Assert.AreNotEqual("", statusResponse.Status);
                if (string.Compare("done", statusResponse.Status, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    break;
                }
                Thread.Sleep(1000);
            }
            */
        }
    }
}