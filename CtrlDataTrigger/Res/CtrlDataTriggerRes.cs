/*
    This file is part of SmartApp.
    Copyright (C) 2007-2016  Pascal Bigot

    SmartApp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SmartApp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CtrlDataTrigger
{
    internal static class CtrlDataTriggerRes
    {
        // si le control à besoin d'utilser des images par défaut, les ajouter dans cette classe
        // de la même manière que présenté ci dessous
        // NOTE: le fichier bitmap doit être ajouté à la solution et doit être copié vers le dossier de sortie 
        // lors de la compilation (voir les propriété du fichier)
        // public static Bitmap /*Nom de mon bitmap*/;
        public static void InitializeBitmap()
        {
            // adaptez le code ici afin de charger l'image souhaité
            //string strAppDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            ///*Nom de mon bitmap*/ = new Bitmap(strAppDir + "\\Res\\/*Nom de mon bitmap*/.bmp");
        }
    }
}
