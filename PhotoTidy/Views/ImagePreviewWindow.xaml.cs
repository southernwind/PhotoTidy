using PhotoTidy.ViewModels;

namespace PhotoTidy.Views;

/// <summary>
///     単一画像の拡大プレビュー用ウィンドウです。
/// </summary>
public sealed partial class ImagePreviewWindow {
	/// <summary>
	///     プレビュー対象の画像アイテム ViewModel を取得します。
	/// </summary>
	public ImageItemViewModel ViewModel {
		get;
	}

	/// <summary>
	///     <see cref="ImagePreviewWindow"/> の新しいインスタンスを初期化します。
	/// </summary>
	public ImagePreviewWindow(ImageItemViewModel viewModel) {
		this.ViewModel = viewModel;
		this.InitializeComponent();
		this.TrySetSize();
	}

	private void TrySetSize() {
		this.AppWindow?.Resize(new(900, 700));
	}
}