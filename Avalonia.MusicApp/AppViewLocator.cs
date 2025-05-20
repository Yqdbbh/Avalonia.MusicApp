using System;
using Avalonia.MusicApp.ViewModels;
using Avalonia.MusicApp.Views;
using ReactiveUI;

namespace Avalonia.MusicApp
{
    public class AppViewLocator:IViewLocator
    {
        public IViewFor ResolveView<T>(T viewModel, string contract = null) => viewModel switch
        {
            FirstViewModel context => new FirstView { DataContext = context },
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }
}
