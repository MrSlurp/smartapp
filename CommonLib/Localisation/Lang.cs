﻿/*========================================================================== 
 *  FILE:   
 *                   Lang.cs
 * 
 *  PROJECT:
 *                   VIDMID
 * 
 * La description du format des fichiers PO est disponible sur le site :
 * 
 *      http://www.gnu.org/software/autoconf/manual/gettext/PO-Files.html
 * 
 * =========================================================================
 *                   ANSALDO STS France - Copyright © 2009
 * ========================================================================= */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace CommonLib
{
    /// <summary>
    /// Classe utiliser pour localiser un assembly
    /// 
    /// Manuel d'utilisation :
    /// Dans l'exécutable de base (ou maitre) utiliser l'initialiseur :
    ///         Lang.Initialize(this, "Langue de développement", "Langue localisée")
    /// juste avant InitializeComponent();
    /// ex: Lang.Initialize(this, "FR", "US")
    ///   ---> utilisera le fichier US.PO   de dirOfExe\Lang\
    /// 
    /// Dans les autres les autres feuilles faire :
    ///     Lang.Initialize(this) --> cela déclare toutes les IHM
    /// 
    /// dans tous les .cs , utiliser Lang.C("texte à localiser)
    /// pour remplacer le texte en paramètre par sa traduction
    /// 
    /// 08/09/2008 : MGL
    /// </summary>
    public class Lang
    {
        static Lang m_SingLangSys;
        public static void InitCommonLibLang(string DevLang, string CurLang)
        {
#if BUILD_LANG
#if TEST_LANG
            m_SingLangSys = new Lang(true, true);
#else
            m_SingLangSys = new Lang(true, false);
#endif
#else
            m_SingLangSys = new Lang();
#endif
            m_SingLangSys.Initialize(DevLang, CurLang, "CommonLib");
        }
        public static Lang LangSys
        {
            get { return m_SingLangSys; }
        }

        // chaine ajoutée aux résultat si la chaine n'est pas trouvée
        const string MSG_MISSING = @"//**\\";

        const string LANG_DIRECTORY_NAME = "Lang";

        private string m_CurrentAssembly;

        // language demandé
        private string mCurrentLangage = "FR";
        public string CurrentLangage { get { return mCurrentLangage; } }
	
        // language utilisé pour le développement 
        private string mDevLangage = "FR";
        
        // stocke la liste des feuilles localisées
        private List<FormInfo> mFormList = new List<FormInfo>();
        private List<UserControlInfo> mUserControlList = new List<UserControlInfo>();
        
        // contient les dictionnaires de chaque assembly
        private Dictionary<string, Dictionary<string, string>> Decoders = new Dictionary<string, Dictionary<string, string>>();
        
        // variable contenant true si on veut que l'item soit créé s'il manque
        private bool mCreateOnMissingItem = false;
        public bool CreateOnMissingItem
        {
            get { return mCreateOnMissingItem; }
            set { mCreateOnMissingItem = value; }
        }

        // affiche MSG_MISSING à la fin du texte si on ne l'a pas trouvé
        private bool mInformOfMissingItem = false;
        public bool InformOfMissingItem
        {
            get { return mInformOfMissingItem; }
            set { mInformOfMissingItem = value; }
        }

        public Lang(bool CreateMissings, bool InformMissings)
        {
            mInformOfMissingItem = InformMissings;
            mCreateOnMissingItem = CreateMissings;
            // charge la tables de traduction des opcodes IL
            Globals.LoadOpCodes();
        }

        public Lang()
        {
            // charge la tables de traduction des opcodes IL
            Globals.LoadOpCodes();
        }

        /// <summary>
        /// Initialisation appelée par les esclaves
        /// </summary>
        /// <param name="frm">feuille de l'appelant</param>
        public void Initialize(Form frm)
        {
#if !PocketPC
            Debug.Print("Lang::Initialize({0})",frm.Name);
#endif
            
#if !PocketPC
            frm.FormClosed += Form_Closed;
#else
            frm.Closed += Form_Closed;
#endif
            mFormList.Add(FormInfo.CreateFormInfo(this, frm));
        }

        public void Initialize(UserControl frm)
        {
            Debug.Print("Lang::Initialize({0})", frm.Name);
            mUserControlList.Add(UserControlInfo.CreateUserControlInfo(this, frm));
        }

        /// <summary>
        /// Initialisation appelée par le maitre (qui défini le langage)
        /// </summary>
        /// <param name="frm">feuille de l'appelant</</param>
        /// <param name="DevLang">Langue utilisée pour le développement</param>
        /// <param name="CurLang">Langue demandée</param>
        public void Initialize(Form frm, string DevLang, string CurLang, string Assembly)
        {
#if !PocketPC
            Debug.Print("Lang::Initialize({0},{1},{2})",frm.Name,DevLang,CurLang);
#endif
            mFormList.Clear();
            Decoders.Clear();
            m_CurrentAssembly = Assembly;
            mCurrentLangage = CurLang.ToUpper();
            mDevLangage = DevLang.ToUpper();
            if (frm != null)
            {
                Form_Closed(frm, null); // décharge la feuille
                //réinitialise
                Initialize(frm);
            }
        }

        /// <summary>
        /// Initialisation appelée par le maitre (qui défini le langage)
        /// </summary>
        /// <param name="frm">feuille de l'appelant</</param>
        /// <param name="DevLang">Langue utilisée pour le développement</param>
        /// <param name="CurLang">Langue demandée</param>
        public void Initialize(string DevLang, string CurLang, string Assembly)
        {
#if !PocketPC
            Debug.Print("Lang::Initialize({0},{1})", DevLang, CurLang);
#endif
            mFormList.Clear();
            Decoders.Clear();
            m_CurrentAssembly = Assembly;
            mCurrentLangage = CurLang.ToUpper();
            mDevLangage = DevLang.ToUpper();
        }

        public void Initialize(UserControl frm, string DevLang, string CurLang, string Assembly)
        {
#if !PocketPC
            Debug.Print("Lang::Initialize({0},{1},{2})", frm.Name, DevLang, CurLang);
#endif
            mFormList.Clear();
            Decoders.Clear();
            m_CurrentAssembly = Assembly;
            mCurrentLangage = CurLang.ToUpper();
            mDevLangage = DevLang.ToUpper();
            Form_Closed(frm, null); // décharge la feuille
            //réinitialise
            Initialize(frm);
        }
        /// <summary>
        /// Permet de changer la langue affichée
        /// </summary>
        /// <param name="Langage"></param>
        public void ChangeLangage(string Langage)
        {
            mCurrentLangage = Langage;
            mInformOfMissingItem = false;
            mCreateOnMissingItem = false;
            foreach (FormInfo frmi in mFormList)
            {
                frmi.Form_Load(null, null);
            }
            //mInformOfMissingItem = false;
        }
        
        // Evènement appelé lorsqu'un feuille est déchargée
        private void Form_Closed(object sender, System.EventArgs e)
        {
            // recherche la feuille dans la liste des feuille interceptées
            Form frm = (Form)sender;
#if !PocketPC
            Debug.Print("Lang::Form_Closed --> " + sender.GetType().ToString());
#endif
            for (int index = 0; index < mFormList.Count; index++)
            {
                if (mFormList[index].FormName == frm.Name)
                {
                    // supprime la feuille de la liste
                    mFormList.RemoveAt(index);
                    return; // break marche aussi ! ! ! pour les puristes mais j'aime pas les trucs en iste
                }
            } 
        }

        static public string C(string DevText, Lang LangSys)
        {
            // trouve le fichier de langue
#if !PocketPC
            string FilePath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
#else
            string FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
#endif
            FilePath = Path.Combine(FilePath, LANG_DIRECTORY_NAME);
            //FilePath = Path.Combine(FilePath, mCurrentLangage + ".po");
            if (string.IsNullOrEmpty(DevText))
                return string.Empty;
            else
                return LangSys.C(FilePath, DevText);
        }

        /// <summary>
        /// Retourne le texte localisé de l'appelant
        /// </summary>
        /// <param name="DevText">Clé à localiser</param>
        /// <returns>Texte localisé</returns>
        public string C(string DevText)
        {
            // trouve le fichier de langue
#if !PocketPC
            string FilePath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
#else
            string FilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
#endif
            FilePath = Path.Combine(FilePath, LANG_DIRECTORY_NAME);
            //FilePath = Path.Combine(FilePath, mCurrentLangage + ".po");
            if (string.IsNullOrEmpty(DevText))
                return string.Empty;
            else
                return C(FilePath ,DevText );
        }
        
        /// <summary>
        /// Retourne le texte traduit de la clé demandée
        /// </summary>
        /// <param name="FilePath">Nom du fichier de traduction</param>
        /// <param name="DevText">Clé de recherche</param>
        /// <returns>Texte localisé</returns>
        public string C(string FilePath,string DevText)
        {
            string FileName;
            if (string.IsNullOrEmpty(m_CurrentAssembly))
                FileName = Path.Combine(FilePath, mCurrentLangage + ".po");
            else
                FileName = Path.Combine(FilePath, mCurrentLangage + "." + m_CurrentAssembly + ".po");

            if (Decoders.ContainsKey(FileName) != true)
            {
                LoadLangage(FilePath);
            }

            if (Decoders.ContainsKey(FileName) == true)
            {
                if (Decoders[FileName].ContainsKey(DevText) == true)
                {
                    if (Decoders[FileName][DevText] != "")
                        return Decoders[FileName][DevText];
                    else
                    {
                        if (mInformOfMissingItem)
                            return (DevText + MSG_MISSING);
                        else
                            return DevText;
                    }
                }
                else if (DevText.EndsWith(MSG_MISSING))
                {
                    string mDevText = DevText.TrimEnd(MSG_MISSING.ToCharArray());
                    if (Decoders[FileName].ContainsKey(mDevText) == true)
                    {
                        if (Decoders[FileName][mDevText] != "")
                            return Decoders[FileName][mDevText];
                        else
                        {
                            if (mInformOfMissingItem)
                                return (mDevText + MSG_MISSING);
                            else
                                return mDevText;
                        }
                    }
                }
            }

            //Le fichier n'existe (peut-être) pas dans le dictionary
            // la chaine n'existe pas dans le fichier
            if (mCreateOnMissingItem)
            {
                CreateLinesOnFile(FileName, DevText);
            }

            if (mInformOfMissingItem)
            {
                return (DevText + MSG_MISSING);
            }
            else
            {
                return DevText;
            }
        }

        /// <summary>
        /// Ajoute une clé/valuer au fichier de langue
        /// </summary>
        /// <param name="FilePath">nom du fichier</param>
        /// <param name="DevText">texte à ajouter</param>
        private void CreateLinesOnFile(string FileName, string DevText)
        {
            //string FileName = Path.Combine(FilePath, mCurrentLangage + ".po"); 

            if (Directory.Exists(Path.GetDirectoryName(FileName)) == false)
            {
                //Le directory n'existe pas
                Directory.CreateDirectory(Path.GetDirectoryName(FileName) );
            }
            
            // la chaine n'existe pas dans le fichier
            using (FileStream fsw = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.Read ))
            {
                
                StreamWriter sw = new StreamWriter(fsw, System.Text.Encoding.Default   );
                //ecriture du message
                sw.WriteLine();
                sw.WriteLine("msgid \"" + DevText + "\"");
                string curlng = "";
                if (mDevLangage   == mCurrentLangage)
                    curlng = DevText;

                sw.WriteLine("msgstr \"" + curlng + "\"");
                //sw.WriteLine();
                sw.Close();
                fsw.Close();
            }

            // ajout de la chaine dans le dictionnaire
            AddLineToDictionnary(FileName, DevText, DevText);
        }

        /// <summary>
        /// Charge un fichier de langue dans le dictionnaire
        /// </summary>
        /// <param name="FilePath"></param>
        public void LoadLangage(string FilePath)
        {
            string FileName;
            if (string.IsNullOrEmpty(m_CurrentAssembly))
                FileName = Path.Combine(FilePath, mCurrentLangage + ".po"); 
            else
                FileName = Path.Combine(FilePath, mCurrentLangage + "." + m_CurrentAssembly + ".po"); 
            
            if (Decoders.ContainsKey(FileName) == false)
            {
                if (File.Exists(FileName))
                {
                    using (FileStream fsw = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        StreamReader sw = new StreamReader(fsw, System.Text.Encoding.Default );
                        string Cur_msgid = "";
                        do
                        {
                            string Line = sw.ReadLine();
                            if (string.IsNullOrEmpty(Line))
                                Line = string.Empty;
                            else
                                Line = Line.Trim();

                            if (Line.ToLower().StartsWith ("msgid"))
                            {
                                string[] txt = Line.Split('"'  );
                                if (txt.GetLength(0) == 3)
                                {
                                    Cur_msgid = txt[1];
                                }

                            }
                            else if (Line.ToLower().StartsWith("msgstr"))
                            {
                                string[] txt = Line.Split('"');
                                if (txt.GetLength(0) == 3)
                                {
                                    AddLineToDictionnary(FileName, Cur_msgid, txt[1]);
                                }
                            }
                            else
                            {
                                //on ne fait rien , ligne info ou autre
                            }

                        } while (sw.EndOfStream == false);
                        sw.Close();
                        fsw.Close();
                    }
                }
            }
        }

        /// <summary>
        /// ajoute ou met à jour une string au dictionnaire
        /// </summary>
        /// <param name="FilePath">Nom du fichier language concerné (sortie)</param>
        /// <param name="ID">Clé de recherche =  texte dans la langue d'origine</param>
        /// <param name="Value">Texte dans la langue de sortie</param>
        private void AddLineToDictionnary(string FileName,string ID, string Value)
        {
            //string FileName = Path.Combine(FilePath, mCurrentLangage + ".po"); 

            if (Decoders.ContainsKey(FileName) == false)
            {
                Decoders.Add(FileName, new Dictionary<string, string>());
            }

            if (Decoders[FileName].ContainsKey(ID) == false)
            {
                Decoders[FileName].Add(ID, Value);
            }
            else
            {
                Decoders[FileName][ID] = Value;
            }

        }

        /// <summary>
        /// Calcule le nom du fichier language à partir d'une form
        /// </summary>
        /// <param name="frm">objet System.Windows.Form</param>
        /// <returns>nom complet du fichier de langue</returns>
        //public static string CreateFileName(Form frm)
        //{
        //    string FilePath = Path.GetDirectoryName(Assembly.GetAssembly(frm.GetType()).Location);
        //    FilePath = Path.Combine(FilePath, LANG_DIRECTORY_NAME);
        //    FilePath = Path.Combine(FilePath, mCurrentLangage + ".po");
        //    return FilePath;
        //}

        /// <summary>
        /// Calcule le directory du fichier language à partir d'une form
        /// </summary>
        /// <param name="frm">objet System.Windows.Form</param>
        /// <returns>nom complet du fichier de langue</returns>
        public string CreateFilePath(Form frm)
        {
#if !PocketPC
            string FilePath = Path.GetDirectoryName(Assembly.GetAssembly(frm.GetType()).Location);
#else
            string FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
#endif
            FilePath = Path.Combine(FilePath, LANG_DIRECTORY_NAME);
            return FilePath;
        }

        public string CreateFilePath(UserControl frm)
        {
#if !PocketPC
            string FilePath = Path.GetDirectoryName(Assembly.GetAssembly(frm.GetType()).Location);
#else
            string FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
#endif
            FilePath = Path.Combine(FilePath, LANG_DIRECTORY_NAME);
            return FilePath;
        }

        public static string CreateFilePath()
        {
#if !PocketPC
            string FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
#else
            string FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
#endif
            FilePath = Path.Combine(FilePath, LANG_DIRECTORY_NAME);
            return FilePath;
        }

        /// <summary>
        /// Parcourt le code de l'assembly associé à la feuille
        /// </summary>
        /// <param name="frm">Form contenue dans l'assembly concernée</param>
        public void GetCode_LangC(Form frm)
        {
#if !PocketPC
            // stocke les méthodes trouvée
            List<MethodInfo> methods = new List<MethodInfo>();
            
            // stocke les chaines trouvées
            List<string> LangC = new List<string>();
            
            // Recherche de toutes les méthodes de l'assembly
            try
            {
                // load the assembly
                Assembly ass = Assembly.GetAssembly(frm.GetType ());

                // get all the methods within the loaded assembly
                Module[] modules = ass.GetModules();
                for (int i = 0; i < modules.Length; i++)
                {
                    Type[] types = modules[i].GetTypes();
                    for (int k = 0; k < types.Length; k++)
                    {
                        MethodInfo[] mis = types[k].GetMethods( BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                        for (int j = 0; j < mis.Length; j++)
                        {
                            // check if the method has a body
                            // ne charge pas les méthode InitializeComponent (inutile)
                            if ((mis[j].GetMethodBody() != null) && mis[j].Name !="InitializeComponent")
                            {
                                methods.Add(mis[j]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                throw ex;
            }


            // parcourt toutes les méthodes pour trouver les Lang.C()
            for (int index = 0; index < methods.Count; index++)
            {
                try
                {
                    // recupère la méthode
                    MethodInfo mi = methods[index];
                    // initialise un convertisseur de code IL byte en texte
                    MethodBodyReader mr = new MethodBodyReader(mi);
                    // récupère le code IL de la méthode limité au call qui nous intéresse
                    string Value = mr.GetBodyCode("ldstr", "lang::c()");
                    string[] lines = Value.Split(new string[] { Environment.NewLine } , StringSplitOptions.None);
                    if (Value.Contains("ldstr") == true)
                    {
                        foreach (string var in lines)
                        {
                            if (var.Contains("ldstr") == true)
                            {
                                // on a trouvé un appel à la fonction Lang.C
                                LangC.Add(var.Split('"')[1]);
                                //ret += var.Split('"')[1] + Environment.NewLine;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Traces.LogAdd(Traces.LOG_LEVEL_ERROR, "Lang", ex.Message);
                }
            }

            // retrouve le path de l'assembly
            string FilePath = CreateFilePath(frm);

            // demande la traduction des textes trouvés
            // qui par effet de bord les créera s'ils manques
            foreach (string str in LangC )
            {
                C(FilePath, str);
            }
#endif
        }

        public void GetCode_LangC(UserControl frm)
        {
#if !PocketPC
            // stocke les méthodes trouvée
            List<MethodInfo> methods = new List<MethodInfo>();

            // stocke les chaines trouvées
            List<string> LangC = new List<string>();

            // Recherche de toutes les méthodes de l'assembly
            try
            {
                // load the assembly
                Assembly ass = Assembly.GetAssembly(frm.GetType());

                // get all the methods within the loaded assembly
                Module[] modules = ass.GetModules();
                for (int i = 0; i < modules.Length; i++)
                {
                    Type[] types = modules[i].GetTypes();
                    for (int k = 0; k < types.Length; k++)
                    {
                        MethodInfo[] mis = types[k].GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                        for (int j = 0; j < mis.Length; j++)
                        {
                            // check if the method has a body
                            // ne charge pas les méthode InitializeComponent (inutile)
                            if ((mis[j].GetMethodBody() != null) && mis[j].Name != "InitializeComponent")
                            {
                                methods.Add(mis[j]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                throw ex;
            }


            // parcourt toutes les méthodes pour trouver les Lang.C()
            for (int index = 0; index < methods.Count; index++)
            {
                try
                {
                    // recupère la méthode
                    MethodInfo mi = methods[index];
                    // initialise un convertisseur de code IL byte en texte
                    MethodBodyReader mr = new MethodBodyReader(mi);
                    // récupère le code IL de la méthode limité au call qui nous intéresse
                    string Value = mr.GetBodyCode("ldstr", "lang::c()");
                    string[] lines = Value.Split(new string[] { Environment.NewLine } , StringSplitOptions.None);
                    if (Value.Contains("ldstr") == true)
                    {
                        foreach (string var in lines)
                        {
                            if (var.Contains("ldstr") == true)
                            {
                                // on a trouvé un appel à la fonction Lang.C
                                LangC.Add(var.Split('"')[1]);
                                //ret += var.Split('"')[1] + Environment.NewLine;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Traces.LogAdd(Traces.LOG_LEVEL_ERROR, "Lang", ex.Message);
                }
            }

            // retrouve le path de l'assembly
            string FilePath = CreateFilePath(frm);

            // demande la traduction des textes trouvés
            // qui par effet de bord les créera s'ils manques
            foreach (string str in LangC)
            {
                C(FilePath, str);
            }
#endif
        }
    }
}
