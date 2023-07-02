function listar() {
    $.get("Matricula/listar", function (data) {
        try {
            let columns = ["Id", "Nombre Periodo", "Nombre Grado", "Nombre Sección", "Nombre Alumno"];
            llenarTablaData(columns, data);
        } catch (e) {
            console.log(e);
        }
    });
    $.get("Matricula/listarPeriodo", function (data) {
        try {
            llenarComboboxElement(data, 'cboPeriodo');
        } catch (e) {
            console.log(e);
        }
    });

    $.get("Matricula/listarGradoSeccion", function (data) {
        try {
            llenarComboboxElement(data, 'cboGradoSeccion');
        } catch (e) {
            console.log(e);
        }
    });

    $.get("Matricula/listarAlumnos", function (data) {
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
        $.get("Matricula/recuperarInformacion?id=" + IID, function (data) {

            obtenerPorId('txtIdMatricula').value = data[0].IID;
            obtenerPorId('cboPeriodo').value = data[0].IIDPERIODO;
            obtenerPorId('cboGradoSeccion').value = data[0].IIDGRADOSECCION;
            obtenerPorId('cboAlumno').value = data[0].IIDALUMNO;
        });
    }
}

function eliminar(id) {
    if (confirm('¿Desea realmente eliminar?') == 1) {
        $.get("GradoSeccionAula/eliminarGradoSeccionAula?id=" + id, function (data) {
            listar();
        });
    }
}


listar();