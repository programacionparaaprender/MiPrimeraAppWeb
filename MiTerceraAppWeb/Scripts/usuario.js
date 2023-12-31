﻿
var base_url = window.location.origin;
const url = base_url + "/Usuario";

function listar() {
    $.get(url + "/listarUsuarios", function (data) {
        try {
            let columns = ["Id Usuario", "Nombre usuario", "Nombre completo persona", "Nombre rol", "Tipo"];
            llenarTablaData(columns, data);
        } catch (e) {
            console.log(e);
        }
    });
    $.get(url + "/listarRol", function (data) {
        try {
            llenarComboboxElement(data, 'cboRol');
        } catch (e) {
            console.log(e);
        }
    });

    $.get(url + "/listarPersonas", function (data) {
        try {
            llenarComboboxElement(data, 'cboPersona');
        } catch (e) {
            console.log(e);
        }
    });

}



function abrirModal(IID = 0) {
    if (IID == 0) {
        borrarDatos();
        obtenerPorId('lblContra').style.display = "block";
        obtenerPorId('txtcontrasenia').style.display = "block";

        obtenerPorId('lblPersona').style.display = "block";
        obtenerPorId('cboPersona').style.display = "block";

    } else {
        obtenerPorId('txtcontrasenia').value = "1";
        obtenerPorId('cboPersona').value = "2";
        obtenerPorId('lblContra').style.display = "none";
        obtenerPorId('txtcontrasenia').style.display = "none";
        $.get(url + "/recuperarInformacion?id=" + IID, function (data) {
            obtenerPorId('cboRol').value = data[0].IIDROL;
            obtenerPorId('txtnombreusuario').value = data[0].NOMBREUSUARIO;
            obtenerPorId('txtidusuario').value = data[0].IIDUSUARIO;
        });
    }
}

function eliminar(id) {
    if (confirm('¿Desea realmente eliminar?') == 1) {
        $.get(url + "/eliminarInformacion?id=" + id, function (data) {
            if (data == 1) {
                alert('Se elimino correctamente');
                listar();
            } else {
                alert('Ocurrio un error');
            }
        });
    }
}

function agregar() {
    try {
        if (datosObligatorios()) {
            if (confirm('¿Desea realmente guardar?') == 1) {
                let frm = new FormData();

                let txtidusuario = obtenerPorId('txtidusuario').value;
                let txtnombreusuario = obtenerPorId('txtnombreusuario').value;
                let txtcontrasenia = obtenerPorId('txtcontrasenia').value;
                let cboRol = obtenerPorId('cboRol').value;
                let cboPersona = obtenerPorId('cboPersona').value;
                let nombrePersona = obtenerPorId('cboPersona').options[obtenerPorId('cboPersona').selectedIndex].text;
                frm.append('IIDUSUARIO', txtidusuario);
                frm.append('NOMBREUSUARIO', txtnombreusuario);
                frm.append('CONTRA', txtcontrasenia);
                frm.append('IIDROL', cboRol);
                frm.append('IID', cboPersona);
                frm.append('BHABILITADO', 1);
                frm.append('nombrePersona', nombrePersona);
                $.ajax({
                    type: "POST",
                    url: url + "/guardarDatos",
                    data: frm,
                    //dataType: "json",
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data == -1) {
                            alert('Ya existe.');
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
        console.log(e);
        alert('ocurrio un error al registrar.');
    }
}



listar();