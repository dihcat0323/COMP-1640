<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="COMP___1640.WebForm6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            LoadData(0);
        });
        function LoadData(page) {
            var pageUrl = '<%=ResolveUrl("Category.aspx")%>';
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
                        bindCategorys(data.d.ListCategories);
                    }
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function bindCategorys(lst) {
            var html = "";
            $.each(lst, function (index, obj) {
                html += "<tr>";
                html += "<td>" + obj.Id + "</td>";
                html += "<td><a href='#'>" + obj.Name + "</a></td>";
                html += "<td>" + obj.Description + "</td>";
                html += "<td><a href='#' onclick='CategoryOnclick(this)' CategoryId='" + obj.Id + "'>Edit Item</a></td>";
                html += "</tr>";
            });

            $("#lstCategorys").html(html);
        }

        function CategoryOnclick(lnk) {
            var id = lnk.getAttribute('CategoryId');
            var pageUrl = '<%=ResolveUrl("Category.aspx")%>';
            $.ajax({
                type: "POST",
                url: pageUrl + "/CategoryClicked",
                data: JSON.stringify({
                    'categoryId': id
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $('#txtName').val(data.d.Name);
                    $('#txtName').attr("CategoryId", data.d.Id);
                    $('#txtDescription').val(data.d.Description);
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function AddCategory() {
            var id = $('#txtName').attr("CategoryId");
            var name = $('#txtName').val();
            var des = $('#txtDescription').val();
            var pageUrl = '<%=ResolveUrl("Category.aspx")%>';

            if (typeof (id) !== 'undefined') {
                alert("[ERROR]: The item was selected to be EDITED, not to be INSERTED!!!");
                return;
            }

            if (name === "") {
                alert("ERROR: Category Name is required!!!");
                return;
            }

            $.ajax({
                type: "POST",
                url: pageUrl + "/Client_AddCategory",
                data: JSON.stringify({
                    'name': name,
                    'description': des
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (!data.d) {
                        alert("ERROR: Cannot insert the Category into the database!!!");
                    } else {
                        alert("SUCCESS: New Category successfully inserted into the database!!!");
                    }
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function EditCategory() {
            var id = $('#txtName').attr("CategoryId");
            var name = $('#txtName').val();
            var des = $('#txtDescription').val();
            var pageUrl = '<%=ResolveUrl("Category.aspx")%>';

            if (name === "") {
                alert("ERROR: Category Name is required!!!");
                return;
            }

            if (typeof (id) === "undefined") {
                alert("[ERROR]: No Category was selected to be edited!!!");
                return;
            }

            $.ajax({
                type: "POST",
                url: pageUrl + "/Client_UpdateCategory",
                data: JSON.stringify({
                    'id': id,
                    'name': name,
                    'description': des
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (!data.d) {
                        alert("[ERROR]: Failure updating Category in DB!!!");
                    } else {
                        alert("[SUCCESS]: Updated successfully!!!");
                    }
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function DeleteCategory() {
            var id = $('#txtName').attr("CategoryId");
            var pageUrl = '<%=ResolveUrl("Category.aspx")%>';

            if (typeof (id) === "undefined") {
                alert("[ERROR]: No Category was selected to be deleted!!!");
                return;
            }

            $.ajax({
                type: "POST",
                url: pageUrl + "/Client_DeleteCategory",
                data: JSON.stringify({
                    'id': id
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (!data.d) {
                        alert("[ERROR]: Failure deleting Category in DB!!!");
                    } else {
                        alert("[SUCCESS]: Category deleted successfully!!!");
                    }
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

    </script>

    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    List of Categories
                </div>
                <div class="panel-body">
                    <div id="" class="form-inline dt-bootstrap no-footer">
                        <div class="row" style="margin-top: 10px">
                            <div class="col-sm-12">
                                <table class="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" id="dataTables-example" role="grid" aria-describedby="dataTables-example_info" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th>ID</th>
                                            <th>Category Name</th>
                                            <th>Description</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody id="lstCategorys">
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
                    Category Details
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>Category Name</label>
                        <input type="text" id="txtName" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>Description</label>
                        <textarea id="txtDescription" class="form-control" rows="3"></textarea>
                    </div>

                    <div style="text-align: center">
                        <button id="btnAddCategory" class="btn btn-success" onclick="AddCategory()">Add New Category</button>
                        <button id="btnEditCategory" class="btn btn-warning" onclick="EditCategory()">Edit Category</button>
                        <button id="btnDeleteCategory" class="btn btn-danger" onclick="DeleteCategory()">Delete Category</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
