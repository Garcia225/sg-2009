using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

/// <summary>
/// Summary description for Serializador
/// </summary>
public class Serializador
{
	public Serializador()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    //metodo que serializa el contenido de un datatable
    //que luego es utlizado por el cliente para generar
    //las tablas dinamicas.

    /// <summary>
    /// Serializa el contenido del datatable para ser utilizado por el cliente
    /// </summary>
    /// <param name="Dt"></param>
    /// <returns></returns>
    public string JSON(DataTable Dt)
    {

        string[] StrDc = new string[Dt.Columns.Count];


        string HeadStr = string.Empty;
        for (int i = 0; i < Dt.Columns.Count; i++)
        {

            StrDc[i] = Dt.Columns[i].Caption;

            HeadStr += "\"" + StrDc[i] + i.ToString() + "¾" + "\",";

        }

        HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
        StringBuilder Sb = new StringBuilder();
        string aux = "";


        Sb.Append("[");
        for (int i = 0; i < Dt.Rows.Count; i++)
        {

            string TempStr = HeadStr;

            Sb.Append("[");

            for (int j = 0; j < Dt.Columns.Count; j++)
            {
                aux = Dt.Rows[i][j].ToString();

                //reemplazar expresiones regulares antes de enviar al cliente
                aux = aux.Replace("\"", "");
                aux = aux.Replace("\'", "");
                aux = aux.Replace("[", "");
                aux = aux.Replace("]", "");
                aux = aux.Replace("{", "");
                aux = aux.Replace("}", "");
                aux = aux.Replace("\\", "");

                TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", aux);


            }
            Sb.Append(TempStr + "],");

        }
        Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));

        //controlar si el datatable está vacio
        if (Sb.Length < 2)
            Sb.Append("[]");
        else
            Sb.Append("]");


        return Sb.ToString();

    }
}
