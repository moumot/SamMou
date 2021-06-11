using System;
using System.Collections.Generic;
using SamMou.Api.DataContext;

namespace SamMou.Api.GraphQL
{
    public class GraphQLSamMouDataContext : Dictionary<string, object>
    {
        public SamMouDataContext Context { get; set; }

        public GraphQLSamMouDataContext(SamMouDataContext dataContext)
        {
            Context = dataContext;
        }
    }  
}
