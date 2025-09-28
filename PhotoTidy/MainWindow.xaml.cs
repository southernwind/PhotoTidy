using Windows.System;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Input;
using PhotoTidy.ViewModels;
using PhotoTidy.Views;

namespace PhotoTidy {
	/// <summary>
	///     画像一覧表示用のメインウィンドウを表します。
	/// </summary>
	[AddSingleton]
	public sealed partial class MainWindow {
		/// <summary>
		///     <see cref="MainWindow" /> クラスの新しいインスタンスを初期化します。
		/// </summary>
		public MainWindow(MainViewModel mainViewModel) {
			this.InitializeComponent();
			this.ViewModel = mainViewModel;
			this.RootGrid.DataContext = this.ViewModel;
		}

		/// <summary>
		///     このウィンドウに関連付けられたビュー モデルを取得します。
		/// </summary>
		public MainViewModel ViewModel {
			get;
		}

		/// <summary>
		///     フォルダ入力テキストボックスで Enter キーが押下されたときに画像読み込みコマンドを実行します。
		/// </summary>
		/// <param name="_">イベント送信元。</param>
		/// <param name="e">キーイベントデータ。</param>
		private void FolderTextBox_KeyDown(object _, KeyRoutedEventArgs e) {
			if (e.Key == VirtualKey.Enter && this.ViewModel.LoadCommand.CanExecute()) {
				this.ViewModel.LoadCommand.Execute(Unit.Default);
			}
		}

		/// <summary>
		///     画像アイテムのダブルタップで拡大プレビューウィンドウを開きます。
		/// </summary>
		private void ImageItem_DoubleTapped(object sender, DoubleTappedRoutedEventArgs _) {
			var previewWindow = Ioc.Default.GetRequiredService<ImagePreviewWindow>();
			previewWindow.Activate();
		}
	}
}