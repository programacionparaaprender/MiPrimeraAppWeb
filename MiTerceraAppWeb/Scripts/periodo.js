﻿function llenarTabla(data) {
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
    $.get("Periodo/listarPeriodo", function (data) {
        llenarTabla(data);
    });
}
$.get("Periodo/listarPeriodo", function (data) {
    llenarTabla(data);
});