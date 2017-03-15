<Query Kind="Statements">
  <Connection>
    <ID>bd054115-557e-480f-be7f-9383653ebdfc</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//query syntax style
var results = from x in Albums
		where x.Artist.Name.Contains("Black")
		orderby x.ReleaseYear descending
		select x;
//.Dump() which is a Linqpad method NOT C#
results.Dump();

//method syntax style
var methodresults = Albums
   .Where (x => x.Artist.Name.Contains ("Black"))
   .OrderByDescending (x => x.ReleaseYear);
methodresults.Dump();