var post = {
    init: function () {
        post.registerEvents();
    },

    registerEvents: function () {
        $('.btn-active').off('click').on('click',
            function (e) {
                e.preventDefault();
                var btn = $(this);
                var id = btn.data('id');

                $.ajax({
                    url: "/Admin/PostAdmin/ChangePublisher",
                    data: {
                        id: id
                    },
                    dataType: "json",
                    type: "POST",
                    success: function(response) {
                        console.log(response);
                        if (response.publisher == true) {
                            btn.text("Activated");
                        } else {
                            btn.text("Inactivated");
                        }
                    }
                });
            });
    }
}
post.init();