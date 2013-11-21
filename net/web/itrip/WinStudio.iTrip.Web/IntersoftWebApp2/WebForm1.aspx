<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="IntersoftWebApp2.WebForm1" %>
<%@ Register Assembly="ISNet.WebUI.WebDesktop" Namespace="ISNet.WebUI.WebDesktop"
    TagPrefix="ISWebDesktop" %>
<%@ Register Assembly="ISNet.WebUI.WebGrid" Namespace="ISNet.WebUI.WebGrid" TagPrefix="ISWebGrid" %>
<%@ Register Assembly="ISNet.WebUI.ISDataSource, Version=1.0.1500.1, Culture=neutral, PublicKeyToken=c4184ef0d326354b"
    Namespace="ISNet.WebUI.DataSource" TagPrefix="ISDataSource" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="Styles/Default.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Default.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" class="DefaultText" style="width: 100%; height: 100%;">
        <tr style="height: 75px;">
            <td style="width: 100%; background-image: url(images/header.png); color: White;
                font-size: 12pt; text-align: right; vertical-align: bottom;">
                Welcome, Admin &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr style="height: 100%;">
            <td>
                <div id="Div1" runat="server" style="height: 100%;">
                    <table id="PlaceHolderManager1_phm" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                        border-collapse: collapse; height: 100%">
                        <tr>
                            <td id="PlaceHolderManager1_phmtda" colspan="3" style="overflow: hidden; height: 1px">
                                <table id="PlaceHolderManager1_phmtda_phmtblda" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 100%; border-collapse: collapse">
                                    <tr>
                                        <td id="PlaceHolderManager1_phmtda_phmtblda_0" align="left" style="overflow: hidden;
                                            height: 1px; background-image: url(images/grdhead_bk.png);" valign="top">
                                            <ISWebDesktop:WebMenuBar ID="WebMenuBar1" runat="server" DockingArea="Top" PlaceHolder="PlaceHolderManager1"
                                                DisplayMode="TextAndImage" HandleVisible="No">
                                                <Menus>
                                                    <ISWebDesktop:MenuCommand Category="WebMenuBar1" Image="./images/view.gif" Name="cmdFile"
                                                        Text="Menu">
                                                        <Items>
                                                            <ISWebDesktop:WebMenuItem ImageURL="./images/customer.png" Name="mnuCustomers" Text="Customers">
                                                            </ISWebDesktop:WebMenuItem>
                                                            <ISWebDesktop:WebMenuItem ImageURL="./images/supplier.gif" Name="mnuSuppliers" Text="Suppliers">
                                                            </ISWebDesktop:WebMenuItem>
                                                            <ISWebDesktop:WebMenuItem Name="mnuProducts" Text="Products">
                                                            </ISWebDesktop:WebMenuItem>
                                                            <ISWebDesktop:WebMenuItem Name="mnuChangePassword" Text="Change Password">
                                                            </ISWebDesktop:WebMenuItem>
                                                        </Items>
                                                    </ISWebDesktop:MenuCommand>
                                                    <ISWebDesktop:MenuCommand Category="WebMenuBar1" Image="./images/LogOff-16.gif" Name="cmdLogout"
                                                        Text="Logout">
                                                    </ISWebDesktop:MenuCommand>
                                                </Menus>
                                                <CommandClientSideEvents OnClick="OnMenuClick" />
                                            </ISWebDesktop:WebMenuBar>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="PlaceHolderManager1_phmtda_phmtblda_1" align="left" style="overflow: hidden;
                                            height: 1px" valign="top">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td id="PlaceHolderManager1_phmlda" style="overflow: hidden; width: 1px">
                                <table id="PlaceHolderManager1_phmlda_phmtblda" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 1px; border-collapse: collapse; height: 100%">
                                    <tr>
                                        <td id="PlaceHolderManager1_phmlda_phmtblda_0" align="left" style="overflow: hidden;
                                            width: 1px" valign="top">
                                        </td>
                                        <td id="PlaceHolderManager1_phmlda_phmtblda_1" align="left" style="overflow: hidden;
                                            width: 1px" valign="top">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td id="PlaceHolderManager1_phmic" style="width: 100%; height: 100%">
                                <ISWebDesktop:WebPaneManager ID="WebPaneManager1" runat="server" Height="100%" Width="100%">
                                    <RootGroupPane GroupType="VerticalTile" Name="RootGroupPane" Text="Root GroupPane">
                                        <Panes>
                                            <ISWebDesktop:WebPane AllowCollapse="Yes" Name="Side" Text="Side" Width="Custom"
                                                ContentScrollable="False" WidthValue="230px">
                                                <ContentTemplate>
                                                    <ISWebDesktop:WebExplorerPane ID="WebExplorerPane1" runat="server" Height="100%"
                                                        Width="100%" RenderMode="RoundCorner">
                                                        <FrameStyle BackColor="White" BorderColor="#E0E0E0" BorderWidth="1px" BorderStyle="Solid"
                                                            BackgroundImage="url(./images/bg2.png)">
                                                            <Padding Bottom="5px" Left="7px" Right="7px" Top="3px" />
                                                        </FrameStyle>
                                                        <Panes>
                                                            <ISWebDesktop:WebExplorerBar CaptionDisplayMode="TextAndImage" ContentMode="UseItems"
                                                                Image="./Images/customer-large.png" ImageSize="30px" Name="barEditCustomers" Text="Customers">
                                                                <PaneItems>
                                                                    <ISWebDesktop:WebExplorerBarItem DisplayMode="TextAndImage" Image="./Images/add.gif"
                                                                        Name="itm_Add" Text="Add Customer" />
                                                                    <ISWebDesktop:WebExplorerBarItem DisplayMode="TextAndImage" Image="./Images/EditExplorer.gif"
                                                                        Name="itm_Edit" Text="Edit Customer" />
                                                                    <ISWebDesktop:WebExplorerBarItem DisplayMode="TextAndImage" Image="./Images/delete.gif"
                                                                        Name="itm_Delete" Text="Delete Customer" />
                                                                </PaneItems>
                                                            </ISWebDesktop:WebExplorerBar>
                                                            <ISWebDesktop:WebExplorerBar CaptionDisplayMode="TextAndImage" ContentMode="UseItems"
                                                                Image="./Images/employee-large.png" ImageSize="30px" Name="barEditEmployees"
                                                                Text="Employees">
                                                                <PaneItems>
                                                                    <ISWebDesktop:WebExplorerBarItem DisplayMode="TextAndImage" Image="./Images/add.gif"
                                                                        Name="itm_Add" Text="Add Employee" />
                                                                    <ISWebDesktop:WebExplorerBarItem DisplayMode="TextAndImage" Image="./Images/EditExplorer.gif"
                                                                        Name="itm_Edit" Text="Edit Employee" />
                                                                    <ISWebDesktop:WebExplorerBarItem DisplayMode="TextAndImage" Image="./Images/delete.gif"
                                                                        Name="itm_Delete" Text="Delete Employee" />
                                                                </PaneItems>
                                                            </ISWebDesktop:WebExplorerBar>
                                                        </Panes>
                                                        <PaneItemSettings>
                                                            <ItemStyle>
                                                                <Active BaseStyle="Over" Font-Bold="True">
                                                                </Active>
                                                                <Over BaseStyle="Normal" BackColor="#E5F3FA" BackColor2="208, 236, 252" BorderColor="#B3E4F9"
                                                                    BorderStyle="Solid" BorderWidth="1px" GradientType="Vertical">
                                                                    <Padding Bottom="3px" Left="6px" Right="6px" Top="3px" />
                                                                </Over>
                                                                <Normal Cursor="Hand" Font-Names="segoe ui,tahoma" Font-Size="9pt" Overflow="Hidden"
                                                                    ForeColor="#0066D4">
                                                                    <Padding Bottom="4px" Left="7px" Right="7px" Top="4px" />
                                                                </Normal>
                                                            </ItemStyle>
                                                            <ItemsContainerStyle BackColor="White" Font-Names="Tahoma" Font-Size="8.25pt">
                                                                <Padding Bottom="4px" Left="4px" Right="4px" Top="4px" />
                                                            </ItemsContainerStyle>
                                                        </PaneItemSettings>
                                                        <PaneSettings>
                                                            <SpecialBarFontStyle>
                                                                <Normal Font-Names="segoe ui,tahoma" Font-Size="9pt" ForeColor="Gray">
                                                                </Normal>
                                                            </SpecialBarFontStyle>
                                                            <BarFrameStyle BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="1px">
                                                                <BorderSettings>
                                                                    <Left Color="224, 224, 224" Style="none" Width="1px" />
                                                                    <Bottom Color="224, 224, 224" Style="none" Width="1px" />
                                                                    <Right Color="224, 224, 224" Style="none" Width="1px" />
                                                                </BorderSettings>
                                                            </BarFrameStyle>
                                                            <BarStyle>
                                                                <Over BackColor="#E0E0E0" BackgroundImage="url(/CommonLibrary/Images/WebDesktop/ep_vis_header.gif)">
                                                                </Over>
                                                                <Normal BackColor="#FCFCFC" Cursor="Hand" Font-Names="segoe ui,tahoma" Font-Size="9pt"
                                                                    Overflow="Hidden" OverflowX="Hidden" OverflowY="Hidden" ForeColor="#003399">
                                                                    <BorderSettings>
                                                                        <Bottom Color="100, 146, 179" Style="solid" Width="1px" />
                                                                    </BorderSettings>
                                                                </Normal>
                                                            </BarStyle>
                                                            <BarFontStyle>
                                                                <Normal Font-Names="segoe ui,tahoma" Font-Size="9pt" ForeColor="#003399">
                                                                </Normal>
                                                            </BarFontStyle>
                                                            <SpecialBarStyle>
                                                                <Over BackgroundImage="url(/CommonLibrary/Images/WebDesktop/ep_vis_header.gif)">
                                                                </Over>
                                                                <Normal BackColor="White" Cursor="Hand" Font-Names="segoe ui,tahoma" Font-Size="9pt"
                                                                    ForeColor="Gray" Overflow="Hidden" OverflowX="Hidden" OverflowY="Hidden">
                                                                    <BorderSettings>
                                                                        <Bottom Color="100, 146, 179" Style="solid" Width="1px" />
                                                                    </BorderSettings>
                                                                </Normal>
                                                            </SpecialBarStyle>
                                                            <ImagesSettings CollapseImage="collapse_XPsilver.gif" CollapseImageOver="collapse_XPsilver_over.gif"
                                                                ExpandImage="expand_XPsilver.gif" ExpandImageOver="expand_XPsilver_over.gif"
                                                                SpecialCollapseImage="special_collapse_XPsilver.gif" SpecialCollapseImageOver="special_collapse_XPsilver.gif"
                                                                SpecialExpandImage="special_expand_XPsilver.gif" SpecialExpandImageOver="special_expand_XPsilver.gif" />
                                                        </PaneSettings>
                                                        <ClientSideEvents OnPaneItemClick="WebExplorerPane1_OnPageItemClick" />
                                                    </ISWebDesktop:WebExplorerPane>
                                                </ContentTemplate>
                                            </ISWebDesktop:WebPane>
                                            <ISWebDesktop:WebPane Name="Content" Text="Content" ContentScrollable="False">
                                                <ContentTemplate>
                                                    <ISWebDesktop:WebTab ID="WebTab1" runat="server" Height="100%" Width="100%" AllowTextWrapping="False"
                                                        DefaultTabSeparatorWidth="0px" RenderMode="ComplexImages" TabSeparator="False">
                                                        <FrameStyle Overflow="Hidden" OverflowX="Hidden" OverflowY="Hidden" BackColor="#BFDBFF">
                                                            <Padding Top="5px" />
                                                            <padding top="5px" />
                                                        </FrameStyle>
                                                        <ContainerStyle BackColor="White" BorderColor="#8DB2E3" BorderStyle="Solid" BorderWidth="1px"
                                                            Height="100%" Overflow="Hidden" OverflowX="Hidden" OverflowY="Hidden" Width="100%"
                                                            Font-Names="segoe ui,tahoma" Font-Size="9pt">
                                                        </ContainerStyle>
                                                        <RoundCornerSettings FillerBorderColor="255, 199, 60" TopBorderColor="230, 139, 44" />
                                                        <roundcornersettings fillerbordercolor="255, 199, 60" 
                                                            topbordercolor="230, 139, 44" />
                                                        <DisabledStyle ForeColor="Gray">
                                                        </DisabledStyle>
                                                        <ClientSideEvents OnAfterTabChanged="WebTab1_OnAfterTabChanged" />
                                                        <TabPages>
                                                            <ISWebDesktop:WebTabItem Name="Tab1" Text="Customers" CaptionDisplayMode="TextAndImage"
                                                                Image="./images/customer.png">
                                                                <PageTemplate>
                                                                    <ISWebGrid:WebGrid ID="WebGrid1" runat="server" Height="100%" Width="100%" DefaultStyleMode="Win7"
                                                                        UseDefaultStyle="True" DataSourceID="LinqDataSource1" 
                                                                        oninitializepostback="WebGrid1_InitializePostBack">
                                                                        <LayoutSettings GridLines="Vertical" AllowFilter="Yes" PagingMode="ClassicPaging">
                                                                            <ClientSideEvents OnRowContextMenu="WebGrid1_OnRowContextMenu" />
                                                                        </LayoutSettings>
                                                                        <RootTable DataKeyField="CustomerID" Caption="Customers">
                                                                            <Columns>
                                                                                <ISWebGrid:WebGridColumn Caption="CustomerID" DataMember="CustomerID" Name="CustomerID"
                                                                                    Width="100px">
                                                                                </ISWebGrid:WebGridColumn>
                                                                                <ISWebGrid:WebGridColumn Caption="CompanyName" DataMember="CompanyName" Name="CompanyName"
                                                                                    Width="100px">
                                                                                </ISWebGrid:WebGridColumn>
                                                                                <ISWebGrid:WebGridColumn Caption="ContactName" DataMember="ContactName" Name="ContactName"
                                                                                    Width="100px">
                                                                                </ISWebGrid:WebGridColumn>
                                                                                <ISWebGrid:WebGridColumn Caption="ContactTitle" DataMember="ContactTitle" Name="ContactTitle"
                                                                                    Width="100px">
                                                                                </ISWebGrid:WebGridColumn>
                                                                                <ISWebGrid:WebGridColumn Caption="Address" DataMember="Address" Name="Address" 
                                                                                    Width="100px">
                                                                                </ISWebGrid:WebGridColumn>
                                                                                <ISWebGrid:WebGridColumn Caption="City" DataMember="City" Name="City" 
                                                                                    Width="100px">
                                                                                </ISWebGrid:WebGridColumn>
                                                                                <ISWebGrid:WebGridColumn Caption="Country" DataMember="Country" Name="Country" Width="100px">
                                                                                </ISWebGrid:WebGridColumn>
                                                                            </Columns>
                                                                        </RootTable>
                                                                    </ISWebGrid:WebGrid>
                                                                </PageTemplate>
                                                            </ISWebDesktop:WebTabItem>
                                                            <ISWebDesktop:WebTabItem Name="WebTabItem0" Text="Employees" CaptionDisplayMode="TextAndImage"
                                                                Image="./images/employee.png">
                                                                <PageTemplate>
                                                                    <ISWebGrid:WebGrid ID="WebGrid2" runat="server" Height="100%" Width="100%" DefaultStyleMode="Win7"
                                                                        UseDefaultStyle="True" DataSourceID="LinqDataSource2" 
                                                                        oninitializepostback="WebGrid2_InitializePostBack">
                                                                        <LayoutSettings GridLines="Vertical" AllowFilter="Yes" PagingMode="ClassicPaging">
                                                                            <ClientSideEvents OnRowContextMenu="WebGrid2_OnRowContextMenu" />
                                                                        </LayoutSettings>
                                                                        <RootTable DataKeyField="EmployeeID" Caption="Orders">
                                                                            <Columns>
                                                                                <ISWebGrid:WebGridColumn DataMember="EmployeeID" Caption="EmployeeID" Width="100px"
                                                                                    DataType="System.Int32" Name="EmployeeID">
                                                                                </ISWebGrid:WebGridColumn>
                                                                                <ISWebGrid:WebGridColumn DataMember="LastName" Caption="LastName" Width="100px" Name="LastName">
                                                                                </ISWebGrid:WebGridColumn>
                                                                                <ISWebGrid:WebGridColumn DataMember="FirstName" Caption="FirstName" Width="100px"
                                                                                    Name="FirstName">
                                                                                </ISWebGrid:WebGridColumn>
                                                                                <ISWebGrid:WebGridColumn DataMember="Title" Caption="Title" Width="100px" Name="Title">
                                                                                </ISWebGrid:WebGridColumn>
                                                                                <ISWebGrid:WebGridColumn Caption="BirthDate" DataMember="BirthDate" DataType="System.DateTime"
                                                                                    Name="BirthDate" Width="100px">
                                                                                </ISWebGrid:WebGridColumn>
                                                                                <ISWebGrid:WebGridColumn Caption="HireDate" DataMember="HireDate" DataType="System.DateTime"
                                                                                    Name="HireDate" Width="100px">
                                                                                </ISWebGrid:WebGridColumn>
                                                                                <ISWebGrid:WebGridColumn Caption="Address" DataMember="Address" Name="Address" Width="100px">
                                                                                </ISWebGrid:WebGridColumn>
                                                                                <ISWebGrid:WebGridColumn Caption="Country" DataMember="Country" Name="Country" Width="100px">
                                                                                </ISWebGrid:WebGridColumn>
                                                                                <ISWebGrid:WebGridColumn Caption="HomePhone" DataMember="HomePhone" Name="HomePhone"
                                                                                    Width="100px">
                                                                                </ISWebGrid:WebGridColumn>
                                                                            </Columns>
                                                                        </RootTable>
                                                                    </ISWebGrid:WebGrid>
                                                                </PageTemplate>
                                                            </ISWebDesktop:WebTabItem>
                                                        </TabPages>
                                                        <TabItemStyle>
                                                            <Active BaseStyle="Normal" BackgroundImage="url(/CommonLibrary/Images/WebDesktop/blank.gif)">
                                                            </Active>
                                                            <Over BackColor="WhiteSmoke" BaseStyle="Normal" BorderColor="#3C7FB1">
                                                            </Over>
                                                            <Normal Cursor="Hand" Font-Names="segoe ui,tahoma" Font-Size="9pt" Height="100%"
                                                                Width="150px" ForeColor="#38428B">
                                                                <Padding Bottom="0px" Left="10px" Right="10px" Top="4px" />
                                                                <padding bottom="0px" left="10px" right="10px" top="4px" />
                                                            </Normal>
                                                        </TabItemStyle>
                                                        <ImagesSettings ActiveCenter="tabo7_center_active.gif" ActiveLeft="tabo7_left_active.gif"
                                                            ActiveRight="tabo7_right_active.gif" NormalCenter="tabo7_normal.gif" NormalLeft="tabo7_normal.gif"
                                                            NormalRight="tabo7_normal.gif" OverCenter="tabo7_center_over.gif" OverLeft="tabo7_left_over.gif"
                                                            OverRight="tabo7_right_over.gif" />
                                                        <clientsideevents onaftertabchanged="WebTab1_OnAfterTabChanged" />
                                                        <imagessettings activecenter="tabo7_center_active.gif" 
                                                            activeleft="tabo7_left_active.gif" activeright="tabo7_right_active.gif" 
                                                            normalcenter="tabo7_normal.gif" normalleft="tabo7_normal.gif" 
                                                            normalright="tabo7_normal.gif" overcenter="tabo7_center_over.gif" 
                                                            overleft="tabo7_left_over.gif" overright="tabo7_right_over.gif" />
                                                    </ISWebDesktop:WebTab>
                                                </ContentTemplate>
                                            </ISWebDesktop:WebPane>
                                        </Panes>
                                    </RootGroupPane>
<ImagesSettings SplitterGripLeft="SplitterLeft.gif" SplitterGripRight="SplitterRight.gif"></ImagesSettings>

                                    <FrameStyle BackColor="Transparent" ForeColor="white">
                                        <Padding Bottom="1px" Left="1px" Right="1px" Top="1px" />
<Padding Top="1px" Left="1px" Right="1px" Bottom="1px"></Padding>
                                    </FrameStyle>
                                    <SplitterStyle>
                                        <Active BackColor="Black" BaseStyle="Normal">
                                        </Active>
                                        <Normal BackColor="Transparent">
                                        </Normal>
                                        <Over BaseStyle="Normal">
                                        </Over>
                                    </SplitterStyle>
                                    <PaneSettings HeaderVisible="No">
                                        <HeaderSubStyle BackColor="#B8D1F8" BorderColor="#002D96" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Tahoma" Font-Size="8pt">
                                            <Padding Bottom="4px" Left="4px" Right="4px" Top="4px" />
<Padding Top="4px" Left="4px" Right="4px" Bottom="4px"></Padding>
                                        </HeaderSubStyle>
                                        <InfoTextStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="White">
                                        </InfoTextStyle>
                                        <ContainerStyle BackColor="white" Font-Names="Segoe UI, Tahoma" Font-Size="8pt" BorderColor="lightsteelblue"
                                            BorderWidth="1px" BorderStyle="solid" ForeColor="black" Overflow="auto" OverflowX="auto"
                                            OverflowY="auto">
                                            <Padding Bottom="1px" Left="1px" Right="1px" Top="1px" />
<Padding Top="1px" Left="1px" Right="1px" Bottom="1px"></Padding>
                                        </ContainerStyle>
                                        <HeaderMainStyle BackColor="#5987D6" BackColor2="7, 59, 150" BorderColor="#002D96"
                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                            ForeColor="White" GradientType="Vertical">
                                            <Padding Bottom="4px" Left="4px" Right="4px" Top="4px" />
<Padding Top="4px" Left="4px" Right="4px" Bottom="4px"></Padding>
                                        </HeaderMainStyle>
                                    </PaneSettings>
                                    <ImagesSettings SplitterGripLeft="SplitterLeft.gif" SplitterGripRight="SplitterRight.gif" />
                                </ISWebDesktop:WebPaneManager>
                            </td>
                            <td id="PlaceHolderManager1_phmrda" style="overflow: hidden; width: 1px">
                                <table id="PlaceHolderManager1_phmrda_phmtblda" border="0" cellpadding="0" cellspacing="0"
                                    style="width: 1px; border-collapse: collapse; height: 100%">
                                    <tr>
                                        <td id="PlaceHolderManager1_phmrda_phmtblda_0" align="left" style="overflow: hidden;
                                            width: 1px" valign="top">
                                        </td>
                                        <td id="PlaceHolderManager1_phmrda_phmtblda_1" align="left" style="overflow: hidden;
                                            width: 1px" valign="top">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td id="PlaceHolderManager1_phmbda" colspan="3" style="overflow: hidden; height: 1px">
                                &nbsp;
            &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <ISWebDesktop:PlaceHolderManager ID="PlaceHolderManager1" runat="server" IsDefaultPagePlaceHolderManager="True"
                    IsDesignInitialize="True" PlaceHolderContainer="Div1" AllowCustomize="No">
                    <WebToolBarStyleSettings>
                        <BodyStyle BackColor="Transparent" ForeColor="Transparent" />
                        <CommandDisabledStyle Font-Names="Segoe UI,Tahoma" Font-Size="8pt" ForeColor="DimGray">
                            <Padding Bottom="1px" Left="3px" Right="3px" Top="1px" />
                        </CommandDisabledStyle>
                        <HeaderButtonStyle>
                            <Active BackColor="#FE9855" BaseStyle="Over" BorderColor="#4B4B6F" Cursor="Hand">
                            </Active>
                            <Over BackColor="#FFF2C8" BaseStyle="Normal" BorderColor="#316AC5" BorderStyle="Solid"
                                BorderWidth="1px" Cursor="Hand" ForeColor="Black">
                                <Padding Bottom="0px" Left="0px" Right="0px" Top="0px" />
                            </Over>
                            <Normal BackColor="#2A66C9" Cursor="Hand" Font-Size="8pt" ForeColor="White" Width="13px">
                                <Padding Bottom="1px" Left="1px" Right="1px" Top="1px" />
                            </Normal>
                        </HeaderButtonStyle>
                        <SeparatorStyle>
                            <BorderSettings>
                                <Left Color="154, 198, 255" Style="solid" Width="1px" />
                            </BorderSettings>
                        </SeparatorStyle>
                        <OptionStyle BackColor="#D3E6FF" BackColor2="116, 161, 220" ForeColor="Black" GradientType="Vertical" />
                        <CommandStyle>
                            <Active BackColor="#FE9855" BaseStyle="Over">
                            </Active>
                            <Over BackColor="#FFF5CC" BorderColor="#FFBD69" BorderStyle="Solid" BorderWidth="1px"
                                Cursor="Default" Font-Names="Segoe UI,Tahoma" Font-Size="8pt" Overflow="Hidden"
                                OverflowX="Hidden" OverflowY="Hidden" BackColor2="255, 219, 117" GradientType="Vertical">
                                <Padding Bottom="0px" Left="2px" Right="2px" Top="0px" />
                            </Over>
                            <Normal Font-Names="Segoe UI,Tahoma" Font-Size="8pt" Overflow="Hidden" OverflowX="Hidden"
                                OverflowY="Hidden">
                                <Padding Bottom="1px" Left="3px" Right="3px" Top="1px" />
                            </Normal>
                        </CommandStyle>
                        <HeaderCaptionStyle BackColor="#2A66C9" Cursor="Move" Font-Bold="True" Font-Names="Tahoma"
                            Font-Size="8pt" ForeColor="White">
                        </HeaderCaptionStyle>
                        <HandleStyle BackColor="#E3EFFF" BackColor2="177, 211, 255" GradientType="Vertical" />
                    </WebToolBarStyleSettings>
                    <WebMenuBarStyleSettings>
                        <CommandDisabledStyle Font-Names="Segoe UI,Tahoma" Font-Size="8pt" ForeColor="DimGray">
                            <Padding Bottom="1px" Left="3px" Right="3px" Top="1px" />
                        </CommandDisabledStyle>
                        <HeaderButtonStyle>
                            <Active BackColor="#FE9855" BaseStyle="Over" BorderColor="#4B4B6F" Cursor="Hand">
                            </Active>
                            <Over BackColor="#FFF2C8" BaseStyle="Normal" BorderColor="#316AC5" BorderStyle="Solid"
                                BorderWidth="1px" Cursor="Hand" ForeColor="Black">
                                <Padding Bottom="0px" Left="0px" Right="0px" Top="0px" />
                            </Over>
                            <Normal BackColor="#2A66C9" Cursor="Hand" Font-Size="8pt" ForeColor="White" Width="13px">
                                <Padding Bottom="1px" Left="1px" Right="1px" Top="1px" />
                            </Normal>
                        </HeaderButtonStyle>
                        <SeparatorStyle Font-Size="1px">
                            <BorderSettings>
                                <Left Color="106, 140, 203" Style="solid" Width="1px" />
                            </BorderSettings>
                        </SeparatorStyle>
                        <OptionStyle BackColor="#C4C4AD" />
                        <CommandStyle>
                            <Active BackColor="White" BaseStyle="Over" BorderColor="#6593CF" Overflow="Hidden"
                                OverflowX="Hidden" OverflowY="Hidden" BackColor2="144, 185, 238">
                            </Active>
                            <Over BackColor="#FFF4C7" BorderColor="#FFBD69" BorderStyle="Solid" BorderWidth="1px"
                                Cursor="Default" Font-Names="Segoe UI,Tahoma" Font-Size="8pt" Overflow="Hidden"
                                OverflowX="Hidden" OverflowY="Hidden" BackColor2="255, 215, 157" GradientType="Vertical">
                                <Padding Bottom="0px" Left="5px" Right="5px" Top="0px" />
                            </Over>
                            <Normal Font-Names="Segoe UI,Tahoma" Font-Size="8pt" Overflow="Hidden" OverflowX="Hidden"
                                OverflowY="Hidden">
                                <Padding Bottom="1px" Left="6px" Right="6px" Top="1px" />
                            </Normal>
                        </CommandStyle>
                        <HeaderCaptionStyle BackColor="#2A66C9" Cursor="Move" Font-Bold="True" Font-Names="Tahoma"
                            Font-Size="8pt" ForeColor="White">
                        </HeaderCaptionStyle>
                        <HandleStyle BackColor="#BFDBFF" />
                    </WebMenuBarStyleSettings>
                    <DockAreaStyle Overflow="Hidden" OverflowX="Hidden" OverflowY="Hidden">
                    </DockAreaStyle>
                    <MenuStyleSettings BackgroundStripColor="233, 238, 238" BackgroundStripColor2=""
                        MenuAnimation="True" MenuDropShadow="True" MenuWindowType="Normal">
                        <DisabledItemStyle>
                            <Over BackColor="WhiteSmoke" BaseStyle="Normal" BorderColor="#FFBD69" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="Gray">
                            </Over>
                            <Normal Font-Names="segoe ui,tahoma" Font-Size="8.25pt" ForeColor="Silver">
                            </Normal>
                        </DisabledItemStyle>
                        <CheckedItemStyle>
                            <Over BackColor="#FB8C3C" BorderColor="#FB8C3C" BorderStyle="Solid" BorderWidth="1px">
                            </Over>
                            <Normal BackColor="#FFBD69" BorderColor="#FFAB3F" BorderStyle="Solid" BorderWidth="1px">
                            </Normal>
                        </CheckedItemStyle>
                        <SeparatorStyle>
                            <BorderSettings>
                                <Top Color="154, 198, 255" Style="solid" Width="1px" />
                            </BorderSettings>
                        </SeparatorStyle>
                        <FrameStyle BackColor="White" BorderColor="#6593CF" BorderStyle="Solid" BorderWidth="1px">
                        </FrameStyle>
                        <ItemStyle>
                            <Over BackColor="#FFE7A2" BaseStyle="Normal" BorderColor="#FFBD69" BorderStyle="Solid"
                                BorderWidth="1px">
                            </Over>
                            <Normal Font-Names="segoe ui,tahoma" Font-Size="8.25pt">
                            </Normal>
                        </ItemStyle>
                    </MenuStyleSettings>
                </ISWebDesktop:PlaceHolderManager>
                <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
                    ContextTypeName="IntersoftWebApp2.northWindDataContext" EnableDelete="True" 
                    EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="Customers">
                </asp:LinqDataSource>
                <asp:LinqDataSource ID="LinqDataSource2" runat="server" 
                    ContextTypeName="IntersoftWebApp2.northWindDataContext" EnableDelete="True" 
                    EnableInsert="True" EnableUpdate="True" EntityTypeName="" TableName="Employees">
                </asp:LinqDataSource>
            </td>
        </tr>
    </table>
    <ISWebDesktop:WebDialogBox ID="WebDialogBox1" runat="server" Height="420px" Width="430px">
        <WindowSettings ContentMode="UseIFrame" ControlBox="Yes">
            <CommandButtonStyle>
                <Active BaseStyle="Normal" ForeColor="White">
                </Active>
                <Over BaseStyle="Normal">
                </Over>
                <Normal BorderStyle="None" Font-Names="Segoe UI, Tahoma" Font-Size="8pt">
                </Normal>
            </CommandButtonStyle>
            <ContainerStyle Width="100%" Font-Size="8pt" Font-Names="Segoe UI, Tahoma" BackColor="White"
                Height="100%">
                <Padding Top="0px" Left="0px" Bottom="0px" Right="0px"></Padding>
            </ContainerStyle>
            <WindowStyle>
                <Active BorderColor="#769BC7" BaseStyle="Normal">
                </Active>
                <Normal BorderStyle="Solid" BorderWidth="1px" BorderColor="#CAD6E5" Font-Size="8pt"
                    Font-Names="Segoe UI, Tahoma" BackColor="White">
                </Normal>
            </WindowStyle>
            <CaptionStyle>
                <Active ForeColor="White" BackgroundImage="url(./Images/ISWndActive.gif)" BaseStyle="Normal"
                    Font-Bold="True">
                </Active>
                <Normal ForeColor="WhiteSmoke" BackgroundImage="url(./Images/ISWndNormal.gif)" Font-Size="10pt"
                    Font-Names="Segoe UI, Tahoma" Font-Bold="True">
                    <Padding Top="2px" Left="2px" Bottom="2px" Right="2px"></Padding>
                </Normal>
            </CaptionStyle>
            <CaptionButtonStyle>
                <Active ForeColor="#C00000" BaseStyle="Normal">
                    <BorderSettings>
                        <Top Color="Gray"></Top>
                        <Left Color="Gray"></Left>
                    </BorderSettings>
                </Active>
                <Over ForeColor="#C0C0FF" BaseStyle="Normal">
                    <BorderSettings>
                        <Bottom Color="Gray"></Bottom>
                        <Right Color="Gray"></Right>
                    </BorderSettings>
                </Over>
                <Normal Height="17px" Overflow="Hidden" OverflowX="Hidden" OverflowY="Hidden">
                </Normal>
            </CaptionButtonStyle>
            <CommandButtonDisabledStyle BorderStyle="None" ForeColor="Gray" Font-Size="8pt" Font-Names="Segoe UI, Tahoma">
            </CommandButtonDisabledStyle>
        </WindowSettings>
        <ClientSideEvents OnBeforeCreated="wdbPreview_BeforeCreated" OnClosed="wdbPreview_Closed" />
    </ISWebDesktop:WebDialogBox>
    </form>
</body>
</html>
