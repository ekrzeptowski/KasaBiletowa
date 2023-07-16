using System;
using KasaBiletowa.Model;

namespace KasaBiletowa;

public class Global
{
    public static Klient? LoggedUser { get; private set; }

    public static void SetLoggedUser(Klient user)
    {
        if (LoggedUser != null)
        {
            throw new Exception("User already logged in");
        }

        LoggedUser = user;
    }

    public static void UnsetLoggedUser()
    {
        if (LoggedUser == null)
        {
            throw new Exception("User not logged in");
        }

        LoggedUser = null;
    }
}