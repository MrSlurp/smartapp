using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;

namespace CommonLib
{
    // classe sp�cialis�e dans la gestion des objets par groupes
    public class BaseGestGroup : BaseGest
    {
        #region classe group repr�sentant les donn�es d'un groupe
        public class Group
        {
            // contiens le nom affich� du group
            public string m_strGroupName;
            // contiens le symbol du group
            public string m_strGroupSymbol;
            // Array de chaines qui stok les symboles de tout les objets que group poss�de
            public ArrayList m_ArrayObjectOfGroup;
            // couleur de fond du groupe
            public Color m_GroupColor;

            /// <summary>
            /// constructeur 
            /// </summary>
            public Group()
            {
                m_ArrayObjectOfGroup = new ArrayList();
            }

            /// <summary>
            /// symbol du groupe
            /// </summary>
            public string GroupSymbol
            {
                get
                {
                    return m_strGroupSymbol;
                }
            }
            /// <summary>
            /// nom affich� du groupe
            /// </summary>
 
            public string GroupName
            {
                get
                {
                    return m_strGroupName;
                }
            }

            /// <summary>
            /// objets contenus dans le groupe
            /// </summary>
            public ArrayList Items
            {
                get
                {
                    return m_ArrayObjectOfGroup;
                }
            }

            /// <summary>
            /// Lit les donn�es de l'objet a partir de son noeud XML
            /// </summary>
            /// <param name="Node">Noeud Xml de l'objet</param>
            /// <param name="TypeApp">type d'application courante</param>
            /// <returns>true si la lecture s'est bien pass�</returns>
            public bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
            {
                XmlNode XmlGroup = XmlDoc.CreateElement(XML_CF_TAG.Group.ToString());
                XmlAttribute Name = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strNom.ToString());
                Name.Value = m_strGroupName;
                XmlGroup.Attributes.Append(Name);
                XmlAttribute Symb = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strSymbol.ToString());
                Symb.Value = m_strGroupSymbol;
                XmlGroup.Attributes.Append(Symb);
                XmlAttribute Col = XmlDoc.CreateAttribute(XML_CF_ATTRIB.bkColor.ToString());
                //Col.Value = m_GroupColor.ToString();
                Col.Value = string.Format("{0}, {1}, {2}", m_GroupColor.R, m_GroupColor.G, m_GroupColor.B);
                XmlGroup.Attributes.Append(Col);
                for (int i = 0; i < m_ArrayObjectOfGroup.Count; i++)
                {
                    BaseObject obj = (BaseObject) m_ArrayObjectOfGroup[i];
                    XmlNode NodeObj = XmlDoc.CreateElement(XML_CF_TAG.Object.ToString());
                    XmlAttribute AttrSymb = XmlDoc.CreateAttribute(XML_CF_ATTRIB.strSymbol.ToString());
                    AttrSymb.Value = obj.Symbol;
                    NodeObj.Attributes.Append(AttrSymb);
                    XmlGroup.AppendChild(NodeObj);
                }

                Node.AppendChild(XmlGroup);
                return true;
            }
        }
        #endregion

        #region donn�es membres
        // texte par d�faut d'un nouveau groupe
        protected const string STR_DEFAULT_GROUP_TEXT = "New Group {0}";
        // symbol format� des groupes
        protected const string STR_GROUP_SYMB = "GROUP_{0}";
        // symbol du groupe par d�fait (toujours pr�sent)
        public const string STR_DEFAULT_GROUP_SYMB = "GROUP_0";
        // Liste des groupes du gestionnaire des groupes
        List<Group> m_ListGroup = new List<Group>();
        #endregion

        #region attributs
        /// <summary>
        /// accesseur sur la liste des groupes
        /// </summary>
        public List<Group> Groups
        {
            get
            {
                return m_ListGroup;
            }
        }
        #endregion

        #region constructeurs et overrides
        /// <summary>
        /// constructeur
        /// </summary>
        public BaseGestGroup()
        {
            CreateDefaultGroup();
        }

        /// <summary>
        /// override de base gest
        /// ajoute l'objet et l'ajoute aussi au groupe par d�faut
        /// </summary>
        /// <param name="Obj">objet � ajouter</param>
        public override void AddObj(BaseObject Obj)
        {
            base.AddObj(Obj);
            this.AddObjectToGroup(STR_DEFAULT_GROUP_SYMB, Obj);
        }

        /// <summary>
        /// override de base gest
        /// enl�ve l'objet du gestionnaire et des groupes (si l'objet a �t� supprim�)
        /// </summary>
        /// <param name="Obj">objet � enlever</param>
        /// <returns>true si l'objet � �t� supprim�</returns>
        public override bool RemoveObj(BaseObject Obj)
        {
            if (base.RemoveObj(Obj))
            {
                Group Gr = GetGroupFromObject(Obj);
                if (Gr != null)
                {
                    RemoveObjectOfGroup(Gr, Obj);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// override de base gest
        /// enl�ve l'objet du gestionnaire et des groupes (si l'objet a �t� supprim�)
        /// </summary>
        /// <param name="strSymbol">symbol de l'objet � enlever</param>
        /// <returns>true si l'objet � �t� supprim�</returns>
        public override bool RemoveObj(String strSymbol)
        {
            if (base.RemoveObj(strSymbol))
            {
                int index = GetIndexFromSymbol(strSymbol);
                if (index == -1)
                    return true;
                BaseObject obj = this[index];
                Group Gr = GetGroupFromObject(obj);
                if (Gr != null)
                {
                    RemoveObjectOfGroup(Gr, obj);
                }
                return true;
            }
            return false;
        }
        #endregion
          
        #region ReadIn / WriteOut
        //*****************************************************************************************************
        // Description: Lit les donn�es de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(XmlNode Node, TYPE_APP TypeApp)
        {
            return ReadGestGroup(Node, TypeApp);
        }

        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        protected bool ReadGestGroup(XmlNode Node, TYPE_APP TypeApp)
        {
            XmlNode NodeSectionGroup = null;
            for (int i = 0; i<Node.ChildNodes.Count; i++)
            {
	            NodeSectionGroup = Node.ChildNodes.Item(i);
                if (NodeSectionGroup.Name == XML_CF_TAG.GroupSection.ToString())
                    break;
                NodeSectionGroup = null;
            }
	        // dans la premi�re version de fichier il n'y avait pas de groupes
	        if (NodeSectionGroup == null)
		        return true;
	        XmlNode GroupNode = NodeSectionGroup.FirstChild;
	        while (GroupNode != null)
	        {
		        // v�rifier si le group n'existe pas d�ja (Groupe "d�fault" toujours cr�e)
		        XmlNode AttrSymb = GroupNode.Attributes.GetNamedItem(XML_CF_ATTRIB.strSymbol.ToString());
		        XmlNode AttrName = GroupNode.Attributes.GetNamedItem(XML_CF_ATTRIB.strNom.ToString());
		        XmlNode AttrColor = GroupNode.Attributes.GetNamedItem(XML_CF_ATTRIB.bkColor.ToString());;
		        if (!(AttrSymb != null && AttrName != null && AttrColor != null))
			        return false;
		        Group Gr = GetGroupFromSymbol(AttrSymb.Value);
                string[] rgbVal = AttrColor.Value.Split(',');
                int r = int.Parse(rgbVal[0]);
                int g = int.Parse(rgbVal[1]);
                int b = int.Parse(rgbVal[2]);
		        // si il existe on le r�cup�re et on y ajoute les objets du groupe
		        if (Gr == null)
		        {
			        Gr = InternalCreateNewGroup(AttrName.Value, AttrSymb.Value, Color.FromArgb(r,g,b));
		        }
                else
                {
                    Gr.m_GroupColor = Color.FromArgb(r, g, b);
                }
		        XmlNode NodeObj = GroupNode.FirstChild;
		        // si le groupe a �t� cr�e, la couleur est a jour, mais pour le groupe par d�faut ce n'est pas le cas
		        // parcour de la liste des objets du groupe
		        while (NodeObj != null)
		        {
			        XmlNode AttrSym = NodeObj.Attributes.GetNamedItem(XML_CF_ATTRIB.strSymbol.ToString());
			        BaseObject Obj = GetFromSymbol(AttrSym.Value);
			        if (Obj == null)
				        return false;
                    AddObjectToGroup(Gr.m_strGroupSymbol, Obj);
                    NodeObj = NodeObj.NextSibling;
		        }
		        // sinon on cr�e un nouveau groupe et on l'ajoute et on ajoute les objets du groupe
		        GroupNode = GroupNode.NextSibling;
	        }
	        return true;
        }

        //*****************************************************************************************************
        // Description: ecrit les donn�es de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool WriteOut(XmlDocument XmlDoc, XmlNode Node)
        {
            base.WriteOut(XmlDoc, Node);
            XmlNode GroupSection = XmlDoc.CreateElement(XML_CF_TAG.GroupSection.ToString());
            Node.AppendChild(GroupSection);
            for (int i =0; i< m_ListGroup.Count; i++)
            {
                m_ListGroup[i].WriteOut(XmlDoc, GroupSection);
            }
            return true;
        }
        #endregion

        #region fonction de gestion des groupes
        /// <summary>
        /// cr�e un nouveau groupe
        /// </summary>
        /// <param name="strGroupName">nom du groupe</param>
        /// <param name="color">couleur du groupe</param>
        /// <returns>l'objet groupe cr�e</returns>
        public Group CreateNewGroup(string strGroupName, Color color)
        {
            bool bFreeIndexFound = false;
            // r�cup�rer le prochain symbol, 
            // pour trouver le premier index libre, on teste si il existe un
            // groupe ayant le symbol GROUP_i
            string strSymb = "";
            for (int i = 0; !bFreeIndexFound; i++)
            {
                strSymb = string.Format(STR_GROUP_SYMB, i);
                // pas de groupe trouv�, l'index est libre
                if (GetGroupFromSymbol(strSymb) == null)
                    bFreeIndexFound = true;
            }
            // cr�e le groupe avec son symbole
            return InternalCreateNewGroup(strGroupName, strSymb, color);
        }

        /// <summary>
        /// D�finit la couleur d'un groupe
        /// </summary>
        /// <param name="strGroupSymbol">symbol du groupe</param>
        /// <param name="color">nouvelle couleur du groupe</param>
        public void SetGroupColor(string strGroupSymbol, Color color)
        {
            Group Gr = GetGroupFromSymbol(strGroupSymbol);
            if (Gr != null)
            {
                Gr.m_GroupColor = color;
            }
        }

        /// <summary>
        /// ajoute un objet au gestionaire dans le groupe donn�
        /// </summary>
        /// <param name="Obj">objet � ajouter</param>
        /// <param name="strGroup">symbol du groupe ou l'objet sera stock�</param>
        public virtual void AddObjAtGroup(BaseObject Obj, string strGroup)
        {
            base.AddObj(Obj);
            Group gr = this.GetGroupFromSymbol(strGroup);
            if (gr != null)
                gr.m_ArrayObjectOfGroup.Add(Obj);
        }

        /// <summary>
        /// cr�e le groupe par d�faut, non �ditable, non supprimable
        /// </summary>
        protected void CreateDefaultGroup()
        {
            InternalCreateNewGroup("Default group", STR_DEFAULT_GROUP_SYMB, Color.White);
        }

        /// <summary>
        /// d�truit un groupe en retransferant tout les objet qu'iul contiens vers le groupe par d�faut
        /// </summary>
        /// <param name="strGroupSymbol">symbol du groupe � supprimer</param>
        public void DeleteGroup(string strGroupSymbol)
        {
            Group gr = GetGroupFromSymbol(strGroupSymbol);
            if (gr != null)
            {
                // si le groupe n'est pas vide, on retransfert le tour vers le group par d�faut
                if (gr.m_ArrayObjectOfGroup.Count != 0)
                {
                    // on passe par une boucle while car les objets transf�r�s sur le groupe par
                    // defaut sont retir� du groupe qui va �tre d�truit
                    while (gr.m_ArrayObjectOfGroup.Count > 0)
                    {
                        // on ne parcour pas cet array avec une boucle for car l'appel a la fonction
                        // AddObjectToGroup() va retirer l'objet du tableau et donc changer les indexs
                        BaseObject Obj = (BaseObject) gr.m_ArrayObjectOfGroup[0];
                        AddObjectToGroup(STR_DEFAULT_GROUP_SYMB, Obj);
                    }
                }
                m_ListGroup.Remove(gr);
            }
        }

        /// <summary>
        /// ajoute un objet au groupe donn�e en le retirant d'un autre groupe si besoin
        /// </summary>
        /// <param name="strGroupSymbol">symbol du groupe</param>
        /// <param name="Obj">objet � ajouter au groupe</param>
        public void AddObjectToGroup(string strGroupSymbol, BaseObject Obj)
        {
            // l'objet peut d�ja appartenir a un autre groupe
            Group OldGr = GetGroupFromObject(Obj);
            if (OldGr != null)
            {
                RemoveObjectOfGroup(OldGr, Obj);
            }
            Group Gr = GetGroupFromSymbol(strGroupSymbol);
            if (Gr != null)
            {
                Gr.m_ArrayObjectOfGroup.Add(Obj);
            }
        }

        /// <summary>
        /// enl�ve l'objet d'un groupe
        /// </summary>
        /// <param name="Gr">objet groupe ou est l'objet</param>
        /// <param name="Obj">objet � retirer</param>
        protected void RemoveObjectOfGroup(Group Gr, BaseObject Obj)
        {
	        if (Gr != null)
	        {
		        if( Gr.m_ArrayObjectOfGroup.Contains(Obj))
		        {
			        Gr.m_ArrayObjectOfGroup.Remove(Obj);
		        }
	        }
        }

        /// <summary>
        /// r�cup�re la couleur d'un groupe
        /// </summary>
        /// <param name="strGroupSymbol">symbol du groupe dont on veux la couleur</param>
        /// <returns>la couleur du groupe</returns>
        public Color GetGroupColor(string strGroupSymbol)
        {
            Group Gr = GetGroupFromSymbol(strGroupSymbol);
            if (Gr != null)
            {
                return Gr.m_GroupColor;
            }
            return Color.White;
        }

        /// <summary>
        /// renvoie le nom d'un groupe a partir de son symbol
        /// </summary>
        /// <param name="strGroupSymbol">Symbol du groupe</param>
        /// <returns>le nom du groupe</returns>
        public string GetGroupName(string strGroupSymbol)
        {
            Group Gr = GetGroupFromSymbol(strGroupSymbol);
            if (Gr != null)
            {
                return Gr.m_strGroupName;
            }
            return "";
        }

        /// <summary>
        /// assigne le nom d'un groupe a partir de son symbol
        /// </summary>
        /// <param name="strGroupSymbol">Symbol du groupe</param>
        /// <param name="strName">nouveau nom du groupe</param>
        public void SetGroupName(string strGroupSymbol, string strName)
        {
            Group Gr = GetGroupFromSymbol(strGroupSymbol);
            if (Gr != null)
            {
                Gr.m_strGroupName = strName;
            }
        }

        /// <summary>
        /// renvoie le nom du groupe a l'index donn�
        /// </summary>
        /// <param name="index">index du groupe</param>
        /// <returns>nom du groupe</returns>
        public string GetGroupNameAt(int index)
        {
            string strSym = "";
            if (index >= 0 && index < m_ListGroup.Count)
            {
                strSym = m_ListGroup[index].m_strGroupName;
            }
            return strSym;
        }

        /// <summary>
        /// renvoie le nombre de groupes existants
        /// </summary>
        public int GroupCount
        {
            get
            {
                return m_ListGroup.Count;
            }
        }

        /// <summary>
        /// renvoie le symbol du groupe a l'index donn�
        /// </summary>
        /// <param name="index">index du groupe</param>
        /// <returns>symbol du groupe</returns>
        public string GetSymbolGroupAt(int index)
        {
            string strSym = "";
            if (index >= 0 && index < m_ListGroup.Count)
            {
                strSym = m_ListGroup[index].m_strGroupSymbol;
            }
            return strSym;
        }

        /// <summary>
        /// renvoie le symbol du groupe contenant l'objet ayant le symbol strObj
        /// </summary>
        /// <param name="strObj">symbol de l'objet</param>
        /// <returns>symbol du groupe</returns>
        public string GetGroupSymbolFromObject(string strObj)
        {
	        string strGroupSymb = "";
            BaseObject obj = this.GetFromSymbol(strObj);
	        for (int i = 0; i < m_ListGroup.Count ; i++)
	        {
		        Group Gr = m_ListGroup[i];
                if (Gr.m_ArrayObjectOfGroup.Contains(obj))
                {
                    strGroupSymb = Gr.m_strGroupSymbol;
                    break;
                }
	        }
	        return strGroupSymb;
        }

        /// <summary>
        /// renvoie le groupe qui contiens l'objet
        /// </summary>
        /// <param name="Obj">symbol de l'objet</param>
        /// <returns>objet groupe</returns>
        public Group GetGroupFromObject(BaseObject Obj)
        {
            for (int i = 0; i < m_ListGroup.Count; i++)
            {
                Group Gr = m_ListGroup[i];
                if (Gr.m_ArrayObjectOfGroup.Contains(Obj))
                    return Gr;
            }
            return null;
        }

        /// <summary>
        /// renvoie l'objet groupe a partir de son symbol
        /// </summary>
        /// <param name="strGroupSymbol">symbol du groupe</param>
        /// <returns>objet groupe</returns>
        public Group GetGroupFromSymbol(string strGroupSymbol)
        {
            for (int i = 0; i < m_ListGroup.Count; i++)
            {
                Group Gr = m_ListGroup[i];
                if (Gr.m_strGroupSymbol == strGroupSymbol)
                    return Gr;
            }
            return null;
        }

        /// <summary>
        /// fonction interne de cr�ation d'un nouveau groupe
        /// </summary>
        /// <param name="strGroupName">nom du groupe</param>
        /// <param name="strGroupSymbol">symbol du groupe</param>
        /// <param name="cr">couleur du groupe</param>
        /// <returns>objet groupe</returns>
        protected Group InternalCreateNewGroup(string strGroupName, string strGroupSymbol, Color cr)
        {
            Group gr = null;
	        // v�rifier que le groupe n'exite pas d�ja
	        gr = GetGroupFromSymbol(strGroupSymbol);
	        // si il exite, on ne fais rien (on pourrai mettre a jour le texte �ventuellement)
	        if (gr != null)
		        return gr;

	        // si il n'existe pas, on cr�e un nouveau groupe avec aucun �l�ments
	        gr = new Group();
	        gr.m_strGroupSymbol = strGroupSymbol;
	        gr.m_strGroupName = strGroupName;
	        gr.m_GroupColor = cr;
            this.m_ListGroup.Add(gr);
	        return gr;

        }
        #endregion

        #region Fonction "utilitaires"
        //*****************************************************************************************************
        // Description: 
        // Return: /
        //*****************************************************************************************************
        /// <summary>
        /// renvoie le prochain nom disponible pour un nom de groupe
        /// </summary>
        /// <returns>nouveau nom de groupe</returns>
        public string GetNextDefaultGroupText()
        {
            for (int i = 0; i < MAX_DEFAULT_ITEM_SYMBOL; i++)
            {
                string strGrText = string.Format(STR_DEFAULT_GROUP_TEXT, i);
                Group ExistingGroup = null;
                for (int j = 0; j < m_ListGroup.Count; j++)
                {
                    Group Gr = m_ListGroup[j];
                    if (Gr.GroupName == strGrText)
                    {
                        ExistingGroup = Gr;
                        break;
                    }
                }
                if (ExistingGroup == null)
                {
                    return strGrText;
                }
            }
            return "";
        }
        #endregion

        #region Gestion des AppMessages
        //*****************************************************************************************************
        // Description: g�re la r�c�ption d'un message
        // Return: /
        //*****************************************************************************************************
        public override void TraiteMessage(MESSAGE Mess, object Param, TYPE_APP TypeApp)
        {
            base.TraiteMessage(Mess, Param, TypeApp);
            // on a rien a faire de plus car les groupes g�re les objets par r�f�rence et non par symbol
        }
        #endregion
    }
}
