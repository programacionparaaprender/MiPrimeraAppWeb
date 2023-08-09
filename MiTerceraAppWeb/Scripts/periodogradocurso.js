function listar() {
    $.get("PeriodoGradoCurso/listarPeriodoGradoCurso", function (data) {
        try {
            let columns = ["Id", "Nombre Periodo", "Nombre Grado", "Nombre Curso"];
            llenarTablaData(columns, data);
        } catch (e) {
            console.log(e);
        }
    });

    $.get("PeriodoGradoCurso/listarPeriodo", function (data) {
        try {
            llenarComboboxElement(data, 'cboPeriodo');
        } catch (e) {
            console.log(e);
        }
    });

    $.get("PeriodoGradoCurso/listarGrado", function (data) {
        try {
            llenarComboboxElement(data, 'cboGrado');
        } catch (e) {
            console.log(e);
        }
    });

    $.get("PeriodoGradoCurso/listarCurso", function (data) {
        try {
            llenarComboboxElement(data, 'cboCurso');
        } catch (e) {
            console.log(e);
        }
    });
}

function abrirModal(IID = 0) {
    if (IID == 0) {
        borrarDatos();
        
    } else {
        $.get("PeriodoGradoCurso/recuperarPeriodoGradoCurso?id=" + IID, function (data) {
            document.getElementById('txtIdPeriodoGradoCurso').value = data[0].IID;
            document.getElementById('cboPeriodo').value = data[0].IIDPERIODO;
            document.getElementById('cboGrado').value = data[0].IIDGRADO;
            document.getElementById('cboCurso').value = data[0].IIDCURSO;
        });
    }
}

function eliminar(id) {
    if (confirm('¿Desea realmente eliminar?') == 1) {
        $.get("PeriodoGradoCurso/eliminarPeriodoGradoCurso?id=" + id, function (data) {
            listar();
        });
    }
}

function agregar() {
    try {
        if (datosObligatorios()) {
            if (confirm('¿Desea realmente guardar?') == 1) {
                let frm = new FormData();
                let IID = document.getElementById('txtIdPeriodoGradoCurso').value;
                let IIDPERIODO = document.getElementById('cboPeriodo').value;
                let IIDGRADO = document.getElementById('cboGrado').value;
                let IIDCURSO = document.getElementById('cboCurso').value;

                frm.append('IID', IID);
                frm.append('IIDPERIODO', IIDPERIODO);
                frm.append('IIDGRADO', IIDGRADO);
                frm.append('IIDCURSO', IIDCURSO);
                frm.append('BHABILITADO', 1);
                $.ajax({
                    type: "POST",
                    url: "PeriodoGradoCurso/guardarDatos",
                    data: frm,
                    //dataType: "json",
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data == -1) {
                            alert('Ya existe registro.');
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