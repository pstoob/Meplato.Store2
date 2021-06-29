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
using System.IO;
using System.Net.Http;
using NUnit.Framework;

namespace Meplato.Store2.Tests
{
    public abstract class TestCase
    {
        public const string TestBaseURL = "https://store.meplato.com/api/v2"; //"http://store2.go/api/v2";
        public const string TestUser = "d1b1818558c71134"; // "<your-api-key-here>";
        public const string TestPassword = "";
        public const string pin = "6AF4CCF8D0";

        protected TestCase()
        {
            Client Client = new Client();
            
            System.Environment.SetEnvironmentVariable("STORE2_USER", "d1b1818558c71134"); //API token
            System.Environment.SetEnvironmentVariable("STORE_URL", "https://store.meplato.com/api/v2"); //Base Store URL, not sure this is necessary
            BaseURL = TestBaseURL;
            User = TestUser;
            Password = TestPassword;
        }

        public IClient Client { get; protected set; }
        public string BaseURL { get; protected set; }
        public string User { get; protected set; }
        public string Password { get; protected set; }

        public Store2.Catalogs.Service GetCatalogsService()
        {
            var service = new Store2.Catalogs.Service(Client)
            {
                //BaseURL = BaseURL ?? Store2.Catalogs.Service.DefaultBaseURL,
                //User = User ?? Environment.GetEnvironmentVariable("STORE2_USER"),
                //Password = Password ?? Environment.GetEnvironmentVariable("STORE2_PASSWORD")
                BaseURL = "https://store.meplato.com/api/v2",
                User = "d1b1818558c71134",
                Password = ""
            };
            return service;
        }

        public Store2.Products.Service GetProductsService()
        {
            var service = new Store2.Products.Service(Client)
            {
                //BaseURL = BaseURL ?? Store2.Products.Service.DefaultBaseURL,
                //User = User ?? Environment.GetEnvironmentVariable("STORE2_USER"),
                //Password = Password ?? Environment.GetEnvironmentVariable("STORE2_PASSWORD")
                BaseURL = "https://store.meplato.com/api/v2",
                User = "d1b1818558c71134",
                Password = "7100Gano"
            };
            return service;
        }

        public Store2.Jobs.Service GetJobsService()
        {
            var service = new Store2.Jobs.Service(Client)
            {
                BaseURL = TestBaseURL ?? Store2.Jobs.Service.DefaultBaseURL,
                User = TestUser ?? Environment.GetEnvironmentVariable("STORE2_USER"),
                Password = TestPassword ?? Environment.GetEnvironmentVariable("STORE2_PASSWORD")
            };
            return service;
        }

        public Service GetRootService()
        {
            var service = new Service(Client)
            {
                BaseURL = TestBaseURL ?? Service.DefaultBaseURL,
                User = TestUser ?? Environment.GetEnvironmentVariable("STORE2_USER"),
                Password = TestPassword ?? Environment.GetEnvironmentVariable("STORE2_PASSWORD")
            };
            return service;
        }

        protected void Mock(IResponse response)
        {
            var mockClient = Client as MockClient;
            if (mockClient == null)
                throw new InvalidOperationException("Current IClient is not a MockClient");
            mockClient.Response = response;
        }

        protected void MockFromFile(string path)
        {
            var mockClient = new MockClient();
            if (mockClient == null)
                throw new InvalidOperationException("Current IClient is not a MockClient");



            var msg = FromFile(path);
            mockClient.Response = new Response(msg);
        }

        public static HttpResponseMessage FromFile(string path)
        {
            return FromStream(File.OpenRead( "C:\temp\test.txt"));
        }

        public static HttpResponseMessage FromStream(Stream stream)
        {
            var response = new HttpResponseMessage();
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            response.Content = new ByteArrayContent(memoryStream.ToArray());
            response.Content.Headers.Add("Content-Type", "application/http;msgtype=response");
            return response.Content.ReadAsHttpResponseMessageAsync().Result;
        }
    }
}