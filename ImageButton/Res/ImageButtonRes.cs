﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using CommonLib;

namespace ImageButton
{
    internal static class ImageButtonRes
    {
        // si le control à besoin d'utilser des images par défaut, les ajouter dans cette classe
        // de la même manière que présenté ci dessous
        // NOTE: le fichier bitmap doit être ajouté à la solution et doit être copié vers le dossier de sortie 
        // lors de la compilation 
        // (voir les propriété du fichier => Build Action = "content", copy to output = "copy if newer")
        // public static Bitmap /*Nom de mon bitmap*/;
        public static Image DefaultImg;

        public static void InitializeBitmap()
        {
            // adaptez le code ici afin de charger l'image souhaité
            //string strAppDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            //*Nom de mon bitmap*/ = new Bitmap(PathTranslator.LinuxVsWindowsPathUse(strAppDir + "\\Res\\/*Nom de mon bitmap*/.bmp,png,gif,jpg"));
            string strAppDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            DefaultImg = new Bitmap(PathTranslator.LinuxVsWindowsPathUse(strAppDir + "\\Res\\DefaultBtnImage.bmp"));

        }
    }
}
