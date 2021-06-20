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
            "url": "Customers/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": 5,
                "className" : "text-right"
            }],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "firstName", "name": "FirstName", "autoWidth": true },
            { "data": "lastName", "name": "LastName", "autoWidth": true },
            { "data": "contact", "name": "Country", "autoWidth": true },
            { "data": "email", "name": "Email", "autoWidth": true },
            { "data": "dateOfBirth", "name": "DateOfBirth", "autoWidth": true },
            {
                "render": function (data, type, row) { return `<a href='Customers/Edit/${row.id}'>modif</a> | <a href='Customers/Delete/${row.id}'>suppr</a>`; }
            },
        ]
    });
});  