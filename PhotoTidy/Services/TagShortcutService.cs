using System.Collections.Generic;
using Windows.System;
using PhotoTidy.Models;

namespace PhotoTidy.Services;

/// <summary>
///     タグショートカットの登録と適用を司るサービス。
/// </summary>
[AddSingleton]
public sealed class TagShortcutService {
	private readonly Dictionary<VirtualKey, TagInfo> _map = new();

	/// <summary>
	///     ショートカットを登録/上書きします。
	/// </summary>
	public void Register(VirtualKey key, string keyName, string tagName, string? targetFolder = null) {
		this._map[key] = new(keyName, tagName, targetFolder);
	}

	/// <summary>
	///     指定キーのタグを対象画像へ適用します。
	/// </summary>
	public void Apply(VirtualKey key, ImageItem? item) {
		if (item == null) {
			return;
		}

		if (!this._map.TryGetValue(key, out var tag)) {
			return;
		}

		item.Tag.Value = tag;
	}

	/// <summary>
	///     登録済みショートカットの列挙。
	/// </summary>
	public IEnumerable<(VirtualKey Key, TagInfo Tag)> Enumerate() {
		return this._map.Select(kv => (kv.Key, kv.Value));
	}
}