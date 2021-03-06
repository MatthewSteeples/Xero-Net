﻿using System.Collections.Specialized;
using Xero.Api.Common;
using Xero.Api.Core.Model;
using Xero.Api.Core.Response;
using Xero.Api.Infrastructure.Http;

namespace Xero.Api.Core.Endpoints
{
    public interface IJournalsEndpoint : IXeroReadEndpoint<IJournalsEndpoint, Journal, JournalsResponse>
    {
        IJournalsEndpoint Offset(int offset);
        IJournalsEndpoint PaymentsOnly(bool value);
    }

    public class JournalsEndpoint : XeroReadEndpoint<IJournalsEndpoint, Journal, JournalsResponse>, IJournalsEndpoint
    {
        public JournalsEndpoint(XeroHttpClient client) :
            base(client, "/api.xro/2.0/Journals")
        {
        }

        public IJournalsEndpoint Offset(int offset)
        {
            AddParameter("offset", offset);
            return this;
        }

        public IJournalsEndpoint PaymentsOnly(bool value)
        {
            AddParameter("paymentsOnly", value);
            return this;
        }
    }
}
