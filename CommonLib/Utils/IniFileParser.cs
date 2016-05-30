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
/***************************************************************************/
// PROJET : BTCommand : system de commande paramétrable pour équipement
// ayant une mécanisme de commande par liaison série/ethernet/http
/***************************************************************************/
// Fichier : 
/***************************************************************************/
// description :
// 
/***************************************************************************/
using System; 
using System.IO; 
using System.Text; 
using System.Collections;

namespace CommonLib
{
    /// <summary>
    /// Cette classe permet d'utiliser un fichier ini. 
    /// </summary>
    public class IniFileParser
    {
        private Hashtable Sections = new Hashtable(); 
        private string sFileName; 
  
        private const string newline = "\r\n"; 
  
        public IniFileParser (){} 
  
        /// <summary> 
        /// Crée une nouvelle instance de IniFile et charge le fichier ini 
        /// </summary> 
        /// <param name="fileName">Chemin du fichier ini</param> 
        public IniFileParser (string fileName) 
        { 
            sFileName = fileName; 
  
            if (File.Exists(fileName)) 
                Load(fileName); 
        }

        public void ClearAll()
        {
            // facile hihi
            Sections = new Hashtable();
        }
  
        /// <summary> 
        /// Ajoute une section [section] au fichier ini 
        /// </summary> 
        /// <param name="section">Nom de la section à créer</param> 
        public void AddSection (string section) 
        { 
            if (!Sections.ContainsKey(section)) 
                Sections.Add(section,new Section()); 
        } 
  
        /// <summary> 
        /// Ajoute une section [section] au fichier ini ainsi qu'une clef et une valeur 
        /// </summary> 
        /// <param name="section">Nom de la section</param> 
        /// <param name="key">Nom de la clef</param> 
        /// <param name="value">Valeur de la clef</param> 
        public void AddSection (string section, string key, string value) 
        { 
            AddSection (section); 
            ((Section)Sections[section]).SetKey(key,value); 
        } 
  
        /// <summary> 
        /// Retire une section du fichier 
        /// </summary> 
        /// <param name="section">Nom de la section à enlever</param> 
        public void RemoveSection (string section) 
        { 
            if (Sections.ContainsKey(section)) 
                Sections.Remove(section); 
        } 
  
        /// <summary> 
        /// Modifie ou crée une valeur d'une clef dans une section 
        /// </summary> 
        /// <param name="section">Nom de la section</param> 
        /// <param name="key">Nom de la clef</param> 
        /// <param name="value">Valeur de la clef</param> 
        public void SetValue (string section, string key, string value) 
        {
            if (!Sections.Contains(section))
            {
                this.AddSection(section);
            }
            this[section].SetKey(key,value); 
        } 
  
        /// <summary> 
        /// Retourne la valeur d'une clef dans une section 
        /// </summary> 
        /// <param name="section">Nom de la section</param> 
        /// <param name="key">Nom de la clef</param> 
        /// <param name="defaut">Valeur par défaut si la clef/section n'existe pas</param> 
        /// <returns>Valeur de la clef, ou la valeur entrée par défaut</returns> 
        public string GetValue (string section, string key, object defaut) 
        {
            if (this[section] != null)
            {
                string val = this[section][key];
                if (val == "")
                {
                    this[section][key] = defaut.ToString();
                    return defaut.ToString();
                }
                else
                    return val;
            }
            else
                return defaut.ToString();
        } 
  
        /// <summary> 
        /// Retourne la valeur d'une clef dans une section 
        /// </summary> 
        /// <param name="section">Nom de la section</param> 
        /// <param name="key">Nom de la clef</param> 
        /// <returns>Valeur de la clef, ou "" si elle n'existe pas</returns> 
        public string GetValue (string section, string key) 
        { 
            return GetValue (section,key,""); 
        } 
  
        // Indexeur des sections 
        private Section this [string section] 
        { 
            get 
            {
                if (!Sections.ContainsKey(section))
                    return null;
  
                return (Section) Sections[section]; 
            } 
            set 
            { 
                if (!Sections.ContainsKey(section)) 
                    AddSection(section); 
                Sections[section] = value; 
            } 
        } 
  
        /// <summary> 
        /// Sauvegarde le fichier INI en cours 
        /// </summary> 
        public void Save () 
        { 
            if (sFileName != "") 
                Save (sFileName); 
        } 
  
        /// <summary> 
        /// Sauvegarde le fichier INI sous un nom spécifique 
        /// </summary> 
        /// <param name="fileName">Nom de fichier</param> 
        public void Save (string fileName) 
        {
            fileName = PathTranslator.LinuxVsWindowsPathUse(fileName);
            StreamWriter str = new StreamWriter(fileName, false); 
  
            foreach (object okey in Sections.Keys) 
            { 
                str.Write("["+okey.ToString()+"]"+newline); 
  
                Section sct = (Section)Sections[okey.ToString()]; 
  
                foreach (string key in (sct.Keys)) 
                { 
                    str.Write(key+"="+sct[key]+newline); 
                } 
            } 
  
            str.Flush(); 
            str.Close(); 
        } 
  
        /// <summary> 
        /// Charge un fichier INI 
        /// </summary> 
        /// <param name="fileName">Nom du fichier à charger</param> 
        public void Load (string fileName) 
        {
            fileName = PathTranslator.LinuxVsWindowsPathUse(fileName);
            Sections = new Hashtable(); 
  
            StreamReader str = new StreamReader(File.Open(fileName,FileMode.OpenOrCreate)); 
  
            string fichier = str.ReadToEnd(); 
  
            string[] lignes = fichier.Split('\r','\n'); 
  
            string currentSection = ""; 
  
            for (int i=0 ; i<lignes.Length ; i++) 
            { 
                string ligne = lignes[i]; 
                
  
                if (ligne.StartsWith("[") && ligne.EndsWith("]")) 
                { 
                    currentSection = ligne.Substring(1,ligne.Length-2); 
                    AddSection(currentSection); 
                } 
                else if (ligne != "") 
                { 
                    //char[] ca = new char [1] {'='};
                    string[] scts = ligne.Split('=');
                    if (this[currentSection] != null && scts.Length>1)
                        this[currentSection].SetKey(scts[0],scts[1]); 
                } 
            } 
            this.sFileName = fileName; 
  
            str.Close(); 
        } 
  
        // Structure de donnée des sections 
        private class Section 
        { 
  
            private Hashtable clefs = new Hashtable(); 
  
            public Section (){} 
  
            /// <summary> 
            /// Affecte une valeur à une clef et la crée si elle n'existe pas 
            /// </summary> 
            /// <param name="key">Nom de la clef</param> 
            /// <param name="value">Valeur de la clef</param> 
            public void SetKey (string key, string value) 
            { 
                if (key.IndexOf("=") > 0) 
                    throw new Exception("Caractère '=' interdit"); 
  
                if (clefs.ContainsKey(key)) 
                    clefs[key] = value; 
                else 
                    clefs.Add(key,value); 
            } 
  
            /// <summary> 
            /// Supprime une clefs 
            /// </summary> 
            /// <param name="key">Nom de la clef à supprimer</param> 
            public void DeleteKey (string key) 
            { 
                if (clefs.ContainsKey(key)) 
                    clefs.Remove(key); 
            } 
  
            /// <summary> 
            /// Les clefs contenues dans la section 
            /// </summary> 
            public ICollection Keys 
            { 
                get 
                { 
                    return clefs.Keys; 
                } 
            } 
  
            /// <summary> 
            /// Indexeur des clefs 
            /// </summary> 
            public string this [string key] 
            { 
                get 
                { 
                    if (clefs.ContainsKey(key)) 
                        return clefs[key].ToString(); 
                    else 
                    { 
                        SetKey (key,""); 
                        return ""; 
                    } 
                         
                } 
                set 
                { 
                    SetKey(key,value); 
                } 
            } 
        } 
    }
}
