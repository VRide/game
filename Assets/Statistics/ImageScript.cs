using UnityEngine;
using System.Collections;
using System.Drawing;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour {
	
	private GameObject raw;
	
	public void DrawLineInt(Bitmap bmp)
	{
		Pen blackPen = new Pen(System.Drawing.Color.Black, 8);
		
		int x1 = 100;
		int y1 = 100;
		int x2 = 500;
		int y2 = 100;
		// Draw line to screen.
		using(var graphics = System.Drawing.Graphics.FromImage(bmp))
		{
			graphics.DrawLine(blackPen, x1, y1, x2, y2);
		}
	}
	
	void Start () {
		try
		{
			Bitmap image1 = (Bitmap) new Bitmap(2000,1000);

			TextureBrush texture = new TextureBrush(image1);
			texture.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;

			if(System.IO.File.Exists(@"Assets\Statistics\empty.png"))
				System.IO.File.Delete(@"Assets\Statistics\empty.png");
			
			
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image1);
			g.Clear(System.Drawing.Color.White);

			DrawLineInt(image1);
			image1.Save(@"Assets\Statistics\empty.png");
			image1.Dispose();

			UnityEditor.AssetDatabase.Refresh();
						
				
		}
		catch(System.Exception e)
		{
			print(e.StackTrace);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
