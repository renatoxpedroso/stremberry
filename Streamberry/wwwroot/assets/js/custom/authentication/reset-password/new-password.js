"use strict";

KTUtil.onDOMContentLoaded((function () {
    newPasswordEvent();
}));

function newPasswordEvent() {

    var t = document.querySelector("#kt_new_password_form");
    var e = document.querySelector("#kt_new_password_submit");

    var o = KTPasswordMeter.getInstance(t.querySelector('[data-kt-password-meter="true"]'));

    var a = function () {
        return 100 === o.getScore();
    }

    t.querySelector('input[name="password"]').addEventListener("input", (function () {
        this.value.length > 0 && r.updateFieldStatus("password", "NotValidated")
    }))

    var r = FormValidation.formValidation(document.querySelector("#kt_new_password_form"), {
        fields: {
            password: {
                validators: {
                    notEmpty: {
                        message: "Senha é obrigatória"
                    },
                    callback: {
                        message: "Informe uma senha válida",
                        callback: function (k) {
                            if (k.value.length > 0) return a()
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
                            return t.querySelector('[name="password"]').value
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
    });

    e.addEventListener("click", (function (a) {
        a.preventDefault();
        r.revalidateField("password");
        r.validate().then((function (k) {
            if ("Valid" == k) {
                e.setAttribute("data-kt-indicator", "on");
                e.disabled = !0;

                newPasswordAjax();
            }
            else {
                Swal.fire({
                    text: "Parece que foram detectados alguns erros, por favor, tente novamente.",
                    icon: "error", buttonsStyling: !1,
                    confirmButtonText: "Ok, got it!",
                    customClass: { confirmButton: "btn btn-primary" }
                });
            }
        }))
    }))
}

function newPasswordAjax() {
    $.ajax({
        url: '/Login/ForgotPasswordChange',
        type: 'POST',
        data: $("#kt_new_password_form").serialize(),
        dataType: 'json',
        success: function (data) {

            var e = document.querySelector("#kt_new_password_submit");
            e.removeAttribute("data-kt-indicator");
            e.disabled = !1;

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

            var e = document.querySelector("#kt_new_password_submit");
            e.removeAttribute("data-kt-indicator");
            e.disabled = !1;
        }
    });
}