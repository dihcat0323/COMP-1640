<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Topic.aspx.cs" Inherits="COMP___1640.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var currentPage = 0;
        var totalPage = 0;

        $(document).ready(function () {
            LoadTopics(0);
            $('#last-page').click(function () {
                currentPage = totalPage - 1;
                LoadTopics(currentPage);
            });

            $('#next-page').click(function () {
                currentPage = currentPage + 1;
                if (currentPage == totalPage) {
                    currentPage = currentPage - 1;
                } else {
                    LoadTopics(currentPage);
                }
            });

            $('#previous-page').click(function () {
                currentPage = currentPage - 1;
                LoadTopics(currentPage);
            });

            $('#first-page').click(function () {
                currentPage = 0;
                LoadTopics(currentPage);
            });

            $('#list-page').on('change', function () {
                currentPage = $('#list-page option:selected').val();
                currentPage = parseInt(currentPage);
                LoadTopics(currentPage);
            });


        });

        function TopicOnclick(lnk) {
            var id = lnk.getAttribute('TopicId');
            var pageUrl = '<%=ResolveUrl("Topic.aspx")%>';
            $.ajax({
                type: "POST",
                url: pageUrl + "/TopicClicked",
                data: JSON.stringify({
                    'TopicId': id
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    window.location.href = "Home.aspx";
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function LoadTopics(page) {
            var pageUrl = '<%=ResolveUrl("Topic.aspx")%>';
            var currentPage = page;

            $.ajax({
                type: "POST",
                url: pageUrl + "/LoadData",
                data: JSON.stringify({
                    'currentPage': page,
                    'pagesize': 5
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d) {
                        bindTopics(data.d.ListTopics);
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

        function bindTopics(lst) {
            var html = "";
            $.each(lst, function (index, obj) {
                html += "<div class='col-12'><div class='well'><p>Posted Date: " + obj.PostedDate + " --- <strong>Closure Date: </strong>" + obj.ClosureDate + " --- <strong>Final Closure Date: </strong>" + obj.FinalClosureDate + "</p><a href='#' onclick='TopicOnclick(this)' TopicId='" + obj.Id + "' style='font-size:30px'>" + obj.Name + "</a><p>" + obj.Details + "</p></div></div>";
            });

            $("#lstTopics").html(html);
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

        <div class='center col-12' id='lstTopics'>
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
