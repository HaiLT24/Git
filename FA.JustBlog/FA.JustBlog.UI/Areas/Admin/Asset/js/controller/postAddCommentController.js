var post = {
    init: function () {
        post.registerEvents();
    },

    registerEvents: function () {
        $('.comment-form').on('click',
            function (e) {
                e.preventDefault();
                var btn = $(this);
                var id = btn.data('id');

                $.ajax({
                    url: "/Post/ChangePublisher",
                    data: {
                        id: id
                    },
                    dataType: "json",
                    type: "POST",
                    success: function (response) {
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