// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using Shouldly;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Dism.Tests
{
    public class GetFeaturesTest : DismTestBase
    {
        private static readonly DismPackageFeatureState[] PackageFeatureStateValues = Enum.GetValues(typeof(DismPackageFeatureState)).Cast<DismPackageFeatureState>().ToArray();

        public GetFeaturesTest(TestWimTemplate template, ITestOutputHelper testOutput)
            : base(template, testOutput)
        {
        }

        [Fact]
        public void GetFeaturesFromOnlineSession()
        {
            using (DismSession session = DismApi.OpenOnlineSession())
            {
                ValidateFeatures(DismApi.GetFeatures(session));
            }
        }

        internal static void ValidateFeatures(DismFeatureCollection features)
        {
            features.ShouldNotBeNull();

            foreach (DismFeature feature in features)
            {
                feature.FeatureName.ShouldNotBeNullOrWhiteSpace();

                feature.State.ShouldBeOneOf(PackageFeatureStateValues);
            }
        }
    }
}