using System.ComponentModel;
using TimeTableMobile.ViewModels;
using Xamarin.Forms;

namespace TimeTableMobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}