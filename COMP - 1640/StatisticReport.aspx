<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="StatisticReport.aspx.cs" Inherits="COMP___1640.WebForm9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            LoadTopics();
            LoadDepartments();
        });

        function LoadTopics() {
            var pageUrl = '<%=ResolveUrl("StatisticReport.aspx")%>';

            $.ajax({
                type: "POST",
                url: pageUrl + "/TotalIdeasPerTopic",
                data: JSON.stringify({
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d) {
                        bindTopics(data.d);
                    }
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
                html += "<td>" + obj.Name + "</td>";
                html += "<td>" + obj.TotalIdeas + "</td>";
                html += "<td>" + obj.Percentages + " %</td>";
                html += "</tr>";
            });

            $("#lstTopics").html(html);
        }

        function LoadDepartments() {
            var pageUrl = '<%=ResolveUrl("StatisticReport.aspx")%>';

            $.ajax({
                type: "POST",
                url: pageUrl + "/DepartmentStatistics",
                data: JSON.stringify({
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d) {
                        bindDepartments(data.d);
                    }
                },
                failure: function (errMsg) {
                    alert(errMsg);
                },
            });
        }

        function bindDepartments(lst) {
            var html = "";
            $.each(lst, function (index, obj) {
                html += "<tr>";
                html += "<td>" + obj.Name + "</td>";
                html += "<td>" + obj.TotalIdeas + "</td>";
                html += "<td>" + obj.IdeaPercentages + " %</td>";
                html += "</tr>";
            });

            $("#lstDeps").html(html);
        }
    </script>

    <div class="col-md-6">
        <div class="panel panel-default">
            <div class="panel-heading">
                Ideas and topics statistic
            </div>
            <div class="panel-body">
                <div class="col-sm-12">
                    <table class="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" id="dataTables-example" role="grid" aria-describedby="dataTables-example_info" style="width: 100%;">
                        <thead>
                            <tr>
                                <th>Topic Name</th>
                                <th>Total Ideas</th>
                                <th>Percentage on Total Ideas</th>
                            </tr>
                        </thead>
                        <tbody id="lstTopics">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-6">
        <div class="panel panel-default">
            <div class="panel-heading">
                Ideas and Comments
            </div>
            <div class="panel-body">
                <table class="table table-striped table-bordered table-hover dataTable no-footer dtr-inline">
                    <tr>
                        <td>
                            <label>Total Ideas (count)</label></td>
                        <td>
                            <asp:Label ID="lblTotalIdeas" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <label>Total Comments (count)</label></td>
                        <td>
                            <asp:Label ID="lblTotalCmts" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <label>Anoymous Ideas (percentage)</label></td>
                        <td>
                            <asp:Label ID="lblIdeas" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <label>Anoymous Comments (percentage)</label></td>
                        <td>
                            <asp:Label ID="lblCmts" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <label>Ideas without comment (percentage)</label></td>
                        <td>
                            <asp:Label ID="lblIdeas2" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <div class="col-md-6">
        <div class="panel panel-default">
            <div class="panel-heading">
                Department Statistics
            </div>
            <div class="panel-body">
                <div class="col-sm-12">
                    <table class="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" id="dataTables-example" role="grid" aria-describedby="dataTables-example_info" style="width: 100%;">
                        <thead>
                            <tr>
                                <td><label>Department</label></td>
                                <td><label>Ideas (count)</label></td>
                                <td><label>Ideas (percentages)</label></td>
                            </tr>
                        </thead>
                        <tbody id="lstDeps">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
