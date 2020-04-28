﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xero.Api.Common;
using Xero.Api.Core.Endpoints.Base;
using Xero.Api.Core.Model;
using Xero.Api.Core.Request;
using Xero.Api.Core.Response;
using Xero.Api.Infrastructure.Http;

namespace Xero.Api.Core.Endpoints
{
    public interface IContactsEndpoint
        : IXeroUpdateEndpoint<IContactsEndpoint, Contact, ContactsRequest, ContactsResponse>,
        IPageableEndpoint<IContactsEndpoint>
    {
        IContactsEndpoint IncludeArchived(bool include);
        ContactCisSetting GetCisSettings(Guid id);
        IContactsEndpoint Ids(IEnumerable<Guid> ids);
    }

    public class ContactsEndpoint
        : XeroUpdateEndpoint<IContactsEndpoint, Contact, ContactsRequest, ContactsResponse>, IContactsEndpoint
    {
        internal ContactsEndpoint(XeroHttpClient client)
            : base(client, "/api.xro/2.0/Contacts")
        {
            Page(1);
        }

        public IContactsEndpoint Page(int page)
        {
            AddParameter("page", page);
            return this;
        }

        public IContactsEndpoint IncludeArchived(bool include)
        {
            if (include)
            {
                AddParameter("includeArchived", true);
            }

            return this;
        }

        public ContactCisSetting GetCisSettings(Guid id)
        {
            var contactCisSettings = Client.Get<ContactCisSetting, ContactCisSettingsResponse>(string.Format("/api.xro/2.0/contacts/{0}/cissettings", id));
            return contactCisSettings.FirstOrDefault();
        }

        public override void ClearQueryString()
        {
            base.ClearQueryString();
            Page(1);
        }

		public IContactsEndpoint Ids(IEnumerable<Guid> ids)
        {
            AddParameter("ids", string.Join(",", ids));

            return this;
        }
    }
}