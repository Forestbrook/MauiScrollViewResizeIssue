namespace MauiUnhandledExceptionTest;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
    }

    private void OnShowExceptionClicked(object sender, EventArgs e)
    {
        _resultLabel.Text = "Reading last thrown exception...";
        var path = Path.Combine(FileSystem.AppDataDirectory, "Error.txt");
        if (File.Exists(path))
        {
            var message = File.ReadAllText(path);
            _resultLabel.Text = $"Last thrown exception: {message}";
            File.Delete(path);
        }
        else
        {
            _resultLabel.Text = "No exception data found.";
        }
    }

    private void OnThrowExceptionClicked(object sender, EventArgs e)
    {
        _resultLabel.Text = "Throwing exception...";
        throw new Exception($"Test exception {Guid.NewGuid()}");
    }

    private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "Error.txt");
        File.WriteAllText(path, $"Unhandled exception: {(e.ExceptionObject as Exception)}");
    }
}