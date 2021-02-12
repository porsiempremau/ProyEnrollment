function DownloadPDF(fileName, url = "") {
    fetch(location.protocol + "//" + location.host + "/Debt/DownloadPDF/" + fileName)
        .then((response) => {
            if (response.status != 200) {
                let errorMessage = "Error al procesar la solicitud... (" + response.status + " " + response.statusText + ")";
                throw new Error(errorMessage);
            } else {
                return response.blob();
            }
        })
        .then((blob) => {
            // !!! see next code block !!!
            proccessBLOB(fileName, blob, url);
            //window.location.reload();
            UnBlock();
        })
        .catch(error => {
            UnBlock();
            console.error(error);
        });
}

function proccessBLOB(filenameForDownload, data, url = "") {
    var textUrl = URL.createObjectURL(data);
    var element = document.createElement('a');
    element.setAttribute('href', textUrl);
    element.setAttribute('download', filenameForDownload);
    element.style.display = 'none';
    document.body.appendChild(element);
    element.click();
    document.body.removeChild(element);
    if (url != "") {
        window.location.href = url;
    }
}