using System;
using System.Windows;
using System.Windows.Controls;
using KasaBiletowa.Model;

namespace KasaBiletowa;

public partial class Register : Window
{
    private readonly KasaBiletowaContext _context = new();

    public Register()
    {
        InitializeComponent();
    }

    // Open Login window on close
    private void Register_OnClosed(object? sender, EventArgs e)
    {
        var login = new Login();
        login.Show();
    }


    private void ButtonRegister_OnClick(object sender, RoutedEventArgs e)
    {
        // check if all fields are filled
        // check if email is correct
        // check if password is correct
        // check if password and confirm password are the same
        // check if email is not already in database
        // if everything is correct, add new user to database
        // else show error message

        if (string.IsNullOrEmpty(Imie.Text) || string.IsNullOrEmpty(Nazwisko.Text) ||
            string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(Haslo.Password))
        {
            MessageBox.Show("Wypełnij wszystkie pola");
            return;
        }

        if (!Email.Text.Contains("@"))
        {
            MessageBox.Show("Niepoprawny adres email");
            return;
        }

        if (Haslo.Password.Length < 8)
        {
            MessageBox.Show("Hasło musi mieć co najmniej 8 znaków");
            return;
        }

        var klient = new Klient()
        {
            Email = Email.Text,
            Haslo = Haslo.Password,
            Imie = Imie.Text,
            Nazwisko = Nazwisko.Text,
        };

        _context.Klienci.Add(klient);
        _context.SaveChanges();

        MessageBox.Show("Zarejestrowano pomyślnie, teraz możesz się zalogować");
        var login = new Login();
        login.Show();
        Close();
    }
}