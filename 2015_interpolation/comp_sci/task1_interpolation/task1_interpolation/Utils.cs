using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace task1_interpolation
{
    class GraphData
    {
        private string title;
        private Color color;
        private PointPairList pvData;


        public GraphData(string _title, PointPairList _pvData, Color _color)
        {
            title = _title;
            pvData = _pvData;
            color = _color;
        }

        public string getTitle() { return title; }
        public Color getColor() { return color; }
        public PointPairList getData() { return pvData; }
    }

    static class GraphGenerator { 
        public static GraphData generateGraphForFunc(string title, Func<double, double> _f, Interval interval, double step, Color color)
        {

            PointPairList ppList = new PointPairList();

            for (int i = 0; i < interval.length / step; i++) {
                double x = interval.from + (double)i * step;
                double y = _f(x);

                ppList.Add(x, y);
            }

            return new GraphData(title, ppList, color);
        }
    }
}
