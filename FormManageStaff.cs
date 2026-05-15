public class FormManageStaff : Form {
    private static readonly Color ADD_BTN = Color.FromArgb(175, 255, 48);
    private static readonly Color PAY_BTN = Color.FromArgb(252, 104, 178);

    private Staff staff;
    private TextBox tb_normal_hours, tb_extra_hours;
    private Label lbl_normal_val, lbl_extra_val, lbl_unpayed_val;

    public FormManageStaff(Staff s) {
        staff = s;

        this.Text = "Staff Control";
        this.Size = new Size(420, 490);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.BackColor = Color.FromArgb(239, 255, 255);

        Label lbl_name = new Label();
        lbl_name.Text = $"{s.FirstName} {s.LastName}  ({s.Role})";
        lbl_name.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        lbl_name.Location = new Point(20, 15);
        lbl_name.Size = new Size(360, 28);
        lbl_name.TextAlign = ContentAlignment.MiddleCenter;
        this.Controls.Add(lbl_name);

        int lx = 60, vx = 215, fy = 55, gap = 28;
        add_info_label("Staff ID:",         lx, fy);
        Label lbl_id_val = new Label();
        lbl_id_val.Text = s.StaffId;
        lbl_id_val.Font = new Font("Segoe UI", 9);
        lbl_id_val.Location = new Point(vx, fy);
        lbl_id_val.Size = new Size(150, 22);
        lbl_id_val.ForeColor = Color.FromArgb(0, 80, 80);
        this.Controls.Add(lbl_id_val);

        add_info_label("Unemployed Hours:", lx, fy + gap);     lbl_normal_val  = add_value_label(vx, fy + gap);
        add_info_label("Extra Hours:",      lx, fy + gap * 2); lbl_extra_val   = add_value_label(vx, fy + gap * 2);
        add_info_label("Unpayed Balance:",  lx, fy + gap * 3); lbl_unpayed_val = add_value_label(vx, fy + gap * 3);

        add_section_label("Normal Hours:", 60, 178);
        tb_normal_hours = new TextBox();
        tb_normal_hours.Location = new Point(185, 175);
        tb_normal_hours.Size = new Size(155, 28);
        tb_normal_hours.Font = new Font("Segoe UI", 10);
        this.Controls.Add(tb_normal_hours);

        Button btn_add_normal = make_btn("Add Normal Hours", ADD_BTN, new Point(60, 215), new Size(280, 42));
        btn_add_normal.ForeColor = Color.FromArgb(40, 80, 0);
        btn_add_normal.Click += btn_add_normal_click;
        this.Controls.Add(btn_add_normal);

        add_section_label("Extra Hours:", 60, 273);
        tb_extra_hours = new TextBox();
        tb_extra_hours.Location = new Point(185, 270);
        tb_extra_hours.Size = new Size(155, 28);
        tb_extra_hours.Font = new Font("Segoe UI", 10);
        this.Controls.Add(tb_extra_hours);

        Button btn_add_extra = make_btn("Add Extra Hours", ADD_BTN, new Point(60, 310), new Size(280, 42));
        btn_add_extra.ForeColor = Color.FromArgb(40, 80, 0);
        btn_add_extra.Click += btn_add_extra_click;
        this.Controls.Add(btn_add_extra);

        Button btn_pay = make_btn("Pay", PAY_BTN, new Point(60, 368), new Size(280, 46));
        btn_pay.ForeColor = Color.White;
        btn_pay.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        btn_pay.Click += btn_pay_click;
        this.Controls.Add(btn_pay);

        update_display();
    }

    private void update_display() {
        lbl_normal_val.Text  = $"{staff.NormalWorkingHours:F1} hrs";
        lbl_extra_val.Text   = $"{staff.ExtraWorkingHours:F1} hrs";
        lbl_unpayed_val.Text = $"${staff.calculate_salary():F2}";
    }

    private bool try_get_hours(TextBox tb, out double hours) {
        if (!double.TryParse(tb.Text, out hours) || hours <= 0) {
            MessageBox.Show("please enter a valid positive number of hours.", "error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        return true;
    }

    private void btn_add_normal_click(object? sender, EventArgs e) {
        if (!try_get_hours(tb_normal_hours, out double hours)) return;
        staff.add_normal_hours(hours);
        Program.bank.save_data();
        update_display();
    }

    private void btn_add_extra_click(object? sender, EventArgs e) {
        if (!try_get_hours(tb_extra_hours, out double hours)) return;
        staff.add_extra_hours(hours);
        Program.bank.save_data();
        update_display();
    }

    private void btn_pay_click(object? sender, EventArgs e) {
        double total = staff.pay();
        Program.bank.save_data();
        update_display();
        MessageBox.Show($"paid ${total:F2} to {staff.FirstName} {staff.LastName}.\nhours have been reset.", "payment complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void add_info_label(string text, int x, int y) {
        Label l = new Label();
        l.Text = text;
        l.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        l.Location = new Point(x, y);
        l.Size = new Size(150, 22);
        this.Controls.Add(l);
    }

    private Label add_value_label(int x, int y) {
        Label l = new Label();
        l.Font = new Font("Segoe UI", 9);
        l.Location = new Point(x, y);
        l.Size = new Size(140, 22);
        l.ForeColor = Color.FromArgb(0, 80, 80);
        this.Controls.Add(l);
        return l;
    }

    private void add_section_label(string text, int x, int y) {
        Label l = new Label();
        l.Text = text;
        l.Font = new Font("Segoe UI", 10);
        l.Location = new Point(x, y);
        l.Size = new Size(120, 26);
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
