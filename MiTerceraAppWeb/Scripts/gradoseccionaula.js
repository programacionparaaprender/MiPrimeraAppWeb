let periodo = obtenerPorId('cboPeriodo');
let gradoseccion = obtenerPorId('cboGradoSeccion');

periodo.onchange = function () {
    cargarComboCurso();
}

gradoseccion.onchange = function () {
    cargarComboCurso();
}

function cargarComboCurso(elementoSeleccionado = 1) {
    if (!EmptyHTMLElement(periodo) && !EmptyHTMLElement(gradoseccion)) {
        const url = "GradoSeccionAula/listarCursos?IIDPERIODO=" + periodo.value + "&IIDGRADOSECCION=" + gradoseccion.value;
        $.get(url, function (data) {
            try {
                llenarComboboxElement(data, 'cboCurso', elementoSeleccionado);
            } catch (e) {
                console.log(e);
            }
        });
    }
}

function listar() {
    $.get("GradoSeccionAula/listar", function (data) {
        try {
            let columns = ["Id", "Nombre Periodo", "Nombre Curso", "Nombre Docente", "Nombre Grado"];
            llenarTablaData(columns, data);
        } catch (e) {
            console.log(e);
        }
    });

    $.get("GradoSeccionAula/listarPeriodo", function (data) {
        try {
            llenarComboboxElement(data, 'cboPeriodo');
        } catch (e) {
            console.log(e);
        }
    });

    $.get("GradoSeccionAula/listarAulas", function (data) {
        try {
            llenarComboboxElement(data, 'cboAula');
        } catch (e) {
            console.log(e);
        }
    });

    $.get("GradoSeccionAula/listarDocentes", function (data) {
        try {
            llenarComboboxElement(data, 'cboDocente');
        } catch (e) {
            console.log(e);
        }
    });

    $.get("GradoSeccionAula/listarGradoSeccion", function (data) {
        try {
            llenarComboboxElement(data, 'cboGradoSeccion');
        } catch (e) {
            console.log(e);
        }
    });

   
}

function abrirModal(IID = 0) {
    if (IID == 0) {
        borrarDatos();
        
    } else {
        $.get("GradoSeccionAula/recuperarInformacion?id=" + IID, function (data) {
            obtenerPorId('txtIdPeriodoGradoCurso').value = data[0].IID;
            obtenerPorId('cboPeriodo').value = data[0].IIDPERIODO;
            obtenerPorId('cboGradoSeccion').value = data[0].IIDGRADOSECCION;

            cargarComboCurso(data[0].IIDCURSO);

            obtenerPorId('cboCurso').value = data[0].IIDCURSO;
            obtenerPorId('cboDocente').value = data[0].IIDDOCENTE;
            obtenerPorId('cboAula').value = data[0].IIDAULA;
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

    function agregar() {
        try {
            if (datosObligatorios()) {
                if (confirm('¿Desea realmente guardar?') == 1) {
                    let frm = new FormData();

                    let IID = obtenerPorId('txtIdPeriodoGradoCurso').value;
                    let IIDPERIODO = obtenerPorId('cboPeriodo').value;
                    let IIDGRADOSECCION = obtenerPorId('cboGradoSeccion').value;
                    let IIDCURSO = obtenerPorId('cboCurso').value;
                    let IIDDOCENTE = obtenerPorId('cboDocente').value;
                    let IIDAULA = obtenerPorId('cboAula').value;

                    frm.append('IID', IID);
                    frm.append('IIDPERIODO', IIDPERIODO);
                    frm.append('IIDGRADOSECCION', IIDGRADOSECCION);
                    frm.append('IIDCURSO', IIDCURSO);
                    frm.append('IIDDOCENTE', IIDDOCENTE);
                    frm.append('IIDAULA', IIDAULA);
                    frm.append('BHABILITADO', 1);
                    $.ajax({
                        type: "POST",
                        url: "GradoSeccionAula/guardarDatos",
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