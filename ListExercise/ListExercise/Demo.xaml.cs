using ListExercise.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListExercise
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Demo : ContentPage
    {
         static ObservableCollection<Search> searchs = new ObservableCollection<Search>
            {
                new Search{Id = 1,Location = "So 1 Thanh Xuan,Ha Noi",CheckIn = DateTime.Today,CheckOut = DateTime.Now},
                new Search{Id = 2,Location = "52 Lac Long Quan,Ha Noi",CheckIn = DateTime.Today,CheckOut = DateTime.Now},
                new Search{Id = 3, Location = "So 53 Trieu Khuc,Ha Noi", CheckIn = DateTime.Today, CheckOut = DateTime.Now}

            };

       
        IEnumerable<Search> GetSearches (string searchText = null)
        {
            
            if (String.IsNullOrWhiteSpace(searchText))
                return searchs;
            return searchs.Where(c => c.Location.StartsWith(searchText)).ToList();
        }
        
        public Demo()
        {
            InitializeComponent();
            listView.ItemsSource = GetSearches();
            
            
          
        }
        
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listView.ItemsSource = GetSearches(e.NewTextValue);
        }

      

        private void Delete_Clicked(object sender, EventArgs e)
        {
            var search = (sender as MenuItem).CommandParameter as Search;
            searchs.Remove(search);
            listView.ItemsSource = GetSearches();
            
        }
        private void listView_Refreshing(object sender, EventArgs e)
        {
            listView.ItemsSource = GetSearches();
            listView.IsRefreshing = false;
            listView.EndRefresh();

        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var search = e.SelectedItem as Search;
            DisplayAlert("Selected", search.Location, "OK");
        }
    }
}