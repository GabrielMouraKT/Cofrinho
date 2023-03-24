using Cofrinho.Models;
using Cofrinho.Repositories;
using Java.Lang;

namespace Cofrinho.Views;

public partial class TransactionAdd : ContentPage
{
    private ITransactionRepository _repository;
	public TransactionAdd(ITransactionRepository repository)
	{
		InitializeComponent();
        _repository = repository;

       
	}
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void OnButtonClicked_Save(object sender, EventArgs e)
    {



        //Validar os dados digitados  return vazio finaliza nao cria exception.
        if (IsValidData() == false)
            return;


        //Salvar no Banco(Pegar os dados, Criar obj Transaction, Enviar para Banco de Dados.
        //Salvar no banco de dados.
        SaveTransactionInDatabase();


        //Fechar a Tela*
        Navigation.PopModalAsync();

       var count = _repository.GettAll().Count;
        App.Current.MainPage.DisplayAlert("Mensagem!",$"Existem {count} registro(s) no banco!","Ok");



    }

    private void SaveTransactionInDatabase()
    {
        Transaction transaction = new Transaction()
        {
            //Ternario
            Type = RadioIncome.IsChecked ? TransactionType.Income : TransactionType.Expenses,
            Name = EntryName.Text,
            Date = DatePickerDate.Date,
            Value = double.Parse(EntryValue.Text)

        };
        _repository.Add(transaction);
    }

    private bool IsValidData() 
        { 
        
            bool valid = true;
            StringBuilder sb = new StringBuilder();

            if(string.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text)) 
            {
                sb.Append("O campo 'Nome' deve ser preenchido!");
            valid = false;
            }
             if (string.IsNullOrEmpty(EntryValue.Text) || string.IsNullOrWhiteSpace(EntryValue.Text))
             {
             sb.Append("O campo 'Valor' deve ser preenchido!");
             valid = false;
             }
             double result;
             if (string.IsNullOrEmpty(EntryValue.Text) && double.TryParse(EntryValue.Text, out result))
             {
             sb.Append("O campo 'Valor' è invalido!");
             valid = false;

             }
             if (valid == false)
             {
            LabelError.Text = sb.ToString();
        }

        return valid;


    }

          
    }
