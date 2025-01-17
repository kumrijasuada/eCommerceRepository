﻿using ShisheVere.DBCONTEXT;
using ShisheVere.Models;
using ShisheVere.Security;
using ShisheVere.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PayPal.Api;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ShisheVere.Controllers
{
    public class ShoppingCartController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: ShoppingCart
        [CostumAuthorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        [CostumAuthorize(Roles = "user")]
        public ActionResult myshoppingcart()
        {
            ViewBag.cart = 1;
            var username = Session["UserName"].ToString();
            var shop = db.ShoppingCart.Where(s => s.UserName == username).ToList();

            if (shop.Count == 0)
                ViewBag.cart = 0;
            return View(db.ShoppingCart.Where(s => s.UserName == username).ToList());
        }

        [CostumAuthorize(Roles = "user")]
        public JsonResult AddtoCart(int? id)
        {

            ShoppingCart shop = new ShoppingCart();
            Shishe shishe = new Shishe();
            shishe = db.Shishe.Where(x => x.Id_shishe == id).FirstOrDefault();
            string username = Session["UserName"].ToString();
            Perdorues user = db.Perdorues.Where(x => x.Username == username).FirstOrDefault();
            if (shishe != null)
            {
                shop.Shishe = shishe.Emertim;
                shop.Id_shishe = shishe.Id_shishe;
                shop.Id_perdorues = user.Id_perdorues;
                shop.UserName = Session["UserName"].ToString();
                shop.Sasia = 1;
                shop.Price = shishe.Price;
                Foto f = new Foto();
                f = db.Foto.Where(p => p.Id_shishe == id).FirstOrDefault();
                if (f != null)
                {
                    shop.foto = f.File;
                }
                ShoppingCart cart = new ShoppingCart();
                cart = db.ShoppingCart.Where(p => p.Shishe == shishe.Emertim && p.UserName == shop.UserName).FirstOrDefault();
                if (cart != null)
                {
                    TempData["cart"] = "Ju e keni te perfshire me pare kete produkt ne Shopping Cart!";
                    return Json(shop, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.ShoppingCart.Add(shop);
                    db.SaveChanges();
                }
            }
            return Json(shop, JsonRequestBehavior.AllowGet);
        }

        [CostumAuthorize(Roles = "user")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shop = db.ShoppingCart.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        [CostumAuthorize(Roles = "user")]
        // POST: Prodhues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection form, int id)
        {
            int quantity = Convert.ToInt32(form["Sasia"]);
            var order = db.ShoppingCart.Where(p => p.id == id).FirstOrDefault();
            order.Sasia = quantity;
            db.SaveChanges();
            return RedirectToAction("myshoppingcart");
        }

        [CostumAuthorize(Roles = "user")]
        [HttpPost]
        public JsonResult Update(ShopModel shop)
        {
            ShoppingCart sh = db.ShoppingCart.Where(p => p.id == shop.id).FirstOrDefault();
            sh.Sasia = shop.Sasia;
            db.SaveChanges();
            return Json(sh, JsonRequestBehavior.AllowGet);
        }

        [CostumAuthorize(Roles = "user")]
        [HttpPost]
        public JsonResult DeleteShop()
        {
            var username = Session["UserName"].ToString();
            var shop = db.ShoppingCart.Where(p => p.UserName == username).ToList();
            foreach (var item in shop)
            {
                db.ShoppingCart.Remove(item);
                db.SaveChanges();
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }


        [CostumAuthorize(Roles = "user")]
        public ActionResult Delete(int? id)
        {
            ShoppingCart model = new ShoppingCart();
            string user = Session["UserName"].ToString();
            if (id > 0)
            {
                ShoppingCart emp = db.ShoppingCart.SingleOrDefault(x => x.id == id && x.UserName == user);
                model.id = emp.id;
            }
            return PartialView("DeleteShop", model);
        }

        [CostumAuthorize(Roles = "user")]
        // POST: Prodhues/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoppingCart shop = db.ShoppingCart.Find(id);
            db.ShoppingCart.Remove(shop);
            db.SaveChanges();
            return RedirectToAction("myshoppingcart");
        }

        public ActionResult Order(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCart shop = db.ShoppingCart.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        [CostumAuthorize(Roles = "admin")]
        public ActionResult DeleteOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders porosi = db.Orders.Find(id);
            if (porosi == null)
            {
                return HttpNotFound();
            }
            return View(porosi);
        }

        [CostumAuthorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOrder(int id)
        {
            Orders porosi = db.Orders.Find(id);
            db.Orders.Remove(porosi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //punojme me pagesen me paypal

        private Payment payment;

        //krijojme nje pagese me APIContext
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var shop = new ItemList() { items = new List<Item>() };
            var username = Session["UserName"].ToString();
            var shop1 = db.ShoppingCart.Where(p => p.UserName == username).ToList();
            foreach (var item in shop1)
            {
                shop.items.Add(new Item()
                {
                    name = item.Shishe,
                    currency = "USD",
                    price = (Convert.ToDouble(item.Price)).ToString(),
                    // price = "150",
                    quantity = item.Sasia.ToString(),
                    //description = item.Id_shishe.ToString(),

                    sku = item.Id_shishe.ToString(),
                });
            }
            var payer = new Payer() { payment_method = "paypal" };

            // Bejme konfigurimet RedirectUrls me objektet perkatese
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            //Krijojm objektin e detajeve qe shfaqet ne paypal

            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = shop1.Sum(x => Convert.ToDouble(x.Sasia * x.Price)).ToString()


            };

            //Krijojme objektin e cmimit

            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),
                details = details
            };

            //Krijojme transaksionin
            var transactionList = new List<Transaction>
            {
                new Transaction()
                {
                    description = "Veprim per testim per shitjen e veres.",
                    invoice_number = Convert.ToString((new Random()).Next(100000)),
                    amount = amount,
                    item_list = shop
                }
            };

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return this.payment.Create(apiContext);
        }

        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            var username = Session["UserName"].ToString();
            var shoppingCarts = db.ShoppingCart.Where(p => p.UserName == username).ToList();
            foreach (var item in shoppingCarts)
            {
                var shopSasidb = db.Shishe.Where(x => x.Id_shishe == item.Id_shishe).FirstOrDefault();
                totalPrice += item.Sasia * item.Price;
            }
            return totalPrice;
        }

        public (bool,string) CheckInventory()
        {
            var username = Session["UserName"].ToString();
            var shopSasi = db.ShoppingCart.Where(p => p.UserName == username).ToList();
            foreach (var item in shopSasi)
            {
                var shopSasidb = db.Shishe.Where(x => x.Id_shishe == item.Id_shishe).FirstOrDefault();
                if(shopSasidb.Sasia < item.Sasia)
                {
                    return (false, $"Inventory for product {shopSasidb.Emertim} does not fill quantity required {item.Sasia}.");
                }
            }
            return (true,string.Empty);
        }

        //Create  execute payment method
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {

            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }

        //Metoda qe do therritet ne klikim te butonit
        public ActionResult CheckoutOrder(string Cancel = null)
        {
            var inventoryCheck = CheckInventory();
            if (!inventoryCheck.Item1)
            {
                ViewBag.Message = inventoryCheck.Item2;
                return View("Inventory");
            }
            //getting context from the paypal bases on client Id and ClientSecret for payment
            APIContext apiContext = PaypalCofiguration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/ShoppingCart/CheckoutOrder?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

                    //get links returned from paypal response to create call function
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;

                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("Failure");
                    }

                }
            }
            catch (Exception ex)
            {

                PaypalLogger.Log("Error : " + ex.Message);
                return View("Failure");
            }

            //If we get here it means everything is correct
            ViewBag.Price = GetTotalPrice().ToString();
            CompleteOrder();
            return View("Success");

        }

        private void CompleteOrder()
        {
            var username = Session["UserName"].ToString();
            var currentUser = db.Perdorues.Where(x => x.Username == username).FirstOrDefault();
            var shop = db.ShoppingCart.Where(p => p.UserName == username).ToList();
            foreach (var item in shop)
            {
                Orders order = new Orders
                {
                    Adresa = currentUser.Adrese,
                    Emri = currentUser.Emer,
                    Telefoni = currentUser.Telefon,
                    Id_shishe = item.Id_shishe,
                    Sasia = item.Sasia,
                    Id_perdorues = currentUser.Id_perdorues,
                    Mbiemri = currentUser.Mbiemer
                };
                db.Orders.Add(order);
                db.SaveChanges();
                db.ShoppingCart.Remove(item);
                db.SaveChanges();

                //Update quantity for current Item
                var shishe = db.Shishe.Where(x => x.Id_shishe == item.Id_shishe).FirstOrDefault();
                shishe.Sasia = shishe.Sasia - item.Sasia;
                db.SaveChanges();
            }
        }

        [HttpPost]
        public async Task<ActionResult> IPN()
        {
            var ipn = Request.Form.AllKeys.ToDictionary(k => k, k => Request[k]);
            ipn.Add("cmd", "_notify-validate");
            string sAmountPaid = Request.Form["mc_gross"];
            string paymentStatus = Request.Form["payment_status"];
            string customField = Request.Form["custom"];
            string numberofitems = Request.Form["num_cart_items"];
            var isIpnValid = await ValidateIpnAsync(ipn);

            if (isIpnValid)
            {

                for (int i = 1; i <= Convert.ToInt32(numberofitems); i++)
                {
                    string buyerEmail = Request.Form["payer_email"];
                    string transactionID = Request.Form["txn_id"];
                    string firstName = Request.Form["first_name"];
                    string lastName = Request.Form["last_name"];
                    string str1 = ("item_name" + i).ToString();
                    string itemName = Request.Form[str1];
                    string str2 = ("item_number" + i).ToString();
                    string itemNumber = Request.Form[str2];
                    string currenci = Request.Form["mc_currency"];
                    string str3 = ("quantity" + i).ToString();
                    string sasia = Request.Form[str3];


                    string cs = ConfigurationManager.ConnectionStrings["StoreContext"].ConnectionString;

                    SqlConnection con = new SqlConnection(cs);
                    SqlCommand cmd = new SqlCommand("PaypalTransaction", con);
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transactionID", transactionID);
                    cmd.Parameters.AddWithValue("@payment", sAmountPaid);
                    cmd.Parameters.AddWithValue("@status", paymentStatus);
                    cmd.Parameters.AddWithValue("@shisheId", itemNumber);
                    cmd.Parameters.AddWithValue("@time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@valuta", currenci);
                    cmd.Parameters.AddWithValue("@sasiaPorosi", sasia);
                    cmd.Parameters.AddWithValue("@shishe", itemName);
                    cmd.Parameters.AddWithValue("@email", buyerEmail);

                    cmd.ExecuteNonQuery();


                }
            }

            return new EmptyResult();
        }

        private static async Task<bool> ValidateIpnAsync(IEnumerable<KeyValuePair<string, string>> ipn)
        {
            using (var client = new HttpClient())
            {
                const string PayPalUrl = "https://ipnpb.sandbox.paypal.com/cgi-bin/webscr";

                // This is necessary in order for PayPal to not resend the IPN.
                await client.PostAsync(PayPalUrl, new StringContent(string.Empty));

                var response = await client.PostAsync(PayPalUrl, new FormUrlEncodedContent(ipn));

                var responseString = await response.Content.ReadAsStringAsync();
                return (responseString == "VERIFIED");
            }
        }
    }
}