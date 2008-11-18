using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FourBitmap
{
    internal static class FourBitmapRes
    {
        // si le control à besoin d'utilser des images par défaut, les ajouter dans cette classe
        // de la même manière que présenté ci dessous
        // NOTE: le fichier bitmap doit être ajouté à la solution et doit être copié vers le dossier de sortie 
        // lors de la compilation (voir les propriété du fichier)
        public static Bitmap DefaultBmp;
        public static void InitializeBitmap()
        {
            // adaptez le code ici afin de charger l'image souhaité
            string strAppDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            DefaultBmp = new Bitmap(strAppDir + "\\Res\\DefaultImage2.bmp");
        }
    }
}
