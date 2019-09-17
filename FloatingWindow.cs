using System;
using System.Windows.Forms;

namespace FloatingWindow
{
	partial class MainForm: Form,IMessageFilter
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel panel_0;
		private System.Windows.Forms.Panel panel_1;
		private System.Windows.Forms.Panel panel_2;
		private System.Windows.Forms.Panel panel_3;
		private BGLabel lab_0;
		private System.Windows.Forms.Label lab_1;
		private System.Windows.Forms.Label lab_2;
		private BGLabel lab_3;
		private BGLabel lab_4;
		private System.Windows.Forms.Label lab_5;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ListView listView;

		private Label label=null;
		private bool isShowing=false;
		private bool isLock=false;
		
		private bool isMove=false;
		private bool isClick=false;
		private int curXPosition;  
		private int curYPosition;
		
		private Monitor monitor;
		
		public MainForm(){
			InitializeComponent();
			Application.AddMessageFilter(this);
			this.Deactivate += Form_Deactivate;
			monitor=new Monitor();
		}
		
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel_1 = new System.Windows.Forms.Panel();
            this.lab_2 = new System.Windows.Forms.Label();
            this.lab_1 = new System.Windows.Forms.Label();
            this.panel_2 = new System.Windows.Forms.Panel();
            this.lab_4 = new FloatingWindow.BGLabel();
            this.lab_3 = new FloatingWindow.BGLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panel_0 = new System.Windows.Forms.Panel();
            this.lab_0 = new FloatingWindow.BGLabel();
            this.panel_1.SuspendLayout();
            this.panel_2.SuspendLayout();
            this.panel_0.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_1
            // 
            this.panel_1.Controls.Add(this.lab_2);
            this.panel_1.Controls.Add(this.lab_1);
            this.panel_1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_1.Location = new System.Drawing.Point(52, 0);
            this.panel_1.Name = "panel_1";
            this.panel_1.Size = new System.Drawing.Size(72, 52);
            this.panel_1.TabIndex = 6;
            // 
            // lab_2
            // 
            this.lab_2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lab_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lab_2.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.lab_2.ForeColor = System.Drawing.Color.White;
            this.lab_2.Image = ((System.Drawing.Image)(resources.GetObject("lab_2.Image")));
            this.lab_2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lab_2.Location = new System.Drawing.Point(0, 26);
            this.lab_2.Margin = new System.Windows.Forms.Padding(0);
            this.lab_2.Name = "lab_2";
            this.lab_2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lab_2.Size = new System.Drawing.Size(72, 26);
            this.lab_2.TabIndex = 3;
            this.lab_2.Text = "0.00KB/s";
            this.lab_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lab_2.Click += new System.EventHandler(this.Lab_Click);
            // 
            // lab_1
            // 
            this.lab_1.BackColor = System.Drawing.Color.Black;
            this.lab_1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lab_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lab_1.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.lab_1.ForeColor = System.Drawing.Color.White;
            this.lab_1.Image = ((System.Drawing.Image)(resources.GetObject("lab_1.Image")));
            this.lab_1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lab_1.Location = new System.Drawing.Point(0, 0);
            this.lab_1.Margin = new System.Windows.Forms.Padding(0);
            this.lab_1.Name = "lab_1";
            this.lab_1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lab_1.Size = new System.Drawing.Size(72, 26);
            this.lab_1.TabIndex = 2;
            this.lab_1.Text = "0.00KB/s";
            this.lab_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lab_1.Click += new System.EventHandler(this.Lab_Click);
            // 
            // panel_2
            // 
            this.panel_2.Controls.Add(this.lab_4);
            this.panel_2.Controls.Add(this.lab_3);
            this.panel_2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_2.Location = new System.Drawing.Point(124, 0);
            this.panel_2.Name = "panel_2";
            this.panel_2.Size = new System.Drawing.Size(64, 52);
            this.panel_2.TabIndex = 7;
            // 
            // lab_4
            // 
            this.lab_4.Dock = System.Windows.Forms.DockStyle.Top;
            this.lab_4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lab_4.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.lab_4.ForeColor = System.Drawing.Color.White;
            this.lab_4.Location = new System.Drawing.Point(0, 26);
            this.lab_4.Margin = new System.Windows.Forms.Padding(0);
            this.lab_4.Name = "lab_4";
            this.lab_4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lab_4.Size = new System.Drawing.Size(64, 26);
            this.lab_4.TabIndex = 5;
            this.lab_4.Text = "DISK 0%";
            this.lab_4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lab_4.Click += new System.EventHandler(this.Lab_Click);
            // 
            // lab_3
            // 
            this.lab_3.Dock = System.Windows.Forms.DockStyle.Top;
            this.lab_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lab_3.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.lab_3.ForeColor = System.Drawing.Color.White;
            this.lab_3.Location = new System.Drawing.Point(0, 0);
            this.lab_3.Margin = new System.Windows.Forms.Padding(0);
            this.lab_3.Name = "lab_3";
            this.lab_3.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lab_3.Size = new System.Drawing.Size(64, 26);
            this.lab_3.TabIndex = 4;
            this.lab_3.Text = "CPU 0%";
            this.lab_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lab_3.Click += new System.EventHandler(this.Lab_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // panel_0
            // 
            this.panel_0.BackColor = System.Drawing.Color.Black;
            this.panel_0.Controls.Add(this.lab_0);
            this.panel_0.Controls.Add(this.panel_1);
            this.panel_0.Controls.Add(this.panel_2);
            this.panel_0.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_0.Location = new System.Drawing.Point(0, 0);
            this.panel_0.Margin = new System.Windows.Forms.Padding(0);
            this.panel_0.Name = "panel_0";
            this.panel_0.Size = new System.Drawing.Size(188, 52);
            this.panel_0.TabIndex = 8;
            // 
            // lab_0
            // 
            this.lab_0.BackColor = System.Drawing.Color.Black;
            this.lab_0.Dock = System.Windows.Forms.DockStyle.Left;
            this.lab_0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lab_0.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.lab_0.ForeColor = System.Drawing.Color.White;
            this.lab_0.Location = new System.Drawing.Point(0, 0);
            this.lab_0.Margin = new System.Windows.Forms.Padding(0);
            this.lab_0.Name = "lab_0";
            this.lab_0.Size = new System.Drawing.Size(52, 52);
            this.lab_0.TabIndex = 8;
            this.lab_0.Text = "RAM\n0%";
            this.lab_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lab_0.Click += new System.EventHandler(this.Lab_Click);
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(188, 52);
            this.Controls.Add(this.panel_0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(1100, 120);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(0, 52);
            this.Name = "MainForm";
            this.Opacity = 0.9D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.panel_1.ResumeLayout(false);
            this.panel_2.ResumeLayout(false);
            this.panel_0.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		
		//		WM_MOUSEMOVE= $0200 	移动鼠标
		//		WM_LBUTTONDOWN= $0201 	按下鼠标左键
		//		WM_LBUTTONUP= $0202 	释放鼠标左键
		
		public bool PreFilterMessage(ref Message m)
        {
			
			if(m.Msg==0x0204){
				isMove = true;
				isClick = true;
               	return true;
			}else if (m.Msg == 0x0205&&isClick){
				isMove = false;
				isLock=!isLock;
				return true;
			}else if (!isLock&&m.Msg == 0x0201){
				isMove = true;
				isClick = true;
               	curXPosition = MousePosition.X;
               	curYPosition = MousePosition.Y;
                return false;
            }else if(!isLock&&m.Msg == 0x0200&&isMove){
				isClick=false;
				this.Left += MousePosition.X - curXPosition;
        		this.Top += MousePosition.Y - curYPosition;
        		curXPosition = MousePosition.X;
        		curYPosition = MousePosition.Y;
            	return true;
            }else if(!isLock&&m.Msg == 0x0202){
             	isMove = false;
            	return !isClick;
            }
            return false;
        }
		
		private void timer_Tick(object sender, System.EventArgs e)
		{
			monitor.refresh();
			if(monitor.isRamUtilChange()){
				lab_0.Value=monitor.getRamUtil();
				lab_0.Text="RAM\n"+monitor.getRamUtil()+"%";
			}
			if(monitor.isDownSpeedChange())
				lab_1.Text=monitor.getDownSpeed();
			if(monitor.isUpSpeedChange())
				lab_2.Text=monitor.getUpSpeed();
			if(monitor.isCpuUtilChange()){
				lab_3.Value=monitor.getCpuUtil();
				lab_3.Text="CPU "+monitor.getCpuUtil()+"%";
			}
			if(monitor.isDiskUtilChange()){
				lab_4.Value=monitor.getDiskUtil();
				lab_4.Text="DISK "+monitor.getDiskUtil()+"%";
			}
		}
		
		private void MainFormLoad(object sender, EventArgs e)
		{
			lab_0.Value=monitor.getRamUtil();
			lab_0.Text="RAM\n"+monitor.getRamUtil()+"%";
			lab_3.BackColorVisible=false;
			lab_3.Value=monitor.getCpuUtil();
			lab_3.Text="CPU "+monitor.getCpuUtil()+"%";
			lab_4.BackColorVisible=false;
			lab_4.Value=monitor.getDiskUtil();
			lab_4.Text="DISK "+monitor.getDiskUtil()+"%";
		}
		
		private void Lab_Click(object sender, EventArgs e)
		{
			if(!isShowing){
				label=(Label)sender;
				label.BackColor=System.Drawing.Color.FromArgb(30,30,30);
				isShowing=true;
//				showListDialog();
			}else if((Label)sender==label){
				label.BackColor=System.Drawing.Color.Black;
				isShowing=false;
				label=null;
//				closeListDialog();
			}else{
				if(label!=null)
					label.BackColor=System.Drawing.Color.Black;
				label=(Label)sender;
				label.BackColor=System.Drawing.Color.FromArgb(30,30,30);
//				showListDialog();
			}
		}
		
		private void showListDialog(){
			// 
			// imageList
			// 
			imageList = new ImageList(this.components);
//			imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
//			imageList.Images.SetKeyName(0, "down.png");
//			imageList.Images.SetKeyName(1, "up.png");
			// 
			// listView
			// 
			listView = new ListView();
			listView.BackColor = System.Drawing.Color.Black;
			listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			listView.Font = new System.Drawing.Font("微软雅黑", 8F);
			listView.ForeColor = System.Drawing.Color.White;
			listView.Columns.Add("appName",120);
			listView.Columns.Add("data",68);
			listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			listView.Location = new System.Drawing.Point(0, 22);
			listView.Margin = new System.Windows.Forms.Padding(0);
			listView.Name = "listView";
			listView.Size = new System.Drawing.Size(206, 182);
			listView.SmallImageList = this.imageList;
			listView.View = System.Windows.Forms.View.Details;
			// 
			// lab_5
			// 
			lab_5 = new Label();
			lab_5.BackColor = System.Drawing.Color.Black;
			lab_5.Font = new System.Drawing.Font("微软雅黑", 8F);
			lab_5.ForeColor = System.Drawing.Color.White;
			lab_5.Location = new System.Drawing.Point(0, 1);
			lab_5.Margin = new System.Windows.Forms.Padding(0);
			lab_5.Name = "lab_5";
			lab_5.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			lab_5.Size = new System.Drawing.Size(188, 21);
			lab_5.TabIndex = 10;
			lab_5.Text = "详细信息";
			lab_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel_3
			// 
			panel_3 = new Panel();
			panel_3.Controls.Add(this.lab_5);
			panel_3.Controls.Add(this.listView);
			panel_3.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel_3.Location = new System.Drawing.Point(0, 52);
			panel_3.Name = "panel_3";
			panel_3.Size = new System.Drawing.Size(188, 224);
			panel_3.TabIndex = 10;
			
			this.Controls.Add(this.panel_3);
			
			this.Height=276;
			listView.BeginUpdate();
			for(int i=0;i<20;i++){
				ListViewItem lvi=new ListViewItem();
//				lvi.ImageIndex=i%2;
				lvi.Text="应用名"+i;
				lvi.SubItems.Add("124.3KB/s");
				listView.Items.Add(lvi);
			}
			listView.EndUpdate();
		}
		
		private void closeListDialog(){
			this.Height=52;
			listView.Clear();
		}
		
		private void Form_Deactivate(Object sender, EventArgs e) {
			if(isShowing&&label!=null){
				label.BackColor=System.Drawing.Color.Black;
				isShowing=false;
				label=null;
//				closeListDialog();
			}
		}
	}
}
