<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Post.aspx.cs" Inherits="COMP___1640.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container body-content col-12">

        <div class="container">
                <div class="row">
                    <aside class="col-md-8">
                        <section class="user_info">
                            <h1>
                                <a href="#">Admin</a>
                            </h1>
                            <div class="timestamp">
                                posted 28 days ago.
                                <p># <a href="/tags/test">test</a></p>
                            </div>
                            <div class="title"><big><h2>McMaster: Potential for war with North Korea 'increasing every day'</h2></big></div>
                        </section>
                        <div class="content"><p>(CNN)White House national security adviser HR McMaster said Saturday that North Korea represents "the greatest immediate threat to the United States" and that the potential for war with the communist nation is growing each day.</p></div>
                        <a href="#">Edit</a>
                    </aside>
                    <div class="col-md-4">
                        <h3>Comment (0)</h3>
                        <div class="form-group">
                            <input type="text" class="form-control" id="exampleInputComment" placeholder="Comment here...">
                            <%--<small class="form-text text-muted">Type it in pussy!</small>--%>
                        </div>
                    </div>
                </div>
            </div>
            
        </div>

    
</asp:Content>
