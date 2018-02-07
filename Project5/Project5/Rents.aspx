<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Rents.aspx.vb" Inherits="Project5.Rents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <div class="jumbotron">
        <h1>Manage Rents</h1>
        <p class="lead">Leaffrey Management</p>
    </div>

    <asp:Panel ID ="ChooseApartmentNumber" runat ="server">

         &nbsp;&nbsp;&nbsp;

        <%--List box that gets loaded with apartment numbers that have a contract, but always has "Select" loaded as a user prompt.--%>
        <asp:ListBox ID="ResidentAppartmentNumberListBox" runat="server" Rows="1" AutoPostBack="True">
            <asp:ListItem>Select</asp:ListItem>
        </asp:ListBox>
        &nbsp;&nbsp;&nbsp;
      <asp:Label ID="ResidentsApartmentNumberLabelMessage" runat="server" Text="ResidentsApartmentNumberLabel" BorderStyle="Double" ForeColor="Red"></asp:Label>
    <p></p>
    <p>
        &nbsp;</p>
    </asp:Panel>

    <asp:Panel ID ="ManageRents" runat="server">
        <asp:Button ID="ChangeApartmentNumberButton" runat="server" Text="Change Apartment Number" />
        <br />
        <asp:Label ID="ApartmentChosenLabel" runat="server" Text="ApartmentChosenLabel"></asp:Label>
        <br />
        <br />
          
        

        <asp:Table ID="TransactionTable" runat="server" CellPadding="3" CellSpacing="3">

            <asp:TableRow runat="server" BorderStyle="Double">
                <asp:TableCell ID= "Cell0" runat="server" Wrap="False">0</asp:TableCell>
                <asp:TableCell ID= "Cell1" runat="server" Wrap="False">1</asp:TableCell>
                <asp:TableCell ID= "Cell2" runat="server" HorizontalAlign="Right" Wrap="False">2</asp:TableCell>
                <asp:TableCell ID= "Cell3" runat="server" HorizontalAlign="Right" Wrap="False">3</asp:TableCell>
            </asp:TableRow>

        </asp:Table>
           
        

        <br />

        <br />
        <asp:DropDownList ID="TransactionChoiceDropDownList" runat="server" AutoPostBack="True" Height="16px">
            <asp:ListItem>Select</asp:ListItem>
            <asp:ListItem>Retrieve Rent Amount</asp:ListItem>
            <asp:ListItem>Payment</asp:ListItem>
            <asp:ListItem>Late Charge</asp:ListItem>
            <asp:ListItem>Other Charge</asp:ListItem>
            <asp:ListItem>Deposit</asp:ListItem>
            <asp:ListItem>Pet Deposit</asp:ListItem>
            <asp:ListItem>Credit</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="TransactionChoiceDropErrorLabel" runat="server" Text="TransactionChoiceDropErrorLabel" BorderStyle="Double" ForeColor="Red"></asp:Label>
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="InputDataPanel" runat="server">

            <asp:Label ID="DescriptionLabel" runat="server" Text="DescriptionLabel"></asp:Label>
            <asp:TextBox ID="DescriptionTextBox" runat="server" Height="25px" Width="108px"></asp:TextBox>
            <asp:Label ID="DescriptionTextBoxErrorLabel" runat="server" BorderStyle="Double" ForeColor="Red" Text="DescriptionTextBoxErrorLabel"></asp:Label>

            
            <asp:TextBox ID="DateTextBox" runat="server" TextMode="Date" ></asp:TextBox>
            <asp:Label ID="DateTextBoxErrorLabel" runat="server" BorderStyle="Double" ForeColor="Red" Text="DateTextBoxErrorLabel"></asp:Label>
            <asp:Label ID="EnterAmountLabel" runat="server" Text="Enter Amount"></asp:Label>
            <asp:TextBox ID="AmountTextBox" runat="server" TextMode="Number" Width="74px"></asp:TextBox>
            <asp:Label ID="AmountErrorLabel" runat="server" BorderStyle="Double" ForeColor="Red" Text="AmountErrorLabel"></asp:Label>
            <asp:DropDownList ID="PaymentTypeDropDownList" runat="server">
                <asp:ListItem>Cash</asp:ListItem>
                <asp:ListItem>Check</asp:ListItem>
                <asp:ListItem>Money Order</asp:ListItem>
                <asp:ListItem>Cashiers Check</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Button ID="SaveTransactionButton" runat="server" Text="Save Transactoin" />
            <br />
            <br />
        
    </asp:Panel>



</asp:Content>
