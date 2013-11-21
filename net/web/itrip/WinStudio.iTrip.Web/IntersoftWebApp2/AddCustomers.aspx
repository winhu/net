<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCustomers.aspx.cs" Inherits="IntersoftWebApp2.AddCustomers" %>

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
<body style="margin:0px 0px 0px 0px; padding: 5px 5px 0px 5px; overflow:hidden;">
    <form id="form1" runat="server">
    <div>
        <table class="DefaultText" style="margin-left: 5px; background-image: url(./images/bgdialog.png);
            width: 100%; height: 100%;">
            <tr>
                <td>
                    Customer ID
                </td>
                <td>
                    :
                </td>
                <td>
                    <ISWebInput:WebInput ID="wiCustomerID" runat="server" MaxLength="5" Width="75px">
                        <HighLight IsEnabled="True" />
                        <CultureInfo CultureName="en-US">
                        </CultureInfo>
                        <EditFormat Format="AAAAA" IsEnabled="True" Type="Other">
                            <MaskInfo MaskExpression="AAAAA">
                            </MaskInfo>
                            <ErrorWindowInfo IsEnabled="True">
                            </ErrorWindowInfo>
                        </EditFormat>
                    </ISWebInput:WebInput>
                </td>
            </tr>
            <tr>
                <td>
                    Company Name
                </td>
                <td>
                    :
                </td>
                <td>
                    <ISWebInput:WebInput ID="wiCompanyName" runat="server" Width="250px">
                        <CultureInfo CultureName="en-US">
                        </CultureInfo>
                    </ISWebInput:WebInput>
                </td>
            </tr>
            <tr>
                <td>
                    Contact Name
                </td>
                <td>
                    :
                </td>
                <td>
                    <ISWebInput:WebInput ID="wiContactName" runat="server" Width="250px">
                        <CultureInfo CultureName="en-US">
                        </CultureInfo>
                    </ISWebInput:WebInput>
                </td>
            </tr>
            <tr>
                <td>
                    Contact Title
                </td>
                <td>
                    :
                </td>
                <td>
                    <ISWebInput:WebInput ID="wiContactTitle" runat="server" Width="250px">
                        <CultureInfo CultureName="en-US">
                        </CultureInfo>
                    </ISWebInput:WebInput>
                </td>
            </tr>
            <tr>
                <td>
                    Address
                </td>
                <td>
                    :
                </td>
                <td>
                    <ISWebInput:WebInput ID="wiAddress" runat="server" Height="72px" TextMode="MultiLine"
                        Width="297px">
                        <CultureInfo CultureName="en-US">
                        </CultureInfo>
                    </ISWebInput:WebInput>
                </td>
            </tr>
            <tr>
                <td>
                    City
                </td>
                <td>
                    :
                </td>
                <td>
                    <ISWebCombo:WebCombo ID="wcCity" runat="server" UseDefaultStyle="true" Width="150px">
                        <Columns>
                            <ISWebCombo:WebComboColumn BaseFieldName="City" Bound="False" Name="Column0" />
                        </Columns>
                        <Rows>
                            <ISWebCombo:WebComboRow>
                                <Cells>
                                    <ISWebCombo:WebComboCell Text="London">
                                    </ISWebCombo:WebComboCell>
                                </Cells>
                            </ISWebCombo:WebComboRow>
                            <ISWebCombo:WebComboRow Position="1">
                                <Cells>
                                    <ISWebCombo:WebComboCell Text="Berlin">
                                    </ISWebCombo:WebComboCell>
                                </Cells>
                            </ISWebCombo:WebComboRow>
                            <ISWebCombo:WebComboRow Position="2">
                                <Cells>
                                    <ISWebCombo:WebComboCell Text="Sao Paulo">
                                    </ISWebCombo:WebComboCell>
                                </Cells>
                            </ISWebCombo:WebComboRow>
                            <ISWebCombo:WebComboRow Position="3">
                                <Cells>
                                    <ISWebCombo:WebComboCell Text="Strasbourg">
                                    </ISWebCombo:WebComboCell>
                                </Cells>
                            </ISWebCombo:WebComboRow>
                            <ISWebCombo:WebComboRow Position="4">
                                <Cells>
                                    <ISWebCombo:WebComboCell Text="Boise">
                                    </ISWebCombo:WebComboCell>
                                </Cells>
                            </ISWebCombo:WebComboRow>
                        </Rows>
                    </ISWebCombo:WebCombo>
                </td>
            </tr>
            <tr>
                <td>
                    Country
                </td>
                <td>
                    :
                </td>
                <td>
                    <ISWebCombo:WebCombo ID="wcCountry" runat="server" UseDefaultStyle="true" Width="150px">
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
                                    <ISWebCombo:WebComboCell Text="Germany">
                                    </ISWebCombo:WebComboCell>
                                </Cells>
                            </ISWebCombo:WebComboRow>
                            <ISWebCombo:WebComboRow Position="3">
                                <Cells>
                                    <ISWebCombo:WebComboCell Text="Mexico">
                                    </ISWebCombo:WebComboCell>
                                </Cells>
                            </ISWebCombo:WebComboRow>
                            <ISWebCombo:WebComboRow Position="4">
                                <Cells>
                                    <ISWebCombo:WebComboCell Text="France">
                                    </ISWebCombo:WebComboCell>
                                </Cells>
                            </ISWebCombo:WebComboRow>
                        </Rows>
                    </ISWebCombo:WebCombo>
                </td>
            </tr>
            <tr>
                <td>
                    Postal Code
                </td>
                <td>
                    :
                </td>
                <td>
                    <ISWebInput:WebInput ID="wiPostalCode" runat="server" Width="80px">
                        <HighLight IsEnabled="True"></HighLight>
                        <CultureInfo CultureName="en-US">
                        </CultureInfo>
                        <EditFormat IsEnabled="True" Type="Other">
                            <MaskInfo MaskExpression="00000-9999">
                            </MaskInfo>
                            <ErrorWindowInfo IsEnabled="True">
                            </ErrorWindowInfo>
                        </EditFormat>
                    </ISWebInput:WebInput>
                </td>
            </tr>
            <tr>
                <td>
                    Region
                </td>
                <td>
                    :
                </td>
                <td>
                    <ISWebInput:WebInput ID="wiRegion" runat="server" Width="80px">
                        <CultureInfo CultureName="en-US">
                        </CultureInfo>
                    </ISWebInput:WebInput>
                </td>
            </tr>
            <tr>
                <td>
                    Phone
                </td>
                <td>
                    :
                </td>
                <td>
                    <ISWebInput:WebInput ID="wiPhone" runat="server" Width="80px">
                        <HighLight IsEnabled="True"></HighLight>
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
                    Fax
                </td>
                <td>
                    :
                </td>
                <td>
                    <ISWebInput:WebInput ID="wiFax" runat="server" Width="80px">
                        <HighLight IsEnabled="True"></HighLight>
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
                <td colspan="2">
                </td>
                <td>
                    <ISWebDesktop:WebButton ID="wbAdd" runat="server" Height="20px" Text="Add" Width="50px"
                        AutoPostback="True" OnClicked="wbAdd_Clicked" PostBackMode="FullPostBack">
                        <ButtonStyle>
                            <Active BackColor="White" BaseStyle="Over" BorderColor="#6593CF" BackColor2="144, 185, 238">
                            </Active>
                            <Over BackColor="#FFF4C7" BorderColor="#FFBD69" Cursor="Default" BackColor2="255, 215, 157"
                                BaseStyle="Normal">
                                <Padding Bottom="0px" Left="5px" Right="5px" Top="0px" />
                            </Over>
                            <Normal Font-Names="Segoe UI,Tahoma" Font-Size="8pt" Overflow="Hidden" OverflowX="Hidden"
                                BorderStyle="Solid" BorderWidth="1px" OverflowY="Hidden" BackColor2="226, 237, 246"
                                BorderColor="#6593CF" BackColor="White" GradientType="Vertical">
                                <Padding Bottom="1px" Left="6px" Right="6px" Top="1px" />
                            </Normal>
                        </ButtonStyle>
                    </ISWebDesktop:WebButton>
                    <ISWebDesktop:WebButton ID="wbCancel" runat="server" Height="20px" Text="Cancel"
                        Width="50px" OnClientClick="wbCancel_OnClientClick">
                        <ButtonStyle>
                            <Active BackColor="White" BaseStyle="Over" BorderColor="#6593CF" BackColor2="144, 185, 238">
                            </Active>
                            <Over BackColor="#FFF4C7" BorderColor="#FFBD69" Cursor="Default" BackColor2="255, 215, 157"
                                BaseStyle="Normal">
                                <Padding Bottom="0px" Left="5px" Right="5px" Top="0px" />
                            </Over>
                            <Normal Font-Names="Segoe UI,Tahoma" Font-Size="8pt" Overflow="Hidden" OverflowX="Hidden"
                                BorderStyle="Solid" BorderWidth="1px" OverflowY="Hidden" BackColor2="226, 237, 246"
                                BorderColor="#6593CF" BackColor="White" GradientType="Vertical">
                                <Padding Bottom="1px" Left="6px" Right="6px" Top="1px" />
                            </Normal>
                        </ButtonStyle>
                    </ISWebDesktop:WebButton>
                </td>
            </tr>
        </table>
    </div>
    <script language="javascript">
        function LoadPage() {
            if (document.getElementById("msg") != null) {
                var msg = document.getElementById("msg").value;
                if (msg == "OK") {
                    var parentFrame = window.parent;
                    var dlg = parentFrame.ISGetObject("WebDialogBox1");
                    var grid = parentFrame.ISGetObject("WebGrid1");

                    grid.Refresh();
                    dlg.CloseDialog();
                }
            }
        }

        function wbCancel_OnClientClick(controlId, parameter) {
            var wbCancel = ISGetObject(controlId);
            var parentFrame = window.parent;
            var dlg = parentFrame.ISGetObject("WebDialogBox1");

            dlg.CloseDialog();

            return true;
        }

        LoadPage();
    </script>
    </form>
</body>
</html>
