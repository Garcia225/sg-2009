var oTableFactura;
var contFac = 0;
var contCta = 0;
var _idFactura = 0;
var _movCtaCte = 0;
var _datosCompletos;
var _cont = 0;

/*Lo primero ue hace la pagina al cargarse por primera vez*/
$(document).ready(function() {
    $("input[id*=tbSaldo]").removeAttr('disabled');
    $("input[id*=tbHaber]").removeAttr('disabled');
    $("input[id*=tbDebe]").removeAttr('disabled');
    $("input[id*=tbDireccion]").removeAttr('disabled');
    $("input[id*=tbDoc]").removeAttr('disabled');
    
    $("input[id*=tbSaldo]").attr('disabled', 'disabled'); 
    $("input[id*=tbHaber]").attr('disabled', 'disabled'); 
    $("input[id*=tbDebe]").attr('disabled', 'disabled'); 
    $("input[id*=tbDireccion]").attr('disabled', 'disabled'); 
    $("input[id*=tbDoc]").attr('disabled', 'disabled');
    
    autocompleteProveedor();
});

/*Trae las facturas vencidas del proveedor*/
function traerVencimiento(idProveedor){
    var fecha = fechaActual();
    //inicia AJAX
	$.ajax({
        type:"POST", 
        dataType: "json", 
        contentType: "application/json; charset=utf-8",
        url:"Pagos.aspx/facturasVencidadas",  //invocar al metodo del servidor que devulve un datatable 
        data:"{'fecha':'"+ fecha +"', " + 
             "'idProveedor':'"+idProveedor+"'}",
        success:  function armarTabla(datatable_servidor){//ver el funcionamiento
            //obtengo el id de la tabla donde se generará la tabla dinamica
            tablaPersona = $('#tablaPersonas').dataTable({
		  	    "aaData": eval(datatable_servidor),     //armar tabla con el arreglo serializado del serividor
		  	    "aoColumns": [ 
		  	            { "sTitle": "Num doc" },// num_doc, fecha, total_factura              
				        { "sTitle": "Fecha" },
						{ "sTitle": "Total" }                   
				 ],
		  	    "oLanguage": {                    //setear variables por defecto al idioma español
                    "sProcessing": "Procesando...",              
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sInfo": "(_START_-_END_) de _TOTAL_ registros",
                    "sInfoEmpty": "(0-0) de 0 registros",
                    "sInfoFiltered": "(_MAX_ registros en total)",
                    "sSearch": "Buscar:"
                }
            });
        },  
        //tirar mensaje de error en caso que la llamada ajax tenga problemas
        error: function(msg){ 
            alert("Se ha producido un error en la llamada ajax al servidor");
        }
    });//fin de llamada ajax
	//final evento AJAX
}

//Obtiene y carga en el campo Fecha Alta la fecha del dia
function fechaActual(){
    var currentTime = new Date();
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    if (day<10)
        day="0"+day
    if(month<10)
        month="0"+month
        var fecha = day+ "/" +month+ "/" + year;
        
        return fecha;
       // $("input[id*=tbFecha]").val(day+ "/" +month+ "/" + year);
}


/*Funcion que autocompleta el campo proveedor*/
function autocompleteProveedor() {
      //llamada ajax para obtener proveedores
        var urlNombreProveedor = "Compras.aspx/GetNombreProveedor";
       $.ajax({
            type:"POST",       
            dataType: "json", 
            contentType: "application/json; charset=utf-8",
            url: urlNombreProveedor, 
            data:"{}", 
            success: function(data){
                  //asginar objeto proveeedores obtenido del servidor
                 //al plugin autcomplete 
                 $("input[id*=tbProveedor]").autocomplete(eval(data), {
                    minChars: 0,
                    width: 310,
                    max:5,
                    matchContains: true,
                    mustMatch: false,
                    autoFill: false,
                    formatItem: function(row, i, max) { //alert(row.id_proveedor);
                        $("input[id*=tbDoc]").val("");
                        $("input[id*=tbDireccion]").val("");
                        //oTableFactura.fnClearTable(oTableFactura);
                        return row[0] + " "+ row[1]+" "+row[2];
                    },
                    formatMatch: function(row, i, max) {
                        $("input[id*=tbDoc]").val("");
                        $("input[id*=tbDireccion]").val("");
                        //oTableFactura.fnClearTable(oTableFactura);
                        return row[0] +"|"+ row[1]+"|"+row[2];
                    },
                    formatResult: function(row) {
                        //_idProveedor = row[0];
                        //return row[1]+" "+row[2]+" "+row[3];
                       // _datosCompletos = row[1]+" "+row[2]+" "+row[3];
                        return row[1];
                    }
                    }).result(function(e, i, row) {
	                	obtenerResultadoProveedor(row);
                    });
               
            },
             //lanzar mensaje de error en caso que la llamada ajax tenga problemas
            error: function(msg){ 
                alert("Se ha producido un Error");
            }    
        });// fin .ajax proveedor
        
        
        //reactivar evento
        var inputTbProveedor = $("input[id*=tbProveedor]");
    
        
       // $("input[id*=txtNotaDescuentos]").focus();
        /*if($("input[id*=tbDireccion]").val() = ""){
            $("input[id*=tbProveedor]").val("");
        }else{
            alert("Existe el proveedor");
        }
*/
         $("input[id*=tbProveedor]").blur(function() {
             inputTbProveedor.search(
                function(result) {
                    //alert(result.val);
                    // if no value found, clear the input box
                    if (typeof (result) == 'undefined') {
                    //if($("input[id*=tbDoc]").val() = ""){
                        inputTbProveedor.val("");
                    }
                    //
                });
            });
        
    return false;
}

/*Obtiene el ID del proveedor seleccionado*/
function obtenerResultadoProveedor(row){
    _cont = _cont+1;
    //obtener el codigo del proveedor
    var codigo = row.split("|")[0];
    //asignar codigo a variable global
    _idProveedor = codigo;
    rellenarCamposProveedor(_idProveedor);
    getCtaCte(_idProveedor);
    
    if(contFac == 0){
      traerFacturas(_idProveedor);
    }else{
    //alert("Recarga");
      recargar(_idProveedor);
    }
    contFac = contFac+1;
}

//Rellena los campos con los datos del proveedor seleccionado
function rellenarCamposProveedor(idProveedor) {

    var urlGetDataSet = "Compras.aspx/DatosProveedor";

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: urlGetDataSet,  //invocar al metodo del servidor que devulve un datatable
        data: "{'idProveedor':'" + idProveedor + "'}",
        success: function(obtener_return_val) {

            // Decodifica la cadena obtenida y lo transforma en un objeto producto
            var proveedor = JSON.decode(obtener_return_val);
            /*razonSocial", "" + datos[1]);
            _values.Add("nombre", "" + datos[2]);
            _values.Add("apellido */
            //$("input[id*=tbProveedor]").val(proveedor.nombre +" "+ proveedor.apellido +" - "+ proveedor.razonSocial);
            $("input[id*=tbDireccion]").val(proveedor.direccion);
            $("input[id*=tbDoc]").val(proveedor.numDoc);
            return false;

        },
        //tirar mensaje de error en caso que la llamada ajax tenga problemas
        error: function(msg) {
            alert("Se ha producido un Error" + urlGetDataSet);
        }
    });  //fin de llamada ajax

   /* if($("input[id*=tbDoc]").val() = ""){
        $("input[id*=tbProveedor]").val("");
    }
*/
    return false;
}

/*Carga la tabla dinamica con las facturas pendientes de ese proveedor*/
function traerFacturas(idProveedor){
//oTableFactura.fnClearTable( oTableFactura );
$.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "Pagos.aspx/facturasPendientes",  //invocar al metodo del servidor que devuelve un datatable
        data: "{'idProveedor':'" + idProveedor + "'}",
        success: function armarTabla(datatable_servidor) {
            //id_factura, num_factura, fecha, total_factura
            //obtengo el id de la tabla donde se generará la tabla dinamica
            oTableFactura = $("table[id*=tablaFacturas]").dataTable({
                "aaData": eval(datatable_servidor),     //armar tabla con el arreglo serializado del servidor
                "aoColumns": [
                    { "sTitle": "", "bSortable": false,
                        "fnRender": function(obj) {
                            var idFactura = obj.aData[0];
                            _idFactura = idFactura;
                            var sReturn = '<img id="' + idFactura + '" src="../images/add.png" alt="" onClick="showDetail(this.id);" style="cursor:pointer"/>';
                            return sReturn;
                        }
                    },
                    {"sTitle": "Nro Factura" },   //crear titulos de las columnas    
                    {"sTitle": "Total Factura" },
				    {"sTitle": "Fecha" }
				],
                "oLanguage": {                    //setear variables por defecto al idioma español
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros.",
                    "sZeroRecords": "No se encontraron resultados",
                    "sInfo": "(_START_-_END_) de _TOTAL_ registros",
                    "sInfoEmpty": "(0-0) de 0 registros",
                    "sInfoFiltered": "(_MAX_ registros en total)",
                    "sSearch": "Buscar:"
                }

            });
            //esconder imagenes 
            //$("#imgLoad").hide();
        },
        //lanzar mensaje de error en caso que la llamada ajax tenga problemas
        error: function(msg) {
            alert("Se ha producido un error en la llamada ajax al metodo:" + urlGetDataSet);
        }
    });  //fin de llamada ajax
    return false;

}

/*Carga los campos de debe y haber de un proveedor*/
function traerDebeHaberProveedor(){
        var urlGetDataSet = "Compras.aspx/DatosProveedor";

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: urlGetDataSet,  //invocar al metodo del servidor que devulve un datatable
        data: "{'idProveedor':'" + idProveedor + "'}",
        success: function(obtener_return_val) {

            // Decodifica la cadena obtenida y lo transforma en un objeto producto
            var proveedor = JSON.decode(obtener_return_val);

            $("input[id*=tbDireccion]").val(proveedor.direccion);
            $("input[id*=tbDoc]").val(proveedor.numDoc);
            return false;

        },
        //tirar mensaje de error en caso que la llamada ajax tenga problemas
        error: function(msg) {
            alert("Se ha producido un Error" + urlGetDataSet);
        }
    });  //fin de llamada ajax

  

    return false;

}

///////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////DETAILS////////////////////////////////////////////////////
/* Formating function for row details */
function listarCuotas(idFactura, fila) {
    //llamada ajax al servidor para generar el datatable cabecera
    //alert(idFactura);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "Pagos.aspx/obtenerCuotas",  //invocar al metodo del servidor que devulve un datatable
        data:"{'idFactura':'"+ idFactura + "'}",
        success: function armarTabla(datos) {
            /* se encarga de mostrar el detalle debajo de la linea seleccionada
            y de traer el detalle de la factura*/
            //parametros(NodoSeleccionado, funcion, clase css)
             oTableFactura.fnOpen(fila, armarDetalle(datos, idFactura), 'details');

             oTableCuota = $("table[id*=detalle" + idFactura + "]").dataTable({
                    "aaData": eval(datos),     //armar tabla con el arreglo serializado del serividor
                    "aoColumns": [//cuotas, numCuotas, importe, saldo, fechaVencimiento, idFactura, ctaCtePro
                    { "sTitle": "Codigo" },      //crear titulos de las columnas
                    { "sTitle": "Nro. Cuota" },
                    { "sTitle": "Importe" },
                    { "sTitle": "Saldo" },
                    { "sTitle": "Fecha de Vencimiento" },
                   { "sTitle": "Pago Parcial", "bSortable": false,
                    "fnRender": function(obj){
                        var monto = obj.aData[0];
                        var retornado = '<input id ="'+monto+'" type = "text"/>';
                        $("input[id*="+monto+"]").numeric();
                        return retornado;
                    }
                    },
                     { "sTitle": "Seleccionar", "bSortable": false,
                    "fnRender": function(obj) {
                        var idFacturaDet = obj.aData[0];
                        var sReturn = '<input id="' + idFacturaDet + '" type="checkbox"/>';
                        return sReturn;
                        }
                    }                
                    ],
                    "oLanguage": {                    //setear variables por defecto al idioma español
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sInfo": "(_START_-_END_) de _TOTAL_ registros",
                    "sInfoEmpty": "(0-0) de 0 registros",
                    "sInfoFiltered": "(_MAX_ registros en total)",
                    "sSearch": "Buscar:"
                    }

              });    
        },
        //lanzar mensaje de error en caso que la llamada ajax tenga problemas
        error: function(msg) {
            alert("Se ha producido un error en la llamada ajax al metodo:" + urlGetDataSet);
        }
    }); //fin de llamada ajax

    return false;
}


function armarDetalle(datos, idFactura) {
    //alert("La nueva tabla");
    //alert("detalle" + idFactura);
    //var html = '<p style="padding-left:50px; font-size:12pt"><u>Cuotas</u></p>';
    var html = '<label style="font-weight: bold; color: gray; text-decoration: underline">Cuotas</label>';
    html += '<div style="padding-left:50px;padding-bottom:15px"><table width="100%" cellpadding="5" cellspacing="0" border="1" id="detalle' + idFactura + '">';
    //cerrar tags
    html += '</table><br/><br/><button style="float:right" id="btnPagar" OnClick="prepararCuota('+ idFactura +'); return false;">Pagar</button><br/></div>';
    return html;
}



function showDetail(idFactura) {
    //alert("detalle");
    //alert("idFactura "+idFactura);
    /*alert($("img[id*=" + idFactura + "]").attr('src'));*/
    //obtengo la fila del boton seleccionado
    
    
    var fila = $("img[id*=" + idFactura + "]").parents('tr:first');
    //_nTr = fila;
    if ($("img[id*=" + idFactura + "]").attr('src') == '../images/add.png') {
 
        $("img[id*=" + idFactura + "]").attr('src', '../images/minus.png');
        //funcion que crea la tabla del detalle
        //alert("nTr -> "+nTr);
        
        listarCuotas(idFactura, fila);
        _movCtaCte = getMovCtaCte(idFactura);
        //alert("Factura "+_movCtaCte);
        //alert("Muestra el detalle");
        
    } else {
       $("img[id*=" + idFactura + "]").attr('src', '../images/add.png');
        var nRemove = $(fila).next()[0];
        //remover el detalle
        nRemove.parentNode.removeChild(nRemove);
        //alert("Oculta el detalle");
    }
    //alert("Show detail");
    return false;
}



/*Actualiza la tabla*/
function recargar(idProveedor)
{
    $.ajax({
        type:"POST", 
        dataType: "json", 
        contentType: "application/json; charset=utf-8",
        url:"Pagos.aspx/facturasPendientes",  //invocar al metodo del servidor que devulve un datatable 
        data: "{'idProveedor':'" + idProveedor + "'}",
        success:  function armarTabla(json){//ver el funcionamiento
            //vacia la tabla
            oTableFactura.fnClearTable(oTableFactura);
            //recarga con los nuevos datos
            oTableFactura.fnAddData(eval(json));
            //repintar la tabla  
            this.fnDraw(that);
          }
          });
}

/*Prepara la cuota a ser pagada*/
function prepararCuota(idFactura){
    //var idMovCtaCtePro = getMovCtaCte(idFactura);
    //alert("_movCtaCte "+_movCtaCte);
    var i = 0;
    //recorrer tabla detalle de factura seleccionada
    $('#detalle' + idFactura + ' tbody tr').each(function() {
        //Guarda la fila selecciona
        var fila = $('td', this);
        //obtener datos de la fila actual
        var datos = oTableCuota.fnGetData(i);
        i++;
        // 0  es la posicion del checkbox en la tabla dinamica
        var idCuota = ($(datos[6]).attr('id'));
        // campo editable es 0
        var monto = ($(datos[5]).attr('id'));
        //crear arreglo bidimensional que será enviado como el detalle
        var filaArray = new Array();
        
        if ($("input[id=" + idCuota + "]").is(':checked')) {
        importe = document.getElementById(monto).value; 
        //alert("importe "+importe);
        
        idCuota = datos[0];
        //alert(idCuota);
        importeTotal = datos[3]
        pagarCuotas(idCuota, importeTotal, _movCtaCte);
        
        return false;
        if(importe == ""){
            //alert("todo "+importeTotal);
            pagarCuotas(idCuota, importeTotal, _movCtaCte);
        }else if (isNaN(importe)){
        alert("");
            if(importe < importeTotal){
              pagarCuotas(idCuota, importe, _movCtaCte);
            }else{
                         $(function() {
	    $("div[id*=alertMonto]").dialog({
		    bgiframe: true,
		    resizable: false,
		    modal: true,
		    hide: true,
		    width: 380,
		    overlay: {
			    backgroundColor: '#000',
			    opacity: 0.5
		    },
	        close: function() {
			    $(this).dialog('destroy');
		    },
		    buttons: {
		    'Aceptar': function(){
		        $("input[id*="+_idFactura+"]").val('disabled');
		        $(this).dialog('destroy');
		        
		    }
		    }
	    });
    });
            
            }
            //alert("parcial "+importe);
            //alert("idCuota "+idCuota);
            
            //var idMovCtaCtePro = getMovCtaCte(idCuota);
            //alert("idMovCtaCtePro "+idMovCtaCtePro);
            //interes = datos[4];
            //montoPagado = datos[5];
            
            //alert("importe "+importe);
            //alert("montoParcial "+montoParcial);
            //getMovCtaCte(idCuota, montoParcial);
            
            
            
            }else{
            
                         $(function() {
	    $("div[id*=noNum]").dialog({
		    bgiframe: true,
		    resizable: false,
		    modal: true,
		    hide: true,
		    width: 380,
		    overlay: {
			    backgroundColor: '#000',
			    opacity: 0.5
		    },
	        close: function() {
			    $(this).dialog('destroy');
		    },
		    buttons: {
		    'Aceptar': function(){
		        //$("input[id*="+_idFactura+"]").val('disabled');
		        $(this).dialog('destroy');
		        
		    }
		    }
	    });
    });
    
            
            }
        }
    });
    return false;
}

/*Paga una cuota*/
function pagarCuotas(idCuota, importe, idMovCtaCtePro){
var fecha = fechaActual();
//alert("Fecha "+fecha);
         $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "Pagos.aspx/pagosCuotas",  //invocar al metodo del servidor que devulve un datatable
        data:"{" +//, , , 
             "'idPagoCuota':'0', " + 
             "'idCuota':'"+ idCuota + "', " + 
             "'importe':'"+ importe + "', " + 
             "'fecha':'"+ fecha + "', " + 
             "'idMovCtaCtePro':'" + idMovCtaCtePro + "'}",
        success: function armarTabla(datos) {
             
//alert("Pagado...!!!!");
        },
        //lanzar mensaje de error en caso que la llamada ajax tenga problemas
        error: function(msg) {
            alert("Se ha producido un error en la llamada ajax al metodo:" + urlGetDataSet);
        }
    }); //fin de llamada ajax

    return false;
}


function getMovCtaCte(idFactura){
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "Pagos.aspx/getMov",  //invocar al metodo del servidor que devulve un datatable
        data: "{'idFactura':'" + idFactura + "'}",
        success: function(obtener_return_val) {
            // Decodifica la cadena obtenida y lo transforma en un objeto producto
            var mov = JSON.decode(obtener_return_val);
            _movCtaCte = mov.movimiento
            return mov.movimiento;

        },
        //tirar mensaje de error en caso que la llamada ajax tenga problemas
        error: function(msg) {
            alert("Se ha producido un Error" + urlGetDataSet);
        }
    });  //fin de llamada ajax
 return false;
}

//Obtiene y carga en el campo Fecha Alta la fecha del dia
function fechaActual(){
    var currentTime = new Date();
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    if (day<10)
        day="0"+day
    if(month<10)
        month="0"+month;
        return day+ "/" +month+ "/" + year;
}


/*Trae los datos de la persona desde el servidor serializados*/
function getCtaCte(idProveedor)
{
//alert("El ID es "+idProveedor_);
        $.ajax({
        type:"POST", 
        dataType: "json", 
        contentType: "application/json; charset=utf-8",
        url:"Pagos.aspx/getCtaCtePro",  // Metodo del servidor que devulve la marca
        data:"{'idProveedor':'" + idProveedor + "'}",
        success:  function (obtener_return_val){
            var ctaCte = JSON.decode(obtener_return_val);
            //_idProveedor = persona.idProveedor;
            $("input[id*=tbDebe]").val(ctaCte.debe);
            $("input[id*=tbHaber]").val(ctaCte.haber);
            $("input[id*=tbSaldo]").val(ctaCte.saldo);
            
        },   
        //Error en caso de que ocurra algun problema
        error: function(msg){ 
            alert("Se ha producido un error en la llamada ajax al servidor");
        }
   });//fin ajax

    return false;
}