using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SmartApp.Datas;
using System.Xml;

namespace SmartApp.Gestionnaires
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

            // constructeur
            public Group()
            {
                m_ArrayObjectOfGroup = new ArrayList();
            }

            // symbol du groupe
            public string GroupSymbol
            {
                get
                {
                    return m_strGroupSymbol;
                }
            }
            // nom affich� du groupe
            public string GroupName
            {
                get
                {
                    return m_strGroupName;
                }
            }

            // objets contenus dans le groupe
            public ArrayList Items
            {
                get
                {
                    return m_ArrayObjectOfGroup;
                }
            }

            //*****************************************************************************************************
            // Description: ecrit les donn�es de l'objet a partir de son noeud XML
            // Return: /
            //*****************************************************************************************************
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
        //*****************************************************************************************************
        // Description: accesseur sur la liste des groupes
        // Return: /
        //*****************************************************************************************************
        public List<Group> Groups
        {
            get
            {
                return m_ListGroup;
            }
        }
        #endregion

        #region constructeurs et overrides
        //*****************************************************************************************************
        // Description: constructeur
        // Return: /
        //*****************************************************************************************************
        public BaseGestGroup()
        {
            CreateDefaultGroup();
        }

        //*****************************************************************************************************
        // Description: override de base gest
        // ajoute l'objet et l'ajoute aussi au groupe par d�faut
        // Return: /
        //*****************************************************************************************************
        public override void AddObj(BaseObject Obj)
        {
            base.AddObj(Obj);
            this.AddObjectToGroup(STR_DEFAULT_GROUP_SYMB, Obj);
        }

        //*****************************************************************************************************
        // Description: override de base gest
        // enl�ve l'objet du gestionnaire et des groupes (si l'objet a �t� supprim�)
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: override de base gest
        // enl�ve l'objet du gestionnaire et des groupes (si l'objet a �t� supprim�)
        // Return: /
        //*****************************************************************************************************
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
          
          //\\
         //!!\\ Incoh�rence a r�gler ou a expliquer entre le read in et le write out
        //||||\\
        #region ReadIn / WriteOut
        //*****************************************************************************************************
        // Description: Lit les donn�es de l'objet a partir de son noeud XML
        // Return: /
        //*****************************************************************************************************
        public override bool ReadIn(XmlNode Node)
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
        //*****************************************************************************************************
        // Description: cr�e un nouveau groupe avec le nom indiqu�
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: assigne la couleur d'un groupe
        // Return: /
        //*****************************************************************************************************
        public void SetGroupColor(string strGroupSymbol, Color color)
        {
            Group Gr = GetGroupFromSymbol(strGroupSymbol);
            if (Gr != null)
            {
                Gr.m_GroupColor = color;
            }
        }

        //*****************************************************************************************************
        // Description: ajoute un objet au gestionaire dans le groupe donn�
        // Return: /
        //*****************************************************************************************************
        public virtual void AddObjAtGroup(BaseObject Obj, string strGroup)
        {
            base.AddObj(Obj);
            Group gr = this.GetGroupFromSymbol(strGroup);
            if (gr != null)
                gr.m_ArrayObjectOfGroup.Add(Obj);
        }

        //*****************************************************************************************************
        // Description: cr�e le groupe par d�faut
        // Return: /
        //*****************************************************************************************************
        protected void CreateDefaultGroup()
        {
            InternalCreateNewGroup("Default group", STR_DEFAULT_GROUP_SYMB, Color.White);
        }

        //*****************************************************************************************************
        // Description: d�truit un groupe en retransferant tou les objet qu'iul contiens vers le groupe par d�faut
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: ajoute un objet au groupe donn�e en ple retirant d'un autre groupe si besoin
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: enl�ve l'objet d'un groupe
        // Return: /
        //*****************************************************************************************************
        public void RemoveObjectOfGroup(Group Gr, BaseObject Obj)
        {
	        if (Gr != null)
	        {
		        if( Gr.m_ArrayObjectOfGroup.Contains(Obj))
		        {
			        Gr.m_ArrayObjectOfGroup.Remove(Obj);
		        }
	        }
        }

        //*****************************************************************************************************
        // Description: renvoie la couleur d'un groupe
        // Return: /
        //*****************************************************************************************************
        public Color GetGroupColor(string strGroupSymbol)
        {
            Group Gr = GetGroupFromSymbol(strGroupSymbol);
            if (Gr != null)
            {
                return Gr.m_GroupColor;
            }
            return Color.White;
        }

        //*****************************************************************************************************
        // Description: renvoie le nom d'un groupe apartir de son symbol
        // Return: /
        //*****************************************************************************************************
        public string GetGroupName(string strGroupSymbol)
        {
            Group Gr = GetGroupFromSymbol(strGroupSymbol);
            if (Gr != null)
            {
                return Gr.m_strGroupName;
            }
            return "";
        }

        //*****************************************************************************************************
        // Description: assigne le nom d'un groupe a partir de son symbol
        // Return: /
        //*****************************************************************************************************
        public void SetGroupName(string strGroupSymbol, string strName)
        {
            Group Gr = GetGroupFromSymbol(strGroupSymbol);
            if (Gr != null)
            {
                Gr.m_strGroupName = strName;
            }
        }

        //*****************************************************************************************************
        // Description: renvoie le nom du groupe a l'index donn�
        // Return: /
        //*****************************************************************************************************
        public string GetGroupNameAt(int index)
        {
            string strSym = "";
            if (index >= 0 && index < m_ListGroup.Count)
            {
                strSym = m_ListGroup[index].m_strGroupName;
            }
            return strSym;
        }

        //*****************************************************************************************************
        // Description: renvoie le nombre de groupes 
        // Return: /
        //*****************************************************************************************************
        public int GroupCount
        {
            get
            {
                return m_ListGroup.Count;
            }
        }

        //*****************************************************************************************************
        // Description: renvoie le symbol du groupe a l'index donn�
        // Return: /
        //*****************************************************************************************************
        public string GetSymbolGroupAt(int index)
        {
            string strSym = "";
            if (index >= 0 && index < m_ListGroup.Count)
            {
                strSym = m_ListGroup[index].m_strGroupSymbol;
            }
            return strSym;
        }


        //*****************************************************************************************************
        // Description: renvoie le symbol du groupe contenant l'objet ayant le symbol strObj
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: renvoie le groupe qui contiens l'objet
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: renvoie l'objet groupe a partir de son symbol
        // Return: /
        //*****************************************************************************************************
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

        //*****************************************************************************************************
        // Description: fonction interne de cr�ation d'un nouveau groupe
        // Return: /
        //*****************************************************************************************************
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
        // Description: renvoie le prochain text disponible pour un nom de groupe
        // Return: /
        //*****************************************************************************************************
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
        public override void TraiteMessage(MESSAGE Mess, object Param)
        {
            // on a rien a faire car les groupes g�re les objets par r�f�rence et non par symbol
        }
        #endregion
    }
}
