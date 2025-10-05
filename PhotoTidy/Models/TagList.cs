namespace PhotoTidy.Models;

[AddSingleton]
public class TagList {
	public ObservableList<TagInfo> Tags {
		get;
	} = [new()];

	public void AddTagRow() {
		this.Tags.Add(new());
	}
}