using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefereeingCHBC.Datas;
using RefereeingCHBC.Extensions;

namespace RefereeingCHBC.Controllers
{
    [Authorize]
    [DisplayName("Report Refereeing")]
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        // GET: Report/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            //Lists
            ViewData["championships"] = Championships.GetChampionships();
            ViewData["clubs"] = Clubs.GetClubs();
            ViewData["referees"] = Referees.GetReferees();
            ViewData["supervisors"] = Supervisors.GetSupervisors();
            return View();
        }

        // POST: Report/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Report report)
        {
            try
            {
                int RowsAffected = 0;
                if (ModelState.IsValid)
                {
                    // TODO: faire verifications
                    if (IsValidReport(report))
                    {
                        RowsAffected = Reports.InsertReport(report);
                        if (RowsAffected > 0)
                        {
                            return RedirectToAction(nameof(List)).WithSuccess("Super !", $"Le Rapport d'Arbitrage pour le match " +
                                $"<b>{report.ClubHome.Name} / {report.ClubVisitor.Name}</b> à bien été <b>Enregistré</b> !");
                        }
                        else
                        {
                            ViewData["championships"] = Championships.GetChampionships();
                            ViewData["clubs"] = Clubs.GetClubs();
                            ViewData["referees"] = Referees.GetReferees();
                            ViewData["supervisors"] = Supervisors.GetSupervisors();
                            return View().WithDanger("Erreur !", "Une erreur s'est produite lors du traitement...");
                        }
                    }
                    else
                    {
                        ViewData["championships"] = Championships.GetChampionships();
                        ViewData["clubs"] = Clubs.GetClubs();
                        ViewData["referees"] = Referees.GetReferees();
                        ViewData["supervisors"] = Supervisors.GetSupervisors();
                        return View().WithDanger("Erreur !", "Les Données renseignées ne respectent pas les règles établies.");
                    }
                }
                else
                {
                    ViewData["championships"] = Championships.GetChampionships();
                    ViewData["clubs"] = Clubs.GetClubs();
                    ViewData["referees"] = Referees.GetReferees();
                    ViewData["supervisors"] = Supervisors.GetSupervisors();
                    return View().WithDanger("Erreur !", "Les Données renseignées ne sont pas complètes !");
                }
            }
            catch
            {
                ViewData["championships"] = Championships.GetChampionships();
                ViewData["clubs"] = Clubs.GetClubs();
                ViewData["referees"] = Referees.GetReferees();
                ViewData["supervisors"] = Supervisors.GetSupervisors();
                return View().WithDanger("Erreur !", "Une erreur s'est produite lors du traitement...");
            }
        }

        // GET: Report/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Report/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Report/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/List
        public ActionResult List()
        {
            //GET ALL REPORTS FOR ADMIN OR REPORT BY USER FOR USERS
            //GET ALL REPORTS FOR THE MOMENT
            var reports = Reports.GetReports().ToList();
            return View(reports);
        }

        private bool IsValidReport(Report report)
        {
            if ((int)report.A1Rate < 1 || (int)report.A1Rate > 4)
            {
                return false;
            }
            if ((int)report.A2Rate < 1 || (int)report.A2Rate > 4)
            {
                return false;
            }
            if ((int)report.A3Rate < 1 || (int)report.A3Rate > 4)
            {
                return false;
            }
            if ((int)report.A4Rate < 1 || (int)report.A4Rate > 4)
            {
                return false;
            }

            if ((int)report.B1Rate < 1 || (int)report.B1Rate > 4)
            {
                return false;
            }
            if ((int)report.B2Rate < 1 || (int)report.B2Rate > 4)
            {
                return false;
            }
            if ((int)report.B3Rate < 1 || (int)report.B3Rate > 4)
            {
                return false;
            }

            if ((int)report.C1Rate < 1 || (int)report.C1Rate > 4)
            {
                return false;
            }
            if ((int)report.C2Rate < 1 || (int)report.C2Rate > 4)
            {
                return false;
            }
            if ((int)report.C3Rate < 1 || (int)report.C3Rate > 4)
            {
                return false;
            }
            if ((int)report.C4Rate < 1 || (int)report.C4Rate > 4)
            {
                return false;
            }
            if ((int)report.C5Rate < 1 || (int)report.C5Rate > 4)
            {
                return false;
            }

            try
            {
                if (report.A1Comment.Length > 250 ||
                report.A2Comment.Length > 250 ||
                report.A3Comment.Length > 250 ||
                report.A4Comment.Length > 250 ||
                report.B1Comment.Length > 250 ||
                report.B2Comment.Length > 250 ||
                report.B3Comment.Length > 250 ||
                report.C1Comment.Length > 250 ||
                report.C2Comment.Length > 250 ||
                report.C3Comment.Length > 250 ||
                report.C4Comment.Length > 250 ||
                report.C5Comment.Length > 250 ||
                report.Comment.Length > 500)
            {
                return false;
            }
            }
            catch { }
            
            return true;
        }
    }
}