using Infrastructure;
using ModuleC.Views;
using Prism.Logging;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;

namespace ModuleC
{
    [ModuleExport(typeof(ModuleCModule))]
    public class ModuleCModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly ILoggerFacade _logger;

        [ImportingConstructor]
        public ModuleCModule(IRegionManager regionManager, ILoggerFacade logger)
        {
            _regionManager = regionManager;
            _logger = logger;
            _logger.Log("ModuleCModule.ctor() [done]", Category.Debug, Priority.Low);
        }

        public void Initialize()
        {
            _logger.Log("ModuleCModule.Initialize() [start]", Category.Debug, Priority.Low);
            _regionManager.RegisterViewWithRegion(RegionNames.LeftRegion, typeof(ViewC));
            _logger.Log("ModuleCModule.Initialize() [end]", Category.Debug, Priority.Low);
        }
    }
}
