<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HourlyWorkerDbDemo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Peicework Babeyyy</h1>
        <p class="lead">IncInc does great stuff! If you log in, it's even greater! Trust me!</p>
    </div>

    <asp:LoginView runat="server" ViewStateMode="Disabled">

        <AnonymousTemplate>
           <div class="row">
                <div class="col-md-2 col-md-offset-4">
                    <a href="Account/Login.aspx" class="btn btn-block btn-primary">Login</a>
                </div>
                <div class="col-md-2">
                    <a href="Account/Register.aspx" class="btn btn-block btn-primary">Register</a>
                </div>
            </div>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <div class="row">
                <div class="col-md-2 col-md-offset-3">
                    <a href="PayrollEntry.aspx" class="btn btn-block btn-primary">Payroll Entry</a>
                </div>
                <div class="col-md-2">
                    <a href="Summary.aspx" class="btn btn-block btn-info">Summary</a>
                </div>
                <div class="col-md-2">
                   <a href="EmployeeList.aspx" class="btn btn-block btn-info">Employee List</a>
                </div>
            </div>
        </LoggedInTemplate>

    </asp:LoginView>

</asp:Content>
