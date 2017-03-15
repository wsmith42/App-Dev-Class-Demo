using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Enitities;
using Chinook.Data.DTOs;
using Chinook.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {
                var results = (from x in context.Playlists
                               where x.Name == playlistname
                                && x.UserName == username
                               select x).FirstOrDefault();
                var theTracks = from x in context.PlaylistTracks
                                where x.PlaylistId == results.PlaylistId
                                select new UserPlaylistTrack
                                {
                                    TrackID = x.TrackId,
                                    TrackNumber = x.TrackNumber,
                                    TrackName = x.Track.Name,
                                    Milliseconds = x.Track.Milliseconds,
                                    UnitPrice = x.Track.UnitPrice
                                };
                return theTracks.ToList();
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookContext())
            {
                //does the playlist already exist
                Playlist exists = (from x in context.Playlists
                                  where x.UserName.Equals(username)
                                     && x.Name.Equals(playlistname)
                                  select x).FirstOrDefault();
                int tracknumber = 0;
                PlaylistTrack newtrack = null;
                if (exists == null)
                {
                    //create the new Playlist
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    exists = context.Playlists.Add(exists);
                    tracknumber = 1;
                }
                else
                {
                    //the playlist already exists
                    //and the query has given us the instance
                    //of that playlist from the database
                    //generate the next tracknumber
                    tracknumber = exists.PlaylistTracks.Count() + 1;

                    //on our sample, playlist tracks for a playlist 
                    //are unique
                    newtrack = exists.PlaylistTracks.SingleOrDefault(x => x.TrackId == trackid);
                    if (newtrack != null)
                    {
                        throw new Exception("Playlist already has requested track.");
                    }
                }
                
                //you have a playlist
                //you know the track will be unique
                //create the new track
                newtrack = new PlaylistTrack();
                newtrack.TrackId = trackid;
                newtrack.TrackNumber = tracknumber;
                //since i am using the navigation property of the
                //playlist to get to playlisttrack
                //the SaveChanges will fill the PlaylsitID
                //from either the HashSet or from the existing instance
                exists.PlaylistTracks.Add(newtrack);


                context.SaveChanges();
            }
        }//eom
    }
}
