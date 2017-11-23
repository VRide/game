using UnityEngine;
using System.Collections;
using System.Drawing;
using UnityEngine.UI;
using System.Collections.Generic;
//using System.Windows.Forms;
using System;

public class ImageScript : MonoBehaviour {
	
	private GameObject raw;

	public void DrawLineInt(Bitmap bmp){
		Pen blackPen = new Pen(System.Drawing.Color.Black, 20);
		Pen greenPen = new Pen(System.Drawing.Color.Green, 20);
		Pen redPen = new Pen(System.Drawing.Color.Red, 20);

		using(var graphics = System.Drawing.Graphics.FromImage(bmp)){
			long count = DatabaseSingleton.Instance.db.SelectCount("from Measure where playerId==?", Convert.ToInt64(1));
		
			int rounds = (Convert.ToInt32(count)/3);
			int x = 2000/rounds;
			int j = 0;
			Measure last = new Measure(0, 0, 0, 0, 0, 0);
			foreach(var i in DatabaseSingleton.Instance.db.Select<Measure>("from Measure where playerId==? order by type", Convert.ToInt64(1))){
				int x1 = x*j;
				int y1 = 1000 - (last.average * 10);
				int x2 = x*(j+1);
				int y2 = 1000 - (i.average * 10);

				int x3 = x*j;
				int y3 = 1000 - (last.min * 10);
				int x4 = x*(j+1);
				int y4 = 1000 - (i.min * 10);

				int x5 =  x*j;
				int y5 = 1000 - (last.max * 10);
				int x6 = x*(j+1);
				int y6 = 1000 - (i.max * 10);

				if(i.type == 0 && gameObject.name == "ImageVelocity"){
					graphics.DrawLine(blackPen, x1 ,y1, x2, y2);
					graphics.DrawLine(redPen, x3 ,y3, x4, y4);
					graphics.DrawLine(greenPen, x5 ,y5, x6, y6);

					//graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
					//TextRenderer.DrawText(graphics, "Hello World",  new System.Drawing.Font("Arial", 12), new Point(0, y4), System.Drawing.Color.Red);

				}else if(i.type == 1 && gameObject.name == "ImageHeart"){
					graphics.DrawLine(blackPen, x1 ,y1, x2, y2);
					graphics.DrawLine(redPen, x3 ,y3, x4, y4);
					graphics.DrawLine(greenPen, x5 ,y5, x6, y6);
				}else if (i.type == 2 && gameObject.name == "ImageElectrodermal"){
					graphics.DrawLine(blackPen, x1 ,y1, x2, y2);
					graphics.DrawLine(redPen, x3 ,y3, x4, y4);
					graphics.DrawLine(greenPen, x5 ,y5, x6, y6);

				}
				last = i;
				j++;
				if(j%rounds == 0)j=0;
			
			}

		}
	
	}
	
	void Start () {
		try{
			Bitmap image = (Bitmap) new Bitmap(2000,1000);

			TextureBrush texture = new TextureBrush(image);
			texture.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
		
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
			g.Clear(System.Drawing.Color.White);

			DrawLineInt(image);
			
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
