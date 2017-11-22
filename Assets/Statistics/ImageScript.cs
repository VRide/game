using UnityEngine;
using System.Collections;
using System.Drawing;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour {
	
	private GameObject raw;
	
	public void DrawLineInt(Bitmap bmp)
	{
		Pen blackPen = new Pen(System.Drawing.Color.Black, 8);

		using(var graphics = System.Drawing.Graphics.FromImage(bmp))
		{
			graphics.DrawLine(blackPen, 0, 1000, 500, 550);
		}
	
	}
	
	void Start () {
	try
	{
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
	catch(System.Exception e)
	{
		print(e.StackTrace);
	}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
