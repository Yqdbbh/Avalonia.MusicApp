using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.MusicApp.Models;
using ReactiveUI;

namespace Avalonia.MusicApp.ViewModels;

public class MusicStoreViewModel:ViewModelBase
{
    public MusicStoreViewModel()
    {
        this.WhenAnyValue(x => x.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(500))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearch!);
        BuyMusicCommand = ReactiveCommand.Create(() =>
        {
            return SelectedAlbum;
        });
    }
    
    private string? _searchText;
    private bool _isSearching;
    private CancellationTokenSource? _cancellationTokenSource;
    
    public string? SearchText
    {
        get => _searchText;
        set=>this.RaiseAndSetIfChanged(ref this._searchText, value);
    }

    public bool IsSearching 
    { 
        get => _isSearching; 
        set=>this.RaiseAndSetIfChanged(ref _isSearching, value); 
    }
    
    private AlbumViewModel? _selectedAlbum;
    public ObservableCollection<AlbumViewModel> SearchResults { get; set; } = [];

    public AlbumViewModel? SelectedAlbum
    {
        get => _selectedAlbum;
        set => this.RaiseAndSetIfChanged(ref _selectedAlbum, value);
    }
    
    public ReactiveCommand<Unit,AlbumViewModel?> BuyMusicCommand { get; }

    private async void DoSearch(string s)
    {
        IsSearching = true;
        SearchResults.Clear();
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = _cancellationTokenSource.Token;
        if (!string.IsNullOrWhiteSpace(s))
        {
            var albums = await Album.Search(s);
            foreach (var album in albums)
            {
                var vm = new AlbumViewModel(album);
                SearchResults.Add(vm);
            }
        }

        if (!cancellationToken.IsCancellationRequested)
        {
            LoadCovers(cancellationToken);
        }
        IsSearching = false;
    }

    private async void LoadCovers(CancellationToken cancellationToken)
    {
        foreach (var album in SearchResults.ToList())
        {
            await album.LoadCoverAsync();
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
        }
    }
}