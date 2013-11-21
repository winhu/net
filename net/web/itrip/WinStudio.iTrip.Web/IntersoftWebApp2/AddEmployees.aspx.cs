using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntersoftWebApp2
{
    public partial class AddEmployees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["keyValue"]))
                {
                    northWindDataContext northWindData = new northWindDataContext();

                    var employeeCol =
                        from em in northWindData.Employees
                        where em.EmployeeID == int.Parse(Request["keyValue"].ToString())
                        select em;

                    var employee = employeeCol.First();

                    wiFirstName.Text = employee.FirstName;
                    wiLastName.Text = employee.LastName;
                    wcTitle.Value = employee.Title;
                    wiBirthDate.Text = employee.BirthDate.ToString();
                    wiHireDate.Text = employee.HireDate.ToString();
                    wcCountry.Value = employee.Country;
                    wiPhone.Text = employee.HomePhone;
                    wiAddress.Text = employee.Address;
                }
            }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetValidUntilExpires(false);
        }

        protected void WebToolBar1_CommandClick(object sender, ISNet.WebUI.WebDesktop.ToolCommandClickEventArgs e)
        {
            if (e.ClickedCommand != null)
            {
                northWindDataContext northWindData = new northWindDataContext();
                string msg = "OK";

                try
                {
                    if (!string.IsNullOrEmpty(Request["keyValue"]))
                    {
                        var employeeCol =
                            from em in northWindData.Employees
                            where em.EmployeeID == int.Parse(Request["keyValue"].ToString())
                            select em;

                        var employee = employeeCol.First();

                        employee.FirstName = wiFirstName.Text;
                        employee.LastName = wiLastName.Text;
                        employee.Title = wcTitle.Value;
                        employee.BirthDate = DateTime.Parse(wiBirthDate.Text);
                        employee.HireDate = DateTime.Parse(wiHireDate.Text);
                        employee.Country = wcCountry.Value;
                        employee.HomePhone = wiPhone.Text;
                        employee.Address = wiAddress.Text;

                        northWindData.SubmitChanges();
                    }
                    else
                    {
                        Employee newEmployee = new Employee()
                        {
                            LastName = wiLastName.Text,
                            FirstName = wiFirstName.Text,
                            Title = wcTitle.Text,
                            BirthDate = DateTime.Parse(wiBirthDate.Text),
                            HireDate = DateTime.Parse(wiHireDate.Text),
                            Address = wiAddress.Text,
                            Country = wcCountry.Text,
                            HomePhone = wiPhone.Text,
                            City = null,
                            Extension = null,
                            Notes = null,
                            Photo = null,
                            PhotoPath = null,
                            PostalCode = null,
                            Region = null,
                            ReportsTo = null,
                            TitleOfCourtesy = null
                        };

                        northWindData.Employees.InsertOnSubmit(newEmployee);
                        northWindData.SubmitChanges();
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }

                switch (e.ClickedCommand.Name)
                {
                    case "cmdSave":
                        msg = "OK-Save";
                        break;
                    case "cmdSaveAndClose":
                        msg = "OK-SaveAndClose";
                        break;
                }

                Page.ClientScript.RegisterHiddenField("msg", msg);
            }
        }
    }
}