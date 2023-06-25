function listar() {
    $.get("Docente/listarDocentes", function (data) {
        llenarTabla(data);
        //llenarCombobox(data);
    });
    $.get("Docente/listarModalidadContrato", function (data) {
        llenarModalidadContrato(data);
    });
}

function llenarCombo(data, control, primerElemento = 1) {
    let html = '';
    html += '<option value="" disabled>--Seleccione elemento--</option>';
    for (let i = 0; i < data.length; i++) {
        if (primerElemento == i) {
            html += '<option value="' + data[i].IID + '" selected>' + data[i].NOMBRE + '</option>';
        } else {
            html += '<option value="' + data[i].IID + '">' + data[i].NOMBRE + '</option>';
        }
    }
    control.innerHTML = html;
}

function llenarModalidadContrato(data) {
    let cbocombollenar = document.getElementById('cboContrato');
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
        html += '<th>TELEFONOCELULAR</th>';
        html += '<th>TELEFONOFIJO</th>';
        html += '<th>EMAIL</th>';
        html += '<th>IIDSEXO</th>';
        html += '<th>FECHACONTRATO</th>';
        html += '<th>FOTO</th>';
        html += '<th>IIDMODALIDADCONTRATO</th>';
        html += '<th>BHABILITADO</th>';
        html += '<th>IIDTIPOUSUARIO</th>';
        html += '<th>bTieneUsuario</th>';
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
            html += '<td>' + curso.TELEFONOCELULAR + '</td>';
            html += '<td>' + curso.TELEFONOFIJO + '</td>';
            html += '<td>' + curso.EMAIL + '</td>';
            html += '<td>' + curso.IIDSEXO + '</td>';
            html += '<td>' + moment(curso.FECHACONTRATO).format("DD/MM/yyyy") + '</td>';
            //html += '<td><img alt="foto" src="' + curso.FOTO + '" width="20" height="20" /></td>';
            html += '<td><img alt="foto" src="@Url.Action("getImage","Docente")" width="20" height="20" /></td>';
            html += '<td>' + curso.IIDMODALIDADCONTRATO + '</td>';
            html += '<td>' + curso.BHABILITADO + '</td>';
            html += '<td>' + curso.IIDTIPOUSUARIO + '</td>';
            html += '<td>' + curso.bTieneUsuario + '</td>';
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


var btnBuscarModalidadContrato = document.getElementById('btnBuscarModalidadContrato');
btnBuscarModalidadContrato.onclick = function () {
    var cboContrato = document.getElementById('cboContrato').value;
    $.get("Docente/buscarDocenteModalidadContrato?ModalidadContrato=" + cboContrato, function (data) {
        llenarTabla(data);
    });
}

var idbutton = document.getElementById('idbutton');
idbutton.onclick = function () {
    var txtnombre = document.getElementById('txtnombre').value;
    $.get("Alumno/buscarAlumnos?nombre=" + txtnombre, function (data) {
        llenarTabla(data);
    });
}

var idlimpiar = document.getElementById('idlimpiar');
idlimpiar.onclick = function () {
    listar();
}

var idlimpiarCombo = document.getElementById('idlimpiarCombo');
idlimpiarCombo.onclick = function () {
    listar();
}

listar();

