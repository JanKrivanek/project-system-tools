﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.ProjectSystem.Tools.BuildLogging.Model.RpcContracts;
using Microsoft.VisualStudio.ProjectSystem.Tools.Providers;

namespace Microsoft.VisualStudio.ProjectSystem.Tools.BuildLogging.Model.BackendAPI
{
    /// <summary>
    /// Implements IBuildLoggerService that interacts with Codespaces
    /// </summary>
    [Export(typeof(IBuildLoggerService))]
    internal sealed class BuildLoggerService : IBuildLoggerService
    {
        IBackendBuildTableDataSource _dataSource;

        [ImportingConstructor]
        public BuildLoggerService(IBackendBuildTableDataSource dataSource) {
            _dataSource = dataSource;
        }

        bool IBuildLoggerService.IsLogging()
        {
            return _dataSource.IsLogging;
        }

        bool IBuildLoggerService.SupportsRoslynLogging()
        {
            return _dataSource.SupportsRoslynLogging;
        }

        void IBuildLoggerService.Start(NotifyCallback notifyCallback)
        {
            _dataSource.Start(notifyCallback);
        }

        void IBuildLoggerService.Stop()
        {
            _dataSource.Stop();
        }

        void IBuildLoggerService.Clear()
        {
            _dataSource.Clear();
        }

        // TODO: Change how data is transfered later in an async / server client scenario
        string IBuildLoggerService.RetrieveLogForBuild(int buildID)
        {
            return _dataSource.RetrieveLogForBuild(buildID);
        }

        ImmutableList<IBuildSummary> IBuildLoggerService.RetrieveAllBuilds()
        {
            return _dataSource.RetrieveAllBuilds();
        }
    }
}
