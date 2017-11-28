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
		private int[] values;
		
		public int scale;
		
		private GameObject raw;
		
		public void drawLines(Bitmap bmp){
			Pen blackPen = new Pen(System.Drawing.Color.Black, widthPen);
			
			using (var graphics = System.Drawing.Graphics.FromImage(bmp)) {				
					int rounds = Convert.ToInt32 (values.Length);
					float x = (float)width / (float)rounds;
					int j = 0, max = 0, min = height;
					int last = 0;
					print(x);
					for (int i=0; i<values.Length; i++) {
						graphics.DrawLine (blackPen, x * j, height-(last*scale), x * (j + 1), height-(values [i]*scale));
						last = values [i];
						max = Math.Max(max, values[i]);
						min = Math.Min(min, values[i]);
						j++;

					}
					drawReferPoints(bmp, max, min);
			}
			
		}
		
		public void drawReferPoints(Bitmap bmp, int max, int min) {
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
			}
			
		}
		
		void Start () {
			string filename = gameObject.name;

			if (filename == "ImageVelocity")
				values = InputOutput.getVelocities ().ToArray();
			else if (filename == "ImageElectrodermal")
				values = InputOutput.getElectrodermalActivities ().ToArray();
			else if (filename == "ImageHeart")
				values = InputOutput.getHeartRates ().ToArray();
			

			try{
				Bitmap image = (Bitmap) new Bitmap(width, height);
				
				TextureBrush texture = new TextureBrush(image);
				texture.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
				
				System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
				g.Clear(System.Drawing.Color.White);
				
				drawLines(image);
				
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

