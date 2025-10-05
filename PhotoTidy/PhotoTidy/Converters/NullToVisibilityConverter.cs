using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace PhotoTidy.Converters;

public sealed class NullToVisibilityConverter : IValueConverter {
	public object Convert(object value, Type targetType, object parameter, string language) {
		var invert = parameter as string == "Invert";
		var isNull = value is null;
		if (invert) {
			isNull = !isNull;
		}

		return isNull ? Visibility.Collapsed : Visibility.Visible;
	}

	public object ConvertBack(object value, Type targetType, object parameter, string language) {
		throw new NotSupportedException();
	}
}