
function loadDataTable(idTable, type = "") {
    //var Y = "30vh";
    //var columns = [];
    //if (modal == "BusquedaMatricula" || modal == "BusquedaPadreTutor") {
    //    Y = "25vh";
    //}
    //if (modal == "modalJustificaciones") {
    //    columns = [
    //        { "width": "5%" },
    //        { "width": "10%" },
    //        { "width": "10%" },
    //        { "width": "20%" },
    //        { "width": "20%" },
    //        { "width": "20%" },
    //        { "width": "10%" },
    //        { "width": "20%" },
    //        { "width": "5%" },
    //    ]
    //} else {
    //    columns = null;
    //}
    var paging = true,
        scrollY = "",
        scrollColapse = false;

    if (type == "TaxUsers") {
        paging = false;
        scrollY = "47vh";
        scrollColapse = true
    }
    table = $("#" + idTable).DataTable({
        retrieve: true,
        "language": {
            "lengthMenu": "Mostrar _MENU_ datos",
            "zeroRecords": "Nada encontrado - lo siento",
            "info": "Mostrando _END_ de _TOTAL_ datos",
            "infoEmpty": "No hay registros disponibles",
            "sSearch": "Filtrar:",
            "infoFiltered": "(filtrado de _MAX_ registros totales)",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },

            "sLoadingRecords": "Cargando...",
            "sProcessing": "Procesando...",
        },
        "pagingType": "full_numbers",
        "filter": true,
        scrollY: scrollY,
        scrollX: true,
        paging: paging,
        scrollCollapse: scrollColapse,
        "iDisplayLength": 10,
        "lengthChange": false,
        //"columns": columns,
        "autoWidth": true
    });
}

function ClearDataTable(idTable) {
    if ($.fn.DataTable.isDataTable('#' + idTable)) {
        var newtable = $("#" + idTable).DataTable();
        newtable.clear();
        newtable.draw();
        newtable.destroy();
        newtable = null;
    }
}
