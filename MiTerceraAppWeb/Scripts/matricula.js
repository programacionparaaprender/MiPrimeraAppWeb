
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
    }
}

function eliminar(id) {
    if (confirm('¿Desea realmente eliminar?') == 1) {
        $.get(url + "/eliminarMatricula?id=" + id, function (data) {
            listar();
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
                frm.append('IIDGRADOSECCION', IIDGRADOSECCION);
                frm.append('IIDALUMNO', IIDALUMNO);
                frm.append('BHABILITADO', 1);
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