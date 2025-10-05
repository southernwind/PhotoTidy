using Windows.System;
using PhotoTidy.Models;

namespace PhotoTidy.Services;

/// <summary>
///     タグショートカットの登録と適用を司るサービス。
/// </summary>
[AddSingleton]
public sealed class TagShortcutService(TagList tagList) {
	/// <summary>
	///     指定キーのタグを対象画像へ適用します。
	/// </summary>
	public void Apply(VirtualKey key, ImageItem? item) {
		if (item == null) {
			return;
		}

		var tag = tagList.Tags.FirstOrDefault(x => x.Key.Value == key);
		if (tag == null) {
			return;
		}

		item.Tag.Value = tag;
	}
}