using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using KasaBiletowa.Model;

namespace KasaBiletowa;

public partial class Login : Window
{
    private readonly KasaBiletowaContext _context = new();

    public Login()
    {
        InitializeComponent();
    }


    private void Zaloguj_OnClick(object sender, RoutedEventArgs e)
    {
        // compare login and password with database
        // if login and password are correct, open new window
        // else show error message

        Debug.Write(LoginTextBox.Text);
        Debug.Write(PasswordBox.Password);
        var klient =
            _context.Klienci.SingleOrDefault(k => k.Email == LoginTextBox.Text && k.Haslo == PasswordBox.Password);

        if (klient == null)
        {
            MessageBox.Show("Niepoprawny login lub hasło");
            return;
        }

        Global.SetLoggedUser(klient);

        var window = new MainWindow();
        window.Show();
        Close();
    }

    private void Zarejestruj_OnClick(object sender, RoutedEventArgs e)
    {
        Register register = new();
        register.Show();
        Close();
    }
}