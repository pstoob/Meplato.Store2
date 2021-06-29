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

using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using Polly;

namespace Meplato.Store2.Tests
{
    [TestFixture]
    public class PollyClientTests : TestCase
    {
        /// <summary>
        /// Run tests with Polly, illustrating retries and exponential backoff.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task TestExecuteFailureWithRetriesAndExponentialBackoff()
        {
            // This test uses Polly to wrap a HTTP request/response cycle with
            // retries and exponential backoff. Polly is much more versatile,
            // and supports integration into HttpClientFactory as well.
            //
            // The main Polly repository can be found at https://github.com/App-vNext/Polly.
            // See advanced samples at https://github.com/App-vNext/Polly-Samples.
            //
            // Documentation is good. Just Google for "Polly HttpClient"

            var client = new MockClient
            {
                Exception = null,
                Response = new Response(429, "{\"error\":{\"message\":\"Too many requests\"}}")
            };


            IResponse response = null;
            var retries = 0;
            var successes = 0;
            var failures = 0;
            var policy = Policy.Handle<Exception>().WaitAndRetryAsync(
                5,
                attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2, attempt)),
                (exception, waitTime) =>
                {
                    Assert.NotNull(exception);
                    Assert.IsInstanceOf<ServiceException>(exception);
                    Assert.AreEqual("Too many requests", exception.Message);
                    retries++;

                    Console.WriteLine($"Retry {retries} with a wait time of {waitTime}");
                }
            );
            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    response = await client.Execute(HttpMethod.Get, "http://localhost/", null, null, null);
                    successes++;
                });
            }
            catch (ServiceException ex)
            {
                Assert.NotNull(ex);
                Assert.IsInstanceOf<ServiceException>(ex);
                Assert.AreEqual(429, ex.StatusCode);
                Assert.AreEqual("Too many requests", ex.Message);
                failures++;
            }
            catch (Exception ex)
            {
                Assert.Fail("expected ServiceException, got {0}", ex);
            }

            Assert.Null(response);
            Assert.AreEqual(0, successes);
            Assert.AreEqual(1, failures);
            Assert.AreEqual(5, retries);
        }

        [Test]
        public async Task TestExecuteSuccess()
        {
            var client = new MockClient
            {
                Exception = null,
                Response = new Response(200, "{}")
            };


            IResponse response = null;
            var retries = 0;
            var successes = 0;
            var failures = 0;
            var policy = Policy.Handle<Exception>().WaitAndRetryAsync(
                6,
                attempt => TimeSpan.FromSeconds(0.1 * Math.Pow(2, attempt)),
                (exception, waitTime) => { retries++; }
            );
            try
            {
                await policy.ExecuteAsync(async () =>
                {
                    response = await client.Execute(HttpMethod.Get, "http://localhost/", null, null, null);
                    successes++;
                });
            }
            catch (Exception)
            {
                failures++;
            }

            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("{}", response.GetBodyString());
            Assert.AreEqual(1, successes);
            Assert.AreEqual(0, failures);
            Assert.AreEqual(0, retries);
        }
    }
}