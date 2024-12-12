using System.Diagnostics;

namespace BackpackTracker
{
    public partial class App : Application
    {
        public App()
        {
            try
            {
                InitializeComponent();
                MainPage = new AppShell();
            }
            catch (Exception ex)
            { 
                Debug.WriteLine($"Error during InitializeComponent: {ex.Message}");
                throw new Exception("test exception");
            }
        }
    }
}
