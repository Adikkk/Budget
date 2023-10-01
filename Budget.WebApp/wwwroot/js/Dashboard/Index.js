init = async () => {
    const expensesAmount = await renderExpenses();
    const incomesAmount = await renderIncomes();

    renderBalance(expensesAmount, incomesAmount);
}

renderExpenses = async () => {
    const expenses = await $.ajax({
        url: 'https://localhost:7082/api/expenses',
        type: 'GET'
    });

    let amount = 0;

    [...expenses].forEach((expense) => {
        amount += expense.amount;
    });

    document.querySelector('#expenses').textContent = `-${amount} PLN`;

    return amount;
}

renderIncomes = async () => {
    const incomes = await $.ajax({
        url: 'https://localhost:7082/api/incomes',
        type: 'GET'
    });

    let amount = 0;

    [...incomes].forEach((expense) => {
        amount += expense.amount;
    });

    document.querySelector('#incomes').textContent = `+${amount} PLN`;

    return amount;
}

renderBalance = (expensesAmount, incomesAmount) => {
    let amount = incomesAmount - expensesAmount;
    let text = `${amount} PLN`;

    if (amount > 0) {
        text = `+${amount} PLN`;
    }

    document.querySelector('#balance').textContent = text;
}