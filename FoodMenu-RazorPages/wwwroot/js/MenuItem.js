$(document).ready(() => {
    $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/MenuItem",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "foodType.name", "width": "15%" },
            {
                "data": "id",
                "render": data =>
                    `<div class="w-75 btn-group d-block d-sm-inline-flex">
                        <a href="/Admin/MenuItems/Upsert?id=${data}" class="btn btn-success mx-2 my-sm-0 my-1">
                            <i class="bi bi-pencil-square"></i>
                        </a>
                        <a href="/Admin/MenuItems/Upsert?id=${data}" class="btn btn-danger mx-2 my-sm-0 my-1">
                            <i class="bi bi-trash-fill"></i>
                        </a>
                    </div>`
            }
        ],
        "width": "100%"
    });
});