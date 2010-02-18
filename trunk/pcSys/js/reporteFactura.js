// JScript File
var _codigo=0;

$(document).ready(function() {
    $("input[id*=tbFechaInicio]").mask("99/99/9999");
    $("input[id*=tbFechaFin]").mask("99/99/9999");
    $("input[id*=tbID]").removeAttr('disabled');
    $("input[id*=tbID]").attr('disabled', 'disabled'); 
    autocompleteProveedor();
    return false;
});
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
                        return row[0] + " "+ row[2];
                    },
                    formatMatch: function(row, i, max) {
                        return row[0] +"|"+ row[2];
                    },
                    formatResult: function(row) {
                        _idProveedor = row[0];
                        return row[2];
                    }
                    }).result(function(e, i, row) {
	                	_codigo=row.split("|")[0];
	                	$("input[id*=tbID]").val(_codigo);
	                	alert("_codigo "+_codigo);
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
        alert("Salio del autocomplete");
    return false;
}

function prueba(){
alert("Esto es una prueba "+_codigo);

return false;
}
