function AjaxGravacaoCliente() {
    $('#form-comentario').submit(function () {

        var form = $(this);
        var form_data = new FormData(this);

        $.ajax({
            url: '/Filme/GravarComentario',
            type: 'POST',
            contentType: false,
            processData: false,
            data: form_data,
            dataType: 'json',
            success: function (data) {
                if (data.success) {
                   
                } else {
                    MsgError(data.error);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(XMLHttpRequest);
                console.log(XMLHttpRequest.statusText);
                console.log(textStatus);
                console.log(errorThrown);

                MsgError("Erro");
            }
        });

        return false;
    });
};

$(document).ready(function () {
    AjaxGravacaoCliente();
});