using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

internal class Program
{
    public struct HoaDon
    {
        public string MaHD { get; set; }
        public DateTime NgayPH { get; set; }
        public double TongTien { get; set; }
        public double SoTienNo { get; set; }
        public int TrangThaiNo { get; set; }
        public string TenKH { get; set; }

        public HoaDon(string maHoaDon, DateTime ngayPhatHanh, double tongTien, double soTienNo, int trangThaiNo, string tenKhachHang)
        {
            MaHD = maHoaDon;
            NgayPH = ngayPhatHanh;
            TongTien = tongTien;
            SoTienNo = soTienNo;
            TrangThaiNo = trangThaiNo;
            TenKH = tenKhachHang;
        }

        public bool Check()
        {
            if (string.IsNullOrEmpty(MaHD))
            {
                Console.WriteLine("Mã hóa đơn không được để trống !!!!");
                return false;
            }
            if (NgayPH > DateTime.Now)
            {
                Console.WriteLine("Ngày phát hành không được lớn hơn ngày hiện tại !!!!");
                return false;
            }
            if (TongTien <= 0)
            {
                Console.WriteLine("Tổng tiền phải lớn hơn 0 !!!!");
                return false;
            }
            if (SoTienNo < 0)
            {
                Console.WriteLine("Số tiền còn nợ không được nhỏ hơn 0 !!!!");
                return false;
            }
            if (TrangThaiNo < 0)
            {
                Console.WriteLine("Số tiền còn nợ không được nhỏ hơn 0 !!!!");
                return false;
            }
            if (string.IsNullOrEmpty(TenKH))
            {
                Console.WriteLine("Tên khách hàng không được bỏ trống !!!!!");
                return false;
            }
            return true;
        }
        public void XoaNo()
        {
            Console.WriteLine();
            Console.WriteLine("Xóa nợ cho hóa đơn: " + MaHD);
            SoTienNo = 0;
            TrangThaiNo = 0;
            Console.WriteLine("Đã xóa nợ thành công. Số tiền nợ hiện tại: " + SoTienNo);
            Console.WriteLine("------------------------------------------");

        }
    }

    public static void NhapDS(List<HoaDon> ds)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Write("Nhập số lượng hóa đơn: ");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.WriteLine("Số lượng hóa đơn phải là số nguyên dương. Vui lòng nhập lại !!!! ");
        }

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Nhập thông tin cho hóa đơn thứ: " + (i + 1));
            Console.Write("Mã hóa đơn: ");
            string MaHD = Console.ReadLine();
            Console.Write("Ngày phát hành dd/MM/yyyy: ");
            DateTime NgayPH;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out NgayPH))
            {
                Console.Write("Ngày phát hành không hợp lệ. Vui lòng nhập lại theo định dạng dd/MM/yyyy: ");
            }
            Console.Write("Tổng số tiền: ");
            double TongTien;
            while (!double.TryParse(Console.ReadLine(), out TongTien) || TongTien <= 0)
            {
                Console.WriteLine("Tổng số tiền phải là một số dương. Vui lòng nhập lại:");
            }
            Console.Write("Số tiền còn nợ: ");
            double SoTienNo;
            while (!double.TryParse(Console.ReadLine(), out SoTienNo))
            {
                Console.WriteLine("Số tiền còn nợ phải là một số. Vui lòng nhập lại:");
            }
            Console.Write("Trạng thái nợ: ");
            int TrangThaiNo;
            while (!int.TryParse(Console.ReadLine(), out TrangThaiNo) || TrangThaiNo < 0)
            {
                Console.WriteLine("Trạng thái nợ phải là một số không âm. Vui lòng nhập lại:");
            }
            Console.Write("Tên khách hàng: ");
            string TenKH = Console.ReadLine();

            HoaDon x = new HoaDon(MaHD, NgayPH, TongTien, SoTienNo, TrangThaiNo, TenKH);

            if (x.Check())
            {
                ds.Add(x);
            }
            else
            {
                Console.WriteLine("Thông tin không hợp lệ. Vui lòng nhập lại !!!");
                i--;
            }
        }
    }
    public static void XoaNoChoHoaDon(List<HoaDon> ds, string maHoaDon)
    {
        for (int i = 0; i < ds.Count; i++)
        {
            if (ds[i].MaHD == maHoaDon)
            {
                ds[i].XoaNo();
                return;
            }
        }
        Console.WriteLine("Không tìm thấy hóa đơn có mã " + maHoaDon);
    }
    public static void XuatDanhSachHoaDon(List<HoaDon> ds, string maHoaDon = null, bool? conNo = null)
    {
        Console.WriteLine("\t\t\tDanh sách hóa đơn");

        for (int i = 0; i < ds.Count; i++)
        {
            var hoaDon = ds[i];

            if ((maHoaDon == null || hoaDon.MaHD == maHoaDon) && (conNo == null || (conNo == true && hoaDon.SoTienNo > 0) || (conNo == false && hoaDon.SoTienNo == 0)))
            {
               
                Console.WriteLine("Mã hóa đơn: " + hoaDon.MaHD);
                Console.WriteLine("Ngày phát hành: " + hoaDon.NgayPH.ToString("dd/MM/yyyy"));
                Console.WriteLine("Tổng tiền: " + hoaDon.TongTien);
                Console.WriteLine("Số tiền nợ: " + hoaDon.SoTienNo);
                Console.WriteLine("Trạng thái nợ: " + (hoaDon.TrangThaiNo == 0 ? "Hết nợ" : "Còn nợ"));
                Console.WriteLine("Tên khách hàng: " + hoaDon.TenKH);
                Console.WriteLine("------------------------------------------");
            }
        }
    }
    public static void HienThiHoaDonConNoTheoMoc(List<HoaDon> ds)
    {
        Console.WriteLine("\t\t\tDanh sách hóa đơn còn nợ theo các mốc thời gian");
        DateTime ngayHienTai = DateTime.Now;
        int[] mocThoiGian = { 30, 60, 90 };

        for (int j = 0; j < ds.Count; j++)
        {
            var hoaDon = ds[j];
            TimeSpan thoiGianDaQua = ngayHienTai - hoaDon.NgayPH;

            for (int i = 0; i < mocThoiGian.Length; i++)
            {
                int moc = mocThoiGian[i];
                if (hoaDon.SoTienNo > 0 && thoiGianDaQua.Days == moc)
                {
                    Console.WriteLine($"Mốc {moc} ngày:");
                    Console.WriteLine("Mã hóa đơn: " + hoaDon.MaHD);
                    Console.WriteLine("Ngày phát hành: " + hoaDon.NgayPH.ToString("dd/MM/yyyy"));
                    Console.WriteLine("Số tiền nợ: " + hoaDon.SoTienNo);
                    Console.WriteLine("Số ngày trôi qua: " + thoiGianDaQua.Days);
                    Console.WriteLine("------------------------------------------");
                    break;
                }
            }
        }
    }
    public static void XuatDanhSachHoaDonKhongConNo(List<HoaDon> ds, string tenFile, int soLuongHoaDon)
    {
        using (StreamWriter writer = new StreamWriter(tenFile))
        {
            int hoaDonDaXuat = 0;
            for (int i = 0; i < ds.Count; i++)
            {
                if (ds[i].SoTienNo == 0)
                {
                    writer.WriteLine("Mã hóa đơn: " + ds[i].MaHD);
                    writer.WriteLine("Ngày phát hành: " + ds[i].NgayPH.ToString("dd/MM/yyyy"));
                    writer.WriteLine("Tổng tiền: " + ds[i].TongTien);
                    writer.WriteLine("Tên khách hàng: " + ds[i].TenKH);
                    writer.WriteLine("------------------------------------------");
                    hoaDonDaXuat++;
                    if (hoaDonDaXuat >= soLuongHoaDon)
                    {
                        break;
                    }
                }
            }
        }

        Console.WriteLine($"Đã xuất {soLuongHoaDon} hóa đơn không còn nợ ra file {tenFile} thành công!");
    }
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        List<HoaDon> ds = new List<HoaDon>();
        bool thoat = false;

        while (!thoat)
        {
            Console.WriteLine("======= MENU =======");
            Console.WriteLine("1. Nhập danh sách hóa đơn");
            Console.WriteLine("2. Xóa nợ cho 1 hóa đơn");
            Console.WriteLine("3. Hiển thị danh sách hóa đơn");
            Console.WriteLine("4. Hiển thị các hóa đơn đang còn nợ theo các mốc");
            Console.WriteLine("5. Xuất danh sách hóa đơn không còn nợ ra file text ");
            Console.WriteLine("6. Thoát");
            Console.Write("Nhập lựa chọn của bạn: ");

            string luaChon = Console.ReadLine();

            switch (luaChon)
            {
                case "1":
                    NhapDS(ds);
                    break;
                case "2":
                    Console.Write("Nhập mã hóa đơn cần xóa nợ: ");
                    string maHoaDon = Console.ReadLine();
                    XoaNoChoHoaDon(ds, maHoaDon);
                    break;
                case "3":
                    XuatDanhSachHoaDon(ds);
                    break;
                case "4":
                    HienThiHoaDonConNoTheoMoc(ds);
                    break;
                case "5":
                    Console.Write("Nhập tên file xuất (bao gồm cả đường dẫn): ");
                    string tenFile = Console.ReadLine();
                    Console.Write("Nhập số lượng hóa đơn không còn nợ cần xuất: ");
                    int soLuong;
                    while (!int.TryParse(Console.ReadLine(), out soLuong) || soLuong <= 0)
                    {
                        Console.WriteLine("Số lượng phải là một số nguyên dương. Vui lòng nhập lại:");
                    }
                    XuatDanhSachHoaDonKhongConNo(ds, tenFile, soLuong);
                    break;
                case "6":
                    thoat = true;
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
