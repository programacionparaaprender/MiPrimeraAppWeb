function listar() {
    $.get("GradoSeccion/obtenerGradoSeccion", function (data) {
        try {
            let columns = ["Id Grado Sección","Nombre Grado","Nombre Sección"];
            llenarTablaData(columns, data);
        } catch (e) {
            console.log(e);
        }
    });
    $.get("GradoSeccion/listarSeccion", function (data) {
        try {
            llenarComboboxElement(data, 'cboSeccion');
        } catch (e) {
            console.log(e);
        }
    });
    $.get("GradoSeccion/listarGrado", function (data) {
        try {
            llenarComboboxElement(data, 'cboPeriodo');
        } catch (e) {
            console.log(e);
        }
    });
}

function abrirModal(IID = 0) {
    if (IID == 0) {
        borrarDatos();
    } else {
        $.get("GradoSeccion/recuperarInformacion?id=" + IID, function (data) {
            try {
                document.getElementById('txtIdGradoSeccion').value = data[0].IID;
                document.getElementById('cboPeriodo').value = data[0].IIDGRADO;
                document.getElementById('cboSeccion').value = data[0].IIDSECCION;
            } catch (e) {
                console.log(e);
            }

        });
    }
}

function eliminar(id) {
    if (confirm('¿Desea realmente eliminar el grado sección?') == 1) {
        $.get("GradoSeccion/eliminarGradoSeccion?id=" + id, function (data) {
            listar();
        });
    }
}

function agregar() {
    try {
        if (datosObligatorios()) {
            if (confirm('¿Desea realmente guardar?') == 1) {
                let frm = new FormData();
                let IID = document.getElementById('txtIdGradoSeccion').value;
                let IIDGRADO = document.getElementById('cboPeriodo').value;
                let IIDSECCION = document.getElementById('cboSeccion').value;
                frm.append('IID', IID);
                frm.append('IIDGRADO', IIDGRADO);
                frm.append('IIDSECCION', IIDSECCION);
                frm.append('BHABILITADO', 1);
                $.ajax({
                    type: "POST",
                    url: "GradoSeccion/guardarDatos",
                    data: frm,
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