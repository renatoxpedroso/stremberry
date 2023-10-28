"use strict";

KTUtil.onDOMContentLoaded((function () {
    signUpEvent();
}));

function signUpEvent() {

    var e = document.querySelector("#kt_sign_up_form");
    var t = document.querySelector("#kt_sign_up_submit");

    var r = KTPasswordMeter.getInstance(e.querySelector('[data-kt-password-meter="true"]'));

    var s = function () {
        return 100 === r.getScore()
    };

    e.querySelector('input[name="password"]').addEventListener("input", (function () {
        this.value.length > 0 && a.updateFieldStatus("password", "NotValidated")
    }));

    var a = FormValidation.formValidation(document.querySelector("#kt_sign_up_form"), {
        fields: {
            "firstname": {
                validators: {
                    notEmpty: {
                        message: "Nome é obrigatório"
                    }
                }
            },
            "last-name": {
                validators: {
                    notEmpty: {
                        message: "Sobrenome é obrigatório"
                    }
                }
            },
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
            },
            password: {
                validators: {
                    notEmpty: {
                        message: "A Senha é obrigatória"
                    },
                    callback: {
                        message: "Informe uma senha válida",
                        callback: function (valid) {
                            if (valid.value.length > 0) return s()
                        }
                    }
                }
            },
            "confirm-password": {
                validators: {
                    notEmpty: {
                        message: "A confirmação da senha é obrigatória"
                    },
                    identical: {
                        compare: function () {
                            return e.querySelector('[name="password"]').value
                        },
                        message: "A senha e sua confirmação não são iguais"
                    }
                }
            },
            toc: {
                validators: {
                    notEmpty: {
                        message: "Você precisa aceitar os termos"
                    }
                }
            }
        },
        plugins: {
            trigger: new FormValidation.plugins.Trigger({
                event: { password: !1 }
            }),
            bootstrap: new FormValidation.plugins.Bootstrap5({
                rowSelector: ".fv-row", eleInvalidClass: "", eleValidClass: ""
            })
        }
    })

    t.addEventListener("click", (function (s) {
        s.preventDefault();
        a.revalidateField("password");
        a.validate().then((function (k) {
            if ("Valid" == k) {
                t.setAttribute("data-kt-indicator", "on");
                t.disabled = !0;

                signUpAjax();
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

function signUpAjax() {
    $.ajax({
        url: '/Login/Registration',
        type: 'POST',
        data: $("#kt_sign_up_form").serialize(),
        dataType: 'json',
        success: function (data) {

            var t = document.querySelector("#kt_sign_up_submit");
            t.removeAttribute("data-kt-indicator");
            t.disabled = !1;

            if (data.success) {
                window.location.href = "/Login/Index";
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

            var t = document.querySelector("#kt_sign_up_submit");
            t.removeAttribute("data-kt-indicator");
            t.disabled = !1;
        }
    });
}