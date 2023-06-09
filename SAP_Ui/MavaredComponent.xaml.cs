﻿using DataAccessLayer;
using SAP_Ui;
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
    /// Interaction logic for MavaredComponent.xaml
    /// </summary>
    public partial class MavaredComponent : UserControl
    {
        public MavaredComponent(Mavared_Tbl mored)
        {
            InitializeComponent();
            Mored = mored;
        }

        public Mavared_Tbl Mored { get; set; }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MoredName_Txt.Text = Mored.MoredTitle;
            MoredType_CmBox.Text = Mored.MoredType;
            MoredScore_Txt.Text = Mored.MoredScore.ToString();
        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {
            var result = Bll.Mored.Delete(Mored.Id);
            if (!result.Success)
            {
                MessageBox.Show(result.Message);
            }
            else
            {
                Forms.homePage.ShowMavared();
            }
        }

        private void MoredScore_Txt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MoredName_Txt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void MoredType_CmBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            double lastScore;
            if (double.TryParse(MoredScore_Txt.Text, out double score))
            {
              lastScore = Mored.MoredScore;
                Mored.MoredScore = score;
                var reuslt = Bll.Mored.Update(Mored, lastScore);
                if (!reuslt.Success)
                {
                    MessageBox.Show("مشکلی پیش امد");
                }
            }
            else
            {
                MessageBox.Show("نمره را درست وارد کنید");
            }
        }
    }
}
