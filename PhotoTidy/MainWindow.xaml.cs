using Microsoft.UI.Xaml.Input;
using PhotoTidy.ViewModels;
using Windows.System;
using Microsoft.UI.Xaml;
using CommunityToolkit.Mvvm.DependencyInjection;
using PhotoTidy.Views;

namespace PhotoTidy {
	/// <summary>
	///     画像一覧表示用のメインウィンドウを表します。
	/// </summary>
	[AddSingleton]
	public sealed partial class MainWindow {
		/// <summary>
		///     このウィンドウに関連付けられたビュー モデルを取得します。
		/// </summary>
		public MainViewModel ViewModel {
			get;
		}

		/// <summary>
		///     <see cref="MainWindow" /> クラスの新しいインスタンスを初期化します。
		/// </summary>
		public MainWindow(MainViewModel mainViewModel) {
			this.InitializeComponent();
			this.ViewModel = mainViewModel;
			this.RootGrid.DataContext = this.ViewModel;
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
			if (sender is not FrameworkElement { DataContext: ImageItemViewModel vm }) {
				return;
			}

			var previewWindow = new ImagePreviewWindow(vm);
			previewWindow.Activate();
		}
	}
}