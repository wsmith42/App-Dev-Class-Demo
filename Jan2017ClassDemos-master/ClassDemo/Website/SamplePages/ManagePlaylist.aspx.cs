using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.BLL;
using Chinook.Data.POCOs;

#endregion
public partial class SamplePages_ManagePlaylist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        //PreRenderComplete occurs just after databinding page events
        //load a pointer to point to your DataPager control
        DataPager thePager = TracksSelectionList.FindControl("DataPager1") as DataPager;
        if (thePager !=null)
        {
            //this code will check the StartRowIndex to see if it is greater that the
            //total count of the collection
            if (thePager.StartRowIndex > thePager.TotalRowCount)
            {
                thePager.SetPageProperties(0, thePager.MaximumRows, true);
            }
        }
    }

    protected void ArtistFetch_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(() =>
        {
            TracksBy.Text = "Artist";
            SearchArgID.Text = ArtistDDL.SelectedValue;
            TracksSelectionList.DataBind();//will force the ODS to execute
        });
    }

    protected void MediaTypeFetch_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(() =>
        {
            TracksBy.Text = "MediaType";
            SearchArgID.Text = MediaTypeDDL.SelectedValue;
            TracksSelectionList.DataBind();//will force the ODS to execute
        });
    }

    protected void GenreFetch_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(() =>
        {
            TracksBy.Text = "Genre";
            SearchArgID.Text = GenreDDL.SelectedValue;
            TracksSelectionList.DataBind();//will force the ODS to execute
        });
    }

    protected void AlbumFetch_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(() =>
        {
            TracksBy.Text = "Album";
            SearchArgID.Text = AlbumDDL.SelectedValue;
            TracksSelectionList.DataBind();//will force the ODS to execute
        });
    }

    protected void PlayListFetch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(PlaylistName.Text))
        {
            MessageUserControl.ShowInfo("Warning", "Playlist Name is required.");
        }
        else
        {
            MessageUserControl.TryRun(() =>
            {
                string username = User.Identity.Name;
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                List<UserPlaylistTrack> playlist = sysmgr.List_TracksForPlaylist(
                    PlaylistName.Text, username);
                PlayList.DataSource = playlist;
                PlayList.DataBind(); 
            });
        }
    }

    protected void TracksSelectionList_ItemCommand(object sender, 
        ListViewCommandEventArgs e)
    {
       if (string.IsNullOrEmpty(PlaylistName.Text))
        {
            MessageUserControl.ShowInfo("Warning", "You must supply a playlist name.");
        }
       else
        {
            //obtain the user name.
            string username = User.Identity.Name;
            //obtain the playlist name
            string playlistname = PlaylistName.Text;
            int trackid = int.Parse(e.CommandArgument.ToString());

            //contect to BLL controller
            //call required method
            //refresh the screen
            //do all this under the user friendly error handler
            MessageUserControl.TryRun(() =>
            {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.Add_TrackToPLaylist(playlistname, username, trackid);
                List<UserPlaylistTrack> results = sysmgr.List_TracksForPlaylist(playlistname, username);
                PlayList.DataSource = results;
                PlayList.DataBind();
            },"Playlist Track Added","You have successfully added a new track to your list.");

        }
    }

    protected void MoveUp_Click(object sender, EventArgs e)
    {
        if (PlayList.Rows.Count == 0)
        {
            //did the user press the up button without fetching a playlist
            MessageUserControl.ShowInfo("Warning", "No playlist has been retreived.");
        }
        else
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Warning", "No playlist name exits.");
            }
            else
            {
                //check only one row selected
                int trackid = 0;
                int tracknumber = -1;
                int rowsselected = 0;
                CheckBox playlistselection = null;
                //traverse the gridview checking each row for a checked Checkbox
                for (int i =0; i < PlayList.Rows.Count; i++)
                {
                    //find the checkbox on the indexed gridview row
                    //playlistselection will point to the checkbox
                    playlistselection = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                    if(playlistselection.Checked)
                    {
                        trackid = int.Parse((PlayList.Rows[i].FindControl("TrackID") as Label).Text);
                        tracknumber= int.Parse((PlayList.Rows[i].FindControl("TrackNumber") as Label).Text);
                        rowsselected++;
                    }
                }
                if(rowsselected != 1)
                {
                    MessageUserControl.ShowInfo("Warning", "Select one track to move.");
                }
                else
                {
                    if (tracknumber == 1)
                    {
                        MessageUserControl.ShowInfo("Information", "Select track cannot be moved up.");
                    }
                    else
                    {
                        MoveTrack(trackid, tracknumber, "up");
                    }
                }
            }
        }
    }

    protected void MoveDown_Click(object sender, EventArgs e)
    {
        if (PlayList.Rows.Count == 0)
        {
            //did the user press the up button without fetching a playlist
            MessageUserControl.ShowInfo("Warning", "No playlist has been retreived.");
        }
        else
        {
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Warning", "No playlist name exits.");
            }
            else
            {
                //check only one row selected
                int trackid = 0;
                int tracknumber = -1;
                int rowsselected = 0;
                CheckBox playlistselection = null;
                //traverse the gridview checking each row for a checked Checkbox
                for (int i = 0; i < PlayList.Rows.Count; i++)
                {
                    //find the checkbox on the indexed gridview row
                    //playlistselection will point to the checkbox
                    playlistselection = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                    if (playlistselection.Checked)
                    {
                        trackid = int.Parse((PlayList.Rows[i].FindControl("TrackID") as Label).Text);
                        tracknumber = int.Parse((PlayList.Rows[i].FindControl("TrackNumber") as Label).Text);
                        rowsselected++;
                    }
                }
                if (rowsselected != 1)
                {
                    MessageUserControl.ShowInfo("Warning", "Select one track to move.");
                }
                else
                {
                    if (tracknumber == PlayList.Rows.Count)
                    {
                        MessageUserControl.ShowInfo("Information", "Select track cannot be moved down.");
                    }
                    else
                    {
                        MoveTrack(trackid, tracknumber, "down");
                    }
                }
            }
        }
    }
    protected void MoveTrack(int trackid, int tracknumber, string direction)
    {
        //call the BLL move method
        //refresh the playlist
        MessageUserControl.TryRun(() =>
        {
            PlaylistTracksController sysmgr = new PlaylistTracksController();
            //sysmgr.Add_TrackToPLaylist(playlistname, username, trackid);
            List<UserPlaylistTrack> results = sysmgr.List_TracksForPlaylist(PlaylistName.Text, 
                User.Identity.Name);
            PlayList.DataSource = results;
            PlayList.DataBind();
        }, "Playlist Track Added", "You have successfully added a new track to your list.");
    }
    protected void DeleteTrack_Click(object sender, EventArgs e)
    {

    }
}