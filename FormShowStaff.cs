public class FormShowStaff : Form {
    private static readonly Color TEAL = Color.FromArgb(0, 137, 123);
    private static readonly Color RED  = Color.FromArgb(211, 47, 47);

    private int current_index = 0;
    private List<Staff> staff_list;
    private Panel fields_panel;

    private Label lbl_nav;
    private Label lbl_first_name_val, lbl_last_name_val, lbl_email_val, lbl_phone_val;
    private Label lbl_address_val, lbl_id_val, lbl_role_val, lbl_balance_val;
    private Label lbl_normal_hours_val, lbl_extra_hours_val, lbl_salary_val;
    private PictureBox photo_box;

    public FormShowStaff() {
        staff_list = Program.bank.get_staff_list();

        this.Text = "Show Staff";
        this.Size = new Size(720, 545);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.BackColor = Color.FromArgb(129, 128, 254);

        Button btn_prev = make_btn("<-", Color.FromArgb(80, 79, 200), new Point(20, 15), new Size(50, 34));
        lbl_nav = new Label();
        lbl_nav.Location = new Point(80, 15);
        lbl_nav.Size = new Size(130, 34);
        lbl_nav.TextAlign = ContentAlignment.MiddleCenter;
        lbl_nav.Font = new Font("Segoe UI", 11);
        lbl_nav.ForeColor = Color.White;
        Button btn_next = make_btn("->", Color.FromArgb(80, 79, 200), new Point(220, 15), new Size(50, 34));
        btn_prev.Click += (s, e) => { if (current_index > 0) { current_index--; display_staff(); } };
        btn_next.Click += (s, e) => { if (current_index < staff_list.Count - 1) { current_index++; display_staff(); } };
        this.Controls.AddRange(new Control[] { btn_prev, lbl_nav, btn_next });

        fields_panel = new Panel();
        fields_panel.BackColor = Color.FromArgb(196, 191, 255);
        fields_panel.Location = new Point(10, 57);
        fields_panel.Size = new Size(690, 398);

        int lx = 10, fy = 12, gap = 35;
        add_field_label("Name:",            lx, fy);             lbl_first_name_val   = add_value_label(lx + 130, fy);
        add_field_label("Last Name:",       lx, fy + gap);       lbl_last_name_val    = add_value_label(lx + 130, fy + gap);
        add_field_label("Email:",           lx, fy + gap * 2);   lbl_email_val        = add_value_label(lx + 130, fy + gap * 2);
        add_field_label("Phone Number:",    lx, fy + gap * 3);   lbl_phone_val        = add_value_label(lx + 130, fy + gap * 3);
        add_field_label("Address:",         lx, fy + gap * 4);   lbl_address_val      = add_value_label(lx + 130, fy + gap * 4);
        add_field_label("Staff ID:",        lx, fy + gap * 5);   lbl_id_val           = add_value_label(lx + 130, fy + gap * 5);
        add_field_label("Role:",            lx, fy + gap * 6);   lbl_role_val         = add_value_label(lx + 130, fy + gap * 6);
        add_field_label("Staff Balance:",   lx, fy + gap * 7);   lbl_balance_val      = add_value_label(lx + 130, fy + gap * 7);
        add_field_label("Normal Hours:",    lx, fy + gap * 8);   lbl_normal_hours_val = add_value_label(lx + 130, fy + gap * 8);
        add_field_label("Extra Hours:",     lx, fy + gap * 9);   lbl_extra_hours_val  = add_value_label(lx + 130, fy + gap * 9);
        add_field_label("Salary Per Hour:", lx, fy + gap * 10);  lbl_salary_val       = add_value_label(lx + 130, fy + gap * 10);

        photo_box = new PictureBox();
        photo_box.Location = new Point(512, 8);
        photo_box.Size = new Size(168, 200);
        photo_box.BackColor = Color.FromArgb(160, 155, 230);
        photo_box.SizeMode = PictureBoxSizeMode.Zoom;
        photo_box.BorderStyle = BorderStyle.FixedSingle;
        fields_panel.Controls.Add(photo_box);

        this.Controls.Add(fields_panel);

        Button btn_edit   = make_btn("Edit",               TEAL, new Point(20, 465),  new Size(130, 40));
        Button btn_manage = make_btn("Manage Hours & Pay", TEAL, new Point(162, 465), new Size(180, 40));
        Button btn_exit   = make_btn("Exit",               RED,  new Point(570, 465), new Size(120, 40));
        btn_edit.Click   += btn_edit_click;
        btn_manage.Click += btn_manage_click;
        btn_exit.Click   += (s, e) => this.Close();
        this.Controls.AddRange(new Control[] { btn_edit, btn_manage, btn_exit });

        display_staff();
    }

    private void display_staff() {
        if (staff_list.Count == 0) return;
        Staff s = staff_list[current_index];
        lbl_nav.Text              = $"{current_index + 1} From {staff_list.Count}";
        lbl_first_name_val.Text   = s.FirstName;
        lbl_last_name_val.Text    = s.LastName;
        lbl_email_val.Text        = s.Email;
        lbl_phone_val.Text        = s.PhoneNumber;
        lbl_address_val.Text      = s.Address;
        lbl_id_val.Text           = s.StaffId;
        lbl_role_val.Text         = s.Role;
        lbl_balance_val.Text      = $"${s.StaffBalance:F2}";
        lbl_normal_hours_val.Text = $"{s.NormalWorkingHours:F1} hrs";
        lbl_extra_hours_val.Text  = $"{s.ExtraWorkingHours:F1} hrs";
        lbl_salary_val.Text       = $"${s.SalaryPerHour:F2}/hr";

        if (s.PhotoUrl != "" && File.Exists(s.PhotoUrl)) {
            photo_box.Image = Image.FromFile(s.PhotoUrl);
        } else {
            photo_box.Image = null;
        }
    }

    private void btn_edit_click(object? sender, EventArgs e) {
        new FormEditStaff(staff_list[current_index]).ShowDialog();
        display_staff();
    }

    private void btn_manage_click(object? sender, EventArgs e) {
        new FormManageStaff(staff_list[current_index]).ShowDialog();
        display_staff();
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
        l.ForeColor = Color.FromArgb(30, 20, 80);
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
