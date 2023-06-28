function borrarDatos(className = 'borrar') {
    let controles = document.getElementsByClassName(className);
    for (let control of controles) {
        control.value = '';
    }
}

function datosObligatorios(className = 'obligatorio') {
    let controles = document.getElementsByClassName(className);
    let contador = 0;
    for (let control of controles) {
        if (control.value == '') {
            contador--;
            control.parentNode.classList.add("error");
        } else {
            contador++;
            control.parentNode.classList.remove("error");
            //var modal = $('#exampleModal');
            //let modal = new bootstrap.Modal('#exampleModal')
            //modal.hide();
        }
    }
    return (contador == controles.length) ? true : false;
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

function llenarComboboxElement(data, element) {
    let cbocombollenar = document.getElementById(element);
    if (data != null && data.length > 0) {
        llenarCombo(data, cbocombollenar);
    } else {
        cbocombollenar.innerHTML = ''
    }
}

function llenarTablaData(columnas, data) {
    let idtabla = document.getElementById('idtabla');
    let html = '';
    if (data != null && data.length > 0) {
        html += ' <table id="tabla-curso" class="table table-dark">';
        html += '    <thead>';
        html += '        <tr>';
        for (let columna of columnas) {
            html += '<th>' + columna + '</th>';
        }
        html += '<th>OPERACIONES</th>';
        html += '        </tr>';
        html += '    </thead>';
        html += '    <tbody>';
        let llaves = Object.keys(data[0]);
        for (var i = 0; i < data.length; i++) {
            html += '<tr>';
            for (var j = 0; j < llaves.length; j++) {
                const valorLlaves = llaves[j];
                html += '<td>' + data[i][valorLlaves] + '</td>';
            }
            html += '<td><div class="btn-group" role="group" aria-label="Basic example">';
            html += '<button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#exampleModal" onclick="abrirModal(' + data[i].IID + ')">';
            html += '<i class="fa fa-pencil" aria-hidden="true"></i>';
            html += '</button>';
            html += '<button type="button" class="btn btn-danger" onclick="eliminar(' + data[i].IID + ')">';
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


function utf8_to_b64(str) {
    return window.btoa(unescape(encodeURIComponent(str)));
}

function b64_to_utf8(str) {
    return decodeURIComponent(escape(window.atob(str)));
}