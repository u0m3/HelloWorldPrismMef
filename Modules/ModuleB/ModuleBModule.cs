using Infrastructure;
using ModuleB.Views;
using Prism.Logging;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;

namespace ModuleB
{
    [ModuleExport(typeof(ModuleBModule))]
    public class ModuleBModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly ILoggerFacade _logger;

        [ImportingConstructor]
        public ModuleBModule(IRegionManager regionManager, ILoggerFacade logger)
        {
            _regionManager = regionManager;
            _logger = logger;

            _logger.Log("ModuleBModule.ctor() [done]", Category.Debug, Priority.Low);
        }

        public void Initialize()
        {
            _logger.Log("ModuleBModule.Initialize() [start]", Category.Debug, Priority.Low);
            _regionManager.RegisterViewWithRegion(RegionNames.RightRegion, typeof(ViewB));
            _logger.Log("ModuleBModule.Initialize() [end]", Category.Debug, Priority.Low);
        }
    }
}
