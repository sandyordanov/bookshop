using DAL;
using DAL.Interfaces;

namespace desktop
{
    internal static class Program
    {
        private static IBookRepository bookRepo = new BookRepository();
        private static IReviewRepository revRepo = new ReviewRepository();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new JungleDesktop(bookRepo,revRepo));
        }
    }
}