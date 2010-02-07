using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CtaCteProveedor
/// </summary>
public class CtaCteProveedor
{
	public CtaCteProveedor( string _numCtaCtePro,
                            string _debe,
                            string _haber,
                            string _saldo,
                            string _idProveedor)
	{
        numCtaCteProv = Convert.ToInt32(_numCtaCtePro);
        debe = float.Parse(_debe);
        haber = float.Parse(_haber);
        saldo = float.Parse(_saldo);
        idProveedor = Convert.ToInt32(_idProveedor);
	}

    #region propiedades cta cte 
    /// <summary>
    /// ID del proveedor
    /// </summary>
    private int numCtaCteProv;
    public int NumCtaCteProv
    {
        get { return numCtaCteProv; }
    }

    /// <summary>
    /// Debe del prov
    /// </summary>
    private float debe;
    public float Debe
    {
        get { return debe; }
    }

    /// <summary>
    /// Haber del prov
    /// </summary>
    private float haber;
    public float Haber
    {
        set { haber = value; }
        get { return haber; }
    }

    /// <summary>
    /// Saldo del prov
    /// </summary>
    private float saldo;
    public float Saldo
    {
        set { saldo = value; }
        get { return saldo; }
    }

    /// <summary>
    /// ID del proveedor
    /// </summary>
    private int idProveedor;
    public int IdProveedor
    {
        set { idProveedor = value; }
        get { return idProveedor; }
    }

    #endregion
}
