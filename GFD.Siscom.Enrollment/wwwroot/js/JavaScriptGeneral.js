function Block() {
    $.blockUI({ message: null });
    document.getElementById("loader").style.display = "block";
    document.getElementsByClassName("blockOverlay")[0].style.zIndex = 10000;
}

function UnBlock() {
    document.getElementById("loader").style.display = "none";
    $.unblockUI();
}

function RenderSelect(idSelect, nameCatalog, selected = "", callback = null, params = {}) { //SE ACTUALIZÓ Y ES LA FUNCIÓN DE ABAJO LA QUE SE OCUPÓ PARA CREATE Y EDIT DE AGREEMENTS
    var select = document.getElementById(idSelect),
        //url = "/CatalogTypes/Get?type=" + nameCatalog + "&isToSelect=true";
        url = "/" + nameCatalog + "/Get/0?isToSelect=true";

    select.innerHTML = "";

    if (callback != null) {
        select.onchange = function () { callback(this, params) };
    }

    axios.get(url).then(response => {
        var data = response.data;
        if (data.length > 0) {
            data.forEach(x => {
                if (x.isActive) {
                    var option = document.createElement("option");
                    option.text = x.name;
                    option.value = x.id;
                    if (x.id == selected) {
                        option.selected = true
                    }
                    select.appendChild(option);
                }
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

function RenderSelectOption(acronym, idSelect, list, isAyuntamiento, callback = null, params = {}) {
    var select = document.getElementById(idSelect);
    select.innerHTML = "";

    if (callback != null) {
        select.onchange = function () { callback(this, params) };
    }

    if (list.length > 0) {
        list.forEach(x => {
            if (idSelect == "typeConsume") {
                var option = document.createElement("option");
                if (isAyuntamiento) {
                    option.text = x.acronym;
                    option.value = x.id;
                } else {
                    option.text = x.name;
                    option.value = x.id;
                }
                select.appendChild(option);
            } else if (idSelect == "typeIntake") {
                var option = document.createElement("option");
                option.text = x.acronym;
                option.value = x.id;
                select.appendChild(option);
            } else if (idSelect == "typeService" || idSelect == "services"
                || idSelect == "typePeriod" || idSelect == "diameter") {
                var option = document.createElement("option");
                option.text = x.name;
                option.value = x.id;
                select.appendChild(option);
            } else if (idSelect == "typeContact" || idSelect == "typeClient" || idSelect == "typeAddress") {
                var option = document.createElement("option");
                option.text = x.description;
                option.value = x.idType;
                select.appendChild(option);
            } else if (x.intakeAcronym == acronym || x.acronym == acronym) {
                var option = document.createElement("option");
                option.text = x.name;
                option.value = x.id;
                select.appendChild(option);
            }
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

function GetCountries(idSelect) {
    Block();
    var select = document.getElementById(idSelect);
    axios.get("/States/Get/0?isToSelect=true").then(response => {
        var data = response.data;
        if (data.length > 0) {
            data.forEach(x => {
                var option = document.createElement("option");
                option.text = x.name;
                option.value = x.id;
                if (x.id == 21) {
                    option.selected = true;
                }
                select.appendChild(option);
            });
        }
        UnBlock();
    }).catch(error => {
        UnBlock();
    })
}

function SearchTownByIdState(id, idSelect) {
    Block();
    __idState = id;
    $("#" + idSelect).html("");
    var select = document.getElementById(idSelect);
    axios.get("/Towns/SearchTownByIdState/" + __idState + "/0").then(response => {
        var data = response.data;
        if (data.length > 0) {
            data.forEach(x => {
                var option = document.createElement("option");
                option.text = x.name;
                option.value = x.id;
                if (x.id == 2) {
                    option.selected = true;
                }
                select.appendChild(option);
            });
        }
        UnBlock();
    }).catch(error => {
        UnBlock();
    })
}

const SearchSuburbByIdTown = (id, idSelect) => {
    return new Promise((resolve, reject) => {
        Block();
        __idTown = id;
        $("#" + idSelect).html("");
        var select = document.getElementById(idSelect);
        axios.get("/Suburbs/SearchSuburbByIdTown/" + __idTown + "/0").then(response => {
            var data = response.data;
            if (data.length > 0) {
                data.forEach(x => {
                    var option = document.createElement("option");
                    option.text = x.name;
                    option.value = x.id;
                    select.appendChild(option);
                });
            }
            UnBlock();
            resolve(true);
        }).catch(error => {
            UnBlock();
            reject(false);
        });
    });
    
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

    resultado.innerText = "Formato: " + valido;
    //resultado.innerText = "CURP: " + curp + "\nFormato: " + valido;
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

function getDescriptionType(id, lista) {
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
    if (key < 46 || key > 57) {
        e.preventDefault();
    }
}

function getFormateDate(d) {
    var date = new Date(d);
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var year = date.getFullYear();
    if (day < 10) {
        day = `0${day}`;
    }
    if (month < 10) {
        month = `0${month}`;
    }
    var result = year + "-" + month + "-" + day;
    return result;
}

const GetSystemsParameters = () => {
    return new Promise((resolve, reject) => {
        axios.get('/SystemsParameters/GetAll').then(response => {
            var sp = [] = response.data;
            resolve(sp);
        }).catch(error => { });
    });
}

function GetOneSystemParameter(id, list) {
    var value = 0;
    if (list.length > 0) {
        var x = list.find(x => x.id == id);
        if (x != undefined) {
            value = x.numberColumn;
            return value;
        }
    }
    return value
}

const YEAR = [
    {
        "index": 0,
        "number": 1,
        "year": 2014,
        "months": [
            {
                "name": "Enero",
                "number": 1,
                "index": 0,
                "value": 1
            },
            {
                "name": "Febrero",
                "number": 2,
                "index": 1,
                "value": 1
            },
            {
                "name": "Marzo",
                "number": 3,
                "index": 2,
                "value": 1
            },
            {
                "name": "Abril",
                "number": 4,
                "index": 3,
                "value": 1
            },
            {
                "name": "Mayo",
                "number": 5,
                "index": 4,
                "value": 1
            },
            {
                "name": "Junio",
                "number": 6,
                "index": 5,
                "value": 1
            },
            {
                "name": "Julio",
                "number": 7,
                "index": 6,
                "value": 1
            },
            {
                "name": "Agosto",
                "number": 8,
                "index": 7,
                "value": 1
            },
            {
                "name": "Septiembre",
                "number": 9,
                "index": 8,
                "value": 1
            },
            {
                "name": "Octubre",
                "number": 10,
                "index": 9,
                "value": 1
            },
            {
                "name": "Noviembre",
                "number": 11,
                "index": 10,
                "value": 1
            },
            {
                "name": "Diciembre",
                "number": 12,
                "index": 11,
                "value": 1
            }
        ]
    },
    {
        "index": 1,
        "number": 2,
        "year": 2015,
        "months": [
            {
                "name": "Enero",
                "number": 1,
                "index": 0,
                "value": 1
            },
            {
                "name": "Febrero",
                "number": 2,
                "index": 1,
                "value": 1
            },
            {
                "name": "Marzo",
                "number": 3,
                "index": 2,
                "value": 1
            },
            {
                "name": "Abril",
                "number": 4,
                "index": 3,
                "value": 1
            },
            {
                "name": "Mayo",
                "number": 5,
                "index": 4,
                "value": 1
            },
            {
                "name": "Junio",
                "number": 6,
                "index": 5,
                "value": 1
            },
            {
                "name": "Julio",
                "number": 7,
                "index": 6,
                "value": 1
            },
            {
                "name": "Agosto",
                "number": 8,
                "index": 7,
                "value": 1
            },
            {
                "name": "Septiembre",
                "number": 9,
                "index": 8,
                "value": 1
            },
            {
                "name": "Octubre",
                "number": 10,
                "index": 9,
                "value": 1
            },
            {
                "name": "Noviembre",
                "number": 11,
                "index": 10,
                "value": 1
            },
            {
                "name": "Diciembre",
                "number": 12,
                "index": 11,
                "value": 1
            }
        ]
    },
    {
        "index": 2,
        "number": 3,
        "year": 2016,
        "months": [
            {
                "name": "Enero",
                "number": 1,
                "index": 0,
                "value": 1
            },
            {
                "name": "Febrero",
                "number": 2,
                "index": 1,
                "value": 1
            },
            {
                "name": "Marzo",
                "number": 3,
                "index": 2,
                "value": 1
            },
            {
                "name": "Abril",
                "number": 4,
                "index": 3,
                "value": 1
            },
            {
                "name": "Mayo",
                "number": 5,
                "index": 4,
                "value": 1
            },
            {
                "name": "Junio",
                "number": 6,
                "index": 5,
                "value": 1
            },
            {
                "name": "Julio",
                "number": 7,
                "index": 6,
                "value": 1
            },
            {
                "name": "Agosto",
                "number": 8,
                "index": 7,
                "value": 1
            },
            {
                "name": "Septiembre",
                "number": 9,
                "index": 8,
                "value": 1
            },
            {
                "name": "Octubre",
                "number": 10,
                "index": 9,
                "value": 1
            },
            {
                "name": "Noviembre",
                "number": 11,
                "index": 10,
                "value": 1
            },
            {
                "name": "Diciembre",
                "number": 12,
                "index": 11,
                "value": 1
            }
        ]
    },
    {
        "index": 3,
        "number": 4,
        "year": 2017,
        "months": [
            {
                "name": "Enero",
                "number": 1,
                "index": 0,
                "value": 1
            },
            {
                "name": "Febrero",
                "number": 2,
                "index": 1,
                "value": 1
            },
            {
                "name": "Marzo",
                "number": 3,
                "index": 2,
                "value": 1
            },
            {
                "name": "Abril",
                "number": 4,
                "index": 3,
                "value": 1
            },
            {
                "name": "Mayo",
                "number": 5,
                "index": 4,
                "value": 1
            },
            {
                "name": "Junio",
                "number": 6,
                "index": 5,
                "value": 1
            },
            {
                "name": "Julio",
                "number": 7,
                "index": 6,
                "value": 1
            },
            {
                "name": "Agosto",
                "number": 8,
                "index": 7,
                "value": 1
            },
            {
                "name": "Septiembre",
                "number": 9,
                "index": 8,
                "value": 1
            },
            {
                "name": "Octubre",
                "number": 10,
                "index": 9,
                "value": 1
            },
            {
                "name": "Noviembre",
                "number": 11,
                "index": 10,
                "value": 1
            },
            {
                "name": "Diciembre",
                "number": 12,
                "index": 11,
                "value": 1
            }
        ]
    },
    {
        "index": 4,
        "number": 5,
        "year": 2018,
        "months": [
            {
                "name": "Enero",
                "number": 1,
                "index": 0,
                "value": 1
            },
            {
                "name": "Febrero",
                "number": 2,
                "index": 1,
                "value": 1
            },
            {
                "name": "Marzo",
                "number": 3,
                "index": 2,
                "value": 1
            },
            {
                "name": "Abril",
                "number": 4,
                "index": 3,
                "value": 1
            },
            {
                "name": "Mayo",
                "number": 5,
                "index": 4,
                "value": 1
            },
            {
                "name": "Junio",
                "number": 6,
                "index": 5,
                "value": 1
            },
            {
                "name": "Julio",
                "number": 7,
                "index": 6,
                "value": 1
            },
            {
                "name": "Agosto",
                "number": 8,
                "index": 7,
                "value": 1
            },
            {
                "name": "Septiembre",
                "number": 9,
                "index": 8,
                "value": 1
            },
            {
                "name": "Octubre",
                "number": 10,
                "index": 9,
                "value": 1
            },
            {
                "name": "Noviembre",
                "number": 11,
                "index": 10,
                "value": 1
            },
            {
                "name": "Diciembre",
                "number": 12,
                "index": 11,
                "value": 1
            }
        ]
    },
    {
        "index": 5,
        "number": 6,
        "year": 2019,
        "months": [
            {
                "name": "Enero",
                "number": 1,
                "index": 0,
                "value": 1
            },
            {
                "name": "Febrero",
                "number": 2,
                "index": 1,
                "value": 1
            },
            {
                "name": "Marzo",
                "number": 3,
                "index": 2,
                "value": 1
            },
            {
                "name": "Abril",
                "number": 4,
                "index": 3,
                "value": 1
            },
            {
                "name": "Mayo",
                "number": 5,
                "index": 4,
                "value": 1
            },
            {
                "name": "Junio",
                "number": 6,
                "index": 5,
                "value": 1
            },
            {
                "name": "Julio",
                "number": 7,
                "index": 6,
                "value": 1
            },
            {
                "name": "Agosto",
                "number": 8,
                "index": 7,
                "value": 1
            },
            {
                "name": "Septiembre",
                "number": 9,
                "index": 8,
                "value": 1
            },
            {
                "name": "Octubre",
                "number": 10,
                "index": 9,
                "value": 1
            },
            {
                "name": "Noviembre",
                "number": 11,
                "index": 10,
                "value": 1
            },
            {
                "name": "Diciembre",
                "number": 12,
                "index": 11,
                "value": 1
            }
        ]
    },
    {
        "index": 6,
        "number": 7,
        "year": 2020,
        "months": [
            {
                "name": "Enero",
                "number": 1,
                "index": 0,
                "value": 1
            },
            {
                "name": "Febrero",
                "number": 2,
                "index": 1,
                "value": 1
            },
            {
                "name": "Marzo",
                "number": 3,
                "index": 2,
                "value": 1
            },
            {
                "name": "Abril",
                "number": 4,
                "index": 3,
                "value": 1
            },
            {
                "name": "Mayo",
                "number": 5,
                "index": 4,
                "value": 1
            },
            {
                "name": "Junio",
                "number": 6,
                "index": 5,
                "value": 1
            },
            {
                "name": "Julio",
                "number": 7,
                "index": 6,
                "value": 1
            },
            {
                "name": "Agosto",
                "number": 8,
                "index": 7,
                "value": 1
            },
            {
                "name": "Septiembre",
                "number": 9,
                "index": 8,
                "value": 1
            },
            {
                "name": "Octubre",
                "number": 10,
                "index": 9,
                "value": 1
            },
            {
                "name": "Noviembre",
                "number": 11,
                "index": 10,
                "value": 1
            },
            {
                "name": "Diciembre",
                "number": 12,
                "index": 11,
                "value": 1
            }
        ]
    },
    {
        "index": 7,
        "number": 8,
        "year": 2021,
        "months": [
            {
                "name": "Enero",
                "number": 1,
                "index": 0,
                "value": 1
            },
            {
                "name": "Febrero",
                "number": 2,
                "index": 1,
                "value": 1
            },
            {
                "name": "Marzo",
                "number": 3,
                "index": 2,
                "value": 1
            },
            {
                "name": "Abril",
                "number": 4,
                "index": 3,
                "value": 1
            },
            {
                "name": "Mayo",
                "number": 5,
                "index": 4,
                "value": 1
            },
            {
                "name": "Junio",
                "number": 6,
                "index": 5,
                "value": 1
            },
            {
                "name": "Julio",
                "number": 7,
                "index": 6,
                "value": 1
            },
            {
                "name": "Agosto",
                "number": 8,
                "index": 7,
                "value": 1
            },
            {
                "name": "Septiembre",
                "number": 9,
                "index": 8,
                "value": 1
            },
            {
                "name": "Octubre",
                "number": 10,
                "index": 9,
                "value": 1
            },
            {
                "name": "Noviembre",
                "number": 11,
                "index": 10,
                "value": 1
            },
            {
                "name": "Diciembre",
                "number": 12,
                "index": 11,
                "value": 1
            }
        ]
    },
    {
        "index": 8,
        "number": 9,
        "year": 2022,
        "months": [
            {
                "name": "Enero",
                "number": 1,
                "index": 0,
                "value": 1
            },
            {
                "name": "Febrero",
                "number": 2,
                "index": 1,
                "value": 1
            },
            {
                "name": "Marzo",
                "number": 3,
                "index": 2,
                "value": 1
            },
            {
                "name": "Abril",
                "number": 4,
                "index": 3,
                "value": 1
            },
            {
                "name": "Mayo",
                "number": 5,
                "index": 4,
                "value": 1
            },
            {
                "name": "Junio",
                "number": 6,
                "index": 5,
                "value": 1
            },
            {
                "name": "Julio",
                "number": 7,
                "index": 6,
                "value": 1
            },
            {
                "name": "Agosto",
                "number": 8,
                "index": 7,
                "value": 1
            },
            {
                "name": "Septiembre",
                "number": 9,
                "index": 8,
                "value": 1
            },
            {
                "name": "Octubre",
                "number": 10,
                "index": 9,
                "value": 1
            },
            {
                "name": "Noviembre",
                "number": 11,
                "index": 10,
                "value": 1
            },
            {
                "name": "Diciembre",
                "number": 12,
                "index": 11,
                "value": 1
            }
        ]
    },
    {
        "index": 9,
        "number": 10,
        "year": 2023,
        "months": [
            {
                "name": "Enero",
                "number": 1,
                "index": 0,
                "value": 1
            },
            {
                "name": "Febrero",
                "number": 2,
                "index": 1,
                "value": 1
            },
            {
                "name": "Marzo",
                "number": 3,
                "index": 2,
                "value": 1
            },
            {
                "name": "Abril",
                "number": 4,
                "index": 3,
                "value": 1
            },
            {
                "name": "Mayo",
                "number": 5,
                "index": 4,
                "value": 1
            },
            {
                "name": "Junio",
                "number": 6,
                "index": 5,
                "value": 1
            },
            {
                "name": "Julio",
                "number": 7,
                "index": 6,
                "value": 1
            },
            {
                "name": "Agosto",
                "number": 8,
                "index": 7,
                "value": 1
            },
            {
                "name": "Septiembre",
                "number": 9,
                "index": 8,
                "value": 1
            },
            {
                "name": "Octubre",
                "number": 10,
                "index": 9,
                "value": 1
            },
            {
                "name": "Noviembre",
                "number": 11,
                "index": 10,
                "value": 1
            },
            {
                "name": "Diciembre",
                "number": 12,
                "index": 11,
                "value": 1
            }
        ]
    },
    {
        "index": 10,
        "number": 11,
        "year": 2024,
        "months": [
            {
                "name": "Enero",
                "number": 1,
                "index": 0,
                "value": 1
            },
            {
                "name": "Febrero",
                "number": 2,
                "index": 1,
                "value": 1
            },
            {
                "name": "Marzo",
                "number": 3,
                "index": 2,
                "value": 1
            },
            {
                "name": "Abril",
                "number": 4,
                "index": 3,
                "value": 1
            },
            {
                "name": "Mayo",
                "number": 5,
                "index": 4,
                "value": 1
            },
            {
                "name": "Junio",
                "number": 6,
                "index": 5,
                "value": 1
            },
            {
                "name": "Julio",
                "number": 7,
                "index": 6,
                "value": 1
            },
            {
                "name": "Agosto",
                "number": 8,
                "index": 7,
                "value": 1
            },
            {
                "name": "Septiembre",
                "number": 9,
                "index": 8,
                "value": 1
            },
            {
                "name": "Octubre",
                "number": 10,
                "index": 9,
                "value": 1
            },
            {
                "name": "Noviembre",
                "number": 11,
                "index": 10,
                "value": 1
            },
            {
                "name": "Diciembre",
                "number": 12,
                "index": 11,
                "value": 1
            }
        ]
    },
    {
        "index": 11,
        "number": 12,
        "year": 2025,
        "months": [
            {
                "name": "Enero",
                "number": 1,
                "index": 0,
                "value": 1
            },
            {
                "name": "Febrero",
                "number": 2,
                "index": 1,
                "value": 1
            },
            {
                "name": "Marzo",
                "number": 3,
                "index": 2,
                "value": 1
            },
            {
                "name": "Abril",
                "number": 4,
                "index": 3,
                "value": 1
            },
            {
                "name": "Mayo",
                "number": 5,
                "index": 4,
                "value": 1
            },
            {
                "name": "Junio",
                "number": 6,
                "index": 5,
                "value": 1
            },
            {
                "name": "Julio",
                "number": 7,
                "index": 6,
                "value": 1
            },
            {
                "name": "Agosto",
                "number": 8,
                "index": 7,
                "value": 1
            },
            {
                "name": "Septiembre",
                "number": 9,
                "index": 8,
                "value": 1
            },
            {
                "name": "Octubre",
                "number": 10,
                "index": 9,
                "value": 1
            },
            {
                "name": "Noviembre",
                "number": 11,
                "index": 10,
                "value": 1
            },
            {
                "name": "Diciembre",
                "number": 12,
                "index": 11,
                "value": 1
            }
        ]
    }
]