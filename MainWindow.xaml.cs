using System;
using System.Linq;
using System.Windows;
using System.Data.Entity; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerStoreAppWPF
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [Column("customer_id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("email")]
        public string Email { get; set; }
    }

    public class StoreContext : DbContext
    {

        public StoreContext() : base(@"Server=sql.bsite.net\MSSQL2016;Database=osipov_Zhukov;User Id=osipov_Zhukov;Password=12345;")
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                using (var db = new StoreContext())
                {
                    var list = db.Customers.ToList();
                    DataGridCustomers.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new StoreContext())
                {
                    var newCust = new Customer
                    {
                        FirstName = "WPF_User",
                        LastName = "ORM_Test",
                        Email = "test@wpf.ru"
                    };
                    db.Customers.Add(newCust);
                    db.SaveChanges();
                    MessageBox.Show("Успешно добавлено!");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}