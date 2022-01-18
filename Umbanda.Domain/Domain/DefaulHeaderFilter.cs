using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Umbanda.Domain.Domain
{
    public class DefaulHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (string.Equals(context.ApiDescription.HttpMethod,
                HttpMethod.Post.Method,
                StringComparison.InvariantCultureIgnoreCase))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "RemoteAddress",
                    In = ParameterLocation.Header,
                    Required = false,
                    Example = new OpenApiString("192.168.7.77")
                });
            }
        }
    }
}
