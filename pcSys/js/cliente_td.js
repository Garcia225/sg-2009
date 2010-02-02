  var oTable;
 var giRedraw = false;       
        //Se activa cuando la pagina se recarga completamente
    $(document).ready(function() {
    getDate();
    vaciarCampos();
    deshabilitarBotones();
    inhabilitarCampos();
    $("input[id*=txtFechaNacimiento]").val('01/01/1987');
    $("input[id*=btnNuevo]").css("display", "inline");
    $("input[id*=btnSalir]").css("display", "inline");
    $("input[id*=btnListar]").css("display", "inline");
    $("input[id*=btnNuevo]").focus();
    
    $.ajax({
        type:"POST", 
        dataType: "json", 
        contentType: "application/json; charset=utf-8",
        url:"Clientes.aspx/ClienteGetDataSet",  //invocar al metodo del servidor que devulve un datatable 
        data:"{}",
        success:  function armarTabla(datatable_servidor){//ver el funcionamiento
            //obtengo el id de la tabla donde se generará la tabla dinamica
            oTable = $('#tabla_cliente').dataTable({
		  	    "aaData": eval(datatable_servidor),     //armar tabla con el arreglo serializado del serividor
		  	    "aoColumns": [               
				        { "sTitle": "Codigo" },
						{ "sTitle": "Nombre" },
						{ "sTitle": "Apellido" },   //crear titulos de las columnas  
						{ "sTitle": "Documento" },
						{ "sTitle": "Direccion" },
						{ "sTitle": "Telefono" },
						{ "sTitle": "Celular" },
						{ "sTitle": "Localidad" }
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
    
       
    //controlador del evento click
    $("#tabla_cliente tbody").click(function(event) {
        $(oTable.fnSettings().aoData).each(function() {
            $(this.nTr).removeClass('row_selected');
        });
        $(event.target.parentNode).addClass('row_selected');
        var tds = $(event.target.parentNode).children('td'); 
        var idCliente  = $(tds[0]);           
        // Si no está en modo edición carga los campos con estos datos
            if (!($("input[id*=txtNombre]").is(":enabled"))) {
            // Cargar campos
            rellenarCampos(idCliente.text());
            $("#ctl00_Main_txtDireccion").val($(tds[4]).text());
            deshabilitarBotones();
             $("input[id*=btnEdit]").css("display", "inline");
            $("input[id*=btnEliminar]").css("display", "inline");
            $("input[id*=btnNuevo]").css("display", "inline");
            $("input[id*=btnListar]").css("display", "inline");
            $("input[id*=btnSalir]").css("display", "inline");
            
        }
    });        
});
        
//Limpia los campos del formulario
function vaciarCampos(){
    $("input[id*=txtIdCliente]").val('0');
    $("input[id*=txtNombre]").val('');
    $("input[id*=txtApellido]").val('');
    $("input[id*=txtDocumento]").val('');  
    $("#ctl00_Main_txtDireccion").val('');
    $("input[id*=txtTelefono]").val('');
    $("input[id*=txtCelular]").val('');
    $("input[id*=txtFax]").val('');
    $("input[id*=txtEmail]").val('');
    $("input[id*=txtContacto]").val('');
    $("input[id*=txtNroContacto]").val('');
    $("select[id*=drlstLocalidad]").val('');
    $("input[id*=txtLimite]").val('');
}

//Desabilita los botones que no se usaran aun
function deshabilitarBotones(){
    $("input[id*=btnEdit]").css("display", "none");
    $("input[id*=btnBuscar]").css("display", "none");
    $("input[id*=btnAceptar]").css("display", "none");
    $("input[id*=btnCancelar]").css("display", "none");
    $("input[id*=btnEliminar]").css("display", "none"); 
    $("input[id*=btnGuardar]").css("display", "none"); 
    $("input[id*=btnNuevo]").css("display", "none"); 
    $("input[id*=btnSalir]").css("display", "none");
    $("input[id*=btnListar]").css("display", "none");
    
}

//Habilita los campos del ABM para la modificacion
function habilitarCampos() {
    $("input[id*=txtNombre]").removeAttr('disabled');
    $("input[id*=txtApellido]").removeAttr('disabled');
    $("input[id*=txtDocumento]").removeAttr('disabled');
    $("input[id*=txtFechaNacimiento]").removeAttr('disabled');
    $("#ctl00_Main_txtDireccion").removeAttr('disabled');
    $("input[id*=txtTelefono]").removeAttr('disabled');
    $("input[id*=txtCelular]").removeAttr('disabled');
    $("input[id*=txtFax]").removeAttr('disabled');
    $("input[id*=txtContacto]").removeAttr('disabled');
    $("input[id*=txtNroContacto]").removeAttr('disabled');
    $("input[id*=drlstLocalidad]").removeAttr('disabled');
    $("select[id*=drlstLocalidad]").removeAttr('disabled');
    $("input[id*=txtFechaAlta]").removeAttr('disabled');
    $("input[id*=txtEmail]").removeAttr('disabled');
    $("input[id*=txtLimite]").removeAttr('disabled');
}
        
//Evento del boton nuevo que habilitas los campos y botones para el ingreso del
//nuevo registro
function nuevo(){
    vaciarCampos();
    habilitarCampos();
    deshabilitarBotones();
    $("input[id*=btnGuardar]").css("display", "inline");
    $("input[id*=btnCancelar]").css("display", "inline");
    $("input[id*=btnNuevo]").css("display", "none");
    $("input[id*=btnSalir]").css("display", "none");
    $("input[id*=imgbtnNuevoPais]").css("display", "inline");
    $("input[id*=txtNombre]").focus();
}

//Inhabilita los campos del ABM para la modificacion
function inhabilitarCampos() {            
    $("input[id*=txtNombre]").removeAttr('disabled');
    $("input[id*=txtNombre]").attr('disabled', 'disabled'); 
    $("input[id*=txtApellido]").removeAttr('disabled');
    $("input[id*=txtApellido]").attr('disabled', 'disabled');
    $("input[id*=txtDocumento]").removeAttr('disabled');
    $("input[id*=txtDocumento]").attr('disabled', 'disabled');
    $("input[id*=txtFechaNacimiento]").removeAttr('disabled');
    $("input[id*=txtFechaNacimiento]").attr('disabled', 'disabled');
    $("#ctl00_Main_txtDireccion").removeAttr('disabled');
    $("#ctl00_Main_txtDireccion").attr('disabled', 'disabled');
    $("input[id*=txtTelefono]").removeAttr('disabled');
    $("input[id*=txtTelefono]").attr('disabled', 'disabled');
    $("input[id*=txtCelular]").removeAttr('disabled');
    $("input[id*=txtCelular]").attr('disabled', 'disabled');
    $("input[id*=txtFax]").removeAttr('disabled');
    $("input[id*=txtFax]").attr('disabled', 'disabled');
    $("input[id*=txtContacto]").removeAttr('disabled');
    $("input[id*=txtContacto]").attr('disabled', 'disabled');
    $("input[id*=txtNroContacto]").removeAttr('disabled');
    $("input[id*=txtNroContacto]").attr('disabled', 'disabled');
    $("select[id*=drlstLocalidad]").removeAttr('disabled');
    $("select[id*=drlstLocalidad]").attr('disabled', 'disabled');
    $("input[id*=txtFechaAlta]").removeAttr('disabled');
    $("input[id*=txtFechaAlta]").attr('disabled', 'disabled');
    $("input[id*=txtEmail]").removeAttr('disabled');
    $("input[id*=txtEmail]").attr('disabled', 'disabled');
    $("input[id*=txtLimite]").removeAttr('disabled');
    $("input[id*=txtLimite]").attr('disabled', 'disabled');
}
        
//Evento del boton Editar habilita los botones y campos para la modificacion
function cancelar() {
	getDate();
    inhabilitarCampos();
    deshabilitarBotones();
    vaciarCampos();
    $("input[id*=btnNuevo]").css("display", "inline");
    $("input[id*=btnCancelar]").css("display", "none");
    $("input[id*=btnSalir]").css("display", "inline");
    $("input[id*=btnListar]").css("display", "inline");
    $("input[id*=btnAceptar]").css("display", "none"); 
    return false;
}
         
//Evento del boton Cancelar habilita los botones y campos para la modificacion
function editar() {
    habilitarCampos();
    deshabilitarBotones();
    $("input[id*=btnNuevo]").css("display", "none");
    $("input[id*=btnCancelar]").css("display", "inline");
    $("input[id*=btnAceptar]").css("display", "inline"); 
    $("input[id*=txtFechaAlta]").removeAttr('disabled');
    $("input[id*=txtFechaAlta]").attr('disabled', 'disabled');
    $("input[id*=imgbtnNuevoPais]").css("display", "inline");
    return false;
}
          
//Evento del boton no,que no borra al usuario
function no(){
    $("div[id*=eliminarCliente]").css("display", "none");
    inhabilitarCampos();
    deshabilitarBotones();
    vaciarCampos();
    $("input[id*=btnNuevo]").css("display", "inline");
    $("input[id*=btnCancelar]").css("display", "none");
    $("input[id*=btnAceptar]").css("display", "none"); 
    $("input[id*=btnSalir]").css("display", "inline");
    $("input[id*=btnListar]").css("display", "inline");
}

//Habilita los campos necesarios en el popUp
function preprarPopUp(){
    $("input[id*=txtNewPais]").val('');
    $("input[id*=txtLoc]").val('');
    $("input[id*=txtNewPais]").css("display", "none");
    $("select[id*=drlstPais]").css("display", "inline");
    $("input[id*=imgbtnNoCountry]").css("display", "none");
    $("input[id*=imgbtnNewPais]").css("display", "inline");
    return false;
}    

//Evento del boton Eliminar habilita los botones y campos para la modificacion
function habilitar() {
    habilitarCampos();
    $("input[id*=txtIdCliente]").removeAttr('disabled');
    return false;
}

//evento del botonSalir en el lado cliente
function Salir() {
    window.location.href = "../Default.aspx";
    return false;
};

//evento del botonSalir en el lado cliente
function ReturnCliente() {
    window.location.href = "../Facturacion_Cliente_Proveedor/Clientes.aspx";
    return false;
};
//evento del boton imprimir en el lado cliente = se va a la interfaz del reporte
function Imprimir() {
    window.location.href = "../reportes/ReportCliente.aspx";
    return false;
};

/*funcion que actualiza la tabla dinamica*/
function refresh(){
   oTable.fnReloadAjax('Clientes.aspx/ClienteGetDataSet');
}
          
// Get the rows which are currently selected 
function fnGetSelected(oTableLocal) {
    var aReturn = new Array();
    var aTrs = oTableLocal.fnGetNodes();
    for (var i = 0; i < aTrs.length; i++) {
        if ($(aTrs[i]).hasClass('row_selected')) {
                    aReturn.push(aTrs[i]);
                }
            }
    return aReturn;
}

/* Función que muestra un dialogo para una eliminar un cliente */
function popupEliminarCliente() {
    $(function() {
	    $("div[id*=eliminarCliente]").dialog({
		    bgiframe: true,
		    resizable: false,
		    modal:true,
		    width: 380,
		    overlay: {
			    backgroundColor: '#000',
			    opacity: 0.5
		    },
		    close: function() {
				    $(this).dialog('destroy');
			},
		    buttons: {
		        Cancel: function() {
				    $(this).dialog('destroy');
			    },
		        //enviar id por ajax y llamar a metodo borrar del servidor
			    'Eliminar': function() {
			     var anSelected = fnGetSelected( oTable );
                 var iRow = oTable.fnGetPosition( anSelected[0]);
                 var idCliente = $("input[id*=txtIdCliente]").val();
	             var nombre = $("input[id*=txtNombre]").val();
	             var apellido = $("input[id*=txtApellido]").val();
	             var documento = $("input[id*=txtDocumento]").val();
	             var fechaNac = $("input[id*=txtFechaNacimiento]").val();
	             var direccion = $("#ctl00_Main_txtDireccion").val();
	             var telefono = $("input[id*=txtTelefono]").val();
	             var celular = $("input[id*=txtCelular]").val();
	             var fax = $("input[id*=txtFax]").val();
	             var email = $("input[id*=txtEmail]").val();
	             var contacto = $("input[id*=txtContacto]").val();
	             var telContacto = $("input[id*=txtNroContacto]").val();
	             var fechaAlta = $("input[id*=txtFechaAlta]").val();
	             var idLocalidad = $("select[id*=drlstLocalidad]").val();
	             var limite = $("input[id*=txtLimite]").val();
			        $("input[id*=txtNombre]").val(fechaAlta);
			        //mostrar mensaje
                 $("#divTitulo").css("display", "inline");
         
			        $.ajax({
                        type:"POST",
                        dataType: "json", 
                        contentType: "application/json; charset=utf-8",
                        url:"Clientes.aspx/Borrar",
                        data:"{" + 
             "'idCliente':'"+ idCliente + "', " + 
             "'nombre':'"+ nombre + "', " + 
             "'apellido':'"+ apellido +"', " + 
             "'documento':'"+ documento +"', " + 
             "'fechaNac':'"+ fechaNac + "', " + 
             "'direccion':'" + direccion +"', " + 
             "'telefono':'" + telefono + "', " + 
             "'celular':'" + celular + "', " + 
             "'fax':'" + fax + "', " + 
             "'email':'" + email +"', " + 
             "'contacto':'" + contacto +"', " + 
             "'telContacto':'" + telContacto + "', " + 
             "'fechaAlta':'" + fechaAlta + "', " + 
             "'idLocalidad':'" + idLocalidad + "', " + 
             "'limite':'" + limite + "'}",
                        success: function(data){
                        //Si hubo exito borra una dato de la tabla denamica
                              if (data=="SUCCESS"){
                                  // borrar de la tabla dinamica
                                  oTable.fnDeleteRow(iRow);
                                  //mostrar mensaje con css correspondiente
                                  $("#divTipo").attr("class", "ui-state-highlight ui-corner-all");
                                  $("span[id*=lbOpcion]").text("El Registro ha sido eliminado correctamente...");
                             }else{
                                   //mostrar mensaje con css correspondiente
                                   $("#divTipo").attr("class", "ui-state-error ui-corner-all");
                                   $("span[id*=lbOpcion]").text("Hubo un error durante el proceso");
                             }
                        }
                    });
                    
                    // Destruye el diálogo
        			 $(this).dialog('destroy');
        			// Limpia los campos
        			vaciarCampos();
        			$("input[id*=btnNuevo]").focus();
                    deshabilitarBotones();
                    inhabilitarCampos();
                    $("input[id*=btnNuevo]").css("display", "inline");
                    $("input[id*=btnSalir]").css("display", "inline");
                    getDate();
			    }
			}
	    });// fin dialog
    }); // fin funcion
    return false;
}             



/* Función que muestra un dialogo para agregar una nueva localidad */
function popupNuevaLocalidad() {
    $("input[id*=txtLoc]").val('');
    $("input[id*=txtNewPais]").val('');
	$("select[id*=drlstPais]").css("display", "inline");
	$("input[id*=txtNewPais]").css("display", "none");
    $(function() {
	    $("div[id*=addLocalidad]").dialog({
		    bgiframe: true,
		    resizable: false,
		    modal:true,
		    width: 380,
		    overlay: {
			    backgroundColor: '#000',
			    opacity: 0.5
		    },
		    close: function() {
				    $(this).dialog('destroy');
			},
		    buttons: {
		        Cancel: function() {
				    $(this).dialog('destroy');
			    },
		        //enviar id por ajax y llamar a metodo borrar del servidor
			    'Aceptar': function() {
	             var localidad = $("input[id*=txtLoc]").val();
	             var idPais = $("select[id*=drlstPais]").val();
	             var pais = $("input[id*=txtNewPais]").val();
			        //mostrar mensaje
                 $("#divTitulo").css("display", "inline");
			        $.ajax({
                        type:"POST",
                        dataType: "json", 
                        contentType: "application/json; charset=utf-8",
                        url:"Clientes.aspx/NuevaLocalidad",
                        data:"{" + 
                         "'localidad':'"+ localidad + "', " + 
                         "'idPais':'"+ idPais +"', " + 
                         "'pais':'" + pais + "'}",
                        success: function(data){
                        //Si hubo exito borra una dato de la tabla denamica
                              if (data=="SUCCESS"){
                              //Actualiza el dropDownList
                                  var jsonLocalidades = Clientes.ObtenerLocalidades();
                                  SelectOptionCargar(jsonLocalidades.value, 'drlstLocalidad');
                                  //mostrar mensaje con css correspondiente
                                  $("#divTipo").attr("class", "ui-state-highlight ui-corner-all");
                                  $("span[id*=lbOpcion]").text("La localidad ha sido guardado correctamente...");
                                  
                             }else{
                                   //mostrar mensaje con css correspondiente
                                   $("#divTipo").attr("class", "ui-state-error ui-corner-all");
                                   $("span[id*=lbOpcion]").text("Hubo un error durante el proceso");
                             }
                        }
                    });
                    
                    // Destruye el diálogo
        			 $(this).dialog('destroy');
        			// Limpia los campos
        			refresh();
			    
			    } }
			    });
			     });
			     }

/* Rellena los campos con los datos del cliente con id = idCliente*/
function rellenarCampos(idCliente) {
    $.ajax({
        type:"POST", 
        dataType: "json", 
        contentType: "application/json; charset=utf-8",
        url:"Clientes.aspx/Obtener",  // Metodo del servidor que devulve la marca
        data:"{'idCliente':'" + idCliente + "'}",
        success:  function (obtener_return_val){
            var cliente = JSON.decode(obtener_return_val);
            $("input[id*=txtIdCliente]").val(cliente.idCliente);
            $("input[id*=txtNombre]").val(cliente.nombre);
            $("input[id*=txtApellido]").val(cliente.apellido);
            $("input[id*=txtDocumento]").val(cliente.numDocRuc);    
            $("input[id*=txtFechaNacimiento]").val(cliente.fechaNacimiento);   
            $("input[id*=txtTelefono]").val(cliente.telefono);
            $("input[id*=txtCelular]").val(cliente.celular);
            $("input[id*=txtFax]").val(cliente.fax);
            $("input[id*=txtEmail]").val(cliente.email);
            $("input[id*=txtContacto]").val(cliente.contacto);
            $("input[id*=txtNroContacto]").val(cliente.telContacto);
            $("input[id*=txtFechaAlta]").val(cliente.fechaAlta);
            $("select[id*=drlstLocalidad]").val(cliente.localidad);
            $("input[id*=txtLimite]").val(cliente.limiteCredito);

        },   
        //tirar mensaje de error en caso que la llamada ajax tenga problemas
        error: function(msg){ 
            alert("Se ha producido un error en la llamada ajax al servidor");
        }
   });//fin de llamada ajax

    return false;

}            

//Obtiene y carga en el campo Fecha Alta la fecha del dia
function getDate(){
    var currentTime = new Date();
    var month = currentTime.getMonth() + 1;
    var day = currentTime.getDate();
    var year = currentTime.getFullYear();
    if (day<10)
        day="0"+day
    if(month<10)
        month="0"+month
    $("#ctl00_Main_txtFechaAlta").val(day+ "/" +month+ "/" + year);
};

//redefino la funcion fnReloadAjax para poder recargar el datatable
$.fn.dataTableExt.oApi.fnReloadAjax = function ( oSettings, sNewSource ){
    if ( typeof sNewSource != 'undefined' )
    {   
        oSettings.sAjaxSource = sNewSource;
    }
    //borrar la tabla dinamica
    this.fnClearTable( this );
    //setear variables de configuracion
    this.oApi._fnProcessingDisplay( oSettings, true );
    var that = this;
    
     //realizar llamada ajax al servidor para traer datos actualizados
     //que se utilizaran para recargar la tabla dinamica
     $.ajax({
        type:"POST", 
        dataType: "json", 
        contentType: "application/json; charset=utf-8",
        url:"Clientes.aspx/ClienteGetDataSet",  //invocar al metodo del servidor que devulve un datatable 
        data:"{}",
        success:  function recargar(json){
           //cargar la tabla con el resultado serializado
            oTable.fnAddData(eval(json) );
            
            //repintar la tabla dinamica 
            oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();
            that.fnDraw( that );
            that.oApi._fnProcessingDisplay( oSettings, false );
        },   
        //tirar mensaje de error en caso que la llamada ajax tenga problemas
        error: function(msg){ 
            alert("Se ha producido un error en la llamada ajax al servidor");
        }
    });//fin de llamada ajax
}

/* Función javascript endcargada de cargar un select a partir de un dataset que
 * debe contener por lo menos dos columnas con los nombres de descripcion e id.
 * <dataset> El dataset q contiene los datos q serán cargados
 * <selectId> El id del select al cual se cargarán los datos
 */ 
function SelectOptionCargar(dataset, selectId)
{
    // Obtengo el select
	var select = $('"select[id*=' + selectId + ']"');

    // Verificar si el dataset es un objeto válido
    if(dataset!=null && typeof(dataset) == "object" && dataset.Tables != null)
    {
        // Obtengo la tabla con los datos a cargar en el select
        var table = dataset.Tables[0];
        // Construir la lista de items
        var options = '';
        for(i = 0; i < table.Rows.length; i++) {
    
            //select.addOption(table.Rows[i].id, table.Rows[i].descripcion);
            options += '<option value="' + table.Rows[i].id + '">' + table.Rows[i].descripcion + '</option>';
            
        }
        // Cargo los item al select uno a uno
        select.html(options);

    }
    
}


/* Guarda la producto actual */
function guardar() {
    // Obtengo los datos del formulario
                 var idCliente = $("input[id*=txtIdCliente]").val();
	             var nombre = $("input[id*=txtNombre]").val();
	             var apellido = $("input[id*=txtApellido]").val();
	             var documento = $("input[id*=txtDocumento]").val();
	             var fechaNac = $("input[id*=txtFechaNacimiento]").val();
	             var direccion = $("#ctl00_Main_txtDireccion").val();
	             var telefono = $("input[id*=txtTelefono]").val();
	             var celular = $("input[id*=txtCelular]").val();
	             var fax = $("input[id*=txtFax]").val();
	             var email = $("input[id*=txtEmail]").val();
	             var contacto = $("input[id*=txtContacto]").val();
	             var telContacto = $("input[id*=txtNroContacto]").val();
	             var fechaAlta = $("input[id*=txtFechaAlta]").val();
	             var idLocalidad = $("select[id*=drlstLocalidad]").val();
	             var limite = $("input[id*=txtLimite]").val();

    // Hago la llamada ajax al servidor para guardar los datos
    $.ajax({
                        type:"POST",
                        dataType: "json", 
                        contentType: "application/json; charset=utf-8",
                        url:"Clientes.aspx/Guardar",
                        data:"{" + 
             "'idCliente':'"+ idCliente + "', " + 
             "'nombre':'"+ nombre + "', " + 
             "'apellido':'"+ apellido +"', " + 
             "'documento':'"+ documento +"', " + 
             "'fechaNac':'"+ fechaNac + "', " + 
             "'direccion':'" + direccion +"', " + 
             "'telefono':'" + telefono + "', " + 
             "'celular':'" + celular + "', " + 
             "'fax':'" + fax + "', " + 
             "'email':'" + email +"', " + 
             "'contacto':'" + contacto +"', " + 
             "'telContacto':'" + telContacto + "', " + 
             "'fechaAlta':'" + fechaAlta + "', " + 
             "'idLocalidad':'" + idLocalidad + "', " + 
             "'limite':'" + limite + "'}",
                        success: function(data){
                         //mostrar mensaje
                        $("#divTitulo").css("display", "inline");
                        //Si hubo exito borra una dato de la tabla denamica
                              if (data=="SUCCESS"){
                                // Finalizo con la edición;
                                    detenerEdicion();
                                     // Actualizo la tabla dinamica
                                     refresh();
                                  //mostrar mensaje con css correspondiente
                                  $("#divTipo").attr("class", "ui-state-highlight ui-corner-all");
                                  $("span[id*=lbOpcion]").text("El Registro ha sido guardado correctamente...");
                             }else{
                                   //mostrar mensaje con css correspondiente
                                   $("#divTipo").attr("class", "ui-state-error ui-corner-all");
                                   $("span[id*=lbOpcion]").text("Hubo un error durante el proceso");
                             }
                        }
                    });//fin llamada ajax al servidor

     return false;
    
}            
/* Detener la edición */
function detenerEdicion() {

    // Limpiar campos
    vaciarCampos();

    // Deshabilitar los campos para cargar los datos
    inhabilitarCampos();
    
    // Esconder todos los botones
    deshabilitarBotones();
   // y, mostrar los botones necesários
    $("input[id*=btnNuevo]").css("display", "inline");
    $("input[id*=btnListar]").css("display", "inline");
    $("input[id*=btnSalir]").css("display", "inline");
    return false;
}









