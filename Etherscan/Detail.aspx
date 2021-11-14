<%@ Page Title="Token Detail" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Detail.aspx.cs" Inherits="Etherscan.Detail" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <meta http-equiv="refresh" content="300"/>
    <div class="card">
        <div class="card-body">
            <h5 class="card-title"><asp:Label ID="lblcontractAddress" runat="server"  /></h5>

              <table class="token-detail">
                        <tr>
                            <td>Price:</td>
                            <td>
                                <b> $<asp:Label ID="lblPrice" runat="server"  /></b>
                        </tr>
                        <tr>
                            <td>Toltal Supply:</td>
                            <td>
                              <asp:Label ID="lblToltalSupply" runat="server"  />  <asp:Label ID="lblToltalSymbol" runat="server"  />

                            </td>
                        </tr>
                        <tr>
                            <td>Total Holders:</td>
                            <td>
                                 <asp:Label ID="lblToltalHolders" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td>Name:</td>
                            <td>
                               <asp:Label ID="lblName" runat="server"  /></td>
                        </tr>
                    </table>
        </div>
    </div>
</asp:Content>
