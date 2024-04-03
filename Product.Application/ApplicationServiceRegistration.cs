﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Common.Behaviours;
using System.Reflection;

namespace Product.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(ApplicationServiceRegistration)))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}
