using CanvasDemo.Core;
using CanvasDemo.Core.EventHandlerBindings;
using CanvasDemo.Core.Settings;
using CanvasDemo.Core.StateManager;
using CanvasDemo.Core.StyleManager;
using CanvasDemo.Handler;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CanvasDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICommonSetting, CommonSetting>();
            services.AddSingleton<IImageBoxEventHandlerManager, ImageBoxEventHandlerManager>();
            services.AddSingleton<IStyleManager, StyleManager>();
            services.AddSingleton<IUndoRedoManager, UndoRedoManager>();
            services.AddSingleton<ISelectionManager, SelectionManager>();
            services.AddSingleton<CanvasMoveEventHandler>();
            services.AddSingleton<CanvasScaleEventHandler>();
            services.AddSingleton<CanvasSelectionEventHandler>();
            services.AddTransient<ImageBoxClickSelectionEventHandler>();
            services.AddSingleton<SelectorMoveEventHandler>();
            services.AddSingleton<SelectorRoateEventHandler>();
            services.AddSingleton<SelectorScaleEventHandler>();

            services.AddSingleton<IBindingEventManager, BindingEventManager>();
            services.AddSingleton<ICanvasManager, CanvasManager>();
            services.AddSingleton<MainWindow>();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            var commonSetting = _serviceProvider.GetRequiredService<ICommonSetting>();
            var bindingEventManager = _serviceProvider.GetRequiredService<IBindingEventManager>();
            commonSetting.Inititial(mainWindow.testCanvas);
            bindingEventManager.Binding();
            mainWindow?.Show();
            // base.OnStartup(e);
        }

    }

}