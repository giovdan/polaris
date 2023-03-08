// ***********************************************************************
// Assembly         : Mitrol.Framework.Domain.Core
// Author           : giovanni.dantonio
// Created          : 09-17-2021
//
// Last Modified By : giovanni.dantonio
// Last Modified On : 10-08-2021
// ***********************************************************************
// <copyright file="ImportExportService.cs" company="Mitrol S.r.l.">
//     Copyright © 2021
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.SignalR;
    using Mitrol.Framework.Domain.SignalR.Gateway;

    /// <summary>
    /// Class InternalBaseService.
    /// Implements the <see cref="Mitrol.Framework.Domain.Core.Models.Microservices.ServiceBase" />
    /// </summary>
    /// <seealso cref="Mitrol.Framework.Domain.Core.Models.Microservices.ServiceBase" />
    public class InternalBaseService : ServiceBase
    {
        protected IEventHubClient ProgressEventHubClient => ServiceFactory.GetService<IEventHubClient>();
        protected IEventLogHubClient EventLogHubClient => ServiceFactory.GetService<IEventLogHubClient>();

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalBaseService"/> class.
        /// </summary>
        /// <param name="serviceFactory">The service factory.</param>
        public InternalBaseService(IServiceFactory serviceFactory)
            : base(serviceFactory)
        {
        }
    }
}
