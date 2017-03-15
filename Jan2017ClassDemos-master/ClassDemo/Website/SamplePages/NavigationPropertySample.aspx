<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NavigationPropertySample.aspx.cs" Inherits="SamplePages_NavigationPropertySample" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
   
     <asp:GridView ID="AlbumArtistList" runat="server" AutoGenerateColumns="False" DataSourceID="AlbumArtistODS" AllowPaging="True">
        <Columns>
            <asp:BoundField DataField="Artist" HeaderText="Artist" SortExpression="Artist"></asp:BoundField>
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title"></asp:BoundField>
            <asp:BoundField DataField="ReleaseYear" HeaderText="ReleaseYear" SortExpression="ReleaseYear"></asp:BoundField>
            <asp:BoundField DataField="ReleaseLabel" HeaderText="ReleaseLabel" SortExpression="ReleaseLabel"></asp:BoundField>
        </Columns>
    </asp:GridView>
    
    <asp:ObjectDataSource ID="AlbumArtistODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="ListAlbumsbyArtist" 
        TypeName="ChinookSystem.BLL.AlbumController">
    </asp:ObjectDataSource>
</asp:Content>

