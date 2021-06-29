using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using  System.Xml;
using System.Configuration;

namespace Types
{
    public class Types
    {
    }

    public class Helper_Function
    {
        public void PutLog(
        int _Error
        , string _Location
        , string _Message
        , string   _sInfo  = null
        )
        {
            try
            {
                StreamWriter _Writer;
                StringBuilder _Line;
                string _Path;
                Helper _Config = new Helper();
   

                _Path = _Config.LogPath;

                if (!Directory.Exists(_Path))
                { Directory.CreateDirectory(_Path); }

                _Path = _Path + DateTime.Today.ToString("yyyyMMdd") + ".log";
                _Writer = new StreamWriter(_Path, true);

                _Line = new StringBuilder();

                _Line.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                switch (_Error)
                {
                    case 1: _Line.Append(" ERROR    "); break;
                    case 2: _Line.Append(" X3WSERR  "); break;
                    case 3: _Line.Append(" APPINFO  "); break;
                            //_Info += sMessage & "~";
                    case 4: _Line.Append("          "); break;
            }

                _Line.Append(" " + _Location + "  " + _Message);

                _Writer.Write(_Line);
                _Writer.WriteLine();
                _Writer.Flush();
                _Writer.Close();

            }
            catch
            {
              
            }
        
        
        
        }


    }
}
