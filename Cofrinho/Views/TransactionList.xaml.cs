using Cofrinho.Repositories;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;

namespace Cofrinho.Views;

public partial class TransactionList : ContentPage
{

    //Publisher e Subscribe 
    //TransactionAdd -> Publisher > Cadastro.
    //Subscribers -> TransactionList (Cadastro).


    private ITransactionRepository _repository;

    public TransactionList(ITransactionRepository repository)
    {

        this._repository = repository;



        InitializeComponent();
        //Busca no banco de Dados os registros salvos;

        //Codigo abaixo so roda quando tiver um novo cadastro.
        Reload();
        WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
        {
            Reload();
        });

    }

    private void Reload(){

        var items = _repository.GettAll();
        CollectionViewTransactions.ItemsSource = items;

       double income = items.Where(a => a.Type == Models.TransactionType.Income).Sum(a => a.Value);
       double expense = items.Where(a => a.Type == Models.TransactionType.Expenses).Sum(a => a.Value);
       double balance = income - expense;

        LabelIncome.Text = income.ToString("C");
        LabelExpense.Text = expense.ToString("C");
        LabelBalance.Text = balance.ToString("C");
    }

	private void OnButtonClicked_To_TransactionAdd(object sender, EventArgs args)
    {

        

        var transactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
        Navigation.PushModalAsync(transactionAdd);
		

    }

    private void OnButtonClicked_To_TransactionEdit(object sender, EventArgs e)
    {
        var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
        Navigation.PushModalAsync(transactionEdit);
    }
}