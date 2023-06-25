var btnSumar = document.getElementById('btnSumar');
btnSumar.onclick = function () {
    var numero1 = document.getElementById('txtnumero1');
    var numero2 = document.getElementById('txtnumero2');
    var suma = parseInt(numero1.value) + parseInt(numero2.value);
    //alert(suma);
    document.getElementById('lblRespuesta').innerHTML = "La suma es: " + String(suma);
}

var btnLimpiar = document.getElementById('btnLimpiar');
btnLimpiar.onclick = function () {
    document.getElementById('lblRespuesta').innerHTML = "";
}

  