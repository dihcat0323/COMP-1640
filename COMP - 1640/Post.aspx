<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="COMP___1640.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function bindComments(lst) {
            var html = "";
            var c = JSON.parse(lst);
            for (var i = 0; i < c.length; i++) {
                html += '<div class="media"><p class="pull-right"><small>' + c[i].postedDate + ' days ago</small></p><div class="media-body"><h4 class="media-heading user_name">' + c[i].userName + '</h4>' + c[i].Details + '</div></div>'
            }

            $("#lstComment").html(html);
        }
    </script>
    <div class="container body-content col-12">
        <div class="container">
            <div class="row">
                <aside class="col-md-8">
                    <section class="user_info">
                        <h1>
                            <a id="lbtnUser" runat="server" href="Login.aspx"></a>
                        </h1>
                        <div class="timestamp">
                            <asp:Label ID="lblPostedDate" runat="server" />
                        </div>
                        <div class="title">
                            <h2 id="lblTitle" runat="server"></h2>
                        </div>
                    </section>
                    <div class="content">
                        <p id="lblContent" runat="server" />
                    </div>
                    <br />
                    <section class="user_info">
                        <div class="timestamp">
                            <strong>Category: </strong>
                            <span id="lblCategory" runat="server"></span>
                        </div>
                        <div class="timestamp">
                            <strong>Document Link: </strong>
                            <span id="lblDocumentLink" runat="server"></span>
                        </div>
                        <div class="timestamp">
                            <strong>Submitted As Anonymous: </strong>
                            <span id="lblAnonymous" runat="server"></span>
                        </div>
                        <div class="timestamp">
                            <strong>Total Views: </strong>
                            <span id="lblTotalView" runat="server"></span>
                        </div>
                    </section>
                </aside>


                <div class="col-md-4">
                    <h3>Comment
                        <asp:Label ID="lblCmtCount" runat="server" />
                    </h3>

                    <div class="form-group comment-list" id="lstComment">
                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="txtCmt" runat="server" CssClass="form-control" placecholder="Comment here..." />
                        <asp:CheckBox ID="ckbAnonymous" runat="server" Text="Submit Comment as Anonymous" />
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnSubmitCmt" runat="server" Text="Submit Comment" CssClass="btn" OnClick="btnSubmitCmt_Click" />
                    </div>
                </div>
            </div>
        </div>

    </div>


</asp:Content>
