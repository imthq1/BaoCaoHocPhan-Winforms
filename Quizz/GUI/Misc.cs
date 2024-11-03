using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Misc
{
    public static void setGridViewStyle(DataGridView dgview)
    {
        dgview.BorderStyle = BorderStyle.None;
        dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
        dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgview.BackgroundColor = Color.White;
        dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    }
}
