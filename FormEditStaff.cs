public class FormEditStaff : Form {
    private static readonly Color TEAL = Color.FromArgb(0, 137, 123);
    private static readonly Color RED  = Color.FromArgb(211, 47, 47);
    private static readonly Color GRAY = Color.FromArgb(100, 100, 100);

    private Staff staff;
    private TextBox tb_first_name, tb_last_name, tb_contact, tb_email, tb_address;
    private ComboBox cb_role;
    private PictureBox photo_box;
    private string selected_photo = "";

    private static readonly string[] roles = { "General Manager", "Manager", "Teller", "Security" };
    private static readonly double[] salaries = { 100, 75, 50, 30 };

    public FormEditStaff(Staff s) {
        staff = s;

        this.Text = "Edit Staff";
        this.Size = new Size(660, 530);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.BackColor = Color.White;

        int lx = 20, y = 20, gap = 68;
        add_label("Name:", lx, y);
        tb_first_name = add_textbox(lx, y + 22, 200);
        tb_first_name.Text = s.FirstName;

        add_label("Last Name:", lx, y + gap);
        tb_last_name = add_textbox(lx, y + gap + 22, 200);
        tb_last_name.Text = s.LastName;

        add_label("Contact:", lx, y + gap * 2);
        tb_contact = add_textbox(lx, y + gap * 2 + 22, 200);
        tb_contact.Text = s.PhoneNumber;

        add_label("Email:", lx, y + gap * 3);
        tb_email = add_textbox(lx, y + gap * 3 + 22, 200);
        tb_email.Text = s.Email;

        add_label("Address:", lx, y + gap * 4);
        tb_address = add_textbox(lx, y + gap * 4 + 22, 200);
        tb_address.Text = s.Address;

        add_label("Role:", lx, y + gap * 5);
        cb_role = new ComboBox();
        cb_role.Location = new Point(lx, y + gap * 5 + 22);
        cb_role.Size = new Size(200, 28);
        cb_role.DropDownStyle = ComboBoxStyle.DropDownList;
        cb_role.Font = new Font("Segoe UI", 10);
        cb_role.Items.AddRange(roles);
        cb_role.SelectedItem = s.Role;
        if (cb_role.SelectedIndex < 0) cb_role.SelectedIndex = 0;
        this.Controls.Add(cb_role);

        int mx = 245;
        add_label("Staff ID:", mx, 20);
        TextBox tb_id = add_textbox(mx, 42, 190);
        tb_id.Text = s.StaffId;
        tb_id.ReadOnly = true;
        tb_id.BackColor = Color.FromArgb(238, 238, 238);

        add_label("Staff Balance:", mx, 90);
        TextBox tb_bal = add_textbox(mx, 112, 190);
        tb_bal.Text = $"{s.StaffBalance:F2}";
        tb_bal.ReadOnly = true;
        tb_bal.BackColor = Color.FromArgb(238, 238, 238);

        int rx = 455;
        add_label("Photo:", rx, 20);
        photo_box = new PictureBox();
        photo_box.Location = new Point(rx, 42);
        photo_box.Size = new Size(170, 190);
        photo_box.BackColor = Color.LightGray;
        photo_box.SizeMode = PictureBoxSizeMode.Zoom;
        photo_box.BorderStyle = BorderStyle.FixedSingle;
        if (s.PhotoUrl != "" && File.Exists(s.PhotoUrl)) {
            photo_box.Image = Image.FromFile(s.PhotoUrl);
        }
        this.Controls.Add(photo_box);

        Button btn_change_img = make_btn("Change Image", GRAY, new Point(rx, 242), new Size(170, 32));
        btn_change_img.Click += btn_change_img_click;
        this.Controls.Add(btn_change_img);

        Button btn_modify = make_btn("Modify", TEAL, new Point(170, 455), new Size(130, 42));
        Button btn_cancel = make_btn("Cancel", RED,  new Point(318, 455), new Size(130, 42));
        btn_modify.Click += btn_modify_click;
        btn_cancel.Click += (s2, e) => this.Close();
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
        staff.FirstName     = tb_first_name.Text.Trim();
        staff.LastName      = tb_last_name.Text.Trim();
        staff.PhoneNumber   = tb_contact.Text.Trim();
        staff.Email         = tb_email.Text.Trim();
        staff.Address       = tb_address.Text.Trim();
        staff.Role          = cb_role.SelectedItem!.ToString()!;
        staff.SalaryPerHour = salaries[cb_role.SelectedIndex];
        if (selected_photo != "") staff.PhotoUrl = selected_photo;
        Program.bank.save_data();
        MessageBox.Show("staff updated successfully.", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
