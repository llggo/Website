var RevolutionSlider = function () {

    return {
        
        //Revolution Slider - Full Width
        initRSfullWidth: function () {
		    var revapi;
	        jQuery(document).ready(function() {
	            revapi = jQuery('.tp-banner').revolution(
	            {
	                delay:8000,
	                startwidth:1170,
	                startheight:350,
	                hideThumbs: 10,
	                onHoverStop: "off",
									navigationStyle:"none"
	            });
	        });
        },

        //Revolution Slider - Full Width
        initRSfullWidth2: function () {
            var revapi;
            jQuery(document).ready(function () {
                revapi = jQuery('.tp-banner-2').revolution(
	            {
	                delay: 5000,
	                startwidth: 1170,
	                startheight: 600,
	                hideThumbs: 10,
	                onHoverStop: "off",
	                navigationStyle: "none"
	            });
            });
        },

        //Revolution Slider - Full Screen Offset Container
        initRSfullScreenOffset: function () {
		    var revapi;
	        jQuery(document).ready(function() {
	           revapi = jQuery('.tp-banner').revolution(
	            {
	                delay:15000,
	                startwidth:1170,
	                startheight:400,
	                hideThumbs:10,
	                fullWidth:"off",
	                fullScreen:"on",
	                hideCaptionAtLimit: "",
	                dottedOverlay:"twoxtwo",
	                navigationStyle:"preview4",
	                fullScreenOffsetContainer: ".header"
	            });
	        });
        }        

    };
}();        