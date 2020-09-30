using System.Runtime.Serialization;
using Xero.Api.Common;
using Xero.Api.Core.Model;
using Xero.Api.Infrastructure.Exceptions;

namespace Xero.Api.Core.Request
{
    [CollectionDataContract(Namespace = "", Name = "Accounts")]
    public class AccountsRequest : XeroRequest<Account>
    {
        public override bool ContainsItems()
        {
            if (this.Count > 1)
                throw new BadRequestException("Accounts can only be Created or Updated 1 at a time");

            return base.ContainsItems();
        }
    }
}