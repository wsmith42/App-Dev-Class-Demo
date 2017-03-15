<Query Kind="Expression">
  <Connection>
    <ID>bd054115-557e-480f-be7f-9383653ebdfc</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//basic grouping
//can use navigation to point to the value to group on
from x in Albums
group x by x.Artist.Name

//store your grouping results into a temporary data collection
//then select from the collection
from x in Albums
group x by x.Artist.Name into result
select result

//you can create a specified data collection
//using the results of a grouping
//to access the group key value use .Key
//you can use the grouped collection in a subquery
from x in Albums
group x by x.Artist.Name into result
select new{
		artist = result.Key,
		albums = from y in result
				 select new{
				 	title = y.Title,
					ryear = y.ReleaseYear,
					label = y.ReleaseLabel
				 }
}
//when grouping by multiple fields
//you need to create new set
//grouping is left to right for order of importance
from x in Tracks
group x by new {x.MediaTypeId, GN = x.GenreId} into results
select results