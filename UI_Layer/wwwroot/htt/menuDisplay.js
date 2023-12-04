$(window).on('load', function () {
        var url = window.location;      

        $('ul.nav-sidebar a').filter(function () {
            if (this.href) {
                return this.href == url || url.href.indexOf(this.href) == 0;
            }
        }).addClass('active');
        // for the treeview
        $('ul.nav-treeview a').filter(function () {
            if (this.href) {
                return this.href == url || url.href.indexOf(this.href) == 0;

            }
        }).parentsUntil(".nav-sidebar > .nav-treeview").css({ 'display': 'block' }).addClass('menu-open').addClass("menu-is-opening").prev('a').addClass('active');
   
        $('ul.nav-sidebar a').filter(function() {
            var currentUrl = this.href;
            if (this.href.slice(-1) === "#") {
                currentUrl = this.href.replace("#", "");
            }
            if (this.baseURI == currentUrl) {
                var selectors = document.getElementsByClassName('nav-link active');
                var olCaption = $("#olCaption");
                olCaption.empty(); // Clear existing content
                Array.from(selectors).forEach(function (element) {
                    var text = element.textContent.trim(); // Get the text content of the element
                    olCaption.append('<li class="breadcrumb-item">' + text + '</li>');
                });
              
            }
        });
        $('.featured__controls li').on('click', function () {
            $('.featured__controls li').removeClass('active');
            $(this).addClass('active');
        });
    });