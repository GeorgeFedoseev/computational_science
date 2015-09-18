using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GoogleChartSharp;

namespace task1_interpolation
{
    class Graph
    {

        public List<float[]> datasets;
        int graphWidth, graphHeight;
        Action<string> imageReceivedCallback;
        string colorHex;


        public Graph(List<float[]> _datasets, Action<string> _callback)
        {
            init(_datasets, 400, 400, _callback);
        }

        public Graph(List<float[]> _datasets, int _graphWidth, int _graphHeight, Action<string> _callback)
        {
            init(_datasets, _graphWidth, _graphHeight, _callback);
           
        }

        void init(List<float[]> _datasets, int _graphWidth, int _graphHeight, Action<string> _callback, string _colorHex = "FF0000")
        {
            datasets = _datasets;
            graphWidth = _graphWidth;
            graphHeight = _graphHeight;

            imageReceivedCallback = _callback;

            colorHex = _colorHex;           

            LineChart lineChart = new LineChart(graphWidth, graphHeight, LineChartType.MultiDataSet);
            lineChart.SetData(datasets);

            // Optional : Provide a hex color for each pair of data sets
            lineChart.SetDatasetColors(new string[] { "FF0000", "008000", "0000ff", "ffa000" });


            ChartAxis topAxis = new ChartAxis(ChartAxisType.Left);
            //bottomAxis.SetRange(0, 500);
            topAxis.Color = colorHex;
            //bottomAxis.FontSize = 14;
            lineChart.AddAxis(topAxis);

            ChartAxis bottomAxis = new ChartAxis(ChartAxisType.Bottom);
            //bottomAxis.SetRange(0, 500);
            bottomAxis.Color = colorHex;
            //bottomAxis.FontSize = 14;
            lineChart.AddAxis(bottomAxis);

            // Optional : Provide a label for each pair of data sets
            //lineChart.SetLegend(new string[] { "First"});


            Async.SetTimeout(() => {
                string url = lineChart.GetUrl();
                imageReceivedCallback(lineChart.GetUrl());
            }, 0);
        }


        
    }
}
