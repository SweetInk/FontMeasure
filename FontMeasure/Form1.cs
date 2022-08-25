using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace FontMesure
{
    public partial class FontMeasure : Form
    {
        int[] _font_size = new int[] { 6, 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        FontStyle[] _font_style = new FontStyle[] { FontStyle.Regular, FontStyle.Bold, FontStyle.Italic };
        String fontSizeDelimeter = "|";
        String fontDelimeter = "__";
        String fontPixelStr = "";
        public FontMeasure()
        {
            InitializeComponent();
            dumpToTxt.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(10, 10);
            bm.SetResolution(96, 96);
            Graphics g = Graphics.FromImage(bm);
            InstalledFontCollection fonts = new InstalledFontCollection();
            StringBuilder str = new StringBuilder(2000);
            foreach (FontFamily family in fonts.Families)
            {
                str.Append(family.Name);
                str.Append("\n");
                foreach (FontStyle style in _font_style)
                {
                    str.Append(style.ToString());
                    str.Append("@");
                    foreach (int fontSize in _font_size)
                    {
                        Font tmp = new Font(family.Name, fontSize, style);
                        float w = 0;
                        for (int i = 0; i < 10; i++)
                        {
                            SizeF sf = g.MeasureString(i + "", tmp, Int32.MaxValue, StringFormat.GenericTypographic);
                            if (sf.Width > w)
                            {
                                w = sf.Width;
                            }
                        }
                        str.Append(fontSize + ":" + w +fontSizeDelimeter);
                    }
                    str.Remove(str.Length - 1, 1);
                    str.Append("\n");
                }
                str.Append(fontDelimeter);
                str.Append("\n");
            }
            fontPixelStr = str.ToString();
            richTextBox1.Text = fontPixelStr;
            g.Dispose();
            dumpToTxt.Enabled = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dumpToTxt_Click(object sender, EventArgs e)
        {
            TextWriter textWriter = new StreamWriter("fontPixelRaw.txt", false);
            textWriter.Write(fontPixelStr);
            textWriter.Close();
        }
    }
}
