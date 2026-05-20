// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using GolfTaskManager;


namespace GolfTaskManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var menu = new MenuPrincipal();
            menu.Afficher();
        }
    }
}