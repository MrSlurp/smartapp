/*
 * Created by SharpDevelop.
 * User: Pascal
 * Date: 16/06/2008
 * Time: 21:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using Microsoft.Win32;
using System.Reflection;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;


namespace CommonLib
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public static class Resources
	{
		public static Bitmap AlignLeft;
		public static Bitmap AlignTop;
		public static Bitmap ArrangeAcross;
		public static Bitmap ArrangeDown;
		public static Bitmap BottomBtn;
		public static Bitmap BottomLeftBtn;
		public static Bitmap BottomRightBtn;
		public static Bitmap CheckBox;
		public static Bitmap DropBtn;
		public static Bitmap LeftBtn;
		public static Bitmap MakeSameBoth;
		public static Bitmap MakeSameHeight;
		public static Bitmap MakeSameWidth;
		public static Bitmap PresAssData;
		public static Bitmap RightBtn;
		public static Bitmap SliderBar;
		public static Bitmap SliderCursor;
		public static Bitmap TopBtn;
		public static Bitmap TopLeftBtn;
		public static Bitmap TopRightBtn;
		public static Bitmap UpDownBtn;
		
		public static Bitmap SLI1;
		public static Bitmap SLI2;
		public static Bitmap SLI3;
		public static Bitmap SLO1;
		public static Bitmap SLO2;
		public static Bitmap SLO3;
		
		public static Bitmap move;
		
		public static Bitmap CxnOn;
		public static Bitmap CxnOff;
        public static Bitmap Empty;
		
		public static void InitializeBitmap()
		{
            string strAppDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
			AlignLeft = new Bitmap(strAppDir + "\\Res\\ToolBar\\AlignLeft.bmp");
			AlignTop = new Bitmap(strAppDir + "\\Res\\ToolBar\\AlignTop.bmp");
			ArrangeAcross = new Bitmap(strAppDir + "\\Res\\ToolBar\\ArrangeAcross.bmp");
			ArrangeDown = new Bitmap(strAppDir + "\\Res\\ToolBar\\ArrangeDown.bmp");
			BottomBtn = new Bitmap(strAppDir + "\\Res\\DownBtn.bmp");
			BottomLeftBtn = new Bitmap(strAppDir + "\\Res\\BottomLeftBtn.bmp");
			BottomRightBtn = new Bitmap(strAppDir + "\\Res\\BottomRightBtn.bmp");
			CheckBox = new Bitmap(strAppDir + "\\Res\\CheckBox.bmp");
			DropBtn = new Bitmap(strAppDir + "\\Res\\DropBtn.bmp");
			LeftBtn = new Bitmap(strAppDir + "\\Res\\LeftBtn.bmp");
			MakeSameBoth = new Bitmap(strAppDir + "\\Res\\ToolBar\\MakeSameBoth.bmp");
			MakeSameHeight = new Bitmap(strAppDir + "\\Res\\ToolBar\\MakeSameHeight.bmp");
			MakeSameWidth = new Bitmap(strAppDir + "\\Res\\ToolBar\\MakeSameWidth.bmp");
			PresAssData = new Bitmap(strAppDir + "\\Res\\PresAssData.bmp");
			RightBtn = new Bitmap(strAppDir + "\\Res\\RightBtn.bmp");
			SliderBar = new Bitmap(strAppDir + "\\Res\\SliderBar.bmp");
			SliderCursor = new Bitmap(strAppDir + "\\Res\\SliderCursor.bmp");
			TopBtn = new Bitmap(strAppDir + "\\Res\\TopBtn.bmp");
			TopLeftBtn = new Bitmap(strAppDir + "\\Res\\TopLeftBtn.bmp");
			TopRightBtn = new Bitmap(strAppDir + "\\Res\\TopRightBtn.bmp");
			UpDownBtn = new Bitmap(strAppDir + "\\Res\\UpDownBtn.bmp");
			
			SLI1 = new Bitmap(strAppDir + "\\Res\\WizRes\\SLI1.bmp");
			SLI2 = new Bitmap(strAppDir + "\\Res\\WizRes\\SLI2.bmp");
			SLI3= new Bitmap(strAppDir + "\\Res\\WizRes\\SLI3.bmp");
			SLO1 = new Bitmap(strAppDir + "\\Res\\WizRes\\SLO1.bmp");
			SLO2 = new Bitmap(strAppDir + "\\Res\\WizRes\\SLO2.bmp");
			SLO3 = new Bitmap(strAppDir + "\\Res\\WizRes\\SLO3.bmp");
			
			move = new Bitmap(strAppDir + "\\Res\\move.bmp");			

			CxnOn = new Bitmap(strAppDir + "\\Res\\CxnOn.bmp");			
			CxnOff = new Bitmap(strAppDir + "\\Res\\CxnOff.bmp");			
            Empty = new Bitmap(strAppDir + "\\Res\\EmptyImg.bmp");			
		}
		
	}
}
