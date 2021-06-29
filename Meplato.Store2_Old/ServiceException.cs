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

namespace Meplato.Store2
{
    /// <summary>
    ///     ServiceException is used to indicate errors communicating with an API endpoint.
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        ///     Initializes a new <see cref="ServiceException" />.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="error">Error details</param>
        /// <param name="innerException">Inner Exception</param>
        /// <seealso cref="Error" />
        public ServiceException(string message, Error error, Exception innerException) : this(message, error, 0,
            innerException)
        {
        }

        /// <summary>
        ///     Initializes a new <see cref="ServiceException" />.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="error">Error details</param>
        /// <param name="statusCode">HTTP status code or <c>0</c></param>
        /// <param name="innerException">Inner Exception</param>
        /// <seealso cref="Error" />
        public ServiceException(string message, Error error, int statusCode, Exception innerException) : base(message,
            innerException)
        {
            Error = error;
            StatusCode = statusCode;
        }

        /// <summary>
        ///     Returns details about the error.
        /// </summary>
        /// <seealso cref="Error" />
        public Error Error { get; }

        /// <summary>
        ///     Returns the HTTP status code, if one was passed by the HTTP response.
        /// </summary>
        /// <remarks>
        ///     If the exception didn't originate from a HTTP response and one could
        ///     not determine the HTTP status code, a value of <c>0</c> is returned.
        /// </remarks>
        public int StatusCode { get; }

        /// <summary>
        ///     Constructs a <see cref="ServiceException" /> from a <see cref="IResponse" />.
        /// </summary>
        /// <param name="response">
        ///     <see cref="IResponse" />
        /// </param>
        /// <returns>A <see cref="ServiceException" /></returns>
        public static ServiceException FromResponse(IResponse response)
        {
            if (response == null)
                return new ServiceException("Request failed", null, null);

            try
            {
                var error = response.GetBodyJSON<Error>();
                if (error?.Details == null)
                    return new ServiceException("Request failed", null, response.StatusCode, null);
                return new ServiceException(error.Details.Message, error, response.StatusCode, null);
            }
            catch (Exception e)
            {
                return new ServiceException("Request failed", null, response.StatusCode, e);
            }
        }
    }
}