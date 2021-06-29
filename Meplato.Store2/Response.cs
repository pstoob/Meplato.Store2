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
using Newtonsoft.Json;

namespace Meplato.Store2
{
    /// <summary>
    ///     Implements a <see cref="IResponse" /> from <see cref="HttpResponseMessage" />.
    /// </summary>
    public class Response : IResponse
    {
        private readonly Task<string> _body;
        private string _rawBody;

        /// <summary>
        ///     Initializes a new <see cref="Response" />.
        /// </summary>
        /// <param name="message">
        ///     <see cref="HttpResponseMessage" />
        /// </param>
        public Response(HttpResponseMessage message)
        {
            StatusCode = (int) message.StatusCode;
            if (message.Content != null)
                _body = message.Content.ReadAsStringAsync();
        }

        /// <summary>
        ///     Initializes a new <see cref="Response" /> directly. Used for testing.
        /// </summary>
        /// <param name="statusCode">HTTP Status Code</param>
        /// <param name="rawBody">Raw body as a <see cref="string" /></param>
        public Response(int statusCode, string rawBody)
        {
            StatusCode = statusCode;
            _rawBody = rawBody;
        }

        /// <summary>
        ///     Returns the HTTP status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        ///     Returns the body of the response as a string.
        /// </summary>
        /// <returns>Body as <see cref="string" /></returns>
        public string GetBodyString()
        {
            return _rawBody ?? _body.Result;
        }

        /// <summary>
        ///     Deserializes the HTTP response body as type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">Type to deserialize</typeparam>
        /// <returns>Instance of type <typeparamref name="T" /></returns>
        public T GetBodyJSON<T>()
        {
            return JsonConvert.DeserializeObject<T>(GetBodyString());
        }

        /// <summary>
        ///     Sets the raw body of the response (only for testing).
        /// </summary>
        /// <param name="body">Raw body as <see cref="string" /></param>
        public void SetBodyString(string body)
        {
            _rawBody = body;
        }
    }
}