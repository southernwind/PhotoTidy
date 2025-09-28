using System.IO;
using System.Threading.Tasks;
using PhotoTidy.Services;

namespace PhotoTidy.Models;

[AddSingleton]
public class ImageList {
	private static readonly string[] ImageExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff", ".webp"];
	private readonly IFolderPickerService _folderPickerService;

	/// <summary>
	///     <see cref="ImageList" /> クラスの新しいインスタンスを初期化します。
	/// </summary>
	/// <param name="folderPickerService">フォルダ選択サービス。</param>
	public ImageList(IFolderPickerService folderPickerService) {
		this._folderPickerService = folderPickerService;
		// 選択画像の派生プロパティ
		this.SelectedImage = this.SelectedIndex
			.Select(i => i >= 0 && i < this.Images.Count ? this.Images[i] : null)
			.ToReadOnlyReactiveProperty();
	}

	/// <summary>
	///     表示対象の画像アイテム集合を取得します。
	/// </summary>
	public ObservableList<ImageItem> Images {
		get;
	} = [];

	/// <summary>
	///     選択中のフォルダパスを取得します。
	/// </summary>
	public ReactiveProperty<string?> FolderPath {
		get;
	} = new();

	/// <summary>
	///     ステータス文字列を取得します。
	/// </summary>
	public ReactiveProperty<string?> Status {
		get;
	} = new();

	/// <summary>
	///     読み込み処理中かどうかを示す値を取得します。
	/// </summary>
	public ReactiveProperty<bool> IsBusy {
		get;
	} = new();

	/// <summary>
	///     選択中の画像インデックス (-1 は未選択)。
	/// </summary>
	public ReactiveProperty<int> SelectedIndex {
		get;
	} = new(-1);

	/// <summary>
	///     選択中の画像。
	/// </summary>
	public ReadOnlyReactiveProperty<ImageItem?> SelectedImage {
		get;
	}

	public async Task BrowseAsync() {
		var path = await this._folderPickerService.PickFolderAsync();
		if (!string.IsNullOrEmpty(path)) {
			this.FolderPath.Value = path;
			this.Load();
		}
	}

	public void Load() {
		if (string.IsNullOrWhiteSpace(this.FolderPath.Value) || !Directory.Exists(this.FolderPath.Value)) {
			this.Status.Value = "無効なフォルダ";
			return;
		}

		try {
			this.IsBusy.Value = true;
			this.Status.Value = "読み込み中...";
			this.Images.Clear();

			var files = Directory.EnumerateFiles(this.FolderPath.Value, "*.*", SearchOption.TopDirectoryOnly)
				.Where(f => ImageExtensions.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
				.ToList();

			foreach (var file in files) {
				var item = new ImageItem(file);
				this.Images.Add(item);
				_ = item.EnsureThumbnailAsync();
			}

			this.Status.Value = $"{this.Images.Count} 件";
			this.SelectedIndex.Value = this.Images.Count > 0 ? 0 : -1;
		} catch (Exception ex) {
			this.Status.Value = "エラー: " + ex.Message;
		} finally {
			this.IsBusy.Value = false;
		}
	}

	public void MoveNext() {
		if (this.Images.Count == 0) {
			return;
		}

		var next = (this.SelectedIndex.Value + 1) % this.Images.Count;
		this.SelectedIndex.Value = next;
	}

	public void MovePrevious() {
		if (this.Images.Count == 0) {
			return;
		}

		var prev = (this.SelectedIndex.Value - 1 + this.Images.Count) % this.Images.Count;
		this.SelectedIndex.Value = prev;
	}
}