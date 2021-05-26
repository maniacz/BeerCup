using BeerCup.DataAccess.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.DataAccess
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly BeerCupStorageContext context;

        public QueryExecutor(BeerCupStorageContext context)
        {
            this.context = context;
        }

        public Task<TResult> Execute<TResult>(QueryBase<TResult> query)
        {
            return query.Execute(this.context);
        }
    }
}
