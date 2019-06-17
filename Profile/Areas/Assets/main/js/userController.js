var user = {
    init: function () {
        user.registerEvent()
    },
    registerEvent: function () {
        $('.btn-outline-success').off('click').on('click', function (e) {
            e.preventDefault()
            var btnStatus = $(this);
            var id = btnStatus.data('id');
            $.ajax({
                url: '/Admin/Users/ChangStatus',
                data: { id: id },
                dataType: 'json',
                type: 'POST',
                success: function (response) { 
                    if (response.status == true) {
                        btnStatus.text('Enable');
                    }
                    else {
                        btnStatus.text('Disable');
                    }
                }
            });
        });
    }

}
user.init();