﻿using Api.ResultWrapper.AspNetCore.Extensions;
using Api.ResultWrapper.AspNetCore.WrapperModel;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Api.ResultWrapper.AspNetCore.Filters
{
    public class ModelStateFeatureFilter : Attribute, IAsyncActionFilter
    { 
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var state = context.ModelState;
            context.HttpContext.Features.Set<ModelStateFeature>(new ModelStateFeature(state));
            if (!state.IsValid)
            {
               throw new ApiException("Your Request is not valid", 500, ModelStateExtension.AllErrors(state));
            }
            await next();
        } 
    }
}
