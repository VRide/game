using UnityEngine;
using System.Collections;
using System.Drawing;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageScript : MonoBehaviour {
	
	private GameObject raw;

	public void DrawLineInt(Bitmap bmp){
		Pen blackPen = new Pen(System.Drawing.Color.Black, 20);
		Pen greenPen = new Pen(System.Drawing.Color.Green, 20);
		Pen redPen = new Pen(System.Drawing.Color.Red, 20);

		using(var graphics = System.Drawing.Graphics.FromImage(bmp)){
			Measure[] measure;
			Player player1 = PlayerDAO.getPlayer(1);
			if(gameObject.name == "ImageVelocity"){
				measure = player1.velocities.ToArray();
			}else if(gameObject.name == "ImageElectrodermal"){
				measure = player1.electrodermalActivities.ToArray();
			}else{
				measure = player1.heartRates.ToArray();
			}

			int x = 2000/measure.Length;
			Measure last = new Measure(0, 0, 0);

			for(int i=0; i < measure.Length; i++){
				int x1 = x*(i+1);
				int y1 = 1000 - (last.max * 10);
				int x2 = x*(i+2);
				int y2 = 1000 - (measure[i].max * 10);
				graphics.DrawLine(greenPen, x1 ,y1, x2, y2);
				last = measure[i];
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
