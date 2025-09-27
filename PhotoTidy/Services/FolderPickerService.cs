using System.Threading.Tasks;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace PhotoTidy.Services;

/// <summary>
///     Windows におけるフォルダ選択サービスの実装です。
/// </summary>
[AddTransient(typeof(IFolderPickerService))]
public sealed class FolderPickerService : IFolderPickerService {
	private nint _hwnd;

	/// <inheritdoc />
	public async Task<string?> PickFolderAsync() {
		this._hwnd = WindowNative.GetWindowHandle(App.MainWindow);
		var picker = new FolderPicker();
		InitializeWithWindow.Initialize(picker, this._hwnd);
		picker.FileTypeFilter.Add("*");
		var folder = await picker.PickSingleFolderAsync();
		return folder?.Path;
	}
}