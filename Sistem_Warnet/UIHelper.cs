using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sistem_Warnet
{
    public static class UIHelper
    {
        // Color Palette definitions
        public static readonly Color ColorBgForm = Color.FromArgb(15, 23, 42);       // #0F172A - Slate Dark Background
        public static readonly Color ColorBgCard = Color.FromArgb(30, 41, 59);       // #1E293B - Lighter Slate Dark for containers/cards
        public static readonly Color ColorBgInput = Color.FromArgb(24, 32, 48);      // Deeper Slate for inputs
        
        public static readonly Color ColorTextPrimary = Color.FromArgb(248, 250, 252); // #F8FAFC - Off-white
        public static readonly Color ColorTextSecondary = Color.FromArgb(148, 163, 184); // #94A3B8 - Slate Gray Muted
        public static readonly Color ColorTextAccent = Color.FromArgb(96, 165, 250);   // #60A5FA - Light Blue
        
        public static readonly Color ColorAccentBlue = Color.FromArgb(37, 99, 235);    // #2563EB - Royal Blue
        public static readonly Color ColorAccentGreen = Color.FromArgb(16, 185, 129);  // #10B981 - Emerald Green
        public static readonly Color ColorAccentOrange = Color.FromArgb(245, 158, 11); // #F59E0B - Amber Orange
        public static readonly Color ColorAccentRed = Color.FromArgb(239, 68, 68);     // #EF4444 - Rose Red
        public static readonly Color ColorAccentGray = Color.FromArgb(75, 85, 99);     // #4B5563 - Slate Gray
        
        public static readonly string FontName = "Segoe UI";

        /// <summary>
        /// Recursively applies the modern tech dark theme to a Form and all its child controls.
        /// </summary>
        public static void ApplyTheme(Form frm)
        {
            if (frm == null) return;

            frm.BackColor = ColorBgForm;
            frm.ForeColor = ColorTextPrimary;
            frm.Font = new Font(FontName, 10f, FontStyle.Regular);
            frm.StartPosition = FormStartPosition.CenterScreen;

            // Apply theme recursively to all controls
            ApplyThemeToControls(frm.Controls);
        }

        private static void ApplyThemeToControls(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                // Skip if already themed
                if (ctrl.Tag != null && ctrl.Tag.ToString() == "themed")
                {
                    continue;
                }

                // Apply to specific control types
                if (ctrl is Button btn)
                {
                    FormatButton(btn);
                }
                else if (ctrl is DataGridView dgv)
                {
                    FormatGrid(dgv);
                }
                else if (ctrl is TextBox txt)
                {
                    FormatTextBox(txt);
                }
                else if (ctrl is ComboBox cmb)
                {
                    FormatComboBox(cmb);
                }
                else if (ctrl is NumericUpDown nud)
                {
                    FormatNumericUpDown(nud);
                }
                else if (ctrl is Label lbl)
                {
                    FormatLabel(lbl);
                }
                else if (ctrl is Panel pnl)
                {
                    FormatPanel(pnl);
                    ApplyThemeToControls(pnl.Controls); // Recurse inside panels
                }
                else if (ctrl is GroupBox grp)
                {
                    FormatGroupBox(grp);
                    ApplyThemeToControls(grp.Controls); // Recurse inside groupboxes
                }
                else if (ctrl is Chart chart)
                {
                    FormatChart(chart);
                }
                else if (ctrl is BindingNavigator nav)
                {
                    FormatBindingNavigator(nav);
                }
                else
                {
                    // For container controls or other standard components, apply forecolor and recurse if it has controls
                    ctrl.ForeColor = ColorTextPrimary;
                    ctrl.Font = new Font(FontName, ctrl.Font.Size > 10f ? ctrl.Font.Size : 10f, ctrl.Font.Style);
                    if (ctrl.Controls.Count > 0)
                    {
                        ApplyThemeToControls(ctrl.Controls);
                    }
                }
            }
        }

        private static void FormatButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font(FontName, 9.5f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Height = Math.Max(btn.Height, 32); // Ensure reasonable button height
            
            // Determine dynamic button action color based on its Text or BackColor
            Color mainColor = ColorAccentBlue; // Default
            string text = btn.Text.ToLower().Trim();

            if (text.Contains("simpan") || text.Contains("save") || text.Contains("pesan") || text.Contains("cetak") || text.Contains("print") || text.Contains("connect"))
            {
                mainColor = ColorAccentGreen;
            }
            else if (text.Contains("delete") || text.Contains("hapus") || text.Contains("batal") || text.Contains("cancel") || text.Contains("logout") || text.Contains("reset"))
            {
                mainColor = ColorAccentRed;
            }
            else if (text.Contains("update") || text.Contains("edit") || text.Contains("ubah"))
            {
                mainColor = ColorAccentOrange;
            }
            else if (text.Contains("cari") || text.Contains("search") || text.Contains("refresh") || text.Contains("tampilkan") || text.Contains("load") || text.Contains("hitung") || text.Contains("import"))
            {
                mainColor = ColorAccentGray;
            }

            btn.BackColor = mainColor;
            btn.ForeColor = Color.White;

            // Interactive Hover & Click Effects
            btn.MouseEnter += (s, e) => { btn.BackColor = GetHoverColor(mainColor); };
            btn.MouseLeave += (s, e) => { btn.BackColor = mainColor; };
            btn.MouseDown += (s, e) => { btn.BackColor = GetPressedColor(mainColor); };
            btn.MouseUp += (s, e) => { btn.BackColor = GetHoverColor(mainColor); };

            btn.Tag = "themed";
        }

        private static void FormatTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.BackColor = ColorBgInput;
            txt.ForeColor = Color.White;
            txt.Font = new Font(FontName, 10f, FontStyle.Regular);
            
            // Adjust height padding indirectly via font or multiline checks
            txt.Tag = "themed";
        }

        private static void FormatComboBox(ComboBox cmb)
        {
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.BackColor = ColorBgInput;
            cmb.ForeColor = Color.White;
            cmb.Font = new Font(FontName, 10f, FontStyle.Regular);
            cmb.Tag = "themed";
        }

        private static void FormatNumericUpDown(NumericUpDown nud)
        {
            nud.BorderStyle = BorderStyle.FixedSingle;
            nud.BackColor = ColorBgInput;
            nud.ForeColor = Color.White;
            nud.Font = new Font(FontName, 10f, FontStyle.Regular);
            nud.Tag = "themed";
        }

        private static void FormatLabel(Label lbl)
        {
            // Determine if the label is a title/header
            bool isTitle = lbl.Font.Size > 12f || lbl.Text.ToUpper() == lbl.Text && lbl.Text.Length > 5;
            
            if (isTitle)
            {
                lbl.Font = new Font(FontName, lbl.Font.Size > 12f ? lbl.Font.Size : 14f, FontStyle.Bold);
                lbl.ForeColor = ColorTextAccent;
            }
            else
            {
                lbl.Font = new Font(FontName, 10f, FontStyle.Regular);
                lbl.ForeColor = ColorTextSecondary;
            }
            
            lbl.Tag = "themed";
        }

        private static void FormatPanel(Panel pnl)
        {
            // If the panel isn't custom colored, set it to the Card Background color to create structured sectioning
            if (pnl.BackColor == SystemColors.Control || pnl.BackColor == SystemColors.ActiveCaption)
            {
                pnl.BackColor = ColorBgCard;
            }
            pnl.Tag = "themed";
        }

        private static void FormatGroupBox(GroupBox grp)
        {
            grp.BackColor = ColorBgCard;
            grp.ForeColor = ColorTextAccent;
            grp.Font = new Font(FontName, 10f, FontStyle.Bold);
            grp.Tag = "themed";
        }

        private static void FormatGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = ColorBgCard;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = Color.FromArgb(51, 65, 85); // Slate Grid lines
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false; // Hide row headers for a cleaner layout
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Column Header styling
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorBgForm;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = ColorTextPrimary;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(FontName, 10f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorBgForm;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = ColorTextPrimary;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeight = 36;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Row cell styling
            dgv.RowsDefaultCellStyle.BackColor = ColorBgCard;
            dgv.RowsDefaultCellStyle.ForeColor = ColorTextPrimary;
            dgv.RowsDefaultCellStyle.Font = new Font(FontName, 9.5f, FontStyle.Regular);
            dgv.RowsDefaultCellStyle.SelectionBackColor = ColorAccentBlue;
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.RowsDefaultCellStyle.Padding = new Padding(6, 4, 6, 4);

            // Alternating Row style
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(24, 32, 48);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = ColorTextPrimary;
            dgv.AlternatingRowsDefaultCellStyle.Font = new Font(FontName, 9.5f, FontStyle.Regular);
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = ColorAccentBlue;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.Padding = new Padding(6, 4, 6, 4);

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowTemplate.Height = 32;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ScrollBars = ScrollBars.Both;
            dgv.Tag = "themed";
        }

        private static void FormatChart(Chart chart)
        {
            chart.BackColor = ColorBgCard;
            chart.BorderlineColor = Color.Transparent;

            // Style Titles
            foreach (Title t in chart.Titles)
            {
                t.Font = new Font(FontName, 12f, FontStyle.Bold);
                t.ForeColor = ColorTextPrimary;
            }

            // Style Legends
            foreach (Legend leg in chart.Legends)
            {
                leg.BackColor = ColorBgCard;
                leg.ForeColor = ColorTextSecondary;
                leg.Font = new Font(FontName, 9f, FontStyle.Regular);
            }

            // Style Chart Areas
            foreach (ChartArea area in chart.ChartAreas)
            {
                area.BackColor = ColorBgInput;
                area.AxisX.LabelStyle.ForeColor = ColorTextSecondary;
                area.AxisX.LabelStyle.Font = new Font(FontName, 9f, FontStyle.Regular);
                area.AxisX.LineColor = ColorAccentGray;
                area.AxisX.MajorGrid.LineColor = Color.FromArgb(51, 65, 85);

                area.AxisY.LabelStyle.ForeColor = ColorTextSecondary;
                area.AxisY.LabelStyle.Font = new Font(FontName, 9f, FontStyle.Regular);
                area.AxisY.LineColor = ColorAccentGray;
                area.AxisY.MajorGrid.LineColor = Color.FromArgb(51, 65, 85);
            }

            // Style Series Colors
            foreach (Series ser in chart.Series)
            {
                ser.Font = new Font(FontName, 9f, FontStyle.Bold);
                ser.LabelForeColor = ColorTextPrimary;
                // Apply gradient coloring
                ser.Color = ColorAccentBlue;
                ser.BackSecondaryColor = Color.FromArgb(99, 102, 241); // Indigo accent
                ser.BackGradientStyle = GradientStyle.LeftRight;
            }

            chart.Tag = "themed";
        }

        private static void FormatBindingNavigator(BindingNavigator nav)
        {
            nav.BackColor = ColorBgForm;
            nav.ForeColor = ColorTextPrimary;
            nav.RenderMode = ToolStripRenderMode.System;
            
            // Format buttons and labels inside navigator
            foreach (ToolStripItem item in nav.Items)
            {
                item.ForeColor = ColorTextPrimary;
                item.Font = new Font(FontName, 9f, FontStyle.Regular);
                
                if (item is ToolStripTextBox txt)
                {
                    txt.BackColor = ColorBgInput;
                    txt.ForeColor = Color.White;
                }
            }
            nav.Tag = "themed";
        }

        // Color modulation helpers for hover/click feel
        private static Color GetHoverColor(Color mainColor)
        {
            int r = Math.Min(255, mainColor.R + 25);
            int g = Math.Min(255, mainColor.G + 25);
            int b = Math.Min(255, mainColor.B + 25);
            return Color.FromArgb(r, g, b);
        }

        private static Color GetPressedColor(Color mainColor)
        {
            int r = Math.Max(0, mainColor.R - 25);
            int g = Math.Max(0, mainColor.G - 25);
            int b = Math.Max(0, mainColor.B - 25);
            return Color.FromArgb(r, g, b);
        }
    }
}
