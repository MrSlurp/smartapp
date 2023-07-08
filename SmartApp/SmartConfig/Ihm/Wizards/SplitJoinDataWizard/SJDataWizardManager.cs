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
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace SmartApp.Wizards
{
    public interface ISJWizForm
    {
        /// <summary>
        /// fonction effectuant le transfert entre l'IHM et les config du Wizard
        /// </summary>
        void HmiToData();

        /// <summary>
        /// fonction effectuant le transfert entre l'IHM et les config du Wizard
        /// </summary>
        void DataToHmi();

        bool AllowGoNext { get; }

        /// <summary>
        /// position d'attachement de la form dans le parent
        /// </summary>
        AnchorStyles Anchor
        { get; set; }

        /// <summary>
        /// définit si le panel est visible
        /// </summary>
        bool Visible
        { get; set; }

        /// <summary>
        /// assigne ou obtient la largeur du panel
        /// </summary>
        int Width
        { get; set; }

        /// <summary>
        /// assigne ou obtient la hauteur du panel
        /// </summary>
        int Height
        { get; set; }

        SJDataWizardManager SJManager
        { get; set; }
    }

    public enum SplitJoinOperation
    {
        SplitOneInTwo,
        SplitOneInFour,
        SplitOneInSixteen,
        JoinTwoInOne,
        JoinFourInOne,
        JoinSixteenInOne,
    }

    public class SJDataWizardManager
    {
        StringCollection m_ListSourceData = new StringCollection();
        StringCollection m_ListDestData = new StringCollection();

        BTDoc m_Document = null;

        SplitJoinOperation m_SplitJoinOp = SplitJoinOperation.SplitOneInTwo;

        int m_iMaxSrcSize = 16;
        int m_iMinSrcSize = 2;
        int m_iRequiredSelectionCount = 0;
        int m_iDestSize = 0;

        public SJDataWizardManager(BTDoc doc)
        {
            m_Document = doc;
        }

        public BTDoc Document
        {
            get { return m_Document; }
        }

        public SplitJoinOperation SplitJoinOp
        {
            get { return m_SplitJoinOp; }
            set 
            { 
                if (m_SplitJoinOp == value)
                    return;
                m_SplitJoinOp = value;
                m_ListSourceData.Clear();
                m_ListDestData.Clear();
                UpdateSourceSizeFromOp();
            }
        }

        public StringCollection ListSourceData
        {
            get { return m_ListSourceData; }
            set { m_ListSourceData = value; }
        }

        public StringCollection ListDestData
        {
            get { return m_ListDestData; }
            set { m_ListDestData = value; }
        }

        public int MaxSrcSize
        { get { return m_iMaxSrcSize; } }

        public int MinSrcSize
        { get { return m_iMinSrcSize; } }

        public int RequiredSelectionCount
        { get { return m_iRequiredSelectionCount; } }

        public int DestSize
        { get { return m_iDestSize; } set { m_iDestSize = value; } }


        private void UpdateSourceSizeFromOp()
        {
            switch (m_SplitJoinOp)
            {
                case SplitJoinOperation.SplitOneInTwo:
                    m_iMaxSrcSize = 16;
                    m_iMinSrcSize = 2;
                    m_iRequiredSelectionCount = 1;
                    break;
                case SplitJoinOperation.SplitOneInFour:
                    m_iMaxSrcSize = 16;
                    m_iMinSrcSize = 4;
                    m_iRequiredSelectionCount = 1;
                    break;
                case SplitJoinOperation.SplitOneInSixteen:
                    m_iMaxSrcSize = 16;
                    m_iMinSrcSize = 16;
                    m_iRequiredSelectionCount = 1;
                    break;
                case SplitJoinOperation.JoinTwoInOne:
                    m_iMaxSrcSize = 8;
                    m_iMinSrcSize = 1;
                    m_iRequiredSelectionCount = 2;
                    break;
                case SplitJoinOperation.JoinFourInOne:
                    m_iMaxSrcSize = 4;
                    m_iMinSrcSize = 1;
                    m_iRequiredSelectionCount = 4;
                    break;
                case SplitJoinOperation.JoinSixteenInOne:
                    m_iMaxSrcSize = 1;
                    m_iMinSrcSize = 1;
                    m_iRequiredSelectionCount = 16;
                    break;
            }
        }
    }
}
