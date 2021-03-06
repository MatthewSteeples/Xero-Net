﻿using System;
using System.Runtime.Serialization;

namespace Xero.Api.Core.Model
{
	[Serializable]
	public class ContactCisSetting
    {
        [DataMember(Name = "CISEnabled")]
        public bool CisEnabled { get; set; }

        [DataMember]
        public decimal Rate { get; set; }
    }
}