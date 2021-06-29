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

using NUnit.Framework;

namespace Meplato.Store2.Tests
{
    [TestFixture]
    public class ServiceExceptionTests : TestCase
    {
        [Test]
        public void TestRaisesServiceException()
        {
            var response = new Response(401, "{\"error\":{\"message\":\"Unauthorized\"}}");
            var ex = ServiceException.FromResponse(response);
            Assert.NotNull(ex);
            Assert.AreEqual("Unauthorized", ex.Message);
        }

        [Test]
        public void TestRaisesServiceExceptionFromFile()
        {
            var httpResponse = FromFile("me.unauthorized");
            var response = new Response(httpResponse);
            var ex = ServiceException.FromResponse(response);
            Assert.NotNull(ex);
            Assert.AreEqual("Unauthorized", ex.Message);
        }

        [Test]
        public void TestRaisesServiceExceptionWithoutBody()
        {
            var httpResponse = FromFile("ping.unauthorized");
            var response = new Response(httpResponse);
            var ex = ServiceException.FromResponse(response);
            Assert.NotNull(ex);
            Assert.AreEqual("Request failed", ex.Message);
        }
    }
}