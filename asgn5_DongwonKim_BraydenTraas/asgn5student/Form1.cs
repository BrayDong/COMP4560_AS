using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;

namespace asgn5v1
{
	/// <summary>
	/// Summary description for Transformer.
	/// </summary>
	public class Transformer : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
        //private bool GetNewData();

        // basic data for Transformer

        System.Windows.Forms.Timer xTimer;
        System.Windows.Forms.Timer yTimer;
        System.Windows.Forms.Timer zTimer;

		int numpts = 0;
		int numlines = 0;
		bool gooddata = false;		
		double[,] vertices;
		double[,] scrnpts;
		double[,] ctrans = new double[4,4];  //your main transformation matrix
        double[,] tempTnet = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { 0, 0, 0, 1}
                };

        private System.Windows.Forms.ImageList tbimages;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton transleftbtn;
		private System.Windows.Forms.ToolBarButton transrightbtn;
		private System.Windows.Forms.ToolBarButton transupbtn;
		private System.Windows.Forms.ToolBarButton transdownbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton scaleupbtn;
		private System.Windows.Forms.ToolBarButton scaledownbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton rotxby1btn;
		private System.Windows.Forms.ToolBarButton rotyby1btn;
		private System.Windows.Forms.ToolBarButton rotzby1btn;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ToolBarButton rotxbtn;
		private System.Windows.Forms.ToolBarButton rotybtn;
		private System.Windows.Forms.ToolBarButton rotzbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton shearrightbtn;
		private System.Windows.Forms.ToolBarButton shearleftbtn;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton resetbtn;
		private System.Windows.Forms.ToolBarButton exitbtn;
		int[,] lines;

        bool runFirst;

		public Transformer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			Text = "COMP 4560:  Assignment 5 (200830) (Shawn Kim / Brayden Traas)";
			ResizeRedraw = true;
			BackColor = Color.Black;
			MenuItem miNewDat = new MenuItem("New &Data...",
				new EventHandler(MenuNewDataOnClick));
			MenuItem miExit = new MenuItem("&Exit", 
				new EventHandler(MenuFileExitOnClick));
			MenuItem miDash = new MenuItem("-");
			MenuItem miFile = new MenuItem("&File",
				new MenuItem[] {miNewDat, miDash, miExit});
			MenuItem miAbout = new MenuItem("&About",
				new EventHandler(MenuAboutOnClick));
			Menu = new MainMenu(new MenuItem[] {miFile, miAbout});

			
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transformer));
            this.tbimages = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.transleftbtn = new System.Windows.Forms.ToolBarButton();
            this.transrightbtn = new System.Windows.Forms.ToolBarButton();
            this.transupbtn = new System.Windows.Forms.ToolBarButton();
            this.transdownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.scaleupbtn = new System.Windows.Forms.ToolBarButton();
            this.scaledownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.rotxby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotyby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotzby1btn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.rotxbtn = new System.Windows.Forms.ToolBarButton();
            this.rotybtn = new System.Windows.Forms.ToolBarButton();
            this.rotzbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.shearrightbtn = new System.Windows.Forms.ToolBarButton();
            this.shearleftbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resetbtn = new System.Windows.Forms.ToolBarButton();
            this.exitbtn = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // tbimages
            // 
            this.tbimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbimages.ImageStream")));
            this.tbimages.TransparentColor = System.Drawing.Color.Transparent;
            this.tbimages.Images.SetKeyName(0, "");
            this.tbimages.Images.SetKeyName(1, "");
            this.tbimages.Images.SetKeyName(2, "");
            this.tbimages.Images.SetKeyName(3, "");
            this.tbimages.Images.SetKeyName(4, "");
            this.tbimages.Images.SetKeyName(5, "");
            this.tbimages.Images.SetKeyName(6, "");
            this.tbimages.Images.SetKeyName(7, "");
            this.tbimages.Images.SetKeyName(8, "");
            this.tbimages.Images.SetKeyName(9, "");
            this.tbimages.Images.SetKeyName(10, "");
            this.tbimages.Images.SetKeyName(11, "");
            this.tbimages.Images.SetKeyName(12, "");
            this.tbimages.Images.SetKeyName(13, "");
            this.tbimages.Images.SetKeyName(14, "");
            this.tbimages.Images.SetKeyName(15, "");
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.transleftbtn,
            this.transrightbtn,
            this.transupbtn,
            this.transdownbtn,
            this.toolBarButton1,
            this.scaleupbtn,
            this.scaledownbtn,
            this.toolBarButton2,
            this.rotxby1btn,
            this.rotyby1btn,
            this.rotzby1btn,
            this.toolBarButton3,
            this.rotxbtn,
            this.rotybtn,
            this.rotzbtn,
            this.toolBarButton4,
            this.shearrightbtn,
            this.shearleftbtn,
            this.toolBarButton5,
            this.resetbtn,
            this.exitbtn});
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.tbimages;
            this.toolBar1.Location = new System.Drawing.Point(484, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(24, 306);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // transleftbtn
            // 
            this.transleftbtn.ImageIndex = 1;
            this.transleftbtn.Name = "transleftbtn";
            this.transleftbtn.ToolTipText = "translate left";
            // 
            // transrightbtn
            // 
            this.transrightbtn.ImageIndex = 0;
            this.transrightbtn.Name = "transrightbtn";
            this.transrightbtn.ToolTipText = "translate right";
            // 
            // transupbtn
            // 
            this.transupbtn.ImageIndex = 2;
            this.transupbtn.Name = "transupbtn";
            this.transupbtn.ToolTipText = "translate up";
            // 
            // transdownbtn
            // 
            this.transdownbtn.ImageIndex = 3;
            this.transdownbtn.Name = "transdownbtn";
            this.transdownbtn.ToolTipText = "translate down";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // scaleupbtn
            // 
            this.scaleupbtn.ImageIndex = 4;
            this.scaleupbtn.Name = "scaleupbtn";
            this.scaleupbtn.ToolTipText = "scale up";
            // 
            // scaledownbtn
            // 
            this.scaledownbtn.ImageIndex = 5;
            this.scaledownbtn.Name = "scaledownbtn";
            this.scaledownbtn.ToolTipText = "scale down";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxby1btn
            // 
            this.rotxby1btn.ImageIndex = 6;
            this.rotxby1btn.Name = "rotxby1btn";
            this.rotxby1btn.ToolTipText = "rotate about x by 1";
            // 
            // rotyby1btn
            // 
            this.rotyby1btn.ImageIndex = 7;
            this.rotyby1btn.Name = "rotyby1btn";
            this.rotyby1btn.ToolTipText = "rotate about y by 1";
            // 
            // rotzby1btn
            // 
            this.rotzby1btn.ImageIndex = 8;
            this.rotzby1btn.Name = "rotzby1btn";
            this.rotzby1btn.ToolTipText = "rotate about z by 1";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxbtn
            // 
            this.rotxbtn.ImageIndex = 9;
            this.rotxbtn.Name = "rotxbtn";
            this.rotxbtn.ToolTipText = "rotate about x continuously";
            // 
            // rotybtn
            // 
            this.rotybtn.ImageIndex = 10;
            this.rotybtn.Name = "rotybtn";
            this.rotybtn.ToolTipText = "rotate about y continuously";
            // 
            // rotzbtn
            // 
            this.rotzbtn.ImageIndex = 11;
            this.rotzbtn.Name = "rotzbtn";
            this.rotzbtn.ToolTipText = "rotate about z continuously";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // shearrightbtn
            // 
            this.shearrightbtn.ImageIndex = 12;
            this.shearrightbtn.Name = "shearrightbtn";
            this.shearrightbtn.ToolTipText = "shear right";
            // 
            // shearleftbtn
            // 
            this.shearleftbtn.ImageIndex = 13;
            this.shearleftbtn.Name = "shearleftbtn";
            this.shearleftbtn.ToolTipText = "shear left";
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // resetbtn
            // 
            this.resetbtn.ImageIndex = 14;
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.ToolTipText = "restore the initial image";
            // 
            // exitbtn
            // 
            this.exitbtn.ImageIndex = 15;
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.ToolTipText = "exit the program";
            // 
            // Transformer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(10, 24);
            this.ClientSize = new System.Drawing.Size(508, 306);
            this.Controls.Add(this.toolBar1);
            this.Name = "Transformer";
            this.Text = "7";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transformer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Transformer());
		}

		protected override void OnPaint(PaintEventArgs pea)
		{
			Graphics grfx = pea.Graphics;

            //Console.WriteLine($"height:{height} width:{width}");

            Pen pen = new Pen(Color.White, 3);
			double temp;
			int k;

            if (gooddata){


                // scrnpts = vertices*ctrans
                for (int i = 0; i < numpts; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        temp = 0.0d;

                        for (k = 0; k < 4; k++)
                            temp += vertices[i, k] * ctrans[k, j];

                        scrnpts[i, j] = temp;

                    }
                }

                for (int i = 0; i < numlines; i++)
                {
                    var x1 = (int)scrnpts[lines[i, 0], 0];
                    var y1 = (int)scrnpts[lines[i, 0], 1];
                    var x2 = (int)scrnpts[lines[i, 1], 0];
                    var y2 = (int)scrnpts[lines[i, 1], 1];
                    
                    grfx.DrawLine(pen, x1, y1, x2, y2);
                }

            } // end of gooddata block	
		} // end of OnPaint

		void MenuNewDataOnClick(object obj, EventArgs ea)
		{
			//MessageBox.Show("New Data item clicked.");
			gooddata = GetNewData();
            runFirst = true;
            RestoreInitialImage();			
		}

		void MenuFileExitOnClick(object obj, EventArgs ea)
		{
			Close();
		}

		void MenuAboutOnClick(object obj, EventArgs ea)
		{
			AboutDialogBox dlg = new AboutDialogBox();
			dlg.ShowDialog();
		}

		void RestoreInitialImage()
		{
			Invalidate();

            ClearTimers();
           
            SetData();
		} // end of RestoreInitialImage

        void ClearTimers()
        {
            if (xTimer != null)
            {
                xTimer.Stop();
                xTimer = null;
            }

            if (yTimer != null)
            {
                yTimer.Stop();
                yTimer = null;
            }

            if (zTimer != null)
            {
                zTimer.Stop();
                zTimer = null;
            }

        }

        bool GetNewData()
		{
			string strinputfile,text;
			ArrayList coorddata = new ArrayList();
			ArrayList linesdata = new ArrayList();
			OpenFileDialog opendlg = new OpenFileDialog();
			opendlg.Title = "Choose File with Coordinates of Vertices";
			if (opendlg.ShowDialog() == DialogResult.OK)
			{
				strinputfile=opendlg.FileName;				
				FileInfo coordfile = new FileInfo(strinputfile);
				StreamReader reader = coordfile.OpenText();
				do
				{
					text = reader.ReadLine();
					if (text != null) coorddata.Add(text);
				} while (text != null);
				reader.Close();
				DecodeCoords(coorddata);
			}
			else
			{
				MessageBox.Show("***Failed to Open Coordinates File***");
				return false;
			}
            
			opendlg.Title = "Choose File with Data Specifying Lines";
			if (opendlg.ShowDialog() == DialogResult.OK)
			{
				strinputfile=opendlg.FileName;
				FileInfo linesfile = new FileInfo(strinputfile);
				StreamReader reader = linesfile.OpenText();
				do
				{
					text = reader.ReadLine();
					if (text != null) linesdata.Add(text);
				} while (text != null);
				reader.Close();
				DecodeLines(linesdata);
			}
			else
			{
				MessageBox.Show("***Failed to Open Line Data File***");
				return false;
			}


            return SetData();

		} // end of GetNewData


        bool SetData()
        {
            ClearTimers();

            scrnpts = new double[numpts, 4];
            setIdentity(ctrans, 4, 4);  //initialize transformation matrix to identity

            double height = this.Height;
            double width = this.Width;
            double screenCenterY = height / 2;
            double screenCenterX = width / 2;

            double minY = 0, maxY = 0;

            for (int row = 0; row < numpts; row++)
            {
                if (vertices[row, 1] < minY)
                {
                    minY = vertices[row, 1];
                }

                if (vertices[row, 1] > maxY)
                {
                    maxY = vertices[row, 1];
                }
            }

            double sFx = height / (maxY - minY) / 2;
            double sFy = height / (maxY - minY) / 2;
            double sFz = height / (maxY - minY) / 2;

            double[,] t1 = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { -vertices[0,0], -vertices[0,1], -vertices[0,2], 1}
                };

            double[,] s1 = new double[,] {
                            { sFx, 0, 0, 0 },
                            { 0, -sFy, 0, 0 },
                            { 0, 0, sFz, 0 },
                            { 0, 0, 0, 1}
                };

            double[,] t2 = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { screenCenterX, screenCenterY, 0, 1}
                };

            double[,] tnet = multMatrics(t1, s1);
            tnet = multMatrics(tnet, t2);

            ctrans = tnet;

            return true;
        }


		void DecodeCoords(ArrayList coorddata)
		{
			//this may allocate slightly more rows that necessary
			vertices = new double[coorddata.Count,4];
			numpts = 0;
			string [] text = null;
			for (int i = 0; i < coorddata.Count; i++)
			{
				text = coorddata[i].ToString().Split(' ',',');
				vertices[numpts,0]=double.Parse(text[0]);
				if (vertices[numpts,0] < 0.0d) break;
				vertices[numpts,1]=double.Parse(text[1]);
				vertices[numpts,2]=double.Parse(text[2]);
				vertices[numpts,3] = 1.0d;
				numpts++;						
			}
			
		}// end of DecodeCoords

		void DecodeLines(ArrayList linesdata)
		{
			//this may allocate slightly more rows that necessary
			lines = new int[linesdata.Count,2];
			numlines = 0;
			string [] text = null;
			for (int i = 0; i < linesdata.Count; i++)
			{
				text = linesdata[i].ToString().Split(' ',',');
				lines[numlines,0]=int.Parse(text[0]);
				if (lines[numlines,0] < 0) break;
				lines[numlines,1]=int.Parse(text[1]);
				numlines++;						
			}
		} // end of DecodeLines

		void setIdentity(double[,] A,int nrow,int ncol)
		{
			for (int i = 0; i < nrow;i++) 
			{
				for (int j = 0; j < ncol; j++) A[i,j] = 0.0d;
				A[i,i] = 1.0d;
			}
		}// end of setIdentity
      

		private void Transformer_Load(object sender, System.EventArgs e)
		{
			
		}

		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
            // initial setup (find lowest point)

            if(scrnpts == null)
            {
                return;
            }

            var lowestPoint = 0;

            for (int i = 0; i < numpts; i++)
            {
                Console.WriteLine($"checking point ({i}): {scrnpts[i, 1]} > {scrnpts[lowestPoint, 1]}");
                if (scrnpts[i, 1] > scrnpts[lowestPoint, 1])
                {
                    lowestPoint = i;
                }

            }

            if (e.Button == transleftbtn)
			{
                Console.WriteLine("Left called");

                ClearTimers();

                tempTnet = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { -75, 0, 0, 1}
                };

                ctrans = multMatrics(ctrans, tempTnet);

                Refresh();
			}

			if (e.Button == transrightbtn) 
			{
                Console.WriteLine("Right called");

                ClearTimers();

                tempTnet = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { 75, 0, 0, 1}
                };

                ctrans = multMatrics(ctrans, tempTnet);

                Refresh();
			}
			if (e.Button == transupbtn)
			{
            
                Console.WriteLine("Up called");

                ClearTimers();

                tempTnet = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { 0, -75, 0, 1}
                };

                ctrans = multMatrics(ctrans, tempTnet);

                Refresh();
			}
			
			if(e.Button == transdownbtn)
			{
                Console.WriteLine("down called");

                ClearTimers();

                tempTnet = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { 0, 75, 0, 1}
                };

                ctrans = multMatrics(ctrans, tempTnet);

                Refresh();
			}

            if (e.Button == scaleupbtn)
            {

                Console.WriteLine("Scale up");

                ClearTimers();

                double x = scrnpts[0, 0];
                double y = scrnpts[0, 1];
                double z = scrnpts[0, 2];

                double[,] trans = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { -x, -y, -z, 1}
                };

                double[,] scale = new double[,] {
                            { 1.1, 0, 0, 0 },
                            { 0, 1.1, 0, 0 },
                            { 0, 0, 1.1, 0 },
                            { 0, 0, 0, 1}
                };

                double[,] transback = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { x, y, z, 1}
                };
                
                tempTnet = multMatrics(trans, scale);
                tempTnet = multMatrics(tempTnet, transback);

                ctrans = multMatrics(ctrans, tempTnet);
                Refresh();
			}

            if (e.Button == scaledownbtn)
            {

                double x = scrnpts[0, 0];
                double y = scrnpts[0, 1];
                double z = scrnpts[0, 2];

                Console.WriteLine("Scale down");

                ClearTimers();

                double[,] trans = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { -x, -y, -z, 1}
                };

                double[,] scale = new double[,] {
                            { 0.9, 0, 0, 0 },
                            { 0, 0.9, 0, 0 },
                            { 0, 0, 0.9, 0 },
                            { 0, 0, 0, 1}
                };

                double[,] transback = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { x, y, z, 1}
                };

                tempTnet = multMatrics(trans, scale);
                tempTnet = multMatrics(tempTnet, transback);

                ctrans = multMatrics(ctrans, tempTnet);
                Refresh();
            }

            if (e.Button == rotxby1btn) 
			{
                Console.WriteLine("rotate by x");

                ClearTimers();

                double x = scrnpts[0, 0];
                double y = scrnpts[0, 1];
                double z = scrnpts[0, 2];

                double[,] trans = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { -x, -y, -z, 1}
                };
                
                double [,] rot = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, Math.Cos(0.52), Math.Sin(0.52), 0 },
                            { 0, -Math.Sin(0.52), Math.Cos(0.52), 0 },
                            { 0, 0, 0, 1}
                };

                double[,] transback = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { x, y, z, 1}
                };


                tempTnet = multMatrics(trans, rot);
                tempTnet = multMatrics(tempTnet, transback);

                ctrans = multMatrics(ctrans, tempTnet);
                Refresh();
            }

			if (e.Button == rotyby1btn) 
			{
                Console.WriteLine("rotate by y");

                ClearTimers();


                double x = scrnpts[0, 0];
                double y = scrnpts[0, 1];
                double z = scrnpts[0, 2];

                double[,] trans = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { -x, -y, -z, 1}
                };


                double[,] rot = new double[,] {
                            { Math.Cos(0.52), 0, Math.Sin(0.52), 0 },
                            { 0, 1, 0, 0 },
                            { -Math.Sin(0.52), 0, Math.Cos(0.52), 0 },
                            { 0, 0, 0, 1}
                };

                double[,] transback = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { x, y, z, 1}
                };


                tempTnet = multMatrics(trans, rot);
                tempTnet = multMatrics(tempTnet, transback);

                ctrans = multMatrics(ctrans, tempTnet);
                Refresh();
            }

			if (e.Button == rotzby1btn) 
			{
                Console.WriteLine("rotate by z");

                ClearTimers();


                double x = scrnpts[0, 0];
                double y = scrnpts[0, 1];
                double z = scrnpts[0, 2];

                double[,] trans = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { -x, -y, -z, 1}
                };


                double[,] rot = new double[,] {
                            { Math.Cos(0.52), Math.Sin(0.52), 0, 0 },
                            { -Math.Sin(0.52), Math.Cos(0.52), 0, 0 },
                            {0,  0, 1, 0 },
                            { 0, 0, 0, 1}
                };

                double[,] transback = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { x, y, z, 1}
                };
                tempTnet = multMatrics(trans, rot);
                tempTnet = multMatrics(tempTnet, transback);

                ctrans = multMatrics(ctrans, tempTnet);
                Refresh(); Refresh();
            }

            // continuous
			if (e.Button == rotxbtn) 
			{
                Console.WriteLine("rotate by x");

                if(xTimer != null)
                {
                    ClearTimers();
                    return;
                }

                ClearTimers();

                xTimer = new System.Windows.Forms.Timer();
                xTimer.Tick += new EventHandler(rotateX);
                xTimer.Interval = 10; // in miliseconds
                xTimer.Start();
                
            }
			if (e.Button == rotybtn) 
			{
                Console.WriteLine("rotate by y");

                if (yTimer != null)
                {
                    ClearTimers();
                    return;
                }

                ClearTimers();



                yTimer = new System.Windows.Forms.Timer();
                yTimer.Tick += new EventHandler(rotateY);
                yTimer.Interval = 10; // in miliseconds
                yTimer.Start();
            }
			
			if (e.Button == rotzbtn) 
			{
                Console.WriteLine("rotate by z");

                if (zTimer != null)
                {
                    ClearTimers();
                    return;
                }

                ClearTimers();

                zTimer = new System.Windows.Forms.Timer();
                zTimer.Tick += new EventHandler(rotateZ);
                zTimer.Interval = 10; // in miliseconds
                zTimer.Start();

            }

			if(e.Button == shearleftbtn)
			{
                Console.WriteLine("shear left");

                ClearTimers();
                
                double x = scrnpts[lowestPoint, 0];
                double y = scrnpts[lowestPoint, 1];
                double z = scrnpts[lowestPoint, 2];

                double[,] trans = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { -x, -y, -z, 1}
                };


                double[,] transform = new double[,] {
                            { 1,    0, 0, 0 },
                            { 0.1,  1, 0, 0 },
                            { 0,    0, 1, 0 },
                            { 0,    0, 0, 1 }
                };

                double[,] transback = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { x, y, z, 1}
                };
                tempTnet = multMatrics(trans, transform);
                tempTnet = multMatrics(tempTnet, transback);

                ctrans = multMatrics(ctrans, tempTnet);

                Refresh();
			}

			if (e.Button == shearrightbtn) 
			{
                Console.WriteLine("shear right");

                ClearTimers();


                double x = scrnpts[lowestPoint, 0];
                double y = scrnpts[lowestPoint, 1];
                double z = scrnpts[lowestPoint, 2];

                double[,] trans = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { -x, -y, -z, 1}
                };


                double[,] transform = new double[,] {
                            { 1,    0,      0, 0 },
                            { -0.1, 1,      0, 0 },
                            { 0,    0,      1, 0 },
                            { 0,    0,      0, 1 }
                };

                double[,] transback = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { x, y, z, 1}
                };
                tempTnet = multMatrics(trans, transform);
                tempTnet = multMatrics(tempTnet, transback);

                ctrans = multMatrics(ctrans, tempTnet);
                Refresh();
			}

			if (e.Button == resetbtn)
			{
                ClearTimers();
                RestoreInitialImage();
			}

			if(e.Button == exitbtn) 
			{
                ClearTimers();
                Close();
			}

		} // end of button event handler

        public double[,] multMatrics(double[,] a, double[,] b)
        {
            double[,] result = new double[a.GetLength(0),b.GetLength(0)];
            double temp;

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    temp = 0.0d;

                    for (int k = 0; k < 4; k++)
                        temp += a[row, k] * b[k, col];

                    result[row, col] = temp;

                }
            }

            return result;
        }

        void rotateX(object sender, EventArgs e)
        {
            var val = 0.05;

            double x = scrnpts[0, 0];
            double y = scrnpts[0, 1];
            double z = scrnpts[0, 2];

            double[,] trans = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { -x, -y, -z, 1}
                };

            double[,] rot = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, Math.Cos(val), Math.Sin(val), 0 },
                            { 0, -Math.Sin(val), Math.Cos(val), 0 },
                            { 0, 0, 0, 1}
                };

            double[,] transback = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { x, y, z, 1}
                };


            tempTnet = multMatrics(trans, rot);
            tempTnet = multMatrics(tempTnet, transback);

            ctrans = multMatrics(ctrans, tempTnet);
            Refresh();
        }

        void rotateY(object sender, EventArgs e)
        {
            var val = 0.05;

            double x = scrnpts[0, 0];
            double y = scrnpts[0, 1];
            double z = scrnpts[0, 2];

            double[,] trans = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { -x, -y, -z, 1}
                };

            double[,] rot = new double[,] {
                            { Math.Cos(val), 0, Math.Sin(val), 0 },
                            { 0, 1, 0, 0 },
                            { -Math.Sin(val), 0, Math.Cos(val), 0 },
                            { 0, 0, 0, 1}
                };

            double[,] transback = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { x, y, z, 1}
                };


            tempTnet = multMatrics(trans, rot);
            tempTnet = multMatrics(tempTnet, transback);

            ctrans = multMatrics(ctrans, tempTnet);
            Refresh();
        }

        void rotateZ(object sender, EventArgs e)
        {
            var val = 0.05;

            double x = scrnpts[0, 0];
            double y = scrnpts[0, 1];
            double z = scrnpts[0, 2];

            double[,] trans = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { -x, -y, -z, 1}
                };

            double[,] rot = new double[,] {
                            { Math.Cos(val), Math.Sin(val), 0, 0 },
                            { -Math.Sin(val), Math.Cos(val), 0, 0 },
                            {0,  0, 1, 0 },
                            { 0, 0, 0, 1}
                };

            double[,] transback = new double[,] {
                            { 1, 0, 0, 0 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 },
                            { x, y, z, 1}
                };


            tempTnet = multMatrics(trans, rot);
            tempTnet = multMatrics(tempTnet, transback);

            ctrans = multMatrics(ctrans, tempTnet);
            Refresh();
        }



    }

	
}
