using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Legends;
using OxyPlot.Axes;

namespace LagrangePoly
{
    public partial class MainWindow : Window
    {
        private ViewModel.ViewModel vm = new ViewModel.ViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void AddNewPoint(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = string.IsNullOrEmpty(X_point.Text) ? 0 : double.Parse(X_point.Text);
                double y = string.IsNullOrEmpty(Y_point.Text) ? 0 : double.Parse(Y_point.Text);
                vm.AddPoint(x, y);

                MyPlot.Model = vm.PlotGraph();
                ErrInfoBlock.Opacity = 0;
            }
            catch (FormatException)
            {
                ErrInfoBlock.Text = "Неверный формат ввода!";
                ErrInfoBlock.Opacity = 1;
            }
        }

        private void PlotView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && MyPlot.Model != null)
            {
                var position = e.GetPosition(MyPlot);

                var xAxis = MyPlot.Model.DefaultXAxis;
                var yAxis = MyPlot.Model.DefaultYAxis;

                double x = xAxis.InverseTransform(position.X);
                double y = yAxis.InverseTransform(position.Y);

                vm.AddPoint(x, y);
                MyPlot.Model = vm.PlotGraph();
            }
        }

        private void CheckPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(char.IsDigit(e.Text, 0) || (e.Text == ",") || (e.Text == "-")))
            {
                e.Handled = true;
            }
        }

        private void ClearPoints(object sender, RoutedEventArgs e)
        {
            vm.Points.Clear();
            MyPlot.Model = vm.PlotGraph();
        }
    }
}