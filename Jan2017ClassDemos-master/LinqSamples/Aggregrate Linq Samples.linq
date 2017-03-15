<Query Kind="Statements">
  <Connection>
    <ID>bd054115-557e-480f-be7f-9383653ebdfc</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

var ex1 = from x in Artists
		select new{
			Artist = x.Name,
			NumberOfAlbums= x.Albums.Count()
		};
ex1.Dump("ex1");

var ex2 = from x in Artists
		where x.Albums.Count() == 2
		select x.Name;
ex2.Dump("ex2");

//so, sometimes you may need to do multiple query steps
//to get partial information to use on future query steps
var ex3a = (from x in MediaTypes
	select x.Tracks.Count()).Max();
//.Dump("Max tracks");

var ex3b = from x in MediaTypes
			where x.Tracks.Count() == ex3a
			select new{
				Type = x.Name,
				TrackCount = x.Tracks.Count()
			};
ex3b.Dump("Most popular Media");

//where clause can contain subqueries
var ex3c = from x in MediaTypes
			where x.Tracks.Count() == (from y in MediaTypes
										select y.Tracks.Count()).Max()
			select new{
				Type = x.Name,
				TrackCount = x.Tracks.Count()
			};
ex3c.Dump("Most popular Media");

//you can mix query syntax with method syntax
var ex3d = from x in MediaTypes
			where x.Tracks.Count() == (MediaTypes.Select(y => y.Tracks.Count())).Max()
			select new{
				Type = x.Name,
				TrackCount = x.Tracks.Count()
			};
ex3d.Dump("Most popular Media");
//be aware that when you use aggregrate methods
//where the collection needs to have at least a single
//record, you will need to test for the situation
//in this example, there are 2 albums where the tracks
//have not yet been entered
//the where clause will ensure these two albums
//are not included in the query
var ex4 = from x in Albums
			where x.Tracks.Count() > 0
			orderby x.Tracks.Count(), x.Title
			select new{
				title = x.Title,
				TotalTracksforAlbum = x.Tracks.Count(),
				TotalPriceforAlbumTracks = x.Tracks.Sum(y=>y.UnitPrice),
				AverageTrackLengthA = x.Tracks.Average(y=>y.Milliseconds)/1000,
				AverageTrackLengthB = x.Tracks.Average(y=>y.Milliseconds/1000)
			};
ex4.Dump("ex4");

//using union()
//(query).union(queryn).OrderBy(sort)
var ex5 = (from x in Albums
			where x.Tracks.Count() > 0
			//orderby x.Tracks.Count(), x.Title
			select new{
				title = x.Title,
				TotalTracksforAlbum = x.Tracks.Count(),
				TotalPriceforAlbumTracks = x.Tracks.Sum(y=>y.UnitPrice),
				AverageTrackLengthA = x.Tracks.Average(y=>y.Milliseconds)/1000.0,
				AverageTrackLengthB = x.Tracks.Average(y=>y.Milliseconds/1000.0)
			}).Union(from x in Albums
			where x.Tracks.Count() == 0
			//orderby x.Tracks.Count(), x.Title
			select new{
				title = x.Title,
				TotalTracksforAlbum = 0,
				TotalPriceforAlbumTracks = 0.00m,
				AverageTrackLengthA = 0.00,
				AverageTrackLengthB = 0.00
			}).OrderBy(y => y.TotalTracksforAlbum).ThenBy(y=>y.title);
ex5.Dump("ex5");
//create a list of employees and their related customers they support
//aggregrate methods are executed against collections
//the many end of a navigation property is considered a collection
//when using a subquery with the collection source being a navigation 
//   property, only the records from the navigation collection (Customer) 
//   that belong to the  navigation parent (Employee) are considered
var ex6 =from x in Employees
		where x.SupportRepIdCustomers.Count() >0
		select new{
			Title = x.Title,
			Name = x.FirstName + " " + x.LastName,
			Phone =x.Phone,
			Customers = from y in x.SupportRepIdCustomers
						orderby y.LastName, y.FirstName
						select new{
							Name = y.LastName + ", " + y.FirstName,
							Company = y.Company,
							Phone = y.Phone,
							Email = y.Email
						}
		};
ex6.Dump("ex6");