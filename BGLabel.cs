
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FloatingWindow
{
	public partial class BGLabel : Label
	{
		public bool BackColorVisible=true;
		public bool BorderColorVisible=true;
		public int Taget=70;
		public int Value=00;
		
		public BGLabel()
		{
			
		}
		
		protected override void OnPaintBackground (PaintEventArgs e){
			base.OnPaintBackground(e);
			if(BackColorVisible){
				if(Value>=Taget){
					Brush brush=new SolidBrush(Color.Red);
				int height=Height*Value/100;
				e.Graphics.FillRectangle(brush,0,Height-height,Width,Height);
				}else{
					Brush brush=new SolidBrush(Color.Green);
					int height=Height*Value/100;
					e.Graphics.FillRectangle(brush,0,Height-height,Width,Height);
				}
			}
			if(BorderColorVisible&&Value>=Taget){
				Pen pen = new Pen(Color.Red,1);
				e.Graphics.DrawRectangle(pen,0,0,Width-1,Height-1);
			}
		}
	}
}
