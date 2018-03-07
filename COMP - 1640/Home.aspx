<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="COMP___1640.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--CSS - JS links-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function ideaOnclick(lnk) {
            var id = lnk.getAttribute('ideaId');
            var pageUrl = '<%=ResolveUrl("Home.aspx")%>';
            $.ajax({
                type: "POST",
                url: pageUrl + "/IdeaClicked",
                data: JSON.stringify({
                    'ideaId': id
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    window.location.href = "Post.aspx";
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function bindIdeasToPage(lst) {
            var html = "";
            var x = JSON.parse(lst);
            for (var i = 0; i < x.length; i++) {
                html += "<div class='col-12'><div class='well'><p><a href='#'>" + x[i].userName + "</a> posted:</p><h2><a href='#' onclick='ideaOnclick(this)' ideaId='" + x[i].ideaId + "'>" + x[i].ideaTitle + "</a></h2><p>" + x[i].ideaContent + "</p><span class='timestamp'>posted " + x[i].postedDate + " days ago.</span></div></div>";
            }
            $("#lstIdeas").html(html);
        }
    </script>

    <div class='container body-content col-12'>

        <div class='col-12'>
            <div class='form-group input-group'>
                <input type='text' name='search' id='search' placeholder='Search post' class='form-control' />
                <span class='input-group-btn'>
                    <input type='submit' value='Search' class='btn btn-default' data-disable-with='Search' />
                </span>
            </div>
        </div>
        <p class='button'>
            <a href='AddIdea.aspx'>New Post</a>
        </p>


        <div class='center col-12' id='lstIdeas'>
        </div>
    </div>
</asp:Content>
