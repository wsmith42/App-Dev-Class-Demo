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
    [DataObject]
    public class AlbumController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<AlbumArtist> ListAlbumsbyArtist()
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Albums
                              orderby x.Artist.Name
                              select new AlbumArtist
                              {
                                  Artist = x.Artist.Name,
                                  Title = x.Title,
                                  ReleaseYear = x.ReleaseYear,
                                  ReleaseLabel = x.ReleaseLabel
                              };
                return results.ToList();
            }
        }//eom

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<Album> Albums_GetForArtistbyName(string name)
        {
            using (var context = new ChinookContext())
            {
                //this is using Linq
                //this is using the method syntax of the query
                var results = context.Albums
                            .Where(x => x.Artist.Name.Contains(name))
                            .OrderByDescending(x => x.ReleaseYear);
                //remember if you have used .Dump() in LinqPad to view
                //your contents of the query, .Dump() is a LinqPad method
                //it is NOT C#
                return results.ToList();
            }
        }//eom

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<ArtistAlbumReleases> ArtistAlbumReleases_List()
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Albums
                              group x by x.Artist.Name into result
                              select new ArtistAlbumReleases
                              {
                                  Artist = result.Key,
                                  Albums = (from y in result
                                           select new AlbumRelease
                                           {
                                               Title = y.Title,
                                               RYear = y.ReleaseYear,
                                               Label = y.ReleaseLabel
                                           }).ToList()
                              };
                return results.ToList();
            }
        }//eom
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SelectionList> List_AlbumTitles()
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Albums
                              orderby x.Title
                              select new SelectionList
                              {
                                  IDValueField = x.AlbumId,
                                  DisplayText = x.Title
                              };
                return results.ToList();
            }
        }

        #region CRUD

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Albums_GetbyTitle(string title)
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Albums
                              where x.Title.Contains(title)
                              select x;
                return results.ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Albums_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Album Albums_Get(int albumid)
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.Find(albumid);
            }
        }
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void Albums_Add(Album item)
        {
           
            using (var context = new ChinookContext())
            { 
                //any business rules
                context.Albums.Add(item);
                context.SaveChanges();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Albums_Update(Album item)
        {
            using (var context = new ChinookContext())
            {
                //any business rules

                //any data refinements
                //review of using iif
                //Release Label can be a null string
                //we do not wish to store an empty string
                context.Albums.Attach(item);
                item.ReleaseLabel = string.IsNullOrEmpty(item.ReleaseLabel) ?
                                            null : item.ReleaseLabel;

                //update the existing instance of trackinfo on the database
                context.Entry(item).State =
                    System.Data.Entity.EntityState.Modified;

                //update command if updating selected fields
                //context.Entry(instancevariablename).Property(y => y.columnname).IsModified = true;
                
                context.SaveChanges();
            }
        }

        //the delete is an overload method technique

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Albums_Delete(Album item)
        {
            Albums_Delete(item.AlbumId);
        }

        public void Albums_Delete(int albumid)
        {
            using (var context = new ChinookContext())
            {
                //any business rules

                //do the delete
                //find the existing record on the database
                var existing = context.Albums.Find(albumid);

                if (existing == null)
                {
                    throw new Exception("Album does not exists on database.");
                }
                //delete the record from the database
                context.Albums.Remove(existing);
                //commit the transaction
                context.SaveChanges();
            }
        }
        #endregion
    }//eoc
}//eon
