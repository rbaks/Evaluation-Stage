$(document).ready(function () {
    $("#customersTable").DataTable({
        "dom": 'Bfrtip',
        "buttons": [
            { "extend": 'csv', "text": 'Export CSV', "className": 'btn btn-sm btn-secondary mx-1' },
            { "extend": 'pdf', "text": 'Export Pdf', "className": 'btn btn-sm btn-secondary mx-1' },
            { "extend": 'excel', "text": 'Export Excel', "className": 'btn btn-sm btn-secondary mx-1' },
            { "extend": 'print', "text": 'Print', "className": 'btn btn-sm btn-secondary mx-1' },
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
            /*{
                "targets": [0],
                "visible": false,
                "searchable": false
            }*/],
        "columns": [
/*            { "data": "routeid", "name": "RouteId", "autoWidth": true },*/
            { "data": "name", "name": "Name", "autoWidth": true },
            { "data": "depart", "name": "Depart", "autoWidth": true },
            { "data": "arrive", "name": "Arrive", "autoWidth": true },
            { "data": "etatglobal", "name": "Etatglobal", "autoWidth": true }
        ]
    });
});  