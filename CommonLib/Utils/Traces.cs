using System;
using System.IO ;


namespace CommonLib
{
	/// <summary>
	/// Description r�sum�e de Traces.
	/// </summary>
	public class Traces
	{
		// Niveaux de trace
		public const int LOG_LEVEL_DEBUG    = 0;
		public const int LOG_LEVEL_INFO     = 1;
		public const int LOG_LEVEL_WARNING  = 2;
		public const int LOG_LEVEL_ERROR    = 3;
		public const int LOG_LEVEL_CRITICAL = 4;
		public const int LOG_LEVEL_NOLOG    = 5;
		static string[] LOG_LEVEL_DESCRIPTIONS = {"DEBUG", "INFO", "WARNING", "ERROR", "CRITICAL","NOLOG"};

		// Taille maximale de la chaine 1 du fichier de traces
		private const int TAILLEMAX_MESSAGE1	= 40;
		
		// Nombre de jours de logs conserv�s
		public const int NBR_JOURS = 7 ;

		// R�pertoire dans lequel les logs doivent �tre �crits
		private static string sLogDirectory ;
		// Nom du fichier de traces
		private static string sLogFileName ;
		// Niveau de trace demand�
		private static int iLogTracesLevel ;
		// Fichier de logs
		private static StreamWriter TxtLog;

		// Protection des fonctions de traces avec une section critique
		// Cette fonctionalit� est rendue n�cessaire par le fait que plusieurs Threads
		// sont susceptibles d'appeler les fonctions de trace de la m�me instance de l'objet.
		// La section critique est d�finie comme un membre priv� de l'objet et prot�ge 
		// les parties de code donnant acc�s au syst�me de fichier
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
        /// <param name="iTracesLevel"></param>
		public static void Initialize(string sDirectory,string sFileName,int iTracesLevel)
		{
			sLogDirectory = sDirectory ;
			sLogFileName = sFileName ;
			
			// Contr�le du niveau de trace demand�
			//if(iTracesLevel > LOG_LEVEL_CRITICAL) iTracesLevel = LOG_LEVEL_CRITICAL ;
			if(iTracesLevel > LOG_LEVEL_NOLOG) iTracesLevel = LOG_LEVEL_NOLOG ;
			if(iTracesLevel < LOG_LEVEL_DEBUG) iTracesLevel = LOG_LEVEL_DEBUG ;
			iLogTracesLevel = iTracesLevel ;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogLevelTest"></param>
        /// <returns></returns>
        public static bool IsLogLevelOK(int LogLevelTest)
        {
            if (iLogTracesLevel <= LogLevelTest)
            {
                return true;
            }
            return false;
        }
        
		// Routine permettant de d�terminer le r�pertoire dans lequel va se trouver
		// le fichier de Logs. Celui-ci est construit avec les param�tres du constructeur
		// plus un r�pertoire du jour : ex : C:\Repertoire\20071023\
		// Cette routine est prot�g�e par la section critique de la proc�dure qui l'appelle
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
			
			// On r�cup�re l'heure syst�me
			oDHSysteme = DateTime.UtcNow ;
			
			// On va utiliser un r�pertoire de Logs dont le nom est la date du jour
			sRepDuJour = oDHSysteme.ToString("yyyyMMdd") ;

			// Si le r�pertoire n'existe pas, on le cr��
			try
			{
				if(! Directory.Exists(PathTranslator.LinuxVsWindowsPathUse(sLogDirectory + @"\" + sRepDuJour)))
				{
					Directory.CreateDirectory(PathTranslator.LinuxVsWindowsPathUse(sLogDirectory + @"\" + sRepDuJour));
					
					// Si le r�pertoire a �t� cr��, on 
					LogPurge(NBR_JOURS) ;
				}
			}
			catch
			{	// On a rencontr� une exception
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
        public static bool LogAddDebug(string sMessage1, string sMessage2)
        {
            return LogAdd(LOG_LEVEL_DEBUG, sMessage1, sMessage2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iTracesLevel"></param>
        /// <param name="sMessage1"></param>
        /// <returns></returns>
        public static bool LogAdd(int iTracesLevel, string sMessage1)
        {
            return LogAdd(iTracesLevel, sMessage1, "");
        }

		// Routine permettant d'ajouter une trace au fichier de logs
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iTracesLevel"></param>
        /// <param name="sMessage1"></param>
        /// <param name="sMessage2"></param>
        /// <returns></returns>
		public static bool LogAdd(int iTracesLevel, string sMessage1, string sMessage2)
		{
			string sFileName ;
			DateTime oDHSysteme;
			bool bRetour = true ;
			
			// Contr�le du niveau de trace demand�
			//if(iTracesLevel > LOG_LEVEL_CRITICAL) iTracesLevel = LOG_LEVEL_CRITICAL ;
			//if(iTracesLevel < LOG_LEVEL_DEBUG) iTracesLevel = LOG_LEVEL_DEBUG ;
			
			// On n'enregistre la trace que si le niveau est sup�rieur ou �gal
			// au niveau demand� lors de l'initialisation
			if(iTracesLevel >= iLogTracesLevel)
			{							   
				// On r�cup�re l'heure syst�me en UTC
				oDHSysteme = DateTime.UtcNow ;
				
				// On prot�ge l'acc�s au syst�me de fichiers par une section critique
				lock(oSectionCritique)
				{
					// Fabrication du nom de fichier auquel va �tre ajout� le log :
					if(LogDirectory(out sFileName))
					{
						sFileName = sFileName + sLogFileName ;
						try
						{
							TxtLog = new StreamWriter(sFileName, true);
					
							// Fabrication du message ajout� dans les logs :
							// DateHeure de l'�criture : LEVEL : Message1 : Message 2
							// Message 1 est tronqu� � TAILLEMAX_MESSAGE1 caract�res ou compl�t� par des espaces

                            string strLog = oDHSysteme.ToString("dd") + @"/" + oDHSysteme.ToString("MM") +
                                @"/" + oDHSysteme.ToString("yyyy") + " " + oDHSysteme.ToString("HH") + ":"
                                + oDHSysteme.ToString("mm") + ":" + oDHSysteme.ToString("ss") + " " +
                                LOG_LEVEL_DESCRIPTIONS[iTracesLevel].PadRight(8, ' ') + " " +
                                sMessage1.PadRight(TAILLEMAX_MESSAGE1, ' ').Substring(0, TAILLEMAX_MESSAGE1) + " " +
                                sMessage2;
                            //Console.WriteLine(strLog);
                            TxtLog.WriteLine(strLog);

						}
						catch
						{ 
//							System.Console.WriteLine("Exception Traces");
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
		
		// Routine permettant d'indiquer si la chaine n'est compos�e que de chiffres
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

		// Routine permettant de supprimer les r�pertoires de trace anciens de plus de N jours
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

			// Calcul de la chaine permettant de d�finir les r�pertoires � supprimer
			oDHSysteme = DateTime.UtcNow ;
			oDHSysteme = oDHSysteme.AddDays(-iNbrJours);
			sDateLimite = oDHSysteme.ToString("yyyyMMdd");
			//LogAdd(LOG_LEVEL_INFO,"DIR","DateLimite : " + sDateLimite);
			
			// On prot�ge le parcours des r�pertoires et leur suppression par la m�me
			// section critique pour l'�criture dans les fichiers de traces
			lock(oSectionCritique)
			{
				// On compl�te le tableau avec les r�pertoires
				try
				{
					dirs = Directory.GetDirectories(@sLogDirectory,"*");

					// On parcoure le tableau de r�pertoires
					foreach (string dir in dirs) 
					{
						// On recherche des r�pertoires ayant la forme : YYYYMMDD
						// Si le r�pertoire trouv� est plus court, on passe
						if(dir.Length >= 8)
						{
							sMonDir = dir.Remove(0,dir.Length-8) ;
							// Le nom du r�pertoire ne doit �tre compos� que de chiffres
							if(isDigit(sMonDir))
							{
								// Si le nom du r�pertoire est inf�rieur � la date limite calcul�e,
								// on supprime le r�pertoire
								if(string.Compare(sMonDir,sDateLimite) < 0)
								{
									// On supprime le r�pertoire
									// Selon mes essais, m�me si le r�pertoire est non vide le Delete du
									// r�pertoire fonctionne
									Directory.Delete(dir,true);

									// Peut-�tre inoffensif, mais ma par�t risqu�, je supprimer donc cette trace 
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
			LogAdd(LOG_LEVEL_INFO,"Traces","Nombre de repertoire(s) supprime(s) : " + iNbrRepsSupprimes.ToString());
			return bRetour ;
		}
	}
}
