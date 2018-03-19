<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="TopicMng.aspx.cs" Inherits="COMP___1640.WebForm7" %>

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

        function LoadTopics(page) {
            var pageUrl = '<%=ResolveUrl("TopicMng.aspx")%>';
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
                html += "<tr>";
                html += "<td>" + obj.Id + "</td>";
                html += "<td><a href='#'>" + obj.Name + "</a></td>";
                html += "<td>...</td>";
                html += "<td>" + obj.PostedDate.split(' ')[0] + "</td>";
                html += "<td>" + obj.ClosureDate.split(' ')[0] + "</td>";
                html += "<td>" + obj.FinalClosureDate.split(' ')[0] + "</td>";
                html += "<td><a href='#' onclick='TopicOnclick(this)' TopicId='" + obj.Id + "'>Edit Item</a></td>";
                html += "</tr>";
            });

            $("#lstTopics").html(html);
        }

        function TopicOnclick(lnk) {
            var id = lnk.getAttribute('TopicId');
            var pageUrl = '<%=ResolveUrl("TopicMng.aspx")%>';
            $.ajax({
                type: "POST",
                url: pageUrl + "/TopicClicked",
                data: JSON.stringify({
                    'id': id
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $('#txtName').val(data.d.Name);
                    $('#txtName').attr("TopicId", data.d.Id);
                    $('#txtName').prop("disabled", true);

                    $('#txtDescription').val(data.d.Details);
                    $('#txtDescription').prop("disabled", true);

                    $('#dtPosted').val(data.d.PostedDate);

                    $('#dtClosure').val(data.d.ClosureDate);
                    $('#dtFinal').val(data.d.FinalClosureDate);
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function AddTopic() {
            var id = $('#txtName').attr("TopicId");
            var name = $('#txtName').val();
            var details = $('#txtDescription').val();
            //var posted = $('#dtPosted').val();
            var closure = $('#dtClosure').val();
            var final = $('#dtFinal').val();

            var pageUrl = '<%=ResolveUrl("TopicMng.aspx")%>';

            if (typeof (id) !== 'undefined') {
                alert("[ERROR]: the item was selected to be EDITED, not to be INSERTED!!!");
                return;
            }

            if (name === "" || closure === "" || final === "") {
                alert("ERROR: Topic Name, Closure Date and Final Closure Date are required!!!");
                return;
            }

            //validate Closure vs Final
            var dclosure = new Date(closure);
            var dfinal = new Date(final);
            if (dfinal < dclosure) {
                alert("ERROR: Final Closure Date cannot be earlier than Closure Date!!!");
                return;
            }

            if (dfinal < new Date() || dclosure < new Date()) {
                alert("ERROR: Closure Date and Final Closure Date cannot be earlier than today!!!");
                return;
            }

            $.ajax({
                type: "POST",
                url: pageUrl + "/Client_AddTopic",
                data: JSON.stringify({
                    'name': name,
                    'details': details,
                    'closure': closure,
                    'final': final
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (!data.d) {
                        alert("ERROR: Cannot insert the Topic into the database!!!");
                    } else {
                        alert("SUCCESS: New Topic successfully inserted into the database!!!");
                    }
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function EditTopic() {
            var id = $('#txtName').attr("TopicId");
            var closure = $('#dtClosure').val();
            var final = $('#dtFinal').val();
            var pageUrl = '<%=ResolveUrl("TopicMng.aspx")%>';

            if (typeof (id) === "undefined") {
                alert("[ERROR]: No Topic was selected to be edited!!!");
                return;
            }

            //validate Closure vs Final
            var dclosure = new Date(closure);
            var dfinal = new Date(final);
            if (dfinal < dclosure) {
                alert("ERROR: Final Closure Date cannot be earlier than Closure Date!!!");
                return;
            }

            if (dfinal < new Date() || dclosure < new Date()) {
                alert("ERROR: Closure Date and Final Closure Date cannot be earlier than today!!!");
                return;
            }

            $.ajax({
                type: "POST",
                url: pageUrl + "/Client_UpdateTopic",
                data: JSON.stringify({
                    'id': id,
                    'closure': closure,
                    'final': final
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (!data.d) {
                        alert("[ERROR]: Failed when update Topic in DB!!!");
                    } else {
                        alert("[SUCCESS]: Updated successfully!!!");
                    }
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
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


    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    List of Topics
                </div>
                <div class="panel-body">
                    <div id="" class="form-inline dt-bootstrap no-footer">
                        <div class="row" style="margin-top: 10px">
                            <div class="col-sm-12">
                                <table class="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" id="dataTables-example" role="grid" aria-describedby="dataTables-example_info" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th>ID</th>
                                            <th>Topic Name</th>
                                            <th>Details</th>
                                            <th>Posted Date</th>
                                            <th>Closure Date</th>
                                            <th>Final Closure Date</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody id="lstTopics">
                                    </tbody>
                                </table>
                            </div>
                            <div class="col-sm-6">
                                <nav aria-label="Page navigation example">
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
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Topic Details
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>Topic Name</label>
                        <input type="text" id="txtName" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>Descriptions</label>
                        <textarea id="txtDescription" class="form-control" rows="3"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Posted Date</label>
                        <input type="date" id="dtPosted" class="form-control" disabled="true" />
                    </div>
                    <div class="form-group">
                        <label>Closure Date</label>
                        <input type="date" id="dtClosure" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>Final Date</label>
                        <input type="date" id="dtFinal" class="form-control" />
                    </div>

                    <div style="text-align: center">
                        <button id="btnAddTopic" class="btn btn-success" onclick="AddTopic()">Add New Topic</button>
                        <button id="btnEditTopic" class="btn btn-warning" onclick="EditTopic()">Edit Topic</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
