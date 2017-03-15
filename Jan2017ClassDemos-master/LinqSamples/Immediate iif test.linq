<Query Kind="Expression">
  <Connection>
    <ID>bd054115-557e-480f-be7f-9383653ebdfc</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//operater is called the ? (vb calls this the iif)
//syntax to us this operate is condition?truevalue:falsevalue
//truevalue or falsevalue must resolve to a single value
//this single value can be create using a string, number, date, or expression
//this is very useful if your application has an enum and not a table
from x in Tracks
select new{
	Name = x.Name,
	Album = x.Album.Title,
	MediaType = x.MediaTypeId==1?"bob":
				x.MediaTypeId ==2?"fred":
				x.MediaTypeId ==3?"mary":
				x.MediaTypeId ==4?"puppy":"george"
				
}