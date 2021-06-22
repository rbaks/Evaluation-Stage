$(document).ready(function () {
    $("#customersTable").DataTable({
        "dom": 'Bfrtip',
        "buttons": [
            { "extend": 'excel', "text": 'Export Excel', "className": 'mx-1' }
        ],
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "Etat/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            {
                "targets": [3],
                "className" : "text-right"
            }],
        "columns": [
/*            { "data": "routeid", "name": "RouteId", "autoWidth": true },*/
            { "data": "name", "name": "Name", "autoWidth": true },
            { "data": "depart", "name": "Depart", "autoWidth": true },
            { "data": "arrive", "name": "Arrive", "autoWidth": true },
            { "data": "etatglobal", "name": "Etatglobal", "autoWidth": true }
        ]
    });
});  