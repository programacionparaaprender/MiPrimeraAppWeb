
var base_url = window.location.origin;
const url = base_url +"/Matricula";
function listar() {
    $.get(url + "/listar", function (data) {
        try {
            let columns = ["Id", "Nombre Periodo", "Nombre Grado", "Nombre Sección", "Nombre Alumno"];
            llenarTablaData(columns, data);
        } catch (e) {
            console.log(e);
        }
    });
    $.get(url + "/listarPeriodo", function (data) {
        try {
            llenarComboboxElement(data, 'cboPeriodo');
        } catch (e) {
            console.log(e);
        }
    });

    $.get(url + "/listarGradoSeccion", function (data) {
        try {
            llenarComboboxElement(data, 'cboGradoSeccion');
        } catch (e) {
            console.log(e);
        }
    });

    $.get(url + "/listarAlumnos", function (data) {
        try {
            llenarComboboxElement(data, 'cboAlumno');
        } catch (e) {
            console.log(e);
        }
    });
}

function abrirModal(IID = 0) {
    if (IID == 0) {
        borrarDatos();
        
    } else {
        $.get(url + "/recuperarInformacion?id=" + IID, function (data) {

            obtenerPorId('txtIdMatricula').value = data[0].IID;
            obtenerPorId('cboPeriodo').value = data[0].IIDPERIODO;
            obtenerPorId('cboGradoSeccion').value = data[0].IIDGRADOSECCION;
            obtenerPorId('cboAlumno').value = data[0].IIDALUMNO;
        });
        $.get(url + '/Cursos?iidmatricula=' + IID, function(data){
            var contenido = "<tbody>";
            for(var i=0; i<data.length; i++){
                contenido += '<tr>';
                contenido += '<td>';
                if(data[i].bhabilitado == 1){
                    contenido += "<input class='checkbox' type='checkbox' id='"+data[i].IIDCURSO+"' checked='true' />";
                }else {
                    contenido += "<input class='checkbox' type='checkbox' id='" + data[i].IIDCURSO +"' />";
                }
                contenido += '</td>';
                contenido += '<td>';
                contenido += data[i].NOMBRE;
                contenido += '</td>';
                contenido += '</tr>';
            }
            contenido += "</tbody>";
            obtenerPorId('tablaCurso').innerHTML = contenido;
        });
    }
}

function eliminar(id) {
    if (confirm('¿Desea realmente eliminar?') == 1) {
        $.get(url + "/eliminarMatricula?id=" + id, function (data) {
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

                let IIDMATRICULA = obtenerPorId('txtIdMatricula').value;
                let IIDPERIODO = obtenerPorId('cboPeriodo').value;
                let IIDGRADOSECCION = obtenerPorId('cboGradoSeccion').value;
                let IIDALUMNO = obtenerPorId('cboAlumno').value;

                frm.append('IIDMATRICULA', IIDMATRICULA);
                frm.append('IIDPERIODO', IIDPERIODO);
                //frm.append('IIDSECCION', IIDGRADOSECCION);
                frm.append('IIDALUMNO', IIDALUMNO);
                frm.append('BHABILITADO', 1);
                frm.append('IIDGRADOSECCION', IIDGRADOSECCION);

                var checkbox = document.getElementsByClassName("checkbox");
                var valorAEnviar;
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
                        if (data != 0) {
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