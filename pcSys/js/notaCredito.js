// JScript File
var _idProveedor=0;
var _idFactura = 0;
var codigo=0;

$(document).ready(function(){
    fechaActual();
    limpiarCampos();
    deshabilitarCampos();
    deshabilitarBoton();
    $("input[id*=tbNumDoc]").removeAttr('disabled');
    $("input[id*=tbNumDoc]").attr('disabled','disabled');
    $("input[id*=tbDireccion]").removeAttr('disabled');
    $("input[id*=tbDireccion]").attr('disabled','disabled');
    autocompleteProveedor();    
    $("input[id*=tbFecha]").removeAttr('disabled');
    $("input[id*=tbFecha]").attr('disabled','disabled');
   // deshabilitarBoton();
    $("input[id*=tbNumNotaCredito]").numeric({allow:"()-"});
    $("input[id*=tbTotal]").numeric({allow:"-"});
    $("input[id*=tbCantCuotas]").numeric();
    
    $("input[id*=imgbtNuevo]").focus();
    $.ajax({
        type:"POST", 
        dataType: "json", 
        contentType: "application/json; charset=utf-8",
        url:"NotaCredito.aspx/notasCredito",  //invocar al metodo del servidor que devulve un datatable 
        data:"{}",
        success:  function armarTabla(datatable_servidor){//ver el funcionamiento
            //obtengo el id de la tabla donde se generará la tabla dinamica
            tablaNotaCredito = $('#tablaNotaCredito').dataTable({
		  	    "aaData": eval(datatable_servidor),     //armar tabla con el arreglo serializado del serividor
		  	    "aoColumns": [ 
		  	            { "sTitle": "N°" },              
				        { "sTitle": "Fecha" },
						{ "sTitle": "Total" },
						{ "sTitle": "Motivo" },
						{ "sTitle": "N° factura" },
						{ "sTitle": "Borrar", "bSortable": false,
                    "fnRender": function(obj) {
                        //var idFacturaDet = obj.aData[obj.iDataColumn];
                        var idNota = obj.aData[5];
                        var sReturn = '<center><A href="#"><IMG id="'+idNota+'"  onclick="AnularNotaCredito(this); return false;" src="../images/delete.ico" style="width: 16px; height: 16px; border-left-color: yellow; border-bottom-color: yellow; border-top-style: none; border-top-color: yellow; border-right-style: none; border-left-style: none; border-right-color: yellow; border-bottom-style: none;"  ></a></center>';
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
        //tirar mensaje de error en caso que la llamada ajax tenga problemas
        error: function(msg){ 
            alert("Se ha producido un error en la llamada ajax al servidor");
        }
    });//fin de llamada ajax
	//final evento AJAX
    
});

//cuando ddl se llama al id select[id*=id del ddl]
//cuando es un texbox y button es input[id*=id del tb o bt]
function deshabilitarCampos(){
    $("input[id*=tbNumNotaCredito]").removeAttr('disabled');
    $("input[id*=tbNumNotaCredito]").attr('disabled','disabled');
    
    $("input[id*=tbProveedor]").removeAttr('disabled');
    $("input[id*=tbProveedor]").attr('disabled','disabled');
    $("input[id*=tbFactura]").removeAttr('disabled');
    $("input[id*=tbFactura]").attr('disabled','disabled');
    $("#ctl00_ContentPlaceHolder1_tbMotivo").removeAttr('disabled');
    $("#ctl00_ContentPlaceHolder1_tbMotivo").attr('disabled','disabled');
    $("input[id*=tbTotal]").removeAttr('disabled');
    $("input[id*=tbTotal]").attr('disabled','disabled');
       return false;
}
function deshabilitarBoton(){
    $("input[id*=imgbtNuevo]").css("display", "inline");
    $("input[id*=imgbtGuardar]").css("display", "none");
    $("input[id*=imgbtModificar]").css("display", "none");
    $("input[id*=imgbtBorrar]").css("display", "none");
}
function habilitarCampos(){
  $("input[id*=tbNumNotaCredito]").removeAttr('disabled');    
    $("input[id*=tbProveedor]").removeAttr('disabled');    
    $("input[id*=tbFactura]").removeAttr('disabled');    
    $("#ctl00_ContentPlaceHolder1_tbMotivo").removeAttr('disabled');    
    $("input[id*=tbTotal]").removeAttr('disabled');   
    return false;
}

function limpiarCampos(){
    $("input[id*=tbNumNotaCredito]").val('');       
    $("input[id*=tbProveedor]").val('');       
    $("input[id*=tbFactura]").val(''); 
    $("input[id*=tbDireccion]").val(''); 
    $("input[id*=tbNumDoc]").val('');       
    $("#ctl00_ContentPlaceHolder1_tbMotivo").val('');       
    $("input[id*=tbTotal]").val('');  
    return false;
}
function nuevo(){
habilitarCampos();
deshabilitarBoton();
$("input[id*=tbNumNotaCredito]").focus();
$("input[id*=imgbtNuevo]").css("display", "none");
$("input[id*=imgbtGuardar]").css("display", "inline");
}
function autocompleteProveedor(){

$.ajax({
            type:"POST",       
            dataType: "json", 
            contentType: "application/json; charset=utf-8",
            url: "Compras.aspx/GetNombreProveedor", 
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
        

        return false;
        }
        
        
        
function autocompleteFactura(idProveedor){

$.ajax({
            type:"POST",       
            dataType: "json", 
            contentType: "application/json; charset=utf-8",
            url: "NotaCredito.aspx/facturasPendientes", 
            data:"{'idProveedor':'" + idProveedor + "'}",
            success: function(data){
                  //asginar objeto proveeedores obtenido del servidor
                 //al plugin autcomplete 
                 $("input[id*=tbFactura]").autocomplete(eval(data), {
                    minChars: 0,
                    width: 310,
                    max:5,
                    matchContains: true,
                    mustMatch: false,
                    autoFill: false,
                    formatItem: function(row, i, max) { //alert(row.id_proveedor);
                        return row[0] ;
                    },
                    formatMatch: function(row, i, max) {
                        return row[0] ;
                    },
                    formatResult: function(row) {
                        _idFactura = row[1];
                       return row[0];
                    }
                    }).result(function(e, i, row) {
	                	obtenerResultadoFactura(row);
                    });
               
            },
             //lanzar mensaje de error en caso que la llamada ajax tenga problemas
            error: function(msg){ 
                alert("Se ha producido un Error");
            }    
        });// fin .ajax proveedor
        

        return false;
        }

function obtenerResultadoProveedor(row){
var codigo = row.split("|")[0];
//alert(codigo);
rellenarCamposProveedor(codigo);
autocompleteFactura(codigo);

}

function obtenerResultadoFactura(row){
//var codigo = row.split("|")[0];
codigo = row[0];
//alert("codigo "+codigo);
//alert(codigo);
//rellenarCamposProveedor(codigo);

}

//Rellena los campos con los datos del proveedor seleccionado
function rellenarCamposProveedor(idProveedor) {


    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "Compras.aspx/DatosProveedor",  //invocar al metodo del servidor que devulve un datatable
        data: "{'idProveedor':'" + idProveedor + "'}",
        success: function(data) {

            // Decodifica la cadena obtenida y lo transforma en un objeto producto
            var proveedor = JSON.decode(data);

            $("select[id*=chFactura]").val(proveedor.numDoc);
            $("input[id*=tbDireccion]").val(proveedor.direccion);

            return false;

        },
        //tirar mensaje de error en caso que la llamada ajax tenga problemas
        error: function(msg) {
            alert("Se ha producido un Error" + urlGetDataSet);
        }
    });  //fin de llamada ajax
    return false;
}

function obtenerFactura(idProveedor){
 $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "NotaCredito.aspx/facturasPendientes",  //invocar al metodo del servidor que devulve un datatable
        data: "{'idProveedor':'" + idProveedor + "'}",
        success: function(dato) {

            // Decodifica la cadena obtenida y lo transforma en un objeto producto
            var proveedor = JSON.decode(dato);

            $("input[id*=tbNumDoc]").val(proveedor.numDoc);
            $("input[id*=tbDireccion]").val(proveedor.direccion);

            return false;

        },
        //tirar mensaje de error en caso que la llamada ajax tenga problemas
        error: function(msg) {
            alert("Se ha producido un Error" + urlGetDataSet);
        }
    });  //fin de llamada ajax
    return false;

}

////////////////////////////////////////////////////////////////////////////////////////////////
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
///////////////////////////////////////////////////////////////////////////
//Guardar la nota de credito
function guardar(){
var idNota = 0;
var num_nota =  $("input[id*=tbNumNotaCredito]").val();
var fecha = $("input[id*=tbFecha]").val();
var total_credito = $("input[id*=tbTotal]").val();
var motivo =$("#ctl00_ContentPlaceHolder1_tbMotivo").val(); 
var id_factura =_idFactura;

//"'id_proveedor':'"+ codigo + "'}",
 $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "NotaCredito.aspx/guardar",  //invocar al metodo del servidor que devulve un datatable
        data:"{" +   // id_factura
             "'idNota':'"+ idNota + "', " + 
             "'num_nota':'"+ num_nota + "', " + 
             "'fecha':'"+ fecha +"', " + 
             "'total_credito':'"+ total_credito +"', " + 
             "'motivo':'"+ motivo +"', " + 
             "'id_factura':'"+ id_factura +"', " + 
             "'id_proveedor':'"+_idProveedor+"'}",
        success: function(data) {
            // Decodifica la cadena obtenida y lo transforma en un objeto producto
           // var proveedor = JSON.decode(dato);
           recargar();
           deshabilitarCampos();
           limpiarCampos();
           deshabilitarBoton();
           
           
            return false;

        },
        //tirar mensaje de error en caso que la llamada ajax tenga problemas
        error: function(msg) {
            alert("Se ha producido un Error" + urlGetDataSet);
        }
    });  //fin de llamada ajax
    return false;
}



function cancelar(){
            limpiarCampos();
            deshabilitarCampos();
            deshabilitarBoton();
}
/*Actualiza la tabla*/
function recargar()
{
    $.ajax({
        type:"POST", 
        dataType: "json", 
        contentType: "application/json; charset=utf-8",
        url:"NotaCredito.aspx/notasCredito",  //invocar al metodo del servidor que devulve un datatable 
        data:"{}",
        success:  function armarTabla(json){//ver el funcionamiento
            //vacia la tabla
            tablaNotaCredito.fnClearTable(tablaNotaCredito);
            //recarga con los nuevos datos
            tablaNotaCredito.fnAddData(eval(json));
            //repintar la tabla  
            this.fnDraw(that);
          }
          });
}
/*Obtiene el id del elemento seleccionado*/
function getButtonId(boton){
        alert($(boton).attr("id"));
        return false;
}
/////////////////////////////////////////////


function modificarPersona(boton){
    _idNotaCredito = $(boton).attr("id");
    rellenarCampos(_idNotaCredito);
    editar();
    return false;
}
/*Evento del boton cancelar que deshabilita y limpia los campos*/
function cancelar(){
    deshabilitarCampos();
    deshabilitarBoton();
    $("input[id*=imgbtNuevo]").css("display", "inline");
    limpiarCampos();
}

function editar(){
    habilitarCampos();
    deshabilitarBoton();
    $("input[id*=imgbtdMod]").css("display", "inline");
    return false;
}



/*Borra un registro en la base de datos*/
function borrar(idNota)
{ 
   var _fecha = $("input[id*=tbFecha]").val();
    // Llamada ajax al servidor para guardar los datos
    $.ajax({
            type:"POST",
            dataType: "json", 
            contentType: "application/json; charset=utf-8",
            url:"NotaCredito.aspx/borrar",
            data:"{" +
             "'idNota':'"+ idNota + "', " + 
             "'num_nota':'0', " + 
             "'fecha':'"+ _fecha +"', " + 
             "'total_credito':'0', " + 
             "'motivo':' ', " + 
             "'id_factura':'9', " + 
             "'id_proveedor':'0'}" , 
             
             success: function(data){
                if(data == 'OK'){
                    recargar(); 
                    $("label[id*=lbError]").text("El Registro ha sido guardado correctamente");
                    limpiarCampos();
                    deshabilitarCampos();
                    deshabilitarBoton();
                    $("input[id*=imgbtNuevo]").css("display", "inline");
                    
                             }else{
                                  $("label[id*=lbMsn]").text("Ha ocurrido un error durante el proceso");
                             }
                        }
                    });//fin llamada ajax al servidor
                    return false;
                   
}


function AnularNotaCredito(boton){
_idProveedor=$(boton).attr("id");
borrar(_idProveedor);
popupEliminarNotaCredito();
return false;
}

function atrasNotaCredito(){
alert("redirecciona");
window.location.href="~/CtaCteCompras/NotaCredito.aspx";
return false;
}

/* Función que muestra un dialogo para una eliminar un nota credito */
function popupEliminarNotaCredito() {
    $(function() {
        $("div[id*=eliminarNotaCredito]").dialog({
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
             'Cancelar': function(){
		        $(this).dialog('destroy');
            
               
            },'Eliminar': function(){
                borrar(_idProveedor);
                $(this).dialog('destroy');
		   
		        
            }
            }
        });
    });
    return false;
}