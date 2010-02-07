/*
* Archivo functions.js
* Aqui se encuentra la funcion que me permite crear la botonera, indistintamente desde el 
* lugar que yo desee, siempre pasando ambas condicionantes, las cuales declaro al inicio
* del archivo aspx
*
* La forma de utilizar esta funcion es la siguiente
* <script language="JavaScript">
*   <!--
*       crearBotonera("<%  Response.Write(AtrasWeb)%>", "<%  Response.Write(nivelUsuario)%>");
*   -->
* </script>
*/

function esperarCargadepag() {
    window.print();
    //setTimeOut("window.onload = function() {}", 500);

}


function crearBotonera(atras, condicionante){
    document.writeln("<ul>");
    document.writeln("<li><a href=\""+atras+"Default.aspx\">Inicio</a></li>");
    document.writeln("<li><a href=\""+atras+"#\">Clientes</a></li>");
    document.writeln("<li><a href=\""+atras+"#\">Proveedores</a></li>");
    document.writeln("<li><a href=\"" + atras + "stock/abm_Producto.aspx\">Productos</a></li>");
    document.writeln("<li><a href=\""+atras+"stock/Crear_remito.aspx\">Remito</a></li>");
    document.writeln("<li><a href=\"" + atras + "sucursal/AgregarSucursal.aspx\">Sucursal</a></li>");
    document.writeln("<li><a href=\"" + atras + "Informes/Detalles_informes.aspx\">Informes</a></li>");
    
       
   // document.writeln("<li><a href=\""+atras+"fact/Agregar.aspx\">Facturaci&oacute;n</a></li>");
    /*
    * El formato para agregar un nuevo enlace es el siguiente:
    * document.writeln("<li><a href=\+atras+"nombre_del_abm_o_form.aspx\">Nombre_del_enlace</a></li>")
    */
    
    /* Esta seccion NO TOCAR, ya que hace el control para que solo el Admin observe esta opcion. */
    if (condicionante == 0){
        document.writeln("<li><a href=\""+atras+"usuario/Agregar.aspx\">Usuarios</a></li>");
    }    
    document.writeln("<li><a href=\""+atras+"Logout.aspx\">Salir</a></li>");
    document.write("</ul>");
}