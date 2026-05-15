public class Bank {
    private List<Customer> customers;
    private List<Staff> staff_members;
    private static Random random = new Random();

    public Bank() {
        customers = new List<Customer>();
        staff_members = new List<Staff>();
    }

    public void add_customer(Customer customer) {
        string user_id = generate_user_id();
        string account_number = generate_account_number();

        while (!is_unique_user_id(user_id)) {
            user_id = generate_user_id();
        }

        while (!is_unique_account_number(account_number)) {
            account_number = generate_account_number();
        }

        customer.UserId = user_id;
        customer.AccountNumber = account_number;
        customers.Add(customer);

        Console.WriteLine($"Customer added successfully! User ID: {user_id}, Account Number: {account_number}");
    }

    public void add_staff(Staff staff) {
        string staff_id = generate_staff_id();

        while (!is_unique_staff_id(staff_id)) {
            staff_id = generate_staff_id();
        }

        staff.StaffId = staff_id;
        staff_members.Add(staff);

        Console.WriteLine($"Staff added successfully! Staff ID: {staff_id}");
    }

    public Customer get_customer_by_id(string customer_id) {
        foreach (Customer customer in customers) {
            if (customer.UserId == customer_id) {
                return customer;
            }
        }
        Console.WriteLine("customer not found");
        return null;
    }

    public Staff get_staff_by_id(string staff_id) {
        foreach (Staff staff in staff_members) {
            if (staff.StaffId == staff_id) {
                return staff;
            }
        }
        Console.WriteLine("staff not found");
        return null;
    }

    public void display_all_customers() {
        foreach (Customer customer in customers) {
            Console.WriteLine($"Name: {customer.FirstName} {customer.LastName} | ID: {customer.UserId} | Balance: {customer.Balance:F2}");
        }
    }

    public void display_all_staff() {
        foreach (Staff staff in staff_members) {
            Console.WriteLine($"Name: {staff.FirstName} {staff.LastName} | ID: {staff.StaffId} | Role: {staff.Role} | Balance: {staff.StaffBalance:F2}");
        }
    }

    private string generate_user_id() {
        string chars = "ABCDE";
        string letter = chars[random.Next(chars.Length)].ToString();
        string digit = random.Next(1000, 9999).ToString();
        return $"{letter}-{digit}";
    }

    private string generate_staff_id() {
        string chars = "TLXYZ";
        string letter = chars[random.Next(chars.Length)].ToString();
        string digit = random.Next(1000, 9999).ToString();
        return $"{letter}-{digit}";
    }

    private string generate_account_number() {
        string pt1 = "5585";
        string pt2 = random.Next(1000, 9999).ToString();
        string pt3 = random.Next(1000, 9999).ToString();
        string pt4 = random.Next(1000, 9999).ToString();
        return $"{pt1}-{pt2}-{pt3}-{pt4}";
    }

    private bool is_unique_user_id(string user_id) {
        foreach (Customer customer in customers) {
            if (customer.UserId == user_id) {
                return false;
            }
        }
        return true;
    }

    private bool is_unique_staff_id(string staff_id) {
        foreach (Staff staff in staff_members) {
            if (staff.StaffId == staff_id) {
                return false;
            }
        }
        return true;
    }

    private bool is_unique_account_number(string account_number) {
        foreach (Customer customer in customers) {
            if (customer.AccountNumber == account_number) {
                return false;
            }
        }
        return true;
    }
}