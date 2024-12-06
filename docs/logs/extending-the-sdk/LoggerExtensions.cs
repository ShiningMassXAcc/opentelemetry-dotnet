// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using OpenTelemetry;
using OpenTelemetry.Logs;

internal static class LoggerExtensions
{
    public static LoggerProviderBuilder AddMyExporter(this LoggerProviderBuilder options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        return options.AddProcessor(new BatchLogRecordExportProcessor(new MyExporter()));
    }
}
