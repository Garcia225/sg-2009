using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// Summary description for PeriodosContables
/// </summary>
public class PeriodosContables
{
	public PeriodosContables()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //Get y set de periodos
    #region propiedades periodos

    /// <summary>
    /// ID del periodo
    /// </summary>
    private int periodo;
    public int Periodo
    {
        get { return periodo; }
    }

    /// <summary>
    /// Anho al que pertenece el periodo
    /// </summary>
    private int anho;
    public int Anho
    {
        set { anho = value; }
        get { return anho; }
    }

    /// <summary>
    /// Fecha de inicio del periodo
    /// </summary>
    private int fechaInicio;
    public int FechaInicio
    {
        set { fechaInicio = value; }
        get { return fechaInicio; }
    }

    /// <summary>
    /// Fecha del final del periodo
    /// </summary>
    private int fechaFinal;
    public int FechaFinal
    {
        set { fechaFinal = value; }
        get { return fechaFinal; }
    }

    #endregion

}
