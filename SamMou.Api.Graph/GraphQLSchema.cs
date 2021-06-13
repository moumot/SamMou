using System;
using GraphQL.Types;

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
