using Infrastructure;
using ModuleA.Views;
using Prism.Logging;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;

namespace ModuleA
{
    [ModuleExport(typeof(ModuleAModule))]
    public class ModuleAModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly ILoggerFacade _logger;

        [ImportingConstructor]
        public ModuleAModule(IRegionManager regionManager, ILoggerFacade logger)
        {
            _regionManager = regionManager;
            _logger = logger;

            _logger.Log("ModuleAModule.ctor() [done]", Category.Debug, Priority.Low);
        }

        public void Initialize()
        {
            _logger.Log("ModuleAModule.Initialize() [start]", Category.Debug, Priority.Low);
            _regionManager.RegisterViewWithRegion(RegionNames.CenterRegion, typeof(ViewA));
            _logger.Log("ModuleAModule.Initialize() [end]", Category.Debug, Priority.Low);
        }
    }
}
