using Windows.System;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using PhotoTidy.ViewModels;

namespace PhotoTidy.Views;

/// <summary>
///     単一画像の拡大プレビュー用ウィンドウです。
/// </summary>
[AddTransient]
public sealed partial class ImagePreviewWindow {
	private readonly MainViewModel _mainViewModel = Ioc.Default.GetRequiredService<MainViewModel>();

	/// <summary>
	///     <see cref="ImagePreviewWindow" /> の新しいインスタンスを初期化します。
	/// </summary>
	public ImagePreviewWindow(ImagePreviewViewModel viewModel) {
		this.ViewModel = viewModel;
		this.InitializeComponent();
		this.TrySetSize();
		this.RootGrid.Loaded += (_, _) => this.RootGrid.Focus(FocusState.Programmatic);
	}

	/// <summary>
	///     プレビュー対象の画像アイテム ViewModel を取得します。
	/// </summary>
	public ImagePreviewViewModel ViewModel {
		get;
	}

	private void TrySetSize() {
		this.AppWindow?.Resize(new(900, 700));
	}

	private void RootGrid_KeyDown(object sender, KeyRoutedEventArgs e) {
		if (e.Key is VirtualKey.Right) {
			this.ViewModel.MoveNext();
		} else if (e.Key is VirtualKey.Left) {
			this.ViewModel.MovePrevious();
		}
	}
}