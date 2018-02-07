<%--Jeffrey Martin--%>

<%@ Page Title="New Contract" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewContract.aspx.vb" Inherits="Project5.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Add a New Contract</h2>

    <p>&nbsp</p>

    <p>
        <asp:Label ID="CalendarErrorLabel" runat="server" BorderStyle="Double" ForeColor="Red" Text="CalendarErrorLabel"></asp:Label>

        <%--Calendar to set the date of the new contract--%>
        <asp:Calendar ID="DateSignedCalendar" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" 
                        Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" 
                        Width="350px" >
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
            <TodayDayStyle BackColor="#CCCCCC" />
        </asp:Calendar>
        
    </p>

    <p>&nbsp;</p>

    <p>
        <asp:Label ID="ApartmentNumberLabel" runat="server" Text="Apartment Number"></asp:Label>
        &nbsp;&nbsp;&nbsp;

        <%--List box that gets loaded with empty apartment numbers, but always has "Select" loaded as a user prompt.--%>
        <asp:ListBox ID="AppartmentNumberListBox" runat="server" Rows="1">
            <asp:ListItem>Select</asp:ListItem>
        </asp:ListBox>
        &nbsp;&nbsp;&nbsp;

        <asp:Label ID="ApartmnetFullLabel" runat="server" Text="ApartmnetFullLabel" BorderStyle="Double" ForeColor="Red"></asp:Label>
    </p>
    
   <%-- ****************Textboxes that are filled with data by the user*********************--%>
    <p>
        <asp:Label ID="CurrentRateLabel" runat="server" Text="Current Rate"></asp:Label>
        <asp:TextBox ID="CurrentRateTextBox" runat="server"></asp:TextBox>
        <asp:Label ID="RateErrorLabel" runat="server" BorderStyle="Double" ForeColor="Red" Text="RateErrorLabel"></asp:Label>
    </p>

    <p>
        Last Name:&nbsp;&nbsp;
        <asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>
        <asp:Label ID="LastNameErrorLabel" runat="server" BorderStyle="Double" ForeColor="Red" Text="LastNameErrorLabel"></asp:Label>
    </p>

    <p>
        First Name:&nbsp;&nbsp;
        <asp:TextBox ID="FirstNameTextBox" runat="server"></asp:TextBox>
        <asp:Label ID="FirstNameErrorLabel" runat="server" BorderStyle="Double" ForeColor="Red" Text="FirstNameErrorLabel"></asp:Label>
    </p>
    <%--**********************************End Textboxes************************************--%>

    <%--Button used to create the new contract--%>
    <p>
        <asp:Button ID="CreateContractButton" runat="server" Text="Create Contract" />
    </p>
</asp:Content>
