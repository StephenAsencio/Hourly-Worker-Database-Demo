<%@ Page Title="Payroll Entry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PayrollEntry.aspx.cs" Inherits="HourlyWorkerDbDemo.PayrollEntry" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-6">
            
            <p>
                Worker First Name:
                <asp:TextBox ID="txtWorkerFirstName" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="txtWorkerFirstName" ErrorMessage="You must enter a first name for the worker" runat="server" ForeColor="#FF3300" Display="Dynamic" /><br />

                Worker Last Name:
                <asp:TextBox ID="txtWorkerLastName" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="txtWorkerLastName" ErrorMessage="You must enter a last name for the worker" runat="server" ForeColor="#FF3300" Display="Dynamic" /><br />

                Pay Rate:
                <asp:TextBox ID="txtRate" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="txtRate" ErrorMessage="You must enter the worker's pay rate" runat="server" ForeColor="#FF3300" Display="Dynamic" />
                <asp:RangeValidator ControlToValidate="txtRate" ErrorMessage="The pay rate must be between $15 and some obscenely high number" MaximumValue="10000000" MinimumValue="15" runat="server" Type="Double" ForeColor="#FF3300" Display="Dynamic" /><br />
                
                Hours Worked:
                <asp:TextBox ID="txtHours" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="txtHours" ErrorMessage="You must enter the worker's hours worked" runat="server" ForeColor="#FF3300" Display="Dynamic" />
                <asp:RangeValidator ControlToValidate="txtHours" ErrorMessage="The hours worked must be between 0 and 336" MaximumValue="336" MinimumValue="0" runat="server" Type="Double" ForeColor="#FF3300" Display="Dynamic" /><br />
                
            </p>

            <p>
                <asp:RadioButton ID="rdoHourlyWorker" runat="server" Text="Hourly Worker" GroupName="workerType" Checked="True" /><br />
                <asp:RadioButton ID="rdoSalariedWorker" runat="server" Text="Salaried Worker" GroupName="workerType" />
            </p>

            <p>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" />
            </p>

            <p>
                <asp:Button ID="btnCalculate" runat="server" Text="Calculate" AccessKey="C" OnClick="btnCalculate_Click" />&nbsp;
                <asp:Button ID="btnClear" runat="server" Text="Clear" AccessKey="L" OnClick="btnClear_Click" CausesValidation="False" />
            </p>


        </div>
        <div class="col-md-6">
            
            <asp:Label ID="lblWorkerConfirmation" runat="server" Text="" /><br /><br />

            Last Worker's Pay: <asp:Label ID="lblWorkerPay" runat="server" Text="" /><br /><br />

            Total Number of Workers: <asp:Label ID="lblTotalWorkers" runat="server" Text="" /><br />
            Total Pay For All Workers: <asp:Label ID="lblTotalPay" runat="server" Text="" /><br />
            
        </div>
        
    </div>
</asp:Content>
