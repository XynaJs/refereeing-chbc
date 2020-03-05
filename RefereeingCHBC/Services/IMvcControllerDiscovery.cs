using RefereeingCHBC.Models;
using System.Collections.Generic;

namespace RefereeingCHBC.Services
{
    public interface IMvcControllerDiscovery
    {
        IEnumerable<MvcControllerInfo> GetControllers();
    }
}