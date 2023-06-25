$.get("RepasoHTML/listarPersonas", function (data) {
    let idtbody = document.getElementById('idtbody');
    let html = '';
    if (data != null && data.length > 0) {
        for (let persona of data) {
            html += '<tr>';
            html += '<td>' + persona.idPersona + '</td>';
            html += '<td>' + persona.nombre + '</td>';
            html += '<td>' + persona.apellidoPaterno + '</td>';
            html += '</tr>';
        }
        idtbody.innerHTML = html;
    } else {
        let idtabla = document.getElementById('idtabla');
        idtabla.innerHTML = 'Tabla vacia'
    }
    //alert(JSON.stringify(data));
});