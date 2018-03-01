<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="COMP___1640.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--CSS - JS links-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--    <div class="container body-content col-12">
        <div class="center jumbotron col-12">
            <h1>Welcome to No Mercy</h1>
            <h2>This is the home page for the <a href="Home.aspx">No Mercy</a> sample application.
            </h2>
            <a href="Login.aspx" class="btn btn-lg btn-primary">Sign in</a>
        </div>
    </div>--%>

    <div class="container body-content col-12">

        <div class="col-12">
            <form action="/posts" accept-charset="UTF-8" method="get">
                <div class="form-group input-group">
                    <input type="text" name="search" id="search" placeholder="Search post" class="form-control" />
                    <span class="input-group-btn">
                        <input type="submit" value="Search" class="btn btn-default" data-disable-with="Search" />
                    </span>
                </div>
            </form>
        </div>
        <p class="button" class="btn btn-default btn-sm">
            <a href="/posts/new">New Post</a>
        </p>

        <div class="center col-12">
            <div class="col-12">
                <div class="well">
                    <p><a href="#">Admin</a> posted:</p>
                    <h2><a href="#">US masses stealth jets in South Korea for war games</a></h2>
                    <p>
                        (CNN)US F-22 fighter jets roared into the sky over South Korea on Monday to start air combat exercises that North Korea says are pushing the peninsula to the brink of nuclear war.
                        A US 7th Air Force official said the top-of-the-line F-22s are being joined by Air Force and Marine Corps F-35s in the largest concentration of fifth-generation fighter jets ever in South Korea.
                    </p>
                    <span class="timestamp">
                        posted 28 days ago.
                    </span>
                </div>
            </div>

            <div class="col-12">
                <div class="well">
                    <p><a href="#">Vinh</a> posted:</p>
                    <h2><a href="#">President, or Emperor? Xi Jinping pushes China back to one-man rule</a></h2>
                    <p>
                        This could lead to future instability in the world's most populous country as wannabe successors jockey for power within a Communist Party (CCP) completely dominated by the 64-year-old Xi.
                        And his absolute authority will also leave him vulnerable to absolute blame in the instance of an economic shock or foreign policy crisis.
                    </p>
                    <span class="timestamp">
                        posted 69 days ago.
                    </span>
                </div>
            </div>

            <div class="col-12">
                <div class="well">
                    <p><a href="#">Gibson</a> posted:</p>
                    <h2><a href="#">US masses stealth jets in South Korea for war games</a></h2>
                    <p>
                        All-electric motorsport took a giant leap into the future last month, with Formula E unveiling it's Batmobile-like Gen2 race car.

                        Now three races into its fourth season, FE unveiled the vehicle its racers will be piloting when season five gets under way later this year.
                        With its futuristic styling and increase in power and range, the Gen2 car further distances itself from established racing categories like Formula One and IndyCar.
                    </p>
                    <span class="timestamp">
                        posted 185 days ago.
                    </span>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
