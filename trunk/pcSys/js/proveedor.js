var tablaPersona;
var giRedraw = false; 
var _idProveedor = 0;

// JScript File
$(document).ready(function() {
    limpiarCampos();
    //inicia AJAX
	$.ajax({
        type:"POST", 
        dataType: "json", 
        contentType: "application/json; charset=utf-8",
        url:"Proveedor.aspx/getPersonas",  //invocar al metodo del servidor que devulve un datatable 
        data:"{}",
        success:  function armarTabla(datatable_servidor){//ver el funcionamiento
            //obtengo el id de la tabla donde se generará la tabla dinamica
            tablaPersona = $('#tablaPersonas').dataTable({
		  	    "aaData": eval(datatable_servidor),     //armar tabla con el arreglo serializado del serividor
		  	    "aoColumns": [ 
		  	            { "sTitle": "Codigo" },              
				        { "sTitle": "Razon Social" },
						{ "sTitle": "Apellido" },
						{ "sTitle": "Direccion" },
						
						{ "sTitle": "Borrar", "bSortable": false,
                    "fnRender": function(obj) {
                        //var idFacturaDet = obj.aData[obj.iDataColumn];
                        var idProveedorBoton = obj.aData[0];
                        var sReturn = '<center><A href="#"><IMG id="'+idProveedorBoton+'"  onclick="borrarPersona(this); return false;" src="../images/delete.ico" style="width: 16px; height: 16px; border-left-color: yellow; border-bottom-color: yellow; border-top-style: none; border-top-color: yellow; border-right-style: none; border-left-style: none; border-right-color: yellow; border-bottom-style: none;"  ></a></center>';
                        return sReturn;
                        }
                    },
                    { "sTitle": "Editar", "bSortable": false,
                    "fnRender": function(obj) {
                        var idProveedorBoton = obj.aData[0];//modificarPersona(boton)
                        var sReturn = '<center><A href="#"><IMG id="'+idProveedorBoton+'"  onclick="modificarPersona(this); return false;" src="../images/edit.png" style="width: 16px; height: 16px; border-left-color: yellow; border-bottom-color: yellow; border-top-style: none; border-top-color: yellow; border-right-style: none; border-left-style: none; border-right-color: yellow; border-bottom-style: none;"  ></a></center>';
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


/*Obtiene el id del elemento seleccionado*/
function getButtonId(boton){
        alert($(boton).attr("id"));
        return false;
}

function borrarPersona(boton){
    _idProveedor = $(boton).attr("id");
    //borrar();
    popupEliminarPersona();
    //return false;
}

function modificarPersona(boton){
    _idProveedor = $(boton).attr("id");
    rellenarCampos(_idProveedor);
    editar();
    return false;
}

/*Trae los datos de la persona desde el servidor serializados*/
function rellenarCampos(idProveedor_)
{
//alert("El ID es "+idProveedor_);
        $.ajax({
        type:"POST", 
        dataType: "json", 
        contentType: "application/json; charset=utf-8",
        url:"Proveedor.aspx/DatosProveedor",  // Metodo del servidor que devulve la marca
        data:"{'idProveedor':'" + idProveedor_ + "'}",
        success:  function (obtener_return_val){
            var persona = JSON.decode(obtener_return_val);
            _idProveedor = persona.idProveedor;
            $("input[id*=tbRazonSocial]").val(persona.razonSocial);
            $("input[id*=tbNombre]").val(persona.nombre);
            $("input[id*=tbApellido]").val(persona.apellido);
            $("select[id*=chTipoDoc]").val(persona.idTipoDocumento);
            $("input[id*=tbNumDoc]").val(persona.numDoc);
            $("#ctl00_ContentPlaceHolder1_tbDireccion").val(persona.direccion);
            $("input[id*=tbTelefono]").val(persona.telefono);
            
        },   
        //Error en caso de que ocurra algun problema
        error: function(msg){ 
            alert("Se ha producido un error en la llamada ajax al servidor");
        }
   });//fin ajax

    return false;
}



/*Guarda un registro en la base de datos*/
function guardar()
{
   var razonSocial = $("input[id*=tbRazonSocial]").val();
   var nombre = $("input[id*=tbNombre]").val();
   var apellido = $("input[id*=tbApellido]").val();
   var tipoDoc = $("select[id*=chTipoDoc]").val();
   var numDoc = $("input[id*=tbNumDoc]").val();
   var direccion = $("#ctl00_ContentPlaceHolder1_tbDireccion").val();
   var telefono = $("input[id*=tbTelefono]").val(); 
    // Llamada ajax al servidor para guardar los datos
    $.ajax({
            type:"POST",
            dataType: "json", 
            contentType: "application/json; charset=utf-8",
            url:"Proveedor.aspx/guardar",
            data:"{" +
             "'idProveedor':'"+ _idProveedor + "', " + 
             "'razon_social':'"+ razonSocial + "', " + 
             "'nombre':'"+ nombre + "', " + 
             "'apellido':'"+ apellido +"', " + 
             "'num_doc':'"+ numDoc +"', " + 
             "'dir':'"+ direccion + "', " + 
             "'telefono':'" + telefono +"', " + 
             "'tipo_doc':'" + tipoDoc + "'}",
             success: function(data){
                if(data == 'OK'){
                    recargar(); 
                    $("span[id*=lbError]").text("El Registro ha sido guardado correctamente");
                    limpiarCampos();
                    deshabilitarCampos();
                    deshabilitarBotones();
                    $("input[id*=imgbtNuevo]").css("display", "inline");
                    
                             }else{
                                  $("span[id*=lbMsn]").text("Ha ocurrido un error durante el proceso");
                             }
                        }
                    });//fin llamada ajax al servidor
                    return false;
                   
}

/*Modifica los datos de un registro*/
function modificar()
{

   var razonSocial = $("input[id*=tbRazonSocial]").val();
   var nombre = $("input[id*=tbNombre]").val();
   var apellido = $("input[id*=tbApellido]").val();
   var tipoDoc = $("select[id*=chTipoDoc]").val();
   var numDoc = $("input[id*=tbNumDoc]").val();
   var direccion = $("#ctl00_ContentPlaceHolder1_tbDireccion").val();
   var telefono = $("input[id*=tbTelefono]").val();
    // Llamada ajax al servidor para guardar los datos
    $.ajax({
            type:"POST",
            dataType: "json", 
            contentType: "application/json; charset=utf-8",
            url:"Proveedor.aspx/modificar",
            data:"{" +
             "'idProveedor':'"+ _idProveedor + "', " + 
             "'razon_social':'"+ razonSocial + "', " + 
             "'nombre':'"+ nombre + "', " + 
             "'apellido':'"+ apellido +"', " + 
             "'num_doc':'"+ numDoc +"', " + 
             "'dir':'"+ direccion + "', " + 
             "'telefono':'" + telefono +"', " + 
             "'tipo_doc':'" + tipoDoc + "'}",
             success: function(data){
                if(data == 'OK'){
                    recargar(); 
                    $("label[id*=lbError]").text("El Registro ha sido guardado correctamente");
                    limpiarCampos();
                    deshabilitarCampos();
                    deshabilitarBotones();
                    $("input[id*=imgbtNuevo]").css("display", "inline");
                    
                             }else{
                                  $("label[id*=lbMsn]").text("Ha ocurrido un error durante el proceso");
                             }
                        }
                    });//fin llamada ajax al servidor
                    return false;
                   
}

//Modifica o mdifica un proveedor
function guardarMod(){
    if(_idProveedor == 0){
        guardar();
    }else{
        modificar();
        _idProveedor = 0;
    }
}

/* Función que muestra un dialogo para una eliminar un proveedor */
function popupEliminarPersona() {
    $(function() {
	    $("div[id*=eliminarPersona]").dialog({
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
		    'Eliminar': function(){
		        borrar();
		        $(this).dialog('destroy');
		        
		    },
		    'Cancelar': function(){
		        $(this).dialog('destroy');
		        
		    }
		    }
	    });
    });
    return false;
}

function popupProveedorDeudas() {
    $(function() {
	    $("div[id*=proveedorDeuda]").dialog({
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
		        $(this).dialog('destroy');
		        
		    }
		    }
	    });
    });
    return false;
}

/*Borra un registro en la base de datos*/
function borrar()
{  
   var razonSocial = $("input[id*=tbRazonSocial]").val();
   var nombre = $("input[id*=tbNombre]").val();
   var apellido = $("input[id*=tbApellido]").val();
   var tipoDoc = $("select[id*=chTipoDoc]").val();
   var numDoc = $("input[id*=tbNumDoc]").val();
   var direccion = $("#ctl00_ContentPlaceHolder1_tbDireccion").val();
   var telefono = $("input[id*=tbTelefono]").val();
    // Llamada ajax al servidor para guardar los datos
    $.ajax({
            type:"POST",
            dataType: "json", 
            contentType: "application/json; charset=utf-8",
            url:"Proveedor.aspx/borrar",
            data:"{" +
             "'idProveedor':'"+ _idProveedor + "', " + 
             "'razon_social':'', " + 
             "'nombre':'', " + 
             "'apellido':'', " + 
             "'num_doc':'0', " + 
             "'dir':'', " + 
             "'telefono':'', " + 
             "'tipo_doc':'0'}",
             success: function(data){
                if(data == 'EXITO'){
                    recargar(); 
                    $("label[id*=lbError]").text("El Registro ha sido guardado correctamente");
                    limpiarCampos();
                    deshabilitarCampos();
                    deshabilitarBotones();
                    $("input[id*=imgbtNuevo]").css("display", "inline");
                    
                             }else{
                                popupProveedorDeudas();
                                  //$("label[id*=lbMsn]").text("Ha ocurrido un error durante el proceso");
                             }
                        }
                    });//fin llamada ajax al servidor
                    return false;
                   
}

/*Actualiza la tabla*/
function recargar()
{
deshabilitarCampos();
    $.ajax({
        type:"POST", 
        dataType: "json", 
        contentType: "application/json; charset=utf-8",
        url:"Proveedor.aspx/getPersonas",  //invocar al metodo del servidor que devulve un datatable 
        data:"{}",
        success:  function armarTabla(json){//ver el funcionamiento
            //vacia la tabla
            tablaPersona.fnClearTable(tablaPersona);
            //recarga con los nuevos datos
            tablaPersona.fnAddData(eval(json));
            //repintar la tabla  
            this.fnDraw(that);
          }
          });
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

/*Limpia los campos del ABM*/
function limpiarCampos(){
    $("input[id*=tbRazonSocial]").val('');
    $("input[id*=tbNombre]").val('');
    $("input[id*=tbApellido]").val('');
    $("select[id*=chTipoDoc]").val('');
    $("input[id*=tbNumDoc]").val('');
    $("#ctl00_ContentPlaceHolder1_tbDireccion").val('');
    $("input[id*=tbTelefono]").val('');
    return false;
}

/*Deshabilita los campos del ABM*/
function deshabilitarCampos(){
    $("input[id*=tbRazonSocial]").removeAttr('disabled');
    $("input[id*=tbNombre]").removeAttr('disabled');
    $("input[id*=tbApellido]").removeAttr('disabled');
    $("select[id*=chTipoDoc]").removeAttr('disabled');
    $("input[id*=tbNumDoc]").removeAttr('disabled');
    $("#ctl00_ContentPlaceHolder1_tbDireccion").removeAttr('disabled');
    $("input[id*=tbTelefono]").removeAttr('disabled');
    
    $("input[id*=tbRazonSocial]").attr('disabled', 'disabled'); 
    $("input[id*=tbNombre]").attr('disabled', 'disabled'); 
    $("input[id*=tbApellido]").attr('disabled', 'disabled'); 
    $("select[id*=chTipoDoc]").attr('disabled', 'disabled'); 
    $("input[id*=tbNumDoc]").attr('disabled', 'disabled'); 
    $("#ctl00_ContentPlaceHolder1_tbDireccion").attr('disabled', 'disabled'); 
    $("input[id*=tbTelefono]").attr('disabled', 'disabled');     
    return false;
}

/*Habilita los campos para su uso*/
function habilitarCampos(){
    $("input[id*=tbRazonSocial]").removeAttr('disabled');
    $("input[id*=tbNombre]").removeAttr('disabled');
    $("input[id*=tbApellido]").removeAttr('disabled');
    $("select[id*=chTipoDoc]").removeAttr('disabled');
    $("input[id*=tbNumDoc]").removeAttr('disabled');
    $("#ctl00_ContentPlaceHolder1_tbDireccion").removeAttr('disabled');
    $("input[id*=tbTelefono]").removeAttr('disabled');
    return false;
}

/*Evento del boton nuevo, que prepara al ABM para ingresar un nuevo registro*/
function nuevo(){
    habilitarCampos();
    deshabilitarBotones();
    $("input[id*=imgbtGuardar]").css("display", "inline");
    _idProveedor = 0;
}

/*Evento del boton cancelar que deshabilita y limpia los campos*/
function cancelar(){
    deshabilitarCampos();
    deshabilitarBotones();
    $("input[id*=imgbtNuevo]").css("display", "inline");
    limpiarCampos();
}

function editar(){
    habilitarCampos();
    deshabilitarBotones();
    $("input[id*=imgbtdMod]").css("display", "inline");
    return false;
}

function Imprimir() {
        window.location.href="reporteProveedores.aspx"; 
        return false;
    };