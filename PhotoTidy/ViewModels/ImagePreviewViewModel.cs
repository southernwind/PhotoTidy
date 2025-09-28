using PhotoTidy.Models;

namespace PhotoTidy.ViewModels;

[AddTransient]
public class ImagePreviewViewModel(ImageList imageList) {
	/// <summary>
	///     現在選択されている画像アイテムのインデックスを取得または設定します。
	/// </summary>
	public BindableReactiveProperty<int> SelectedIndex {
		get;
	} = imageList.SelectedIndex.ToBindableReactiveProperty();

	/// <summary>
	///     現在選択されている画像アイテムを取得します。
	/// </summary>
	public IReadOnlyBindableReactiveProperty<ImageItemViewModel?> SelectedImage {
		get;
	} = imageList.SelectedImage.Select(x => x == null ? null : new ImageItemViewModel(x)).ToBindableReactiveProperty();

	/// <summary>
	///     一覧内の選択を次のアイテムに移動します。
	/// </summary>
	public void MoveNext() {
		imageList.MoveNext();
	}

	/// <summary>
	///     一覧内の選択を前のアイテムに移動します。
	/// </summary>
	public void MovePrevious() {
		imageList.MovePrevious();
	}
}