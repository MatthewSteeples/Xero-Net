﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Xero.Api.Infrastructure.Http;
using Xero.Api.Infrastructure.Interfaces;

namespace Xero.Api.Common
{
    public abstract class XeroReadEndpoint<T, TResult, TResponse> : IXeroReadEndpoint<T, TResult, TResponse> 
        where T : IXeroReadEndpoint<T, TResult, TResponse>
        where TResponse : IXeroResponse<TResult>, new()
    {
        private DateTime? _modifiedSince;
        private string _query;
        private string _orderBy;
        
        protected NameValueCollection Parameters { get; private set; }

        protected string ApiEndpointUrl { get; private set; }
        public XeroHttpClient Client { get; private set; }

        protected XeroReadEndpoint(XeroHttpClient client, string apiEndpointUrl)
        {
            Client = client;
            ApiEndpointUrl = apiEndpointUrl;
        }

        public T ModifiedSince(DateTime modified)
        {
            _modifiedSince = modified;
            return (T)(IXeroReadEndpoint<T, TResult, TResponse>)this;
        }

        public T Where(string query)
        {
            _query = query;
            return (T)(IXeroReadEndpoint<T, TResult, TResponse>)this;
        }

        public T Or(string query)
        {
            _query = string.Concat(_query, " OR ", query);
            return (T)(IXeroReadEndpoint<T, TResult, TResponse>)this;
        }

        public T And(string query)
        {
            _query = string.Concat(_query, " AND ", query);
            return (T)(IXeroReadEndpoint<T, TResult, TResponse>)this;
        }

        public T OrderBy(string query)
        {
            _orderBy = query;
            return (T)(IXeroReadEndpoint<T, TResult, TResponse>)this;
        }

        public T OrderByDescending(string query)
        {
            _orderBy = query + " DESC";
            return (T)(IXeroReadEndpoint<T, TResult, TResponse>)this;
        }

        public T UseFourDecimalPlaces(bool use4Dp)
        {
            Apply4Dp(use4Dp);

            return (T)(IXeroReadEndpoint<T, TResult, TResponse>)this;
        }

        public virtual IEnumerable<TResult> Find()
        {
            return Get(ApiEndpointUrl, null);
        }

        public virtual TResult Find(Guid child)
        {
            return Find(child.ToString("D"));
        }

        public TResult Find(string child)
        {
            return Get(ApiEndpointUrl, "/" + child).FirstOrDefault();
        }

        public virtual void ClearQueryString()
        {
            _orderBy = null;
            _query = null;
            _modifiedSince = null;
            Parameters = null;
        }

        protected void Apply4Dp(bool use4Dp)
        {
            const string name = "unitdp";

            if (use4Dp)
            {
                AddParameter(name, 4);
            }
            else
            {
                RemoveParameter(name);
            }
        }

        public string QueryString
        {
            get
            {
                return new QueryGenerator(_query, _orderBy, Parameters).QueryString;
            }
        }

        internal protected T AddParameter(string name, int value)
        {
            return AddParameter(name, value.ToString("D"));
        }

        internal protected T AddParameter(string name, bool value)
        {
            return AddParameter(name, value.ToString().ToLower());
        }

        internal void RemoveParameter(string name)
        {
            if (Parameters != null)
            {
                Parameters.Remove(name);
            }
        }

        internal protected T AddParameter(string name, string value)
        {
            if (Parameters == null)
            {
                Parameters = new NameValueCollection();
            }

            Parameters[name] = value;

            return (T)(IXeroReadEndpoint<T, TResult, TResponse>)this;
        }

        internal protected T AddParameters(NameValueCollection parameters)
        {
            if (Parameters == null)
            {
                Parameters = parameters;
            }
            else
            {
                Parameters.Add(parameters);
            }

            return (T)(IXeroReadEndpoint<T, TResult, TResponse>)this;
        }

        private IEnumerable<TResult> Get(string endpoint, string child)
        {
            try
            {
                if (Parameters == null)
                {
                    Parameters = new NameValueCollection();
                }

                Client.Where = _query;
                Client.Order = _orderBy;
                Client.ModifiedSince = _modifiedSince;
                Client.Parameters = Parameters;

                var result = Client.Get<TResult, TResponse>(endpoint + (child ?? string.Empty));
                return result;
            }
            finally
            {
                ClearQueryString();
            }
        }
    }
}
