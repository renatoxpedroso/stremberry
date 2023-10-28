"use strict";

KTUtil.onDOMContentLoaded((function () {
    loginEvent()
}));

function loginEvent() {

    var i = FormValidation.formValidation(document.querySelector("#kt_sign_in_form"), {
        fields: {
            email: {
                validators: {
                    regexp: {
                        regexp: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                        message: "Este não é um email válido"
                    },
                    notEmpty: {
                        message: "O Email é obrigatório"
                    }
                }
            }, password: {
                validators: {
                    notEmpty: {
                        message: "A Senha é obrigatória"
                    }
                }
            }
        }, plugins: {
            trigger: new FormValidation.plugins.Trigger,
            bootstrap: new FormValidation.plugins.Bootstrap5({
                rowSelector: ".fv-row",
                eleInvalidClass: "",
                eleValidClass: ""
            })
        }
    });

    document.querySelector("#kt_sign_in_submit").addEventListener("click", (function (n) {
        n.preventDefault();

        var t = document.querySelector("#kt_sign_in_submit");

        i.validate().then((function (i) {
            if ("Valid" == i) {
                t.setAttribute("data-kt-indicator", "on");
                t.disabled = !0;

                loginAjax();
            }
            else {
                Swal.fire({
                    text: "Parece que foram detectados alguns erros, por favor, tente novamente.",
                    icon: "error",
                    buttonsStyling: !1,
                    confirmButtonText: "Ok!",
                    customClass: {
                        confirmButton: "btn btn-primary"
                    }
                })
            }
        }))
    }))
}

function loginAjax() {
    $.ajax({
        url: '/Login/Login',
        type: 'POST',
        data: $("#kt_sign_in_form").serialize(),
        dataType: 'json',
        success: function (data) {

            var t = document.querySelector("#kt_sign_in_submit");
            t.removeAttribute("data-kt-indicator");
            t.disabled = !1;

            if (data.success) {
                window.location.href = "/Filme";
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

            var t = document.querySelector("#kt_sign_in_submit");
            t.removeAttribute("data-kt-indicator");
            t.disabled = !1;
        }
    });
}
