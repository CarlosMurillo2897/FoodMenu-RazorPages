var dataTable;

$(document).ready(() => {
    dataTable = $('#DT_load').DataTable({
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
                        <a onclick="Delete('/api/MenuItem/${data}')" class="btn btn-danger mx-2 my-sm-0 my-1">
                            <i class="bi bi-trash-fill"></i>
                        </a>
                    </div>`
            }
        ],
        "width": "100%"
    });
});

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    });
}