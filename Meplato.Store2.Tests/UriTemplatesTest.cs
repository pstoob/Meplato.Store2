﻿#region Copyright and terms of services

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
using NUnit.Framework;
using Resta.UriTemplates;

namespace Meplato.Store2.Tests
{
    [TestFixture]
    public class UriTemplatesTest : TestCase
    {
        [Test]
        public void TestResolving()
        {
            var template = new UriTemplate("http://store2.io/api/v2/catalogs/{pin}/products/scroll{?pageToken,pretty}");

            var uri = template.Resolve(new Dictionary<string, object>
            {
                {"pin", "13"}, // int here fails!
                {"pageToken", null}
            });

            Assert.AreEqual("http://store2.io/api/v2/catalogs/13/products/scroll", uri);
        }
    }
}