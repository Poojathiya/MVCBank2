using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBank2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCBank2.Controllers
{
    public class BankController : Controller
    {
        private readonly BankContext db;
        public BankController(BankContext _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminView()
        {

            return View(db.Newaccounts.ToList());
        }
        [HttpGet]
        public IActionResult AddCustomer()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddCustomer(Newaccount s)
        {
            db.Newaccounts.Add(s);
            db.SaveChanges();
            return RedirectToAction("AdminView");
        }
        [HttpGet]
        public IActionResult Edit(int AccountNumber)
        {
            Newaccount S = db.Newaccounts.Find(AccountNumber);
            HttpContext.Session.SetString("CurrentBalance", S.CurrentBalance.ToString());
            return View(S);
        }
        [HttpPost]
        public IActionResult Edit(Newaccount s)
        {
            s.CurrentBalance = Convert.ToInt32(HttpContext.Session.GetString("CurrentBalance"));


            db.Newaccounts.Update(s);

            db.SaveChanges();
            return RedirectToAction("AdminView");
        }

        [HttpGet]
        public IActionResult Details(int AccountNumber)
        {

            Newaccount S = db.Newaccounts.Find(AccountNumber);
            return View(S);

        }
        [HttpGet]
        public IActionResult Delete(int AccountNumber)
        {
            Newaccount S = db.Newaccounts.Find(AccountNumber);
            return View(S);
        }
        [HttpPost]
        public IActionResult Delete(Newaccount s)
        {
            try                                                                            /* Doubt*/
            {
                db.Newaccounts.Remove(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                ViewBag.Message = "Denied Access";

                return View();

            }
            return View("Index");
        }
        //---------------------------------------------------------------------TRANSACTION------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult TransactionTable()
        {
            var result = db.Transaction1s.Include(x => x.AccountNumberNavigation).ToList();
            return View(db.Transaction1s.ToList());
        }

        [HttpGet]

        public IActionResult MakeTransaction()
        {
            var result = new SelectList(from i in db.Newaccounts select i.AccountNumber).ToList();
            ViewBag.CustomerName = result;

            return View();

        }
        [HttpPost]
        public IActionResult MakeTransaction(Transaction1 s)
        {
            db.Transaction1s.Add(s);

            Newaccount c = db.Newaccounts.Find(s.AccountNumber);

            var bal = (from cnt in db.Newaccounts where cnt.AccountNumber == s.AccountNumber select cnt).First<Newaccount>().CurrentBalance;
            //List<Newaccount> bal = (from i in db.Newaccounts
            //                    where i.AccountNumber == s.AccountNumber
            //                     select i).ToList();
            if (s.TransactionType == "Deposit")
            {
                bal += s.Amount;
            }
            else
            {
                bal -= s.Amount;
            }
            c.CurrentBalance = bal;

            db.Newaccounts.Update(c);


            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public IActionResult MakeTransaction(Transaction1 s)
        //{


        //    db.Transaction1s.Add(s);
        //    //HttpContext.Session.SetString("AccountNumber", s.AccountNumber.ToString());
        //    //Newaccount c = new Newaccount();
        //    //var bal = (from cnt in db.Newaccounts where cnt.AccountNumber == s.AccountNumber select cnt).First<Newaccount>().CurrentBalance;

        //    //c.CurrentBalance += bal;


        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}


        public IActionResult ShowDetails(int AccountNumber)
        {
            AccountNumber = Convert.ToInt32(HttpContext.Session.GetString("AccountNumber"));
            Newaccount S = db.Newaccounts.Find(AccountNumber);
            return View(S);

        }
        public IActionResult TransactionHistory(int AccountNumber)
        {
            List<Transaction1> result = (from i in db.Transaction1s.Include(x => x.AccountNumberNavigation)
                                         where i.AccountNumber == AccountNumber
                                         select i).ToList();
            return View(result);
        }


    }
}
