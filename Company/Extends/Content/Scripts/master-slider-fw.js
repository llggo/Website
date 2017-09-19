var MSfullWidth = function () {

    return {
        
        //Master Slider - Full Width
        initMSfullWidth: function () {
		    var slider = new MasterSlider();
		    slider.setup('masterslider' , {
		        width:1024,
		        height:0,
		        fullwidth: true,
		        autoHeight:true,
		        centerControls:false,
		        speed: 100,
                //space:20,
                view: 'mask',
				loop:true,
				autoplay: true,
				layersMode: "center",
		    });
		    slider.control('arrows');
		    slider.control('bullets' ,{autohide:true});
        },
        //Master Slider - Full Width
        initMSfullWidth2: function () {
            var slider = new MasterSlider();
            slider.setup('masterslider2', {
                width: 1024,
                height: 0,
                fullwidth: true,
                autoHeight: true,
                centerControls: false,
                speed: 100,
                //space:20,
                view: 'mask',
                loop: true,
                autoplay: true,
                layersMode: "center",
            });
        },

    };
}();