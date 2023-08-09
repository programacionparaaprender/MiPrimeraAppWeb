

function listar() {

    $.get("Alumno/listarAlumnos", function (data) {
        //let llaves = Object.keys(data[0]);
        //console.log(llaves);
        //let columns = [];
        //let dataSet = [];
        //for (let column of llaves) {
        //    columns.push({ title: column });
        //}
        //llaves = Object.values(data[0]);
        //console.log(llaves);
        //for (let alumno of data) {
        //    dataSet.push(Object.values(alumno));
        //}
        //console.log(columns);

        //const columnas = ['IID', 'IIDALUMNO', 'NOMBRE', 'APPATERNO', 'APMATERNO', 'FECHANACIMIENTO', 'IIDSEXO', 'TELEFONOPADRE', 'TELEFONOMADRE', 'NUMEROHERMANOS', 'BHABILITADO', 'IIDTIPOUSUARIO', 'bTieneUsuario']
        //llenarTabla(columnas, data);
        //$('#tabla-curso').DataTable({
        //    data: data,
        //    columns: [
        //        { title: 'IIDCURSO' },
        //        { title: 'NOMBRE' },
        //        { title: 'DESCRIPCION' },
        //        { title: 'BHABILITADO.' }
        //    ],
        //});
        //$('#tabla-curso').DataTable({
        //    data: dataSet,
        //    columns: columns,
        //});
        llenarTabla(data);
        llenarCombobox(data);
    });
    $.get("Alumno/listarSexo", function (data) {
        llenarSexo(data);
        llenarSexoPopup(data);
    });
}

function llenarCombobox(data) {
    let cbocombollenar = document.getElementById('cbocombollenar');
    if (data != null && data.length > 0) {
        llenarCombo(data, cbocombollenar);
    } else {
        cbocombollenar.innerHTML = ''
    }
}

function llenarSexo(data) {
    let cbocombollenar = document.getElementById('cboSexo');
    if (data != null && data.length > 0) {
        llenarCombo(data, cbocombollenar);
    } else {
        cbocombollenar.innerHTML = ''
    }
}

function llenarSexoPopup(data) {
    let cbocombollenar = document.getElementById('cboSexoPopup');
    if (data != null && data.length > 0) {
        llenarCombo(data, cbocombollenar);
    } else {
        cbocombollenar.innerHTML = ''
    }
}


function llenarTabla(columnas, data) {
    let idtabla = document.getElementById('idtabla');
    let html = '';
    if (data != null && data.length > 0) {
        html += ' <table id="tabla-curso" class="table table-dark">';
        html += '    <thead>';
        html += '        <tr>';
        for (let columna of columnas) {
            html += '<th>'+columna+'</th>';
        }
        html += '        </tr>';
        html += '    </thead>';
        html += '    <tbody>';
        let llaves = Object.keys(data[0]);
        for (var i = 0; i < data.length; i++) {
            html += '<tr>';
            for (var j = 0; j < llaves.length; j++) {
                const valorLlaves = llaves[j];
                html += '<td>' + curso[i][valorLlaves] + '</td>';
            }
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

function llenarTabla(data) {
    let idtabla = document.getElementById('idtabla');
    let html = '';
    if (data != null && data.length > 0) {
        html += ' <table id="tabla-curso" class="table table-dark">';
        html += '    <thead>';
        html += '        <tr>';
        html += '            <th>IIDALUMNO</th>';
        html += '            <th>NOMBRE</th>';
        html += '            <th>APPATERNO</th>';
        html += '            <th>APMATERNO</th>';
        html += '            <th>FECHANACIMIENTO</th>';
        //html += '            <th>IIDSEXO</th>';
        //html += '            <th>TELEFONOPADRE</th>';
        //html += '            <th>TELEFONOMADRE</th>';
        //html += '            <th>NUMEROHERMANOS</th>';
        //html += '            <th>BHABILITADO</th>';
        //html += '            <th>IIDTIPOUSUARIO</th>';
        //html += '            <th>bTieneUsuario</th>';
        html += '            <th>OPERACIONES</th>';
        html += '        </tr>';
        html += '    </thead>';
        html += '    <tbody>';
        for (let curso of data) {
            html += '<tr>';
            html += '<td>' + curso.IIDALUMNO + '</td>';
            html += '<td>' + curso.NOMBRE + '</td>';
            html += '<td>' + curso.APPATERNO + '</td>';
            html += '<td>' + curso.APMATERNO + '</td>';
            html += '<td>' + moment(curso.FECHANACIMIENTO).format("DD/MM/yyyy") + '</td>';
            //html += '<td>' + curso.IIDSEXO + '</td>';
            //html += '<td>' + curso.TELEFONOPADRE + '</td>';
            //html += '<td>' + curso.TELEFONOMADRE + '</td>';
            //html += '<td>' + curso.NUMEROHERMANOS + '</td>';
            //html += '<td>' + curso.BHABILITADO + '</td>';
            //html += '<td>' + curso.IIDTIPOUSUARIO + '</td>';
            //html += '<td>' + curso.bTieneUsuario + '</td>';
            html += '<td><div class="btn-group" role="group" aria-label="Basic example">';
            html += '<button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#exampleModal"  onclick="abrirModal(' + curso.IIDALUMNO + ')">';
            html += '<i class="fa fa-pencil" aria-hidden="true"></i>';
            html += '</button>';
            html += '<button type="button" class="btn btn-danger" onclick="eliminar(' + curso.IIDALUMNO + ')">';
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

function eliminar(id) {
    if (confirm('¿Desea realmente eliminar el alumno?') == 1) {
        $.get("Alumno/eliminarAlumno?id=" + id, function (data) {
            listar();
        });
    }
}

function abrirModal(IIDALUMNO = 0) {
    if (IIDALUMNO == 0) {
        borrarDatos();
    } else {
        $.get("Alumno/recuperarAlumno?id=" + IIDALUMNO, function (data) {
            try {
                document.getElementById('txtIdAlumno').value = data[0].IIDALUMNO;
                document.getElementById('txtnombrealumno').value = data[0].NOMBRE;
                document.getElementById('txtappaterno').value = data[0].APPATERNO;
                document.getElementById('txtapmaterno').value = data[0].APMATERNO;

  
                document.getElementById('txtfecnacimiento').value = moment(data[0].FECHANACIMIENTO).format("yyyy-MM-DD");
                document.getElementById('txttelpadre').value = data[0].TELEFONOPADRE;
                document.getElementById('txttelmadre').value = data[0].TELEFONOMADRE;
                document.getElementById('txtnrohermanos').value = data[0].NUMEROHERMANOS;
            } catch (e) {
                console.log(e);
            }

        });
    }
}


var btnBuscarSexo = document.getElementById('btnBuscarSexo');
btnBuscarSexo.onclick = function () {
    var cboSexo = document.getElementById('cboSexo').value;
    $.get("Alumno/buscarAlumnossexo?sexo=" + cboSexo, function (data) {
        llenarTabla(data);
    });
}

var idbutton = document.getElementById('idbutton');
idbutton.onclick = function () {
    var txtnombrealumno = document.getElementById('txtnombrealumno').value;
    $.get("Alumno/buscarAlumnos?nombre=" + txtnombrealumno, function (data) {
        llenarTabla(data);
    });
}

var idlimpiar = document.getElementById('idlimpiar');
idlimpiar.onclick = function () {
    $.get("Alumno/listarAlumnos", function (data) {
        llenarTabla(data);
    });
}

var idlimpiarCombo = document.getElementById('idlimpiarCombo');
idlimpiarCombo.onclick = function () {
    $.get("Alumno/listarAlumnos", function (data) {
        llenarTabla(data);
    });
}
function agregar() {
    try {
        if (datosObligatorios()) {
            if (confirm('¿Desea realmente guardar?') == 1) {
                let frm = new FormData();
                let IIDALUMNO = document.getElementById('txtIdAlumno').value;
                let NOMBRE = document.getElementById('txtnombrealumno').value;
                let APPATERNO = document.getElementById('txtappaterno').value;
                let APMATERNO = document.getElementById('txtapmaterno').value;

                let fec = document.getElementById('txtfecnacimiento').value;
                let FECHANACIMIENTO = moment(fec, "DD/MM/yyyy").toDate();
                let IIDSEXO = document.getElementById('cboSexoPopup').value;
                let TELEFONOPADRE = document.getElementById('txttelpadre').value;
                let TELEFONOMADRE = document.getElementById('txttelmadre').value;
                let NUMEROHERMANOS = document.getElementById('txtnrohermanos').value;

                frm.append('IIDALUMNO', IIDALUMNO);
                frm.append('NOMBRE', NOMBRE);
                frm.append('APPATERNO', APPATERNO);
                frm.append('APMATERNO', APMATERNO);
                frm.append('FECHANACIMIENTO', FECHANACIMIENTO);
                frm.append('FECHANACIMIENTOSTRING', fec);
                frm.append('IIDSEXO', IIDSEXO);
                frm.append('TELEFONOPADRE', TELEFONOPADRE);
                frm.append('TELEFONOMADRE', TELEFONOMADRE);
                frm.append('NUMEROHERMANOS', NUMEROHERMANOS);
                frm.append('BHABILITADO', 1);
                $.ajax({
                    type: "POST",
                    url: "Alumno/guardarDatos",
                    data: frm,
                    //dataType: "json",
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data == -1) {
                            alert('Alumno ya existe.');
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
        alert('ocurrio un error al registrar.');
    }
}

listar();