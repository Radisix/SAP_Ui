﻿using System;
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

namespace SAP_Ui
{
    /// <summary>
    /// Interaction logic for ShowScore.xaml
    /// </summary>
    public partial class ShowScore : UserControl
    {
        public ShowScore(string studentCode)
        {
            InitializeComponent();
            StudentCode = studentCode;
        }
        public string StudentCode{ get; set; }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var result = Bll.Student.SelectScore(StudentCode);
            if (!result.Success)
            {
                MessageBox.Show(result.Message);
            }
            else
            {
                if (result.Data < 0)
                {
                    result.Data = 0;
                }
                else if (result.Data > 20)
                {
                    result.Data = 20;
                }
                Score_TxtBlock.Text = result.Data.ToString();
            }
        }
    }
}
