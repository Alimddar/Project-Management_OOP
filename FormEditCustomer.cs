public class FormEditCustomer : Form {
    private static readonly Color MODIFY = Color.FromArgb(158, 249, 156);
    private static readonly Color RED    = Color.FromArgb(211, 47, 47);
    private static readonly Color GRAY   = Color.FromArgb(100, 100, 100);

    private Customer customer;
    private TextBox tb_first_name, tb_last_name, tb_contact, tb_email, tb_address;
    private ComboBox cb_plan;
    private PictureBox photo_box;
    private string selected_photo = "";

    public FormEditCustomer(Customer c) {
        customer = c;

        this.Text = "Edit Customer";
        this.Size = new Size(660, 530);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.BackColor = Color.FromArgb(239, 230, 139);

        int lx = 20, y = 20, gap = 68;
        add_label("Name:", lx, y);
        tb_first_name = add_textbox(lx, y + 22, 200);
        tb_first_name.Text = c.FirstName;

        add_label("Last Name:", lx, y + gap);
        tb_last_name = add_textbox(lx, y + gap + 22, 200);
        tb_last_name.Text = c.LastName;

        add_label("Contact:", lx, y + gap * 2);
        tb_contact = add_textbox(lx, y + gap * 2 + 22, 200);
        tb_contact.Text = c.PhoneNumber;

        add_label("Email:", lx, y + gap * 3);
        tb_email = add_textbox(lx, y + gap * 3 + 22, 200);
        tb_email.Text = c.Email;

        add_label("Address:", lx, y + gap * 4);
        tb_address = add_textbox(lx, y + gap * 4 + 22, 200);
        tb_address.Text = c.Address;

        add_label("Plan:", lx, y + gap * 5);
        cb_plan = new ComboBox();
        cb_plan.Location = new Point(lx, y + gap * 5 + 22);
        cb_plan.Size = new Size(200, 28);
        cb_plan.DropDownStyle = ComboBoxStyle.DropDownList;
        cb_plan.Font = new Font("Segoe UI", 10);
        cb_plan.Items.AddRange(new string[] { "Silver Saving", "Gold Saving", "Platinum Saving" });
        cb_plan.SelectedItem = c.Plan;
        if (cb_plan.SelectedIndex < 0) cb_plan.SelectedIndex = 0;
        this.Controls.Add(cb_plan);

        int mx = 245;
        add_label("Account Number:", mx, 20);
        TextBox tb_acc = add_textbox(mx, 42, 190);
        tb_acc.Text = c.AccountNumber;
        tb_acc.ReadOnly = true;
        tb_acc.BackColor = Color.FromArgb(220, 210, 110);

        add_label("Customer ID:", mx, 90);
        TextBox tb_id = add_textbox(mx, 112, 190);
        tb_id.Text = c.UserId;
        tb_id.ReadOnly = true;
        tb_id.BackColor = Color.FromArgb(220, 210, 110);

        add_label("Balance:", mx, 160);
        TextBox tb_bal = add_textbox(mx, 182, 190);
        tb_bal.Text = $"{c.Balance:F2}";
        tb_bal.ReadOnly = true;
        tb_bal.BackColor = Color.FromArgb(220, 210, 110);

        add_label("Savings:", mx, 230);
        TextBox tb_sav = add_textbox(mx, 252, 190);
        tb_sav.Text = $"{c.Savings:F2}";
        tb_sav.ReadOnly = true;
        tb_sav.BackColor = Color.FromArgb(220, 210, 110);

        int rx = 455;
        add_label("Photo:", rx, 20);
        photo_box = new PictureBox();
        photo_box.Location = new Point(rx, 42);
        photo_box.Size = new Size(170, 190);
        photo_box.BackColor = Color.FromArgb(210, 200, 100);
        photo_box.SizeMode = PictureBoxSizeMode.Zoom;
        photo_box.BorderStyle = BorderStyle.FixedSingle;
        if (c.PhotoUrl != "" && File.Exists(c.PhotoUrl)) {
            photo_box.Image = Image.FromFile(c.PhotoUrl);
        }
        this.Controls.Add(photo_box);

        Button btn_change_img = make_btn("Change Image", GRAY, new Point(rx, 242), new Size(170, 32));
        btn_change_img.Click += btn_change_img_click;
        this.Controls.Add(btn_change_img);

        Button btn_modify = make_btn("Modify", MODIFY, new Point(170, 455), new Size(130, 42));
        Button btn_cancel = make_btn("Cancel", RED,    new Point(318, 455), new Size(130, 42));
        btn_modify.ForeColor = Color.FromArgb(30, 80, 30);
        btn_modify.Click += btn_modify_click;
        btn_cancel.Click += (s, e) => this.Close();
        this.Controls.Add(btn_modify);
        this.Controls.Add(btn_cancel);
    }

    private void btn_change_img_click(object? sender, EventArgs e) {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
        if (dlg.ShowDialog() == DialogResult.OK) {
            selected_photo = dlg.FileName;
            photo_box.Image = Image.FromFile(selected_photo);
        }
    }

    private void btn_modify_click(object? sender, EventArgs e) {
        if (tb_first_name.Text.Trim() == "" || tb_last_name.Text.Trim() == "") {
            MessageBox.Show("name and last name are required.", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        customer.FirstName   = tb_first_name.Text.Trim();
        customer.LastName    = tb_last_name.Text.Trim();
        customer.PhoneNumber = tb_contact.Text.Trim();
        customer.Email       = tb_email.Text.Trim();
        customer.Address     = tb_address.Text.Trim();
        customer.Plan        = cb_plan.SelectedItem!.ToString()!;
        if (selected_photo != "") customer.PhotoUrl = selected_photo;
        Program.bank.save_data();
        MessageBox.Show("customer updated successfully.", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
