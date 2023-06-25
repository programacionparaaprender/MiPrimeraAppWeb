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
        html += '        </tr>';
        html += '    </thead>';
        html += '    <tbody>';
        for (let curso of data) {
            html += '<tr>';
            html += '<td>' + curso.IIDCURSO + '</td>';
            html += '<td>' + curso.NOMBRE + '</td>';
            html += '<td>' + curso.DESCRIPCION + '</td>';
            html += '<td>' + curso.BHABILITADO + '</td>';
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

$.get("Curso/listarCursos", function (data) {
    llenarTabla(data);
});