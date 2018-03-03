<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="COMP___1640.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container body-content col-6">
        <div class="container col-12">
            <div class="form-group">
                <label for="exampleInputUsername">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email" />
            </div>
            <div class="form-group">
                <label for="exampleInputPassword1">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter password" />
            </div>
            <%--<div class="form-check">
                <asp:CheckBox ID="ckbRemember" runat="server" Text="Remember Me" />
            </div>--%>
            <%--<button type="submit" class="btn btn-primary">Login</button>--%>
            <asp:LinkButton CssClass="btn btn-primary" ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"/>
        </div>
    </div>
</asp:Content>
