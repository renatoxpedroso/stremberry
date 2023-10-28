"use strict";

KTUtil.onDOMContentLoaded((function () {
    resetPasswordEvent();
}));

function resetPasswordEvent() {
    var t = document.querySelector("#kt_password_reset_form");
    var e = document.querySelector("#kt_password_reset_submit");

    var i = FormValidation.formValidation(t, {
        fields: {
            email: {
                validators: {
                    regexp: {
                        regexp: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                        message: "Este não é um email válido"
                    },
                    notEmpty: {
                        message: "Email é obrigatório"
                    }
                }
            }
        },
        plugins: {
            trigger: new FormValidation.plugins.Trigger,
            bootstrap: new FormValidation.plugins.Bootstrap5({
                rowSelector: ".fv-row", eleInvalidClass: "", eleValidClass: ""
            })
        }
    });

    e.addEventListener("click", (function (r) {
        r.preventDefault();
        i.validate().then((function (k) {
            if ("Valid" == k) {
                e.removeAttribute("data-kt-indicator");
                e.disabled = !1;

                resetPasswordAjax();
            }
            else {
                Swal.fire({
                    text: "Parece que foram detectados alguns erros, por favor, tente novamente.",
                    icon: "error",
                    buttonsStyling: !1,
                    confirmButtonText: "Ok!",
                    customClass: { confirmButton: "btn btn-primary" }
                });
            }
        }));
    }));
}

function resetPasswordAjax() {
    $.ajax({
        url: '/Login/ForgotPassword',
        type: 'POST',
        data: $("#kt_password_reset_form").serialize(),
        dataType: 'json',
        success: function (data) {
            var e = document.querySelector("#kt_password_reset_submit");
            e.setAttribute("data-kt-indicator", "on");
            e.disabled = !0;

            if (data.success) {
                Swal.fire({
                    text: "Um email com o link para alteração da senha foi enviado para o endereço informado",
                    icon: "success",
                    buttonsStyling: !1,
                    confirmButtonText: "Ok!",
                    customClass: {
                        confirmButton: "btn btn-primary"
                    }
                }).then((function (t) {
                    window.location.href = "/Login/Index";
                }))
            }
            else {
                var msg = data.msg;

                if (msg == null || msg == "") {
                    msg = "Parece que foram detectados alguns erros, por favor, tente novamente.";
                }

                Swal.fire({
                    text: msg,
                    icon: "error",
                    buttonsStyling: !1,
                    confirmButtonText: "Ok!",
                    customClass: {
                        confirmButton: "btn btn-primary"
                    }
                });
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest);
            console.log(XMLHttpRequest.statusText);
            console.log(textStatus);
            console.log(errorThrown);

            Swal.fire({
                text: "Parece que foram detectados alguns erros, por favor, tente novamente.",
                icon: "error",
                buttonsStyling: !1,
                confirmButtonText: "Ok!",
                customClass: {
                    confirmButton: "btn btn-primary"
                }
            });

            var e = document.querySelector("#kt_password_reset_submit");
            e.setAttribute("data-kt-indicator", "on");
        }
    });
}