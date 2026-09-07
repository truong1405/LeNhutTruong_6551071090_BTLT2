using System;
using System.Collections.Generic;

namespace QuanLySanPhamCuaHang
{
    class SanPham
    {
        private string _maSP;
        private string _tenSP;
        private decimal _gia;
        private int _soLuongTon;

        public SanPham(string maSP, string tenSP, decimal gia, int soLuongTon)
        {
            _maSP = maSP;
            _tenSP = tenSP;
            _gia = gia;
            _soLuongTon = soLuongTon;
        }

        public string MaSP
        {
            get { return _maSP; }
            set { _maSP = value; }
        }

        public string TenSP
        {
            get { return _tenSP; }
            set { _tenSP = value; }
        }

        public decimal Gia
        {
            get { return _gia; }
            set { _gia = value; }
        }

        public int SoLuongTon
        {
            get { return _soLuongTon; }
            set { _soLuongTon = value; }
        }
        public virtual decimal TinhGiaBan()
        {
            return _gia;
        }

        public virtual string MoTa()
        {
            return $"[Sản phẩm] {_tenSP} (Mã: {_maSP}) - Giá gốc: {_gia:N0}đ - Tồn: {_soLuongTon}";
        }
    }

    // ---------------- SanPhamThucPham ----------------
    class SanPhamThucPham : SanPham
    {
        private DateTime _ngayHetHan;
        private int _nhietDoBaoQuan;

        public SanPhamThucPham(string maSP, string tenSP, decimal gia, int soLuongTon,
                                DateTime ngayHetHan, int nhietDoBaoQuan)
            : base(maSP, tenSP, gia, soLuongTon)
        {
            _ngayHetHan = ngayHetHan;
            _nhietDoBaoQuan = nhietDoBaoQuan;
        }

        public DateTime NgayHetHan
        {
            get { return _ngayHetHan; }
            set { _ngayHetHan = value; }
        }

        public int NhietDoBaoQuan
        {
            get { return _nhietDoBaoQuan; }
            set { _nhietDoBaoQuan = value; }
        }

        public override decimal TinhGiaBan()
        {
            int soNgayConLai = (_ngayHetHan.Date - DateTime.Now.Date).Days;
            if (soNgayConLai <= 3)
            {
                // Còn <= 3 ngày hết hạn => giảm 30%
                return Gia * 0.7m;
            }
            return Gia;
        }

        public override string MoTa()
        {
            int soNgayConLai = (_ngayHetHan.Date - DateTime.Now.Date).Days;
            string canhBao = soNgayConLai <= 3 ? " -> SẮP HẾT HẠN, GIẢM GIÁ 30%!" : "";
            return $"[Thực phẩm] {TenSP} (Mã: {MaSP}) - HSD: {_ngayHetHan:dd/MM/yyyy} " +
                   $"- Bảo quản: {_nhietDoBaoQuan}°C - Giá bán: {TinhGiaBan():N0}đ{canhBao}";
        }
    }

    // ---------------- SanPhamDienTu ----------------
    class SanPhamDienTu : SanPham
    {
        private int _baoHanhThang;
        private string _hangSanXuat;

        public SanPhamDienTu(string maSP, string tenSP, decimal gia, int soLuongTon,
                              int baoHanhThang, string hangSanXuat)
            : base(maSP, tenSP, gia, soLuongTon)
        {
            _baoHanhThang = baoHanhThang;
            _hangSanXuat = hangSanXuat;
        }

        public int BaoHanhThang
        {
            get { return _baoHanhThang; }
            set { _baoHanhThang = value; }
        }

        public string HangSanXuat
        {
            get { return _hangSanXuat; }
            set { _hangSanXuat = value; }
        }

        public override decimal TinhGiaBan()
        {
            if (_baoHanhThang > 12)
            {
                // Cộng thêm 10% phí bảo hành mở rộng
                return Gia * 1.1m;
            }
            return Gia;
        }

        public override string MoTa()
        {
            return $"[Điện tử] {TenSP} (Mã: {MaSP}) - Hãng: {_hangSanXuat} " +
                   $"- Bảo hành: {_baoHanhThang} tháng - Giá bán: {TinhGiaBan():N0}đ";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<SanPham> danhSach = new List<SanPham>
            {
                new SanPhamThucPham("TP001", "Sữa tươi Vinamilk", 30000, 50,
                    DateTime.Now.AddDays(2), 5),           // sắp hết hạn -> giảm giá
                new SanPhamThucPham("TP002", "Bánh mì sandwich", 15000, 30,
                    DateTime.Now.AddDays(10), 20),         // còn hạn dài
                new SanPhamDienTu("DT001", "Tai nghe Bluetooth", 500000, 20,
                    18, "Sony"),                            // bảo hành > 12 tháng
                new SanPhamDienTu("DT002", "Chuột không dây", 200000, 40,
                    6, "Logitech"),                         // bảo hành <= 12 tháng
                new SanPham("SP001", "Sản phẩm chung chung", 100000, 10)
            };

            Console.WriteLine("===== DANH SÁCH SẢN PHẨM (ĐA HÌNH) =====\n");

            decimal tongGiaTriKho = 0;
            foreach (SanPham sp in danhSach)
            {
                Console.WriteLine(sp.MoTa());
                decimal giaBan = sp.TinhGiaBan();
                tongGiaTriKho += giaBan * sp.SoLuongTon;
            }

            Console.WriteLine("\n===== TỔNG GIÁ TRỊ KHO HÀNG =====");
            Console.WriteLine($"Tổng giá trị kho (theo giá bán thực tế): {tongGiaTriKho:N0}đ");
        }
    }
}