﻿using Cofrinho.Views;

namespace Cofrinho;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new TransactionList();
	}
}
