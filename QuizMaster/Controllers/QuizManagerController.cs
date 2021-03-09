using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizMaster.Data;
using QuizMaster.Models;
using QuizMaster.Services;
using QuizMaster.ViewModels;

namespace QuizMaster.Controllers
{
    [System.Runtime.InteropServices.Guid("4ED32701-1C16-4D87-BB82-0BE69348E67F")]
    public class QuizManagerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public QuizManagerController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        [HttpGet]
        public IActionResult ErrorPage(string message)
        {
            if (!string.IsNullOrEmpty(message))
                TempData["ErrorMessage"] = message;
            return View();


        }

        [HttpGet]
        public IActionResult ValidateCandidate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ValidateCandidate(CandidateValidationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var candidate = await _context.Candidates.Where(x => x.Email.Contains(viewModel.Email) && x.ExamCode.Contains(viewModel.ExamCode)).FirstOrDefaultAsync();
                if (candidate != null)
                {
                    HttpContext.Session.SetString("sessionExamCode", candidate.ExamCode);
                    HttpContext.Session.SetString("sessionEmail", candidate.Email);
                    HttpContext.Session.SetString("sessionName", candidate.Name);
                    HttpContext.Session.SetInt32("sessionId", candidate.Id);
                    if (HttpContext.Session.GetString("sessionEmail") != null)
                    {
                        return RedirectToAction(nameof(Instruction));
                    }

                }
                ViewBag.ErrorMessage = "Your Details are not found";
                //return NotFound("Your Details are not found");
                return RedirectToAction("ErrorPage", new { message = "We could not verify your credentials. Please double-check and try again" });
            }
            return View();
        }
        [HttpGet]
        public IActionResult Instruction()
        {
            ViewBag.QuestionCount = _context.Questions.Count();
            ViewBag.Name = HttpContext.Session.GetString("sessionName");
            return View();
        }
        [HttpPost]
        public IActionResult Instruction(string name)
        {
            var examCode = HttpContext.Session.GetString("sessionExamCode");
            var examStatus = _context.Examinations.Where(x => x.ExamCode.Contains(examCode)).FirstOrDefault();
            if (examStatus == null)
            {
                return RedirectToAction(nameof(TakeExam));
            }
            return RedirectToAction("ErrorPage", new { message = "Quiz is no more available for this candidate, Contact the Admin!" });
        }

        [HttpGet]
        public IActionResult CreatSubject()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatSubject(Subject model)
        {
            var IsExist = _context.Subjects.Where(x => x.Name == model.Name).FirstOrDefault();
            if (IsExist == null)
            {
                _unitOfWork.AddSubject(model);
                await _unitOfWork.Save();
                return RedirectToAction(nameof(CreatSubject));
            }
            else
            {
                ViewBag.Message = "Subject Name Already Exist";
                return View();
            }

        }
        public IActionResult CreatQuestion()
        {
            ViewBag.Subject = _unitOfWork.GetSubjects();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatQuestion(Question model)
        {
            _unitOfWork.AddQuestion(model);
            await _unitOfWork.Save();
            return View();
        }
        public IActionResult CreatExam()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatExam(Examination model)
        {
            _unitOfWork.AddExam(model);
            await _unitOfWork.Save();
            return View();
        }
        [HttpGet]
        public IActionResult TakeExam()
        {
            var examCode = HttpContext.Session.GetString("sessionExamCode");
            var examcount = _context.Examinations.Where(x => x.ExamCode == examCode).Count();
            var questionCount = _context.Questions.Count();
            var id = examcount + 1;
            ViewBag.Question = _context.Questions.Where(x => x.QuestionNumber == id).ToList();
            ViewBag.count = id;
            ViewBag.Value = _context.Questions.Count();
            ViewBag.Max = _context.Examinations.Count();
            ViewBag.ExamCount = id;



            if (HttpContext.Session.GetString("sessionEmail") != null)
            {

                if (examcount != questionCount)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction(nameof(CalculateScores));
                }
            }
            return RedirectToAction("ErrorPage", new { message = "Section has Expired" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TakeExam(ExaminationViewModel Vmodel)
        {
            if (ModelState.IsValid)
            {
                var examcount = _context.Examinations.Count();
                var questionCount = _context.Questions.Count();
                var model = new Examination
                {
                    Option = Vmodel.Option,
                    Answer = Vmodel.Answer,
                    QuestionId = Vmodel.QuestionId,
                    CandidateId = (int)HttpContext.Session.GetInt32("sessionId"),
                    ExamCode = HttpContext.Session.GetString("sessionExamCode"),
                };
                await _context.Examinations.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(TakeExam));

            };
            return RedirectToAction("ErrorPage", new { message = "Error has Occured" });

        }


        public async Task<IActionResult> CalculateScores()
        {

            var examCode = HttpContext.Session.GetString("sessionExamCode");
            var candidateName = HttpContext.Session.GetString("sessionName");
            var pass = _context.Examinations.Where(x => x.Answer == x.Option && x.ExamCode == examCode).Count();
            var fail = _context.Examinations.Where(x => x.Answer != x.Option && x.ExamCode == examCode).Count();
            var Total = _context.Examinations.Where(x => x.ExamCode == examCode).Count();
            var percentageScore = (pass / Total) / 100;
            ViewBag.Pass = pass;
            ViewBag.Fail = fail;
            ViewBag.Total = Total;
            ViewBag.PercentageScore = percentageScore;
            ViewBag.Score = _context.Examinations.Where(x => x.Answer == x.Option && x.ExamCode == examCode).Count();
            var score = new Score
            {
                ExamCode = examCode,
                CandidateName = candidateName,
                CandidateScore = pass,
                TestDate = DateTime.Now
            };
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Scores.AddRangeAsync(score);
                    await _context.SaveChangesAsync();

                    return View();
                }
                catch
                {
                    return RedirectToAction("ErrorPage", new { message = "Error has Occured" });
                }

            }
            else
            {
                return RedirectToAction("ErrorPage", new { message = "Invalid Query" });
            }
        }
        public async Task<IActionResult> AllScore()
        {
            var scores = await _context.Scores.ToListAsync();
            return View(scores);

        }

        [HttpGet]
        public IActionResult GetScore()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetScore(Score score)
        {
            await _context.Scores.Where(x => x.ExamCode == score.ExamCode).FirstOrDefaultAsync();
            var scores = await _context.Scores.ToListAsync();
            return View(scores);
        }


        [HttpGet]
        public IActionResult TakeExams()
        {
            var examCode = HttpContext.Session.GetString("sessionExamCode");
            var examcount = _context.Examinations.Where(x => x.ExamCode == examCode).Count();
            var questionCount = _context.Questions.Count();
            var id = examcount + 2;
            ViewBag.Question = _context.Questions.Where(x => x.QuestionNumber == id).ToList();
            ViewBag.count = id;
            ViewBag.Value = _context.Questions.Count();
            ViewBag.Max = _context.Examinations.Count();


            if (HttpContext.Session.GetString("sessionEmail") != null)
            {

                if (examcount != questionCount)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction(nameof(CalculateScores));
                }
            }
            return RedirectToAction("ErrorPage", new { message = "Session has expired" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TakeExams(ExaminationViewModel Vmodel)
        {
            if (ModelState.IsValid)
            {
                var examcount = _context.Examinations.Count();
                var questionCount = _context.Questions.Count();
                var model = new Examination
                {
                    Option = Vmodel.Option,
                    Answer = Vmodel.Answer,
                    QuestionId = Vmodel.QuestionId,
                    CandidateId = (int)HttpContext.Session.GetInt32("sessionId"),
                    ExamCode = HttpContext.Session.GetString("sessionExamCode"),
                };
                await _context.Examinations.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(TakeExam));

            };
            return RedirectToAction("ErrorPage", new { message = "Invalid Query" });

        }


    }
}
