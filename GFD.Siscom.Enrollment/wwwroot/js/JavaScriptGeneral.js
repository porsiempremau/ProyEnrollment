function Block() {
    $.blockUI({ message: null });
    document.getElementById("loader").style.display = "block";
    document.getElementsByClassName("blockOverlay")[0].style.zIndex = 10000;
}

function UnBlock() {
    document.getElementById("loader").style.display = "none";
    $.unblockUI();
}

function RenderSelect(idSelect, nameCatalog, selected = "", callback = null, params = {}) {
    var select = document.getElementById(idSelect),
        url = "/CatalogTypes/Get?type=" + nameCatalog + "&isToSelect=true";

    select.innerHTML = "";

    if (callback != null) {
        select.onchange = function () { callback(this, params) };
    }

    axios.get(url).then(response => {
        var data = response.data;
        if (data.length > 0) {
            data.forEach(x => {
                var option = document.createElement("option");
                option.text = x.name;
                option.value = x.id;
                if (x.id == selected) {
                    option.selected = true
                }
                select.appendChild(option);
            });
        } else {
            var option = document.createElement("option");
            option.text = "Sin Datos";
            option.value = 0;
            select.appendChild(option);
        }

        if (callback != null) {
            callback(select, params);
        }
    }).catch(error => {
        var option = document.createElement("option");
        option.text = "No se pudo obtener";
        option.value = 0;
        select.appendChild(option);
    });
}

function RenderSelectOption(idSelect, list, selected = "", callback = null, params = {}) {
    var select = document.getElementById(idSelect);
    select.innerHTML = "";

    if (callback != null) {
        select.onchange = function () { callback(this, params) };
    }

    if (list.length > 0) {
        list.forEach(x => {
            var option = document.createElement("option");
            if (idSelect == "typeContact") {
                option.text = x.description;
                option.value = x.idType;
            } else {
                option.text = x.name;
                option.value = x.id;
            }
            
            if (x.id == selected) {
                option.selected = true
            }
            select.appendChild(option);
        });
    } else {
        var option = document.createElement("option");
        option.text = "Sin Datos";
        option.value = 0;
        select.appendChild(option);
    }

    if (callback != null) {
        callback(select, params);
    }
}

function GetValue(id, isInt) {
    var value = document.getElementById(id);
    if (!isInt) {
        if (value != null || value != undefined) {
            return value.value;
        } else {
            return "";
        }
    } else {
        if (value != null || value != undefined) {
            return parseInt(value.value);
        } else {
            return 0;
        }
    }
}

function validarCurp(input, result) {
    var curp = input.value.toUpperCase(),
        resultado = document.getElementById(result),
        valido = "No válido";

    if (curpValida(curp)) {
        valido = "Válido";
        resultado.classList.add("ok");
    } else {
        resultado.classList.remove("ok");
    }

    //resultado.innerText = "CURP: " + curp + "\nFormato: " + valido;
    resultado.innerText = "Formato: " + valido;
}

function curpValida(curp) {
    if (curp == "XEXX010101HNEXXXA4" || curp == "XEXX010101MNEXXXA8") { //CURP genérica*********
        return true;
    }
    var re = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0\d|1[0-2])(?:[0-2]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/,
        validado = curp.match(re);

    if (!validado)  //Coincide con el formato general?
        return false;

    //Validar que coincida el dígito verificador
    function digitoVerificador(curp17) {
        //Fuente https://consultas.curp.gob.mx/CurpSP/
        var diccionario = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ",
            lngSuma = 0.0,
            lngDigito = 0.0;
        for (var i = 0; i < 17; i++)
            lngSuma = lngSuma + diccionario.indexOf(curp17.charAt(i)) * (18 - i);
        lngDigito = 10 - lngSuma % 10;
        if (lngDigito == 10)
            return 0;
        return lngDigito;
    }
    if (validado[2] != digitoVerificador(validado[1]))
        return false;

    return true; //Validado
}

function validarCampo(input, preResult, tipo) {
    var patternCertificado
    
    if (tipo == 'TarjetaDebito') {
        patternCertificado = /[0-9]{16}/;
    }
    if (tipo == 'CuentaBancaria') {
        patternCertificado = /[0-9]{11}/;
    }
    if (tipo == 'ClabeBancaria') {
        patternCertificado = /[0-9]{18}/;
    }
    if (tipo == 'RFC') {
        patternCertificado = /[A-Z]{4}[0-9]{6}[A-Z0-9]{3}/;
    }
    if (tipo == 'INE') {
        patternCertificado = /[0-9]{13}/;
    }

    var inputValue = input.value;
    var result = document.getElementById(preResult);
    var valida = inputValue.match(patternCertificado);
    var isValid = "No válido";
    if (valida) {
        isValid = "Válido";
        result.classList.add("ok");
    } else {
        result.classList.remove("ok");
    }
    result.innerText = "Formato: " + isValid;
}

function getDescriptionTypeContact(id, lista) {
    var descripcion = "";
    if (id == 0 || id == "") {
        descripcion = "No registrado";
    } else {
        var result = lista.find(x => x.idType == id);
        if (typeof result != "undefined") {
            descripcion = result.description;
        }
    }
    return descripcion;
}

function onlyNumbers(e) {
    var key = window.event ? e.which : e.keyCode;
    if (key < 48 || key > 57) {
        e.preventDefault();
    }
}