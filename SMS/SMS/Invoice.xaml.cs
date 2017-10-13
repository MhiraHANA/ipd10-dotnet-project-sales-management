using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SMS
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : Window
    {
        public object groupBox1 { get; private set; }

        public Invoice()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pdf file (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (FileStream S = File.Open(saveFileDialog.FileName, FileMode.CreateNew))
                {
                    using (StreamWriter st = new StreamWriter(S))
                    {
                        foreach (var item in lsvInvoice.Items)
                            st.WriteLine(item.ToString());
                    }
                }
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            //PrintDocument doc = new PrintDocument();
            //doc.PrintPage += this.Doc_PrintPage;
            //PrintDialog dlgSettings = new PrintDialog();
            //dlgSettings.Document = doc;
            //if (dlgSettings.ShowDialog() == DialogResult.OK)
            //{
            //    doc.Print();
            //}
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //float x = e.MarginBounds.Left;
            //float y = e.MarginBounds.Top;
            //Bitmap bmp = new Bitmap(this.groupBox1.Width, this.groupBox1.Height);
            //this.groupBox1.DrawToBitmap(bmp, new Rectangle(0, 0, this.groupBox1.Width, this.groupBox1.Height));
            //e.Graphics.DrawImage((System.Drawing.Image)bmp, x, y);
        }
    }
}
