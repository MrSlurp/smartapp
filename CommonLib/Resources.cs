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

        public static Bitmap WizardSLProject;
        public static Bitmap TypeSplit16;
        public static Bitmap TypeSplit4;
        public static Bitmap TypeSplit2;
		
		public static Bitmap move;
		
		public static Bitmap CxnOn;
		public static Bitmap CxnOff;
        public static Bitmap Empty;
        public static Icon AppIcon;
		
		public static void InitializeBitmap()
		{
            string strAppDir = Path.GetDirectoryName(Application.ExecutablePath);
#if KENNEN
            AppIcon = NewIcoTrPath(strAppDir + "\\Kennen.ico");
#else
            AppIcon = NewIcoTrPath(strAppDir + "\\SmartApp.ico");
#endif
            AlignLeft = NewBmpTrPath(strAppDir + "\\Res\\ToolBar\\AlignLeft.bmp");
            AlignTop = NewBmpTrPath(strAppDir + "\\Res\\ToolBar\\AlignTop.bmp");
            ArrangeAcross = NewBmpTrPath(strAppDir + "\\Res\\ToolBar\\ArrangeAcross.bmp");
            ArrangeDown = NewBmpTrPath(strAppDir + "\\Res\\ToolBar\\ArrangeDown.bmp");
            BottomBtn = NewBmpTrPath(strAppDir + "\\Res\\DownBtn.bmp");
            BottomLeftBtn = NewBmpTrPath(strAppDir + "\\Res\\BottomLeftBtn.bmp");
            BottomRightBtn = NewBmpTrPath(strAppDir + "\\Res\\BottomRightBtn.bmp");
            CheckBox = NewBmpTrPath(strAppDir + "\\Res\\CheckBox.bmp");
            DropBtn = NewBmpTrPath(strAppDir + "\\Res\\DropBtn.bmp");
            LeftBtn = NewBmpTrPath(strAppDir + "\\Res\\LeftBtn.bmp");
            MakeSameBoth = NewBmpTrPath(strAppDir + "\\Res\\ToolBar\\MakeSameBoth.bmp");
            MakeSameHeight = NewBmpTrPath(strAppDir + "\\Res\\ToolBar\\MakeSameHeight.bmp");
            MakeSameWidth = NewBmpTrPath(strAppDir + "\\Res\\ToolBar\\MakeSameWidth.bmp");
            PresAssData = NewBmpTrPath(strAppDir + "\\Res\\PresAssData.bmp");
            RightBtn = NewBmpTrPath(strAppDir + "\\Res\\RightBtn.bmp");
            SliderBar = NewBmpTrPath(strAppDir + "\\Res\\SliderBar.bmp");
            SliderCursor = NewBmpTrPath(strAppDir + "\\Res\\SliderCursor.bmp");
            TopBtn = NewBmpTrPath(strAppDir + "\\Res\\TopBtn.bmp");
            TopLeftBtn = NewBmpTrPath(strAppDir + "\\Res\\TopLeftBtn.bmp");
            TopRightBtn = NewBmpTrPath(strAppDir + "\\Res\\TopRightBtn.bmp");
            UpDownBtn = NewBmpTrPath(strAppDir + "\\Res\\UpDownBtn.bmp");

            SLI1 = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\SLI1.bmp");
            SLI2 = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\SLI2.bmp");
            SLI3 = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\SLI3.bmp");
            SLO1 = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\SLO1.bmp");
            SLO2 = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\SLO2.bmp");
            SLO3 = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\SLO3.bmp");

            move = NewBmpTrPath(strAppDir + "\\Res\\move.bmp");

            CxnOn = NewBmpTrPath(strAppDir + "\\Res\\CxnOn.bmp");
            CxnOff = NewBmpTrPath(strAppDir + "\\Res\\CxnOff.bmp");
            Empty = NewBmpTrPath(strAppDir + "\\Res\\EmptyImg.bmp");

            WizardSLProject = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\M3SL_wiz.png");

            TypeSplit16 = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\Type_Split16.png");
            TypeSplit4 = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\Type_Split4.png");
            TypeSplit2 = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\Type_Split2.png");

		}

        public static Bitmap NewBmpTrPath(string path)
        {
            return new Bitmap(PathTranslator.LinuxVsWindowsPathUse(path));
        }

        public static Icon NewIcoTrPath(string path)
        {
            return new Icon(PathTranslator.LinuxVsWindowsPathUse(path));
        }
    }
}
