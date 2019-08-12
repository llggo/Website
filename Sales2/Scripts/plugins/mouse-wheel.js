var MouseWheel = function () {

    return {

        initMouseWheel: function () {
            jQuery('.slider-snap').noUiSlider({
                start: [ 50, 800 ],
                snap: true,
                connect: true,
                range: {
                    'min': 0,
                    '10%': 50,
                    '20%': 100,
                    '30%': 200,
                    '40%': 400,
                    '50%': 800,
                    '60%': 1600,
                    '70%': 3200,
                    '80%': 6400,
                    '90%': 12800,
                    'max': 25600
                }
            });
            jQuery('.slider-snap').Link('lower').to(jQuery('.slider-snap-value-lower'));
            jQuery('.slider-snap').Link('upper').to(jQuery('.slider-snap-value-upper'));
        }

    };

}();        