using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    //BỎ PHẦN NÀY
    public class MonHocSinhVienDangKiBusiness : BaseBusiness
    {
        public List<MonHocSinhVienDangKiDTO> LayMonHocSinhVienDangKi(int idkhoadt, int idaccount, int idhocki,int idlophoc)
        {
            try
            {
                var ketquasaukhidangki = model.MonHocSinhVienDangKis.Where(s => s.IDAccount == idaccount && s.IDHocKi == idhocki).Select(s => new MonHocSinhVienDangKiDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    IDHocPhanTienQuyet = s.HocPhanTienQuyet.ID,
                    IDMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.ID,
                    TenMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.TenMonHoc,

                    IDHocPhanHocTruoc = s.HocPhanHocTruoc.ID,
                    IDMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.ID,
                    TenMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.TenMonHoc,

                    LoaiDangKi = s.LoaiDangKi,
                    TrangThai = s.TrangThai,
                    IDHocKi = s.IDHocKi,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                }).ToList();
                var monhocchuongtrinhdaotao_loaibatbuoc = model.MonHocKhoaDaoTaos.Where(s => s.IDHocKi == idhocki && s.IDKhoaDaoTao == idkhoadt && s.IDPhanLoaiMonHoc == 1).Select(s => new MonHocSinhVienDangKiDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    IDHocPhanTienQuyet = s.HocPhanTienQuyet.ID,
                    IDMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.ID,
                    TenMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.TenMonHoc,

                    IDHocPhanHocTruoc = s.HocPhanHocTruoc.ID,
                    IDMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.ID,
                    TenMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.TenMonHoc,

                    LoaiDangKi = 1,
                    TrangThai = false,
                    IDHocKi = s.IDHocKi,
                    IDAccount = idaccount,
                    IDLopHoc = idlophoc,
                    IDKhoaDaoTao = idkhoadt,
                    ChoPhepDangKi = 0
                }).ToList();
                var monhocketquahoctap = model.KetQuaHocTaps.Where(s => s.IDAccount == idaccount && s.KetQua == false).Select(s => new MonHocSinhVienDangKiDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.IDKhoaBoMon,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    IDHocPhanTienQuyet = s.HocPhanTienQuyet.ID,
                    IDMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.ID,
                    TenMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.TenMonHoc,

                    IDHocPhanHocTruoc = s.HocPhanHocTruoc.ID,
                    IDMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.ID,
                    TenMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.TenMonHoc,

                    Diem = s.Diem,

                    LoaiDangKi = 3,
                    TrangThai = false,
                    IDHocKi = idhocki,
                    IDAccount = s.IDAccount,
                    IDLopHoc = idlophoc,
                    IDKhoaDaoTao = idkhoadt,
                }).ToList();

                foreach (var item in monhocchuongtrinhdaotao_loaibatbuoc)
                {
                    var idmonhocTQ = model.HocPhanTienQuyets.Where(s => s.IDMonHoc == item.IDMonHoc).Select(s => s.IDMonHocTienQuyet).FirstOrDefault();
                    if (idmonhocTQ > 0)
                    {

                        var ketquahoctap = model.KetQuaHocTaps.Where(s => s.IDMonHoc == idmonhocTQ).Select(s => s.Diem).FirstOrDefault();
                        item.ChoPhepDangKi = ketquahoctap < 4 ? 1 : 2;
                    }
                }


                var monhocchuongtrinhdaotao_loaituchon = model.MonHocKhoaDaoTaos.Where(s => s.IDHocKi == idhocki && s.IDKhoaDaoTao == idkhoadt && s.IDPhanLoaiMonHoc == 2).Select(s => new MonHocSinhVienDangKiDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    IDHocPhanTienQuyet = s.HocPhanTienQuyet.ID,
                    IDMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.ID,
                    TenMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.TenMonHoc,

                    IDHocPhanHocTruoc = s.HocPhanHocTruoc.ID,
                    IDMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.ID,
                    TenMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.TenMonHoc,

                    LoaiDangKi = 2,
                    TrangThai = false,
                    IDHocKi = s.IDHocKi,
                    IDAccount = idaccount,
                    IDLopHoc = idlophoc,
                    IDKhoaDaoTao = idkhoadt,
                    ChoPhepDangKi = 0

                }).ToList();
                foreach (var item in monhocchuongtrinhdaotao_loaituchon)
                {
                    var idmonhocTQ = model.HocPhanTienQuyets.Where(s => s.IDMonHoc == item.IDMonHoc).Select(s => s.IDMonHocTienQuyet).FirstOrDefault();
                    if (idmonhocTQ > 0)
                    {
                        var ketquahoctap = model.KetQuaHocTaps.Where(s => s.IDMonHoc == idmonhocTQ).Select(s => s.Diem).FirstOrDefault();
                        item.ChoPhepDangKi = ketquahoctap < 4 ? 1 : 2;

                    }
                }


                //Lay mon bat buoc,tu chon,mon hoc chua dat ket qua
                var result = new List<MonHocSinhVienDangKiDTO>();
                result.AddRange(monhocchuongtrinhdaotao_loaibatbuoc);
                result.AddRange(monhocchuongtrinhdaotao_loaituchon);
                result.AddRange(monhocketquahoctap);

                //Lay mon hoc vuot
                if (ketquasaukhidangki.Count() > 0)
                {
                    foreach (var item1 in ketquasaukhidangki)
                    {
                        var temp = result.Where(s => s.IDMonHoc == item1.IDMonHoc).FirstOrDefault();
                        if (temp == null)
                        {
                            result.Add(item1);
                        }
                        else
                        {
                            foreach (var item2 in result)
                            {
                                if (item2.IDMonHoc == item1.IDMonHoc)
                                {
                                    item2.TrangThai = item1.TrangThai;
                                }
                            }
                        }

                    }


                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Them_MonHocVuotVaoDanhSach(MonHocSinhVienDangKiDTO item)
        {
            try
            {
                var kiemtramonhoc = model.MonHocSinhVienDangKis.Where(s => s.IDHocKi == item.IDHocKi && s.IDAccount == item.IDAccount && s.IDMonHoc == item.IDMonHoc && s.LoaiDangKi == item.LoaiDangKi).FirstOrDefault();
                if (kiemtramonhoc != null)
                {
                    kiemtramonhoc.TrangThai = item.TrangThai;
                }
                else
                {
                    var newItem = new MonHocSinhVienDangKi();
                    newItem.IDMonHoc = item.IDMonHoc;
                    newItem.IDAccount = item.IDAccount;
                    newItem.IDHocKi = item.IDHocKi;
                    newItem.IDLopHoc = item.IDLopHoc;
                    newItem.IDKhoaDaoTao = item.IDKhoaDaoTao;
                    newItem.IDHocPhanTienQuyet = item.IDHocPhanTienQuyet;
                    newItem.IDHocPhanHocTruoc = item.IDHocPhanHocTruoc;
                    newItem.LoaiDangKi = item.LoaiDangKi;
                    newItem.TrangThai = item.TrangThai;

                    model.MonHocSinhVienDangKis.Add(newItem);

                }

                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MonHocSinhVienDangKiDTO> LayLichSuDangKi(int idaccount)
        {
            try
            {
                var lichsudangki = model.MonHocSinhVienDangKis.Where(s => s.IDAccount == idaccount && s.TrangThai == true).Select(s => new MonHocSinhVienDangKiDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.IDKhoaBoMon,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    IDHocPhanTienQuyet = s.HocPhanTienQuyet.ID,
                    IDMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.ID,
                    TenMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.TenMonHoc,

                    IDHocPhanHocTruoc = s.HocPhanHocTruoc.ID,
                    IDMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.ID,
                    TenMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.TenMonHoc,

                    LoaiDangKi = s.LoaiDangKi,
                    TrangThai = s.TrangThai,
                    IDHocKi = s.IDHocKi,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao
                }).ToList();
                return lichsudangki;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int CheckMonHocVuotHocKiTruoc(int idMonhoc, int idAcount)
        {
            try
            {
                return model.MonHocSinhVienDangKis.Where(s => s.IDAccount == idAcount && s.IDMonHoc == idMonhoc && s.TrangThai == true).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}