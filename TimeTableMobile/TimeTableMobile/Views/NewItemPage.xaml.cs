using System;
using System.Collections.Generic;
using System.ComponentModel;
using TimeTableMobile.Models;
using TimeTableMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableMobile.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}