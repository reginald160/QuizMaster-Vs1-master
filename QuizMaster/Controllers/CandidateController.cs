using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizMaster.Data;
using QuizMaster.Models;
using QuizMaster.ViewModels;

namespace QuizMaster.Controllers
{
    public class CandidateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public CandidateController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public async Task<ActionResult> AllCandidates()
        {
            var candidate = await _context.Candidates.ToListAsync();
            if(candidate != null)
            {
                return View(candidate);
            }
            else
            {
                TempData["NullScore"] = "There is no registered candidate yet";
                return View("Index", "Home");
            }
        }

        // GET: Candidate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Candidate/Create
        public ActionResult CreateCandidate()
        {
            return View();
        }

        // POST: Candidate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCandidate(CandidateViewModel Vmodel)
        {
            try
            {
                var examCode = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace("AM", "00").Replace("PM", "1").Replace(" ", "");
                var candidate = new Candidate
                {
                    Name = Vmodel.Name,
                    Email = Vmodel.Email,
                    ExamCode = examCode,
                    Phone = Vmodel.Phone,
                    RegistrationDate = DateTime.Now

                };
                var emailMessage = $"Dear {candidate.Name} your registration was sucessfull, you can visit our page for your exam. use {candidate.ExamCode} to login";
                var result = _context.Candidates.Where(x => x.Email == candidate.Email).FirstOrDefault();
                if (result == null)
                {
                    await _context.Candidates.AddAsync(candidate);
                    await _context.SaveChangesAsync();
                    await _emailSender.SendEmailAsync(candidate.Email, "Quiz Master Registration", emailMessage);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "User with this Email Already Exist";
                    return View();
                }

            }
            catch
            {
                return View();
            }
        }


        // GET: Candidate/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Candidate/Edit/5
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

        // GET: Candidate/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Candidate/Delete/5
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
    }
}