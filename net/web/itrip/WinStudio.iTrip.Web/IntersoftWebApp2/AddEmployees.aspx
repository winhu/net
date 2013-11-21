<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEmployees.aspx.cs" Inherits="IntersoftWebApp2.AddEmployees" %>
<%@ Register Assembly="ISNet.WebUI.ISDataSource, Version=1.0.1500.1, Culture=neutral, PublicKeyToken=c4184ef0d326354b"
    Namespace="ISNet.WebUI.DataSource" TagPrefix="ISDataSource" %>
<%@ Register Assembly="ISNet.WebUI.WebDesktop" Namespace="ISNet.WebUI.WebDesktop"
    TagPrefix="ISWebDesktop" %>
<%@ Register Assembly="ISNet.WebUI.WebInput" Namespace="ISNet.WebUI.WebControls"
    TagPrefix="ISWebInput" %>
<%@ Register Assembly="ISNet.WebUI.WebCombo" Namespace="ISNet.WebUI.WebCombo" TagPrefix="ISWebCombo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="Styles/Default.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin:0px 0px 0px 0px; overflow:hidden;">
    <form id="form1" runat="server">
    <div id="Div1" runat="server">
        <table id="PlaceHolderManager1_phm" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            border-collapse: collapse; height: 100%;">
            <tr>
                <td id="PlaceHolderManager1_phmtda" colspan="3">
                    <table id="PlaceHolderManager1_phmtda_phmtblda" border="0" cellpadding="0" cellspacing="0"
                        style="width: 100%; border-collapse: collapse">
                        <tr>
                            <td id="PlaceHolderManager1_phmtda_phmtblda_0" align="left" style="overflow: hidden;
                                height: 1px;" valign="top">
                                <ISWebDesktop:WebToolBar ID="WebToolBar1" runat="server" DockingArea="Top" PlaceHolder="PlaceHolderManager1"
                                    Width="100%" Caption="WebToolBar1" CommandSize="" HeaderHeight="" NewDockingArea="NotSet"
                                    NewDockingRow="0" NewIsFloat="Default" BarStyle="ComplexImages" AllowMove="No"
                                    DisplayMode="TextAndImage" AllowCustomize="No" AllowExpandCollapse="No" CommandMargin="0px"
                                    HandleVisible="No" OnCommandClick="WebToolBar1_CommandClick">
                                    <CommandClientSideEvents OnMouseUp="WebToolBar1_OnMouseUp" />
                                    <Commands>
                                        <ISWebDesktop:ToolCommand AutoPostBack="Yes" Category="WebToolBar1" Image="./Images/Save-16.gif"
                                            Name="cmdSave" Text="Save" AccessKey="s">
                                        </ISWebDesktop:ToolCommand>
                                        <ISWebDesktop:ToolCommand AutoPostBack="Yes" Category="WebToolBar1" Image="./Images/SaveAndClose-16.gif"
                                            Name="cmdSaveAndClose" Text="Save and Close">
                                        </ISWebDesktop:ToolCommand>
                                        <ISWebDesktop:ToolCommand Category="WebToolBar1" Name="cmdTool1" Type="Separator">
                                        </ISWebDesktop:ToolCommand>
                                        <ISWebDesktop:ToolCommand Category="WebToolBar1" DisplayMode="TextAndImage" Image="./Images/Delete.gif"
                                            Name="cmdClose" Text="Close">
                                        </ISWebDesktop:ToolCommand>
                                    </Commands>
                                    <MenuStyleSettings HighlightMode="UseComplexImages">
                                        <ItemStyle>
                                            <Normal ForeColor="white">
                                            </Normal>
                                        </ItemStyle>
                                    </MenuStyleSettings>
                                </ISWebDesktop:WebToolBar>
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
                <td id="PlaceHolderManager1_phmic" style="width: 100%; height: 100%;">
                    <table class="DefaultText DefaultTable" style="background-image: url(./images/bgdialog.png);
                        margin: 5px;">
                        <tr>
                            <td style="width: 75px;">
                                First Name</td>
                            <td>
                                :
                            </td>
                            <td>
                                <ISWebInput:WebInput ID="wiFirstName" runat="server" Width="150px">
                                    <CultureInfo CultureName="en-US">
                                    </CultureInfo>
                                </ISWebInput:WebInput>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Last Name
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <ISWebInput:WebInput ID="wiLastName" runat="server"
                                    Width="150px">
                                    <CultureInfo CultureName="en-US">
                                    </CultureInfo>
                                </ISWebInput:WebInput>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Title</td>
                            <td>
                                :
                            </td>
                            <td>
                                <ISWebCombo:WebCombo ID="wcTitle" runat="server" UseDefaultStyle="true" 
                                    Width="150px">
                                    <Columns>
                                        <ISWebCombo:WebComboColumn BaseFieldName="Title" Bound="False" Name="Column0" />
                                    </Columns>
                                    <Rows>
                                        <ISWebCombo:WebComboRow>
                                            <Cells>
                                                <ISWebCombo:WebComboCell Text="Sales Representative">
                                                </ISWebCombo:WebComboCell>
                                            </Cells>
                                        </ISWebCombo:WebComboRow>
                                        <ISWebCombo:WebComboRow Position="1">
                                            <Cells>
                                                <ISWebCombo:WebComboCell Text="Inside Sales Coordinator">
                                                </ISWebCombo:WebComboCell>
                                            </Cells>
                                        </ISWebCombo:WebComboRow>
                                        <ISWebCombo:WebComboRow Position="2">
                                            <Cells>
                                                <ISWebCombo:WebComboCell Text="Sales Manager">
                                                </ISWebCombo:WebComboCell>
                                            </Cells>
                                        </ISWebCombo:WebComboRow>
                                        <ISWebCombo:WebComboRow Position="3">
                                            <Cells>
                                                <ISWebCombo:WebComboCell Text="Vice President, Sales">
                                                </ISWebCombo:WebComboCell>
                                            </Cells>
                                        </ISWebCombo:WebComboRow>
                                    </Rows>
                                </ISWebCombo:WebCombo>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Birth Date</td>
                            <td>
                                :
                            </td>
                            <td>
                                <ISWebInput:WebInput ID="wiBirthDate" runat="server" Width="125px">
                                    <DisplayFormat Format="MM/dd/yyyy" IsEnabled="True">
                                        <ErrorWindowInfo IsEnabled="True">
                                        </ErrorWindowInfo>
                                    </DisplayFormat>
                                    <HighLight IsEnabled="True" />
                                    <CultureInfo CultureName="en-US">
                                    </CultureInfo>
                                    <DateTimeEditor IsEnabled="True">
                                    </DateTimeEditor>
                                </ISWebInput:WebInput>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Hire Date</td>
                            <td>
                                :
                            </td>
                            <td>
                                <ISWebInput:WebInput ID="wiHireDate" runat="server" Width="125px">
                                    <DisplayFormat Format="MM/dd/yyyy" IsEnabled="True">
                                        <ErrorWindowInfo IsEnabled="True">
                                        </ErrorWindowInfo>
                                    </DisplayFormat>
                                    <HighLight IsEnabled="True" />
                                    <CultureInfo CultureName="en-US">
                                    </CultureInfo>
                                    <DateTimeEditor IsEnabled="True">
                                    </DateTimeEditor>
                                </ISWebInput:WebInput>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Country</td>
                            <td>
                                :
                            </td>
                            <td>
                                <ISWebCombo:WebCombo ID="wcCountry" runat="server" UseDefaultStyle="true" 
                                    Width="150px">
                                    <Columns>
                                        <ISWebCombo:WebComboColumn BaseFieldName="Country" Bound="False" Name="Column0" />
                                    </Columns>
                                    <Rows>
                                        <ISWebCombo:WebComboRow>
                                            <Cells>
                                                <ISWebCombo:WebComboCell Text="UK">
                                                </ISWebCombo:WebComboCell>
                                            </Cells>
                                        </ISWebCombo:WebComboRow>
                                        <ISWebCombo:WebComboRow Position="1">
                                            <Cells>
                                                <ISWebCombo:WebComboCell Text="USA">
                                                </ISWebCombo:WebComboCell>
                                            </Cells>
                                        </ISWebCombo:WebComboRow>
                                        <ISWebCombo:WebComboRow Position="2">
                                            <Cells>
                                                <ISWebCombo:WebComboCell Text="Brazil">
                                                </ISWebCombo:WebComboCell>
                                            </Cells>
                                        </ISWebCombo:WebComboRow>
                                        <ISWebCombo:WebComboRow Position="3">
                                            <Cells>
                                                <ISWebCombo:WebComboCell Text="Germany">
                                                </ISWebCombo:WebComboCell>
                                            </Cells>
                                        </ISWebCombo:WebComboRow>
                                        <ISWebCombo:WebComboRow Position="4">
                                            <Cells>
                                                <ISWebCombo:WebComboCell Text="China">
                                                </ISWebCombo:WebComboCell>
                                            </Cells>
                                        </ISWebCombo:WebComboRow>
                                    </Rows>
                                </ISWebCombo:WebCombo>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Home Phone</td>
                            <td>
                                :
                            </td>
                            <td>
                                <ISWebInput:WebInput ID="wiPhone" runat="server" Width="80px">
                                    <HighLight IsEnabled="True" />
                                    <CultureInfo CultureName="en-US">
                                    </CultureInfo>
                                    <EditFormat IsEnabled="True" Type="Other">
                                        <MaskInfo MaskExpression="(999) 000-0000">
                                        </MaskInfo>
                                        <ErrorWindowInfo IsEnabled="True">
                                        </ErrorWindowInfo>
                                    </EditFormat>
                                </ISWebInput:WebInput>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address</td>
                            <td>
                                :
                            </td>
                            <td>
                                <ISWebInput:WebInput ID="wiAddress" runat="server"
                                    Height="55px" Rows="5" TextMode="MultiLine">
                                    <CultureInfo CultureName="en-US">
                                    </CultureInfo>
                                </ISWebInput:WebInput>
                            </td>
                        </tr>
                        <tr style="height: 100%">
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
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
                    <table id="PlaceHolderManager1_phmbda_phmtblda" border="0" cellpadding="0" cellspacing="0"
                        style="width: 100%; border-collapse: collapse">
                        <tr>
                            <td id="PlaceHolderManager1_phmbda_phmtblda_0" align="left" style="overflow: hidden;
                                height: 1px" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td id="PlaceHolderManager1_phmbda_phmtblda_1" align="left" style="overflow: hidden;
                                height: 1px" valign="top">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <ISWebDesktop:PlaceHolderManager ID="PlaceHolderManager1" runat="server" IsDefaultPagePlaceHolderManager="True" AllowCustomize="No"
        IsDesignInitialize="True" PlaceHolderContainer="Div1" BarStyle="Office2000">
        <WebToolBarStyleSettings>
            <BodyStyle BackColor="#5777A7"></BodyStyle>
            <CommandDisabledStyle Cursor="Default" Font-Size="8pt" Font-Names="Segoe UI, Tahoma">
                <Padding Top="1px" Left="3px" Bottom="1px" Right="3px"></Padding>
            </CommandDisabledStyle>
            <HeaderButtonStyle>
                <Active BorderColor="#4B4B6F" BaseStyle="Over" BackColor="#98B5E2" Cursor="Hand">
                </Active>
                <Over BorderStyle="Solid" BorderWidth="1px" BorderColor="#316AC5" BaseStyle="Normal"
                    BackColor="#C1D2EE" Cursor="Hand">
                    <Padding Top="0px" Left="0px" Bottom="0px" Right="0px"></Padding>
                </Over>
                <Normal ForeColor="White" Width="13px" Font-Size="8pt" BackColor="#20334E" Cursor="Hand">
                    <Padding Top="1px" Left="1px" Bottom="1px" Right="1px"></Padding>
                </Normal>
            </HeaderButtonStyle>
            <SeparatorStyle Font-Size="1px" BackColor="#C5C2B8"></SeparatorStyle>
            <OptionStyle BackColor="#6F94C2" ForeColor="White"></OptionStyle>
            <CommandStyle>
                <Active ForeColor="White" BorderColor="White" BaseStyle="Over" BackColor="#324E78"
                    Overflow="Hidden" OverflowX="Hidden" OverflowY="Hidden">
                </Active>
                <Over BorderStyle="Solid" BorderWidth="1px" BorderColor="#316AC5" Font-Size="8pt"
                    Font-Names="Segoe UI, Tahoma" BackColor="#C1D2EE" Overflow="Hidden" Cursor="Default"
                    OverflowX="Hidden" OverflowY="Hidden">
                    <Padding Top="0px" Left="2px" Bottom="0px" Right="2px"></Padding>
                </Over>
                <Normal ForeColor="White" Font-Size="8pt" Font-Names="Segoe UI, Tahoma" Overflow="Hidden"
                    OverflowX="Hidden" OverflowY="Hidden">
                    <Padding Top="1px" Left="3px" Bottom="1px" Right="3px"></Padding>
                </Normal>
            </CommandStyle>
            <HeaderCaptionStyle ForeColor="White" Font-Size="8pt" Font-Names="Tahoma" Font-Bold="True"
                BackColor="#20334E" Cursor="Move">
            </HeaderCaptionStyle>
            <HandleStyle BackColor="#5777A7" ForeColor="White"></HandleStyle>
        </WebToolBarStyleSettings>
        <DockAreaStyle BackColor="#5777A7" Overflow="Hidden" OverflowX="Hidden" OverflowY="Hidden">
        </DockAreaStyle>
        <MenuStyleSettings BackgroundStripColor="87, 119, 167" BackgroundStripColor2="White">
            <FrameStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#20334E" BackColor="White">
            </FrameStyle>
        </MenuStyleSettings>
        <WebMenuBarStyleSettings>
            <BodyStyle BackColor="#5777A7"></BodyStyle>
            <CommandDisabledStyle ForeColor="DimGray" Font-Size="8pt" Font-Names="Tahoma">
                <Padding Top="1px" Left="3px" Bottom="1px" Right="3px"></Padding>
            </CommandDisabledStyle>
            <HeaderButtonStyle>
                <Active BorderColor="#4B4B6F" BaseStyle="Over" BackColor="#98B5E2" Cursor="Hand">
                </Active>
                <Over BorderStyle="Solid" BorderWidth="1px" BorderColor="#316AC5" BaseStyle="Normal"
                    BackColor="#C1D2EE" Cursor="Hand">
                    <Padding Top="0px" Left="0px" Bottom="0px" Right="0px"></Padding>
                </Over>
                <Normal ForeColor="White" Width="13px" Font-Size="8pt" BackColor="#20334E" Cursor="Hand">
                    <Padding Top="1px" Left="1px" Bottom="1px" Right="1px"></Padding>
                </Normal>
            </HeaderButtonStyle>
            <SeparatorStyle Font-Size="1px" BackColor="#C5C2B8"></SeparatorStyle>
            <OptionStyle BackColor="#6F94C2" ForeColor="White"></OptionStyle>
            <CommandStyle>
                <Active ForeColor="White" BorderColor="White" BaseStyle="Over" BackColor="#324E78"
                    Overflow="Hidden" OverflowX="Hidden" OverflowY="Hidden">
                </Active>
                <Over BorderStyle="Solid" BorderWidth="1px" BorderColor="#316AC5" Font-Size="8pt"
                    Font-Names="Tahoma" BackColor="#C1D2EE" Overflow="Hidden" Cursor="Default" OverflowX="Hidden"
                    OverflowY="Hidden">
                    <Padding Top="0px" Left="2px" Bottom="0px" Right="2px"></Padding>
                </Over>
                <Normal ForeColor="White" Font-Size="8pt" Font-Names="Tahoma" Overflow="Hidden" OverflowX="Hidden"
                    OverflowY="Hidden">
                    <Padding Top="1px" Left="3px" Bottom="1px" Right="3px"></Padding>
                </Normal>
            </CommandStyle>
            <HeaderCaptionStyle ForeColor="White" Font-Size="8pt" Font-Names="Tahoma" Font-Bold="True"
                BackColor="#20334E" Cursor="Move">
            </HeaderCaptionStyle>
            <HandleStyle BackColor="#5777A7" ForeColor="White"></HandleStyle>
        </WebMenuBarStyleSettings>
    </ISWebDesktop:PlaceHolderManager>
    </form>
    <script language="javascript">
        function WebToolBar1_OnMouseUp(id, cmd) {
            if (cmd.Name == "cmdClose") {
                var dlg = window.parent.ISGetObject("WebDialogBox1");
                dlg.CloseDialog();
            }
            return true;
        }

        function LoadPage() {
            if (document.getElementById("msg") != null) {
                var msg = document.getElementById("msg").value;
                if (msg.indexOf("OK") != -1) {
                    var parentFrame = window.parent;
                    var grid = parentFrame.ISGetObject("WebGrid2");

                    alert("Employee saved");
                    grid.Refresh();

                    if (msg.indexOf("Close") != -1) {
                        var dlg = parentFrame.ISGetObject("WebDialogBox1");

                        dlg.CloseDialog();
                    }
                }
            }
        }

        LoadPage();
    </script>
</body>
</html>