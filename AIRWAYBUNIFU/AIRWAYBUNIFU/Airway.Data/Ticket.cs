using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;

namespace Airway.Data
{
    public class Ticket
    {
        PrintDocument pdoc = null;
        string ticketNo;
        DateTime TicketDate;
        String from, to, name, mobile;

        public Ticket(string ticketNo, DateTime TicketDate, String from,
               String to, string mobile, String name)
        {
            this.ticketNo = ticketNo;
            this.TicketDate = TicketDate;
            this.from = from;
            this.to = to;
            this.name = name;
            this.mobile = mobile;
        }
        public void print()
        {
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15);


            PaperSize psize = new PaperSize("Custom", 830, 400);

            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            pdoc.DefaultPageSettings.PaperSize.Height = 830;

            pdoc.DefaultPageSettings.PaperSize.Width = 400;

            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);

            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                PrintPreviewDialog pp = new PrintPreviewDialog();
                pp.Document = pdoc;
                result = pp.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pdoc.Print();
                }
            }

        }
        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Graphics graphics = e.Graphics;
                graphics.DrawRectangle(Pens.Black, 5, 5, 830, 400);

                string title = Application.StartupPath + "\\logo.png";
                Image yourImage = (Image)(new Bitmap(Image.FromFile(title), new Size(90, 90)));

                graphics.DrawImage(yourImage, 390, 20);



                Font font = new Font("Courier New", 20);
                SolidBrush brush = new SolidBrush(Color.Black);
                float fontHeight = font.GetHeight();
                int startX = 50;
                int startY = 55;
                int Offset = 70;
                graphics.DrawString("---------------AIUB Airline----------------", font, brush, startX, startY + Offset);

                font = new Font("Courier New", 14);
                Offset = Offset + 30;
                graphics.DrawString("Ticket No : " + this.ticketNo, font, brush, startX, startY + Offset);
                Offset = Offset + 30;
                graphics.DrawString("Ticket Date : " + this.TicketDate.Date, font, brush, startX, startY + Offset);
                Offset = Offset + 30;
                graphics.DrawString("Customer Name : " + this.name, font, brush, startX, startY + Offset);
                Offset = Offset + 30;
                graphics.DrawString("Contact no : " + this.mobile, font, brush, startX, startY + Offset);
                Offset = Offset + 30;
                graphics.DrawString("Traveling From : " + this.from + "  To : " + this.to, font, brush, startX, startY + Offset);

                //barCode font and drow it 
                //font = new Font("IDAutomationHC39M Free Version", 20);
                Offset = Offset + 40;
                //graphics.DrawString("Ticket No : " + this.ticketNo, font, brush, 250, startY + Offset);

                Image imgBarcode = BarcodeLib.Barcode.DoEncode(BarcodeLib.TYPE.CODE128, ticketNo, true, Color.Black, Color.White, 600, 60);
                graphics.DrawImage(imgBarcode, 40, startY + Offset);
            }
            catch(Exception ex) { MessageBox.Show("Error in printing "+e.ToString()); }
        }

        public static implicit operator string(Ticket v)
        {
            throw new NotImplementedException();
        }
    }
}