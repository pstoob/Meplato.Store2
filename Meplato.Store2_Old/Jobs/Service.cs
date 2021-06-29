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
using Meplato.Store2;

namespace Meplato.Store2.Jobs
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
		///     Get a single job.
		/// </summary>
		public GetService Get() {
			return new GetService(this);
		}

		/// <summary>
		///     Search for jobs.
		/// </summary>
		public SearchService Search() {
			return new SearchService(this);
		}
		#endregion // Service
	}

	/// <summary>
	///     Job that processes a task in the background, e.g. publishing a
	///     catalog.
	/// </summary>
	public class Job
	{
		#region Job

		/// <summary>
		///     ID of the catalog.
		/// </summary>
		[JsonProperty("catalogId")]
		public long CatalogId { get; set; }

		/// <summary>
		///     Name of the catalog.
		/// </summary>
		[JsonProperty("catalogName")]
		public string CatalogName { get; set; }

		/// <summary>
		///     Completed is the date and time when the job has been completed,
		///     either successfully or failed.
		/// </summary>
		[JsonProperty("completed")]
		public DateTimeOffset? Completed { get; set; }

		/// <summary>
		///     Created is the creation date and time of the job.
		/// </summary>
		[JsonProperty("created")]
		public DateTimeOffset? Created { get; set; }

		/// <summary>
		///     Email of the user that initiated the job.
		/// </summary>
		[JsonProperty("email")]
		public string Email { get; set; }

		/// <summary>
		///     ID is a unique (internal) identifier of the job.
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		///     Kind is store#job for a job entity.
		/// </summary>
		[JsonProperty("kind")]
		public string Kind { get; set; }

		/// <summary>
		///     ID of the merchant.
		/// </summary>
		[JsonProperty("merchantId")]
		public long MerchantId { get; set; }

		/// <summary>
		///     MPCC of the merchant.
		/// </summary>
		[JsonProperty("merchantMpcc")]
		public string MerchantMpcc { get; set; }

		/// <summary>
		///     Name of the merchant.
		/// </summary>
		[JsonProperty("merchantName")]
		public string MerchantName { get; set; }

		/// <summary>
		///     URL to this page.
		/// </summary>
		[JsonProperty("selfLink")]
		public string SelfLink { get; set; }

		/// <summary>
		///     Started is the date and time when the job has been started.
		/// </summary>
		[JsonProperty("started")]
		public DateTimeOffset? Started { get; set; }

		/// <summary>
		///     State describes the current state of the job, i.e. one of
		///     waiting,working,succeeded, or failed.
		/// </summary>
		[JsonProperty("state")]
		public string State { get; set; }

		/// <summary>
		///     Topic of the job, e.g. if it was an import or a validation
		///     task.
		/// </summary>
		[JsonProperty("topic")]
		public string Topic { get; set; }

		#endregion // Job
	}

	/// <summary>
	///     SearchResponse is a partial listing of jobs.
	/// </summary>
	public class SearchResponse
	{
		#region SearchResponse

		/// <summary>
		///     Items is the slice of jobs of this result.
		/// </summary>
		[JsonProperty("items")]
		public Job[] Items { get; set; }

		/// <summary>
		///     Kind is store#jobs for this kind of response.
		/// </summary>
		[JsonProperty("kind")]
		public string Kind { get; set; }

		/// <summary>
		///     NextLink returns the URL to the next slice of jobs (if any).
		/// </summary>
		[JsonProperty("nextLink")]
		public string NextLink { get; set; }

		/// <summary>
		///     PreviousLink returns the URL of the previous slice of jobs (if
		///     any).
		/// </summary>
		[JsonProperty("previousLink")]
		public string PreviousLink { get; set; }

		/// <summary>
		///     SelfLink returns the URL to this page.
		/// </summary>
		[JsonProperty("selfLink")]
		public string SelfLink { get; set; }

		/// <summary>
		///     TotalItems describes the total number of jobs found.
		/// </summary>
		[JsonProperty("totalItems")]
		public long TotalItems { get; set; }

		#endregion // SearchResponse
	}

	/// <summary>
	///     GetService: Get a single job.
	/// </summary>
	public class GetService
	{
		#region GetService

		private readonly Service _service;
		private readonly IDictionary<string, object> _opt = new Dictionary<string, object>();
		private readonly IDictionary<string, string> _hdr = new Dictionary<string, string>();

		private string _id;

		/// <summary>
		///     Creates a new instance of GetService.
		/// </summary>
		public GetService(Service service)
		{
			_service = service;
		}

		/// <summary>
		///     ID of the job.
		/// </summary>
		public GetService Id(string id)
		{
			_id = id;
			return this;
		}

		/// <summary>
		///     Execute the operation.
		/// </summary>
		public async Task<Job> Do()
			{
			// Make a copy of the parameters and add the path parameters to it
			var parameters = new Dictionary<string, object>();
			// UriTemplates package wants path parameters as strings
			parameters["id"] = string.Format("{0}", _id);

			// Make a copy of the header parameters and set UA
			var headers = new Dictionary<string, string>();
			string authorization = _service.GetAuthorizationHeader();
			if (!string.IsNullOrEmpty(authorization))
			{
				headers["Authorization"] = authorization;
			}

			var uriTemplate = _service.BaseURL + "/jobs/{id}";
			var response = await _service.Client.Execute(
				HttpMethod.Get,
				uriTemplate,
				parameters,
				headers,
				null);
			return response.GetBodyJSON<Job>();
		}

		#endregion // GetService
	}

	/// <summary>
	///     SearchService: Search for jobs.
	/// </summary>
	public class SearchService
	{
		#region SearchService

		private readonly Service _service;
		private readonly IDictionary<string, object> _opt = new Dictionary<string, object>();
		private readonly IDictionary<string, string> _hdr = new Dictionary<string, string>();

		/// <summary>
		///     Creates a new instance of SearchService.
		/// </summary>
		public SearchService(Service service)
		{
			_service = service;
		}

		/// <summary>
		///     Skip specifies how many catalogs to skip (default 0).
		/// </summary>
		public SearchService Skip(long skip)
		{
			_opt["skip"] = skip;
			return this;
		}

		/// <summary>
		///     State filter, e.g. waiting,working,succeeded,failed.
		/// </summary>
		public SearchService State(string state)
		{
			_opt["state"] = state;
			return this;
		}

		/// <summary>
		///     Take defines how many catalogs to return (max 100, default 20).
		/// </summary>
		public SearchService Take(long take)
		{
			_opt["take"] = take;
			return this;
		}

		/// <summary>
		///     Execute the operation.
		/// </summary>
		public async Task<SearchResponse> Do()
			{
			// Make a copy of the parameters and add the path parameters to it
			var parameters = new Dictionary<string, object>();
			if (_opt.ContainsKey("skip"))
			{
				// UriTemplates package wants query parameters as strings
				parameters["skip"] = string.Format("{0}", _opt["skip"]);
			}
			if (_opt.ContainsKey("state"))
			{
				// UriTemplates package wants query parameters as strings
				parameters["state"] = string.Format("{0}", _opt["state"]);
			}
			if (_opt.ContainsKey("take"))
			{
				// UriTemplates package wants query parameters as strings
				parameters["take"] = string.Format("{0}", _opt["take"]);
			}

			// Make a copy of the header parameters and set UA
			var headers = new Dictionary<string, string>();
			string authorization = _service.GetAuthorizationHeader();
			if (!string.IsNullOrEmpty(authorization))
			{
				headers["Authorization"] = authorization;
			}

			var uriTemplate = _service.BaseURL + "/jobs{?merchantId,skip,take,state}";
			var response = await _service.Client.Execute(
				HttpMethod.Get,
				uriTemplate,
				parameters,
				headers,
				null);
			return response.GetBodyJSON<SearchResponse>();
		}

		#endregion // SearchService
	}
}

