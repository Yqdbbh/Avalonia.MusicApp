using System;
using ReactiveUI;

namespace Avalonia.MusicApp.ViewModels;

public class FirstViewModel:ReactiveObject,IRoutableViewModel
{
    public string? UrlPathSegment { get; }=Guid.NewGuid().ToString().Substring(0, 5);
    public IScreen HostScreen { get; }
    public FirstViewModel(IScreen screen) => HostScreen = screen;
}