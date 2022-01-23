﻿using NEW.LSP.Dta;
using NEW.LSP.Dta.Custom;
using NEW.LSP.Dto;
using NEW.LSP.Dto.Custom;
using NEW.LSP.Logic;
using NEW.LSP.UI.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace NEW.LSP.UI.Controllers
{    
    public class ProfileKKSKLController : BaseController
    {
        public string userLogin = string.Empty;
        public string NPSN = string.Empty;
        // GET: ProfileKKSKL
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                if (Session["usrTypeLogin"] != null) { if (Session["usrTypeLogin"].ToString().ToUpper() != "SKL") { Response.Redirect("~/Login"); } }
                int npsn = 0;
                int.TryParse(Session["NPSN"].ToString(), out npsn);

                List<Tb_SMK_Kompetensi_Keahlian_cstm> EmpInfo = new List<Tb_SMK_Kompetensi_Keahlian_cstm>();
                EmpInfo = Tb_SMK_Kompetensi_Keahlian_cstmItem.GetByNPSN(npsn);

                //returning the employee list to view  
                return View(EmpInfo);
            }
            catch (Exception err)
            {
                Tb_Log_Error obj = new Tb_Log_Error(); obj.FunctionName = MethodBase.GetCurrentMethod().Name; obj.Menu = this.GetType().Name; obj.ErrorLog = err.ToString(); obj.creator = "System"; obj.created = DateTime.Now; Tb_Log_ErrorItem.Insert(obj); return View(err.Message);
            }
        }

        // GET: SMKKK/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            try
            {
                Tb_SMK_Kompetensi_Keahlian_cstm EmpInfo = new Tb_SMK_Kompetensi_Keahlian_cstm();

                Int32 ID = 0;
                Int32.TryParse(id, out ID);

                EmpInfo = Tb_SMK_Kompetensi_Keahlian_cstmItem.GetByPK(ID);
                //cek
                if (EmpInfo.NPSN != Convert.ToInt32(Session["NPSN"].ToString())) { return RedirectToAction("Index"); }
                //
                return View(new m_Tb_SMK_Kompetensi_Keahlian_cstm(EmpInfo));
            }
            catch (Exception err)
            {
                Tb_Log_Error obj = new Tb_Log_Error(); obj.FunctionName = MethodBase.GetCurrentMethod().Name; obj.Menu = this.GetType().Name; obj.ErrorLog = err.ToString(); obj.creator = "System"; obj.created = DateTime.Now; Tb_Log_ErrorItem.Insert(obj); return View(err.Message);
            }
        }

        // GET: SMKKK/Create
        [Authorize]
        public ActionResult Create()
        {
            try
            {

                Tb_SMK_Kompetensi_Keahlian_cstm EmpInfo = new Tb_SMK_Kompetensi_Keahlian_cstm();
                List<Tb_Kompetensi_Keahlian> objKK = new List<Tb_Kompetensi_Keahlian>();
                List<Tb_SMK> objSMK = new List<Tb_SMK>();

                objKK = Tb_Kompetensi_KeahlianItem.GetAll();
                objSMK = Tb_SMKItem.GetAll();

                Dictionary<string, string> ooList = new Dictionary<string, string>();
                foreach (var xx in objKK)
                {
                    ooList.Add(xx.Kode_KK.ToString(), xx.Kode_KK.ToString() + " - " + xx.Nama_KK);
                }
                ViewBag.Kode_KKList = dropDownGenerate.toSelectCustom(ooList);


                ooList = new Dictionary<string, string>();
                foreach (var xx in objSMK)
                {
                    ooList.Add(xx.NPSN.ToString(), xx.NPSN.ToString() + " - " + xx.Nama_Sekolah);
                }
                ViewBag.dataSMK = dropDownGenerate.toSelectCustom(ooList);

                return View(new m_Tb_SMK_Kompetensi_Keahlian_cstm(EmpInfo));
            }
            catch (Exception err)
            {
                Tb_Log_Error obj = new Tb_Log_Error(); obj.FunctionName = MethodBase.GetCurrentMethod().Name; obj.Menu = this.GetType().Name; obj.ErrorLog = err.ToString(); obj.creator = "System"; obj.created = DateTime.Now; Tb_Log_ErrorItem.Insert(obj); return View(err.Message);
            }
        }

        // POST: SMKKK/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //check
                if (Request.Form["NPSN"].ToString() != Session["NPSN"].ToString()) { return RedirectToAction("Index"); }
                //check

                userLogin = Session["userLogin"].ToString();
                Tb_SMK_Kompetensi_Keahlian obj = new Tb_SMK_Kompetensi_Keahlian();
                obj.NPSN = Convert.ToInt32(Request.Form["NPSN"]);
                obj.Kode_KK = Convert.ToInt32(Request.Form["Kode_KK"]);
                obj.creator = userLogin;
                obj.created = DateTime.Now;

                Tb_SMK_Kompetensi_KeahlianItem.Insert(obj);

                return RedirectToAction("Index");
            }
            catch (Exception err)
            {
                return RedirectToAction("Create");
            }
        }

        // GET: SMKKK/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index");
                }

                Tb_SMK_Kompetensi_Keahlian_cstm EmpInfo = new Tb_SMK_Kompetensi_Keahlian_cstm();
                List<Tb_Kompetensi_Keahlian> objKK = new List<Tb_Kompetensi_Keahlian>();
                List<Tb_SMK> objSMK = new List<Tb_SMK>();
                objKK = Tb_Kompetensi_KeahlianItem.GetAll();
                objSMK = Tb_SMKItem.GetAll();

                Int32 ID = 0;
                Int32.TryParse(id, out ID);
                EmpInfo = Tb_SMK_Kompetensi_Keahlian_cstmItem.GetByPK(ID);

                //check
                if (EmpInfo.NPSN != Convert.ToInt32(Session["NPSN"].ToString())) { return RedirectToAction("Index"); }
                //check

                Dictionary<string, string> ooList = new Dictionary<string, string>();
                foreach (var xx in objKK)
                {
                    ooList.Add(xx.Kode_KK.ToString(), xx.Kode_KK.ToString() + " - " + xx.Nama_KK);
                }
                ViewBag.Kode_KKList = dropDownGenerate.toSelectCustom(ooList);

                ooList = new Dictionary<string, string>();
                foreach (var xx in objSMK)
                {
                    ooList.Add(xx.NPSN.ToString(), xx.NPSN.ToString() + " - " + xx.Nama_Sekolah);
                }
                ViewBag.dataSMK = dropDownGenerate.toSelectCustom(ooList);

                return View(new m_Tb_SMK_Kompetensi_Keahlian_cstm(EmpInfo));
            }
            catch (Exception err)
            {
                Tb_Log_Error obj = new Tb_Log_Error(); obj.FunctionName = MethodBase.GetCurrentMethod().Name; obj.Menu = this.GetType().Name; obj.ErrorLog = err.ToString(); obj.creator = "System"; obj.created = DateTime.Now; Tb_Log_ErrorItem.Insert(obj); return View(err.Message);
            }
        }

        // POST: SMKKK/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                Int32 ID = 0;
                Int32.TryParse(id, out ID);

                //check
                Tb_SMK_Kompetensi_Keahlian_cstm EmpInfo = new Tb_SMK_Kompetensi_Keahlian_cstm();
                EmpInfo = Tb_SMK_Kompetensi_Keahlian_cstmItem.GetByPK(ID);
                if (EmpInfo.NPSN != Convert.ToInt32(Session["NPSN"].ToString())) { return RedirectToAction("Index"); }
                //check

                userLogin = Session["userLogin"].ToString();
                Tb_SMK_Kompetensi_Keahlian obj = new Tb_SMK_Kompetensi_Keahlian();

                obj.ID = Convert.ToInt32(id);
                obj.NPSN = Convert.ToInt32(Request.Form["NPSN"]);
                obj.Kode_KK = Convert.ToInt32(Request.Form["Kode_KK"]);
                obj.editor = userLogin;
                obj.edited = DateTime.Now;

                Tb_SMK_Kompetensi_KeahlianItem.Update(obj);
                return RedirectToAction("Details/" + id);
            }
            catch (Exception err)
            {
                Tb_Log_Error obj = new Tb_Log_Error(); obj.FunctionName = MethodBase.GetCurrentMethod().Name; obj.Menu = this.GetType().Name; obj.ErrorLog = err.ToString(); obj.creator = "System"; obj.created = DateTime.Now; Tb_Log_ErrorItem.Insert(obj); return View(err.Message);
            }
        }

        // GET: SMKKK/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            try
            {
                //cek
                Int32 ID = 0;
                Int32.TryParse(id, out ID);

                Tb_SMK_Kompetensi_Keahlian_cstm EmpInfo = new Tb_SMK_Kompetensi_Keahlian_cstm();
                EmpInfo = Tb_SMK_Kompetensi_Keahlian_cstmItem.GetByPK(ID);
                if (EmpInfo.NPSN != Convert.ToInt32(Session["NPSN"].ToString())) { return RedirectToAction("Index"); }
                //

                Tb_SMK_Kompetensi_KeahlianItem.Delete(ID);

                return RedirectToAction("Index");
            }
            catch (Exception err)
            {
                Tb_Log_Error obj = new Tb_Log_Error(); obj.FunctionName = MethodBase.GetCurrentMethod().Name; obj.Menu = this.GetType().Name; obj.ErrorLog = err.ToString(); obj.creator = "System"; obj.created = DateTime.Now; Tb_Log_ErrorItem.Insert(obj); return View(err.Message);
            }
        }

    }
}
