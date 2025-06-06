using LagrangePoly.Model;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot;
using System.Collections.ObjectModel;
using System.Linq;
using OxyPlot.Axes;

namespace LagrangePoly.ViewModel
{
    public class ViewModel
    {
        public ObservableCollection<PointPairs> Points { get; } = new ObservableCollection<PointPairs>();

        public void AddPoint(double x, double y)
        {
            foreach (var p in Points)
            {
                if (p.X_point == x) return;
            }
            Points.Add(new PointPairs(Points.Count, x, y));
        }

        public double CalculateLagrange(double x)
        {
            double result = 0;
            for (int i = 0; i < Points.Count; i++)
            {
                double term = Points[i].y_point;
                for (int j = 0; j < Points.Count; j++)
                {
                    if (j != i)
                    {
                        term *= (x - Points[j].X_point) / (Points[i].X_point - Points[j].X_point);
                    }
                }
                result += term;
            }
            return result;
        }


        public PlotModel? PlotGraph()
        {
            var model = new PlotModel { Title = "Lagrange Polynomial" };
            model.Legends.Add(new Legend());

            // Добавляем оси с автоматическим масштабированием
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });

            var pointsSeries = new LineSeries
            {
                Title = "Points",
                MarkerType = MarkerType.Circle,
                MarkerSize = 5,
                MarkerFill = OxyColors.Red,
                LineStyle = LineStyle.None
            };

            foreach (var point in Points)
            {
                pointsSeries.Points.Add(new DataPoint(point.X_point, point.y_point));
            }
            model.Series.Add(pointsSeries);

            if (Points.Count > 1)
            {
                var polySeries = new LineSeries
                {
                    Title = "Lagrange Polynomial",
                    Color = OxyColors.Blue
                };

                double minX = Points.Min(p => p.X_point);
                double maxX = Points.Max(p => p.X_point);

                // Добавляем небольшие отступы по краям
                double padding = (maxX - minX) * 0.1;
                minX -= padding;
                maxX += padding;

                double step = (maxX - minX) / 100;
                for (double x = minX; x <= maxX; x += step)
                {
                    polySeries.Points.Add(new DataPoint(x, CalculateLagrange(x)));
                }
                model.Series.Add(polySeries);
            }

            return model;
        }

    }
}