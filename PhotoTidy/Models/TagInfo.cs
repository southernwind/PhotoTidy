using Windows.System;

namespace PhotoTidy.Models;

/// <summary>
///     画像に付与するタグ情報
/// </summary>
public sealed class TagInfo {
	public BindableReactiveProperty<VirtualKey?> Key {
		get;
	} = new();

	public BindableReactiveProperty<string?> Name {
		get;
	} = new();

	public BindableReactiveProperty<string?> TargetFolder {
		get;
	} = new();

	public override string ToString() {
		return this.Name.Value ?? "";
	}
}