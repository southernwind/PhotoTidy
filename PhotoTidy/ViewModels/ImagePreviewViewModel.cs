using Windows.System;
using PhotoTidy.Models;
using PhotoTidy.Services;

namespace PhotoTidy.ViewModels;

[AddTransient]
public class ImagePreviewViewModel {
	private readonly ImageList _imageList;

	public ImagePreviewViewModel(ImageList imageList, TagShortcutService tagShortcutService) {
		this._imageList = imageList;
		this.SelectedIndex = imageList.SelectedIndex.ToBindableReactiveProperty();
		this.SelectedImage = imageList.SelectedImage.Select(x => x == null ? null : new ImageItemViewModel(x)).ToBindableReactiveProperty();
		this.ShortcutKeyCommand.Subscribe(x => {
			switch (x) {
				case VirtualKey.Right:
					this._imageList.MoveNext();
					return;
				case VirtualKey.Left:
					this._imageList.MovePrevious();
					return;
				default:
					if (this.SelectedImage.Value == null) {
						return;
					}

					tagShortcutService.Apply(x, this.SelectedImage.Value.ImageItem);
					break;
			}
		});
	}

	/// <summary>
	///     現在選択されている画像アイテムのインデックスを取得または設定します。
	/// </summary>
	public BindableReactiveProperty<int> SelectedIndex {
		get;
	}

	/// <summary>
	///     現在選択されている画像アイテムを取得します。
	/// </summary>
	public IReadOnlyBindableReactiveProperty<ImageItemViewModel?> SelectedImage {
		get;
	}

	public ReactiveCommand<VirtualKey> ShortcutKeyCommand {
		get;
	} = new();
}