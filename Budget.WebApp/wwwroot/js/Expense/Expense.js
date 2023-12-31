﻿initIndex = () => {
    initializeTable();
};

initDetails = () => {
    renderExpense('Details');
};

initEdit = () => {
    renderExpense('Edit');
    addEventListeners('Edit');
}

initCreate = () => {
    addEventListeners('Create');
}

addEventListeners = (mode) => {
    if (mode === 'Edit') {
        const saveButton = document.querySelector('#edit-save-button');
        saveButton.addEventListener('click', editExpense);
    }
    else if (mode === 'Create') {
        const saveButton = document.querySelector('#create-save-button');
        saveButton.addEventListener('click', createExpense);
    }
}

getExpenseById = async (id) => {
    const expense = await $.get({
        url: `https://localhost:7082/api/expenses/${id}`,
    });

    return expense;
};

getExpenses = async () => {
    const expenses = await $.get({
        url: 'https://localhost:7082/api/expenses',
    });

    return expenses;
};

renderExpense = async (renderType) => {
    const guidRegex = /(?:\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\}{0,1})/g;
    let idContainer = document.querySelector('[bkey="id"]');

    if (renderType === 'Details') {
        idContainer = idContainer.textContent;
    }
    else {
        idContainer = idContainer.value;
    }

    const expenseId = idContainer.match(guidRegex);

    const expense = await getExpenseById(expenseId);

    mapExpense(expense, renderType);
}

mapExpense = (expense, renderType) => {
    const selectors = document.querySelectorAll('[bkey]');

    selectors.forEach((selector) => {
        const bkey = selector.getAttribute('bkey');
        if (bkey in expense) {
            if (renderType === 'Details') {
                selector.textContent = expense[bkey];
            }
            else
            {
                selector.value = expense[bkey];
            }
        }
    });
}

initializeTable = async () => {
    const expenses = await getExpenses();

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
                        onClick: () => location.href = `/Expense/Edit/${row.cells[0].data}`
                    }, 'Edit');

                    const detailsButton = gridjs.h('button', {
                        className: 'py-2 px-4 border rounded-md text-white btn btn-warning table-button',
                        onClick: () => location.href = `/Expense/Details/${row.cells[0].data}`
                    }, 'Details');

                    const deleteButton = gridjs.h('button', {
                        className: 'py-2 px-4 border rounded-md text-white btn btn-danger table-button',
                        onClick: () => {
                            generateDeleteModal(row.cells[0].data);
                            $('#delete-expense-modal').modal('show');
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
        data: expenses
    }).render(document.getElementById("wrapper"));
}

editExpense = () => {
    const request = serializeData("Edit");

     $.ajax({
        url: 'https://localhost:7082/api/expenses',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(request),
        success: () => {
            toastr.success("Successfully updated expense");
            window.setTimeout(function () {
                toastr.clear();
                location.href = "/Expense";
            }, 2000);
        }
     });

}

serializeData = (mode) => {
    let container = "";

    if (mode === 'Edit') {
        container = document.querySelector('#expense-edit');
    }
    else if (mode === 'Create'){
        container = document.querySelector('#expense-create');
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

createExpense = () => {
    const request = serializeData('Create');

    $.ajax({
        url: 'https://localhost:7082/api/expenses',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(request),
        success: () => {
            toastr.success("Successfully created expense");
            window.setTimeout(function () {
                toastr.clear();
                location.href = "/Expense";
            }, 2000);
        }
    });
}

generateDeleteModal = (id) => {
    const modalsContainer = document.querySelector('.expense-delete-modals');

    modalsContainer.innerHTML = `
<div class="modal fade" id="delete-expense-modal" expense-id="${id}" tabindex="-1" role="dialog" aria-labelledby="delete-expense-modal-label" aria-hidden="true">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="delete-expense-modal-label">Delete expense</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <p>Are you sure you want to remove this expense? ID: ${id}</p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-danger delete-button">Delete</button>
            </div>
        </div>
    </div>
</div>
    `;

    document.querySelector(`[expense-id="${id}"] .delete-button`).addEventListener('click', () => {
        $.ajax({
            url: `https://localhost:7082/api/expenses/${id}`,
            type: 'DELETE',
            success: () => {
                $('#delete-expense-modal').modal('hide');
                toastr.success('Successfully deleted expense.');
                window.setTimeout(function () {
                    toastr.clear();
                    location.href = "/Expense";
                }, 2000);
            }
        });
    });
}