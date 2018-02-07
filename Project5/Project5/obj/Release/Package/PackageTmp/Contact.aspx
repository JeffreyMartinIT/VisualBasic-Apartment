<%--Jeffrey Martin--%>


<%--Page that simply displays contact information for help. Does nothing progmatically.--%>
<%@ Page Title="Get Help" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.vb" Inherits="Project5.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p>Jeff Martin</p>

    <address>
        15500 N Uldriks Dr<br />
        Battle Creek, MI 48017<br />
        <abbr title="Phone"></abbr>
        (810)922-2770
        <abbr title="Email"></abbr>
        animeman1999@gmail.com
    </address>

</asp:Content>
