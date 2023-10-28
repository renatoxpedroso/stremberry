function InitDatatable() {

    dt = $("#datatable").DataTable({
        language: {
            url: 'assets/plugins/custom/datatables/pt_BR.json'
        },
        searchDelay: 500,
        pageLength: 50,
        processing: true,
        serverSide: true,
        order: [[1, 'asc']],
        stateSave: false,
        ajax: {
            url: "Filme/Listar",
            method: 'POST',
            datatype: 'json',
            data: function (data) {
                data.Titulo = $('#titulo').val();
                data.Ano = $('#ano').val();
                data.IdGenero = $('#idgenero').val();
                data.Start = data.start;
                data.Limit = data.length;
            }
        },
        columns: [
            { data: 'id' },
            { data: 'titulo' },
            { data: 'ano' },
            { data: 'genero.nome' },
            { data: null },
        ],
        columnDefs: [
            {
                targets: 0,
                orderable: false,
                render: function (data, type, row) {
                    return ``;
                }
            },
            {
                targets: -1,
                data: null,
                orderable: false,
                className: 'text-end',
                render: function (data, type, row) {
                    return `
                         <a href="Filme/Detalhes?id=${row.id}" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1 btn-acao-editar">
                            <span b-s582s56vir="" class="svg-icon svg-icon-1">
                                <svg b-s582s56vir="" width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path b-s582s56vir="" opacity="0.3" d="M20 3H4C2.89543 3 2 3.89543 2 5V16C2 17.1046 2.89543 18 4 18H4.5C5.05228 18 5.5 18.4477 5.5 19V21.5052C5.5 22.1441 6.21212 22.5253 6.74376 22.1708L11.4885 19.0077C12.4741 18.3506 13.6321 18 14.8167 18H20C21.1046 18 22 17.1046 22 16V5C22 3.89543 21.1046 3 20 3Z" fill="currentColor"></path>
                                    <rect b-s582s56vir="" x="6" y="12" width="7" height="2" rx="1" fill="currentColor"></rect>
                                    <rect b-s582s56vir="" x="6" y="7" width="12" height="2" rx="1" fill="currentColor"></rect>
                                </svg>
                            </span>
                        </a>

                        <a href="Filme/Editar?id=${row.id}" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1 btn-acao-editar">
		                    <span class="svg-icon svg-icon-3">
			                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
				                    <path opacity="0.3" d="M21.4 8.35303L19.241 10.511L13.485 4.755L15.643 2.59595C16.0248 2.21423 16.5426 1.99988 17.0825 1.99988C17.6224 1.99988 18.1402 2.21423 18.522 2.59595L21.4 5.474C21.7817 5.85581 21.9962 6.37355 21.9962 6.91345C21.9962 7.45335 21.7817 7.97122 21.4 8.35303ZM3.68699 21.932L9.88699 19.865L4.13099 14.109L2.06399 20.309C1.98815 20.5354 1.97703 20.7787 2.03189 21.0111C2.08674 21.2436 2.2054 21.4561 2.37449 21.6248C2.54359 21.7934 2.75641 21.9115 2.989 21.9658C3.22158 22.0201 3.4647 22.0084 3.69099 21.932H3.68699Z" fill="currentColor"></path>
				                    <path d="M5.574 21.3L3.692 21.928C3.46591 22.0032 3.22334 22.0141 2.99144 21.9594C2.75954 21.9046 2.54744 21.7864 2.3789 21.6179C2.21036 21.4495 2.09202 21.2375 2.03711 21.0056C1.9822 20.7737 1.99289 20.5312 2.06799 20.3051L2.696 18.422L5.574 21.3ZM4.13499 14.105L9.891 19.861L19.245 10.507L13.489 4.75098L4.13499 14.105Z" fill="currentColor"></path>
			                    </svg>
		                    </span>
	                    </a>
	                    <a href="#" class="btn btn-icon btn-bg-light btn-active-color-primary btn-sm btn-acao-excluir" data-kt-docs-table-filter="delete_row" idreg="${row.id}" value="${row.id}">
		                    <span class="svg-icon svg-icon-3">
			                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
				                    <path d="M5 9C5 8.44772 5.44772 8 6 8H18C18.5523 8 19 8.44772 19 9V18C19 19.6569 17.6569 21 16 21H8C6.34315 21 5 19.6569 5 18V9Z" fill="currentColor"></path>
				                    <path opacity="0.5" d="M5 5C5 4.44772 5.44772 4 6 4H18C18.5523 4 19 4.44772 19 5V5C19 5.55228 18.5523 6 18 6H6C5.44772 6 5 5.55228 5 5V5Z" fill="currentColor"></path>
				                    <path opacity="0.5" d="M9 4C9 3.44772 9.44772 3 10 3H14C14.5523 3 15 3.44772 15 4V4H9V4Z" fill="currentColor"></path>
			                    </svg>
		                    </span>
	                    </a>
                    `;
                },
            },
        ],
        createdRow: function (row, data, dataIndex) {
            //$(row).find('td:eq(4)').attr('data-filter', data.CreditCardType);
        }
    });

    dt.on('draw', function () {
        InitToggleToolbar();
        ToggleToolbars();
        DeleteRows();
    });
}

function SearchDatatable() {
    const filterButton = document.querySelector('[data-kt-docs-table-filter="filter"]');
    filterButton.addEventListener('click', function () {
        dt.search("").draw();
    });
}

function InitToggleToolbar() {
    const container = document.querySelector('#datatable');
    const checkboxes = container.querySelectorAll('[type="checkbox"]');

    checkboxes.forEach(c => {
        c.addEventListener('click', function () {
            setTimeout(function () {
                ToggleToolbars();
            }, 50);
        });
    });
}

function ToggleToolbars() {
    const container = document.querySelector('#datatable');
    const toolbarBase = document.querySelector('[data-kt-docs-table-toolbar="base"]');
    const toolbarSelected = document.querySelector('[data-kt-docs-table-toolbar="selected"]');
    const selectedCount = document.querySelector('[data-kt-docs-table-select="selected_count"]');

    const allCheckboxes = container.querySelectorAll('tbody [type="checkbox"]');

    let checkedState = false;
    let count = 0;

    allCheckboxes.forEach(c => {
        if (c.checked) {
            checkedState = true;
            count++;
        }
    });

    if (checkedState) {
        selectedCount.innerHTML = count;
        toolbarBase.classList.add('d-none');
    } else {
        toolbarBase.classList.remove('d-none');
    }
}

function DeleteRows() {
    const deleteButtons = document.querySelectorAll('[data-kt-docs-table-filter="delete_row"]');

    deleteButtons.forEach(d => {
        d.addEventListener('click', function (e) {
            e.preventDefault();

            const parent = e.target.closest('tr');
            const txtInfo = parent.querySelectorAll('td')[2].innerText;
            var idReg = e.currentTarget.getAttribute("idreg");

            Swal.fire({
                text: "Tem certeza que deseja excluir " + txtInfo + "?",
                icon: "question",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Sim",
                cancelButtonText: "Não, cancelar",
                customClass: {
                    confirmButton: "btn fw-bold btn-danger",
                    cancelButton: "btn fw-bold btn-active-light-primary"
                }
            }).then(function (result) {
                if (result.value) {
                    const selection = [];
                    selection.push(idReg);

                    AjaxDelete(selection);
                }
            });
        })
    });
}

function AjaxDelete(selection) {

    if (selection.length > 0) {
        var form_data = new FormData();
        form_data.append("__RequestVerificationToken", $('input[name ="__RequestVerificationToken"]').val());

        for (var i = 0; i < selection.length; i++) {
            form_data.append('ids[]', selection[i]);
        }

        $.ajax({
            url: '/Filme/Deletar',
            type: 'POST',
            contentType: false,
            processData: false,
            data: form_data,
            dataType: 'json',
            success: function (data) {
                if (data.success) {

                    dt.draw();

                    const container = document.querySelector('#datatable');
                    const headerCheckbox = container.querySelectorAll('[type="checkbox"]')[0];
                    headerCheckbox.checked = false;

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
    }
};

$(document).ready(function () {
    InitDatatable();
    SearchDatatable();
    InitToggleToolbar();
    DeleteRows();
});