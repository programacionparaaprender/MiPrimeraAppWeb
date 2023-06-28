function listar() {
    $.get("Docente/listarDocentes", function (data) {
        try {
            llenarTabla(data);
            //llenarCombobox(data);
        } catch (e) {
            console.log(e);
        }
    });
    $.get("Docente/listarModalidadContrato", function (data) {
        //llenarModalidadContrato(data);
        llenarModalidadContrato(data, 'cboContrato');
        llenarModalidadContrato(data, 'cboModalidadContratoPopup');
    });
    $.get("Alumno/listarSexo", function (data) {
        llenarModalidadContrato(data, 'cboSexo');
    });
}

function llenarModalidadContrato(data) {
    let cbocombollenar = document.getElementById('cboContrato');
    if (data != null && data.length > 0) {
        llenarCombo(data, cbocombollenar);
    } else {
        cbocombollenar.innerHTML = ''
    }
}

function llenarModalidadContrato(data, texto) {
    let cbocombollenar = document.getElementById(texto);
    if (data != null && data.length > 0) {
        llenarCombo(data, cbocombollenar);
    } else {
        cbocombollenar.innerHTML = ''
    }
}

function llenarTabla(data) {
    let idtabla = document.getElementById('idtabla');
    let html = '';
    if (data != null && data.length > 0) {
        html += ' <table id="tabla-curso" class="table table-dark">';
        html += '    <thead>';
        html += '        <tr>';

        html += '<th>IID</th>';
        html += '<th>NOMBRE</th>';
        html += '<th>APPATERNO</th>';
        html += '<th>APMATERNO</th>';
        html += '<th>DIRECCION</th>';
        //html += '<th>TELEFONOCELULAR</th>';
        //html += '<th>TELEFONOFIJO</th>';
        //html += '<th>EMAIL</th>';
        //html += '<th>IIDSEXO</th>';
        //html += '<th>FECHACONTRATO</th>';
        //html += '<th>FOTO</th>';
        //html += '<th>IIDMODALIDADCONTRATO</th>';
        //html += '<th>BHABILITADO</th>';
        //html += '<th>IIDTIPOUSUARIO</th>';
        //html += '<th>bTieneUsuario</th>';
        html += '            <th>OPERACIONES</th>';
        html += '        </tr>';
        html += '    </thead>';
        html += '    <tbody>';
        for (let curso of data) {
            html += '<tr>';
            html += '<td>' + curso.IID + '</td>';
            html += '<td>' + curso.NOMBRE + '</td>';
            html += '<td>' + curso.APPATERNO + '</td>';
            html += '<td>' + curso.APMATERNO + '</td>';
            html += '<td>' + curso.DIRECCION + '</td>';
            //html += '<td>' + curso.TELEFONOCELULAR + '</td>';
            //html += '<td>' + curso.TELEFONOFIJO + '</td>';
            //html += '<td>' + curso.EMAIL + '</td>';
            //html += '<td>' + curso.IIDSEXO + '</td>';
            //html += '<td>' + moment(curso.FECHACONTRATO).format("DD/MM/yyyy") + '</td>';
            //html += '<td><img alt="foto" src="' + curso.FOTO + '" width="20" height="20" /></td>';
            ////html += '<td><img alt="foto" src="@Url.Action("getImage","Docente")" width="20" height="20" /></td>';
            //html += '<td>' + curso.IIDMODALIDADCONTRATO + '</td>';
            //html += '<td>' + curso.BHABILITADO + '</td>';
            //html += '<td>' + curso.IIDTIPOUSUARIO + '</td>';
            //html += '<td>' + curso.bTieneUsuario + '</td>';
            html += '<td><div class="btn-group" role="group" aria-label="Basic example">';
            html += '<button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#exampleModal" onclick="abrirModal(' + curso.IID + ')">';
            html += '<i class="fa fa-pencil" aria-hidden="true"></i>';
            html += '</button>';
            html += '<button type="button" class="btn btn-danger" onclick="eliminar(' + curso.IID + ')">';
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

function abrirModal(IIDDOCENTE = 0) {
    if (IIDDOCENTE == 0) {
        borrarDatos();
    } else {
        $.get("Docente/recuperarDocente?id=" + IIDDOCENTE, function (data) {
            try {
                console.log('"data:image/jpeg;base64," + data[0].FOTO;');
                console.log("data:image/jpeg;base64," + data[0].FOTO);
                document.getElementById('txtIdDocente').value = data[0].IIDDOCENTE;
                document.getElementById('txtnombredocente').value = data[0].NOMBRE;
                document.getElementById('txtappaterno').value = data[0].APPATERNO;
                document.getElementById('txtapmaterno').value = data[0].APMATERNO;
                document.getElementById('txtdireccion').value = data[0].DIRECCION;
                document.getElementById('txttelefonofijo').value = data[0].TELEFONOFIJO;
                document.getElementById('txttelefonocelular').value = data[0].TELEFONOCELULAR;
                document.getElementById('txtemail').value = data[0].EMAIL;
                document.getElementById('cboSexo').value = data[0].IIDSEXO;
                document.getElementById('dtFechaContrato').value = moment(data[0].FECHACONTRATO).format("yyyy-MM-DD");
                document.getElementById('cboModalidadContratoPopup').value = data[0].IIDMODALIDADCONTRATO;
                document.getElementById('imgFoto').value = "data:image/png;base64," + data[0].FOTO;

            } catch (e) {
                console.log(e);
            }

        });
    }
}

function eliminar(id) {
    if (confirm('¿Desea realmente eliminar el docente?') == 1) {
        $.get("Docente/eliminarDocente?id=" + id, function (data) {
            listar();
        });
    }
}


/*
var btnBuscarModalidadContrato = document.getElementById('btnBuscarModalidadContrato');
btnBuscarModalidadContrato.onclick = function () {
    var cboContrato = document.getElementById('cboContrato').value;
    $.get("Docente/buscarDocenteModalidadContrato?ModalidadContrato=" + cboContrato, function (data) {
        llenarTabla(data);
    });
}
*/
var cboContrato = document.getElementById('cboContrato');
cboContrato.onchange = function () {
    var cboContrato2 = document.getElementById('cboContrato').value;
    $.get("Docente/buscarDocenteModalidadContrato?ModalidadContrato=" + cboContrato2, function (data) {
        llenarTabla(data);
    });
}


var idbutton = document.getElementById('idbutton');
idbutton.onclick = function () {
    var txtnombre = document.getElementById('txtnombredocente').value;
    $.get("Alumno/buscarAlumnos?nombre=" + txtnombre, function (data) {
        llenarTabla(data);
    });
}

var idlimpiar = document.getElementById('idlimpiar');
idlimpiar.onclick = function () {
    listar();
}
/*
var idlimpiarCombo = document.getElementById('idlimpiarCombo');
idlimpiarCombo.onclick = function () {
    listar();
}
*/

const inputElement = document.getElementById("avatar");
inputElement.onchange = function () {
    let file = document.getElementById("avatar").files[0];
    let reader = new FileReader();
    if (reader != null) {
        reader.onloadend = function () {
            let img = document.getElementById('imgFoto');
            img.src = reader.result;
            let datos = reader.result.replace("data:image/png;base64,", "");
            datos = datos.replace("data:image/jpeg;base64,", "");
            document.getElementById("imgFotoHidden").value = datos;
            //alert(datos);
        }
    }
    reader.readAsDataURL(file);
}

function agregar() {
    try {
        if (datosObligatorios()) {
            if (confirm('¿Desea realmente guardar?') == 1) {
                let frm = new FormData();


                let IIDDOCENTE = document.getElementById('txtIdDocente').value;
                let NOMBRE = document.getElementById('txtnombredocente').value;
                let APPATERNO = document.getElementById('txtappaterno').value;
                let APMATERNO = document.getElementById('txtapmaterno').value;
                let DIRECCION = document.getElementById('txtdireccion').value;
                let TELEFONOFIJO = document.getElementById('txttelefonofijo').value;
                let TELEFONOCELULAR = document.getElementById('txttelefonocelular').value;
                let EMAIL = document.getElementById('txtemail').value;
                let IIDSEXO = document.getElementById('cboSexo').value;
                let fec = document.getElementById('dtFechaContrato').value;
                let FECHACONTRATO = moment(fec, "DD/MM/yyyy").toDate();
                let IIDMODALIDADCONTRATO = document.getElementById('cboModalidadContratoPopup').value;
                //let FOTO = document.getElementById('imgFoto').src;
                let FOTO = utf8_to_b64(document.getElementById("imgFotoHidden").value);
                console.log('FOTO');
                console.log(FOTO);
                //let datos = document.getElementById('imgFoto').innerText.replace("data:image/png;base64,", "");
                //let FOTOSTRING = datos.replace("data:image/jpeg;base64,", "");

                frm.append('IIDDOCENTE', IIDDOCENTE);
                frm.append('NOMBRE', NOMBRE);
                frm.append('APPATERNO', APPATERNO);
                frm.append('APMATERNO', APMATERNO);
                frm.append('DIRECCION', DIRECCION);
                frm.append('TELEFONOFIJO', TELEFONOFIJO);
                frm.append('TELEFONOCELULAR', TELEFONOCELULAR);
                frm.append('EMAIL', EMAIL);
                frm.append('IIDSEXO', IIDSEXO);
                frm.append('FECHACONTRATO', FECHACONTRATO);
                frm.append('FECHACONTRATOSTRING', fec);
                frm.append('IIDMODALIDADCONTRATO', IIDMODALIDADCONTRATO);
                frm.append('FOTO', FOTO);
                //frm.append('FOTOSTRING', FOTOSTRING);
                //console.log('fec');
                //console.log(fec);
                frm.append('BHABILITADO', 1);
                $.ajax({
                    type: "POST",
                    url: "Docente/guardarDatos",
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

