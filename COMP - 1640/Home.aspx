<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="COMP___1640.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--CSS - JS links-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var currentPage = 0;
        var totalPage = 0;

        $(document).ready(function () {
            LoadIdeas(0);
            $('#last-page').click(function () {
                currentPage = totalPage - 1;
                LoadIdeas(currentPage);
            });

            $('#next-page').click(function () {
                currentPage = currentPage + 1;
                if (currentPage == totalPage) {
                    currentPage = currentPage - 1;
                } else {
                    LoadIdeas(currentPage);
                }
            });

            $('#previous-page').click(function () {
                currentPage = currentPage - 1;
                LoadIdeas(currentPage);
            });

            $('#first-page').click(function () {
                currentPage = 0;
                LoadIdeas(currentPage);
            });

            $('#list-page').on('change', function () {
                currentPage = $('#list-page option:selected').val();
                currentPage = parseInt(currentPage);
                LoadIdeas(currentPage);
            });
        });

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

        function LoadIdeas(page) {
            var pageUrl = '<%=ResolveUrl("Home.aspx")%>';
            var currentPage = page;
            $.ajax({
                type: "POST",
                url: pageUrl + "/LoadData",
                data: JSON.stringify({
                    'currentPage': page,
                    'pagesize': 2
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d) {
                        bindIdeas(data.d.ListIdeas);
                        totalPage = data.d.TotalPage;
                        $('#list-page option').remove();
                        for (var j = 0; j < totalPage; j++) {
                            if (j === parseInt(currentPage)) {
                                $('#list-page').append('<option selected = "selected" value="' + j + '">' + (j + 1) + '</option>');
                            } else {
                                $('#list-page').append('<option value="' + j + '">' + (j + 1) + '</option>');
                            }
                        }
                    }

                    enablePaging(currentPage, totalPage);
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function bindIdeas(lst) {
            var html = "";
            $.each(lst, function (index, obj) {
                html += "<div class='col-12'><div class='well'><p><a href='#'>" + obj.userName + "</a> posted:</p><h2><a href='#' onclick='ideaOnclick(this)' ideaId='" + obj.ideaId + "'>" + obj.ideaTitle + "</a></h2><p>" + obj.ideaContent + "</p><span class='timestamp'>posted " + obj.postedDate + " days ago.</span></div></div>";
            });

            $("#lstIdeas").html(html);
        }

        function enablePaging(currentP, totalPage) {
            var current = parseInt(currentP);
            if (totalPage === 1) {
                $('#last-page').addClass('not-active-link');
                $('#next-page').addClass('not-active-link');
                $('#previous-page').addClass('not-active-link');
                $('#first-page').addClass('not-active-link');
            }

            else if (current === 0) {
                $('#last-page').removeClass('not-active-link');
                $('#next-page').removeClass('not-active-link');
                $('#previous-page').addClass('not-active-link');
                $('#first-page').addClass('not-active-link');
            }

            else if (current === totalPage - 1) {
                $('#last-page').addClass('not-active-link');
                $('#next-page').addClass('not-active-link');
                $('#previous-page').removeClass('not-active-link');
                $('#first-page').removeClass('not-active-link');
            }

            else {
                $('#last-page').removeClass('not-active-link');
                $('#next-page').removeClass('not-active-link');
                $('#previous-page').removeClass('not-active-link');
                $('#first-page').removeClass('not-active-link');
            }
        }
    </script>

    <div class='container body-content col-12'>
        <h1>List of Ideas for Topic: <asp:label ID="lblTopicName" runat="server"></asp:label></h1>
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

        <nav aria-label="Page navigation example" style="margin-bottom:20px">
            <ul class="pagination">
                <li class="page-item"><a id="first-page" class="page-link" href="#">First</a></li>
                <li class="page-item"><a id="previous-page" class="page-link" href="#">Previous</a></li>
                <li class="page-item">
                    <a class="page-link" href="#">
                        <select id="list-page">
                        </select>
                    </a>
                </li>
                <li class="page-item"><a id="next-page" class="page-link" href="#">Next</a></li>
                <li class="page-item"><a id="last-page" class="page-link" href="#">Last</a></li>
            </ul>
        </nav>
    </div>
</asp:Content>
