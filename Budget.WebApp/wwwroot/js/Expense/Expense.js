initIndex = () => {
    initializeTable();
};

initDetails = () => {
    renderExpenseDetails();
};

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

renderExpenseDetails = async () => {
    const guidRegex = /(?:\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\}{0,1})/g;
    const idContainer = document.querySelector('[bkey="id"]').textContent;
    const expenseId = idContainer.match(guidRegex);

    const expense = await getExpenseById(expenseId);

    mapExpense(expense);
}

mapExpense = (expense) => {
    const selectors = document.querySelectorAll('[bkey]');

    selectors.forEach((selector) => {
        const bkey = selector.getAttribute('bkey');
        if (bkey in expense) {
            selector.textContent = expense[bkey];
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
                        onClick: () => alert(`Editing "${row.cells[0].data}" "${row.cells[1].data}"`)
                    }, 'Edit');

                    const detailsButton = gridjs.h('button', {
                        className: 'py-2 px-4 border rounded-md text-white btn btn-warning table-button',
                        onClick: () => alert(`Editing "${row.cells[0].data}" "${row.cells[1].data}"`)
                    }, 'Details');

                    const deleteButton = gridjs.h('button', {
                        className: 'py-2 px-4 border rounded-md text-white btn btn-danger table-button',
                        onClick: () => alert(`Editing "${row.cells[0].data}" "${row.cells[1].data}"`)
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