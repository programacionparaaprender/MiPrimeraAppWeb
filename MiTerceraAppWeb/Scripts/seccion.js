$('#txtfecnacimiento').datepicker({
    dateFormar: "dd/mm/yyyy",
    changeMonth: true,
    changeYear: true
});
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


function llenarTabla(data) {
    let idtabla = document.getElementById('idtabla');
    let html = '';
    if (data != null && data.length > 0) {
        html += ' <table id="tabla-curso" class="table table-dark">';
        html += '    <thead>';
        html += '        <tr>';
        html += '            <th>IID</th>';
        html += '            <th>NOMBRE</th>';
        //html += '            <th>BHABILITADO</th>';
        html += '            <th>OPERACIONES</th>';
        html += '        </tr>';
        html += '    </thead>';
        html += '    <tbody>';
        for (let curso of data) {
            html += '<tr>';
            html += '<td>' + curso.IID + '</td>';
            html += '<td>' + curso.NOMBRE + '</td>';
            //html += '<td>' + curso.BHABILITADO + '</td>';
            html += '<td><div class="btn-group" role="group" aria-label="Basic example">';
            html += '<button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#exampleModal">';
            html += '<i class="fa fa-pencil" aria-hidden="true"></i>';
            html += '</button>';
            html += '<button type="button" class="btn btn-danger">';
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
    $.get("Alumno/buscarAlumnos?nombre=" + txtnombre, function (data) {
        llenarTabla(data);
    });
}

var idlimpiar = document.getElementById('idlimpiar');
idlimpiar.onclick = function () {
    $.get("Alumno/listarAlumnos", function (data) {
        llenarTabla(data);
    });
}

function listar() {
    $.get("Seccion/listarSecciones", function (data) {
        llenarTabla(data);
        //llenarCombobox(data);
    });

}

listar();