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

namespace Meplato.Store2.Catalogs
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
		///     Create a new catalog (admin only).
		/// </summary>
		public CreateService Create() {
			return new CreateService(this);
		}

		/// <summary>
		///     Get a single catalog.
		/// </summary>
		public GetService Get() {
			return new GetService(this);
		}

		/// <summary>
		///     Publishes a catalog.
		/// </summary>
		public PublishService Publish() {
			return new PublishService(this);
		}

		/// <summary>
		///     Status of a publish process.
		/// </summary>
		public PublishStatusService PublishStatus() {
			return new PublishStatusService(this);
		}

		/// <summary>
		///     Purge the work or live area of a catalog, i.e. remove all
		///     products in the given area, but do not delete the catalog
		///     itself.
		/// </summary>
		public PurgeService Purge() {
			return new PurgeService(this);
		}

		/// <summary>
		///     Search for catalogs.
		/// </summary>
		public SearchService Search() {
			return new SearchService(this);
		}
		#endregion // Service
	}

	/// <summary>
	///     Catalog is a container for products, to be used in a certain
	///     project.
	/// </summary>
	public class Catalog
	{
		#region Catalog

		/// <summary>
		///     Country is the ISO-3166 alpha-2 code for the country that the
		///     catalog is destined for (e.g. DE or US).
		/// </summary>
		[JsonProperty("country")]
		public string Country { get; set; }

		/// <summary>
		///     Created is the creation date and time of the catalog.
		/// </summary>
		[JsonProperty("created")]
		public DateTimeOffset? Created { get; set; }

		/// <summary>
		///     Currency is the ISO-4217 currency code that is used for all
		///     products in the catalog (e.g. EUR or USD).
		/// </summary>
		[JsonProperty("currency")]
		public string Currency { get; set; }

		/// <summary>
		///     CustFields is an array of generic name/value pairs for
		///     customer-specific attributes.
		/// </summary>
		[JsonProperty("custFields")]
		public CustField[] CustFields { get; set; }

		/// <summary>
		///     Description of the catalog.
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		///     DownloadChecksum represents the checksum of the catalog last
		///     downloaded.
		/// </summary>
		[JsonProperty("downloadChecksum")]
		public string DownloadChecksum { get; set; }

		/// <summary>
		///     DownloadInterval represents the interval to use for checking
		///     new versions of a catalog at the DownloadURL.
		/// </summary>
		[JsonProperty("downloadInterval")]
		public string DownloadInterval { get; set; }

		/// <summary>
		///     DownloadURL represents a URL which is periodically downloaded
		///     and imported as a new catalog.
		/// </summary>
		[JsonProperty("downloadUrl")]
		public string DownloadUrl { get; set; }

		/// <summary>
		///     ERPNumberBuyer is the number of the merchant of this catalog in
		///     the SAP/ERP system of the buyer.
		/// </summary>
		[JsonProperty("erpNumberBuyer")]
		public string ErpNumberBuyer { get; set; }

		/// <summary>
		///     Expired indicates whether the catalog is expired as of now.
		/// </summary>
		[JsonProperty("expired")]
		public bool Expired { get; set; }

		/// <summary>
		///     HubURL represents the Meplato Hub URL for this catalog, e.g.
		///     https://hub.meplato.de/forward/12345/shop
		/// </summary>
		[JsonProperty("hubUrl")]
		public string HubUrl { get; set; }

		/// <summary>
		///     ID is a unique (internal) identifier of the catalog.
		/// </summary>
		[JsonProperty("id")]
		public long Id { get; set; }

		/// <summary>
		///     KeepOriginalBlobs indicates whether the URLs in a blob will be
		///     passed through and not cached by Store.
		/// </summary>
		[JsonProperty("keepOriginalBlobs")]
		public bool KeepOriginalBlobs { get; set; }

		/// <summary>
		///     Kind is store#catalog for a catalog entity.
		/// </summary>
		[JsonProperty("kind")]
		public string Kind { get; set; }

		/// <summary>
		///     KPISummary returns the outcome of analyzing the contents for
		///     key performance indicators.
		/// </summary>
		[JsonProperty("kpiSummary")]
		public KPISummary KpiSummary { get; set; }

		/// <summary>
		///     Language is the IETF language tag of the language of all
		///     products in the catalog (e.g. de or pt-BR).
		/// </summary>
		[JsonProperty("language")]
		public string Language { get; set; }

		/// <summary>
		///     LastImported is the date and time the catalog was last
		///     imported.
		/// </summary>
		[JsonProperty("lastImported")]
		public DateTimeOffset? LastImported { get; set; }

		/// <summary>
		///     LastPublished is the date and time the catalog was last
		///     published.
		/// </summary>
		[JsonProperty("lastPublished")]
		public DateTimeOffset? LastPublished { get; set; }

		/// <summary>
		///     LockedForDownload indicates whether a catalog is locked and
		///     cannot be downloaded.
		/// </summary>
		[JsonProperty("lockedForDownload")]
		public bool LockedForDownload { get; set; }

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
		///     MPSC of the merchant.
		/// </summary>
		[JsonProperty("merchantMpsc")]
		public string MerchantMpsc { get; set; }

		/// <summary>
		///     Name of the merchant.
		/// </summary>
		[JsonProperty("merchantName")]
		public string MerchantName { get; set; }

		/// <summary>
		///     Name of the catalog.
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     Number of products currently in the live area (only returned
		///     when getting the details of a catalog).
		/// </summary>
		[JsonProperty("numProductsLive")]
		public long? NumProductsLive { get; set; }

		/// <summary>
		///     Number of products currently in the work area (only returned
		///     when getting the details of a catalog).
		/// </summary>
		[JsonProperty("numProductsWork")]
		public long? NumProductsWork { get; set; }

		/// <summary>
		///     OciURL represents the OCI punchout URL that the supplier
		///     specified for this catalog, e.g.
		///     https://my-shop.com/oci?param1=a
		/// </summary>
		[JsonProperty("ociUrl")]
		public string OciUrl { get; set; }

		/// <summary>
		///     PIN of the catalog.
		/// </summary>
		[JsonProperty("pin")]
		public string Pin { get; set; }

		/// <summary>
		///     Project references the project that this catalog belongs to.
		/// </summary>
		[JsonProperty("project")]
		public Project Project { get; set; }

		/// <summary>
		///     ID of the project.
		/// </summary>
		[JsonProperty("projectId")]
		public long ProjectId { get; set; }

		/// <summary>
		///     MPBC of the project.
		/// </summary>
		[JsonProperty("projectMpbc")]
		public string ProjectMpbc { get; set; }

		/// <summary>
		///     MPCC of the project.
		/// </summary>
		[JsonProperty("projectMpcc")]
		public string ProjectMpcc { get; set; }

		/// <summary>
		///     Name of the project.
		/// </summary>
		[JsonProperty("projectName")]
		public string ProjectName { get; set; }

		/// <summary>
		///     PublishedVersion is the version number of the published
		///     catalog. It is incremented when the publish task publishes the
		///     catalog.
		/// </summary>
		[JsonProperty("publishedVersion")]
		public long? PublishedVersion { get; set; }

		/// <summary>
		///     SageContract represents the internal identifier at Meplato for
		///     the contract of this catalog.
		/// </summary>
		[JsonProperty("sageContract")]
		public string SageContract { get; set; }

		/// <summary>
		///     SageNumber represents the internal identifier at Meplato for
		///     the merchant of this catalog.
		/// </summary>
		[JsonProperty("sageNumber")]
		public string SageNumber { get; set; }

		/// <summary>
		///     URL to this page.
		/// </summary>
		[JsonProperty("selfLink")]
		public string SelfLink { get; set; }

		/// <summary>
		///     State describes the current state of the catalog, e.g. idle.
		/// </summary>
		[JsonProperty("state")]
		public string State { get; set; }

		/// <summary>
		///     SupportsOciBackgroundsearch indicates whether a catalog
		///     supports the OCI BACKGROUNDSEARCH transaction.
		/// </summary>
		[JsonProperty("supportsOciBackgroundsearch")]
		public bool SupportsOciBackgroundsearch { get; set; }

		/// <summary>
		///     SupportsOciDetail indicates whether a catalog supports the OCI
		///     DETAIL transaction.
		/// </summary>
		[JsonProperty("supportsOciDetail")]
		public bool SupportsOciDetail { get; set; }

		/// <summary>
		///     SupportsOciDetailadd indicates whether a catalog supports the
		///     OCI DETAILADD transaction.
		/// </summary>
		[JsonProperty("supportsOciDetailadd")]
		public bool SupportsOciDetailadd { get; set; }

		/// <summary>
		///     SupportsOciDownloadjson indicates whether a catalog supports
		///     the OCI DOWNLOADJSON transaction.
		/// </summary>
		[JsonProperty("supportsOciDownloadjson")]
		public bool SupportsOciDownloadjson { get; set; }

		/// <summary>
		///     SupportsOciQuantitycheck indicates whether a catalog supports
		///     the OCI QUANTITYCHECK transaction.
		/// </summary>
		[JsonProperty("supportsOciQuantitycheck")]
		public bool SupportsOciQuantitycheck { get; set; }

		/// <summary>
		///     SupportsOciSourcing indicates whether a catalog supports the
		///     OCI SOURCING transaction.
		/// </summary>
		[JsonProperty("supportsOciSourcing")]
		public bool SupportsOciSourcing { get; set; }

		/// <summary>
		///     SupportsOciValidate indicates whether a catalog supports the
		///     OCI VALIDATE transaction.
		/// </summary>
		[JsonProperty("supportsOciValidate")]
		public bool SupportsOciValidate { get; set; }

		/// <summary>
		///     Target represents the target system which can be either an
		///     empty string, "catscout" or "mall".
		/// </summary>
		[JsonProperty("target")]
		public string Target { get; set; }

		/// <summary>
		///     Type represents a catalog type which can be either "CC" 1:1
		///     Corporate or "MB" Meplato Business 1 Creditor.
		/// </summary>
		[JsonProperty("type")]
		public string Type { get; set; }

		/// <summary>
		///     Updated is the last modification date and time of the catalog.
		/// </summary>
		[JsonProperty("updated")]
		public DateTimeOffset? Updated { get; set; }

		/// <summary>
		///     ValidFrom is the date the catalog becomes effective
		///     (YYYY-MM-DD).
		/// </summary>
		[JsonProperty("validFrom")]
		public string ValidFrom { get; set; }

		/// <summary>
		///     ValidUntil is the date the catalog expires (YYYY-MM-DD).
		/// </summary>
		[JsonProperty("validUntil")]
		public string ValidUntil { get; set; }

		#endregion // Catalog
	}

	/// <summary>
	///     CreateCatalog holds the properties of a new catalog.
	/// </summary>
	public class CreateCatalog
	{
		#region CreateCatalog

		/// <summary>
		///     Country is the ISO-3166 alpha-2 code for the country that the
		///     catalog is destined for (e.g. DE or US).
		/// </summary>
		[JsonProperty("country")]
		public string Country { get; set; }

		/// <summary>
		///     Currency is the ISO-4217 currency code that is used for all
		///     products in the catalog (e.g. EUR or USD).
		/// </summary>
		[JsonProperty("currency")]
		public string Currency { get; set; }

		/// <summary>
		///     Description of the catalog.
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		///     Language is the IETF language tag of the language of all
		///     products in the catalog (e.g. de or pt-BR).
		/// </summary>
		[JsonProperty("language")]
		public string Language { get; set; }

		/// <summary>
		///     ID of the merchant.
		/// </summary>
		[JsonProperty("merchantId")]
		public long MerchantId { get; set; }

		/// <summary>
		///     Name of the catalog.
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     ID of the project.
		/// </summary>
		[JsonProperty("projectId")]
		public long ProjectId { get; set; }

		/// <summary>
		///     MPCC of the project.
		/// </summary>
		[JsonProperty("projectMpcc")]
		public string ProjectMpcc { get; set; }

		/// <summary>
		///     SageContract represents the internal identifier at Meplato for
		///     the contract of this catalog.
		/// </summary>
		[JsonProperty("sageContract")]
		public string SageContract { get; set; }

		/// <summary>
		///     SageNumber represents the internal identifier at Meplato for
		///     the merchant of this catalog.
		/// </summary>
		[JsonProperty("sageNumber")]
		public string SageNumber { get; set; }

		/// <summary>
		///     Target represents the target system which can be either an
		///     empty string, "catscout" or "mall".
		/// </summary>
		[JsonProperty("target")]
		public string Target { get; set; }

		/// <summary>
		///     Type represents a catalog type which can be either "CC" 1:1
		///     Corporate or "MB" Meplato Business 1 Creditor.
		/// </summary>
		[JsonProperty("type")]
		public string Type { get; set; }

		/// <summary>
		///     ValidFrom is the date the catalog becomes effective
		///     (YYYY-MM-DD).
		/// </summary>
		[JsonProperty("validFrom")]
		public string ValidFrom { get; set; }

		/// <summary>
		///     ValidUntil is the date the catalog expires (YYYY-MM-DD).
		/// </summary>
		[JsonProperty("validUntil")]
		public string ValidUntil { get; set; }

		#endregion // CreateCatalog
	}

	/// <summary>
	///     CustField describes a generic name/value pair. Its purpose is
	///     to provide a mechanism for customer-specific fields.
	/// </summary>
	public class CustField
	{
		#region CustField

		/// <summary>
		///     Name is the name of the customer-specific field, e.g. TaxRate.
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     Value is the value of the customer-specific field, e.g. 19%.
		/// </summary>
		[JsonProperty("value")]
		public string Value { get; set; }

		#endregion // CustField
	}

	/// <summary>
	///     KPISummary represents the outcome of analyzing the contents for
	///     key performance indicators.
	/// </summary>
	public class KPISummary
	{
		#region KPISummary

		/// <summary>
		///     Coefficients represents the weight that is used to calculate
		///     the weighted coefficients for a criteria. It relies on the
		///     medal stored in DegreesOfFulfillment.
		/// </summary>
		[JsonProperty("coefficients")]
		public IDictionary<string, double> Coefficients { get; set; }

		/// <summary>
		///     CreatedAt is the date/time when the KPI summary has been
		///     created.
		/// </summary>
		[JsonProperty("createdAt")]
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		///     DegreesOfFulfillment represents the medal for all KPI criteria:
		///     3 for gold, 2 for silver, 1 for bronze, 0 for no medal.
		/// </summary>
		[JsonProperty("degreesOfFulfillment")]
		public IDictionary<string, int> DegreesOfFulfillment { get; set; }

		/// <summary>
		///     FinalResult returns a value between 0.0 and 1.0 that describes
		///     the weighted sum of all content-related test criteria.
		/// </summary>
		[JsonProperty("finalResult")]
		public double FinalResult { get; set; }

		/// <summary>
		///     OverallResult returns 3 for a gold medal, 2 for a silver medal,
		///     1 for a bronze medal, and 0 for no medal.
		/// </summary>
		[JsonProperty("overallResult")]
		public int OverallResult { get; set; }

		/// <summary>
		///     TestResults represents the unweighted outcome for a specific
		///     KPI criteria, i.e. the percentage of products that fulfill the
		///     criteria.
		/// </summary>
		[JsonProperty("testResults")]
		public IDictionary<string, double> TestResults { get; set; }

		/// <summary>
		///     WeightedCoefficients is a value between 0.0 and 1.0 that
		///     represents the weighted outcome of a KPI criteria, as
		///     calculated by the coefficient and the test result.
		/// </summary>
		[JsonProperty("weightedCoefficients")]
		public IDictionary<string, double> WeightedCoefficients { get; set; }

		#endregion // KPISummary
	}

	/// <summary>
	///     Project describes customer-specific settings, typically
	///     encompassing a set of catalogs.
	/// </summary>
	public class Project
	{
		#region Project

		/// <summary>
		///     Country specifies the country code where catalogs for this
		///     project are located.
		/// </summary>
		[JsonProperty("country")]
		public string Country { get; set; }

		/// <summary>
		///     Created is the creation date and time of the project.
		/// </summary>
		[JsonProperty("created")]
		public DateTimeOffset? Created { get; set; }

		/// <summary>
		///     ID is a unique (internal) identifier of the project.
		/// </summary>
		[JsonProperty("id")]
		public long Id { get; set; }

		/// <summary>
		///     Kind is store#project for a project entity.
		/// </summary>
		[JsonProperty("kind")]
		public string Kind { get; set; }

		/// <summary>
		///     Language specifies the language code of the catalogs of this
		///     project.
		/// </summary>
		[JsonProperty("language")]
		public string Language { get; set; }

		/// <summary>
		///     MPBC is the Meplato Buyer Code that identifies a set of
		///     buy-side companies that belong together.
		/// </summary>
		[JsonProperty("mpbc")]
		public string Mpbc { get; set; }

		/// <summary>
		///     MPCC is the Meplato Company Code that uniquely identifies the
		///     buy-side.
		/// </summary>
		[JsonProperty("mpcc")]
		public string Mpcc { get; set; }

		/// <summary>
		///     Name is a short description of the project.
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///     URL to this page.
		/// </summary>
		[JsonProperty("selfLink")]
		public string SelfLink { get; set; }

		/// <summary>
		///     Type describes the type of project which can be either
		///     corporate or basic.
		/// </summary>
		[JsonProperty("type")]
		public string Type { get; set; }

		/// <summary>
		///     Updated is the last modification date and time of the project.
		/// </summary>
		[JsonProperty("updated")]
		public DateTimeOffset? Updated { get; set; }

		/// <summary>
		///     Visible indicates whether this project is visible to merchants.
		/// </summary>
		[JsonProperty("visible")]
		public bool Visible { get; set; }

		#endregion // Project
	}

	/// <summary>
	///     PublishResponse is the response of the request to publish a
	///     catalog.
	/// </summary>
	public class PublishResponse
	{
		#region PublishResponse

		/// <summary>
		///     Kind is store#catalogPublish for this kind of response.
		/// </summary>
		[JsonProperty("kind")]
		public string Kind { get; set; }

		/// <summary>
		///     SelfLink returns the URL to this page.
		/// </summary>
		[JsonProperty("selfLink")]
		public string SelfLink { get; set; }

		/// <summary>
		///     StatusLink returns the URL that returns the current status of
		///     the request.
		/// </summary>
		[JsonProperty("statusLink")]
		public string StatusLink { get; set; }

		#endregion // PublishResponse
	}

	/// <summary>
	///     PublishStatusResponse returns current information about the
	///     status of a publish request.
	/// </summary>
	public class PublishStatusResponse
	{
		#region PublishStatusResponse

		/// <summary>
		///     Busy indicates whether the catalog is still busy.
		/// </summary>
		[JsonProperty("busy")]
		public bool Busy { get; set; }

		/// <summary>
		///     Canceled indicates whether the publishing process has been
		///     canceled.
		/// </summary>
		[JsonProperty("canceled")]
		public bool Canceled { get; set; }

		/// <summary>
		///     CurrentStep is an indicator of the current step in the total
		///     list of steps. Use in combination with TotalSteps to retrieve
		///     the progress in percent.
		/// </summary>
		[JsonProperty("currentStep")]
		public long CurrentStep { get; set; }

		/// <summary>
		///     Done indicates whether publishing is finished.
		/// </summary>
		[JsonProperty("done")]
		public bool Done { get; set; }

		/// <summary>
		///     Kind is store#catalogPublishStatus for this kind of response.
		/// </summary>
		[JsonProperty("kind")]
		public string Kind { get; set; }

		/// <summary>
		///     Percent indicates the progress of the publish request.
		/// </summary>
		[JsonProperty("percent")]
		public int Percent { get; set; }

		/// <summary>
		///     SelfLink returns the URL to this page.
		/// </summary>
		[JsonProperty("selfLink")]
		public string SelfLink { get; set; }

		/// <summary>
		///     Status describes the general status of the publish request.
		/// </summary>
		[JsonProperty("status")]
		public string Status { get; set; }

		/// <summary>
		///     TotalSteps is an indicator of the total number steps required
		///     to complete the publish request. Use in combination with
		///     CurrentStep.
		/// </summary>
		[JsonProperty("totalSteps")]
		public long TotalSteps { get; set; }

		#endregion // PublishStatusResponse
	}

	/// <summary>
	///     PurgeResponse is the response of the request to purge an area
	///     of a catalog.
	/// </summary>
	public class PurgeResponse
	{
		#region PurgeResponse

		/// <summary>
		///     Kind is store#catalogPurge for this kind of response.
		/// </summary>
		[JsonProperty("kind")]
		public string Kind { get; set; }

		#endregion // PurgeResponse
	}

	/// <summary>
	///     SearchResponse is a partial listing of catalogs.
	/// </summary>
	public class SearchResponse
	{
		#region SearchResponse

		/// <summary>
		///     Items is the slice of catalogs of this result.
		/// </summary>
		[JsonProperty("items")]
		public Catalog[] Items { get; set; }

		/// <summary>
		///     Kind is store#catalogs for this kind of response.
		/// </summary>
		[JsonProperty("kind")]
		public string Kind { get; set; }

		/// <summary>
		///     NextLink returns the URL to the next slice of catalogs (if
		///     any).
		/// </summary>
		[JsonProperty("nextLink")]
		public string NextLink { get; set; }

		/// <summary>
		///     PreviousLink returns the URL of the previous slice of catalogs
		///     (if any).
		/// </summary>
		[JsonProperty("previousLink")]
		public string PreviousLink { get; set; }

		/// <summary>
		///     SelfLink returns the URL to this page.
		/// </summary>
		[JsonProperty("selfLink")]
		public string SelfLink { get; set; }

		/// <summary>
		///     TotalItems describes the total number of catalogs found.
		/// </summary>
		[JsonProperty("totalItems")]
		public long TotalItems { get; set; }

		#endregion // SearchResponse
	}

	/// <summary>
	///     CreateService: Create a new catalog (admin only).
	/// </summary>
	public class CreateService
	{
		#region CreateService

		private readonly Service _service;
		private readonly IDictionary<string, object> _opt = new Dictionary<string, object>();
		private readonly IDictionary<string, string> _hdr = new Dictionary<string, string>();

		private CreateCatalog _catalog;

		/// <summary>
		///     Creates a new instance of CreateService.
		/// </summary>
		public CreateService(Service service)
		{
			_service = service;
		}

		/// <summary>
		///     Catalog properties of the new catalog.
		/// </summary>
		public CreateService Catalog(CreateCatalog catalog)
		{
			_catalog = catalog;
			return this;
		}

		/// <summary>
		///     Execute the operation.
		/// </summary>
		public async Task<Catalog> Do()
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

			var uriTemplate = _service.BaseURL + "/catalogs";
			var response = await _service.Client.Execute(
				HttpMethod.Post,
				uriTemplate,
				parameters,
				headers,
				_catalog);
			return response.GetBodyJSON<Catalog>();
		}

		#endregion // CreateService
	}

	/// <summary>
	///     GetService: Get a single catalog.
	/// </summary>
	public class GetService
	{
		#region GetService

		private readonly Service _service;
		private readonly IDictionary<string, object> _opt = new Dictionary<string, object>();
		private readonly IDictionary<string, string> _hdr = new Dictionary<string, string>();

		private string _pin;

		/// <summary>
		///     Creates a new instance of GetService.
		/// </summary>
		public GetService(Service service)
		{
			_service = service;
		}

		/// <summary>
		///     PIN of the catalog.
		/// </summary>
		public GetService Pin(string pin)
		{
			_pin = pin;
			return this;
		}

		/// <summary>
		///     Execute the operation.
		/// </summary>
		public async Task<Catalog> Do()
			{
			// Make a copy of the parameters and add the path parameters to it
			var parameters = new Dictionary<string, object>();
			// UriTemplates package wants path parameters as strings
			parameters["pin"] = string.Format("{0}", _pin);

			// Make a copy of the header parameters and set UA
			var headers = new Dictionary<string, string>();
			string authorization = _service.GetAuthorizationHeader();
			if (!string.IsNullOrEmpty(authorization))
			{
				headers["Authorization"] = authorization;
			}

			var uriTemplate = _service.BaseURL + "/catalogs/{pin}";
			var response = await _service.Client.Execute(
				HttpMethod.Get,
				uriTemplate,
				parameters,
				headers,
				null);
			return response.GetBodyJSON<Catalog>();
		}

		#endregion // GetService
	}

	/// <summary>
	///     PublishService: Publishes a catalog.
	/// </summary>
	public class PublishService
	{
		#region PublishService

		private readonly Service _service;
		private readonly IDictionary<string, object> _opt = new Dictionary<string, object>();
		private readonly IDictionary<string, string> _hdr = new Dictionary<string, string>();

		private string _pin;

		/// <summary>
		///     Creates a new instance of PublishService.
		/// </summary>
		public PublishService(Service service)
		{
			_service = service;
		}

		/// <summary>
		///     PIN of the catalog to publish.
		/// </summary>
		public PublishService Pin(string pin)
		{
			_pin = pin;
			return this;
		}

		/// <summary>
		///     Execute the operation.
		/// </summary>
		public async Task<PublishResponse> Do()
			{
			// Make a copy of the parameters and add the path parameters to it
			var parameters = new Dictionary<string, object>();
			// UriTemplates package wants path parameters as strings
			parameters["pin"] = string.Format("{0}", _pin);

			// Make a copy of the header parameters and set UA
			var headers = new Dictionary<string, string>();
			string authorization = _service.GetAuthorizationHeader();
			if (!string.IsNullOrEmpty(authorization))
			{
				headers["Authorization"] = authorization;
			}

			var uriTemplate = _service.BaseURL + "/catalogs/{pin}/publish";
			var response = await _service.Client.Execute(
				HttpMethod.Post,
				uriTemplate,
				parameters,
				headers,
				null);
			return response.GetBodyJSON<PublishResponse>();
		}

		#endregion // PublishService
	}

	/// <summary>
	///     PublishStatusService: Status of a publish process.
	/// </summary>
	public class PublishStatusService
	{
		#region PublishStatusService

		private readonly Service _service;
		private readonly IDictionary<string, object> _opt = new Dictionary<string, object>();
		private readonly IDictionary<string, string> _hdr = new Dictionary<string, string>();

		private string _pin;

		/// <summary>
		///     Creates a new instance of PublishStatusService.
		/// </summary>
		public PublishStatusService(Service service)
		{
			_service = service;
		}

		/// <summary>
		///     PIN of the catalog to get the publish status from.
		/// </summary>
		public PublishStatusService Pin(string pin)
		{
			_pin = pin;
			return this;
		}

		/// <summary>
		///     Execute the operation.
		/// </summary>
		public async Task<PublishStatusResponse> Do()
			{
			// Make a copy of the parameters and add the path parameters to it
			var parameters = new Dictionary<string, object>();
			// UriTemplates package wants path parameters as strings
			parameters["pin"] = string.Format("{0}", _pin);

			// Make a copy of the header parameters and set UA
			var headers = new Dictionary<string, string>();
			string authorization = _service.GetAuthorizationHeader();
			if (!string.IsNullOrEmpty(authorization))
			{
				headers["Authorization"] = authorization;
			}

			var uriTemplate = _service.BaseURL + "/catalogs/{pin}/publish/status";
			var response = await _service.Client.Execute(
				HttpMethod.Get,
				uriTemplate,
				parameters,
				headers,
				null);
			return response.GetBodyJSON<PublishStatusResponse>();
		}

		#endregion // PublishStatusService
	}

	/// <summary>
	///     PurgeService: Purge the work or live area of a catalog, i.e.
	///     remove all products in the given area, but do not delete the
	///     catalog itself.
	/// </summary>
	public class PurgeService
	{
		#region PurgeService

		private readonly Service _service;
		private readonly IDictionary<string, object> _opt = new Dictionary<string, object>();
		private readonly IDictionary<string, string> _hdr = new Dictionary<string, string>();

		private string _pin;
		private string _area;

		/// <summary>
		///     Creates a new instance of PurgeService.
		/// </summary>
		public PurgeService(Service service)
		{
			_service = service;
		}

		/// <summary>
		///     Area of the catalog to purge, i.e. work or live.
		/// </summary>
		public PurgeService Area(string area)
		{
			_area = area;
			return this;
		}

		/// <summary>
		///     PIN of the catalog to purge.
		/// </summary>
		public PurgeService Pin(string pin)
		{
			_pin = pin;
			return this;
		}

		/// <summary>
		///     Execute the operation.
		/// </summary>
		public async Task<PurgeResponse> Do()
			{
			// Make a copy of the parameters and add the path parameters to it
			var parameters = new Dictionary<string, object>();
			// UriTemplates package wants path parameters as strings
			parameters["area"] = string.Format("{0}", _area);
			// UriTemplates package wants path parameters as strings
			parameters["pin"] = string.Format("{0}", _pin);

			// Make a copy of the header parameters and set UA
			var headers = new Dictionary<string, string>();
			string authorization = _service.GetAuthorizationHeader();
			if (!string.IsNullOrEmpty(authorization))
			{
				headers["Authorization"] = authorization;
			}

			var uriTemplate = _service.BaseURL + "/catalogs/{pin}/{area}";
			var response = await _service.Client.Execute(
				HttpMethod.Delete,
				uriTemplate,
				parameters,
				headers,
				null);
			return response.GetBodyJSON<PurgeResponse>();
		}

		#endregion // PurgeService
	}

	/// <summary>
	///     SearchService: Search for catalogs.
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
		///     Q defines are full text query.
		/// </summary>
		public SearchService Q(string q)
		{
			_opt["q"] = q;
			return this;
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
		///     Sort order, e.g. name or id or -created (default: score).
		/// </summary>
		public SearchService Sort(string sort)
		{
			_opt["sort"] = sort;
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
			if (_opt.ContainsKey("q"))
			{
				// UriTemplates package wants query parameters as strings
				parameters["q"] = string.Format("{0}", _opt["q"]);
			}
			if (_opt.ContainsKey("skip"))
			{
				// UriTemplates package wants query parameters as strings
				parameters["skip"] = string.Format("{0}", _opt["skip"]);
			}
			if (_opt.ContainsKey("sort"))
			{
				// UriTemplates package wants query parameters as strings
				parameters["sort"] = string.Format("{0}", _opt["sort"]);
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

			var uriTemplate = _service.BaseURL + "/catalogs{?q,skip,take,sort}";
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

