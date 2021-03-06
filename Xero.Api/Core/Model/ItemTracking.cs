﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xero.Api.Core.Model
{
    [Serializable]
    [CollectionDataContract(Namespace = "", Name = "Tracking")]
    public class ItemTracking : List<ItemTrackingCategory>
    {
        public ItemTracking() { }

        public ItemTracking(IEnumerable<ItemTrackingCategory> itemTrackingCategories) : base(itemTrackingCategories) { }
    }
}