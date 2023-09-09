using System.Windows;

namespace MVVMCommsDemo;

public static class MvvmBehaviors
{


    public static string GetLoadedMethodName(DependencyObject obj)
    {
        return (string)obj.GetValue(LoadedMethodNameProperty);
    }

    public static void SetLoadedMethodName(DependencyObject obj, string value)
    {
        obj.SetValue(LoadedMethodNameProperty, value);
    }

    // Using a DependencyProperty as the backing store for LoadedMethodName.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LoadedMethodNameProperty =
        DependencyProperty.RegisterAttached("LoadedMethodName", typeof(string), typeof(MvvmBehaviors),
            new PropertyMetadata(string.Empty, OnLoadedMethodNameChanged));

    private static void OnLoadedMethodNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        FrameworkElement element = (FrameworkElement)d;
        if (element == null) return;

        element.Loaded += (s, e2) =>
        {
            var viewModel = element.DataContext;
            if (viewModel == null) return;
            var methodName = e.NewValue.ToString() ?? string.Empty;
            var methodInfo = viewModel.GetType().GetMethod(methodName);
            methodInfo?.Invoke(viewModel, null);
        };
    }
}
