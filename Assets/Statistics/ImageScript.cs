using UnityEngine;
using System.Collections;
using System.Drawing;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ImageScript : MonoBehaviour {

	private const int width = 2000;
	private const int height = 1000;
	private const int widthPen = 20;
	private const int measureQuantity = 3;

	public int scale;

	private GameObject raw;

	public void drawLines(Bitmap bmp){
		Pen blackPen = new Pen(System.Drawing.Color.Black, widthPen);
		Pen greenPen = new Pen(System.Drawing.Color.Green, widthPen);
		Pen redPen = new Pen(System.Drawing.Color.Red, widthPen);

		using(var graphics = System.Drawing.Graphics.FromImage(bmp)){
			long count = DatabaseSingleton.Instance.db.SelectCount("from Measure where playerId==?", Convert.ToInt64(1));
		
			int rounds = (Convert.ToInt32(count)/measureQuantity);
			int x = width/rounds;
			int j = 0, max = 0, min = 0, average = 0;
			Measure last = new Measure(0, 0, 0, 0, 0, 0);
			foreach(var i in DatabaseSingleton.Instance.db.Select<Measure>("from Measure where playerId==? order by type", Convert.ToInt64(1))){
				int x1 = x*j;
				int y1 = height - (last.average * scale);
				int x2 = x*(j+1);
				int y2 = height - (i.average * scale);
				average = Math.Max(average, i.average);

				int x3 = x*j;
				int y3 = height - (last.min * scale);
				int x4 = x*(j+1);
				int y4 = height - (i.min * scale);
				min = Math.Max(min, i.min);

				int x5 =  x*j;
				int y5 = height - (last.max * scale);
				int x6 = x*(j+1);
				int y6 = height - (i.max * scale);
				max = Math.Max(max, i.max);

				if(i.type == 0 && gameObject.name == "ImageVelocity"){
					graphics.DrawLine(blackPen, x1 ,y1, x2, y2);
					graphics.DrawLine(redPen, x3 ,y3, x4, y4);
					graphics.DrawLine(greenPen, x5 ,y5, x6, y6);
					if((j+1)%rounds == 0) drawReferPoints(bmp, max, min, average, x); 
				}else if(i.type == 1 && gameObject.name == "ImageHeart"){
					graphics.DrawLine(blackPen, x1 ,y1, x2, y2);
					graphics.DrawLine(redPen, x3 ,y3, x4, y4);
					graphics.DrawLine(greenPen, x5 ,y5, x6, y6);
					if((j+1)%rounds == 0) drawReferPoints(bmp, max, min, average, x); 
				}else if (i.type == 2 && gameObject.name == "ImageElectrodermal"){
					graphics.DrawLine(blackPen, x1 ,y1, x2, y2);
					graphics.DrawLine(redPen, x3 ,y3, x4, y4);
					graphics.DrawLine(greenPen, x5 ,y5, x6, y6);
					if((j+1)%rounds == 0) drawReferPoints(bmp, max, min, average, x); 
				}
				last = i;
				j++;
				if(j%rounds == 0) j = max = min = average = 0; 
			}
		}
	
	}

	public void drawReferPoints(Bitmap bmp, int max, int min, int average, int xStep) {
		Pen grayPen = new Pen(System.Drawing.Color.Gray, 1); 

		using (var graphics = System.Drawing.Graphics.FromImage(bmp)) {
		
			System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 30);
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

			y = (float)height-(average * scale);
			drawString = Convert.ToString(average);
			drawPoint = new PointF(5F, (float)height-(average * scale));
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
