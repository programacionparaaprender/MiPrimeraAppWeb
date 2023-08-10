
var base_url = window.location.origin;
const url = base_url + "/RolPagina";

function listar() {
    $.get(url + "/listarRol", function (data) {
        try {
            let columns = ["Id", "Nombre Rol", "Descripción Rol"];
            llenarTablaData(columns, data);
        } catch (e) {
            console.log(e);
        }
    });
}



function abrirModal(IID = 0) {
    $.get(url + "/listarPaginas", function (data) {
        try {
            var contenido = "<tbody>";
            for(let dat of data){
                contenido += '<tr>';
                contenido += '<td>';
                if(dat.BHABILITADO == 1){
                    contenido += "<input class='checkbox' type='checkbox' id='"+dat.IIDPAGINA+"' checked='true' />";
                } else {
                    contenido += "<input class='checkbox' type='checkbox' id='" + dat.IIDPAGINA +"' />";
                }
                contenido += '</td>';
                contenido += '<td>';
                contenido += dat.MENSAJE;
                contenido += '</td>';
                contenido += '</tr>';
            }
            contenido += "</tbody>";
            obtenerPorId('tblpagina').innerHTML = contenido;
        } catch (e) {
            console.log(e);
        }
    });
    if (IID == 0) {
        borrarDatos();
        
    } else {
        
        $.get(url + "/recuperarInformacion?id=" + IID, function (data) {
            obtenerPorId('txtidrol').value = data[0].IID;
            obtenerPorId('txtnombrerol').value = data[0].NOMBRE;
            obtenerPorId('txtdescripcionrol').value = data[0].DESCRIPCION;
        });
    }
}

    function eliminar(id) {
        if (confirm('¿Desea realmente eliminar?') == 1) {
            $.get(url + "/eliminarInformacion?id=" + id, function (data) {
                if(data == 1){
                    alert('Se elimino correctamente');
                    listar();
                }else {
                    alert('Ocurrio un error');
                }
            });
        }
    }

    function agregar() {
        try {
            if (datosObligatorios()) {
                if (confirm('¿Desea realmente guardar?') == 1) {
                    let frm = new FormData();

                    let IIDROL = obtenerPorId('txtidrol').value;
                    let NOMBRE = obtenerPorId('txtnombrerol').value;
                    let DESCRIPCION = obtenerPorId('txtdescripcionrol').value;

                    frm.append('IIDROL', IIDROL);
                    frm.append('NOMBRE', NOMBRE);
                    frm.append('DESCRIPCION', DESCRIPCION);
                    frm.append('BHABILITADO', 1);
                    var checkbox = document.getElementsByClassName("checkbox");
                    var valorAEnviar = '';
                    for (let check of checkbox) {
                        if (check.checked) {
                            valorAEnviar += check.id;
                            valorAEnviar += "$";
                        }
                    }
                

                    valorAEnviar = valorAEnviar.substring(0, valorAEnviar.length - 1);
                    frm.append('valorAEnviar', valorAEnviar);
                    $.ajax({
                        type: "POST",
                        url: url + "/guardarDatos",
                        data: frm,
                        //dataType: "json",
                        contentType: false,
                        processData: false,
                        success: function (data) {
                            if (data == -1) {
                                alert('Ya existe.');
                            }
                            else if (data != 0) {
                                alert('guardado con exito.');
                                listar();
                                document.getElementById('btnCancelar').click();
                            } else {
                                alert('ocurrio un error.');
                            }
                        }
                    });
                }
            }
        } catch (e) {
            console.log(e);
            alert('ocurrio un error al registrar.');
        }
    }



    listar();