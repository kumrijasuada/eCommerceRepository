using ShisheVere.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Principal;
using ShisheVere.DBCONTEXT;
using System.Text;
using ShisheVere.ViewModels;
using System.Net.Mail;
using System.Security.Cryptography;

namespace ShisheVere.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login(string returnURL)
        {
            var userinfo = new LoginVM();
            // We do not want to use any existing identity information
            EnsureLoggedOut();
            // Store the originating URL so we can attach it to a form field
            userinfo.ReturnURL = returnURL;

            return View(userinfo);
        }

        //GET: EnsureLoggedOut
        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (Request.IsAuthenticated)
                Session["Logout"] = "Ju aktualisht jeni te loguar !! Nqs doni te perdorni nje llogari tjeter ,duhet te dilni nga llogaria aktuale . ";
        }

        //POST: Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            // First we clean the authentication ticket like always
            //required NameSpace: using System.Web.Security;
            FormsAuthentication.SignOut();
            // Second we clear the principal to ensure the user does not retain any authentication
            //required NameSpace: using System.Security.Principal;
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();
            // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
            // this clears the Request.IsAuthenticated flag since this triggers a new request
            return RedirectToLocal();
        }

        //GET: SignInAsync
        private void SignInRemember(string userName, bool isPersistent = false)
        {
            // Clear any lingering authencation data
            FormsAuthentication.SignOut();

            // Write the authentication cookie
            FormsAuthentication.SetAuthCookie(userName, isPersistent);
        }

        //GET: RedirectToLocal
        private ActionResult RedirectToLocal(string returnURL = "")
        {
            try
            {
                // If the return url starts with a slash "/" we assume it belongs to our site
                // so we will redirect to this "action"
                if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL))
                    return Redirect(returnURL);

                // If we cannot verify if the url is local to our host we redirect to a default location
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM entity)
        {
            using (StoreContext db = new StoreContext())
            {
                // Ensure we have a valid viewModel to work with
                if (!ModelState.IsValid)
                    return View(entity);

                var dbUser = db.Perdorues.Where(s =>
                s.Username.Equals(entity.Username.Trim())).FirstOrDefault();

                bool isCorrectPassword = false;
                if (dbUser != null)
                {
                    byte[] salt = new byte[32];
                    salt = dbUser.Salt;
                    byte[] password = new byte[32];
                    password = dbUser.Password;

                    int iterations = 10000;
                    using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(entity.Password, salt, iterations))
                    {
                        byte[] derivedKey = pbkdf2.GetBytes(32);
                        if (derivedKey.SequenceEqual(password))
                        {
                            isCorrectPassword = true;
                        }
                    }
                }

                if (isCorrectPassword)
                {
                    SignInRemember(entity.Username, entity.isRemember);

                    Session["UserName"] = dbUser.Username;
                    Session["roli"] = dbUser.Roli;

                    if (string.IsNullOrEmpty(entity.ReturnURL))
                    {
                        if (dbUser.Roli == "admin")
                            return RedirectToAction("Index", "Admin");
                        else if (dbUser.Roli == "prodhues")
                            return RedirectToAction("Index", "Prodhues");
                        else return RedirectToAction("Index", "Home");

                    }
                    return RedirectToLocal(entity.ReturnURL);
                }

                else
                {
                    //Login Fail
                    TempData["ErrorMSG"] = "Access Denied! Wrong Credential";
                    return View(entity);
                }
            }
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegistrationView registrationView)
        {
            if (string.IsNullOrEmpty(registrationView.Roli) || string.IsNullOrEmpty(registrationView.Password) || string.IsNullOrEmpty(registrationView.Adresa) || string.IsNullOrEmpty(registrationView.Telefon) || string.IsNullOrEmpty(registrationView.Username) || string.IsNullOrEmpty(registrationView.Password) || string.IsNullOrEmpty(registrationView.Emer) || string.IsNullOrEmpty(registrationView.Mbiemer) || string.IsNullOrEmpty(registrationView.Email) || string.IsNullOrEmpty(registrationView.ConfirmPassword))
            {
                TempData["ErrorReg"] = "Please fill the fields !";
                return View(registrationView);
            }

            else
            {
                StoreContext db = new StoreContext();

                int count1 = (from u in db.Perdorues
                              where string.Compare(registrationView.Username, u.Username) == 0
                              select u.Username).Count();
                if (count1 > 0)
                {
                    TempData["username"] = "- Sorry: Username already Exists ! Choose another Username !";
                    return View(registrationView);
                }

                int count2 = (from u in db.Perdorues
                              where string.Compare(registrationView.Email, u.Email) == 0
                              select u.Username).Count();

                if (count2 > 0)
                {
                    TempData["email"] = "- Sorry:Email already Exists ! Choose another Email !";
                    return View(registrationView);
                }

                //Save User Data   
                using (StoreContext dbContext = new StoreContext())
                {
                    Tuple<byte[], byte[]> passwordAndSalt = PasswordHashing(registrationView.Password);

                    var user = new Perdorues()
                    {
                        Username = registrationView.Username,
                        Emer = registrationView.Emer,
                        Mbiemer = registrationView.Mbiemer,
                        Email = registrationView.Email,
                        Password = passwordAndSalt.Item1,
                        Salt = passwordAndSalt.Item2,
                        Telefon = registrationView.Telefon,
                        Status = "aktiv",
                        Roli = registrationView.Roli,
                        Adrese = registrationView.Adresa
                    };

                    dbContext.Perdorues.Add(user);
                    dbContext.SaveChanges();
                    if (registrationView.Roli == "prodhues")
                    {
                        var prodhues = new Prodhues()
                        {
                            Emertim = registrationView.Username,
                            Email = registrationView.Email,
                            Telefon = registrationView.Telefon,
                            Status = "aktiv",
                            Adrese = registrationView.Adresa
                        };

                        dbContext.Prodhues.Add(prodhues);
                        dbContext.SaveChanges();
                    }
                    return View("Login");
                }

            }
        }

        // PBKDF2 algorithm (Password-Based Key Derivation Function 2)
        private Tuple<byte[], byte[]> PasswordHashing(string password)
        {
            byte[] bytePassword;

            byte[] randomSalt = new byte[32];
            using (var x = new RNGCryptoServiceProvider())
            {
                x.GetBytes(randomSalt);
            }
            int iterations = 10000;
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, randomSalt, iterations))
            {
                byte[] derivedKey = pbkdf2.GetBytes(32);
                bytePassword = derivedKey; 
            }
            return Tuple.Create(bytePassword, randomSalt);
        }

        public string GetUserNameByEmail(string email)
        {
            using (StoreContext dbContext = new StoreContext())
            {
                string username = (from u in dbContext.Perdorues
                                   where string.Compare(email, u.Email) == 0
                                   select u.Username).FirstOrDefault();

                return !string.IsNullOrEmpty(username) ? username : string.Empty;
            }
        }

        // GET: Account/LostPassword      
        [AllowAnonymous]
        public ActionResult LostPassword()
        {
            return View();
        }

        // POST: Account/LostPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LostPassword(FormCollection model)
        {
            StoreContext db = new StoreContext();
            string txt = Guid.NewGuid().ToString();
            if (model != null)
            {
                string Email = model["Email"].ToString();
                if (!string.IsNullOrEmpty(Email))
                {

                    string username = GetUserNameByEmail(Email);
                    if (string.IsNullOrEmpty(username))
                    {
                        TempData["reset"] = "Nje email i tille nuk ekziston!!";
                        return View("LostPassword");
                    }

                    else
                    {
                        Kerkesat kerkese = new Kerkesat();
                        username = GetUserNameByEmail(Email);
                        kerkese.kerkesaId = txt;
                        kerkese.date = DateTime.Now;
                        int id = (from u in db.Perdorues
                                  where string.Compare(username, u.Username) == 0
                                  select u.Id_perdorues).FirstOrDefault();
                        kerkese.perdoruesId = id;
                        db.Kerkesat.Add(kerkese);
                        db.SaveChanges();
                        MailMessage email = new MailMessage();
                        StringBuilder trupiEmail = new StringBuilder();
                        trupiEmail.Append("Pershendetje " + username + ",<br/><br/>");
                        trupiEmail.Append(" Ju mund te ndryshoni passwordin duke klikuar ne linkun e meposhtem" + "<br/><br/>");
                        trupiEmail.Append("http://localhost:51358/Account/ResetPassword?id=" + txt);
                        email.To.Add(Email);
                        email.From = new MailAddress("erion.ibraliu@fshnstudent.info");
                        email.IsBodyHtml = true;
                        email.Body = trupiEmail.ToString();
                        email.Subject = "Rivendos Password";
                        SmtpClient client = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false

                        };
                        client.Credentials = new System.Net.NetworkCredential()
                        {
                            UserName = "erion.ibraliu@fshnstudent.info",
                            Password = "1999denis"
                        };

                        client.Send(email);
                    }
                    TempData["reset"] = "Email-i u dergua me sukses";
                    return View("LostPassword");
                }
                else
                {
                    TempData["reset"] = "Jepni email-in!";
                    return View("LostPassword");
                }
            }
            else
            {
                return View("LostPassword");
            }

        }

        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            StoreContext db = new StoreContext();
            string idKerkese = Request.QueryString["id"];
            string gjej = (from u in db.Kerkesat
                           where string.Compare(idKerkese, u.kerkesaId) == 0
                           select u.kerkesaId).FirstOrDefault();
            if (string.IsNullOrEmpty(gjej))
                return RedirectToAction("Index", "NotFound");
            else return View();
        }


        // ToDo

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ResetPassword(FormCollection perdorues, string id)
        //{
        //    StoreContext db = new StoreContext();
        //    string idKerkese = Request.QueryString["id"];
        //    string gjej = (from u in db.Kerkesat
        //                   where string.Compare(idKerkese, u.kerkesaId) == 0
        //                   select u.kerkesaId).FirstOrDefault();
        //    if (!string.IsNullOrEmpty(gjej))
        //    {
        //        string Username = perdorues["username"].ToString();
        //        string Pass1 = perdorues["pass1"].ToString();
        //        string Confpass1 = perdorues["confpass1"].ToString();

        //        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Pass1) || string.IsNullOrEmpty(Confpass1))
        //        {
        //            TempData["sukses"] = "Ju lutemi plotesoni te gjitha fushat!";
        //            return View("ResetPassword");

        //        }
        //        else if (Pass1 != Confpass1)
        //        {
        //            TempData["sukses"] = "Ju lutemi sigurohuni qe passwordi i ri te jete i njejte me passwordin e konfirmuar!";
        //            return View("ResetPassword");
        //        }
        //        else
        //        {
        //            var userInfo = db.Perdorues.Where(s => s.Username.Equals(Username.Trim())).FirstOrDefault();
        //            if (userInfo != null)
        //            {
        //                userInfo.Password = Pass1;
        //                db.SaveChanges();
        //                TempData["sukses"] = "Passwordi u ndryshua me sukses!";
        //                return View("ResetPassword");
        //            }
        //            else
        //            {
        //                TempData["sukses"] = "Nje llogari e tille nuk ekziston";
        //                return View("ResetPassword");
        //            }
        //        }
        //    }
        //    else return RedirectToAction("Index", "NotFound");
        //}
    }
}