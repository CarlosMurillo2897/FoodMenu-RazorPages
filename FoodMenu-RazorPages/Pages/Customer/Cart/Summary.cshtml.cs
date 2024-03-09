using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using FoodMenu.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FoodMenu_RazorPages.Pages.Customer.Cart
{
    [Authorize]
    [BindProperties]
    public class SummaryModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHeader OrderHeader { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OrderHeader = new OrderHeader();
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
                    filter: x => x.ApplicationUserID == claim.Value,
                    includeProperties: new[] { nameof(MenuItem), $"{nameof(MenuItem)}.{nameof(FoodType)}", $"{nameof(MenuItem)}.{nameof(Category)}" }
                );
                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
                }
                ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == claim.Value);
                OrderHeader.PickupName = $"{applicationUser.FirstName} {applicationUser.LastName}";
                OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
            }
        }

        public IActionResult OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
                    filter: x => x.ApplicationUserID == claim.Value,
                    includeProperties: new[] { nameof(MenuItem), $"{nameof(MenuItem)}.{nameof(FoodType)}", $"{nameof(MenuItem)}.{nameof(Category)}" }
                );
                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
                }
                OrderHeader.Status = OrderStatus.StatusPending;
                OrderHeader.OrderDate = System.DateTime.Now;
                OrderHeader.UserID = claim.Value;
                OrderHeader.PickupTime = Convert.ToDateTime($"{OrderHeader.PickupDate.ToShortDateString()} {OrderHeader.PickupTime.ToShortTimeString()}");

                _unitOfWork.OrderHeader.Add(OrderHeader);
                _unitOfWork.Save();

                foreach (var item in ShoppingCartList)
                {
                    OrderDetails orderDetail = new OrderDetails
                    {
                        MenuItemID = item.MenuItemID,
                        OrderID = OrderHeader.ID,
                        Name = item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        Count = item.Count
                    };
                    _unitOfWork.OrderDetails.Add(orderDetail);
                }
                int quantity = ShoppingCartList.ToList().Count();
                _unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList);
                _unitOfWork.Save();

                var domain = "https://localhost:44320/";
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>(),
                    PaymentMethodTypes = new List<string>
                    {
                        "card"
                    },
                    Mode = "payment",
                    SuccessUrl = domain + $"/customer/cart/OrderConfirmation?id={OrderHeader.ID}",
                    CancelUrl = domain + "/customer/cart/index",
                };

                // Add Line Items.
                foreach (var item in ShoppingCartList)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.MenuItem.Price * 100),
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.MenuItem.Name,
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new SessionService();
                Session session = service.Create(options);

                Response.Headers.Add("Location", session.Url);
                 OrderHeader.SessionID = session.Id;
                 OrderHeader.PaymentIntentID = session.PaymentIntentId;
                return new StatusCodeResult(303);
            }
            return Page();
        }
    }
}
