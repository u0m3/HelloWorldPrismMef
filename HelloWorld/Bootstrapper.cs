using HelloWorld.Views;
using Prism.Mef;
using Prism.Modularity;
using Prism.Mvvm;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.Reflection;
using System.Windows;

namespace HelloWorld
{
    public class Bootstrapper : MefBootstrapper
    {
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new DirectoryCatalog(@".\Modules"));
        }

        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (MainWindow)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        /// <summary>
        /// This is required so for MEF to be able to propperly load and permit Prism to wire the Views to ViewModels and to Regions.
        /// <see cref="http://stackoverflow.com/questions/33043978/prismviewmodellocator-autowireviewmodel-true-will-not-work-for-not-referenced" />
        /// </summary>
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var viewName = viewType.FullName;
                viewName = viewName.Replace(".Views.", ".ViewModels.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
                var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}{1}", viewName, suffix);

                var assembly = viewType.GetTypeInfo().Assembly;
                var type = assembly.GetType(viewModelName, true);

                return type;
            });
        }
    }
}
