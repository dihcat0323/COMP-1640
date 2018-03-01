<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="COMP___1640.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container body-content col-6">
        <div class="container col-12">
            <div class="form-group">
                <label for="exampleInputUsername">Email</label>
                <%--<input type="text" class="form-control" id="exampleInputUsername" placeholder="Enter username">--%>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email" />
            </div>
            <div class="form-group">
                <label for="exampleInputPassword1">Password</label>
                <%--<input type="password" class="form-control" id="exampleInputPassword" placeholder="Password">--%>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter password" />
            </div>
            <div class="form-check">
                <input type="checkbox" class="form-check-input" id="exampleRememberMe">
                <label class="form-check-label" for="exampleRememberMe">Remember me</label>
            </div>
            <%--<button type="submit" class="btn btn-primary">Login</button>--%>
            <asp:LinkButton CssClass="btn btn-primary" ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"/>
        </div>
    </div>
</asp:Content>
