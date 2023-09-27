var base_url = window.location.origin;
const url = base_url + "/Login";
var btnIngresar = obtenerPorId('btnIngresar');
btnIngresar.onclick = function () {
    var usuario = obtenerPorId('txtusuario').value;
    var contra = obtenerPorId('txtcontra').value;
    if (usuario == "") {
        alert("Ingrese un usuario");
        return;
    }



    $.get(url + "/validarUsuario?usuario=" + usuario + "&contra=" + contra, function (data) {
        if (data == 1) {
            //document.location.href = "@Url.Action('Index', 'PaginaPrincipal')";
            document.location.href = "PaginaPrincipal/Index";
        } else {
            alert("Usuario o contraseña incorrectos");
        }
    });
}