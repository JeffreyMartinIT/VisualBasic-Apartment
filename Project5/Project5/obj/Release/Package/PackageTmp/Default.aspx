<%--Jeffrey Martin
--%>

<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="Project5._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Apartment Contracts</h1>
        <p class="lead">Leaffrey Management</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Current Leases</h2>
            <p>

                <%--Format the Gridview and load the data into to it.--%>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" BackColor="White" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">

                    <AlternatingRowStyle BackColor="#CCCCCC" />

                    <Columns>
                        <asp:BoundField DataField="LeaseNumber" HeaderText="Lease Number" SortExpression="LeaseNumber" >
                        </asp:BoundField>

                        <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" >
                        </asp:CheckBoxField>

                        <asp:BoundField DataField="ApartmentNumber" HeaderText="Apartment Number" SortExpression="ApartmentNumber">
                        </asp:BoundField>

                        <asp:BoundField DataField="DateSigned" HeaderText="Date Signed" SortExpression="DateSigned" >
                        </asp:BoundField>

                        <asp:BoundField DataField="CurrentRate" HeaderText="Current Rate" SortExpression="CurrentRate">
                        </asp:BoundField>

                        <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />

                        <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />

                    </Columns>

                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />

                </asp:GridView>

                <%--Gets the data that is bound to the Gridview above--%>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:apartmentConnectionString %>" 
                    SelectCommand="  SELECT [Leases].[LeaseNumber], [Leases].[Active], [ApartmentNumber], [DateSigned], [CurrentRate]
                                            ,[Residents].[LastName], [Residents].[FirstName]
                                      FROM [Leases] 
                                      LEFT JOIN [Residents] ON [Leases].[LeaseNumber] = [Residents].[LeaseNumber]
                                      WHERE   [Leases].[Active] = 1 
                                      ORDER BY [ApartmentNumber]"></asp:SqlDataSource>

            </p>
            <p> &nbsp;</p>
            <p>

                <%--The drop down list used to deactivate a lease and residents based upon apartment number chosen--%>
                Moved Out
                <asp:DropDownList ID="MoveOutDropDownList" runat="server" AutoPostBack="True">
                    <asp:ListItem>Select</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="MovedOutLabel" runat="server" Text="Label"></asp:Label>

                <asp:Button ID="DeleteButton" runat="server" Text="DELETE" />
                <asp:Button ID="CancelButton" runat="server" Text="Cancel" />

            </p>

        </div>
      
       
    </div>

</asp:Content>
