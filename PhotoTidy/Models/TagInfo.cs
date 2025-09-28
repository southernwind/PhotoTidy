namespace PhotoTidy.Models;

/// <summary>
///     画像に付与するタグ情報 (ショートカット設定で事前登録)。
/// </summary>
public sealed class TagInfo(string keyName, string name, string? targetFolder = null) {
	/// <summary>ショートカットとして表示するキーの表示名 ("1" など)。</summary>
	public string KeyName {
		get;
	} = keyName;

	/// <summary>タグ名 (UI 表示用)。</summary>
	public string Name {
		get;
	} = name;

	/// <summary>割り当て先フォルダ (オプション)。</summary>
	public string? TargetFolder {
		get;
	} = targetFolder;

	public override string ToString() {
		return this.Name;
	}
}