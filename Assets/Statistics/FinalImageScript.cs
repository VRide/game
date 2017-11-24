using UnityEngine;
using System.Collections;
using System.Drawing;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class FinalImageScript : MonoBehaviour
{
	
		private const int width = 2000;
		private const int height = 1000;
		private const int widthPen = 20;
		
		public int scale;
		
		private GameObject raw;
		
		public void drawLines(Bitmap bmp){
			Pen blackPen = new Pen(System.Drawing.Color.Black, widthPen);
			
			//int [] velocity_values = InputOutput.getVelocities ();
			int[] velocity_values = new int[] {0, 14, 15, 26, 18, 19, 24, 56};
			
			using (var graphics = System.Drawing.Graphics.FromImage(bmp)) {				
					int rounds = Convert.ToInt32 (velocity_values.Length);
					int x = width / rounds;
					int j = 0, max = 0, min = height;
					int last = 0;
					for (int i=0; i<velocity_values.Length; i++) {
						graphics.DrawLine (blackPen, x * j, height-(last*scale), x * (j + 1), height-(velocity_values [i]*scale));
						last = velocity_values [i];
						max = Math.Max(max, velocity_values[i]);
						min = Math.Min(min, velocity_values[i]);
						j++;

					}
					drawReferPoints(bmp, max, min, x);
			}
			
		}
		
		public void drawReferPoints(Bitmap bmp, int max, int min ,int xStep) {
			Pen grayPen = new Pen(System.Drawing.Color.Gray, 1); 
			
			using (var graphics = System.Drawing.Graphics.FromImage(bmp)) {
				
				System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 40);
				SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);
				
				float y = (float)height-(min * scale);
				String drawString = Convert.ToString(min);
				PointF drawPoint = new PointF(5F, y);
				graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
				graphics.DrawLine(grayPen, 0F, y, width, y);
				
				y = (float)height-(max * scale);
				drawString = Convert.ToString(max);
				drawPoint = new PointF(5F, (float)height-(max * scale));
				graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
				graphics.DrawLine(grayPen, 0F, y, width, y);
				
				if(xStep < 10) xStep = 10;
				
				for(int i = xStep; i <= width; i+= xStep){
					drawString = Convert.ToString(i/xStep);
					drawPoint = new PointF((float)i, 40F);
					graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
					graphics.DrawLine(grayPen, i, 0F, i, height);
				}
			}
			
		}
		
		void Start () {
			try{
				Bitmap image = (Bitmap) new Bitmap(width, height);
				
				TextureBrush texture = new TextureBrush(image);
				texture.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
				
				System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
				g.Clear(System.Drawing.Color.White);
				
				drawLines(image);
				
				string filename = gameObject.name;
				if(System.IO.File.Exists(@"Assets\Statistics\" + filename + ".png"))
					System.IO.File.Delete(@"Assets\Statistics\" + filename + ".png");
				
				image.Save(@"Assets\Statistics\"+ filename +".png");
				image.Dispose();
				
				UnityEditor.AssetDatabase.Refresh();
			}
			catch(System.Exception e){
				print(e.StackTrace);
			}
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
}

