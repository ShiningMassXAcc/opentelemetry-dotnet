// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Trace;

public class Program
{
    private static readonly ActivitySource MyActivitySource = new("MyCompany.MyProduct.MyLibrary");

    public static void Main()
    {
        IServiceCollection services = new ServiceCollection();

        // Add tracing
        services.AddOpenTelemetry()
            .WithTracing(tracingBuilder =>
            {
                tracingBuilder
                    .AddSource(MyActivitySource.Name)
                    .AddConsoleExporter();
            });

        var serviceProvider = services.BuildServiceProvider();

        using (var activity = MyActivitySource.StartActivity("SayHello"))
        {
            if (activity is null)
            {
                Console.WriteLine("Activity is null");
            }

            activity?.SetTag("foo", 1);
            activity?.SetTag("bar", "Hello, World!");
            activity?.SetTag("baz", new int[] { 1, 2, 3 });
            activity?.SetStatus(ActivityStatusCode.Ok);
        }

        serviceProvider.Dispose();
    }
}
