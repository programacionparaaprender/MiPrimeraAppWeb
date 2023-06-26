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