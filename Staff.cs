public class Staff : User {
    private string staff_id;
    private string role;
    private double staff_balance;
    private double normal_working_hours;
    private double extra_working_hours;
    private double salary_per_hour;

    public Staff(string first_name, string last_name, string photo_url, string email, string address, string phone_number, string staff_id, string role, double staff_balance, double normal_working_hours, double extra_working_hours, double salary_per_hour) : base(first_name, last_name, photo_url, email, address, phone_number) {
        this.staff_id = staff_id;
        this.role = role;
        this.staff_balance = staff_balance;
        this.normal_working_hours = normal_working_hours;
        this.extra_working_hours = extra_working_hours;
        this.salary_per_hour = salary_per_hour;
    }

    public string StaffId {
        get { return staff_id; }
        set { staff_id = value; }
    }

    public string Role {
        get { return role; }
        set { role = value; }
    }

    public double StaffBalance {
        get { return staff_balance; }
        set { staff_balance = value; }
    }

    public double NormalWorkingHours {
        get { return normal_working_hours; }
        set { normal_working_hours = value; }
    }

    public double ExtraWorkingHours {
        get { return extra_working_hours; }
        set { extra_working_hours = value; }
    }

    public double SalaryPerHour {
        get { return salary_per_hour; }
        set { salary_per_hour = value; }
    }

    public void add_normal_hours(double hours){
        if(hours > 0){
            normal_working_hours += hours;
        } else {
            Console.WriteLine("enter a positive number");
        }
    }

    public void add_extra_hours(double hours){
        if(hours > 0){
            extra_working_hours += hours;
        } else {
            Console.WriteLine("enter a positive number");
        }
    }

    public double calculate_salary(){
        double normal_pay = normal_working_hours * salary_per_hour;
        double extra_pay = extra_working_hours * salary_per_hour * 1.5;
        return normal_pay + extra_pay;
    }

    public double pay() {
        double total_salary = calculate_salary();
        staff_balance += total_salary;
        normal_working_hours = 0;
        extra_working_hours = 0;
        return total_salary;
    }
}