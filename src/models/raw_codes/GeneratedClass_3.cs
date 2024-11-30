@{
ViewBag.Title = "Registreer je vandaag bij de Oogstplanner";
}

<div class="flowtype-area">

<!-- START RESPONSIVE RECTANGLE LAYOUT -->

<!-- Top bar -->
<div id="top">
</div>

<!-- Fixed main screen -->
<div id="login" class="bg imglogin">
<div class="row">
<div class="mainbox col-md-12 col-sm-12 col-xs-12">
<div class="panel panel-info">
<div class="panel-heading">
<div class="panel-title">Registreer je vandaag bij de Oogstplanner</div>
<div class="panel-side-link">
<a id="signin-link" href="#">Inloggen</a>
</div>
</div>
<div class="panel-body">
@{ Html.RenderPartial("_RegisterForm"); }

</div><!-- End panel body -->
</div><!-- End panel -->
</div><!-- End signup box -->
</div><!-- End row -->
</div><!-- End main login div -->

<!-- END RESPONSIVE RECTANGLES LAYOUT -->

</div><!-- End flowtype-area -->
