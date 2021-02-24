function GetFieldsToReport(nameReport) {
    var fields = [];
    switch (nameReport) {
        case "Orders":
            fields = [
                {
                    caption: "División",
                    dataField: "division",
                    area: "row",
                },
                {
                    caption: "Folio",
                    dataField: "folio",
                    dataType: "text",
                    area: "row",
                },
                {
                    caption: "Descripción",
                    dataField: "description",
                    area: "row"
                },
                {
                    caption: "Se creó",
                    dataField: "fechaOS",
                    format: "text",
                    area: "row"
                },
                {
                    caption: "Cliente",
                    dataField: "cliente",
                    dataType: "text",
                    area: "row"
                }, {
                    caption: "Estado",
                    dataField: "estado",
                    dataType: "text",
                    area: "column"
                }, {
                    caption: "Descuento",
                    dataField: "descuento",
                    dataType: "number",
                    summaryType: "sum",
                    format: "$ #,##0.##",
                    area: "data"
                }, {
                    caption: "Subtotal",
                    dataField: "subtotal",
                    dataType: "number",
                    summaryType: "sum",
                    format: "$ #,##0.##",
                    area: "data"
                }, {
                    caption: "I.V.A.",
                    dataField: "iva",
                    dataType: "number",
                    summaryType: "sum",
                    format: "$ #,##0.##",
                    area: "data"
                }, {
                    caption: "Total",
                    dataField: "total",
                    dataType: "number",
                    summaryType: "sum",
                    format: "$ #,##0.##",
                    area: "data"
                }
            ];
            break;
    }

    return fields;
}