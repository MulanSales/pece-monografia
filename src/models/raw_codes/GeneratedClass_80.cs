@model Flurfunk.Data.Model.User
@using Flurfunk.Helper
@{
ViewBag.Title = "";
}
@section featured {
@* <section class="featured">
<div class="content-wrapper">
<hgroup class="title">
<h1>@ViewBag.Title</h1>
<h2>@ViewBag.Message</h2>
</hgroup>
<p>
To learn more about ASP.NET MVC visit
<a href="http://asp.net/mvc" title="ASP.NET MVC Website">http://asp.net/mvc</a>.
The page features <mark>videos, tutorials, and samples</mark> to help you get the most from ASP.NET MVC.
If you have any questions about ASP.NET MVC visit
<a href="http://forums.asp.net/1146.aspx/1?MVC" title="ASP.NET MVC Forum">our forums</a>.
</p>
</div>
</section>*@
}
<div class="row">
<div class="span3 sidebar">
<h3>
<img src="@Url.ProfilePicture(Model)" style="height: 55px" />@Model.Name
</h3>
<div ng-controller="sendMessageController">
<form class="form" ng-submit="sendMessage()">
<div class="control-group">
<div class="controls">
<textarea ng-model="message" ng-change="messageUpdate()" placeholder="Teile es mit allen…" class="span3" id="textarea" rows="3"></textarea>
</div>

<button type="submit" class="btn btn-{{messageButtonClass}}">Senden</button>
<h3 style="float: right">{{140 - message.length}}</h3>

</div>
</form>
</div>

<div ng-controller='filterController'>
<h2>Filter</h2>
<div class="input-append">

<input class="span3" ng-model="filterKeyword" ng-change="filterUpdate()" placeholder="nach Schlagwort filtern" size="16" type="text">
<button ng-click="addFilter()" class="btn {{addButtonClass}}" type="button">+</button>

</div>
<ul class="nav nav-pills nav-stacked filters" >
@* <li class="active"><a href="#">alle Nachrichten</a></li>*@
<li ng-repeat='filter in filters' id="filter-{{$index}}"><a href="#" ng-click="selectFilter($index)" ><span class="keyword">{{filter}}</span><button ng-click="removeFilter(filter)" eat-click class="close">&times;</button> </a></li>
</ul>
</div>

<h2>Gruppen</h2>
<div class="input-append">
<input class="span3" id="appendedInputButton" placeholder="neue Gruppe erstellen" size="16" type="text">
<button class="btn" type="button">+</button>
</div>
<ul class="nav nav-pills nav-stacked">
<li><a href="#">Projekt A<button class="close">&times;</button></a></li>
<li><a href="#">Mittagessen Gruppe<button class="close">&times;</button></a></li>
<li><a href="#">Abteilung B<button class="close">&times;</button></a> </li>
</ul>
</div>

<div ng-controller='loadMessageController'>
<div infinite-scroll='nextPage()' infinite-scroll-disabled='busy'>
<div class="row span9" style="float: right" ng-repeat='item in items'>
<div id="{{item._id}}" class="span8 textmessage">
<span class="messageImage" >
<img src="{{getUserImageUrl(item.Creator.ProviderId)}}"/>
</span>
<p class="span6">{{item.Text}}</p>
<small class="span4">von {{item.Creator.Name}} am {{getTimeString(item.Created)}}</small>
</div>
</div>
@*  <div ng-show='busy'>Loading data...</div>*@
</div>
</div>
</div>
