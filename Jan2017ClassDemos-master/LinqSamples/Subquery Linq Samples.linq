<Query Kind="Expression">
  <Connection>
    <ID>bd054115-557e-480f-be7f-9383653ebdfc</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//list all Artists and their albums
//Albums will be a collection created by a subquery
from x in Artists
select new {
	Artist = x.Name,
	Albums = from y in x.Albums
			 select new {
				Title = y.Title,
				Year = y.ReleaseYear
			}
}