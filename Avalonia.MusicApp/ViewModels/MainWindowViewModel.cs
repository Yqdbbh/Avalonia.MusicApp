using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using System.Linq;
using System.Reactive;
using Avalonia.MusicApp.Models;
using System.Reactive.Concurrency;

namespace Avalonia.MusicApp.ViewModels;

public class MainWindowViewModel : ReactiveObject,IScreen  // ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static
    
    public MainWindowViewModel()
    {
        BuyMusicCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            // 点击按钮后触发
            // Console.WriteLine("Buy music");
            var store = new MusicStoreViewModel();
            var result = await ShowDialog.Handle(store);
            if (result != null)
            {
                Albums.Add(result);
                await result.SaveToDiskAsync();
            }
        });
        ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();
        RxApp.MainThreadScheduler.Schedule(LoadAlbums);
        GoNext = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new FirstViewModel(this))
        );
    }
    public ICommand BuyMusicCommand { get; }
    public Interaction<MusicStoreViewModel, AlbumViewModel?> ShowDialog { get; }

    public ObservableCollection<AlbumViewModel> Albums { get; } = [];

    private async void LoadAlbums()
    {
        var albums = (await Album.LoadCachedAsync()).Select(x=>new AlbumViewModel(x));
        foreach (var album in albums)
        {
            Albums.Add(album);
        }

        foreach (var album in Albums.ToList())
        {
            await album.LoadCoverAsync();
        }
    }

    public RoutingState Router { get; } = new RoutingState();
    
    // 导航用户到第一个视图模型的命令。
    public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

    // 导航用户返回的命令。
    public ReactiveCommand<Unit, IRoutableViewModel> GoBack => Router.NavigateBack;
}