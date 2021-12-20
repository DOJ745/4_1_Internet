//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using LB6_7.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.SqlClient;
using System.Data.Services.Common;
using System.Data.Services.Providers;
using System.ServiceModel;

namespace LB6_7
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [DataServiceKey("id")]
    public class MainWcfDataService : EntityFrameworkDataService<WSSDAEntities>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            // config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);

            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;

            //config.UseVerboseErrors = true;
        }
    }
}
