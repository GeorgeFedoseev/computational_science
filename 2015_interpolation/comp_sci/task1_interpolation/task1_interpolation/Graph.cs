using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace task1_interpolation
{
    class Graph:  System.Windows.Forms.Form
    {
        private ZedGraphControl zedGraph;

        
        private System.ComponentModel.Container components = null;

        public Graph()
		{
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(680, 414);
            this.Controls.Add(this.zedGraph);
            this.Name = "Graph";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		

		private void Form1_Load( object sender, System.EventArgs e )
		{
			zedGraph.IsShowPointValues = true;
			zedGraph.GraphPane.Title = "Test Case for C#";
			double[] x = new double[100];
			double[] y = new double[100];
			int	i;
			for ( i=0; i<100; i++ )
			{
				x[i] = (double) i / 100.0 * Math.PI * 2.0;
				y[i] = Math.Sin( x[i] );
			}
			zedGraph.GraphPane.AddCurve( "Sine Wave", x, y, Color.Red, SymbolType.Square );
			zedGraph.AxisChange();
			zedGraph.Invalidate();
		}

        private void zedGraph_Load(object sender, EventArgs e)
        {

        }


        
    }
}
