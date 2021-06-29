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
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Resta.UriTemplates;

namespace Meplato.Store2
{
    /// <summary>
    ///     Client implements <see cref="IClient" /> with System.Net.Http and JSON.Net.
    /// </summary>
    public class Client : IClient
    {
        /// <summary>
        ///     User Agent for .NET Clients.
        /// </summary>
        public const string UserAgent = "meplato-api-csharp-client/2.2.0";

        private readonly HttpClient _httpClient;

        /// <inheritdoc />
        /// <summary>
        ///     Create a new client.
        /// </summary>
        public Client() : this(null)
        {
        }

        /// <summary>
        ///     Create a new Client using a <see cref="HttpClient" /> passed
        ///     in by the consumer.
        /// </summary>
        /// <param name="httpClient">HTTP Client to use for requests or <c>null</c></param>
        public Client(HttpClient httpClient)
        {
            _httpClient = httpClient ?? HttpClientFactory.Create();

            // Always use application/json
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Execute runs a HTTP request/response with the API endpoint.
        /// </summary>
        /// <param name="method">HTTP method, e.g. POST or GET</param>
        /// <param name="uriTemplate">URI Template as of RFC 6570</param>
        /// <param name="parameters">Query String Parameters</param>
        /// <param name="headers">Headers key/value pairs</param>
        /// <param name="body">Body</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> with an <see cref="T:Meplato.Store2.IResponse" /></returns>
        /// <exception cref="T:Meplato.Store2.ServiceException">A <see cref="T:Meplato.Store2.ServiceException" /> is thrown if something goes wrong.</exception>
        public async Task<IResponse> Execute(HttpMethod method, string uriTemplate,
            IDictionary<string, object> parameters,
            IDictionary<string, string> headers, object body)
        {
            var template = new UriTemplate(uriTemplate);
            var url = template.Resolve(parameters);

            // Always add a User-Agent
            var request = new HttpRequestMessage(method, url);
            request.Headers.Add("User-Agent", UserAgent);
            foreach (var kv in headers) request.Headers.Add(kv.Key, kv.Value);
            if (body != null)
            {
                var content = body is string ? (string) body : JsonConvert.SerializeObject(body);
                request.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }

            // Perform HTTP request and wrap response in IResponse
            try
            {
                var httpResponse = await _httpClient.SendAsync(request);
                var response = new Response(httpResponse);

                if (httpResponse.IsSuccessStatusCode)
                    return response;

                throw ServiceException.FromResponse(response);
            }
            catch (ServiceException _ex)
            {
                throw _ex;
            }
            catch (Exception e)
            {
                throw new ServiceException("Request failed", null, e);
            }
        }
    }
}