$('#datepickerInicio').datepicker({
    dateFormar: "dd/mm/yyyy",
    changeMonth: true,
    changeYear: true
});
$('#datepickerFin').datepicker({
    dateFormar: "dd/mm/yyyy",
    changeMonth: true,
    changeYear: true
});

function listar() {
    $.get("Periodo/listarPeriodo", function (data) {
        llenarTabla(data);
    });
}

function abrirModal(IIDPERIODO = 0) {
    if (IIDPERIODO == 0) {
        borrarDatos();
    } else {
        $.get("Periodo/recuperarPeriodo?id=" + IIDPERIODO, function (data) {
            document.getElementById('txtIdPeriodo').value = data[0].IIDPERIODO;
            document.getElementById('txtnombreperiodo').value = data[0].NOMBRE;
            document.getElementById('datepickerInicio').value = moment(data[0].FECHAINICIO).format("DD/MM/yyyy");
            document.getElementById('datepickerFin').value = moment(data[0].FECHAFIN).format("DD/MM/yyyy");
        });
    }
}

function eliminar(id) {
    if (confirm('¿Desea realmente eliminar?') == 1) {
        $.get("Periodo/eliminarPeriodo?id=" + id, function (data) {
            listar();
        });
    }
}

function llenarTabla(data) {
    let idtabla = document.getElementById('idtabla');
    let html = '';
    if (data != null && data.length > 0) {
        html += '<table id="tabla-curso" class="table table-dark">';
        html += '    <thead>';
        html += '        <tr>';
        html += '           <th>IIDPERIODO</th>';
        html += '            <th>NOMBRE</th>';
        html += '            <th>FECHAINICIO</th>';
        html += '            <th>FECHAFIN</th>';
        html += '            <th>BHABILITADO</th>';
        html += '            <th>OPERACIONES</th>';
        html += '        </tr>';
        html += '    </thead>';
        html += '    <tbody>';
        for (let curso of data) {
            html += '<tr>';
            html += '<td>' + curso.IIDPERIODO + '</td>';
            html += '<td>' + curso.NOMBRE + '</td>';
            html += '<td>' + moment(curso.FECHAINICIO).format("DD/MM/yyyy") + '</td>';
            html += '<td>' + moment(curso.FECHAFIN).format("DD/MM/yyyy") + '</td>';
            html += '<td>' + curso.BHABILITADO + '</td>';
            html += '<td><div class="btn-group" role="group" aria-label="Basic example">';
            html += '<button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#exampleModal"  onclick="abrirModal(' + curso.IIDPERIODO + ')">';
            html += '<i class="fa fa-pencil" aria-hidden="true"></i>';
            html += '</button>';
            html += '<button type="button" class="btn btn-danger" onclick="eliminar(' + curso.IIDPERIODO + ')">';
            html += '<i class="fa fa-trash" aria-hidden="true"></i>';
            html += '</button>';
            html += '</div></td>';
            html += '</tr>';
        }
        html += '    </tbody>';
        html += '</table>';
        idtabla.innerHTML = html;
        $("#tabla-curso").dataTable({
            searching: false
        });
    } else {

        idtabla.innerHTML = 'Tabla vacia'
    }
}

var idbutton = document.getElementById('idbutton');
idbutton.onclick = function () {
    var txtnombre = document.getElementById('txtnombre').value;
    $.get("Periodo/buscarPeriodo?nombre=" + txtnombre, function (data) {
        llenarTabla(data);
    });
}

var idlimpiar = document.getElementById('idlimpiar');
idlimpiar.onclick = function () {
    listar();
}

function agregar() {
    try {
        if (datosObligatorios()) {
            if (confirm('¿Desea realmente guardar?') == 1) {
                let frm = new FormData();
                let IIDPERIODO = document.getElementById('txtIdPeriodo').value;
                let NOMBRE = document.getElementById('txtnombreperiodo').value;
                let FECHAINICIO = moment(document.getElementById('datepickerInicio').value).format("DD/MM/yyyy");
                let FECHAFIN = moment(document.getElementById('datepickerFin').value).format("DD/MM/yyyy");
                frm.append('IIDPERIODO', IIDPERIODO);
                frm.append('NOMBRE', NOMBRE);
                frm.append('FECHAINICIO', FECHAINICIO);
                frm.append('FECHAFIN', FECHAFIN);
                frm.append('BHABILITADO', 1);
                $.ajax({
                    type: "POST",
                    url: "Periodo/guardarDatos",
                    data: frm,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data != 0) {
                            alert('guardado con exito.');
                            listar();
                            document.getElementById('btnCancelar').click();
                        } else {
                            if (data == -1) {
                                alert('Ya existe.');
                            } else {
                                alert('ocurrio un error.');
                            }
                        }
                    }
                });
            }
        }
    } catch (e) {
        alert('ocurrio un error al registrar.');
    }
}


listar();