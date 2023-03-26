using Cofrinho.Models;
using Cofrinho.Repositories;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;

namespace Cofrinho.Views;

public partial class TransactionAdd : ContentPage
{
    
    private ITransactionRepository _repository;
	public TransactionAdd(ITransactionRepository repository)
	{
		InitializeComponent();
        _repository = repository;
       

       
	}
    private void TapGestureRecognizerTappedToClose(object sender, TappedEventArgs e)
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
        WeakReferenceMessenger.Default.Send<string>(String.Empty);  

       

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
                sb.AppendLine("O campo 'Nome' deve ser preenchido!");
            valid = false;
            }
             if (string.IsNullOrEmpty(EntryValue.Text) || string.IsNullOrWhiteSpace(EntryValue.Text))
             {
             sb.AppendLine("O campo 'Valor' deve ser preenchido!");
             valid = false;
             }
             double result;
             if (!string.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text, out result))
             {
             sb.AppendLine("O campo 'Valor' è invalido!");
             valid = false;

             }
             if (valid == false)
             {
            LabelError.IsVisible= true;
            LabelError.Text = sb.ToString();
        }

        return valid;


    }

          
    }
