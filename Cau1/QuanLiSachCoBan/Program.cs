using System;

namespace QuanLiSachCoBan
{
    class Sach
    {
        private string _maSach;
        private string _tenSach;
        private string _tacGia;
        private int _namXuatBan;
        private double _giaBan;

        public Sach(string maSach, string tenSach, string tacGia, int namXuatBan, double giaBan)
        {
            _maSach = maSach;
            _tenSach = tenSach;
            _tacGia = tacGia;
            NamXuatBan = namXuatBan;   
            TenSach = tenSach;
            _giaBan = giaBan;
        }

        public Sach()
        {
            _maSach = "SACH000";
            _tenSach = "Chưa có tên";
            _tacGia = "Chưa rõ tác giả";
            _namXuatBan = DateTime.Now.Year;
            _giaBan = 0;
        }

        // ---- Property ----
        public string MaSach
        {
            get { return _maSach; }
        }

        public string TenSach
        {
            get { return _tenSach; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Tên sách không được để trống!");
                _tenSach = value;
            }
        }

        public string TacGia
        {
            get { return _tacGia; }
            set { _tacGia = value; }
        }

        public int NamXuatBan
        {
            get { return _namXuatBan; }
            set
            {
                int namHienTai = DateTime.Now.Year;
                if (value < 1900 || value > namHienTai)
                    throw new ArgumentOutOfRangeException(
                        nameof(value), $"Năm xuất bản phải trong khoảng 1900 - {namHienTai}!");
                _namXuatBan = value;
            }
        }

        public double GiaBan
        {
            get { return _giaBan; }
        }

        public void CapNhatGia(double giaMoi)
        {
            if (giaMoi < 0)
                throw new ArgumentException("Giá bán không được âm!");
            _giaBan = giaMoi;
        }

        public void HienThiThongTin()
        {
            Console.WriteLine("┌──────────────────────────────────────────┐");
            Console.WriteLine($"│ Mã sách    : {_maSach}");
            Console.WriteLine($"│ Tên sách   : {_tenSach}");
            Console.WriteLine($"│ Tác giả    : {_tacGia}");
            Console.WriteLine($"│ Năm XB     : {_namXuatBan}");
            Console.WriteLine($"│ Giá bán    : {_giaBan:N0} đ");
            Console.WriteLine("└──────────────────────────────────────────┘");
        }

        public override string ToString()
        {
            return $"[{_maSach}] \"{_tenSach}\" - {_tacGia} ({_namXuatBan}) - {_giaBan:N0}đ";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Cách 1:
            Sach sach1 = new Sach("S001", "Lập trình C# căn bản", "Nguyễn Văn A", 2020, 120000);

            // Cách 2:
            Sach sach2 = new Sach();
            sach2.TenSach = "Cấu trúc dữ liệu và giải thuật";
            sach2.TacGia = "Trần Thị B";
            sach2.NamXuatBan = 2019;
            sach2.CapNhatGia(95000);

            // Cách 3:
            Sach sach3 = new Sach("S003", "Lập trình hướng đối tượng", "Lê Văn C", 2022, 150000)
            {
                TenSach = "Lập trình hướng đối tượng với C#"
            };

            Console.WriteLine("===== THÔNG TIN CÁC SÁCH =====\n");
            sach1.HienThiThongTin();
            Console.WriteLine();
            sach2.HienThiThongTin();
            Console.WriteLine();
            sach3.HienThiThongTin();

            Console.WriteLine("\n===== ToString() =====");
            Console.WriteLine(sach1);
            Console.WriteLine(sach2);
            Console.WriteLine(sach3);

            Console.WriteLine("\n===== THỬ GÁN NĂM XUẤT BẢN KHÔNG HỢP LỆ =====");
            try
            {
                sach1.NamXuatBan = 1850;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            try
            {
                sach2.NamXuatBan = 3000; 
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }
    }
}