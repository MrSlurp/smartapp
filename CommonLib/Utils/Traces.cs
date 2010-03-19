using System;
using System.IO ;


namespace CommonLib
{
	/// <summary>
	/// Description résumée de Traces.
	/// </summary>
	public enum TracesLevel
	{
		NoLog    = 0, // aucune trace
		Critical = 1, // Erreures critiques
		Error    = 2, // erreur non critique
		Warning  = 3, // warning 
		Info     = 4, // message d'info
		Debug    = 5, // message de deboggage
    }

    [Flags]
    public enum TraceCat
    {
        // aucune trace
        None = 0x00000000,
        // traces du parser
        Parser = 0x00000001,
        // traces de l'executer
        Executer = 0x00000002,
        // traces du système de lang
        Lang = 0x00000004,
        // traces de smart config
        SmartConfig = 0x00000008,
        // traces de smartcommand
        SmartCommand = 0x00000010,
        // traces diver commonlib (gestionnaire, objets de bases, etc))
        CommonLib = 0x00000020,
        // traces de la sérialisation
        Serialization = 0x00000040,
        // traces des plugins
        Plugin = 0x00000080,
        // traces de la partir comm
        Communication = 0x00000100,
        // traces BTDoc
        Document = 0x00000200,
        // traces de l'éditeur de script
        ScriptEditor = 0x00000400,
        // traces de performances
        PerfChrono = 0x00000800,
        // 8 places libres ici
        //0x00001000
        //0x00002000
        //0x00004000
        //0x00008000
        //0x00010000
        //0x00020000
        //0x00040000
        //0x00080000
        
        // traces autres
        Others = 0x00080000,
        
        LogDispCats = 0x000FFFFF,
        
       
        // sous catégories de l'execution
        // permet de controler l'execution d'un type de fonction
        // en particulier 
        ExecuteFrame = 0x00100000 | Executer,
        ExecuteMath = 0x00200000 | Executer,
        ExecuteLogic = 0x00400000 | Executer, 
        ExecuteFunc = 0x00800000 | Executer,
        ExecuteScreen = 0x01000000 | Executer,
        ExecuteTimer = 0x02000000 | Executer, 
        ExecuteLogger = 0x04000000 | Executer,
        // je garde de la marge pour les futures fonction de script (4 places libres)
        //0x08000000
        //0x10000000
        //0x20000000
        //0x40000000
        ExecuteAll = 0x7FF00002, 
        ExecuteAllButFrame = ExecuteAll | ~ExecuteFrame,    
        // toutes traces actives
        All = 0x7FFFFFFF,  
    }
    
	public class Traces
	{
		// Taille maximale de la chaine 1 du fichier de traces
		private const int TAILLEMAX_CAT	= 20;
		
		// Nombre de jours de logs conservés
		public const int NBR_JOURS = 2 ;

		// Répertoire dans lequel les logs doivent être écrits
		private static string sLogDirectory ;
		// Nom du fichier de traces
		private static string sLogFileName ;
		// Niveau de trace demandé
        private static TracesLevel mTracesLevel = TracesLevel.Debug;
		// Fichier de logs
		//private static StreamWriter TxtLog;
		
		private static TraceCat mTraceCat = TraceCat.All;

        private static bool m_bTraceToFile = false;

		// Protection des fonctions de traces avec une section critique
		// Cette fonctionalité est rendue nécessaire par le fait que plusieurs Threads
		// sont susceptibles d'appeler les fonctions de trace de la même instance de l'objet.
		// La section critique est définie comme un membre privé de l'objet et protège 
		// les parties de code donnant accès au système de fichier
		private static Object oSectionCritique = new Object();

		// Constructeur de la classe
		static Traces()
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sDirectory"></param>
        /// <param name="sFileName"></param>
        /// <param name="Level"></param>
		public static void Initialize(string sDirectory,string sFileName,TracesLevel Level)
		{
		    Initialize(sDirectory,sFileName,Level, TraceCat.All, false);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sDirectory"></param>
        /// <param name="sFileName"></param>
        /// <param name="Level"></param>
        /// <param name="cat"></param>
        public static void Initialize(string sDirectory, string sFileName, TracesLevel Level, TraceCat cat, bool bToFile)
		{
			sLogDirectory = sDirectory;
			sLogFileName = sFileName;
			mTracesLevel = Level;
			mTraceCat = cat;
            m_bTraceToFile = bToFile;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogLevelTest"></param>
        /// <returns></returns>
        public static bool IsLogLevelOK(TracesLevel Level)
        {
            if (Level <= mTracesLevel)
            {
                return true;
            }
            return false;
        }
        
        public static bool IsCatOK(TraceCat cat)
        {
            if ((cat & mTraceCat) == cat)
            {
                return true;
            }
            return false;
        }
    
        public static bool IsLevelAndCatOK(TracesLevel Level, TraceCat cat)
        {
            if (IsLogLevelOK(Level) && IsCatOK(cat))
                return true;
                
            return false;
        }

        public static bool IsDebugAndCatOK(TraceCat cat)
        {
            if (IsLogLevelOK(TracesLevel.Debug) && IsCatOK(cat))
                return true;
                
            return false;
        }
        
		// Routine permettant de déterminer le répertoire dans lequel va se trouver
		// le fichier de Logs. Celui-ci est construit avec les paramètres du constructeur
		// plus un répertoire du jour : ex : C:\Repertoire\20071023\
		// Cette routine est protégée par la section critique de la procédure qui l'appelle
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sRepertoire"></param>
        /// <returns></returns>
		private static bool LogDirectory(out string sRepertoire)
		{
			string sRepDuJour ;
			DateTime oDHSysteme;
			bool bRetour = true ;
			
			// On récupère l'heure système
			oDHSysteme = DateTime.UtcNow ;
			
			// On va utiliser un répertoire de Logs dont le nom est la date du jour
			sRepDuJour = oDHSysteme.ToString("yyyyMMdd") ;

			// Si le répertoire n'existe pas, on le créé
			try
			{
				if(! Directory.Exists(PathTranslator.LinuxVsWindowsPathUse(sLogDirectory + @"\" + sRepDuJour)))
				{
					Directory.CreateDirectory(PathTranslator.LinuxVsWindowsPathUse(sLogDirectory + @"\" + sRepDuJour));
					
					// Si le répertoire a été créé, on 
					LogPurge(NBR_JOURS) ;
				}
			}
			catch
			{	// On a rencontré une exception
				bRetour = false ;
			}
			finally	
			{
				sRepertoire = PathTranslator.LinuxVsWindowsPathUse(sLogDirectory + @"\" + sRepDuJour + @"\");	
			}
			
			return bRetour ;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sMessage1"></param>
        /// <param name="sMessage2"></param>
        /// <returns></returns>
        public static bool LogAddDebug(TraceCat cat,string sMessage1)
        {
            return LogAdd(TracesLevel.Debug, cat, sMessage1);
        }

        public static bool LogAddDebug(TraceCat cat,string sMessage1, string sMessage2)
        {
            return LogAdd(TracesLevel.Debug, cat, sMessage1, sMessage2);
        }


        public static bool LogAddInfo(TraceCat cat,string sMessage1)
        {
            return LogAdd(TracesLevel.Info, cat, sMessage1);
        }
        public static bool LogAddInfo(TraceCat cat,string sMessage1, string sMessage2)
        {
            return LogAdd(TracesLevel.Info, cat, sMessage1, sMessage2);
        }

        public static bool LogAddWarning(TraceCat cat,string sMessage1, string sMessage2)
        {
            return LogAdd(TracesLevel.Warning, cat, sMessage1, sMessage2);
        }

        public static bool LogAddError(TraceCat cat,string sMessage1, string sMessage2)
        {
            return LogAdd(TracesLevel.Error, cat, sMessage1, sMessage2);
        }

        public static bool LogAddCritical(TraceCat cat,string sMessage1, string sMessage2)
        {
            return LogAdd(TracesLevel.Critical, cat, sMessage1, sMessage2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iTracesLevel"></param>
        /// <param name="sMessage1"></param>
        /// <returns></returns>
        public static bool LogAdd(TracesLevel Level, TraceCat cat, string sMessage1)
        {
            return LogAdd(Level, cat, sMessage1, "");
        }

		// Routine permettant d'ajouter une trace au fichier de logs
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iTracesLevel"></param>
        /// <param name="sMessage1"></param>
        /// <param name="sMessage2"></param>
        /// <returns></returns>
		public static bool LogAdd(TracesLevel Level, TraceCat cat, string sMessage1, string sMessage2)
		{
			string sFileName ;
			DateTime oDHSysteme;
			bool bRetour = true ;
			
			// On n'enregistre la trace que si le niveau est supérieur ou égal
			// au niveau demandé lors de l'initialisation
			// et dont les catégories sont actives
			if(IsLevelAndCatOK(Level ,cat))
			{
				// On récupère l'heure système en UTC
				oDHSysteme = DateTime.UtcNow ;
				
				// On protège l'accès au système de fichiers par une section critique
				lock(oSectionCritique)
				{
					// Fabrication du nom de fichier auquel va être ajouté le log :
					if(LogDirectory(out sFileName))
					{
						sFileName = sFileName + sLogFileName ;
                        StreamWriter TxtLog = null;
                        try
						{
                            if (m_bTraceToFile)
                                TxtLog = new StreamWriter(sFileName, true);
					
							// Fabrication du message ajouté dans les logs :
							// DateHeure de l'écriture : LEVEL : Message1 : Message 2
							// Message 1 est tronqué à TAILLEMAX_MESSAGE1 caractères ou complété par des espaces
                            TraceCat DispCat = cat & TraceCat.LogDispCats;
                            string strLog = oDHSysteme.ToString("dd/MM/yyyy HH:mm:ss") + " " +
                                            Level.ToString().PadRight(8, ' ') + " " + 
                                            DispCat.ToString().PadRight(TAILLEMAX_CAT, ' ') + " " +
                                            sMessage1 + " " +sMessage2;
                                
                            Console.WriteLine(strLog);
                            if (TxtLog != null)
                                TxtLog.WriteLine(strLog);
						}
						catch
						{ 
							bRetour = false ;								
						}
						try
						{
							if(TxtLog != null) TxtLog.Close();							
						}
						catch
						{
						}
					} 
					else bRetour = false ;
				}
			}	
			return bRetour ;
		}
		
		// Routine permettant d'indiquer si la chaine n'est composée que de chiffres
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sReceive"></param>
        /// <returns></returns>
		private static bool isDigit(string sReceive) 
		{ 
			bool bResult; 
			bResult = true; 
			foreach (char cWork in sReceive) 
			{ 
				if (char.IsDigit(cWork) == false) 
				{ 
					bResult = false; 
				} 
			} 
			return bResult; 
		} 		

		// Routine permettant de supprimer les répertoires de trace anciens de plus de N jours
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iNbrJours"></param>
        /// <returns></returns>
		public static bool LogPurge(int iNbrJours)
		{
			string[] dirs ;
			string sMonDir,sDateLimite ;
			DateTime oDHSysteme;
			bool bRetour = true ;
			int iNbrRepsSupprimes = 0 ;

			// Calcul de la chaine permettant de définir les répertoires à supprimer
			oDHSysteme = DateTime.UtcNow ;
			oDHSysteme = oDHSysteme.AddDays(-iNbrJours);
			sDateLimite = oDHSysteme.ToString("yyyyMMdd");
			//LogAdd(LOG_LEVEL_INFO,"DIR","DateLimite : " + sDateLimite);
			
			// On protège le parcours des répertoires et leur suppression par la même
			// section critique pour l'écriture dans les fichiers de traces
			lock(oSectionCritique)
			{
				// On complète le tableau avec les répertoires
				try
				{
					dirs = Directory.GetDirectories(@sLogDirectory,"*");

					// On parcoure le tableau de répertoires
					foreach (string dir in dirs) 
					{
						// On recherche des répertoires ayant la forme : YYYYMMDD
						// Si le répertoire trouvé est plus court, on passe
						if(dir.Length >= 8)
						{
							sMonDir = dir.Remove(0,dir.Length-8) ;
							// Le nom du répertoire ne doit être composé que de chiffres
							if(isDigit(sMonDir))
							{
								// Si le nom du répertoire est inférieur à la date limite calculée,
								// on supprime le répertoire
								if(string.Compare(sMonDir,sDateLimite) < 0)
								{
									// On supprime le répertoire
									// Selon mes essais, même si le répertoire est non vide le Delete du
									// répertoire fonctionne
									Directory.Delete(dir,true);

									// Peut-être inoffensif, mais ma parît risqué, je supprimer donc cette trace 
									// LogAdd(LOG_LEVEL_INFO,"Traces","Suppression du repertoire : " + dir);
									iNbrRepsSupprimes++ ;
								
								} // else LogAdd(1,"Traces","Repertoire non supprime : " + sMonDir);
							} // else LogAdd(1,"Traces","Pas que des chiffres : " + sMonDir);
						} // else LogAdd(1,"Traces","Moins de 8 caracteres : " + dir);				
					}
				}
				catch
				{
					bRetour = false ;
				}
			}
			LogAddInfo(TraceCat.Others, "Nombre de repertoire(s) supprime(s) : " + iNbrRepsSupprimes.ToString());
			return bRetour ;
		}
	}
}
