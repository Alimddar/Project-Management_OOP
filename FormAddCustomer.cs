public class FormAddCustomer : Form {
    private static readonly Color GREEN = Color.FromArgb(56, 142, 60);
    private static readonly Color RED   = Color.FromArgb(211, 47, 47);
    private static readonly Color GRAY  = Color.FromArgb(100, 100, 100);

    private TextBox tb_first_name, tb_last_name, tb_contact, tb_email, tb_address;
    private ComboBox cb_plan;
    private TextBox tb_account_number, tb_customer_id;
    private PictureBox photo_box;
    private string selected_photo = "";

    public FormAddCustomer() {
        this.Text = "Add Customer";
        this.Size = new Size(660, 530);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.BackColor = Color.FromArgb(255, 224, 193);

        string prev_id  = Program.bank.preview_user_id();
        string prev_acc = Program.bank.preview_account_number();

        int lx = 20, y = 20, gap = 68;
        add_label("Name:", lx, y);
        tb_first_name = add_textbox(lx, y + 22, 200);

        add_label("Last Name:", lx, y + gap);
        tb_last_name = add_textbox(lx, y + gap + 22, 200);

        add_label("Contact:", lx, y + gap * 2);
        tb_contact = add_textbox(lx, y + gap * 2 + 22, 200);

        add_label("Email:", lx, y + gap * 3);
        tb_email = add_textbox(lx, y + gap * 3 + 22, 200);

        add_label("Address:", lx, y + gap * 4);
        tb_address = add_textbox(lx, y + gap * 4 + 22, 200);

        add_label("Plan:", lx, y + gap * 5);
        cb_plan = new ComboBox();
        cb_plan.Location = new Point(lx, y + gap * 5 + 22);
        cb_plan.Size = new Size(200, 28);
        cb_plan.DropDownStyle = ComboBoxStyle.DropDownList;
        cb_plan.Font = new Font("Segoe UI", 10);
        cb_plan.Items.AddRange(new string[] { "Silver Saving", "Gold Saving", "Platinum Saving" });
        cb_plan.SelectedIndex = 0;
        this.Controls.Add(cb_plan);

        int mx = 245;
        add_label("Account Number:", mx, 20);
        tb_account_number = add_textbox(mx, 42, 190);
        tb_account_number.Text = prev_acc;
        tb_account_number.ReadOnly = true;
        tb_account_number.BackColor = Color.FromArgb(238, 220, 180);

        add_label("Customer ID:", mx, 90);
        tb_customer_id = add_textbox(mx, 112, 190);
        tb_customer_id.Text = prev_id;
        tb_customer_id.ReadOnly = true;
        tb_customer_id.BackColor = Color.FromArgb(238, 220, 180);

        int rx = 455;
        add_label("Photo:", rx, 20);
        photo_box = new PictureBox();
        photo_box.Location = new Point(rx, 42);
        photo_box.Size = new Size(170, 190);
        photo_box.BackColor = Color.FromArgb(220, 190, 150);
        photo_box.SizeMode = PictureBoxSizeMode.Zoom;
        photo_box.BorderStyle = BorderStyle.FixedSingle;
        this.Controls.Add(photo_box);

        Button btn_browse = make_btn("Browse...", GRAY, new Point(rx, 242), new Size(170, 32));
        btn_browse.Click += btn_browse_click;
        this.Controls.Add(btn_browse);

        Button btn_save   = make_btn("Save",   GREEN, new Point(170, 455), new Size(130, 42));
        Button btn_cancel = make_btn("Cancel", RED,   new Point(318, 455), new Size(130, 42));
        btn_save.Click   += btn_save_click;
        btn_cancel.Click += (s, e) => this.Close();
        this.Controls.Add(btn_save);
        this.Controls.Add(btn_cancel);
    }

    private void btn_browse_click(object? sender, EventArgs e) {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
        if (dlg.ShowDialog() == DialogResult.OK) {
            selected_photo = dlg.FileName;
            photo_box.Image = Image.FromFile(selected_photo);
        }
    }

    private void btn_save_click(object? sender, EventArgs e) {
        if (tb_first_name.Text.Trim() == "" || tb_last_name.Text.Trim() == "") {
            MessageBox.Show("name and last name are required.", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        Customer c = new Customer(
            tb_first_name.Text.Trim(),
            tb_last_name.Text.Trim(),
            selected_photo,
            tb_email.Text.Trim(),
            tb_address.Text.Trim(),
            tb_contact.Text.Trim(),
            tb_customer_id.Text,
            tb_account_number.Text,
            cb_plan.SelectedItem!.ToString()!,
            0,
            0
        );
        Program.bank.add_customer_direct(c);
        Program.bank.save_data();
        MessageBox.Show($"customer added!\nid: {c.UserId}\naccount: {c.AccountNumber}", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        this.Close();
    }

    private void add_label(string text, int x, int y) {
        Label l = new Label();
        l.Text = text;
        l.Location = new Point(x, y);
        l.Size = new Size(210, 20);
        l.Font = new Font("Segoe UI", 9);
        this.Controls.Add(l);
    }

    private TextBox add_textbox(int x, int y, int width) {
        TextBox tb = new TextBox();
        tb.Location = new Point(x, y);
        tb.Size = new Size(width, 28);
        tb.Font = new Font("Segoe UI", 10);
        this.Controls.Add(tb);
        return tb;
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
