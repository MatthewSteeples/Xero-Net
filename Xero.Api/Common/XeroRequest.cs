using System.Collections.Generic;
using System.Linq;
using Xero.Api.Infrastructure.Interfaces;

namespace Xero.Api.Common
{
    public class XeroRequest<T> : List<T>, IXeroRequest<T>
    {
        public XeroRequest()
        {
        }

        public XeroRequest(IEnumerable<T> items)
        {
            AddRange(items);
        }

        public IList<T> Items
        {
            get { return this; }
        }

        public virtual bool ContainsItems()
        {
            return this.Any();
        }
    }
}
