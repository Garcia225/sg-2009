var oTablaDetalle;
var item = 0;
var totalFactura = 0;
var _idProveedor = 0;
var _estado = "";
var _idBanco = 0;
var _movIdCtaCte = 0;

$(document).ready(function() {
//btEditar

$("input[id*=tbNum]").numeric();
$("input[id*=tbCant]").numeric();
$("input[id*=btEditar]").css("display", "none");
$("input[id*=btEliminar]").css("display", "none");
$("input[id*=btGuardar]").css("display", "none");
$("input[id*=btCancelar]").css("display", "none");

$("#divCuotas").slideUp();
        //control de checkbox condicion de pago
     $("#cbCredito").change(function(){
        _estado = "Credito";
        alert(_estado);
        if ($("#cbCredito").is(":checked")) {
          document.forms[0].cbContado.checked = false;
          $("#lbContado").removeClass("LabelSelected");
          $("#lbCredito").addClass("LabelSelected");
          $("#divCuotas").slideDown();
        } else {
          document.forms[0].cbContado.checked = true;
          $("#lbContado").addClass("LabelSelected");
          $("#lbCredito").removeClass("LabelSelected");
          $("#divCuotas").slideUp();
        }    
      });
      
          //control de checkbox condicion de pago
     $("#cbContado").change(function(){
      _estado = "Contado"
      alert(_estado);
        if ($("#cbContado").is(":checked")) {
          document.forms[0].cbCredito.checked = false;
          $("#lbCredito").removeClass("LabelSelected");
          $("#lbContado").addClass("LabelSelected");
          $("#divCuotas").slideUp();
        } else {
          document.forms[0].cbCredito.checked = true;
          $("#lbCredito").addClass("LabelSelected");
          $("#lbContado").removeClass("LabelSelected");
          $("#divCuotas").slideUp();
        }    
      });
      
      //Evento click
          $("#tablaDetalle tbody").click(function(event) {
        $(oTablaDetalle.fnSettings().aoData).each(function() {
            $(this.nTr).removeClass('row_selected');
        });
        $(event.target.parentNode).addClass('row_selected');
        var tds = $(event.target.parentNode).children('td'); 
        var cantidad  = $(tds[0]).text(); 
        
        $("input[id*=btEditar]").css("display", "inline");
        $("input[id*=btEliminar]").css("display", "inline");
        $("input[id*=btAgregar]").css("display", "none");
        $("input[id*=btCancelar]").css("display", "none");
        
        
       $("select[id*=chComponente]").removeAttr('disabled');
       $("input[id*=tbCant]").removeAttr('disabled');
       $("select[id*=chComponente]").attr('disabled', 'disabled'); 
       $("input[id*=tbCant]").attr('disabled', 'disabled');

        
        //alert("Entro");
        //parseInt(cantidad)
        $("input[id*=tbCant]").val(cantidad);
        // Si no está en modo edición carga los campos con estos datos
        if (!($("input[id*=tbCant]").is(":enabled"))) {
                $("input[id*=tbCant]").val(cantidad);
        }
    }); 
    autocompleteProveedor();
    crearTablaDetalle();
    fechaActual();
    deshabilitarCampos();
});

function editarDetalle(){
    $("select[id*=chComponente]").removeAttr('disabled');
    $("input[id*=tbCant]").removeAttr('disabled');
    $("input[id*=btGuardar]").css("display", "inline");
    $("input[id*=btEliminar]").css("display", "none");
    $("input[id*=btCancelar]").css("display", "inline");
    $("input[id*=btEditar]").css("display", "none");
    return false;
}

function cancelarDetalle(){
    $("select[id*=chComponente]").removeAttr('disabled');
    $("input[id*=tbCant]").removeAttr('disabled');
    $("input[id*=btAgregar]").css("display", "inline");
    $("input[id*=btGuardar]").css("display", "none");
    $("input[id*=btEditar]").css("display", "none");
    $("input[id*=btCancelar]").css("display", "none");
    return false;
}

function guardarDetalle(){
        //4ft5vhcf
        var anSelected = fnGetSelected( oTablaDetalle );
        var nFila = oTablaDetalle.fnGetPosition( anSelected[0]);
        //oTablaDetalle.fnDeleteRow(iRow);
        
        var idComp =  $("select[id*=chComponente]").val();
        var cant = $("input[id*=tbCant]").val();
        
        //alert("hgfghjk "+idComp);
        
        oTablaDetalle.fnUpdate(idComp, nFila, 0);
        oTablaDetalle.fnUpdate(cant, nFila, 2);
        return false;
}

/* crear tabla dinamica para el detalle */
function crearTablaDetalle() {
    var aaData = "[]";

    //genera tabla dinamica vacia
    oTablaDetalle = $("table[id*=tablaDetalle]").dataTable({
        "aaData": eval(aaData),
        "bFilter":false,
		"bPaginate":false,
        "aoColumns": [
            //{ "sTitle": "Item" },
            { "sTitle": "Cantidad" }, 
		    { "sTitle": "Codigo" },          //crear titulos de las columnas
		    { "sTitle": "Componente" },
		    { "sTitle": "Costo Unit." },
		    { "sTitle": "Total" },
		    ],
        "oLanguage": {                    //setear variables por defecto al idioma español
            "sProcessing": "Procesando...",
            "sZeroRecords": "No se cargaron detalles",
            "sInfo": "(_START_-_END_) de _TOTAL_ detalles"
        }

    });
    return false;
}


/*Funcion que habilita el automplete del campo proveedor*/
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
                    formatItem: function(row, i, max) {
                        return row[0] + " "+ row[1];
                    },
                    formatMatch: function(row, i, max) {
                        return row[0] +"|"+ row[1];
                    },
                    formatResult: function(row) {
                        _idProveedor = row[0];
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

         $("input[id*=tbProveedor]").blur(function() {
             inputTbProveedor.search(
                function(result) {
                    // if no value found, clear the input box
                    if (typeof (result) == 'undefined') {
                        inputTbProveedor.val("");
                    }
                });
            });
        
    return false;
}


//
function obtenerResultadoProveedor(row){
    //obtener el codigo del proveedor
    var codigo = row.split("|")[0];
    //asignar codigo a variable global
    _idProveedor = codigo;
    alert("EL ID DEL PROVEEDORE ESSSSSSSSSSS ->"+_idProveedor);
    rellenarCamposProveedor(_idProveedor);
    _movIdCtaCte = getIdMovCtaCte(_idProveedor);
    alert("el movimiento es "+_movIdCtaCte);
}


/*Funcion que habilita el automplete del campo cheque*/
function autocompleteBancos() {
      //llamada ajax para obtener proveedores
      $("span[id*=lbValor]").text("Banco");
      
        var urlNombreProveedor = "Compras.aspx/GetNombreProveedor";
       $.ajax({
            type:"POST",       
            dataType: "json", 
            contentType: "application/json; charset=utf-8",
            url: "Compras.aspx/GetBancos", 
            data:"{}", 
            success: function(data){
                  //asginar objeto proveeedores obtenido del servidor
                 //al plugin autcomplete 
                 $("input[id*=tbBanco]").autocomplete(eval(data), {
                    minChars: 0,
                    width: 310,
                    max:5,
                    matchContains: true,
                    mustMatch: false,
                    autoFill: false,
                    formatItem: function(row, i, max) {
                        return row[0] + " "+ row[1];
                    },
                    formatMatch: function(row, i, max) {
                        return row[0] +"|"+ row[1];
                    },
                    formatResult: function(row) {
                        _idProveedor = row[0];
                        return row[1];
                    }
                    }).result(function(e, i, row) {
	                	//obtenerResultadoProveedor(row);
	                	_idBanco = row.split("|")[0];
                    });
               
            },
             //lanzar mensaje de error en caso que la llamada ajax tenga problemas
            error: function(msg){ 
                alert("Se ha producido un Error");
            }    
        });// fin .ajax proveedor
        //reactivar evento
        var inputTbProveedor = $("input[id*=tbBanco]");

         $("input[id*=tbBanco]").blur(function() {
             inputTbProveedor.search(
                function(result) {
                    // if no value found, clear the input box
                    if (typeof (result) == 'undefined') {
                        inputTbProveedor.val("");
                    }
                });
            });
        
    return false;
}

/*Funcion que habilita el automplete del campo tarjeta*/
function autocompleteTarjeta() {
    $("span[id*=lbValor]").text("Targeta de Credito");
       $.ajax({
            type:"POST",       
            dataType: "json", 
            contentType: "application/json; charset=utf-8",
            url: "Compras.aspx/GetTargetas", 
            data:"{}", 
            success: function(data){
                  //asginar objeto proveeedores obtenido del servidor
                 //al plugin autcomplete 
                 $("input[id*=tbValor]").autocomplete(eval(data), {
                    minChars: 0,
                    width: 310,
                    max:5,
                    matchContains: true,
                    mustMatch: false,
                    autoFill: false,
                    formatItem: function(row, i, max) {
                        return row[0];
                    },
                    formatMatch: function(row, i, max) {
                        return row[0] +"|";
                    },
                    formatResult: function(row) {
                        _idProveedor = row[0];
                        return row[0];
                    }
                    }).result(function(e, i, row) {
	                	//obtenerResultadoProveedor(row);
                    });
               
            },
             //lanzar mensaje de error en caso que la llamada ajax tenga problemas
            error: function(msg){ 
                alert("Se ha producido un Error");
            }    
        });// fin .ajax proveedor
        //reactivar evento
        var inputTbProveedor = $("input[id*=tbValor]");

         $("input[id*=tbValor]").blur(function() {
             inputTbProveedor.search(
                function(result) {
                    // if no value found, clear the input box
                    if (typeof (result) == 'undefined') {
                        inputTbProveedor.val("");
                    }
                });
            });
        
    return false;
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

/*funcion que agrega una nueva fila a la tabla detalle*/
function fnAddRow() {
  if ($("input[id*=tbCant]").val() == "" || $("input[id*=tbCant]").val() == 0) {
        $("input[id*=tbCant]").val('');
        $("input[id*=tbCant]").focus();
    }else {
        var idComp =  $("select[id*=chComponente]").val();
        var componente;
        var costo;
        $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "Compras.aspx/getCostoComponente",  //invocar al metodo del servidor que devulve un datatable
        data: "{'idComponente':'" + idComp + "'}",
        success: function(obtener_return_val) {
            // Decodifica la cadena obtenida y lo transforma en un objeto producto
            var costoComp = JSON.decode(obtener_return_val);
            costo = costoComp.costo;
            
            //var componente = nombreMatPrima(idComp);
            //alert(componente);
            //var componente = $("select[id*=chComponente]").text();
            var cantidad = parseInt($("input[id*=tbCant]").val());
            var total = costo*cantidad;
            totalFactura = totalFactura + total;
            componente = costoComp.descripcion;
            $("input[id*=tbTotal]").val(totalFactura);

        //Agrego en la tabla detalle
       oTablaDetalle.fnAddData( [
            cantidad + "",
            idComp + "",
            componente + "",
            costo + "",
            total+""]);
            var serial = cargarArreglo();
            return false;

        },//tira un error en caso de haya un problema con el servidor
        error: function(msg) {
            alert("Se ha producido un Error");
        }
    });  //fin ajax
    }
    
    return false;
}

//Rellena los campos con los datos del proveedor seleccionado
function guardarFactura() {
    var condicion_pago = 1;
    /*if($("#lbCredito").attr("class") == "CheckBoxLabelClass LabelSelected"){
        var condicion_pago = 2;
        alert("Credito");
    }*/
    alert("Esta dentro de guardar");
    
        
    var id_factura = '1';
    var proveedor = _idProveedor;//$("select[id*=chProveedor]").val();
    alert("PROVEEDORRRRR "+_idProveedor);
    //return false;
    var fecha = $("input[id*=tbFecha]").val();
    var total_factura = $("input[id*=tbTotal]").val();
    var empleado = "1";
    var num_factura = $("input[id*=tbNum]").val();
    var num_cheque = $("input[id*=tbNroCheque]").val();
    var cant_cuotas;
    var sumaResta;
    //alert(_estado);
    //return false;
    
    var serial = cargarArreglo();
    var detalle_factura = serial;
    if(serial[3] == 'N'){
        alert('No se cargaron los detalles');
    }else{
    
    
    
    if ($("input[id*=cbCredito]").is(":checked")) {
        alert("Credito");
        opcion = "Credito";
        cant_cuotas = $("input[id*=tbCantCuotas]").val();
        sumaResta = "R";
        if(cant_cuotas == ""){
            alert("Ingrese la cantidad de Cuotas");
            return false;
        }
        //tbCantCuotas
    //return false;
    }else{
        alert("Contado");
        opcion = "Contado"; 
        sumaResta = "S"; 
        cant_cuotas = "0";
    }
    alert("num_cheque "+num_cheque+" _idBanco "+_idBanco+" opcion "+opcion);
    //return false;
    /*id_factura,  
    proveedor,  
    num_factura,  
    fecha, 
    total_factura,  
    condicion_pago,  
    empleado,  
    detalle_factura,  
    num_cheque,
    id_banco,  
    opcion, */
    alert("Suma o Resta "+sumaResta);
    var numCuota = 0;
    //var cantCuotas = $("input[id*=tbCantCuotas]").val();//
    var cantCuotas = cant_cuotas;
    var importe = $("input[id*=tbTotal]").val();//
    var saldo = importe;
    var fechaVencimiento = "01/01/2009"; 
    var idFormaPago = $("select[id*=chFormaPago]").val();//
    var idMovCtaCtePro = _movIdCtaCte;
    alert(idMovCtaCtePro);
    //return false;
          var parametros = "{" +
             "'id_factura':'"+ id_factura + "', "+ 
             "'proveedor':'"+ proveedor + "', " + 
             "'num_factura':'"+ num_factura + "', " + 
             "'fecha':'"+ fecha + "', " + 
             "'total_factura':'"+ total_factura +"', " + 
             "'condicion_pago':'"+ condicion_pago +"', " +
             "'empleado':'"+ empleado +"', " + 
             "'detalle_factura':'"+ detalle_factura +"', " + 
             "'num_cheque':'"+ num_cheque +"', " + 
             "'id_banco':'"+ _idBanco +"', " + 
             "'opcion':'"+ opcion +"', " + 
             "'numCuota':'"+ numCuota + "', " + 
             "'cantCuotas':'"+ cantCuotas +"', " + 
             "'importe':'"+ importe +"', " +
             "'saldo':'"+ saldo +"', " + 
             "'fechaVencimiento':'"+ fechaVencimiento +"', " +
             "'idFormaPago':'"+ idFormaPago +"', " + 
             "'sumaResta':'"+ sumaResta +"', " + 
             "'idMovCtaCtePro':'"+idMovCtaCtePro+"'}"; 
             
    /*string detalle_factura, string num_cheque,
        string id_banco, string opcion*/
        
     $.ajax({
            type:"POST",
            dataType: "json", 
            contentType: "application/json; charset=utf-8",
            url:"Compras.aspx/GuardarFactura",
            data: parametros, 
             success: function(data){
                if(data == 'OK'){
                
                             }else{
                             
                             }
                        }
                    });//fin ajax
                    }
                    return false;
}

//Devuelbe un arreglo serializado del detalle
function cargarArreglo(){
    var aTrs    = oTablaDetalle.fnGetNodes();
    var arreglo = new Array();
    var fila = new Array();
    var cont = 0;
   //"[[\"29\",200000,\"Pago total de la cuota Nº 1/2 de la factura 321\"],[\"30\",200000,\"Pago total de la cuota Nº 2/2 de la factura 321\"]]"
    //recorrer datatable detalle
    var res = '[';
    $('#tablaDetalle tbody tr').each(function() {
        if(cont != 0){
            res += ",";
        }
        
        var nTds = $('td', this);
        var cant = $(nTds[0]).text();
        var cod = $(nTds[1]).text();
        var costoUnit = $(nTds[3]).text();
        var total = $(nTds[4]).text();
        res += '[\"'+cant+'\\",'+cod+',\\"'+costoUnit+'\\",\\"'+total+'\\"]';

        var fila = new Array();
        fila.push(cant);
        fila.push(cod);
        fila.push(costoUnit);
        fila.push(total);
        arreglo.push(fila);
        cont++;
    });
    res += ']';
        return res;    
        // "[[\"29\",200000,\"Pago total de la cuota Nº 1/2 de la factura 321\"],[\"30\",200000,\"Pago total de la cuota Nº 2/2 de la factura 321\"]]"
 }

 
function getIdMovCtaCte(idFactura){
       $.ajax({
            type:"POST",
            dataType: "json", 
            contentType: "application/json; charset=utf-8",
            url:"Compras.aspx/movCtaCte",
            data: "{'idFactura':'"+idFactura+"'}", 
            success: function(data){
//                if(data == 'OK'){
//                
//                  }else{
//                             
//                  }
                }
        });//fin ajax
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
        month="0"+month
        $("input[id*=tbFecha]").val(day+ "/" +month+ "/" + year);
}


//Habilita los campos
function habilitarCamposFactura(){
    $("input[id*=tbCod]").removeAttr('disabled');
    $("input[id*=tbNum]").removeAttr('disabled');
    $("input[id*=tbProveedor]").removeAttr('disabled');
    $("input[id*=cbContado]").removeAttr('disabled');
    $("input[id*=cbCredito]").removeAttr('disabled');
    
    
    $("input[id*=cbCheque]").removeAttr('disabled');
    $("input[id*=cbTargeta]").removeAttr('disabled');
    $("input[id*=tbValor]").removeAttr('disabled');
    $("input[id*=tbNroCheque]").removeAttr('disabled');
    $("input[id*=tbBanco]").removeAttr('disabled');
    
    $("select[id*=chComponente]").removeAttr('disabled');
    $("input[id*=tbCant]").removeAttr('disabled');
    $("input[id*=tbTotal]").removeAttr('disabled');
    
    
    //Botones
    $("input[id*=btAgregar]").removeAttr('disabled');
    $("input[id*=btEditar]").removeAttr('disabled');
    $("input[id*=btEliminar]").removeAttr('disabled');
    
    $("input[id*=tbTotal]").removeAttr('disabled');
    $("input[id*=tbTotal]").attr('disabled', 'disabled');
    return false;
}

/*Deshabilita los campos del ABM*/
function deshabilitarCampos(){
    $("input[id*=tbCod]").removeAttr('disabled');
    $("input[id*=tbNum]").removeAttr('disabled');
    $("input[id*=tbFecha]").removeAttr('disabled');
    $("input[id*=cbContado]").removeAttr('disabled');
    $("input[id*=cbCredito]").removeAttr('disabled');
    $("select[id*=chProveedor]").removeAttr('disabled');
    $("input[id*=tbDoc]").removeAttr('disabled');
    $("input[id*=tbDireccion]").removeAttr('disabled');
    $("select[id*=chComponente]").removeAttr('disabled');
    $("input[id*=tbCant]").removeAttr('disabled');
    $("input[id*=tbTotal]").removeAttr('disabled');   
    $("input[id*=tbProveedor]").removeAttr('disabled');
    
    $("input[id*=cbCheque]").removeAttr('disabled');
    $("input[id*=cbTargeta]").removeAttr('disabled');
    $("input[id*=tbValor]").removeAttr('disabled');
    $("input[id*=tbNroCheque]").removeAttr('disabled');
    $("input[id*=tbBanco]").removeAttr('disabled');
    //Botones
    $("input[id*=btAgregar]").removeAttr('disabled');
    $("input[id*=btEditar]").removeAttr('disabled');
    $("input[id*=btEliminar]").removeAttr('disabled'); 
    
    $("input[id*=tbCod]").attr('disabled', 'disabled');
    $("input[id*=tbNum]").attr('disabled', 'disabled');
    $("input[id*=tbFecha]").attr('disabled', 'disabled'); 
    $("input[id*=cbContado]").attr('disabled', 'disabled'); 
    $("input[id*=cbCredito]").attr('disabled', 'disabled'); 
    $("select[id*=chProveedor]").attr('disabled', 'disabled'); 
    $("input[id*=tbDoc]").attr('disabled', 'disabled'); 
    $("input[id*=tbDireccion]").attr('disabled', 'disabled'); 
    $("select[id*=chComponente]").attr('disabled', 'disabled'); 
    $("input[id*=tbCant]").attr('disabled', 'disabled'); 
    $("input[id*=tbTotal]").attr('disabled', 'disabled'); 
    $("input[id*=tbProveedor]").attr('disabled', 'disabled'); 
    $("input[id*=cbCheque]").attr('disabled', 'disabled'); 
    $("input[id*=cbTargeta]").attr('disabled', 'disabled'); 
    $("input[id*=tbValor]").attr('disabled', 'disabled'); 
    $("input[id*=tbNroCheque]").attr('disabled', 'disabled'); 
    $("input[id*=tbBanco]").attr('disabled', 'disabled'); 
    
    //Botones
    $("input[id*=btAgregar]").attr('disabled', 'disabled'); 
    $("input[id*=btEditar]").attr('disabled', 'disabled'); 
    $("input[id*=btEliminar]").attr('disabled', 'disabled'); 
    
    
    return false;
}

//Habilita los campos para un nuevo registro
function nuevaFactura(){
    habilitarCamposFactura();
    deshabilitarBotones();
    $("input[id*=imgbtGuardar]").css("display", "inline");
    $("input[id*=imgbtCancel]").css("display", "inline");
    
}

//Deshabilita los campos
function cancelarFatura(){
    deshabilitarCampos();
    deshabilitarBotones();
    $("input[id*=imgbtNuevo]").css("display", "inline");
    $("input[id*=imgbtCancel]").css("display", "inline");
}


/*Deshabilita los botones del ABM*/
function deshabilitarBotones(){
    $("input[id*=imgbtNuevo]").css("display", "none");
    $("input[id*=imgbtEditar]").css("display", "none");
    $("input[id*=imgbtGuardar]").css("display", "none");
    $("input[id*=imgbtdMod]").css("display", "none");
    $("input[id*=imgbtBorrar]").css("display", "none");
    $("input[id*=imgbtCancel]").css("display", "inline");
    return false;
}



