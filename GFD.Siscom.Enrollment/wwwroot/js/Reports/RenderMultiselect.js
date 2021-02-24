function renderSelect(IdSelect, endpointName, callback = null, params = {}) {
    axios.get("/Reports/GetDataToSelects?NameEndpoint=" + endpointName).then(response => {
        var data = response.data;

        data.sort((a, b) => {
            const nameA = a.name.toLowerCase();
            const nameB = b.name.toLowerCase();
            if (nameA < nameB)
                return -1
            if (nameA > nameB)
                return 1
            return 0
        });

        const Select = document.getElementById(IdSelect);
        Select.innerHTML = "";

        if (callback != null) {
            Select.onchange = function () { callback(this, params) };
        }
        if (data.length > 0) {
            data.forEach(x => {
                var option = document.createElement("option");
                option.text = x.name;
                option.value = x.id;
                Select.appendChild(option);
            });
        }
        else {
            const option = document.createElement("option");
            option.text = "Sin Datos";
            option.value = 0;
            Select.appendChild(option);
        }

        if (callback != null) {
            callback(Select, params);
        }

        SelectMultiple(IdSelect);
    }).catch(error => {
        UnBlock();
        CatchError(error);
    })
};

function SelectMultiple(idSelect) {
    $('#' + idSelect).multiselect({
        maxHeight: 270,
        includeSelectAllOption: true,
        enableFiltering: true,
        filterPlaceholder: 'Filtrar...'
    });
}