public class Customer : User {
    private string user_id;
    private string account_number;
    private string plan;
    private double balance;
    private double savings;

    
    public Customer(string first_name, string last_name, string photo_url, string email, string address, string phone_number, string user_id, string account_number, string plan, double balance, double savings) : base(first_name, last_name, photo_url, email, address, phone_number) {
        this.user_id = user_id;
        this.account_number = account_number;
        this.plan = plan;
        this.balance = balance;
        this.savings = savings;
    }

    public string UserId {
        get { return user_id; }
        set { user_id = value; }
    }

    public string AccountNumber {
        get { return account_number; }
        set { account_number = value; }
    }

    public string Plan {
        get { return plan; }
        set { plan = value; }
    }

    public double Balance {
        get { return balance; }
        set { balance = value; }
    }

    public double Savings {
        get { return savings; }
        set { savings = value; }
    }

    public void Deposit(double amount) {
        if(amount > 0){
            balance += amount;
        }
        if(amount < 0){
            Console.WriteLine("enter a positive number");
        }
    }

    public void Withdraw(double amount) {
        if(amount > 0 && amount <= balance){
            balance -= amount;
        }
        if(amount < 0){
            Console.WriteLine("enter a positive number");
        }
        if(amount > balance){
            Console.WriteLine("insufficient funds");
        }
    }

    public void transfer_to_savings(double amount){
        if(amount > 0 && amount <= balance){
            balance -= amount;
            savings += amount;
        }
        if(amount < 0){
            Console.WriteLine("enter a positive number");
        }
        if(amount > balance){
            Console.WriteLine("insufficient funds");
        }
    }
}