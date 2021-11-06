$(document).ready(function () {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "progressBar": true,
        "preventDuplicates": false,
        "positionClass": "toast-top-right",
        "showDuration": "400",
        "hideDuration": "1000",
        "timeOut": "7000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
    showEmptyTableMessageIfEmpty()
})

$("#menu-toggle").click(function (e) {
    e.preventDefault()
    $("#wrapper").toggleClass("menuDisplayed")
});

function showEmptyTableMessageIfEmpty() {
    if ($('#mydiv tr').length <= 2) {
        $(".empty-table").removeClass("hide")
        $(".empty-table-text").removeClass("hide")
    }
    else {
        $(".empty-table").addClass("hide")
        $(".empty-table-text").addClass("hide")
    }
}

function deleteConfirmation(id, url, msg) {
    swal({
        title: "Tem certeza?",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Deletar",
        cancelButtonText: "Cancelar",
    },
        function (willDelete) {
            if (willDelete) {
                $.ajax({
                    url: url + id,
                    success: function () {
                        $("#mydiv").load(location.href + " #mydiv>*", "")
                        toastr.success(msg)
                        showEmptyTableMessageIfEmpty()
                    }
                })
            }
        })
}

function setValidator() {
    $("#form").removeData("validator")
    $("#form").removeData("unobtrusiveValidation")
    $.validator.unobtrusive.parse("#form")
}

function handleSuccess() {
    showEmptyTableMessageIfEmpty()

    var message = $('#Message').text();

    if (message !== '') {
        $('#myModal .modal-body').html('')
        $('#myModal .modal-title').html('')
        $('#myModal').modal('hide')
        $("#mydiv").load(location.href + " #mydiv>*", "")
        $("#myModal").load(location.href + " #myModal>*", "")
        toastr.success(message)
        $('#Message').text('')
    }
}

function handleBtnModalClick(createUrl, editUrl) {
    $('body').on('click', '.btnModal', function () {
        var id = $(this).data("value")
        if (id == null) {
            $('#myModal .modal-title').html("Criar")
            $("#teste").load(createUrl, function () {
                $('#myModal').modal("show")
            })
        }
        else {
            $('#myModal .modal-title').html("Editar")
            $("#teste").load(editUrl + id, function () {
                $('#myModal').modal("show")
            })
        }
        setValidator()
    })
}
