<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Etherscan._Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-body">
            <h5 class="card-title">Save/Update Token</h5>
            <div class="row">
                <div class="col-6">
                    <table class="token-form">
                        <tr>
                            <td>Name</td>
                            <td>
                                <asp:Label ID="lblId" runat="server" CssClass="form-control" Hidden="true" ReadOnly="true" />
                                  <asp:Label ID="lblPrice" runat="server" CssClass="form-control" Hidden="true" ReadOnly="true" />
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name" /></td>
                        </tr>
                        <tr>
                            <td>Symbol</td>
                            <td>
                                <asp:TextBox ID="txtSymbol" runat="server" CssClass="form-control" placeholder="Symbol" MaxLength="8" /></td>
                        </tr>
                        <tr>
                            <td>Contract Address</td>
                            <td>
                                <asp:TextBox ID="txtContractAddress" runat="server" CssClass="form-control" placeholder="Contract Address" /></td>
                        </tr>
                        <tr>
                            <td>Total Supply</td>
                            <td>
                                <asp:TextBox ID="txtTotalSupply" runat="server" CssClass="form-control" placeholder="Total Supply" TextMode="Number" /></td>
                        </tr>
                        <tr>
                            <td>Total Holders</td>
                            <td>
                                <asp:TextBox ID="txtTotalHolders" runat="server" CssClass="form-control" placeholder="Total Holders" TextMode="Number" /></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2">
                                <asp:Button ID="btnSave" Text="Save" runat="server" OnClick="Save" CssClass="btn btn-primary" />
                                <asp:Button ID="btnReset" Text="Reset" runat="server" OnClick="Reset" CssClass="btn btn-info" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-6">
                    <asp:Chart ID="TokenStatisChart" runat="server"
                        BorderlineWidth="0" Height="360px" Palette="None" PaletteCustomColors="Maroon"
                        Width="380px" BorderlineColor="64, 0, 64">
                        <Titles>
                            <asp:Title ShadowOffset="10" Name="Token" />
                        </Titles>
                        <Series>
                            <asp:Series Name="Default" />
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                        </ChartAreas>
                    </asp:Chart>
                </div>
                <div class="col-12">
                     <asp:Button ID="btnExport" runat="server" Text="Export To Csv" OnClick="onExport" class="btn btn-success"/>  
                    <asp:GridView ID="TokenGrid" runat="server" AutoGenerateColumns="false"
                        DataKeyNames="id" ClientIDMode="Static"
                        PageSize="10" AllowPaging="true" OnPageIndexChanging="OnPaging"
                        EmptyDataText="No records has been added."
                        class="table">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="Rank" />
                            <asp:TemplateField  HeaderText="Symbol" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <a runat="server" ID="lnkDet" href='<%# $"~/detail?id={Eval("Symbol")}"%>' class="btn btn-link"><%# Eval("Symbol") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="ContractAddress" HeaderText="Contract Address" />
                            <asp:BoundField DataField="TotalHolders" HeaderText="Total Holders" />
                            <asp:BoundField DataField="TotalSupply" HeaderText="Total Supply" />
                            <asp:BoundField DataField="PercentTotalSupply" HeaderText="Total Supply %" />
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" Text="Edit" runat="server" OnClick="Edit" CssClass="btn btn-link" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
