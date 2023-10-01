initIndex = () => {
    initializeTable();
};

initDetails = () => {
    renderIncome('Details');
};

initEdit = () => {
    renderIncome('Edit');
    addEventListeners('Edit');
}

initCreate = () => {
    addEventListeners('Create');
}

addEventListeners = (mode) => {
    if (mode === 'Edit') {
        const saveButton = document.querySelector('#edit-save-button');
        saveButton.addEventListener('click', editIncome);
    }
    else if (mode === 'Create') {
        const saveButton = document.querySelector('#create-save-button');
        saveButton.addEventListener('click', createIncome);
    }
}

getIncomeById = async (id) => {
    const income = await $.get({
        url: `https://localhost:7082/api/incomes/${id}`,
    });

    return income;
};

getIncomes = async () => {
    const incomes = await $.get({
        url: 'https://localhost:7082/api/incomes',
    });

    return incomes;
};

renderIncome = async (renderType) => {
    const guidRegex = /(?:\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\}{0,1})/g;
    let idContainer = document.querySelector('[bkey="id"]');

    if (renderType === 'Details') {
        idContainer = idContainer.textContent;
    }
    else {
        idContainer = idContainer.value;
    }

    const incomeId = idContainer.match(guidRegex);

    const income = await getIncomeById(incomeId);

    mapIncome(income, renderType);
}

mapIncome = (income, renderType) => {
    const selectors = document.querySelectorAll('[bkey]');

    selectors.forEach((selector) => {
        const bkey = selector.getAttribute('bkey');
        if (bkey in income) {
            if (renderType === 'Details') {
                selector.textContent = income[bkey];
            }
            else {
                selector.value = income[bkey];
            }
        }
    });
}

initializeTable = async () => {
    const incomes = await getIncomes();

    new gridjs.Grid({
        columns: [
            {
                name: "Id",
                hidden: true
            },
            {
                name: "Name"
            },
            {
                name: "Description"
            },
            {
                name: "Category"
            },
            {
                name: "Amount"
            },
            {
                name: "Payment Date"
            },
            {
                name: 'Actions',
                formatter: (cell, row) => {

                    const editButton = gridjs.h('button', {
                        className: 'py-2 px-4 border rounded-md text-white btn btn-info table-button',
                        onClick: () => location.href = `/Income/Edit/${row.cells[0].data}`
                    }, 'Edit');

                    const detailsButton = gridjs.h('button', {
                        className: 'py-2 px-4 border rounded-md text-white btn btn-warning table-button',
                        onClick: () => location.href = `/Income/Details/${row.cells[0].data}`
                    }, 'Details');

                    const deleteButton = gridjs.h('button', {
                        className: 'py-2 px-4 border rounded-md text-white btn btn-danger table-button',
                        onClick: () => {
                            generateDeleteModal(row.cells[0].data);
                            $('#delete-income-modal').modal('show');
                        }
                    }, 'Delete');

                    let buttons = [];
                    buttons.push(editButton);
                    buttons.push(detailsButton);
                    buttons.push(deleteButton);

                    return buttons;
                }
            },
        ],
        data: incomes
    }).render(document.getElementById("wrapper"));
}

editIncome = () => {
    const request = serializeData("Edit");

    $.ajax({
        url: 'https://localhost:7082/api/incomes',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(request),
        success: () => {
            toastr.success("Successfully updated income");
            window.setTimeout(function () {
                toastr.clear();
                location.href = "/Income";
            }, 2000);
        }
    });

}

serializeData = (mode) => {
    let container = "";

    if (mode === 'Edit') {
        container = document.querySelector('#income-edit');
    }
    else if (mode === 'Create') {
        container = document.querySelector('#income-create');
    }
    const selectors = container.querySelectorAll('input');

    const request = {};

    [...selectors].forEach((selector) => {
        let bkey = selector.getAttribute('bkey');

        if (bkey === 'amount') {
            request[bkey] = Number(selector.value);
        } else {
            request[bkey] = selector.value;
        }

    });

    // TODO: temporary
    request['isActive'] = true;

    return request;
}

createIncome = () => {
    const request = serializeData('Create');

    $.ajax({
        url: 'https://localhost:7082/api/incomes',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(request),
        success: () => {
            toastr.success("Successfully created income");
            window.setTimeout(function () {
                toastr.clear();
                location.href = "/Income";
            }, 2000);
        }
    });
}

generateDeleteModal = (id) => {
    const modalsContainer = document.querySelector('.income-delete-modals');

    modalsContainer.innerHTML = `
<div class="modal fade" id="delete-income-modal" income-id="${id}" tabindex="-1" role="dialog" aria-labelledby="delete-income-modal-label" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="delete-income-modal-label">Delete income</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <p>Are you sure you want to remove this income? ID: ${id}</p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-danger delete-button">Delete</button>
            </div>
        </div>
    </div>
</div>
    `;

    document.querySelector(`[income-id="${id}"] .delete-button`).addEventListener('click', () => {
        $.ajax({
            url: `https://localhost:7082/api/incomes/${id}`,
            type: 'DELETE',
            success: () => {
                $('#delete-income-modal').modal('hide');
                toastr.success('Successfully deleted income.');
                window.setTimeout(function () {
                    toastr.clear();
                    location.href = "/Income";
                }, 2000);
            }
        });
    });
}