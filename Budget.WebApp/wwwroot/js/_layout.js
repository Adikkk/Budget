checkAPI = () => {
    $.get({
        url: 'https://localhost:7082/api/service/ping',
        statusCode: {
            200: function (response) {
                setAPIStatusLabel(true);
            },
            400: function (response) {
                setAPIStatusLabel(false);
            },
            0: function (response) {
                setAPIStatusLabel(false);
            },
        },
    });
}

setAPIStatusLabel = (isUp) => {
    const label = document.querySelector('#api-status-label');

    if (isUp) {
        label.textContent = 'online';
        label.classList.add('text-success');
    } else {
        label.textContent = 'offline';
        label.classList.add('text-danger');
    }
};

setActiveTab = () => {
    const pathName = window.location.pathname;

    const expenseTabContainer = document.querySelector('#expense-nav-tab');
    const dashboardTabContainer = document.querySelector('#dashboard-nav-tab');
    const incomeTabContainer = document.querySelector('#income-nav-tab');


    if (pathName.includes("Expense")) {
        expenseTabContainer.classList.add('active');
        dashboardTabContainer.classList.remove('active');
        incomeTabContainer.classList.remove('active');

    } else if (pathName.includes("Incomes")) {
        expenseTabContainer.classList.remove('active');
        dashboardTabContainer.classList.remove('active');
        incomeTabContainer.classList.add('active');
    } else {
        expenseTabContainer.classList.remove('active');
        dashboardTabContainer.classList.add('active');
        incomeTabContainer.classList.remove('active');
    }
};