using System;

namespace TinhLuongNhanVien
{
    class NhanVien
    {
        private string _maNV;
        private string _hoTen;
        private decimal _luongCoBan;
        private int _soNgayLam;
        private int _soNgayNghiPhep;

        public NhanVien()
        {
            _maNV = "NV000";
            _hoTen = "Chưa cập nhật";
            _luongCoBan = 0;
            _soNgayLam = 0;
            _soNgayNghiPhep = 0;
        }

        public NhanVien(string maNV, string hoTen)
        {
            _maNV = maNV;
            HoTen = hoTen;
            _luongCoBan = 0;
            _soNgayLam = 0;
            _soNgayNghiPhep = 0;
        }

        public NhanVien(string maNV, string hoTen, decimal luongCoBan, int soNgayLam, int soNgayNghiPhep)
        {
            _maNV = maNV;
            HoTen = hoTen;
            LuongCoBan = luongCoBan;
            SoNgayLam = soNgayLam;
            _soNgayNghiPhep = soNgayNghiPhep;
        }

        public NhanVien(string maNV, string hoTen, decimal luong = 5_000_000, int soNgayLam = 26)
        {
            _maNV = maNV;
            HoTen = hoTen;
            LuongCoBan = luong;
            SoNgayLam = soNgayLam;
            _soNgayNghiPhep = 0;
        }

        // ---- Property ----
        public string MaNV
        {
            get { return _maNV; }
        }

        public string HoTen
        {
            get { return _hoTen; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Họ tên không được để trống!");
                _hoTen = value;
            }
        }

        public decimal LuongCoBan
        {
            get { return _luongCoBan; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Lương cơ bản không được âm!");
                _luongCoBan = value;
            }
        }

        public int SoNgayLam
        {
            get { return _soNgayLam; }
            set
            {
                if (value < 0 || value > 31)
                    throw new ArgumentException("Số ngày làm phải trong khoảng 0 - 31!");
                _soNgayLam = value;
            }
        }

        public int SoNgayNghiPhep
        {
            get { return _soNgayNghiPhep; }
            set { _soNgayNghiPhep = value; }
        }

        // Lương thực nhận: chỉ đọc, tính tự động
        public decimal LuongThucNhan
        {
            get
            {
                decimal luongTheoNgay = _luongCoBan / 26m * _soNgayLam;
                decimal khauTruBHXH = _luongCoBan * 0.08m;
                return luongTheoNgay - khauTruBHXH;
            }
        }

        // ---- Overload TinhThuong ----
        public decimal TinhThuong()
        {
            return 0;
        }

        public decimal TinhThuong(decimal heSo)
        {
            return _luongCoBan * heSo;
        }

        public decimal TinhThuong(decimal heSo, bool coPhucLoi)
        {
            decimal thuong = _luongCoBan * heSo;
            if (coPhucLoi)
                thuong += 500_000;
            return thuong;
        }

        public override string ToString()
        {
            return $"[{_maNV}] {_hoTen} - Lương CB: {_luongCoBan:N0}đ - Ngày làm: {_soNgayLam} - Lương thực nhận: {LuongThucNhan:N0}đ";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // NV1: dùng constructor chỉ mã + họ tên
            NhanVien nv1 = new NhanVien("NV001", "Nguyễn Văn An");

            // NV2: dùng constructor đầy đủ tham số
            NhanVien nv2 = new NhanVien("NV002", "Trần Thị Bình", 8_000_000, 24, 2);

            // NV3: dùng constructor Optional Parameters + Named Arguments
            NhanVien nv3 = new NhanVien(maNV: "NV003", hoTen: "Lê Văn Cường", soNgayLam: 20);

            Console.WriteLine("===== DANH SÁCH NHÂN VIÊN =====");
            Console.WriteLine(nv1);
            Console.WriteLine(nv2);
            Console.WriteLine(nv3);

            Console.WriteLine("\n===== SO SÁNH CÁC OVERLOAD TinhThuong() =====");
            foreach (var nv in new[] { nv1, nv2, nv3 })
            {
                decimal t1 = nv.TinhThuong();
                decimal t2 = nv.TinhThuong(0.1m);
                decimal t3 = nv.TinhThuong(0.1m, true);

                Console.WriteLine($"\nNhân viên: {nv.HoTen} ({nv.MaNV})");
                Console.WriteLine($"  TinhThuong()                 = {t1:N0}đ");
                Console.WriteLine($"  TinhThuong(0.1m)              = {t2:N0}đ");
                Console.WriteLine($"  TinhThuong(0.1m, true)        = {t3:N0}đ (đã cộng phúc lợi)");
            }
        }
    }
}