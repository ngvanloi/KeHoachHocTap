using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class TrangThaiDangKiMonHocBusiness : BaseBusiness
    {
        public TrangThaiDangKiMonHocDTO LayTrangThaiDangKiMonHoc(int id)
        {
            try
            {
                var trangthai = model.TrangThaiDangKiMonHocs.Where(s => s.ID == id).Select(s => new TrangThaiDangKiMonHocDTO
                {
                    ID = s.ID,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    IDHocKi = s.IDHocKi,
                    ThoiGianBatDau = s.ThoiGianBatDau,
                    ThoiGianKetThuc = s.ThoiGianKetThuc,
                    TrangThai = s.TrangThai,
                    GhiChu = s.GhiChu
                }).FirstOrDefault();
                return trangthai;
            }catch(Exception)
            {
                throw ;
            }
        }      

        public List<TrangThaiDangKiMonHocDTO> LayDanhSachTrangThaiDangKiMonHoc()
        {
            try
            {
                var lsttrangthai = model.TrangThaiDangKiMonHocs.Select(s => new TrangThaiDangKiMonHocDTO
                {
                    ID = s.ID,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    IDHocKi = s.IDHocKi,
                    ThoiGianBatDau = s.ThoiGianBatDau,
                    ThoiGianKetThuc = s.ThoiGianKetThuc,
                    TrangThai = s.TrangThai,
                    GhiChu = s.GhiChu
                }).ToList();
                return lsttrangthai;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ThemTrangThaiDangKiMonHoc(TrangThaiDangKiMonHocDTO trangthai)
        {
            try
            {
                var newtrangthai = new TrangThaiDangKiMonHoc();
                newtrangthai.ID = trangthai.ID;
                newtrangthai.IDKhoaDaoTao = trangthai.IDKhoaDaoTao;
                newtrangthai.IDHocKi = trangthai.IDHocKi;
                newtrangthai.ThoiGianBatDau = trangthai.ThoiGianBatDau;
                newtrangthai.ThoiGianKetThuc = trangthai.ThoiGianKetThuc;
                newtrangthai.TrangThai = trangthai.TrangThai;
                newtrangthai.GhiChu = trangthai.GhiChu;

                model.TrangThaiDangKiMonHocs.Add(newtrangthai);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool XoaTrangThaiDangKiMonHoc(int id)
        {
            try
            {
                var trangthai = model.TrangThaiDangKiMonHocs.Where(s => s.ID == id).FirstOrDefault();
                model.TrangThaiDangKiMonHocs.Remove(trangthai);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool SuaTrangThaiDangKiMonHoc(TrangThaiDangKiMonHocDTO trangthai)
        {
            try
            {
                var trangthais = model.TrangThaiDangKiMonHocs.Where(s => s.ID == trangthai.ID).FirstOrDefault();
                trangthais.ID = trangthai.ID;
                trangthais.IDKhoaDaoTao = trangthai.IDKhoaDaoTao;
                trangthais.IDHocKi = trangthai.IDHocKi;
                trangthais.ThoiGianBatDau = trangthai.ThoiGianBatDau;
                trangthais.ThoiGianKetThuc = trangthai.ThoiGianKetThuc;
                trangthais.TrangThai = trangthai.TrangThai;
                trangthais.GhiChu = trangthai.GhiChu;

                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Mo_TrangThaiDangKiMonHoc(int idKhoaDT,int idHocKi)
        {
            try
            {
                var trangthais = model.TrangThaiDangKiMonHocs.Where(s => s.IDKhoaDaoTao == idKhoaDT && s.IDHocKi == idHocKi).FirstOrDefault();

                var result = false;
                if(trangthais != null)
                {
                    var thoigianbatdau = Convert.ToDateTime(trangthais.ThoiGianBatDau);
                    var thoigianketthuc = Convert.ToDateTime(trangthais.ThoiGianKetThuc);
                    var thoigianhientai = DateTime.Now;

                    if(thoigianbatdau <= thoigianhientai && thoigianhientai <= thoigianketthuc)
                    {
                        trangthais.TrangThai = true;
                    }
                    else
                    {
                        trangthais.TrangThai = false;
                    }
                    result = trangthais.TrangThai;
                }
                model.SaveChanges();
                return result;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Check_TrangThaiMonHocQuaThoiGianDangKi(int idKhoaDT,int idHocKi)
        {
            try
            {
                var trangthais = model.TrangThaiDangKiMonHocs.Where(s => s.IDKhoaDaoTao == idKhoaDT && s.IDHocKi == idHocKi).FirstOrDefault();
                var result = false;
                if(trangthais != null)
                {
                    var thoigianbatdau = Convert.ToDateTime(trangthais.ThoiGianBatDau);
                    var thoigianketthuc = Convert.ToDateTime(trangthais.ThoiGianKetThuc);
                    var thoigianhientai = DateTime.Now;

                    if(thoigianbatdau <= thoigianhientai && thoigianhientai <= thoigianketthuc)
                    {
                        trangthais.TrangThai = true;
                    }
                    else
                    {
                        trangthais.TrangThai = false;
                    }
                    result = trangthais.TrangThai;
                }
                return result;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}