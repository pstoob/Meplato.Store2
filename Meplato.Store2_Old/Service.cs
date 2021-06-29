#region Copyright and terms of services
// Copyright (c) 2013-present Meplato GmbH.
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

// THIS FILE IS AUTO-GENERATED. DO NOT MODIFY!

// The file implements the Meplato Store API.
//
// Author:  Meplato API Team <support@meplato.com>
// Version: 2.1.9
// License: Copyright (c) 2015-2020 Meplato GmbH. All rights reserved.
// See <a href="https://developer.meplato.com/store2/#terms">Terms of Service</a>
// See <a href="https://developer.meplato.com/store2/">External documentation</a>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Meplato.Store2
{
	/// <summary>
	///     The Meplato Store API enables technical integration of
	///     customers and partners. 
	/// </summary>
	public class Service
	{
		#region Service
		public const string Title = "Meplato Store API";
		public const string Version = "2.1.9";
		public const string UserAgent = "meplato-csharp-client/2.0";
		public const string DefaultBaseURL = "https://store.meplato.com/api/v2";

		/// <summary>
		///     Initializes a new <see cref="Service"/>.
		/// </summary>
		/// <param name="client">Client to use for requests</param>
		public Service(IClient client)
		{
			Client = client;
			BaseURL = DefaultBaseURL;
		}

		/// <summary>
		///     Returns the <see cref="IClient"/> to perform requests.
		/// </summary>
		public IClient Client { get; private set; }

		/// <summary>
		///     Represents the BaseURL to use for requests (default is <see
		///     cref="DefaultBaseURL"/>).
		/// </summary>
		public string BaseURL { get; set; }

		/// <summary>
		///     Specifies the username to authenticate requests.
		/// </summary>
		public string User { get; set; }

		/// <summary>
		///     Specifies the password to authenticate requests.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		///     Returns the authentication header for HTTP Basic Author
		///     <code>null</code> for unauthenticated requests.
		/// </summary>
		public string GetAuthorizationHeader()
		{
			if (!string.IsNullOrEmpty(User) || !string.IsNullOrEmpty(Password))
			{
				string userPass = "";
				if (!string.IsNullOrEmpty(User))
				{
					userPass = User;
				}
				userPass = userPass + ":";
				if (!string.IsNullOrEmpty(Password))
				{
					userPass = userPass + Password;
				}
				var bytes = Encoding.UTF8.GetBytes(userPass);
				var credentials = Convert.ToBase64String(bytes);
				return "Basic " + credentials;
			}
			return null;
		}

		/// <summary>
		///     Me returns information about your user profile and the API
		///     endpoints of the Meplato Store 2.0 API.
		/// </summary>
		public MeService Me() {
			return new MeService(this);
		}

		/// <summary>
		///     Ping allows you to test if the Meplato Store 2.0 API is
		///     currently operational.
		/// </summary>
		public PingService Ping() {
			return new PingService(this);
		}
		#endregion // Service
	}

	/// <summary>
	///     MeResponse returns various information about the user and
	///     endpoints.
	/// </summary>
	public class MeResponse
	{
		#region MeResponse

		/// <summary>
		///     CatalogsLink is the URL for retrieving the list of catalogs.
		/// </summary>
		[JsonProperty("catalogsLink")]
		public string CatalogsLink { get; set; }

		/// <summary>
		///     Kind is store#me for this entity.
		/// </summary>
		[JsonProperty("kind")]
		public string Kind { get; set; }

		/// <summary>
		///     Merchant returns information about your merchant account.
		/// </summary>
		[JsonProperty("merchant")]
		public Merchant Merchant { get; set; }

		/// <summary>
		///     SelfLink is the URL of this request.
		/// </summary>
		[JsonProperty("selfLink")]
		public string SelfLink { get; set; }

		/// <summary>
		///     User returns information about your user account.
		/// </summary>
		[JsonProperty("user")]
		public User User { get; set; }

		#endregion // MeResponse
	}

	/// <summary>
	///     Merchant holds account data for the merchant/supplier in
	///     Meplato Store.
	/// </summary>
	public class Merchant
	{
		#region Merchant

		/// <summary>
		///     Country is the ISO code for the country of the merchant, e.g.
		///     DE or CH.
		/// </summary>
		[JsonProperty("country")]
		public string Country { get; set; }

		/// <summary>
		///     Created is the date/time when the merchant was created, e.g.
		///     2015-03-19T12:09:45Z
		/// </summary>
		[JsonProperty("created")]
		public DateTimeOffset? Created { get; set; }

		/// <summary>
		///     Currency is the default ISO code for new catalogs, e.g. EUR or
		///     GBP.
		/// </summary>
		[JsonProperty("currency")]
		public string Currency { get; set; }

		/// <summary>
		///     ID is a unique (internal) identifier of the merchant.
		/// </summary>
		[JsonProperty("id")]
		public long Id { get; set; }

		/// <summary>
		///     Kind is store#merchant for this entity.
		/// </summary>
		[JsonProperty("kind")]
		public string Kind { get; set; }

		/// <summary>
		///     Language is the code for the language of the merchant, e.g. de
		///     or en.
		/// </summary>
		[JsonProperty("language")]
		public string Language { get; set; }

		/// <summary>
		///     Locale is the regional code in the format de_AT.
		/// </summary>
		[JsonProperty("locale")]
		public string Locale { get; set; }

		/// <summary>
		///     MPCC is the Meplato Company Code, a unique identifier.
		/// </summary>
		[JsonProperty("mpcc")]
		public string Mpcc { get; set; }

		/// <summary>
		///     MPSC is the Meplato Supplier Code.
		/// </summary>
		[JsonProperty("mpsc")]
		public string Mpsc { get; set; }

		/// <summary>
		///     Name is the name of the merchant.
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     OU is the default ISO code of the order unit, e.g. PCE or EA.
		/// </summary>
		[JsonProperty("ou")]
		public string Ou { get; set; }

		/// <summary>
		///     SelfLink is the URL for this merchant.
		/// </summary>
		[JsonProperty("selfLink")]
		public string SelfLink { get; set; }

		/// <summary>
		///     SelfService indicates whether this merchant is on self-service
		///     or managed by Meplato.
		/// </summary>
		[JsonProperty("selfService")]
		public bool SelfService { get; set; }

		/// <summary>
		///     TimeZone is the time zone in the format Europe/Berlin.
		/// </summary>
		[JsonProperty("timeZone")]
		public string TimeZone { get; set; }

		/// <summary>
		///     Token is a shared token for this merchant.
		/// </summary>
		[JsonProperty("token")]
		public string Token { get; set; }

		/// <summary>
		///     Updated is the date/time when the merchant was last modified,
		///     e.g. 2015-03-19T12:09:45Z
		/// </summary>
		[JsonProperty("updated")]
		public DateTimeOffset? Updated { get; set; }

		#endregion // Merchant
	}

	/// <summary>
	///     User holds account data for the user in Meplato Store.
	/// </summary>
	public class User
	{
		#region User

		/// <summary>
		///     Country is the ISO code for the country, e.g. DE or CH.
		/// </summary>
		[JsonProperty("country")]
		public string Country { get; set; }

		/// <summary>
		///     Created is the date/time when the user was created, e.g.
		///     2015-03-19T12:09:45Z
		/// </summary>
		[JsonProperty("created")]
		public DateTimeOffset? Created { get; set; }

		/// <summary>
		///     Currency is the default ISO code for currencies, e.g. EUR or
		///     GBP.
		/// </summary>
		[JsonProperty("currency")]
		public string Currency { get; set; }

		/// <summary>
		///     Email is the email address.
		/// </summary>
		[JsonProperty("email")]
		public string Email { get; set; }

		/// <summary>
		///     ID is a unique (internal) identifier of the user.
		/// </summary>
		[JsonProperty("id")]
		public long Id { get; set; }

		/// <summary>
		///     Kind is store#user for this entity.
		/// </summary>
		[JsonProperty("kind")]
		public string Kind { get; set; }

		/// <summary>
		///     Language is the code for the language, e.g. de or en.
		/// </summary>
		[JsonProperty("language")]
		public string Language { get; set; }

		/// <summary>
		///     Locale is the regional code in the format de_AT.
		/// </summary>
		[JsonProperty("locale")]
		public string Locale { get; set; }

		/// <summary>
		///     Name is the user, including first and last name.
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     Provider is used internally.
		/// </summary>
		[JsonProperty("provider")]
		public string Provider { get; set; }

		/// <summary>
		///     TimeZone is the time zone in the format Europe/Berlin.
		/// </summary>
		[JsonProperty("timeZone")]
		public string TimeZone { get; set; }

		/// <summary>
		///     UID is used internally.
		/// </summary>
		[JsonProperty("uid")]
		public string Uid { get; set; }

		/// <summary>
		///     Updated is the date/time when the user was last modified, e.g.
		///     2015-03-19T12:09:45Z
		/// </summary>
		[JsonProperty("updated")]
		public DateTimeOffset? Updated { get; set; }

		#endregion // User
	}

	/// <summary>
	///     MeService: Me returns information about your user profile and
	///     the API endpoints of the Meplato Store 2.0 API.
	/// </summary>
	public class MeService
	{
		#region MeService

		private readonly Service _service;
		private readonly IDictionary<string, object> _opt = new Dictionary<string, object>();
		private readonly IDictionary<string, string> _hdr = new Dictionary<string, string>();

		/// <summary>
		///     Creates a new instance of MeService.
		/// </summary>
		public MeService(Service service)
		{
			_service = service;
		}

		/// <summary>
		///     Execute the operation.
		/// </summary>
		public async Task<MeResponse> Do()
			{
			// Make a copy of the parameters and add the path parameters to it
			var parameters = new Dictionary<string, object>();

			// Make a copy of the header parameters and set UA
			var headers = new Dictionary<string, string>();
			string authorization = _service.GetAuthorizationHeader();
			if (!string.IsNullOrEmpty(authorization))
			{
				headers["Authorization"] = authorization;
			}

			var uriTemplate = _service.BaseURL + "/";
			var response = await _service.Client.Execute(
				HttpMethod.Get,
				uriTemplate,
				parameters,
				headers,
				null);
			return response.GetBodyJSON<MeResponse>();
		}

		#endregion // MeService
	}

	/// <summary>
	///     PingService: Ping allows you to test if the Meplato Store 2.0
	///     API is currently operational.
	/// </summary>
	public class PingService
	{
		#region PingService

		private readonly Service _service;
		private readonly IDictionary<string, object> _opt = new Dictionary<string, object>();
		private readonly IDictionary<string, string> _hdr = new Dictionary<string, string>();

		/// <summary>
		///     Creates a new instance of PingService.
		/// </summary>
		public PingService(Service service)
		{
			_service = service;
		}

		/// <summary>
		///     Execute the operation.
		/// </summary>
		public async Task Do()
			{
			// Make a copy of the parameters and add the path parameters to it
			var parameters = new Dictionary<string, object>();

			// Make a copy of the header parameters and set UA
			var headers = new Dictionary<string, string>();
			string authorization = _service.GetAuthorizationHeader();
			if (!string.IsNullOrEmpty(authorization))
			{
				headers["Authorization"] = authorization;
			}

			var uriTemplate = _service.BaseURL + "/";
			await _service.Client.Execute(
				HttpMethod.Head,
				uriTemplate,
				parameters,
				headers,
				null);
		}

		#endregion // PingService
	}
}

