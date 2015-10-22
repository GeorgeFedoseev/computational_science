using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace task1_interpolation
{

    class Graph:  System.Windows.Forms.Form
    {
        private ZedGraphControl zedGraph;

        private int width, height;

        private List<GraphData> graphsList;

        private string title;

        
        private System.ComponentModel.Container components = null;

        public Graph(string _title, List<GraphData> _graphsList, int _width = 600, int _height = 400)
		{            
            title = _title;
            graphsList = _graphsList;

            width = _width;
            height = _height;

            
            

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.zedGraph = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // zedGraph
            // 
            this.zedGraph.IsShowPointValues = false;
            this.zedGraph.Location = new System.Drawing.Point(0, 0);
            this.zedGraph.Name = "zedGraph";
            this.zedGraph.PointValueFormat = "G";
            this.zedGraph.Size = new System.Drawing.Size(680, 414);
            this.zedGraph.TabIndex = 0;
            this.zedGraph.Load += new System.EventHandler(this.zedGraph_Load);
            // 
            // Graph
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(680, 412);
            this.Controls.Add(this.zedGraph);
            this.MaximizeBox = false;
            this.Name = "Graph";
            this.Text = "GraphForm";
            this.Load += new System.EventHandler(this.GraphForm_Load);
            this.Resize += new System.EventHandler(this.GraphForm_Resize);
            this.ResumeLayout(false);

		}
		#endregion
		

		private void GraphForm_Load( object sender, System.EventArgs e )
		{
			zedGraph.IsShowPointValues = true;

           

         
            
			zedGraph.GraphPane.Title = title;

            this.TopLevelControl.Size = new Size(width, height);

            
			/*double[] x = new double[100];
			double[] y = new double[100];
			int	i;
			for ( i=0; i<100; i++ )
			{
				x[i] = (double) i / 100.0 * Math.PI * 2.0;
				y[i] = Math.Sin( x[i] );
			}*/

            foreach (GraphData graph in graphsList) {
                zedGraph.GraphPane.AddCurve(graph.getTitle(), graph.getData(), graph.getColor(), SymbolType.None);
            }

            UpdateGraph();
		}

        private void zedGraph_Load(object sender, EventArgs e)
        {

        }

        private void GraphForm_Resize(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;
            /*
            // Ensure the Form remains square (Height = Width).
            if (control.Size.Height != control.Size.Width)
            {
                control.Size = new Size(control.Size.Width, control.Size.Width);
            }*/

            width = control.Size.Width;
            height = control.Size.Height;

            UpdateGraph();            
        }


        private void UpdateGraph() {
            this.zedGraph.Size = new System.Drawing.Size(width - 15, height - 40);
            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }


        
    }
}
