public class FormControlBalance : Form {
    private static readonly Color GREEN = Color.FromArgb(56, 142, 60);
    private static readonly Color RED   = Color.FromArgb(211, 47, 47);
    private static readonly Color TEAL  = Color.FromArgb(0, 137, 123);

    private Customer customer;
    private TextBox tb_amount;
    private Label lbl_balance_val, lbl_savings_val;

    public FormControlBalance(Customer c) {
        customer = c;

        this.Text = "Account Control";
        this.Size = new Size(420, 450);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.BackColor = Color.FromArgb(241, 253, 239);

        Label lbl_name = new Label();
        lbl_name.Text = $"{c.FirstName} {c.LastName}";
        lbl_name.Font = new Font("Segoe UI", 13, FontStyle.Bold);
        lbl_name.Location = new Point(20, 15);
        lbl_name.Size = new Size(360, 28);
        lbl_name.TextAlign = ContentAlignment.MiddleCenter;
        this.Controls.Add(lbl_name);

        int lx = 60, vx = 215, fy = 58, gap = 28;

        add_info_label("Account:",    lx, fy);
        Label lbl_acc_val = new Label();
        lbl_acc_val.Text = c.AccountNumber;
        lbl_acc_val.Font = new Font("Segoe UI", 9);
        lbl_acc_val.Location = new Point(vx, fy);
        lbl_acc_val.Size = new Size(155, 22);
        lbl_acc_val.ForeColor = Color.FromArgb(27, 94, 32);
        this.Controls.Add(lbl_acc_val);

        add_info_label("Customer ID:", lx, fy + gap);
        Label lbl_id_val = new Label();
        lbl_id_val.Text = c.UserId;
        lbl_id_val.Font = new Font("Segoe UI", 9);
        lbl_id_val.Location = new Point(vx, fy + gap);
        lbl_id_val.Size = new Size(155, 22);
        lbl_id_val.ForeColor = Color.FromArgb(27, 94, 32);
        this.Controls.Add(lbl_id_val);

        add_info_label("Balance:", lx, fy + gap * 2);
        lbl_balance_val = new Label();
        lbl_balance_val.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        lbl_balance_val.Location = new Point(vx, fy + gap * 2);
        lbl_balance_val.Size = new Size(130, 24);
        lbl_balance_val.ForeColor = Color.FromArgb(27, 94, 32);
        this.Controls.Add(lbl_balance_val);

        add_info_label("Savings:", lx, fy + gap * 3);
        lbl_savings_val = new Label();
        lbl_savings_val.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        lbl_savings_val.Location = new Point(vx, fy + gap * 3);
        lbl_savings_val.Size = new Size(130, 24);
        lbl_savings_val.ForeColor = Color.FromArgb(0, 77, 64);
        this.Controls.Add(lbl_savings_val);

        Label lbl_amount = new Label();
        lbl_amount.Text = "Amount:";
        lbl_amount.Font = new Font("Segoe UI", 10);
        lbl_amount.Location = new Point(60, 180);
        lbl_amount.Size = new Size(80, 26);
        this.Controls.Add(lbl_amount);

        tb_amount = new TextBox();
        tb_amount.Location = new Point(150, 180);
        tb_amount.Size = new Size(190, 28);
        tb_amount.Font = new Font("Segoe UI", 10);
        this.Controls.Add(tb_amount);

        Button btn_deposit  = make_btn("Deposit",             GREEN, new Point(60, 225), new Size(280, 42));
        Button btn_withdraw = make_btn("Withdraw",            RED,   new Point(60, 278), new Size(280, 42));
        Button btn_transfer = make_btn("Transfer to Savings", TEAL,  new Point(60, 331), new Size(280, 42));
        btn_deposit.Click  += btn_deposit_click;
        btn_withdraw.Click += btn_withdraw_click;
        btn_transfer.Click += btn_transfer_click;
        this.Controls.AddRange(new Control[] { btn_deposit, btn_withdraw, btn_transfer });

        update_display();
    }

    private void update_display() {
        lbl_balance_val.Text = $"${customer.Balance:F2}";
        lbl_savings_val.Text = $"${customer.Savings:F2}";
    }

    private bool try_get_amount(out double amount) {
        if (!double.TryParse(tb_amount.Text, out amount) || amount <= 0) {
            MessageBox.Show("please enter a valid positive amount.", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        return true;
    }

    private void btn_deposit_click(object? sender, EventArgs e) {
        if (!try_get_amount(out double amount)) return;
        customer.Deposit(amount);
        Program.bank.save_data();
        update_display();
    }

    private void btn_withdraw_click(object? sender, EventArgs e) {
        if (!try_get_amount(out double amount)) return;
        customer.Withdraw(amount);
        Program.bank.save_data();
        update_display();
    }

    private void btn_transfer_click(object? sender, EventArgs e) {
        if (!try_get_amount(out double amount)) return;
        customer.transfer_to_savings(amount);
        Program.bank.save_data();
        update_display();
    }

    private void add_info_label(string text, int x, int y) {
        Label l = new Label();
        l.Text = text;
        l.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        l.Location = new Point(x, y);
        l.Size = new Size(150, 24);
        this.Controls.Add(l);
    }

    private Button make_btn(string text, Color bg, Point loc, Size size) {
        Button btn = new Button();
        btn.Text = text;
        btn.BackColor = bg;
        btn.ForeColor = Color.White;
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.Font = new Font("Segoe UI", 10);
        btn.Location = loc;
        btn.Size = size;
        return btn;
    }
}
