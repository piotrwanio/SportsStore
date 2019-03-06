using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";
        public object BindModel(ControllerContext  controllerContext, ModelBindingContext bindingContext)
        {
            //get Cart object from session
            Cart cart = (Cart)controllerContext.HttpContext.Session[sessionKey];

            //creating Cart object if there was no cart in session
            if(cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[sessionKey] = cart;
            }

            //returning shopping cart
            return cart;
        }
    }
}