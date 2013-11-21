// JScript File
function WebExplorerPane1_OnPageItemClick(id, item) 
{
    Editing(item.Text);
}

function Editing(text)
{
    var url = "";
    var dlg = ISGetObject("WebDialogBox1");
    var grid1 = ISGetObject("WebGrid1");
    var grid2 = ISGetObject("WebGrid2");
    var selObj1 = grid1.GetSelectedObject();
    var selObj2 = grid2.GetSelectedObject();
    
    switch(text)
    {
        case "Add Customer" :
            SetDialogBox(dlg, "Add Customer", "./images/customer.png");
                
            dlg.SetContentURL("AddCustomers.aspx");
            dlg.ShowDialog();
            
            break;     
        
        case "Edit Customer" :
            SetDialogBox(dlg, "Edit Customer", "./images/customer.png");
            
            if(selObj1 != null)
                dlg.SetContentURL("AddCustomers.aspx?keyValue=" + selObj1.GetRowObject().KeyValue);
            else
            {
                alert("Please select a customer");
                return false;
            }
            
            dlg.ShowDialog();
            break;
        
        case "Delete Customer" :
            if(selObj1 != null)
            {
                if(confirm("Are you sure you want to delete the selected customer?"))
                {
                    var tab = ISGetObject("WebTab1");
                    var tabItem = tab.TabPages[1];
                    var text = tabItem.GetElement().innerText;
                    
                    grid1.AddInput("keyValue", selObj1.GetRowObject().KeyValue);
                    grid1.SendCustomRequest();                            
                }
            }
            else
            {
                alert("Please select a customer");
                return false;
            }
            break;
            
        case "Add Employee" :
            SetDialogBox(dlg, "Add Employee", "./images/employee.png");
                
            dlg.SetContentURL("AddEmployees.aspx");
            dlg.ShowDialog();
            
            break;     
        
        case "Edit Employee" :
            SetDialogBox(dlg, "Edit Employee", "./images/employee.png");
            
            if(selObj2 != null)
                dlg.SetContentURL("AddEmployees.aspx?keyValue=" + selObj2.GetRowObject().KeyValue);
            else
            {
                alert("Please select an employee");
                return false;
            }
            
            dlg.ShowDialog();
            break;
        
        case "Delete Employee" :
            if(selObj2 != null)
            {
                if(confirm("Are you sure you want to delete the selected employee?"))
                {
                    grid2.AddInput("keyValue", selObj2.GetRowObject().KeyValue);
                    grid2.SendCustomRequest();                            
                }
            }
            else
            {
                alert("Please select an employee");
                return false;
            }
            break;
    }
    
    return true;
}

function SetDialogBox(dlg, text, imagePath)
{
    if(dlg.Window != null)
    {
        dlg.Window.SetCaption(text);
        dlg.Window.SetControlBoxImage(imagePath);
    }
    else
    {
        dlg.Text = text;
        dlg.ControlBoxImage = imagePath;
    }
}

function wdbPreview_BeforeCreated(id, dm)
{
    var dlg = ISGetObject(id);
    var imgSet = dm.ImagesSettings;
    
    imgSet.InProgress = "is_progress-16.gif"
    dm.UseWebResourcesForClient = true;
    
    return true;
}

function wdbPreview_Closed(id)
{
    var dlg = ISGetObject(id);
    dlg.SetContentURL("");
    
    return true;
}

function WebGrid1_OnRowContextMenu(controlId, rowType, rowElement, menuObject)
{
	var addItem = new WebMenuItem();
    addItem.ImageURL = "./Images/add.gif";                
    addItem.Text = "Add Customer";
    addItem.OnClick = function() { Editing('Add Customer') };
    addItem.Name = "addCustomer";    
    menuObject.Items.InsertAt(addItem,0);  
    
    var editItem = new WebMenuItem();
    editItem.ImageURL = "./Images/EditExplorer.gif";                
    editItem.Text = "Edit Customer";
    editItem.OnClick = function() { Editing('Edit Customer') };
    editItem.Name = "editCustomer";    
    menuObject.Items.InsertAt(editItem,1);  
    
    var deleteItem = new WebMenuItem();
    deleteItem.ImageURL = "./Images/delete.gif";                
    deleteItem.Text = "Delete Customer";
    deleteItem.OnClick = function() { Editing('Delete Customer') };
    deleteItem.Name = "deleteCustomer";    
    menuObject.Items.InsertAt(deleteItem,2);  
    
    var separator = new WebMenuItem();
    separator.Type = "Separator";
    separator.Name = "separator";
    
    menuObject.Items.InsertAt(separator,3);

	return true;
}

function WebGrid2_OnRowContextMenu(controlId, rowType, rowElement, menuObject)
{
	var addItem = new WebMenuItem();
    addItem.ImageURL = "./Images/add.gif";                
    addItem.Text = "Add Employee";
    addItem.OnClick = function() { Editing('Add Employee') };
    addItem.Name = "addEmployee";    
    menuObject.Items.InsertAt(addItem,0);  
    
    var editItem = new WebMenuItem();
    editItem.ImageURL = "./Images/EditExplorer.gif";                
    editItem.Text = "Edit Employee";
    editItem.OnClick = function() { Editing('Edit Employee') };
    editItem.Name = "editEmployee";    
    menuObject.Items.InsertAt(editItem,1);  
    
    var deleteItem = new WebMenuItem();
    deleteItem.ImageURL = "./Images/delete.gif";                
    deleteItem.Text = "Delete Employee";
    deleteItem.OnClick = function() { Editing('Delete Employee') };
    deleteItem.Name = "deleteEmployee";    
    menuObject.Items.InsertAt(deleteItem,2);  
    
    var separator = new WebMenuItem();
    separator.Type = "Separator";
    separator.Name = "separator";
    
    menuObject.Items.InsertAt(separator,3);

	return true;
}

function OnMenuClick(controlId, menuObj)
{
    var webMenu = ISGetObject(controlId);
    var menuName = menuObj.Name;
    
    if (menuObj.SelectedMenuItem != null)
        menuName = menuObj.SelectedMenuItem.Name;

    switch (menuName)
    {
        case "cmdFile":
            
            break;
        case "mnuCustomers":
            alert("menu Customer is clicked");
            break;
        case "mnuSuppliers":
            alert("menu Suppliers is clicked");
            break;
        case "mnuProducts":
            alert("menu Products is clicked");
            break;
        case "mnuChangePassword":
            alert("menu Change Password is clicked");
            break;
        case "cmdLogout":
            alert("menu Logout is clicked");
            break;
    }
}

function WebTab1_OnAfterTabChanged(controlId, activeTab, previousTab)
{
    if (IS.safari)
        wgDoResize(true, true);
    
    return true;
}