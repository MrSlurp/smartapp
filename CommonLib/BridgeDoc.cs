using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CommonLib
{
    public class BridgeDoc : BaseDoc
    {
        /*
        internal class DataBridgeInfo 
        {
            public string docSrc;
            public string dataSrc;
            public string docDst;
            public string dataDst;
        }

        SolutionGest m_solution;*/

        //List<DataBridgeInfo> m_ListDataBridge = new List<DataBridgeInfo>();

        public BridgeDoc(TYPE_APP TypeApp) :base (TypeApp)
        {
        }
        

        public bool ReadIn(string filename)
        {
            return true;
        }
        public bool WriteOut(string filename, bool bShowError)
        {
            m_strfileFullName = filename;
            return true;
        }
    }
}
