public class User {
    private string first_name;
    private string last_name;
    private string photo_url;
    private string email;
    private string address;
    private string phone_number;

    public User(string first_name, string last_name, string photo_url, string email, string address, string phone_number) {
        this.first_name = first_name;
        this.last_name = last_name;
        this.photo_url = photo_url;
        this.email = email;
        this.address = address;
        this.phone_number = phone_number;
    }

    public string FirstName {
        get { return first_name; }
        set { first_name = value; }
    }

    public string LastName {
        get { return last_name; }
        set { last_name = value; }
    }

    public string PhotoUrl {
        get { return photo_url; }
        set { photo_url = value; }
    }

    public string Email {
        get { return email; }
        set { email = value; }
    }

    public string Address {
        get { return address; }
        set { address = value; }
    }

    public string PhoneNumber {
        get { return phone_number; }
        set { phone_number = value; }
    }
}