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

namespace Meplato.Store2
{
    /// <summary>
    ///     IResponse defines the contract for HTTP responses from the API.
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        ///     Returns the HTTP status code of the response.
        /// </summary>
        int StatusCode { get; }

        /// <summary>
        ///     Returns the body of the response as a string.
        /// </summary>
        /// <returns></returns>
        string GetBodyString();

        /// <summary>
        ///     Deserializes the body of the response as a JSON entity of type T.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize.</typeparam>
        /// <returns>An object of type T or <code>default(T)</code></returns>
        T GetBodyJSON<T>();
    }
}