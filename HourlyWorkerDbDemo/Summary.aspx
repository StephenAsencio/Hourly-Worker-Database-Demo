<%@ Page Title="Summary" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Summary.aspx.cs" Inherits="HourlyWorkerDbDemo.Summary" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
              
    <div class="row">
                <div class="col-md-4 col-md-offset-4">
                    <p>
                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableHeaderCell>
                                    Total Workers:
                                </asp:TableHeaderCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblTotalWorkersOutput" runat="server" Text="0" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableHeaderCell>
                                    Total Hours:
                                </asp:TableHeaderCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblTotalHoursOutput" runat="server" Text="0" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableHeaderCell>
                                    Total Pay:
                                </asp:TableHeaderCell>
                                <asp:TableCell>
                                    <asp:Label ID="lblTotalPayOutput" runat="server" Text="$0.00" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableHeaderCell>
                                    Average Pay:
                                </asp:TableHeaderCell>
                                <asp:TableCell>
                                   <asp:Label ID="lblAveragePayOutput" runat="server" Text="$0.00" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        
                    </p>

                </div>
            </div>
</asp:Content>
