using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using System.Text;

namespace PASSBOOKBRI.Data
{
    public class Printer
    {
        PrintServer printServer = new PrintServer();
        PrintDocument printDoc = new PrintDocument();
        Transaksi _trx = new Transaksi();
        Font font = new Font("Calibri", 8, FontStyle.Regular);

        public void BeginPrintEH(object sender, PrintEventArgs e)
        {
            SolidBrush blackbrush = new SolidBrush(Color.Black);
        }
        public void EndPrintEH(object sender, PrintEventArgs e)
        {
            SolidBrush blackbrush = new SolidBrush(Color.Black);
            blackbrush.Dispose();
        }
        public string PassbookPrintRekening(Transaksi trx)
        {
            printDoc = new PrintDocument();
            _trx = trx;
            string printername = "PsiPR-OLI";
            //string printername = "Canon MP280 series Printer";
            printDoc.PrinterSettings.PrinterName = printername;
            printDoc.BeginPrint += new PrintEventHandler(BeginPrintEH);
            printDoc.EndPrint += new PrintEventHandler(EndPrintEH);
            printDoc.PrintPage += new PrintPageEventHandler(PassbookPrintPageRekening);
            printDoc.Print();
            printServer.Printserver(printername);
            Console.WriteLine("Printing Selesai...");
            return _trx._saldo;
        }
        public void PassbookPrintPageRekening(object sender, PrintPageEventArgs e)
        {
            font = new Font("Calibri", 8, FontStyle.Regular);
            SolidBrush blackbrush = new SolidBrush(Color.Black);
            Graphics g = e.Graphics;

            long saldo = long.Parse(_trx._saldo);
            int baris = int.Parse(_trx._baris);
            int ypoint = 75;
            int sisabaris = 13 * baris;
            if (baris > 20)
            {
                sisabaris = sisabaris + (12 * 5);
            }
            ypoint += sisabaris;
            for (int i = 0; i < _trx._tipe.Length; i++)
            {
                string keterangan = _trx._sandi[i];
                int nominal = int.Parse(_trx._nominal[i]);

                if (keterangan == "DBT")
                {
                    string sandi = _trx._tipe[i];
                    saldo -= nominal;
                    string debetprint = "-" + nominal.ToString("N0");
                    g.DrawString(sandi, font, blackbrush, new Point(87, ypoint));
                    g.DrawString(debetprint, font, blackbrush, new Point(155, ypoint));
                    g.DrawString(saldo.ToString("N0"), font, blackbrush, new Point(340, ypoint));
                }
                else
                {
                    string sandi = _trx._tipe[i];
                    saldo += nominal;
                    string kreditprint = nominal.ToString("N0");
                    g.DrawString(sandi, font, blackbrush, new Point(87, ypoint));
                    g.DrawString(kreditprint, font, blackbrush, new Point(249, ypoint));
                    g.DrawString(saldo.ToString("N0"), font, blackbrush, new Point(340, ypoint));
                }

                g.DrawString(baris.ToString(), font, blackbrush, new Point(0, ypoint));
                g.DrawString(_trx._startdate, font, blackbrush, new Point(30, ypoint));
                g.DrawString(_trx._pengesahan[i], font, blackbrush, new Point(442, ypoint));
                baris += 1;
                ypoint += 13;
                if (baris == 21)
                {
                    ypoint = ypoint + (12 * 5);
                }
            }

            _trx._saldo = saldo.ToString();
        }
        public String PassbookPrintBisnis(Transaksi trx)
        {
            printDoc = new PrintDocument();
            _trx = trx;
            //printDoc.PrinterSettings.PrinterName = "Canon MP280 series Printer";
            string printername = "PsiPR-OLI";
            printDoc.PrinterSettings.PrinterName = printername;
            printDoc.BeginPrint += new PrintEventHandler(BeginPrintEH);
            printDoc.EndPrint += new PrintEventHandler(EndPrintEH);
            printDoc.PrintPage += new PrintPageEventHandler(PassbookPrintPageBisnis);
            printDoc.Print();
            printServer.Printserver(printername);
            Console.WriteLine("Printing Selesai...");
            return _trx._saldo;
        }

        public void PassbookPrintPageBisnis(object sender, PrintPageEventArgs e)
        {
            SolidBrush blackbrush = new SolidBrush(Color.Black);
            Graphics g = e.Graphics;

            int saldo = int.Parse(_trx._saldo);
            int baris = int.Parse(_trx._baris);
            int ypoint = 70;
            int sisabaris = 13 * baris;
            if (baris > 10)
            {
                sisabaris = sisabaris + (12 * 5);
            }
            ypoint += sisabaris;
            for (int i = 0; i < _trx._tipe.Length; i++)
            {

                if (i % 2 == 0)
                {
                    string keterangan = _trx._sandi[i];
                    int nominal = int.Parse(_trx._nominal[i]);

                    if (keterangan == "DBT")
                    {
                        string sandi = _trx._tipe[i];
                        saldo -= nominal;
                        string debetprint = "-" + nominal.ToString();
                        g.DrawString(sandi, font, blackbrush, new Point(87, ypoint));
                        g.DrawString(debetprint, font, blackbrush, new Point(155, ypoint));
                        g.DrawString(saldo.ToString(), font, blackbrush, new Point(340, ypoint));
                    }
                    else
                    {
                        string sandi = _trx._tipe[i];
                        saldo += nominal;
                        string kreditprint = nominal.ToString();
                        g.DrawString(sandi, font, blackbrush, new Point(87, ypoint));
                        g.DrawString(kreditprint, font, blackbrush, new Point(249, ypoint));
                        g.DrawString(saldo.ToString(), font, blackbrush, new Point(340, ypoint));
                    }

                    g.DrawString(baris.ToString(), font, blackbrush, new Point(0, ypoint));
                    g.DrawString(_trx._startdate, font, blackbrush, new Point(30, ypoint));
                    g.DrawString(_trx._pengesahan[i], font, blackbrush, new Point(442, ypoint));
                    baris += 1;
                    ypoint += (13 * 2);
                    if (baris == 11)
                    {
                        ypoint = ypoint + (12 * 5);
                    }
                }
            }

            _trx._saldo = saldo.ToString();
        }
        public string HistoriPrint(Transaksi trx)
        {
            bool result = false;
            printDoc = new PrintDocument();
            _trx = trx;
            //string printername = "EPSON L3150 Series";
            //string printername = "Canon MP280 series Printer";
            //string printername = "PsiPR-OLI";
            string printername = "Brother HL-L2360D series";
            //printDoc.PrinterSettings.PrinterName = "Canon MP280 series Printer";
            //printDoc.PrinterSettings.PrinterName = "PsiPR-OLI";
            //printDoc.PrinterSettings.PrinterName = "EPSON L3150 Series";
            printDoc.PrinterSettings.PrinterName = printername;

            printDoc.BeginPrint += new PrintEventHandler(BeginPrintEH);
            printDoc.EndPrint += new PrintEventHandler(EndPrintEH);
            printDoc.PrintPage += new PrintPageEventHandler(HistoriPrintPage);
            printDoc.Print();
            printServer.Printserver(printername);
            Console.WriteLine("Print Selesai ...");

            return _trx._saldo;
        }
        public void HistoriPrintPage(object sender, PrintPageEventArgs e)
        {
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            Graphics g = e.Graphics;
            Font font = new Font("Calibri", 8, FontStyle.Regular);

            string logo = "C:\\inputpassbook\\logo-banksulselbar-profil.jpg";
            Image img = Image.FromFile(logo);
            g.DrawImage(img, new Point(50, 30));

            string cabang = "Bank Sulselbar Cabang " + _trx._namacabang;
            string nasabah = _trx._nasabah;
            string alamat = _trx._alamatnasabah;

            g.DrawString("Nama Nasabah", new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(50, 130));
            g.DrawString("Alamat Nasabah", new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(50, 150));

            g.DrawString(":", new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(170, 130));
            g.DrawString(":", new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(170, 150));

            g.DrawString(cabang, new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(50, 110));
            g.DrawString(nasabah, new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(180, 130));
            g.DrawString(alamat, new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(180, 150));

            string periode = _trx._startdate + " s/d " + _trx._enddate;
            string norekening = _trx._rekening;
            string jumlah = _trx._jumlahtransaksi + " transaksi";

            g.DrawString("Periode", new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(350, 90));
            g.DrawString("Nomor Rekening", new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(350, 110));
            g.DrawString("Jenis Tabungan", new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(350, 130));
            g.DrawString("Jumlah Transaksi", new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(350, 150));

            g.DrawString(":", new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(470, 90));
            g.DrawString(":", new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(470, 110));
            g.DrawString(":", new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(470, 130));
            g.DrawString(":", new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(470, 150));

            g.DrawString(periode, new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(480, 90));
            g.DrawString(norekening, new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(480, 110));
            g.DrawString("Taplus", new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(480, 130));
            g.DrawString(jumlah, new Font("Arial", 10, FontStyle.Regular), blackBrush, new Point(480, 150));

            g.DrawString("Uraian", new Font("Arial", 12, FontStyle.Regular), blackBrush, new Point(50, 200));
            g.DrawString("Tipe", new Font("Arial", 12, FontStyle.Regular), blackBrush, new Point(350, 200));
            g.DrawString("Nominal", new Font("Arial", 12, FontStyle.Regular), blackBrush, new Point(450, 200));
            g.DrawString("Saldo", new Font("Arial", 12, FontStyle.Regular), blackBrush, new Point(600, 180));
            g.DrawString("Akhir", new Font("Arial", 12, FontStyle.Regular), blackBrush, new Point(600, 200));

            int saldo = int.Parse(_trx._saldo);
            int ypoint = 220;
            font = new Font("Arial", 10, FontStyle.Regular);
            for (int i = 0; i < _trx._uraian.Length; i++)
            {
                int ypoint1 = ypoint;
                int nominal = int.Parse(_trx._nominal[i]);
                if (_trx._uraian[i].Length >= 40)
                {
                    string[] arrayhsl = stringSplit(_trx._uraian[i]);
                    for (int j = 0; j < arrayhsl.Length; j++)
                    {
                        g.DrawString(arrayhsl[j], font, blackBrush, new Point(50, ypoint1));
                        ypoint1 += 20;
                    }
                }
                else
                {
                    g.DrawString(_trx._uraian[i], font, blackBrush, new Point(50, ypoint));
                    ypoint1 += 20;
                }
                g.DrawString(_trx._enddate, font, blackBrush, new Point(50, ypoint1));
                g.DrawString(_trx._hour, font, blackBrush, new Point(130, ypoint1));
                ypoint1 += 40;
                g.DrawString(_trx._tipe[i], font, blackBrush, new Point(350, ypoint));
                g.DrawString(nominal.ToString("N0"), font, blackBrush, new Point(450, ypoint));
                g.DrawString(saldo.ToString("N0"), font, blackBrush, new Point(600, ypoint));
                ypoint = ypoint1;
                if (_trx._tipe[i] == "D")
                    saldo -= nominal;
                else
                    saldo += nominal;
                //saldo += nominal;
            }
            ypoint += 10;
            string halaman = "halaman ke " + _trx._halaman + " dari " + _trx._maxhalaman;
            g.DrawString(halaman, new Font("Calibri", 10, FontStyle.Regular), blackBrush, new Point(600, ypoint));
            _trx._saldo = saldo.ToString();
        }
        public string[] stringSplit(string strtext)
        {
            //string result = ypoint;
            string[] strarray = strtext.Split().ToArray();
            string temp = string.Empty;
            int j = 0;
            int pos = 0;
            //int ypoint = 170;
            int n = strtext.Length / 40;
            if (strtext.Length % 40 > 0)
            {
                n += 1;
            }
            string[] arrayhasil = new string[n];
            string[] temparray = null;
            for (int i = 0; i < n; i++)
            {
                while (j < strarray.Length)
                {
                    if (j == 0)
                    {
                        temp = temp + strarray[j];
                        pos = j;
                    }
                    else
                    {
                        temp = temp + " " + strarray[j];
                        pos = j;
                    }
                    if (temp.Length < 40)
                    {
                        arrayhasil[i] = temp;
                    }
                    else
                    {
                        j += strarray.Length;
                    }
                    j++;
                }

                if ((strarray.Length - pos) != 0)
                    temparray = new string[strarray.Length - pos];
                else
                    temparray = new string[1];
                Array.Copy(strarray, pos, temparray, 0, strarray.Length - pos);
                strarray = temparray;
                pos = 0;
                j = 0;
                temp = string.Empty;
            }
            return arrayhasil;
        }
        public async Task ThermalPrint(Transaksi trx)
        {
            printDoc = new PrintDocument();
            _trx = trx;
            //string printername = "EPSON L3150 Series";
            string printername = "BT-T080(U) 1";

            printDoc.PrinterSettings.PrinterName = printername;
            //printDoc.BeginPrint += new PrintEventHandler(BeginPrintEH);
            //printDoc.EndPrint += new PrintEventHandler(EndPrintEH);
            printDoc.PrintPage += new PrintPageEventHandler(ThermalPrintPage);
            printServer.Printserver(printername);
            Console.WriteLine("Printing Selesai...");
            printDoc.Print();
        }
        public void ThermalPrintPage(object sender, PrintPageEventArgs e)
        {
            string logo = "C:\\inputpassbook\\logo-banksulselbar-profil-thermal.jpg";
            StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
            StringFormat formatCenter = new StringFormat(formatLeft);
            formatCenter.Alignment = StringAlignment.Center;
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            Graphics g = e.Graphics;
            font = new Font("Arial", 10, FontStyle.Regular);
            float point = 4;
            float sizex = 20;
            float sizey = point;
            float offset = 0;
            float lineheight = font.GetHeight() + point;
            SizeF layoutsize = new SizeF(280, lineheight);
            RectangleF layout = new RectangleF(new PointF(sizex, sizey + offset), layoutsize);
            Image img = Image.FromFile(logo);

            g.DrawImage(img, (e.PageBounds.Width - img.Width) / 2, 0, img.Width, img.Height);
            offset = img.Height + point;
            offset += lineheight;
            layout = new RectangleF(new PointF(sizex, sizey + offset), layoutsize);

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            date = "Date :" + date;
            string lokasi = _trx._namacabang;

            string joint = string.Empty;

            joint = "Lokasi : " + lokasi;
            g.DrawString(joint, font, blackBrush, layout, formatCenter);
            offset += lineheight;
            layout = new RectangleF(new PointF(sizex, sizey + offset), layoutsize);
            g.DrawString(date, font, blackBrush, layout, formatCenter);
            offset += lineheight;
            offset += lineheight;
            layout = new RectangleF(new PointF(sizex, sizey + offset), layoutsize);

            joint = "No Rekening : " + _trx._rekening;
            g.DrawString(joint, font, blackBrush, layout, formatLeft);
            offset += lineheight;
            layout = new RectangleF(new PointF(sizex, sizey + offset), layoutsize);
            joint = "Nama Nasabah : " + _trx._nasabah;
            g.DrawString(joint, font, blackBrush, layout, formatLeft);
            offset += lineheight;
            offset += lineheight;
            layout = new RectangleF(new PointF(sizex, sizey + offset), layoutsize);

            joint = string.Empty;


            int batastrx = _trx._tipe.Length - 10;
            int ypoint = 190;
            //string joint = string.Empty;
            int saldo = int.Parse(_trx._saldo);
            for (int i = batastrx; i < _trx._tipe.Length; i++)
            {
                int nominal = int.Parse(_trx._nominal[i]);
                Random rnd = new Random();
                if (_trx._tipe[i] == "D")
                {
                    saldo -= nominal;
                }
                else
                {
                    saldo += nominal;
                }
                string doublespasi = "  ";
                string nominalprint = nominal.ToString("N0");
                string saldoprint = saldo.ToString("N0");
                joint = joint + _trx._tanggal[i] + doublespasi + doublespasi + _trx._tipe[i] + doublespasi + doublespasi + nominalprint + doublespasi + doublespasi + saldoprint;
                g.DrawString(joint, font, blackBrush, layout, formatLeft);
                offset += lineheight;
                layout = new RectangleF(new PointF(sizex, sizey + offset), layoutsize);
                ypoint += 20;
                joint = string.Empty;
            }
        }
    }
}
