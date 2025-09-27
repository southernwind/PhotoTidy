using System.Collections.ObjectModel;
using System.IO;
using PhotoTidy.Models;

namespace PhotoTidy.ViewModels;

/// <summary>
///     画像の読み込みと UI 状態を調整するビュー モデルです。
/// </summary>
[AddSingleton]
public sealed class MainViewModel {
	/// <summary>
	///     <see cref="MainViewModel" /> クラスの新しいインスタンスを初期化します。
	/// </summary>
	/// <param name="imageList">イメージリストモデル</param>
	public MainViewModel(ImageList imageList) {
		this.Images = imageList.Images.ToNotifyCollectionChanged(x => new ImageItemViewModel(x));
		this.FolderPath = imageList.FolderPath.ToBindableReactiveProperty();
		this.Status = imageList.Status.ToBindableReactiveProperty();
		this.IsBusy = imageList.IsBusy.ToBindableReactiveProperty();
		this.BrowseCommand.Subscribe(async _ => await imageList.BrowseAsync());
		this.LoadCommand = this.IsBusy.CombineLatest(this.FolderPath, (isBusy, folderPath) => (isBusy, folderPath)).Select(x => !x.isBusy && Directory.Exists(x.folderPath ?? string.Empty)).ToReactiveCommand();
		this.LoadCommand.Subscribe(_ => imageList.Load());
	}

	/// <summary>
	///     表示対象の画像アイテム集合を取得します。
	/// </summary>
	public NotifyCollectionChangedSynchronizedViewList<ImageItemViewModel> Images {
		get;
	}

	/// <summary>
	///     選択中のフォルダパスを取得します。
	/// </summary>
	public BindableReactiveProperty<string?> FolderPath {
		get;
	}

	/// <summary>
	///     ステータス文字列を取得します。
	/// </summary>
	public BindableReactiveProperty<string?> Status {
		get;
	}

	/// <summary>
	///     読み込み処理中かどうかを示す値を取得します。
	/// </summary>
	public BindableReactiveProperty<bool> IsBusy {
		get;
	}

	/// <summary>
	///     フォルダ参照ダイアログを開くコマンドを取得します。
	/// </summary>
	public ReactiveCommand BrowseCommand {
		get;
	} = new();

	/// <summary>
	///     画像読み込みコマンドを取得します。
	/// </summary>
	public ReactiveCommand LoadCommand {
		get;
	}
}