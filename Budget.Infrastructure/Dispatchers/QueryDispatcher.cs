﻿using Autofac;
using Budget.Application.Query.Abstractions;

namespace Budget.Infrastructure.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IComponentContext componentContext;

        public QueryDispatcher(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public Task<TModel> ExecuteAsync<TModel>(IQuery<TModel> query)
        {
            var queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TModel));

            var handler = componentContext.Resolve(queryHandlerType);

            return (Task<TModel>)queryHandlerType
                .GetMethod("HandleAsync")
                .Invoke(handler, new object[] { query });
        }
    }
}
