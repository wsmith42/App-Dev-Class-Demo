<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CRUDReview.aspx.cs" Inherits="SamplePages_CRUDReview" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <h1>CRUD in Tabs</h1>
       <!--Common Update panel for user message control -->
        <asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
            <ContentTemplate>
                <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="row">
        <div class="col-md-12">
          <!--script for tab to tab movement -->
            <script>
                function nextButton(anchorRef) {
                    $('a[href="' + anchorRef + '"]').tab('show');
                }
            </script>
            <!--Nav tabs-->
            <ul class="nav nav-tabs">
                <li class="active"><a href="#find" data-toggle="tab">Find</a></li>
                <li ><a href="#maintain" data-toggle="tab">Maintain</a></li>
                <li ><a href="#lvmaintain" data-toggle="tab">ListView</a></li>
             
            </ul>

            <!--Tab panes one for each tab-->
            <div class="tab-content"> 
        
                <div class="tab-pane fade in active" id="find">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                             <!--  user message control for this panel -->
                                <uc1:MessageUserControl runat="server" ID="MessageUserControl1" />
                                <asp:Label ID="Label1" runat="server" Text="Album Title:"></asp:Label>&nbsp;&nbsp;
                                <asp:TextBox ID="SearchArg" runat="server"></asp:TextBox>&nbsp;&nbsp;

                                <!-- Search buttons do not want Validation(on tap 2) to happen on the web page -->
                                <asp:Button  ID="Search" runat="server" OnClick="Search_Click" Text="Search" CausesValidation="False"></asp:Button>
                            <br /><br />
                                <asp:GridView ID="AlbumList" runat="server" AutoGenerateColumns="False" 
                                    OnSelectedIndexChanged="AlbumList_SelectedIndexChanged">
                                    <Columns>
                                        <asp:CommandField ShowSelectButton="True"></asp:CommandField>
                                        <asp:TemplateField HeaderText="Title">
                                            <ItemTemplate>
                                                <asp:Label Id="Title" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Year">
                                            <ItemTemplate>
                                                <asp:Label Id="Year" runat="server" Text='<%# Eval("ReleaseYear") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Label">
                                            <ItemTemplate>
                                                <asp:Label Id="AlbumLabel" runat="server" Text='<%# Eval("ReleaseLabel") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AlbumID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label Id="AlbumID" runat="server" Text='<%# Eval("AlbumId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="ArtistID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label Id="ArtistID" runat="server" Text='<%# Eval("ArtistId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br /><br />
                                <asp:Label ID="Label2" runat="server" Text="Selected"></asp:Label>

                            <!--added attribute to button to call js script for tab to tab movement-->
                                <button type="button" class="btn" onclick="nextButton('#maintain')" >Next</button><br />

                                <asp:Label ID="SelectedTitle" runat="server" ></asp:Label>

                           
                        </ContentTemplate>
                    </asp:UpdatePanel>   
                </div>
                 <div class="tab-pane fade" id="maintain">
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                             <!--  user message control for this panel -->
                            <uc1:MessageUserControl runat="server" ID="MessageUserControl2" />
                            <!--validation controls on Insert, Update, and Delete tab panel-->
                            <asp:ValidationSummary ID="ContrestValidationSummary" runat="server" BackColor="#ff99cc"
                                HeaderText="Please correct the following parts of the form before submitting:<br/>" />
                            <asp:RequiredFieldValidator ID="RequiredFieldAlbumTitle" runat="server" ForeColor="#990000" 
                                ErrorMessage="Title required field" Display="None" ControlToValidate="AlbumTitle" SetFocusOnError="True">
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldAlbumReleaseYear" runat="server" ForeColor="#990000" 
                                ErrorMessage="Year required field" ControlToValidate="AlbumReleaseYear" SetFocusOnError="True"  Display="None">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareAlbumReleaseYear" runat="server" ForeColor="#990000" Display="None"
                                ErrorMessage="Invalid year not numeric." ControlToValidate="AlbumReleaseYear" SetFocusOnError="True" 
                                Type="Integer" Operator="DataTypeCheck">
                            </asp:CompareValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionAlbumReleaseYear" runat="server" Display="None"
                                 ControlToValidate="AlbumReleaseYear" ValidationExpression="[0-9][0-9][0-9][0-9]" 
                                SetFocusOnError="True" ForeColor="#990000" ErrorMessage="Invalid year format (yyyy).">
                            </asp:RegularExpressionValidator>    

                            <!--form-->
                               <fieldset class="form-horizontal">
                                    <legend>Album Maintenance</legend>
                                    <asp:Label ID="Label4" runat="server" Text="Album ID:"
                                        AssociatedControlID="AlbumID"></asp:Label> 
                                    <asp:Label ID="AlbumID" runat="server" ></asp:Label><br />
                                    <asp:Label ID="Label6" runat="server" Text="Title"
                                         AssociatedControlID="AlbumTitle"></asp:Label>
                                    <asp:TextBox ID="AlbumTitle" runat="server"></asp:TextBox><br />
                                    <asp:Label ID="Label3" runat="server" Text="Artist"
                                         AssociatedControlID="ArtistList"></asp:Label>
                                    <asp:DropDownList ID="ArtistList" runat="server" 
                                        DataSourceID="ArtistListODS" 
                                        DataTextField="Name" 
                                        DataValueField="ArtistId">
                                    </asp:DropDownList><br />
                                    <asp:Label ID="Label7" runat="server" Text="Year"
                                         AssociatedControlID="AlbumReleaseYear"></asp:Label>
                                    <asp:TextBox ID="AlbumReleaseYear" runat="server"></asp:TextBox><br />
                                    <asp:Label ID="Label8" runat="server" Text="Label"
                                         AssociatedControlID="AlbumReleaseLabel"></asp:Label>
                                    <asp:TextBox ID="AlbumReleaseLabel" runat="server"></asp:TextBox><br />
                               </fieldset>
                            
                            <br/>
                                <!-- Delete and Clear buttons do not want Validation to happen on the web page-->
                                <asp:Button cssclass="btn" ID="AddAlbum" runat="server" OnClick="AddAlbum_Click" Text="Add"></asp:Button>
                                <asp:Button cssclass="btn" ID="UpdateAlbum" runat="server" OnClick="UpdateAlbum_Click" Text="Update"></asp:Button>
                                <asp:Button cssclass="btn" ID="DeleteAlbum" runat="server" OnClick="DeleteAlbum_Click" Text="Delete" CausesValidation="false"></asp:Button>
                                <asp:Button cssclass="btn" ID="Clear" runat="server" OnClick="Clear_Click" Text="Clear" CausesValidation="false"></asp:Button>
                                <button type="button"  class="btn" onclick="nextButton('#find')" >Back</button><br />
                            <br/>
                            <asp:ObjectDataSource ID="ArtistListODS" runat="server" 
                                OldValuesParameterFormatString="original_{0}" 
                                SelectMethod="Artist_ListAll" 
                                TypeName="ChinookSystem.BLL.ArtistController">
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                    </asp:UpdatePanel> 
                </div> 
                 <div class="tab-pane fade" id="lvmaintain">
                      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:ListView ID="AlbumCRUD" runat="server" 
                                DataSourceID="AlbumCRUDODS" 
                                InsertItemPosition="LastItem"
                                 DataKeyNames="AlbumId"
                                 ItemType="Chinook.Data.Enitities.Album">
                                <AlternatingItemTemplate>
                                    <tr style="background-color: #FFFFFF; color: #284775;">
                                        <td>
                                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" CausesValidation="false" />
                                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" CausesValidation="false"/>
                                        </td>
                                        <td>
                                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel"  Width="50"/></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                                        <td>
                                            
                                            <asp:DropDownList ID="ArtistListLV" runat="server" 
                                                DataSourceID="ArtistListODS" 
                                                DataTextField="Name" 
                                                DataValueField="ArtistId"
                                                selectedvalue='<%# Eval("ArtistId") %>'
                                                 Width="250">
                                            </asp:DropDownList></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel"  Width="50" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                                       
                                    </tr>
                                </AlternatingItemTemplate>
                                <EditItemTemplate>
                                    <tr style="background-color: #999999;">
                                        <td>
                                            <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" CausesValidation="false"/>
                                            <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" CausesValidation="false"/>
                                        </td>
                                        <td>
                                            <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox"  Width="50" /></td>
                                        <td>
                                            <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBox" /></td>
                                        <td>
                                                <asp:DropDownList ID="ArtistListLV" runat="server" 
                                                DataSourceID="ArtistListODS" 
                                                DataTextField="Name" 
                                                DataValueField="ArtistId"
                                                selectedvalue='<%# Bind("ArtistId") %>'
                                                     Width="250">
                                            </asp:DropDownList></td>
                                        <td>
                                            <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBox"  Width="50" /></td>
                                        <td>
                                            <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBox" /></td>
                                        
                                    </tr>
                                </EditItemTemplate>
                                <EmptyDataTemplate>
                                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                                        <tr>
                                            <td>No data was returned.</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <InsertItemTemplate>
                                    <tr style="">
                                        <td>
                                            <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" CausesValidation="false"/>
                                            <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" CausesValidation="false"/>
                                        </td>
                                        <td>
                                            <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox"  Width="50" /></td>
                                        <td>
                                            <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBox" /></td>
                                        <td>
                                                <asp:DropDownList ID="ArtistListLV" runat="server" 
                                                DataSourceID="ArtistListODS" 
                                                DataTextField="Name" 
                                                DataValueField="ArtistId"
                                                selectedvalue='<%# Bind("ArtistId") %>'
                                                     Width="250">
                                            </asp:DropDownList></td>
                                        <td>
                                            <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBox"  Width="50" /></td>
                                        <td>
                                            <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBox" /></td>
                                       
                                    </tr>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: #E0FFFF; color: #333333;">
                                        <td>
                                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" CausesValidation="false"/>
                                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" CausesValidation="false"/>
                                        </td>
                                        <td>
                                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel"  Width="50" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                                        <td>
                                                <asp:DropDownList ID="ArtistListLV" runat="server" 
                                                DataSourceID="ArtistListODS" 
                                                DataTextField="Name" 
                                                DataValueField="ArtistId"
                                                selectedvalue='<%# Eval("ArtistId") %>'
                                                     Width="250">
                                            </asp:DropDownList></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel"  Width="50" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                                       
                                    </tr>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <table runat="server">
                                        <tr runat="server">
                                            <td runat="server">
                                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                                    <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                                        <th runat="server"></th>
                                                        <th runat="server">Id</th>
                                                        <th runat="server">Title</th>
                                                        <th runat="server">Artist</th>
                                                        <th runat="server">Year</th>
                                                        <th runat="server">Label</th>
                                                       
                                                    </tr>
                                                    <tr runat="server" id="itemPlaceholder"></tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server">
                                            <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                                                <asp:DataPager runat="server" ID="DataPager1">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                        <asp:NumericPagerField></asp:NumericPagerField>
                                                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                    </Fields>
                                                </asp:DataPager>
                                            </td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <SelectedItemTemplate>
                                    <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                                        <td>
                                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" CausesValidation="false"/>
                                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" CausesValidation="false"/>
                                        </td>
                                        <td>
                                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel"  Width="50"/></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                                        <td>
                                                <asp:DropDownList ID="ArtistListLV" runat="server" 
                                                DataSourceID="ArtistListODS" 
                                                DataTextField="Name" 
                                                DataValueField="ArtistId"
                                                selectedvalue='<%# Eval("ArtistId") %>'
                                                     Width="250">
                                            </asp:DropDownList></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel"  Width="50"/></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                                     
                                    </tr>
                                </SelectedItemTemplate>
                            </asp:ListView>
                            <asp:ObjectDataSource ID="AlbumCRUDODS" runat="server"
                                 DataObjectTypeName="Chinook.Data.Enitities.Album" 
                                DeleteMethod="Albums_Delete" 
                                InsertMethod="Albums_Add"
                                SelectMethod="Albums_List"  
                                UpdateMethod="Albums_Update"
                                OldValuesParameterFormatString="original_{0}" 
                                TypeName="ChinookSystem.BLL.AlbumController" 
                                 OnSelected="CheckForException"
                                 OnDeleted="CheckForException"
                                 OnInserted="CheckForException"
                                 OnUpdated="CheckForException"
                               >
                            </asp:ObjectDataSource>
                            <%-- 
                                this ODS already exists in tab2 therefore I do not need
                                to recreate for this tab.
                                IF the tab was on a page by itself, then I would need
                                to create the ArtistListODS
                                <asp:ObjectDataSource ID="ArtistListODS" runat="server" 
                                OldValuesParameterFormatString="original_{0}" 
                                SelectMethod="Artist_ListAll" 
                                TypeName="ChinookSystem.BLL.ArtistController">
                            </asp:ObjectDataSource>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>   
                </div>


           </div>
        </div>
    </div>
  <%-- to use Bootwrap-freecode for formating as shown in CPSC1517
       this will NOT work here as the author of the code
        did not design the javascript to handle bootstrap tabs
        
      <script src="../Scripts/bootwrap-freecode.js"></script>--%>
</asp:Content>

