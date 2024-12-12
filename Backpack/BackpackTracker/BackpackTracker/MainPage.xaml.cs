
using Microsoft.EntityFrameworkCore;
using BackpackTracker.Data.Contexts;
using BackpackTracker.Data.Entities;
namespace BackpackTracker
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly BackpackAppContext _dbContext;

        public MainPage(BackpackAppContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
        }

        private async void OnAddBackpackClicked(object sender, EventArgs e)
        {
            var backpack = new Backpack
            {
                BackpackId = 1,
                Name = "Osprey",
                Weight = 75.5,
                CapacityLiters = 45,
                Description = "Test"
            };

            // Add user to the database
            _dbContext.Backpacks.Add(backpack);
            await _dbContext.SaveChangesAsync();
        }

        private async void OnShowBackpacksClicked(object sender, EventArgs e)
        {
            var backpacks = await _dbContext.Backpacks.ToListAsync();
            foreach (var pack in backpacks)
            {
                Console.WriteLine($"Backpack: {pack.Name}, Capacity: {pack.CapacityLiters}");
            }
        }
    }

}
