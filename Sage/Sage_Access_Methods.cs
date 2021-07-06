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
using System.Net;

namespace Sage
{
    //-------------------------------
    //
    //-------------------------------
    class Sage_Access_Methods
    {

        private Sage Config { get; set; }

        Helper_Function _Helper { get; set; }

        private CAdxWebServiceXmlCCService _Wsvc { get; set; }

        private CAdxCallContext _CallContext { get; set; }        // Instance of CAdxCallContext

        //-------------------------------
        //
        //-------------------------------
        public void Init()
        {
            try
            {
                this.Config = new Sage();
                this._Helper = new Helper_Function();

                if (Config.X3_Version == "V6")
                {
                    _Wsvc = new Sage_Ws.CAdxWebServiceXmlCCService();
                    _CallContext = Get_V6_Call_Context();
                    _Wsvc.Url = Config.Sage_Url;
                    _Wsvc.Timeout = Config.Sage_TimeOut;
                }

                if (Config.X3_Version == "V12")
                {
                    _Wsvc = new BasicAuth();
                    _CallContext = Get_V12_Call_Context();
                    _Wsvc.Url = Config.V12_Url;
                    _Wsvc.Credentials = new NetworkCredential(Config.V12_User, Config.V12_Password);
                    _Wsvc.PreAuthenticate = true;
                    _Wsvc.Timeout = Config.Sage_TimeOut;
                }
            }
            catch (Exception _ex)
            {
                _Helper.PutLog(1, " Sage_Access_Methods.Init", _ex.Message);
                throw _ex;
            }
           }

        //-------------------------------
        //
        //-------------------------------
        private CAdxCallContext Get_V6_Call_Context()
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

        //-------------------------------
        // Calling context - class CAdxCallContext
        //-------------------------------
        private CAdxCallContext Get_V12_Call_Context()
        {
            try
            {
  
                CAdxCallContext cc = new CAdxCallContext();         // Instance of CAdxCallContext
                cc.codeLang = Config.Sage_Language;                 // Language code
                cc.poolAlias = Config.V12_Pool;                     // Pool name
                cc.requestConfig = Config.Sage_Request_Config;      // Request configuration string

                return cc;
            }
            catch (Exception _ex)
            {
                _Helper.PutLog(1, " Sage_Access_Methods.Get_V12_Call_Context", _ex.Message);
                throw _ex;
            }
        }

        //-------------------------------
        //
        //-------------------------------
        public XmlDocument Run(string _PublicName, string _InputXml)
        {
            try
            {
 
                XmlDocument _XmlDoc = new XmlDocument();
                CAdxResultXml _Result;
 
                    _Result = _Wsvc.run(_CallContext, _PublicName, _InputXml);

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

    //-------------------------------
    //
    //-------------------------------
    public class BasicAuth : CAdxWebServiceXmlCCService
    {

        //-------------------------------
        //
        //-------------------------------
        protected override WebRequest GetWebRequest(Uri uri)
        {
            HttpWebRequest _WebRequest = (HttpWebRequest)base.GetWebRequest(uri);

            NetworkCredential credentials = Credentials as NetworkCredential;

            if (credentials != null)
            {
                string _authinfo = "";
                if (credentials.Domain != null && credentials.Domain.Length > 0)
                {
                    _authinfo = string.Format(@"{0}\{1}:{2}", credentials.Domain, credentials.UserName, credentials.Password);
                }
                else
                {
                    _authinfo = string.Format(@"{0}:{1}", credentials.UserName, credentials.Password);
                }

                _authinfo = Convert.ToBase64String(Encoding.Default.GetBytes(_authinfo));
                _WebRequest.Headers["Authorization"] = "Basic " + _authinfo;
            }

            return _WebRequest;

        }

    }
}
