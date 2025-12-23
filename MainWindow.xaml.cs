using System;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace FlowerDeliveryWPF
{

    public class Flower
    {
        public int FlowerId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
    }

    public class FlowerContext : DbContext
    {
        public DbSet<Flower> Flowers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=sql.bsite.net\MSSQL2016;Database=osipov_;User Id=osipov_;Password=osipow188;TrustServerCertificate=True;");
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new FlowerContext())
                {
                   
                    db.Database.EnsureCreated();

                  
                    if (!db.Flowers.Any())
                    {
                        db.Flowers.Add(new Flower { Name = "Розы красные", Price = 1500, InStock = 25 });
                        db.SaveChanges();
                    }

                
                    DataDisplayGrid.ItemsSource = db.Flowers.ToList();
                    MessageBox.Show("Данные успешно получены через ORM!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message);
            }
        }
    }
}