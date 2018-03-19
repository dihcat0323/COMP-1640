<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Role.aspx.cs" Inherits="COMP___1640.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            LoadData(0);
        });
        function LoadData(page) {
            var pageUrl = '<%=ResolveUrl("Role.aspx")%>';
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
                        bindRoles(data.d.ListRoles);
                    }
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function bindRoles(lst) {
            var html = "";
            $.each(lst, function (index, obj) {
                html += "<tr>";
                html += "<td>" + obj.Id + "</td>";
                html += "<td><a href='#'>" + obj.Name + "</a></td>";
                html += "<td>" + obj.Description + "</td>";
                html += "<td><a href='#' onclick='RoleOnclick(this)' RoleId='" + obj.Id + "'>Edit Item</a></td>";
                html += "</tr>";
            });

            $("#lstRoles").html(html);
        }

        function RoleOnclick(lnk) {
            var id = lnk.getAttribute('RoleId');
            var pageUrl = '<%=ResolveUrl("Role.aspx")%>';
            $.ajax({
                type: "POST",
                url: pageUrl + "/RoleClicked",
                data: JSON.stringify({
                    'roleId': id
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $('#txtName').val(data.d.Name);
                    $('#txtName').attr("roleId", data.d.Id);
                    $('#txtDescription').val(data.d.Description);
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function AddRole() {
            var id = $('#txtName').attr("roleId");
            var name = $('#txtName').val();
            var des = $('#txtDescription').val();
            var pageUrl = '<%=ResolveUrl("Role.aspx")%>';

            if (typeof (id) !== 'undefined') {
                alert("[ERROR]: the item was selected to be EDITED, not to be INSERTED!!!");
                return;
            }

            if (name === "") {
                alert("ERROR: Role Name is required!!!");
                return;
            }

            $.ajax({
                type: "POST",
                url: pageUrl + "/Client_AddRole",
                data: JSON.stringify({
                    'name': name,
                    'description': des
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (!data.d) {
                        alert("ERROR: Cannot insert the role into the database!!!");
                    } else {
                        alert("SUCCESS: New role successfully inserted into the database!!!");
                    }
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function EditRole() {
            var id = $('#txtName').attr("roleId");
            var name = $('#txtName').val();
            var des = $('#txtDescription').val();
            var pageUrl = '<%=ResolveUrl("Role.aspx")%>';

            if (name === "") {
                alert("ERROR: Role Name is required!!!");
                return;
            }

            if (typeof (id) === "undefined") {
                alert("[ERROR]: No role was selected to be edited!!!");
                return;
            }

            $.ajax({
                type: "POST",
                url: pageUrl + "/Client_UpdateRole",
                data: JSON.stringify({
                    'id': id,
                    'name': name,
                    'description': des
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (!data.d) {
                        alert("[ERROR]: Failed when update role in DB!!!");
                    } else {
                        alert("[SUCCESS]: Updated successfully!!!");
                    }
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

    </script>

    <div class="row">
        <aside class="col-lg-3">
            <ul class="nav nav-pills nav-stacked">
                <li class="dropdown-header">Navigation</li>
                <li><a href="#">Comments</a></li>
                <li><a href="#">Posts</a></li>
                <li><a href="#">Tags</a></li>
                <li><a href="#">Users</a></li>
            </ul>
        </aside>

        <div class="col-lg-9">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        List of Roles
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
                                                <th>Description</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="lstRoles">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Role Details
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Role Name</label>
                            <input type="text" id="txtName" class="form-control" />
                        </div>
                        <div class="form-group">
                            <label>Descriptions</label>
                            <textarea id="txtDescription" class="form-control" rows="3"></textarea>
                        </div>

                        <div style="text-align: center">
                            <button id="btnAddRole" class="btn btn-success" onclick="AddRole()">Add New Role</button>
                            <button id="btnEditRole" class="btn btn-warning" onclick="EditRole()">Edit Role</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
