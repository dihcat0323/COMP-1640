<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="UserMng.aspx.cs" Inherits="COMP___1640.WebForm8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var currentPage = 0;
        var totalPage = 0;

        $(document).ready(function () {
            Loadusers(0);

            $('#last-page').click(function () {
                currentPage = totalPage - 1;
                Loadusers(currentPage);
            });

            $('#next-page').click(function () {
                currentPage = currentPage + 1;
                if (currentPage == totalPage) {
                    currentPage = currentPage - 1;
                } else {
                    Loadusers(currentPage);
                }
            });

            $('#previous-page').click(function () {
                currentPage = currentPage - 1;
                Loadusers(currentPage);
            });

            $('#first-page').click(function () {
                currentPage = 0;
                Loadusers(currentPage);
            });

            $('#list-page').on('change', function () {
                currentPage = $('#list-page option:selected').val();
                currentPage = parseInt(currentPage);
                Loadusers(currentPage);
            });

        });

        function Loadusers(page) {
            var pageUrl = '<%=ResolveUrl("UserMng.aspx")%>';
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
                        bindusers(data.d.ListUsers);
                        bindRole(data.d.ListRoles);
                        bindDepartment(data.d.ListDepartment);

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

        function bindusers(lst) {
            var html = "";
            $.each(lst, function (index, obj) {
                html += "<tr>";
                html += "<td>" + obj.Id + "</td>";
                html += "<td>" + obj.roleName + "</td>";
                html += "<td>" + obj.departmentName + "</td>";
                html += "<td>" + obj.Name + "</td>";
                html += "<td>" + obj.Email + "</td>";
                html += "<td>...</td>";
                html += "<td><a href='#' onclick='userOnclick(this)' userId='" + obj.Id + "'>Edit Item</a></td>";
                html += "</tr>";
            });

            $("#lstUsers").html(html);
        }

        function bindRole(lst) {
            var html = "";
            $.each(lst, function (index, obj) {
                html += "<option value='" + obj.Id + "'>" + obj.Name + "</option>";
            });

            $("#lstRoles").html(html);
        }

        function bindDepartment(lst) {
            var html = "";
            $.each(lst, function (index, obj) {
                html += "<option value='" + obj.Id + "'>" + obj.Name + "</option>";
            });

            $("#lstDepartments").html(html);
        }

        function userOnclick(lnk) {
            var id = lnk.getAttribute('userId');
            var pageUrl = '<%=ResolveUrl("UserMng.aspx")%>';

            $.ajax({
                type: "POST",
                url: pageUrl + "/userClicked",
                data: JSON.stringify({
                    'id': id
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#lstRoles").val(data.d.roleId).change();
                    $("#lstDepartments").val(data.d.departmentId).change();
                    $('#txtName').val(data.d.Name);
                    $('#txtName').attr('userId', data.d.Id);
                    $('#txtEmail').val(data.d.Email);
                    $('#txtPass').val(data.d.Pass);
                    $('#txtDetails').val(data.d.Details);
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function AddUser() {
            var id = $('#txtName').attr("userId");
            var rId = $("#lstRoles").val();
            var dpId = $("#lstDepartments").val();
            var name = $("#txtName").val();
            var email = $("#txtEmail").val();
            var pass = $("#txtPass").val();
            var details = $("#txtDetails").val();

            var pageUrl = '<%=ResolveUrl("UserMng.aspx")%>';

            if (typeof (id) !== 'undefined') {
                alert("[ERROR]: the item was selected to be EDITED, not to be INSERTED!!!");
                return;
            }

            if (rId === "" || dpId === "" || name === "" || email === "" || pass === "") {
                alert("ERROR: user Name, Email and Pass are required!!!");
                return;
            }

            $.ajax({
                type: "POST",
                url: pageUrl + "/Client_Adduser",
                data: JSON.stringify({
                    'rId': rId,
                    'dpId': dpId,
                    'name': name,
                    'email': email,
                    'pass': pass,
                    'details': details
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (!data.d) {
                        alert("ERROR: Cannot insert the user into the database!!!");
                    } else {
                        alert("SUCCESS: New user successfully inserted into the database!!!");
                    }
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function EditUser() {
            var id = $('#txtName').attr("userId");
            var pageUrl = '<%=ResolveUrl("UserMng.aspx")%>';

            if (typeof (id) === "undefined") {
                alert("[ERROR]: No user was selected to be edited!!!");
                return;
            }
            var rId = $("#lstRoles").val();
            var dpId = $("#lstDepartments").val();
            var name = $("#txtName").val();
            var email = $("#txtEmail").val();
            var pass = $("#txtPass").val();
            var details = $("#txtDetails").val();


            $.ajax({
                type: "POST",
                url: pageUrl + "/Client_Updateuser",
                data: JSON.stringify({
                    'id': id,
                    'rId': rId,
                    'dpId': dpId,
                    'name': name,
                    'email': email,
                    'pass': pass,
                    'details': details
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (!data.d) {
                        alert("[ERROR]: Failed when update user in DB!!!");
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
                    List of Users
                </div>
                <div class="panel-body">
                    <div id="" class="form-inline dt-bootstrap no-footer">
                        <div class="row" style="margin-top: 10px">
                            <div class="col-sm-12">
                                <table class="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" id="dataTables-example" role="grid" aria-describedby="dataTables-example_info" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th>ID</th>
                                            <th>Role Name</th>
                                            <th>Department Name</th>
                                            <th>User Name</th>
                                            <th>Email</th>
                                            <th>Details</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody id="lstUsers">
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
                    User Details
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>Role</label>
                        <select class="form-control input-sm" id="lstRoles">
                        </select>
                    </div>
                    <div class="form-group">
                        <label>Department</label>
                        <select class="form-control input-sm" id="lstDepartments">
                        </select>
                    </div>
                    <div class="form-group">
                        <label>User Name</label>
                        <input type="text" id="txtName" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>Email</label>
                        <input type="text" id="txtEmail" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>Pass</label>
                        <input type="Text" id="txtPass" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>Details</label>
                        <textarea id="txtDetails" class="form-control" rows="3"></textarea>
                    </div>

                    <div style="text-align: center">
                        <button id="btnAddUser" class="btn btn-success" onclick="AddUser()">Add New User</button>
                        <button id="btnEditUser" class="btn btn-warning" onclick="EditUser()">Edit User</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
