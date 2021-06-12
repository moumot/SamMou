using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamMou.Api.GraphQL
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(QueryObject query, MutationObject mutation, IServiceProvider sp) : base(sp)
        {
                Query = query;
                Mutation = mutation;
        }
    }
}
