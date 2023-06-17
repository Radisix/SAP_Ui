﻿using DataAccessLayer;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FormComponent
{
    /// <summary>
    /// Interaction logic for TakhirForm.xaml
    /// </summary>
    public partial class TakhirForm : UserControl
    {
        public TakhirForm()
        {
            InitializeComponent();
        }
        public List<GetTakhir> takhirs = new List<GetTakhir>();
        public TakhirForm(List<GetTakhir> Takhirs)
        {
            InitializeComponent();
            takhirs = Takhirs;
        }
        public string Date { get; set; }
        public string DateSelect { get; set; }
        public string FillTakhir(List<GetTakhir> takhirs)
        {
            WrapPanel panel = new WrapPanel();
            panel.Orientation = Orientation.Horizontal;
            panel.Width = TakhirsPanelShow_WrpPnl.Width;
            panel.Height = TakhirsPanelShow_WrpPnl.Height;
            foreach (GetTakhir takhir in takhirs)
            {
                panel.FlowDirection = FlowDirection.LeftToRight;
                panel.Children.Add(new TakhirComponent() { takhir = takhir });
            }

            TakhirsPanelShow_WrpPnl.Children.Add(panel);
            return takhirs.Count.ToString();

        }
        private void Takhir_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnGheybat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (takhirs != null)
            {
                TakhirNumber_Lbl.Content = FillTakhir(takhirs) + "تاخیر ";
            }
        }

        private void ChoozeDateSubmit_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Do something with the selected date
        }
    }
}
