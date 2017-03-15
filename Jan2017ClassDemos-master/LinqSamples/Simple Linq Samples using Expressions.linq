<Query Kind="Expression">
  <Connection>
    <ID>bd054115-557e-480f-be7f-9383653ebdfc</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//dump the table (entity)
Artists

//dump the table (query syntax)
from row in Artists
select row

//dump the table (method syntax)
Artists.Select(row => row)

//using the where
//this is C# therefore for compound wheres use
//appropriate and (&&) and or (||)
from thetrack in Tracks
where thetrack.Milliseconds > 325000 && thetrack.Milliseconds < 330000
select thetrack

//use method syntax to find all customers who have a State value
Customers.Where(row => row.State != null).Select(row => row)

//find all Albums belonging to an artist who's name contains Black
//order by most recent year
from x in Albums
where x.Artist.Name.Contains("Black")
orderby x.ReleaseYear descending
select x

Albums
   .Where (x => x.Artist.Name.Contains ("Black"))
   .OrderByDescending (x => x.ReleaseYear) 
   
// find artists who have a C in their name, order alphabetically
//use an anyonymous datatype to select the specified fields
//off the row instance of Artists
from x in Artists
where x.Name.Contains("C")
orderby x.Name
select new {
	Mary = x.ArtistId,
	Fred = x.Name
}