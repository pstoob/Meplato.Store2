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
using Newtonsoft.Json;

namespace Meplato.Store2
{
    /// <summary>
    ///     Error is a valid response from a service call
    ///     that returns error information from the server.
    /// </summary>
    public class Error
    {
        /// <summary>
        ///     Initializes a new <see cref="Error" />.
        /// </summary>
        /// <param name="error">Inner error details</param>
        public Error(ErrorDetails details)
        {
            Details = details;
        }

        /// <summary>
        ///     Error details.
        /// </summary>
        [JsonProperty("error")]
        public ErrorDetails Details { get; private set; }

        /// <summary>
        ///     Error details.
        /// </summary>
        public class ErrorDetails
        {
            /// <summary>
            ///     Initializes new error details.
            /// </summary>
            /// <param name="code">Error code (numeric)</param>
            /// <param name="message">Error message</param>
            /// <param name="details">Error details</param>
            public ErrorDetails(int code, string message, IEnumerable<string> details)
            {
                Code = code;
                Message = message;
                Details = details;
            }

            /// <summary>
            ///     Returns the error code.
            /// </summary>
            [JsonProperty("code")]
            public int Code { get; private set; }

            /// <summary>
            ///     Returns the error message.
            /// </summary>
            [JsonProperty("message")]
            public string Message { get; private set; }

            /// <summary>
            ///     Returns additional detail about the error.
            /// </summary>
            [JsonProperty("details")]
            public IEnumerable<string> Details { get; private set; }
        }
    }
}