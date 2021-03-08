using Xero.Api.Common;
using Xero.Api.Core.Endpoints.Base;
using Xero.Api.Core.Model;
using Xero.Api.Core.Request;
using Xero.Api.Core.Response;
using Xero.Api.Infrastructure.Http;

namespace Xero.Api.Core.Endpoints
{
    public interface IPaymentsEndpoint : IXeroUpdateEndpoint<IPaymentsEndpoint, Payment, PaymentsRequest, PaymentsResponse>,
        IPageableEndpoint<IPaymentsEndpoint>
    {

    }

    public class PaymentsEndpoint
        : FourDecimalPlacesEndpoint<IPaymentsEndpoint, Payment, PaymentsRequest, PaymentsResponse>, IPaymentsEndpoint
    {
        public PaymentsEndpoint(XeroHttpClient client) :
            base(client, "/api.xro/2.0/Payments")
        {
        }

        public IPaymentsEndpoint Page(int page)
        {
            AddParameter("page", page);
            return this;
        }

        public override void ClearQueryString()
        {
            base.ClearQueryString();
            Page(1);
        }
    }
}