<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AddIdea.aspx.cs" Inherits="COMP___1640.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container body-content col-12">

        <div class="container col-12">
            <h1>Idea Submission</h1>
            <div class="form-group">
                <label for="post_title">Title</label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="post_content">Categories</label>
                <asp:DropDownList ID="ddlCatgeory" runat="server" CssClass="form-control"/>
            </div>

            <div class="form-group">
                <label for="post_content">Content</label>
                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" CssClass="form-control"/>
            </div>

            <div class="form-group">
                <label>Document Link</label>
                <asp:FileUpload ID="fuDocLink" runat="server" disable="true" />
            </div>

            <%--<div class="form-group">
                <label>Tags</label>
                <asp:TextBox ID="txtTag" runat="server" CssClass="form-control" placeholder="Tags" />
            </div>--%>

            <div class="form-group">
                <asp:CheckBox ID="ckbAnonymous" runat="server" Text="Submit as Anonymous" />
            </div>

            <div class="form-group">
                <asp:CheckBox ID="ckbAgreeTerms" runat="server" />
                <a href="#">Agree With Terms and Conditions</a>
            </div>

            <asp:LinkButton ID="btnAddIdea" runat="server" CssClass="btn btn-primary col-12" Text="Submit Idea" OnClick="btnAddIdea_Click"/>
        </div>

    </div>
</asp:Content>
