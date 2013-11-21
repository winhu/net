using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntersoftWebApp2
{
    public partial class AddCustomers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["keyValue"]))
                {
                    northWindDataContext northWindData = new northWindDataContext();

                    var customerCol =
                        from c in northWindData.Customers
                        where c.CustomerID == Request["keyValue"]
                        select c;

                    var customer = customerCol.First();

                    wiCustomerID.Text = customer.CustomerID;
                    wiCustomerID.Enabled = false;
                    wiCompanyName.Text = customer.CompanyName;
                    wiContactName.Text = customer.ContactName;
                    wiAddress.Text = customer.Address;
                    wiFax.Text = customer.Fax;
                    wiPostalCode.Text = customer.PostalCode;
                    wiPhone.Text = customer.Phone;
                    wcCity.Value = customer.City;
                    wiContactTitle.Text = customer.ContactTitle;
                    wcCountry.Value = customer.Country;
                    wiRegion.Text = customer.Region;

                    wbAdd.Text = "Edit";
                }
            }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetValidUntilExpires(false);
        }

        protected void wbAdd_Clicked(object sender, ISNet.WebUI.WebDesktop.WebButtonClickedEventArgs e)
        {
            northWindDataContext northWindData = new northWindDataContext();
            string msg = "OK";

            try
            {
                if (!string.IsNullOrEmpty(Request["keyValue"]))
                {
                    var customerCol =
                        from cs in northWindData.Customers
                        where cs.CustomerID == Request["keyValue"].ToString()
                        select cs;

                    var customer = customerCol.First();

                    customer.CustomerID = wiCustomerID.Value.ToString();
                    customer.CompanyName = wiCompanyName.Text;
                    customer.ContactName = wiContactName.Text;
                    customer.ContactTitle = wiContactTitle.Text;
                    customer.Country = wcCountry.Value;
                    customer.Address = wiAddress.Text;
                    customer.City = wcCity.Text;
                    customer.Region = wiRegion.Text;
                    customer.PostalCode = wiPostalCode.Text;
                    customer.Phone = wiPhone.Text;
                    customer.Fax = wiFax.Text;

                    northWindData.SubmitChanges();
                }
                else
                {
                    Customer newCustomer = new Customer()
                    {
                        CustomerID = wiCustomerID.Value.ToString(),
                        CompanyName = wiCompanyName.Text,
                        ContactName = wiContactName.Text,
                        ContactTitle = wiContactTitle.Text,
                        Country = wcCountry.Value,
                        Address = wiAddress.Text,
                        City = wcCity.Text,
                        Region = wiRegion.Text,
                        PostalCode = wiPostalCode.Text,
                        Phone = wiPhone.Text,
                        Fax = wiFax.Text
                    };

                    northWindData.Customers.InsertOnSubmit(newCustomer);
                    northWindData.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            Page.ClientScript.RegisterHiddenField("msg", msg);
        }
    }
}