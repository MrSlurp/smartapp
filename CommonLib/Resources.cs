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
	/// Classes des ressources (images) communes de SmartApp.
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
        public static Bitmap WizardEthProject;
        public static Bitmap WizardZ2SLProject;

        public static Bitmap TypeSplit16_SL_IN;
        public static Bitmap TypeSplit4_SL_IN;
        public static Bitmap TypeSplit2_SL_IN;
        public static Bitmap TypeSplit16_SL_OUT;
        public static Bitmap TypeSplit4_SL_OUT;
        public static Bitmap TypeSplit2_SL_OUT;

        public static Bitmap TypeSplit16_ETH_IN;
        public static Bitmap TypeSplit4_ETH_IN;
        public static Bitmap TypeSplit2_ETH_IN;
        public static Bitmap TypeSplit16_ETH_OUT;
        public static Bitmap TypeSplit4_ETH_OUT;
        public static Bitmap TypeSplit2_ETH_OUT;
		
		public static Bitmap move;
		
		public static Bitmap CxnOn;
		public static Bitmap CxnOff;
        public static Bitmap Empty;
        public static Icon AppIcon;

        public static Bitmap TreeViewScreenIcon;
        public static Bitmap TreeViewGroupIcon;
        public static Bitmap TreeViewTimerIcon;
        public static Bitmap TreeViewFrameIcon;
        public static Bitmap TreeViewFunctionIcon;
        public static Bitmap TreeViewLoggerIcon;
        public static Bitmap TreeViewIOIcon;
        public static Icon TreeViewSolutionIcon;
        public static Bitmap TreeViewBridgeIcon;

        public static Bitmap SimpleArrowDown;
        public static Bitmap SimpleArrowUp;
        public static Bitmap SimpleArrowLeft;
        public static Bitmap SimpleArrowRight;
		
		public static void InitializeBitmap()
		{
            string strAppDir = Path.GetDirectoryName(Application.ExecutablePath);
            AppIcon = NewIcoTrPath(strAppDir + "\\SmartApp.ico");
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

            CxnOn = NewBmpTrPath(strAppDir + "\\Res\\green-on-016.png");
            CxnOff = NewBmpTrPath(strAppDir + "\\Res\\red-on-016.png");
            Empty = NewBmpTrPath(strAppDir + "\\Res\\EmptyImg.bmp");

            WizardSLProject = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\M3SL_wiz.png");
            WizardEthProject = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\M3Eth_wiz.png");
            WizardZ2SLProject = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\Z2SL_wiz.png");

            TypeSplit16_SL_IN = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\SLType_Split16.png");
            TypeSplit4_SL_IN = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\SLType_Split4.png");
            TypeSplit2_SL_IN = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\SLType_Split2.png");
            TypeSplit16_SL_OUT = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\SLType_Split16O.png");
            TypeSplit4_SL_OUT = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\SLType_Split4O.png");
            TypeSplit2_SL_OUT = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\SLType_Split2O.png");

            TypeSplit16_ETH_IN = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\EthType_Split16.png");
            TypeSplit4_ETH_IN = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\EthType_Split4.png");
            TypeSplit2_ETH_IN = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\EthType_Split2.png");
            TypeSplit16_ETH_OUT = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\EthType_Split16O.png");
            TypeSplit4_ETH_OUT = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\EthType_Split4O.png");
            TypeSplit2_ETH_OUT = NewBmpTrPath(strAppDir + "\\Res\\WizRes\\EthType_Split2O.png");

            TreeViewScreenIcon = NewBmpTrPath(strAppDir + "\\Res\\icon_screen.png");
            TreeViewGroupIcon = NewBmpTrPath(strAppDir + "\\Res\\icon_group.png");
            TreeViewTimerIcon = NewBmpTrPath(strAppDir + "\\Res\\icon_timer.png");
            TreeViewFrameIcon = NewBmpTrPath(strAppDir + "\\Res\\icon_frame.png");
            TreeViewFunctionIcon = NewBmpTrPath(strAppDir + "\\Res\\icon_function.png");
            TreeViewLoggerIcon = NewBmpTrPath(strAppDir + "\\Res\\icon_logger.png");
            TreeViewIOIcon = NewBmpTrPath(strAppDir + "\\Res\\icon_io.png");
            TreeViewSolutionIcon = NewIcoTrPath(strAppDir + "\\SmartAppSln.ico");
            TreeViewBridgeIcon = NewBmpTrPath(strAppDir + "\\Res\\icon_bridge.png"); ;

            SimpleArrowDown =  NewBmpTrPath(strAppDir + "\\Res\\arrow-down.png"); ;
            SimpleArrowUp = NewBmpTrPath(strAppDir + "\\Res\\arrow-up.png"); ;
            SimpleArrowLeft = NewBmpTrPath(strAppDir + "\\Res\\arrow-left.png"); ;
            SimpleArrowRight = NewBmpTrPath(strAppDir + "\\Res\\arrow-right.png"); ;
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
