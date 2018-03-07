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
    </script>
</asp:Content>
