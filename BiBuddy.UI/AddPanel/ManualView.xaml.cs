﻿using Bibuddy.DataAccess.Abstract;
using Bibuddy.DataAccess.Concrete.Dapper;
using Bibuddy.DataAccess.Core.DI.Ninject;
using Bibuddy.DataAccess.Core.Utility;
using BiBuddy.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BiBuddy.UI.AddPanel
{
    /// <summary>
    /// Interaction logic for ManualView.xaml
    /// </summary>
    public partial class ManualView : Window
    {
        private readonly IManualDal _iManualService;
        public ManualView()
        {
            _iManualService = InstanceFactory.GetInstance<DapperManualDal>();
            InitializeComponent();
            DataContext = new manual();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            UIElement parent = App.Current.MainWindow;
            parent.IsEnabled = true;
        }
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().First();
            manual _manual = new manual()
            {
                bibtexkey = bibtexKey_Txt.Text,
                title = title_Txt.Text,
                year = year_Txt.Text != "" ? Convert.ToInt32(year_Txt.Text) : null as int?,
                month = month_Txt.Text != "" ? Convert.ToInt32(month_Txt.Text) : null as int?,
                author = author_Txt.Text,
                note = note_Txt.Text,
                address=address_Txt.Text,
                edition=edition_Txt.Text != "" ? Convert.ToInt32(edition_Txt.Text): null as int?,
                organization=organization_Txt.Text,
                entrytype = GetEntryType.GetValueByEnum(GetEntryType.EntryType.Article)
            };


            bool result = AllFieldEmpty();
            if (!result)
            {
                _iManualService.Add(_manual);
                SetFieldClear();
                UIElement parent = App.Current.MainWindow;
                parent.IsEnabled = true;
                main.DataGridMain.ItemsSource = _iManualService.GetAll();
                this.Close();
            }
            else
            {
                if (MessageBox.Show("Are you Sure to create a empty .bib", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                    return;
                }
                else
                {
                    _iManualService.Add(_manual);
                    SetFieldClear();
                    main.DataGridMain.ItemsSource = _iManualService.GetAll();
                    UIElement parent = App.Current.MainWindow;
                    parent.IsEnabled = true;
                    this.Close();
                    //do yes stuff
                }
            }
        }
        public void SetFieldClear()
        {
            bibtexKey_Txt.Text = "";
            title_Txt.Text = "";
            author_Txt.Text = "";
            edition_Txt.Text = "";
            month_Txt.Text = "";
            organization_Txt.Text = "";
            address_Txt.Text = "";
            note_Txt.Text = "";
            year_Txt.Text = "";
        }
        public bool AllFieldEmpty()
        {
            if (bibtexKey_Txt.Text == "" &&
            title_Txt.Text == "" &&
            author_Txt.Text == "" &&
            edition_Txt.Text == "" &&
            month_Txt.Text == "" &&
            address_Txt.Text == "" &&
            organization_Txt.Text == "" &&
            note_Txt.Text == "" &&
            year_Txt.Text == "" )
            {
                return true;
            }
            return false;
        }
        private void shomaretextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // xaml.cs code
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
