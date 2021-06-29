using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sage;
using Sage.Sage_Ws;
using System.Xml;
using System.Configuration;
using Types;

namespace Sage
{
    class Sage_Access_Methods
    {

        Sage Config { get; set; }

        Helper_Function _Helper { get; set; }

        private CAdxCallContext Get_Call_Context()
        {
            try
            {
                // Calling context - class CAdxCallContext
                CAdxCallContext cc = new CAdxCallContext();         // Instance of CAdxCallContext
                cc.codeLang = Config.Sage_Language;                 // Language code
                cc.codeUser = Config.Sage_User;                     // X3 user
                cc.password = Config.Sage_Pwd;                      // X3 password
                cc.poolAlias = Config.Sage_Pool;                    // Pool name
                cc.requestConfig = Config.Sage_Request_Config;      // Request configuration string

                return cc;
            }
            catch (Exception _ex)
            {
                _Helper.PutLog(1, " Sage_Access_Methods.Get_Call_Context", _ex.Message);
                throw _ex;
            }
        }


        internal XmlDocument Run(string _PublicName, string _InputXml)
        {
            try
            {
                Sage_Ws.CAdxWebServiceXmlCCService _Wsvc = new Sage_Ws.CAdxWebServiceXmlCCService();
                XmlDocument _XmlDoc = new XmlDocument();
                this.Config = new Sage();
                this._Helper = new Helper_Function();

                _Wsvc.Url = Config.Sage_Url; 
                _Wsvc.Timeout = 300000;

                CAdxResultXml _Result;

                // Calling context - class CAdxCallContext
                CAdxCallContext cc = Get_Call_Context();         // Instance of CAdxCallContext
 
                    _Result = _Wsvc.run(cc, _PublicName, _InputXml);

                if (_Result.status == 1)
                {   _XmlDoc.LoadXml(_Result.resultXml); 
                    return _XmlDoc;
                }
                else
                { return null; }

            }
            catch ( System.Net.Sockets.SocketException _soex)
            {
                _Helper.PutLog(1, " Sage_Access_Methods.Run", _soex.Message);
                throw _soex;
            }
            catch (Exception _ex)
            {
                _Helper.PutLog(1, " Sage_Access_Methods.Run", _ex.Message);
                throw _ex;
            }
        }


    }
}
