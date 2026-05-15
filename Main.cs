static class Program {
    public static Bank bank = new Bank();

    [STAThread]
    static void Main() {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        bank.load_data();
        Application.Run(new FormMain());
    }
}
