<%@ Page Title="Employee List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="HourlyWorkerDbDemo.EmployeeList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8 col-md-offset-2">
            <p>
                <asp:GridView id="gvWorkerList" runat="server" />
            </p>
            <p>
                <asp:Label ID="lblError" runat="server" />
            </p>
        </div>
    </div>
</asp:Content>
