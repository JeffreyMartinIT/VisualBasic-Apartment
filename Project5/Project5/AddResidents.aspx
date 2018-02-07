<%@ Page Title="Add Residents" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AddResidents.aspx.vb" Inherits="Project5.AddResidents" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Add Resident</h1>
        <p class="lead">Leaffrey Management</p>
    </div>
    <p>&nbsp;</p>
    <p></p>
    <asp:Panel ID="SelectApartmentNumberPanel" runat="server">
        <p>
        <asp:Label ID="ResidentsApartmentNumberLabel" runat="server" Text="Apartment Number of Resident"></asp:Label>
        &nbsp;&nbsp;&nbsp;

        <%--List box that gets loaded with apartment numbers that have a contract, but always has "Select" loaded as a user prompt.--%>
        <asp:ListBox ID="ResidentAppartmentNumberListBox" runat="server" Rows="1">
            <asp:ListItem>Select</asp:ListItem>
        </asp:ListBox>
        &nbsp;&nbsp;&nbsp;

        <asp:Label ID="ResidentsApartmentNumberLabelMessage" runat="server" Text="ApartmentsNotRentedLabel" BorderStyle="Double" ForeColor="Red"></asp:Label>
    </p>
    <p>
        <asp:Button ID="AddResidentForApartmentChosenButton" runat="server" Text="Start Add Residents" />
    </p>
    </asp:Panel>


    <asp:Panel ID="AddResidentsPanel" runat="server">

        <p>
            <asp:Label ID="PrimaryLeaserLabel" runat="server" Text="PrimaryLeaserLabel"></asp:Label>
        </p>
        <p></p>
        <p>
            <asp:Label ID="ResidentCalendarErrofLabel" runat="server" BackColor="White" ForeColor="Red" Text="ResidentCalendarErrofLabel"></asp:Label>
        </p>
         <%--Calendar to set the date resident being added--%>
            <asp:Calendar ID="DateResidentAddedCalendar" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" 
                            Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" 
                            Width="350px" >
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                <TodayDayStyle BackColor="#CCCCCC" />
            </asp:Calendar>


        <p>
            <asp:Label ID="ResidentLastNameLabel" runat="server" Text="Last Name: "></asp:Label>
            <asp:TextBox ID="ResidentLastNameTextBox" runat="server"></asp:TextBox>
            <asp:Label ID="ResidentLastNameErrorLabel" runat="server" ForeColor="#CC0000" Text="ResidentLastNameErrorLabel"></asp:Label>
        </p>
        <p>
            <asp:Label ID="ResidentFirstNameLabel" runat="server" Text="First Name:"></asp:Label>
            <asp:TextBox ID="ResidentFirstNameTextBox" runat="server"></asp:TextBox>

            <asp:Label ID="ResidentFirstNameErrorLable" runat="server" Text="ResidentFirstNameErrorLable" BorderStyle="Double" ForeColor="Red"></asp:Label>
        </p>
        <p>
            <asp:DropDownList ID="SignedLeaseDropDownList" runat="server">
                <asp:ListItem>Select</asp:ListItem>
                <asp:ListItem>Resident</asp:ListItem>
                <asp:ListItem>Signed Lease</asp:ListItem>
            </asp:DropDownList>

            <asp:Label ID="SignedLeaseDropDownListErrorLabel" runat="server" Text="SignedLeaseDropDownListErrorLabel" BorderStyle="Double" ForeColor="Red"></asp:Label>
        </p>

         <%--Button used to add a resident--%>
        <p>
            <asp:Button ID="AddResidentButton" runat="server" Text="Add Resident" />
        </p>
    </asp:Panel>
   
</asp:Content>
