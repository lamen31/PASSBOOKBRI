using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace PASSBOOKBRI.Data
{
    public class Transaksi
    {
        //Data Masukan Passbook
        public string _pathA4 = "C:\\inputpassbook\\input histori a4.csv"; 
        public string _pathpassbook = "C:\\inputpassbook\\input passbook bni.csv";
        public string _pathThermal = "C:\\inputpassbook\\input thermal.csv";
        public string _namacabang { get; set; }
        public string _rekening { get; set; }
        public string _nasabah { get; set; }
        public string _alamatnasabah { get; set; }
        public string _jenisperiode { get; set; }
        public string[] _uraian { get; set; }
        public string _jumlahtransaksi { get; set; }
        public string[] _tipe { get; set; }
        public string[] _sandi { get; set; }
        public string[] _nominal { get; set; }
        public string _saldo { get; set; }
        public string[] _pengesahan { get; set; }
        public string _baris { get; set; }
        public string _startdate { get; set; }
        public string _enddate { get; set; }
        public string _hour { get; set; }
        public int _sisatransaksi { get; set; }
        public string _halaman { get; set; }
        public string _maxhalaman { get; set; }
        public string[] _tanggal { get; set; }
        public bool _isBaris { get; set; }
        public bool _isVisible { get; set; }
        public bool _norek { get; set; }
        public bool _flag { get; set; }

        public void setcabang(string strnamacabang)
        {
            _namacabang = strnamacabang;
        }
        public void setNasabah(string strnasabah, string strrekening, string alamatnasabah)
        {
            _nasabah = strnasabah;
            _rekening = strrekening;
            _alamatnasabah = alamatnasabah;
        }
        public void setTransaksiPassbook(string strbaris, string strhalaman, string strtanggal, string[] strtipe, string[] strsandi, string[] strnominal, string strsaldo, string[] strpengesahan)
        {
            _baris = strbaris;
            _halaman = strhalaman;
            _startdate = strtanggal;
            _tipe = strtipe;
            _sandi = strsandi;
            _nominal = strnominal;
            _saldo = strsaldo;
            _pengesahan = strpengesahan;
            //_debet = strdebet;
            //_kredit = strkredit;
        }
        public void setTransaksiHistori(string strjumlahtransaksi, string[] struraian, string[] strtipe, string[] strnominal)
        {
            _jumlahtransaksi = strjumlahtransaksi;
            _uraian = struraian;
            _tipe = strtipe;
            _nominal = strnominal;
        }
        public void setTransaksiHistori(string strperiodeprint, string strstartdate, string strenddate)
        {
            _jenisperiode = strperiodeprint;
            _startdate = strstartdate;
            _enddate = strenddate;
        }
        public void setTransaksiHistori(string strsaldo, string strhour)
        {
            _saldo = strsaldo;
            _hour = strhour;
        }
        public void setTransaksiHistori(string[] struraian, string[] strtipe, string[] strnominal, string strhalaman)
        {
            _uraian = struraian;
            _tipe = strtipe;
            _nominal = strnominal;
            _halaman = strhalaman;
        }
        public void setModal(bool bolisbaris, bool bolisvisible)
        {
            _isBaris = bolisbaris;
            _isVisible = bolisvisible;
        }
        public void setModalBaris(bool bolisbaris)
        {
            _isBaris = bolisbaris;
        }
        public void setModalVisible(bool bolisvisible)
        {
            _isVisible = bolisvisible;
        }
        public void setSisaTransaksi(int intsisatransaksi)
        {
            _sisatransaksi = intsisatransaksi;
        }
        public void setMaxHalaman(string strmaxhalaman)
        {
            _maxhalaman = strmaxhalaman;
        }
        public void setFlag(bool bolflag)
        {
            _flag = bolflag;
        }
        public void setPrintThermal(string[] strtanggal, string[] strkode, string[] strnominal, string strsaldo)
        {
            _tanggal = strtanggal;
            _nominal = strnominal;
            _tipe = strkode;
            _saldo = strsaldo;
        }
        public void clear()
        {
            _namacabang = string.Empty;
            _rekening = string.Empty;
            _nasabah = string.Empty;
            _alamatnasabah = string.Empty;
            _jenisperiode = string.Empty;
            _jumlahtransaksi = string.Empty;
            _saldo = string.Empty;
            _startdate = string.Empty;
            _enddate = string.Empty;
            _hour = string.Empty;
            _maxhalaman = string.Empty;
        }
        public void clearArray()
        {
            _uraian = null;
            _tipe = null;
            _nominal = null;
        }
    }
}
