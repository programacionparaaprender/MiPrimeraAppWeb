function listar() {
    $.get("Curso/listarCursos", function (data) {
        llenarTabla(data);
    });
}

function llenarTabla(data) {
    let idtabla = document.getElementById('idtabla');
    let html = '';
    if (data != null && data.length > 0) {
        html += ' <table id="tabla-curso" class="table table-dark">';
        html += '    <thead>';
        html += '        <tr>';
        html += '            <th>IIDCURSO</th>';
        html += '            <th>NOMBRE</th>';
        html += '            <th>DESCRIPCION</th>';
        html += '            <th>BHABILITADO</th>';
        html += '            <th>OPERACIONES</th>';
        html += '        </tr>';
        html += '    </thead>';
        html += '    <tbody>';
        for (let curso of data) {
            html += '<tr>';
            html += '<td>' + curso.IIDCURSO + '</td>';
            html += '<td>' + curso.NOMBRE + '</td>';
            html += '<td>' + curso.DESCRIPCION + '</td>';
            html += '<td>' + curso.BHABILITADO + '</td>';
            html += '<td><div class="btn-group" role="group" aria-label="Basic example">';
            html += '<button type="button" class="btn btn-success" onclick="abrirModal(' + curso.IIDCURSO + ')" data-bs-toggle="modal" data-bs-target="#exampleModal">';
            html += '<i class="fa fa-pencil" aria-hidden="true"></i>';
            html += '</button>';
            html += '<button type="button" class="btn btn-danger" onclick="eliminar(' + curso.IIDCURSO + ')">';
            html += '<i class="fa fa-trash" aria-hidden="true"></i>';
            html += '</button>';
            html += '</div></td>';
            html += '</tr>';
        }
        html += '    </tbody>';
        html += '</table>';
        idtabla.innerHTML = html;

        /*
         $('#tabla-curso').DataTable({
             data: data,
             columns: [
                 { title: 'IIDCURSO' },
                 { title: 'NOMBRE' },
                 { title: 'DESCRIPCION' },
                 { title: 'BHABILITADO.' }
             ],
         });
         */
        $("#tabla-curso").dataTable({
            searching: false
        });
    } else {   
        idtabla.innerHTML = 'Tabla vacia'
    }
}

function eliminar(id) {
    if (confirm('¿Desea realmente eliminar el curso?') == 1) {
        $.get("Curso/eliminarCurso?id=" + id, function (data) {
            listar();
        });
    }
}

function agregar() {
    try {
        if (datosObligatorios()) {
            if (confirm('¿Desea realmente guardar?') == 1) {
                let frm = new FormData();
                let IIDCURSO = document.getElementById('txtIdCurso').value;
                let NOMBRE = document.getElementById('txtnombrecurso').value;
                let DESCRIPCION = document.getElementById('txtdescripcion').value;
                frm.append('IIDCURSO', IIDCURSO);
                frm.append('NOMBRE', NOMBRE);
                frm.append('DESCRIPCION', DESCRIPCION);
                frm.append('BHABILITADO', 1);
                $.ajax({
                    type: "POST",
                    url: "Curso/guardarDatos",
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
        alert('ocurrio un error al registrar.');
    }
}

function abrirModal(IIDCURSO = 0) {
    if (IIDCURSO == 0) {
        borrarDatos();
        document.getElementById('txtIdCurso').title = 'Id del curso';
        document.getElementById('txtnombrecurso').title = 'Nombre del curso';
        document.getElementById('txtdescripcion').title = 'Descripción del curso';
    } else {
        $.get("Curso/recuperarCursos?id=" + IIDCURSO, function (data) {
            document.getElementById('txtIdCurso').value = data[0].IIDCURSO;
            document.getElementById('txtnombrecurso').value = data[0].NOMBRE;
            document.getElementById('txtdescripcion').value = data[0].DESCRIPCION;
            document.getElementById('txtIdCurso').title = 'Id del curso';
            document.getElementById('txtnombrecurso').title = 'Nombre del curso';
            document.getElementById('txtdescripcion').title = 'Descripción del curso';
        });
    }
}

var idbutton = document.getElementById('idbutton');
idbutton.onclick = function () {
    var txtnombre = document.getElementById('txtnombre').value;
    $.get("Curso/buscarCursos?nombre=" + txtnombre, function (data) {
        llenarTabla(data);
    });
}

var idlimpiar = document.getElementById('idlimpiar');
idlimpiar.onclick = function () {
    $.get("Curso/listarCursos", function (data) {
        llenarTabla(data);
    });
}

listar();
