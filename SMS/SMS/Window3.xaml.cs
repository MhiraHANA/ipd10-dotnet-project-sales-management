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
using LiveCharts;
using LiveCharts.Wpf;
using System.Data.SqlClient;
using SMS.Model;

namespace SMS
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        List<KeyValuePair<String, long>> Power = new List<KeyValuePair<String, long>>();
        Database DB = new Database();
        public Window3()
        {
            InitializeComponent();
          
           PointLabel = chartPoint =>
             string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);


            showColumnChart();
          

        }
        public Func<ChartPoint, string> PointLabel { get; set; }
        private void showColumnChart()
        {
          
             int TotalQuantity = 0;
            List<Orders> orders = new List<Orders>();
           
           string sql = "SELECT TOP(5) O.ProductID, SUM(O.Quantity) as TotalQuantity, P.ProductName FROM Products as P INNER JOIN  Orders as O On P.ProductID= O.ProductID GROUP BY O.ProductID, P.ProductName  ORDER BY SUM(O.Quantity) DESC; ";
          SqlCommand Command = new SqlCommand(sql, DB.conn);
          List<Products> list = new List<Products>();
          using (var reader = Command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var p = new Products();
                    p.ProductID = Convert.ToInt32(reader["ProductID"].ToString());
                    p.ProductName = reader["ProductName"].ToString();
               
                   // p.TotalQuantity = Int32.Parse(reader["TotalQuantity"].ToString());
                    list.Add(p);
                }

            }

            foreach (var p in list)
            {
                Power.Add(new KeyValuePair<String, long>(p.ProductName, p.TotalQuantity));
       
            }
            //Setting data for line chart
            lineChartProduct.DataContext = Power;
        }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
