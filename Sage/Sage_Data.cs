using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using Types;
using System.Globalization;

namespace Sage
{
    //-------------------------------
    //
    //-------------------------------
    public class Sage_Data
    {
        Helper_Function _Helper { get; set; }

        //-------------------------------
        //
        //-------------------------------
        public Types.Product Get_Siemens_Product(string _Start_Product)
        {
            try
            {
                XmlDocument _XmlDoc;
                Sage_Access_Methods _Sage_Method = new Sage_Access_Methods();
                Types.Product _Product = new Types.Product();
                _Helper = new Helper_Function();

                _Sage_Method.Init();

                string _InputXml = "<PARAM>" +
                    "<GRP ID= \"GRP1\">" +
                    "<FLD NAME = \"ITMREF\">" + _Start_Product + "</FLD>" +
                    "</GRP>" +
                    "</PARAM>";

                _XmlDoc = _Sage_Method.Run("BGR_SIEPRO", _InputXml);

                _Product = Convert_Product_Xml(_XmlDoc);

                return _Product;
            }
            catch (Exception _ex)
            {
                _Helper.PutLog(1, " Sage_Data.Get_Siemens_Product", _ex.Message);
                throw _ex;
            }
        }

        //-------------------------------
        //
        //-------------------------------
        Types.Product Convert_Product_Xml(XmlDocument _XmlDoc)
        {

                Types.Product _Product = new Types.Product();
                Types.Product.ProductsRow _ProductsRow;
                XmlNode _Result;
                XmlNode _Tab;
                int _lb1, _lb2;
                
                try
                {
                    if (_XmlDoc != null)
                {
                    _Result = _XmlDoc.SelectSingleNode("RESULT");
                    if (_Result != null)
                    { 
                    _Tab = _Result.SelectSingleNode("TAB");
                        if (_Tab != null)
                        {
                            foreach (XmlNode _Line in _Tab.SelectNodes("LIN"))
                                
                            {
                                if (_Line != null)
                                {
                                    _ProductsRow = _Product.Products.NewProductsRow();

                                    _ProductsRow.Spn = GetFieldVal(_Line, "SUPPLIERAID");
                                    _ProductsRow.Name = (GetFieldVal(_Line, "DESCSHORT"));
                                    _ProductsRow.Description = (GetFieldVal(_Line, "DESCLONG"));
                                    _ProductsRow.Currency = GetFieldVal(_Line, "CURRENCY");
                                    _ProductsRow.Category1 = GetFieldVal(_Line, "CATEGORY1");
                                    _ProductsRow.Category2 = GetFieldVal(_Line, "CATEGORY2");
                                    _ProductsRow.Price = Convert.ToDouble(GetFieldVal(_Line, "PRIAMT1").Replace(",", "."), CultureInfo.InvariantCulture);
                                    _ProductsRow.PriceQuantity = Convert.ToDouble(GetFieldVal(_Line, "PRIQTY").Replace(",", "."), CultureInfo.InvariantCulture);
                                    _ProductsRow.OrderUnit = GetFieldVal(_Line, "ORDERUNIT");
                                    _ProductsRow.ContentUnit = GetFieldVal(_Line, "CONTENTUNIT");
                                    _ProductsRow.Manufacturer = GetFieldVal(_Line, "MFGNAME");
                                    _ProductsRow.Mpn = GetFieldVal(_Line, "MFGAID");
                                    _ProductsRow.LeadTime = Convert.ToDouble(GetFieldVal(_Line, "DLVTIME"), CultureInfo.InvariantCulture);
                                    _ProductsRow._Unspscs_Code = GetFieldVal(_Line, "UNSPSC");
                                    _ProductsRow.MimeInfo = GetFieldVal(_Line, "MIMEINFO");
                                    //_ProductsRow.QuantityMin = Convert.ToInt32(GetFieldVal(_Line, "LOWERBOUND1"), CultureInfo.InvariantCulture);
                                    _ProductsRow.QuantityInterval = Convert.ToInt32(GetFieldVal(_Line, "QTYINTERVAL"), CultureInfo.InvariantCulture);
                                    _ProductsRow.ScalePrice = Convert.ToDouble(GetFieldVal(_Line, "PRIAMT2").Replace(",", "."), CultureInfo.InvariantCulture);
                                    //_ProductsRow.QtyScale = Convert.ToInt32(GetFieldVal(_Line, "LOWERBOUND2"), CultureInfo.InvariantCulture);
                                    _ProductsRow.CuPerOu = Convert.ToDouble(GetFieldVal(_Line, "CONTENTCOEFF").Replace(",", "."), CultureInfo.InvariantCulture);
                                    _lb1 = Convert.ToInt32(GetFieldVal(_Line, "LOWERBOUND1"), CultureInfo.InvariantCulture);
                                    _lb2 = Convert.ToInt32(GetFieldVal(_Line, "LOWERBOUND2"), CultureInfo.InvariantCulture);

                                    if (_lb1 > _lb2)
                                    { 
                                        _ProductsRow.QuantityMin = _lb2;
                                        _ProductsRow.QtyScale = _lb1;
                                    }
                                    else
                                    {
                                        _ProductsRow.QuantityMin = _lb1;
                                        _ProductsRow.QtyScale = _lb2;
                                    }

                                    _Product.Products.AddProductsRow(_ProductsRow);
                                }
                            }
                            return _Product;
                        }
                        else
                        { return null; }
                    }
                    else
                    { return null; }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception _ex)
            {
                _Helper.PutLog(1, " Sage_Data.Convert_Product_Xml", _ex.Message );
                throw _ex;
            }
        }

        //-------------------------------
        //
        //-------------------------------
        string GetFieldVal(
          XmlNode _ParentNode 
          , String _FieldName 
          )
        {
            try
            {
                //XmlNode _field;
                //XmlAttribute _attribute;
                Boolean _found;
                string _Value = null;

                _found = false;

                foreach (XmlNode _field in _ParentNode.ChildNodes)
                {
                    foreach (XmlAttribute _attribute in _field.Attributes)
                    {
                        if (_attribute.Name == "NAME" & _attribute.Value == _FieldName)
                        {
                            if (_field.InnerXml.Trim(null).Length > 0)
                            {
                                _Value = _field.InnerText.Trim(null).ToString();
                            }
                            else
                            { _Value = null; }

                            _found = true;
                            break;
                        }
                    }
                    if(_found)
                    { break; }
                }

                return _Value;
            }
            catch (Exception _ex)
            {
                _Helper.PutLog(1, " Sage_Data.GetFieldVal", _ex.Message);
                throw _ex;
            }

        }

        //-------------------------------
        //
        //-------------------------------
        string Remove_Quotes(string Input)
        {
            try
            {
                string tmp_string;

                tmp_string = Input.Trim();

                tmp_string = tmp_string.Replace('"', 'i');
                tmp_string = tmp_string.Replace('"', 'i');
                tmp_string = tmp_string.Replace("'", "f");
                tmp_string = tmp_string.Replace("'", "f");

                return tmp_string;
            
            }
            catch
            {
                return null;
            }
        
        
        }
    }
}
