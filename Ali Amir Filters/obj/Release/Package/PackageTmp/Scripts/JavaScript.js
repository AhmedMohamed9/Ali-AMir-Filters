
//add active class to navigation bar
$('.nav.navbar-nav').find('[href="' + window.location.pathname + '"]').parent().addClass('active')
