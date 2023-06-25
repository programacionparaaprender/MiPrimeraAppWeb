function llenarCombobox(data) {
    let cbocombollenar = document.getElementById('cbocombollenar');
    let html = '';
    if (data != null && data.length > 0) {
        for (let curso of data) {
            html += '<option value="' + curso.IIDCURSO + '">' + curso.NOMBRE + '</option>';
        }
        cbocombollenar.innerHTML = html;
    } else {
        cbocombollenar.innerHTML = ''
    }
}

$.get("RepasoHTML/listarCursos", function (data) {
    llenarCombobox(data);
});

var btnValor = document.getElementById('btnValor');
btnValor.onclick = function () {
    var cbocategoria = document.getElementById('cbocategoria').value;
    alert(cbocategoria);
}