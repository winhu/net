using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ISNet.WebUI.WebGrid;

namespace IntersoftWebApp2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //delete customer
        protected void WebGrid1_InitializePostBack(object sender, PostbackEventArgs e)
        {
            if (e.Action == "Custom")
            {
                if (!string.IsNullOrEmpty(Request["keyValue"]))
                {
                    WebGrid grid = (WebGrid)sender;

                    northWindDataContext northWindData = new northWindDataContext();

                    var customerCol =
                        from cs in northWindData.Customers
                        where cs.CustomerID == Request["keyValue"].ToString()
                        select cs;

                    var customer = customerCol.First();
                    northWindData.Customers.DeleteOnSubmit(customer);
                    northWindData.SubmitChanges();

                    grid.ClientAction.Refresh();
                }
            }
        }

        //delete employee
        protected void WebGrid2_InitializePostBack(object sender, ISNet.WebUI.WebGrid.PostbackEventArgs e)
        {
            if (e.Action == "Custom")
            {
                if (!string.IsNullOrEmpty(Request["keyValue"]))
                {
                    WebGrid grid = (WebGrid)sender;

                    northWindDataContext northWindData = new northWindDataContext();

                    var employeeCol =
                        from em in northWindData.Employees
                        where em.EmployeeID == int.Parse(Request["keyValue"].ToString())
                        select em;

                    var employee = employeeCol.First();
                    northWindData.Employees.DeleteOnSubmit(employee);
                    northWindData.SubmitChanges();

                    grid.ClientAction.Refresh();
                }
            }
        }
    }
}