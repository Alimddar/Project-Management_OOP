public class FormMain : Form {
    private static readonly Color CUST_BTN  = Color.FromArgb(0, 180, 90);
    private static readonly Color STAFF_BTN = Color.FromArgb(0, 160, 145);
    private static readonly Color EXIT_BTN  = Color.FromArgb(234, 130, 127);
    private static readonly Color CUST_BG   = Color.FromArgb(1, 254, 127);
    private static readonly Color STAFF_BG  = Color.FromArgb(64, 224, 207);

    public FormMain() {
        this.Text = "My Bank Management";
        this.Size = new Size(440, 520);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.BackColor = Color.FromArgb(249, 241, 230);

        Panel cust_panel = make_section_panel(CUST_BG, 20, 20);
        cust_panel.Controls.Add(make_section_label("Customer Control", Color.FromArgb(0, 80, 40)));
        Button btn_add_cust  = make_btn("Add Customers",  CUST_BTN, new Point(20, 65), new Size(160, 50));
        Button btn_show_cust = make_btn("Show Customers", CUST_BTN, new Point(198, 65), new Size(160, 50));
        btn_add_cust.Click  += (s, e) => new FormAddCustomer().ShowDialog();
        btn_show_cust.Click += (s, e) => {
            if (Program.bank.get_customers().Count == 0) {
                MessageBox.Show("no customers found.", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            new FormShowCustomer().ShowDialog();
        };
        cust_panel.Controls.Add(btn_add_cust);
        cust_panel.Controls.Add(btn_show_cust);

        Panel staff_panel = make_section_panel(STAFF_BG, 20, 205);
        staff_panel.Controls.Add(make_section_label("Staff Control", Color.FromArgb(0, 60, 55)));
        Button btn_add_staff  = make_btn("Add Staff",  STAFF_BTN, new Point(20, 65), new Size(160, 50));
        Button btn_show_staff = make_btn("Show Staff", STAFF_BTN, new Point(198, 65), new Size(160, 50));
        btn_add_staff.Click  += (s, e) => new FormAddStaff().ShowDialog();
        btn_show_staff.Click += (s, e) => {
            if (Program.bank.get_staff_list().Count == 0) {
                MessageBox.Show("no staff found.", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            new FormShowStaff().ShowDialog();
        };
        staff_panel.Controls.Add(btn_add_staff);
        staff_panel.Controls.Add(btn_show_staff);

        Label lbl_footer = new Label();
        lbl_footer.Text = "Developed by David";
        lbl_footer.Font = new Font("Segoe UI", 10, FontStyle.Italic);
        lbl_footer.ForeColor = Color.FromArgb(100, 80, 60);
        lbl_footer.Location = new Point(20, 392);
        lbl_footer.Size = new Size(380, 26);
        lbl_footer.TextAlign = ContentAlignment.MiddleCenter;

        Button btn_exit = make_btn("Exit", EXIT_BTN, new Point(155, 430), new Size(110, 42));
        btn_exit.Click += (s, e) => Application.Exit();

        this.Controls.AddRange(new Control[] { cust_panel, staff_panel, lbl_footer, btn_exit });
    }

    private Panel make_section_panel(Color bg, int x, int y) {
        Panel p = new Panel();
        p.BackColor = bg;
        p.Location = new Point(x, y);
        p.Size = new Size(380, 165);
        return p;
    }

    private Label make_section_label(string text, Color color) {
        Label l = new Label();
        l.Text = text;
        l.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        l.ForeColor = color;
        l.Location = new Point(0, 8);
        l.Size = new Size(380, 42);
        l.TextAlign = ContentAlignment.MiddleCenter;
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
