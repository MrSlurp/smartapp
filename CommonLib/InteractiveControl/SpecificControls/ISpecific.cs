/*
    This file is part of SmartApp.

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
using System.Windows.Forms;
using System.Drawing;

namespace CommonLib
{
    /// <summary>
    /// interface des controles spécifiques et DLL
    /// </summary>
    public interface ISpecificControl
    {
        /// <summary>
        /// fonction de dessin personelle
        /// </summary>
        /// <param name="gr">graphics du control</param>
        /// <param name="ctrl">objet control</param>
        void SelfPaint(Graphics gr, Control ctrl);
        /// <summary>
        /// form de paramétrage du control spécifique
        /// </summary>
        UserControl SpecificPropPanel { get;}
        /// <summary>
        /// accesseur sur les propriété d'activation des paramètres standards
        /// </summary>
        StandardPropEnabling StdPropEnabling { get;}
        /// <summary>
        /// accesseur sur les propriété de comportement graphique des controles spécifiques et DLL
        /// </summary>
        SpecificGraphicProp SpecGraphicProp { get;}
    }

    /// <summary>
    /// propriété d'activation des différents paramètres standards pour les controles spécifiques et DLL
    /// </summary>
    public struct StandardPropEnabling
    {
        /// <summary>
        /// autorise ou non la saisie d'un texte pour le control
        /// </summary>
        public bool m_bEditTextEnabled;
        /// <summary>
        /// autorise ou non la saisie d'un texte pour le control
        /// </summary>
        public bool m_bSelectFontEnabled;
        /// <summary>
        /// autorise ou non l'association d'une donnée
        /// </summary>
        public bool m_bEditAssociateDataEnabled;
        /// <summary>
        /// autorise ou non le paramètre read only pour un control
        /// </summary>
        public bool m_bcheckReadOnlyEnabled;
        /// <summary>
        /// coche par défaut ou non le paramètre read only
        /// </summary>
        public bool m_bcheckReadOnlyChecked;
        /// <summary>
        /// autorise ou non le paramètre UseScreenEvent pour un control
        /// </summary>
        public bool m_bcheckScreenEventEnabled;
        /// <summary>
        /// coche par défaut ou non le paramètre UseScreenEvent
        /// </summary>
        public bool m_bcheckScreenEventChecked;
        /// <summary>
        /// autorise ou non la saisie d'un script pour le control
        /// </summary>
        public bool m_bCtrlEventScriptEnabled;
    }

    /// <summary>
    /// propriété des controles spécifiques dans la surface de dessin.
    /// </summary>
    public class SpecificGraphicProp
    {
        /// <summary>
        /// autorise ou non le redimensionnement en largeur
        /// </summary>
        public bool m_bcanResizeWidth;
        /// <summary>
        /// autorise ou non le redimensionnement en hauteur
        /// </summary>
        public bool m_bcanResizeHeight;
        /// <summary>
        /// taille minimum du controle spécifique dans la surface de cablage
        /// </summary>
        public Size m_MinSize;
        /// <summary>
        /// taille minimum du controle spécifique dans la surface de cablage
        /// </summary>
        public Size m_MaxSize;

        public SpecificGraphicProp()
        {
            m_bcanResizeWidth = true;
            m_bcanResizeHeight = true;
            m_MinSize = new Size (5,5);
            m_MaxSize = new Size(800, 800);
        }
    }
}
