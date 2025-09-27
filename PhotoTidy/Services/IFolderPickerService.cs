using System.Threading.Tasks;

namespace PhotoTidy.Services;

/// <summary>
///     フォルダ選択機能を抽象化するインターフェイスです。
/// </summary>
public interface IFolderPickerService {
	/// <summary>
	///     フォルダ選択ダイアログを表示します。
	/// </summary>
	/// <returns>選択されたフォルダの絶対パス。キャンセル時は null。</returns>
	public Task<string?> PickFolderAsync();
}