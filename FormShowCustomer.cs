public class FormShowCustomer : Form {
    private static readonly Color GREEN = Color.FromArgb(56, 142, 60);
    private static readonly Color RED   = Color.FromArgb(211, 47, 47);

    private int current_index = 0;
    private List<Customer> customers;
    private Panel fields_panel;

    private Label lbl_nav;
    private Label lbl_first_name_val, lbl_last_name_val, lbl_email_val, lbl_phone_val;
    private Label lbl_address_val, lbl_account_val, lbl_id_val, lbl_plan_val;
    private Label lbl_balance_val, lbl_savings_val;
    private PictureBox photo_box;

    public FormShowCustomer() {
        customers = Program.bank.get_customers();

        this.Text = "Show Customer";
        this.Size = new Size(720, 530);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.BackColor = Color.FromArgb(255, 224, 193);

        Button btn_prev = make_btn("<-", GREEN, new Point(20, 15), new Size(50, 34));
        lbl_nav = new Label();
        lbl_nav.Location = new Point(80, 15);
        lbl_nav.Size = new Size(130, 34);
        lbl_nav.TextAlign = ContentAlignment.MiddleCenter;
        lbl_nav.Font = new Font("Segoe UI", 11);
        Button btn_next = make_btn("->", GREEN, new Point(220, 15), new Size(50, 34));
        btn_prev.Click += (s, e) => { if (current_index > 0) { current_index--; display_customer(); } };
        btn_next.Click += (s, e) => { if (current_index < customers.Count - 1) { current_index++; display_customer(); } };
        this.Controls.AddRange(new Control[] { btn_prev, lbl_nav, btn_next });

        fields_panel = new Panel();
        fields_panel.BackColor = Color.FromArgb(255, 193, 126);
        fields_panel.Location = new Point(10, 57);
        fields_panel.Size = new Size(690, 388);

        int lx = 10, fy = 12, gap = 38;
        add_field_label("Name:",           lx, fy);           lbl_first_name_val = add_value_label(lx + 130, fy);
        add_field_label("Last Name:",      lx, fy + gap);     lbl_last_name_val  = add_value_label(lx + 130, fy + gap);
        add_field_label("Email:",          lx, fy + gap * 2); lbl_email_val      = add_value_label(lx + 130, fy + gap * 2);
        add_field_label("Phone Number:",   lx, fy + gap * 3); lbl_phone_val      = add_value_label(lx + 130, fy + gap * 3);
        add_field_label("Address:",        lx, fy + gap * 4); lbl_address_val    = add_value_label(lx + 130, fy + gap * 4);
        add_field_label("Account Number:", lx, fy + gap * 5); lbl_account_val    = add_value_label(lx + 130, fy + gap * 5);
        add_field_label("Customer ID:",    lx, fy + gap * 6); lbl_id_val         = add_value_label(lx + 130, fy + gap * 6);
        add_field_label("Plan:",           lx, fy + gap * 7); lbl_plan_val       = add_value_label(lx + 130, fy + gap * 7);
        add_field_label("Balance:",        lx, fy + gap * 8); lbl_balance_val    = add_value_label(lx + 130, fy + gap * 8);
        add_field_label("Saving:",         lx, fy + gap * 9); lbl_savings_val    = add_value_label(lx + 130, fy + gap * 9);

        photo_box = new PictureBox();
        photo_box.Location = new Point(512, 8);
        photo_box.Size = new Size(168, 200);
        photo_box.BackColor = Color.FromArgb(220, 170, 100);
        photo_box.SizeMode = PictureBoxSizeMode.Zoom;
        photo_box.BorderStyle = BorderStyle.FixedSingle;
        fields_panel.Controls.Add(photo_box);

        this.Controls.Add(fields_panel);

        Button btn_edit    = make_btn("Edit",            GREEN, new Point(20, 456), new Size(130, 40));
        Button btn_control = make_btn("Control Balance", GREEN, new Point(162, 456), new Size(150, 40));
        Button btn_exit    = make_btn("Exit",            RED,   new Point(570, 456), new Size(120, 40));
        btn_edit.Click    += btn_edit_click;
        btn_control.Click += btn_control_click;
        btn_exit.Click    += (s, e) => this.Close();
        this.Controls.AddRange(new Control[] { btn_edit, btn_control, btn_exit });

        display_customer();
    }

    private void display_customer() {
        if (customers.Count == 0) return;
        Customer c = customers[current_index];
        lbl_nav.Text            = $"{current_index + 1} From {customers.Count}";
        lbl_first_name_val.Text = c.FirstName;
        lbl_last_name_val.Text  = c.LastName;
        lbl_email_val.Text      = c.Email;
        lbl_phone_val.Text      = c.PhoneNumber;
        lbl_address_val.Text    = c.Address;
        lbl_account_val.Text    = c.AccountNumber;
        lbl_id_val.Text         = c.UserId;
        lbl_plan_val.Text       = c.Plan;
        lbl_balance_val.Text    = $"${c.Balance:F2}";
        lbl_savings_val.Text    = $"${c.Savings:F2}";

        if (c.PhotoUrl != "" && File.Exists(c.PhotoUrl)) {
            photo_box.Image = Image.FromFile(c.PhotoUrl);
        } else {
            photo_box.Image = null;
        }
    }

    private void btn_edit_click(object? sender, EventArgs e) {
        new FormEditCustomer(customers[current_index]).ShowDialog();
        display_customer();
    }

    private void btn_control_click(object? sender, EventArgs e) {
        new FormControlBalance(customers[current_index]).ShowDialog();
        display_customer();
    }

    private void add_field_label(string text, int x, int y) {
        Label l = new Label();
        l.Text = text;
        l.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        l.Location = new Point(x, y);
        l.Size = new Size(125, 22);
        fields_panel.Controls.Add(l);
    }

    private Label add_value_label(int x, int y) {
        Label l = new Label();
        l.Font = new Font("Segoe UI", 9);
        l.Location = new Point(x, y);
        l.Size = new Size(275, 22);
        l.ForeColor = Color.FromArgb(60, 30, 0);
        fields_panel.Controls.Add(l);
        return l;
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
