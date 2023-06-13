﻿using NoteApp.Views;

namespace NoteApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(NotePage), typeof(NotePage));
	}
}
