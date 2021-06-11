using System;
using System.Threading;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using SamMou.Api.DataContext;
using SamMou.Api.GraphQL;

namespace SamMou.Api.Controllers
{
#nullable enable
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastGraphController : ControllerBase
    {
        private readonly IDocumentExecuter _executer;
        private readonly ISchema _schema;

        public WeatherForecastGraphController(GraphQLSchema schema, IDocumentExecuter executer)
        {
            _schema = schema;
            _executer = executer;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostBody query)
        {

            var result = await _executer.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = query.Query;
                _.Inputs = query.Variables?.ToInputs();

            });
            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }
            return Ok(result.Data);
        }

        public class PostBody
        {
            public string? OperationName { get; set; }
            public string Query { get; set; } = null!;
            public JObject? Variables { get; set; }
        }
    }
#nullable disable
}
