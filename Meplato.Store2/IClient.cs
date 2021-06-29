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

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Meplato.Store2
{
    /// <summary>
    ///     An interface for HTTP clients.
    /// </summary>
    public interface IClient
    {
        /// <summary>
        ///     Execute runs a HTTP request/response with the API endpoint.
        /// </summary>
        /// <param name="method">HTTP method, e.g. POST or GET</param>
        /// <param name="uriTemplate">URI Template as of RFC 6570</param>
        /// <param name="parameters">Query String Parameters</param>
        /// <param name="headers">Headers key/value pairs</param>
        /// <param name="body">Body</param>
        /// <returns>A <see cref="Task{T}" /> with an <see cref="IResponse" /></returns>
        /// <exception cref="ServiceException">A <see cref="ServiceException" /> is thrown if something goes wrong.</exception>
        Task<IResponse> Execute(HttpMethod method, string uriTemplate, IDictionary<string, object> parameters,
            IDictionary<string, string> headers, object body);
    }
}